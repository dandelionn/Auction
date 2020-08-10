//-----------------------------------------------------------------------
// <copyright file="BidderServices.cs" company="Transilvania University of Brasov">    
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

    public class BidderProfileServices
    {
        private IBidderRepository bidderRepository;

        public BidderProfileServices(IBidderRepository bidderRepository)
        {
            this.bidderRepository = bidderRepository;
        }

        public void Delete(Bidder entity)
        {
            bidderRepository.Delete(entity);
        }

        public IEnumerable<Bidder> GetAll()
        {
            return bidderRepository.Get().OrderBy(bidder => bidder.Person.Name).ToList();
        }

        public Bidder GetByID(object id)
        {
            return bidderRepository.GetByID(id);
        }

        public IList<ValidationResult> Insert(Bidder entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                bidderRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(Bidder entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            {
                bidderRepository.Update(entity);
            }

            return results;
        }
    }
}
