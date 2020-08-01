//-----------------------------------------------------------------------
// <copyright file="BidServicesImplementation.cs" company="Transilvania University of Brasov">    
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

    class BidServicesImplementation: IBidService
    {
        private IBidRepository bidRepository;

        public BidServicesImplementation(IBidRepository bidRepository)
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

        public IList<ValidationResult> Insert(Bid entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                bidRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(Bid entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            {
                bidRepository.Update(entity);
            }

            return results;
        }
    }
}
