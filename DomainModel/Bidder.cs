//-----------------------------------------------------------------------
// <copyright file="Bidder.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Bidder : IValidatableObject
    {
        [ForeignKey(nameof(Person))]
        public int Id { get; set; }

        public List<Bid> Bids { get; set; } = new List<Bid>();

        public virtual Person Person { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
