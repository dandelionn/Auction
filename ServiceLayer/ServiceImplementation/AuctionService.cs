//-----------------------------------------------------------------------
// <copyright file="AuctionService.cs" company="Transilvania University of Brasov">    
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
    using DataLayer;
    using DataMapper;
    using DomainModel;
    using DomainModel.Validators;

    /// <summary>
    /// Defines the <see cref="AuctionService" />.
    /// </summary>
    public class AuctionService : IAuctionService
    {
        /// <summary>
        /// Defines the Logger.
        /// </summary>
        private static readonly Log4NetWrapper Logger = new Log4NetWrapper(typeof(AuctionService));

        /// <summary>
        /// Defines the auctionRepository.
        /// </summary>
        private IAuctionRepository auctionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        /// <param name="auctionService">The auctionService<see cref="IAuctionRepository"/>.</param>
        public AuctionService(IAuctionRepository auctionService)
        {
            this.auctionRepository = auctionService;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="Auction"/>.</param>
        public void Delete(Auction entity)
        {
            this.auctionRepository.Delete(entity);
        }

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Auction}"/>.</returns>
        public IEnumerable<Auction> GetAll()
        {
            return this.auctionRepository.Get().OrderBy(auction => auction.Name).ToList();
        }

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The <see cref="Auction"/>.</returns>
        public Auction GetByID(object id)
        {
            return this.auctionRepository.GetByID(id);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="Auction"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Insert(Auction entity)
        {
            SetAuctionStatus(entity);
            var results = EntityValidator.IsEntityValid(entity);
            if (!results.Any())
            {
                results = results.Concat(ApplyAdditionalInsertRules(entity)).ToList();
            }

            if (results.Count == 0)
            {
                this.auctionRepository.Insert(entity);
            }
            else
            {
                Logger.Info("Entity insertion failed!");
            }

            return results;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="Auction"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Update(Auction entity)
        {
            this.UpdateAuctionStatus(entity.Id);
            var results = EntityValidator.IsEntityValid(entity);
            if (!results.Any())
            {
                results = results.Concat(this.ApplyAdditionalUpdateRules(entity)).ToList();
            }

            if (results.Count == 0)
            {
                this.auctionRepository.Update(entity);
            }
            else
            {
                Logger.Info("Entity update failed!");
            }

            return results;
        }

        /// <summary>
        /// The AnyProductOfCategory.
        /// </summary>
        /// <param name="products">The products<see cref="List{Product}"/>.</param>
        /// <param name="category">The category<see cref="Category"/>.</param>
        /// <returns>True if found, false otherwise.</returns>
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

        /// <summary>
        /// The GetAuctionsStartedAndNotFinalizedForCategory.
        /// </summary>
        /// <param name="auctions">The auctions<see cref="List{Auction}"/>.</param>
        /// <param name="category">The category<see cref="Category"/>.</param>
        /// <returns>The count of auctions meeting the conditions.</returns>
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

        /// <summary>
        /// The GetAuctionsStartedAndNotFinalized.
        /// </summary>
        /// <param name="auctions">The auctions<see cref="List{Auction}"/>.</param>
        /// <returns>The count of auctions meeting the conditions.</returns>
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

        /// <summary>
        /// The TooManyAuctionsStartedAndNotFinalized.
        /// </summary>
        /// <param name="auction">The auction<see cref="Auction"/>.</param>
        /// <returns>True if too many auctions started and not finalized, false otherwise.</returns>
        private static bool TooManyAuctionsStartedAndNotFinalized(Auction auction)
        {
            var auctionsStartedAndNotFinalized = GetAuctionsStartedAndNotFinalized(auction.Seller.Auctions);
            var maxAuctionsStartedAndNotFinalized = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalized"));
            return auctionsStartedAndNotFinalized > maxAuctionsStartedAndNotFinalized;
        }

        /// <summary>
        /// The GetAuctionProductsCategories.
        /// </summary>
        /// <param name="products">The products<see cref="IList{Product}"/>.</param>
        /// <returns>A list of categories.</returns>
        private static IList<Category> GetAuctionProductsCategories(IList<Product> products)
        {
            IList<Category> categories = new List<Category>();
            foreach (var product in products)
            {
                categories = categories.Union(product.Categories).ToList();
            }

            return categories;
        }

        /// <summary>
        /// The TooManyAuctionsStartedAndNotFinalizedForCategory.
        /// </summary>
        /// <param name="auction">The auction<see cref="Auction"/>.</param>
        /// <returns>True if there are too many auctions started and not finalized for category, false otherwise.</returns>
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

        /// <summary>
        /// The SellerBanned.
        /// </summary>
        /// <param name="auction">The auction<see cref="Auction"/>.</param>
        /// <returns>True if seller is banned, false otherwise.</returns>
        private static bool SellerBanned(Auction auction)
        {
            return auction.Seller.Person.BanEndDate > DateTime.Now.Date;
        }

        /// <summary>
        /// The AuctionPeriodValid.
        /// </summary>
        /// <param name="auction">The auction<see cref="Auction"/>.</param>
        /// <returns>The if auction period is valid, false otherwise.</returns>
        private static bool AuctionPeriodValid(Auction auction)
        {
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            var lastPossibleEndDate = auction.BeginDate.Value.AddMonths(auctionMaxPeriodInMonths);
            return lastPossibleEndDate.Date >= auction.EndDate;
        }

        /// <summary>
        /// The ApplyAdditionalInsertRules.
        /// </summary>
        /// <param name="auction">The auction<see cref="Auction"/>.</param>
        /// <returns>The list of validation results.</returns>
        private static IList<ValidationResult> ApplyAdditionalInsertRules(Auction auction)
        {
            IList<ValidationResult> validationResult = new List<ValidationResult>();
            if (auction.BeginDate < DateTime.Now.Date)
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BeginDateShouldNotBeInThePast));
                Logger.Info("Auction begin date is in the past!");
            }

            if (auction.BeginDate > auction.EndDate)
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BeginDateIsAfterEndDate));
                Logger.Info("Begin date is after end date!");
            }

            if (!AuctionPeriodValid(auction))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.AuctionPeriodIsTooLarge));
                Logger.Info("Auction period is not valid!");
            }

            if (TooManyAuctionsStartedAndNotFinalized(auction))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.TooManyAuctionsStartedAndNotFinalized));
                Logger.Info("Too many auctions started and not finalized!");
            }

            if (TooManyAuctionsStartedAndNotFinalizedForCategory(auction))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory));
                Logger.Info("Too many auctions started and not finalized for category!");
            }

            if (SellerBanned(auction))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.SellerIsBanned));
                Logger.Info("Seller is banned because of low score!");
            }

            return validationResult;
        }

        /// <summary>
        /// The setAuctionStatus.
        /// </summary>
        /// <param name="auction">The auction<see cref="Auction"/>.</param>
        private static void SetAuctionStatus(Auction auction)
        {
            var beginDate = auction.BeginDate.Value.Date;
            var nowDate = DateTime.Now.Date;
            auction.Active = beginDate <= nowDate;
        }

        /// <summary>
        /// The ApplyAdditionalUpdateRules.
        /// </summary>
        /// <param name="auction">The auction<see cref="Auction"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/> of validation results.</returns>
        private IList<ValidationResult> ApplyAdditionalUpdateRules(Auction auction)
        {
            IList<ValidationResult> validationResult = new List<ValidationResult>();
            var dbAuction = this.GetByID(auction.Id);
            if (!dbAuction.Active && DateTime.Now >= dbAuction.BeginDate)
            {
                validationResult.Add(new ValidationResult(ErrorMessages.ChangesNotAllowedInExpiredAuctions));
                Logger.Info("Changes are not allowed in expired auctions!");
            }

            return validationResult;
        }

        /// <summary>
        /// The updateAuctionStatus.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        private void UpdateAuctionStatus(object id)
        {
            var dbAuction = this.GetByID(id);
            var beginDate = dbAuction.BeginDate.Value.Date;
            var endDate = dbAuction.EndDate.Value.Date;
            var nowDate = DateTime.Now.Date;
            dbAuction.Active = beginDate <= nowDate && endDate > nowDate;
            this.auctionRepository.Update(dbAuction);
        }
    }
}
