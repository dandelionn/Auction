//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Validators;

    /// <summary>
    /// Defines the <see cref="User" />.
    /// </summary>
    public class User : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.Products = new List<Product>();
            this.Bids = new List<Bid>();
            this.OwnedAuctions = new List<Auction>();
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.NameRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Surname.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.SurnameRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And50)]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        [StringLength(100, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And100)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the PhoneNumber.
        /// </summary>
        [RequiredPhoneNumberOrEmail]
        [RegularExpression(@"^(\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\s|\.|\-)?([0-9]{3}(\s|\.|\-|)){2}$", ErrorMessage = ErrorMessages.NotAValidPhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        [RequiredPhoneNumberOrEmail]
        [EmailAddress(ErrorMessage = ErrorMessages.NotAValidEmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.ProductsRequired)]
        public List<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the Bids.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.BidsRequired)]
        public List<Bid> Bids { get; set; }

        [Required(ErrorMessage = ErrorMessages.OwnedAuctionsRequired)]
        public List<Auction> OwnedAuctions { get; set; }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="IEnumerable{ValidationResult}"/>.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) { MemberName = nameof(Name) }, results);
            Validator.TryValidateProperty(this.Surname, new ValidationContext(this, null, null) { MemberName = nameof(Surname) }, results);
            Validator.TryValidateProperty(this.Address, new ValidationContext(this, null, null) { MemberName = nameof(Address) }, results);
            Validator.TryValidateProperty(this.PhoneNumber, new ValidationContext(this, null, null) { MemberName = nameof(PhoneNumber) }, results);
            Validator.TryValidateProperty(this.Email, new ValidationContext(this, null, null) { MemberName = nameof(Email) }, results);
            return results;
        }
    }
}
