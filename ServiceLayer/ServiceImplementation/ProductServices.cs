//-----------------------------------------------------------------------
// <copyright file="ProductServicesImplementation.cs" company="Transilvania University of Brasov">    
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

    class ProductServices: IProductService
    {
        private IProductRepository productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Delete(Product entity)
        {
            productRepository.Delete(entity);
        }

        public IEnumerable<Product> GetAll()
        {
            return productRepository.Get().OrderBy(product => product.Name).ToList();
        }

        public Product GetByID(object id)
        {
            return productRepository.GetByID(id);
        }

        public IList<ValidationResult> Insert(Product entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                productRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(Product entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            {
                productRepository.Update(entity);
            }

            return results;
        }
    }
}
