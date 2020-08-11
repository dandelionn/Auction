//-----------------------------------------------------------------------
// <copyright file="AuctionDBContext.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.DataMapper
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
        public AuctionDBContext() : base("myConStr")
        {
        }

        /// <summary>
        /// Gets or sets the Auctions.
        /// </summary>
        public virtual DbSet<Auction> Auctions { get; set; }

        /// <summary>
        /// Gets or sets the Person.
        /// </summary>
        public virtual DbSet<Person> People { get; set; }

        /// <summary>
        /// Gets or sets the Category.
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the Product.
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the Bids.
        /// </summary>
        public virtual DbSet<Bid> Bids { get; set; }

        public virtual DbSet<Bidder> Bidders { get; set; }

        public virtual DbSet<Seller> Sellers { get; set; }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
    }
}
