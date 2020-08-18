//-----------------------------------------------------------------------
// <copyright file="SellerServices.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ServiceLayer.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataMapper;
    using DomainModel;

    public class SellerServices
    {
        private ISellerRepository sellerRepository;

        public SellerServices(ISellerRepository sellerRepository)
        {
            this.sellerRepository = sellerRepository;
        }

        public void Delete(Seller entity)
        {
            sellerRepository.Delete(entity);
        }

        public IEnumerable<Seller> GetAll()
        {
            return sellerRepository.Get().OrderBy(seller => seller.Person.Name).ToList();
        }

        public Seller GetByID(object id)
        {
            return sellerRepository.GetByID(id);
        }

        public IList<ValidationResult> Insert(Seller entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                sellerRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(Seller entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            {
                sellerRepository.Update(entity);
            }

            return results;
        }
    }
}
