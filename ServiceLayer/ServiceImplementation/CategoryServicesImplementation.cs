//-----------------------------------------------------------------------
// <copyright file="CategoryServicesImplementation.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ServiceLayer.ServiceImplementation
{
    using DataMapper;
    using DomainModel;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class CategoryServicesImplementation
    {
        private ICategoryRepository categoryRepository;

        public CategoryServicesImplementation(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public void Delete(Category entity)
        {
            categoryRepository.Delete(entity);
        }

        public IEnumerable<Category> GetAll()
        {
            return categoryRepository.Get().OrderBy(category => category.Name).ToList();
        }

        public Category GetByID(object id)
        {
            return categoryRepository.GetByID(id);
        }

        public IList<ValidationResult> Insert(Category entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                categoryRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(Category entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            {
                categoryRepository.Update(entity);
            }

            return results;
        }
    }
}
