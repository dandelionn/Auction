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

    public class Seller : IValidatableObject
    {
        public Seller()
        {
            this.Auctions = new List<Auction>();
            this.Products = new List<Product>();
        }

        [ForeignKey(nameof(Person))]
        public int Id { get; set; }

        public List<Auction> Auctions { get; set; }
        public List<Product> Products { get; set; }
        public virtual Person Person { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
