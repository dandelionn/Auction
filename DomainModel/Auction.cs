//-----------------------------------------------------------------------
// <copyright file="Auction.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DomainModel
{
    using DomainModel.Validators;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Auction : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.NameRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And50)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.AddressRequired)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And100)]
        public string Address { get; set; }

        [Required(ErrorMessage = ErrorMessages.BeginDateRequired)]
        [DataType(DataType.Date, ErrorMessage = ErrorMessages.InvalidDate)]
        public DateTime? BeginDate { get; set; }

        [Required(ErrorMessage = ErrorMessages.EndDateRequired)]
        [DataType(DataType.Date, ErrorMessage = ErrorMessages.InvalidDate)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = ErrorMessages.CurrencyNameRequired)]
        [CurrencyNameValidator]
        public string CurrencyName { get; set; }

        [Required(ErrorMessage = ErrorMessages.BeginPriceRequired)]
        [PriceValidator]
        public decimal? BeginPrice { get; set; }

        [Required(ErrorMessage = ErrorMessages.CurrentPriceRequired)]
        [PriceValidator]
        public decimal? CurrentPrice { get; set; }

        public bool Active { get; set; } = false;

        [Required(ErrorMessage = ErrorMessages.ProductsRequired)] //test
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthMustBeGreaterThanZero)]
        public List<Product> Products { get; set; } = new List<Product>();

        public List<Bid> Bids { get; set; } = new List<Bid>();

        [Required(ErrorMessage = ErrorMessages.SellerRequired)]
        public Seller Seller { get; set; }

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
