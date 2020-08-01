//-----------------------------------------------------------------------
// <copyright file="AuctionRepository.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DataMapper.SqlServerDAO
{
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="AuctionRepository" />.
    /// </summary>
    public class AuctionRepository : BaseRepository<Auction>, IAuctionRepository
    {
    }
}
