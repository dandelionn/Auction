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

    /// <summary>
    /// Defines the <see cref="Bid" />.
    /// </summary>
    public class Bid : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Bidder.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.BidderRequired)]
        public virtual Bidder Bidder { get; set; }

        /// <summary>
        /// Gets or sets the Auction.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.AuctionRequired)]
        public virtual Auction Auction { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.ValueRequired)]
        [Range(0, double.MaxValue, ErrorMessage = ErrorMessages.NegativeValue)]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = ErrorMessages.ValueShouldNotBeZero)] // Whatever but not zero
        public decimal? Value { get; set; }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/> Tests if this class is equal to another.</returns>
        public override bool Equals(object obj)
        {
            var bid = obj as Bid;
            return bid != null &&
                   Id == bid.Id &&
                   EqualityComparer<Bidder>.Default.Equals(Bidder, bid.Bidder) &&
                   EqualityComparer<Auction>.Default.Equals(Auction, bid.Auction) &&
                   EqualityComparer<decimal?>.Default.Equals(Value, bid.Value);
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>Computes hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1542110439;
            hashCode = (hashCode * -1521134295) + Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<Bidder>.Default.GetHashCode(Bidder);
            hashCode = (hashCode * -1521134295) + EqualityComparer<Auction>.Default.GetHashCode(Auction);
            hashCode = (hashCode * -1521134295) + EqualityComparer<decimal?>.Default.GetHashCode(Value);
            return hashCode;
        }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="IEnumerable{ValidationResult}"/>.</returns>
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
