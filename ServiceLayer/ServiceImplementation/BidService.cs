//-----------------------------------------------------------------------
// <copyright file="BidService.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Linq;
    using DataLayer;
    using DataMapper;
    using DomainModel;
    using DomainModel.Validators;

    /// <summary>
    /// Defines the <see cref="BidService" />.
    /// </summary>
    public class BidService : IBidService
    {
        /// <summary>
        /// Defines the Logger.
        /// </summary>
        private static readonly Log4NetWrapper Logger = new Log4NetWrapper(typeof(BidService));

        /// <summary>
        /// Defines the bidRepository.
        /// </summary>
        private IBidRepository bidRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BidService"/> class.
        /// </summary>
        /// <param name="bidRepository">The bidRepository<see cref="IBidRepository"/>.</param>
        public BidService(IBidRepository bidRepository)
        {
            this.bidRepository = bidRepository;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="Bid"/>.</param>
        public void Delete(Bid entity)
        {
            this.bidRepository.Delete(entity);
        }

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Bid}"/>.</returns>
        public IEnumerable<Bid> GetAll()
        {
            return this.bidRepository.Get().OrderBy(bid => bid.Value).ToList();
        }

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The <see cref="Bid"/>The entity identified by the id.</returns>
        public Bid GetByID(object id)
        {
            return this.bidRepository.GetByID(id);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="Bid"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Insert(Bid entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (!results.Any())
            {
                results = results.Concat(ApplyAdditionalRules(entity)).ToList();
            }

            if (results.Count == 0)
            {
                this.bidRepository.Insert(entity);
            }
            else
            {
                Logger.Info("Bid insertion failed!");
            }

            return results;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="Bid"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Update(Bid entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.bidRepository.Update(entity);
            }
            else
            {
                Logger.Info("Bid update failed!");
            }

            return results;
        }

        /// <summary>
        /// The ApplyAdditionalRules.
        /// </summary>
        /// <param name="bid">The bid<see cref="Bid"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        private static IList<ValidationResult> ApplyAdditionalRules(Bid bid)
        {
            IList<ValidationResult> validationResult = new List<ValidationResult>();
            if (BidValueBiggerThanMaxBidValue(bid))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BidValueBiggerThanMaxBidValue));
                Logger.Info("Bid value bigger that max bid value!");
            }

            if (BidValueLessOrEqualThanAuctionCurrentPrice(bid))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BidValueLessOrEqualThanAuctionCurrentPrice));
                Logger.Info("Bid value less or equal than auction current price!");
            }

            if (BidderOwnsThePreviousBid(bid))
            {
                validationResult.Add(new ValidationResult(ErrorMessages.BidderOwnsThePreviousBid));
                Logger.Info("Bidder owns the previous bid!");
            }

            return validationResult;
        }

        /// <summary>
        /// The BidValueLessOrEqualThanAuctionCurrentPrice.
        /// </summary>
        /// <param name="bid">The bid<see cref="Bid"/>.</param>
        /// <returns>True if bid value is less or equal to the current auction price, false otherwise.</returns>
        private static bool BidValueLessOrEqualThanAuctionCurrentPrice(Bid bid)
        {
            return bid.Auction.CurrentPrice >= bid.Value;
        }

        /// <summary>
        /// The BidValueBiggerThanMaxBidValue.
        /// </summary>
        /// <param name="bid">The bid<see cref="Bid"/>.</param>
        /// <returns>True, if bid value bigger than max bid value.</returns>
        private static bool BidValueBiggerThanMaxBidValue(Bid bid)
        {
            var maxRaisePercentage = decimal.Parse(ConfigurationManager.AppSettings.Get("MaxRaisePercentage"));
            var maxBidAddedValue = bid.Auction.CurrentPrice * maxRaisePercentage;
            var addedValue = bid.Value - bid.Auction.CurrentPrice;
            return addedValue > maxBidAddedValue;
        }

        /// <summary>
        /// The BidderOwnsThePreviousBid.
        /// </summary>
        /// <param name="bid">The bid<see cref="Bid"/>.</param>
        /// <returns>True if bidder owns the previous bid.</returns>
        private static bool BidderOwnsThePreviousBid(Bid bid)
        {
            var noBids = !bid.Auction.Bids.Any();
            return noBids ? false : bid.Auction.Bids.Last().Bidder == bid.Bidder;
        }
    }
}
