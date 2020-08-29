//-----------------------------------------------------------------------
// <copyright file="AuctionDBContext.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestDatabase")]

namespace DataLayer.AccessLayer
{
    using System.Data.Entity;
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="AuctionDBContext" />.
    /// </summary>
    internal class AuctionDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionDBContext"/> class.
        /// </summary>
        public AuctionDBContext() : base("AuctionDBConnection")
        {
        }

        /// <summary>
        /// Gets or sets the Auctions.
        /// </summary>
        public virtual DbSet<Auction> Auctions { get; set; }

        /// <summary>
        /// Gets or sets the People.
        /// </summary>
        public virtual DbSet<Person> People { get; set; }

        /// <summary>
        /// Gets or sets the Categories.
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the Bids.
        /// </summary>
        public virtual DbSet<Bid> Bids { get; set; }

        /// <summary>
        /// Gets or sets the Bidders.
        /// </summary>
        public virtual DbSet<Bidder> Bidders { get; set; }

        /// <summary>
        /// Gets or sets the Sellers.
        /// </summary>
        public virtual DbSet<Seller> Sellers { get; set; }

        /// <summary>
        /// Gets or sets the UserProfiles.
        /// </summary>
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
    }
}
