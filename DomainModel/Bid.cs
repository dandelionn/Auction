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
        /// Initializes a new instance of the <see cref="Bid"/> class.
        /// </summary>
        public Bid()
        {
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.UserRequired)]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the Auction.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.AuctionRequired)]
        public Auction Auction { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.ValueRequired)]
        [Range(.0, double.MaxValue, ErrorMessage = ErrorMessages.NegativeValue)]
        public double? Value { get; set; }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="IEnumerable{ValidationResult}"/>.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.User, new ValidationContext(this, null, null) { MemberName = nameof(User) }, results);
            Validator.TryValidateProperty(this.Auction, new ValidationContext(this, null, null) { MemberName = nameof(Auction) }, results);
            Validator.TryValidateProperty(this.Value, new ValidationContext(this, null, null) { MemberName = nameof(Value) }, results);
            return results;
        }
    }
}
