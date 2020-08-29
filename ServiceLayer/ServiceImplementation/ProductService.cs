//-----------------------------------------------------------------------
// <copyright file="ProductService.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="ProductService" />.
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Defines the Logger.
        /// </summary>
        private static readonly Log4NetWrapper Logger = new Log4NetWrapper(typeof(ProductService));

        /// <summary>
        /// Defines the productRepository.
        /// </summary>
        private IProductRepository productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">The productRepository<see cref="IProductRepository"/>.</param>
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="Product"/>.</param>
        public void Delete(Product entity)
        {
            this.productRepository.Delete(entity);
        }

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Product}"/>.</returns>
        public IEnumerable<Product> GetAll()
        {
            return this.productRepository.Get().OrderBy(product => product.Name).ToList();
        }

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The <see cref="Product"/>.</returns>
        public Product GetByID(object id)
        {
            return this.productRepository.GetByID(id);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="Product"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Insert(Product entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.productRepository.Insert(entity);
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
        /// <param name="entity">The entity<see cref="Product"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Update(Product entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.productRepository.Update(entity);
            }
            else
            {
                Logger.Info("Person update failed");
            }

            return results;
        }
    }
}
