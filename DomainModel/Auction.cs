//-----------------------------------------------------------------------
// <copyright file="Auction.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Validators;

    /// <summary>
    /// Defines the <see cref="Auction" />.
    /// </summary>
    public class Auction : IValidatableObject
    {
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
        /// Gets or sets the BeginDate.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.BeginDateRequired)]
        [DataType(DataType.Date, ErrorMessage = ErrorMessages.InvalidDate)]
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// Gets or sets the EndDate.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.EndDateRequired)]
        [DataType(DataType.Date, ErrorMessage = ErrorMessages.InvalidDate)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the CurrencyName.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.CurrencyNameRequired)]
        [CurrencyNameValidator]
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets the BeginPrice.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.BeginPriceRequired)]
        [PriceValidator]
        public decimal? BeginPrice { get; set; }

        /// <summary>
        /// Gets or sets the CurrentPrice.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.CurrentPriceRequired)]
        [PriceValidator]
        public decimal? CurrentPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Active.
        /// </summary>
        public bool Active { get; set; } = false;

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.ProductsRequired)]
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthMustBeGreaterThanZero)]
        public virtual List<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Gets or sets the Bids.
        /// </summary>
        public virtual List<Bid> Bids { get; set; } = new List<Bid>();

        /// <summary>
        /// Gets or sets the Seller.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.SellerRequired)]
        public virtual Seller Seller { get; set; }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>True if the objects properties contain equal values, False otherwise.</returns>
        public override bool Equals(object obj)
        {
            var auction = obj as Auction;
            return auction != null &&
                   Id == auction.Id &&
                   Name == auction.Name &&
                   Address == auction.Address &&
                   CurrencyName == auction.CurrencyName &&
                   EqualityComparer<decimal?>.Default.Equals(BeginPrice, auction.BeginPrice) &&
                   EqualityComparer<decimal?>.Default.Equals(CurrentPrice, auction.CurrentPrice) &&
                   Active == auction.Active;
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1230604823;
            hashCode = (hashCode * -1521134295) + Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(CurrencyName);
            hashCode = (hashCode * -1521134295) + EqualityComparer<decimal?>.Default.GetHashCode(BeginPrice);
            hashCode = (hashCode * -1521134295) + EqualityComparer<decimal?>.Default.GetHashCode(CurrentPrice);
            hashCode = (hashCode * -1521134295) + Active.GetHashCode();
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
            Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) { MemberName = nameof(Name) }, results);
            Validator.TryValidateProperty(this.Address, new ValidationContext(this, null, null) { MemberName = nameof(Address) }, results);
            Validator.TryValidateProperty(this.CurrencyName, new ValidationContext(this, null, null) { MemberName = nameof(CurrencyName) }, results);
            Validator.TryValidateProperty(this.BeginPrice, new ValidationContext(this, null, null) { MemberName = nameof(BeginPrice) }, results);
            Validator.TryValidateProperty(this.CurrentPrice, new ValidationContext(this, null, null) { MemberName = nameof(CurrentPrice) }, results);
            Validator.TryValidateProperty(this.BeginDate, new ValidationContext(this, null, null) { MemberName = nameof(BeginDate) }, results);
            Validator.TryValidateProperty(this.EndDate, new ValidationContext(this, null, null) { MemberName = nameof(EndDate) }, results);
            Validator.TryValidateProperty(this.Products, new ValidationContext(this, null, null) { MemberName = nameof(Products) }, results);
            Validator.TryValidateProperty(this.Seller, new ValidationContext(this, null, null) { MemberName = nameof(Seller) }, results);
            return results;
        }
    }
}
