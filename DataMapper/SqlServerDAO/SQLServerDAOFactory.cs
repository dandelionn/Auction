//-----------------------------------------------------------------------
// <copyright file="SQLServerDAOFactory.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DataMapper.SqlServerDAO
{
    /// <summary>
    /// Defines the <see cref="SQLServerDAOFactory" />.
    /// </summary>
    public class SQLServerDAOFactory : IDAOFactory
    {
        /// <summary>
        /// Gets the AuctionRepository.
        /// </summary>
        public IAuctionRepository AuctionRepository
        {
            get
            {
                return new AuctionRepository();
            }
        }

        /// <summary>
        /// Gets the PersonRepository.
        /// </summary>
        public IPersonRepository PersonRepository
        {
            get
            {
                return new PersonRepository();
            }
        }

        /// <summary>
        /// Gets the CategoryRepository.
        /// </summary>
        public ICategoryRepository CategoryRepository
        {
            get
            {
                return new CategoryRepository();
            }
        }

        /// <summary>
        /// Gets the ProductRepository.
        /// </summary>
        public IProductRepository ProductRepository
        {
            get
            {
                return new ProductRepository();
            }
        }

        /// <summary>
        /// Gets the BidRepository.
        /// </summary>
        public IBidRepository BidRepository
        {
            get
            {
                return new BidRepository();
            }
        }
    }
}
