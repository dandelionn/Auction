//-----------------------------------------------------------------------
// <copyright file="Auction.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Validators;

    /// <summary>
    /// Defines the <see cref="Auction" />.
    /// </summary>
    public class Auction : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Auction"/> class.
        /// </summary>
        public Auction()
        {
            this.Bids = new List<Bid>();
            this.Products = new List<Product>();
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
        /// Gets or sets the Address.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.AddressRequired)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And100)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.PriceRequired)]
        [Range(.0, double.MaxValue, ErrorMessage = ErrorMessages.NegativePrice)]
        public double? Price { get; set; }

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.ProductsRequired)]
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthGreaterThanZero)]
        public List<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the Bids.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.BidsRequired)]
        public List<Bid> Bids { get; set; }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="IEnumerable{ValidationResult}"/>.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) { MemberName = nameof(Name) }, results);
            Validator.TryValidateProperty(this.Address, new ValidationContext(this, null, null) { MemberName = nameof(Address) }, results);
            Validator.TryValidateProperty(this.Price, new ValidationContext(this, null, null) { MemberName = nameof(Price) }, results);
            Validator.TryValidateProperty(this.Products, new ValidationContext(this, null, null) { MemberName = nameof(Products) }, results);
            Validator.TryValidateProperty(this.Bids, new ValidationContext(this, null, null) { MemberName = nameof(Bids) }, results);
            return results;
        }
    }
}
