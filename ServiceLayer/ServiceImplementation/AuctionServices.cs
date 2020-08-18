//-----------------------------------------------------------------------
// <copyright file="AuctionServicesImplementation.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ServiceLayer.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using DomainModel.Validators;

    public class AuctionServices : IAuctionServices
    {
        private IAuctionRepository auctionRepository;

        public AuctionServices(IAuctionRepository auctionService)
        {
            this.auctionRepository = auctionService;
        }

        public void Delete(Auction entity)
        {
            auctionRepository.Delete(entity);
        }

        public IEnumerable<Auction> GetAll()
        {
            return auctionRepository.Get().OrderBy(auction => auction.Name).ToList();
        }

        public Auction GetByID(object id)
        {
            return auctionRepository.GetByID(id);
        }

        private static bool AnyProductOfCategory(List<Product> products, Category category)
        {
            var found = false;
            foreach (var product in products)
            {
                if (product.Categories.Exists(x => (x.Id == category.Id)))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        private static int GetAuctionsStartedAndNotFinalizedForCategory(List<Auction> auctions, Category category)
        {
            var auctionsCount = 0;
            foreach (var auction in auctions)
            {
                var auctionTimeExpired = auction.EndDate.Value.Date <= DateTime.Now.Date;
                var anyProductOfCategory = AnyProductOfCategory(auction.Products, category);
                if (auction.Active && !auctionTimeExpired && anyProductOfCategory)
                {
                    auctionsCount++;
                }
            }
            return auctionsCount;
        }

        private static int GetAuctionsStartedAndNotFinalized(List<Auction> auctions)
        {
            var auctionsCount = 0;
            foreach (var auction in auctions)
            {
                var auctionTimeExpired = auction.EndDate.Value.Date <= DateTime.Now.Date;
                if (auction.Active && !auctionTimeExpired)
                {
                    auctionsCount++;
                }
            }
            return auctionsCount;
        }

        private static bool TooManyAuctionsStartedAndNotFinalized(Auction auction)
        {
            var auctionsStartedAndNotFinalized = GetAuctionsStartedAndNotFinalized(auction.Seller.Auctions);
            var maxAuctionsStartedAndNotFinalized = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalized"));
            return auctionsStartedAndNotFinalized > maxAuctionsStartedAndNotFinalized;
        }

        private static IList<Category> GetAuctionProductsCategories(IList<Product> products)
        {
            IList<Category> categories = new List<Category>();
            foreach (var product in products)
            {
                categories = categories.Union(product.Categories).ToList();
            }
            return categories;
        }

        private static bool TooManyAuctionsStartedAndNotFinalizedForCategory(Auction auction)
        {
            var tooManyAuctions = false;
            var categories = GetAuctionProductsCategories(auction.Products);
            foreach (var category in categories)
            {
                var auctionsStartedAndNotFinalizedForCategory = GetAuctionsStartedAndNotFinalizedForCategory(auction.Seller.Auctions, category);
                var maxAuctionsStartedAndNotFinalizedForCategory = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
                if (auctionsStartedAndNotFinalizedForCategory > maxAuctionsStartedAndNotFinalizedForCategory)
                {
                    tooManyAuctions = true;
                    break;
                }
            }
            return tooManyAuctions;
        }

        private static bool SellerBanned(Auction auction)
        {
            return auction.Seller.Person.BanEndDate > DateTime.Now.Date;
        }

        private static bool AuctionPeriodValid(Auction auction)
        {
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            var lastPossibleEndDate = auction.BeginDate.Value.AddMonths(auctionMaxPeriodInMonths);
            return lastPossibleEndDate.Date >= auction.EndDate;
        }

        private static IList<ValidationResult> ApplyAdditionalInsertRules(Auction auction)
        {
            IList<ValidationResult> validationResult = new List<ValidationResult>();
            if(auction.BeginDate < DateTime.Now.Date)
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BeginDateShouldNotBeInThePast));
            }
            if(auction.BeginDate > auction.EndDate)
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BeginDateIsAfterEndDate));
            }
            if(!AuctionPeriodValid(auction))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.AuctionPeriodIsTooLarge));
            }
            if (TooManyAuctionsStartedAndNotFinalized(auction))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.TooManyAuctionsStartedAndNotFinalized));
            }
            if (TooManyAuctionsStartedAndNotFinalizedForCategory(auction))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory));
            }
            if (SellerBanned(auction))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.SellerIsBanned));
            }
            return validationResult;
        }

        public static void setAuctionStatus(Auction auction)
        {
            var beginDate = auction.BeginDate.Value.Date;
            var nowDate = DateTime.Now.Date;
            auction.Active = beginDate <= nowDate;
        }

        public IList<ValidationResult> Insert(Auction entity)
        {
            setAuctionStatus(entity);
            var results = EntityValidator.IsEntityValid(entity);
            if (!results.Any())
            {
                results = results.Concat(ApplyAdditionalInsertRules(entity)).ToList();
            }
            if (results.Count == 0)
            {
                auctionRepository.Insert(entity);
            }

            return results;
        }

        public void updateAuctionStatus(object id)
        {
            var dbAuction = GetByID(id);
            var beginDate = dbAuction.BeginDate.Value.Date;
            var endDate = dbAuction.EndDate.Value.Date;
            var nowDate = DateTime.Now.Date;
            dbAuction.Active = beginDate <= nowDate && endDate > nowDate;
            auctionRepository.Update(dbAuction);
        }

        private IList<ValidationResult> ApplyAdditionalUpdateRules(Auction auction)
        {
            IList<ValidationResult> validationResult = new List<ValidationResult>();
            var dbAuction = GetByID(auction.Id);
            if (!dbAuction.Active && DateTime.Now >= dbAuction.BeginDate)
            {
                validationResult.Add(new ValidationResult(ErrorMessages.ChangesNotAllowedInExpiredAuctions));
            }

            return validationResult;
        }


        public IList<ValidationResult> Update(Auction entity)
        {
            updateAuctionStatus(entity.Id);
            var results = EntityValidator.IsEntityValid(entity);
            if (!results.Any())
            {
                results = results.Concat(ApplyAdditionalUpdateRules(entity)).ToList();
            }
            if (results.Count == 0)
            {
                auctionRepository.Update(entity);
            }

            return results;
        }
    }
}
