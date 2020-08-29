//-----------------------------------------------------------------------
// <copyright file="Seller.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Seller" />.
    /// </summary>
    public class Seller : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [ForeignKey(nameof(Person))]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Auctions.
        /// </summary>
        public virtual List<Auction> Auctions { get; set; } = new List<Auction>();

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        public virtual List<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Gets or sets the Person.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>Tests if this class is equal to another.</returns>
        public override bool Equals(object obj)
        {
            var seller = obj as Seller;
            return seller != null &&
                   Id == seller.Id;
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>Computes hash code.</returns>
        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="IEnumerable{ValidationResult}"/>.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
