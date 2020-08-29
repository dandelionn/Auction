//-----------------------------------------------------------------------
// <copyright file="CategoryService.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="CategoryService" />.
    /// </summary>
    public class CategoryService
    {
        /// <summary>
        /// Defines the Logger.
        /// </summary>
        private static readonly Log4NetWrapper Logger = new Log4NetWrapper(typeof(CategoryService));

        /// <summary>
        /// Defines the categoryRepository.
        /// </summary>
        private ICategoryRepository categoryRepository;    

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="categoryRepository">The categoryRepository<see cref="ICategoryRepository"/>.</param>
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="Category"/>.</param>
        public void Delete(Category entity)
        {
            this.categoryRepository.Delete(entity);
        }

        /// <summary>
        /// The GetAll.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Category}"/>.</returns>
        public IEnumerable<Category> GetAll()
        {
            return this.categoryRepository.Get().OrderBy(category => category.Name).ToList();
        }

        /// <summary>
        /// The GetByID.
        /// </summary>
        /// <param name="id">The id<see cref="object"/>.</param>
        /// <returns>The <see cref="Category"/>.</returns>
        public Category GetByID(object id)
        {
            return this.categoryRepository.GetByID(id);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="Category"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Insert(Category entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.categoryRepository.Insert(entity);
            }
            else
            {
                Logger.Info("Category insertion failed!");
            }

            return results;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="Category"/>.</param>
        /// <returns>The <see cref="IList{ValidationResult}"/>.</returns>
        public IList<ValidationResult> Update(Category entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                this.categoryRepository.Update(entity);
            }
            else
            {
                Logger.Info("Category update failed!");
            }

            return results;
        }
    }
}
