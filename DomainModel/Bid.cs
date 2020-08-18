//-----------------------------------------------------------------------
// <copyright file="Bid.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Validators;

    public class Bid : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.BidderRequired)]
        public Bidder Bidder { get; set; }

        [Required(ErrorMessage = ErrorMessages.AuctionRequired)]
        public Auction Auction { get; set; }

        [Required(ErrorMessage = ErrorMessages.ValueRequired)]
        [Range(0, double.MaxValue, ErrorMessage = ErrorMessages.NegativeValue)]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = ErrorMessages.ValueShouldNotBeZero)] // Whatever but not zero
        public decimal? Value { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Bidder, new ValidationContext(this, null, null) { MemberName = nameof(Bidder) }, results);
            Validator.TryValidateProperty(this.Auction, new ValidationContext(this, null, null) { MemberName = nameof(Auction) }, results);
            Validator.TryValidateProperty(this.Value, new ValidationContext(this, null, null) { MemberName = nameof(Value) }, results);
            return results;
        }
    }
}
