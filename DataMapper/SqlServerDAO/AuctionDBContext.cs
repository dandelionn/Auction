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
        public virtual DbSet<Person> Person { get; set; }

        /// <summary>
        /// Gets or sets the Category.
        /// </summary>
        public virtual DbSet<Category> Category { get; set; }

        /// <summary>
        /// Gets or sets the Product.
        /// </summary>
        public virtual DbSet<Product> Product { get; set; }

        /// <summary>
        /// Gets or sets the Bids.
        /// </summary>
        public virtual DbSet<Bid> Bids { get; set; }
    }
}
