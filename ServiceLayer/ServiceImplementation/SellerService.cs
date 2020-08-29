//-----------------------------------------------------------------------
// <copyright file="SellerService.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="SellerService" />.
    /// </summary>
    public class SellerService
    {
        /// <summary>
        /// Defines the Logger.
        /// </summary>
        private static readonly Log4NetWrapper Logger = new Log4NetWrapper(typeof(SellerService));

        /// <summary>
        /// Defines the sellerRepository.
        /// </summary>
        private ISellerRepository sellerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SellerService"/> class.
        /// </summary>
        /// <param name="sellerRepository">The sellerRepository<see cref="ISellerRepository"/>.</param>
        public SellerService(ISellerRepository sellerRepository)
        {
            this.sellerRepository = sellerRepository;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="Seller"/>.</param>
        public void Delete(Seller entity)
        {
            this.sellerRepository.Delete(entity);
        }

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Seller}"/>.</returns>
        public IEnumerable<Seller> GetAll()
        {
            return this.sellerRepository.Get().OrderBy(seller => seller.Person.Name).ToList();
        }

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The <see cref="Seller"/>.</returns>
        public Seller GetByID(object id)
        {
            return this.sellerRepository.GetByID(id);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="Seller"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Insert(Seller entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.sellerRepository.Insert(entity);
            }
            else
            {
                Logger.Info("Person insertion failed");
            }

            return results;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="Seller"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Update(Seller entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.sellerRepository.Update(entity);
            }
            else
            {
                Logger.Info("Person update failed");
            }

            return results;
        }
    }
}
