//-----------------------------------------------------------------------
// <copyright file="Bidder.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Bidder" />.
    /// </summary>
    public class Bidder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bidder"/> class.
        /// </summary>
        public Bidder()
        {
            this.Bids = new List<Bid>();
        }

        /// <summary>
        /// Gets or sets the BidderId.
        /// </summary>
        [ForeignKey(nameof(Person))]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Bids.
        /// </summary>
        public List<Bid> Bids { get; set; }

        public virtual Person Person { get; set; }
    }
}
