//-----------------------------------------------------------------------
// <copyright file="AuctionServicesImplementation.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataMapper;
    using DomainModel;

    public class AuctionServicesImplementation : IAuctionService
    {
        private IAuctionRepository auctionRepository;

        public AuctionServicesImplementation(IAuctionRepository auctionService)
        {
            this.auctionRepository = auctionService;
        }

        public void Delete(Auction entity)
        {
            auctionRepository.Delete(entity);
        }

        public IEnumerable<Auction> GetAll() => auctionRepository.Get().OrderBy(auction => auction.Name).ToList();

        public Auction GetByID(object id)
        {
            return auctionRepository.GetByID(id);
        }

        public IList<ValidationResult> Insert(Auction entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                auctionRepository.Insert(entity);
            }

            return results;
        }

        public IList<ValidationResult> Update(Auction entity)
        {
            var results = EntityValidator.IsEntityValid(entity);
            if (results.Count == 0)
            {
                auctionRepository.Update(entity);
            }

            return results;
        }
    }
}
