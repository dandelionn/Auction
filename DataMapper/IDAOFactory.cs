//-----------------------------------------------------------------------
// <copyright file="IDAOFactory.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper
{
    /// <summary>
    /// Defines the <see cref="IDAOFactory" />.
    /// </summary>
    public interface IDAOFactory
    {
        /// <summary>
        /// Gets the AuctionRepository.
        /// </summary>
        IAuctionRepository AuctionRepository { get; }

        /// <summary>
        /// Gets the PersonRepository.
        /// </summary>
        IPersonRepository PersonRepository { get; }

        /// <summary>
        /// Gets the CategoryRepository.
        /// </summary>
        ICategoryRepository CategoryRepository { get; }

        /// <summary>
        /// Gets the ProductRepository.
        /// </summary>
        IProductRepository ProductRepository { get; }

        /// <summary>
        /// Gets the BidRepository.
        /// </summary>
        IBidRepository BidRepository { get; }
    }
}
