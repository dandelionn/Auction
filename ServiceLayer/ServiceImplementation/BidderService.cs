//-----------------------------------------------------------------------
// <copyright file="BidderService.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataLayer;
    using DataMapper;
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="BidderService" />.
    /// </summary>
    public class BidderService
    {
        /// <summary>
        /// Defines the Logger.
        /// </summary>
        private static readonly Log4NetWrapper Logger = new Log4NetWrapper(typeof(BidderService));

        /// <summary>
        /// Defines the bidderRepository.
        /// </summary>
        private IBidderRepository bidderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BidderService"/> class.
        /// </summary>
        /// <param name="bidderRepository">The bidderRepository<see cref="IBidderRepository"/>.</param>
        public BidderService(IBidderRepository bidderRepository)
        {
            this.bidderRepository = bidderRepository;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="Bidder"/>.</param>
        public void Delete(Bidder entity)
        {
            this.bidderRepository.Delete(entity);
        }

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Bidder}"/>.</returns>
        public IEnumerable<Bidder> GetAll()
        {
            return this.bidderRepository.Get().OrderBy(bidder => bidder.Person.Name).ToList();
        }

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The <see cref="Bidder"/>.</returns>
        public Bidder GetByID(object id)
        {
            return this.bidderRepository.GetByID(id);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="Bidder"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Insert(Bidder entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.bidderRepository.Insert(entity);
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
        /// <param name="entity">The entity<see cref="Bidder"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Update(Bidder entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.bidderRepository.Update(entity);
            }
            else
            {
                Logger.Info("Entity update failed!");
            }

            return results;
        }
    }
}
