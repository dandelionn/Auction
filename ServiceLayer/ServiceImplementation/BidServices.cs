//-----------------------------------------------------------------------
// <copyright file="BidServicesImplementation.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ServiceLayer.ServiceImplementation
{
    using DataMapper;
    using DomainModel;
    using DomainModel.Validators;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Linq;

    public class BidServices: IBidServices
    {
        private IBidRepository bidRepository;

        public BidServices(IBidRepository bidRepository)
        {
            this.bidRepository = bidRepository;
        }

        public void Delete(Bid entity)
        {
            bidRepository.Delete(entity);
        }

        public IEnumerable<Bid> GetAll()
        {
            return bidRepository.Get().OrderBy(bid => bid.Value).ToList();
        }

        public Bid GetByID(object id)
        {
            return bidRepository.GetByID(id);
        }

        private static bool BidValueBiggerThanMaxBidValue(Bid bid)
        {
            var maxRaisePercentage = decimal.Parse(ConfigurationManager.AppSettings.Get("MaxRaisePercentage"));
            var maxBidAddedValue = bid.Auction.CurrentPrice * maxRaisePercentage;
            var addedValue = bid.Value - bid.Auction.CurrentPrice;
            return addedValue > maxBidAddedValue;
        }

        private static bool BidValueLessOrEqualThanAuctionCurrentPrice(Bid bid)
        {
            return bid.Auction.CurrentPrice >= bid.Value;
        }

        private static bool BidderOwnsThePreviousBid(Bid bid)
        {
            var noBids = !bid.Auction.Bids.Any();
            return noBids ? false : bid.Auction.Bids.Last().Bidder == bid.Bidder;
        }

        private static IList<ValidationResult> ApplyAdditionalRules(Bid bid)
        {
            IList<ValidationResult> validationResult = new List<ValidationResult>();
            if(BidValueBiggerThanMaxBidValue(bid))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BidValueBiggerThanMaxBidValue));
            }
            if(BidValueLessOrEqualThanAuctionCurrentPrice(bid))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BidValueLessOrEqualThanAuctionCurrentPrice));
            }
            if(BidderOwnsThePreviousBid(bid))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BidderOwnsThePreviousBid));
            }

            return validationResult;
        }

        public IList<ValidationResult> Insert(Bid entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (!results.Any())
            {
                results = results.Concat(ApplyAdditionalRules(entity)).ToList();
            }
            if (results.Count == 0)
            {
                bidRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(Bid entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                bidRepository.Update(entity);
            }

            return results;
        }
    }
}
