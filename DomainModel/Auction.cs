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
        public Auction()
        {
            this.Bids = new List<Bid>();
            this.Products = new List<Product>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.NameRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And50)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.AddressRequired)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And100)]
        public string Address { get; set; }

        [Required(ErrorMessage = ErrorMessages.CurrencyNameRequired)]
        [CurrencyNameValidator]
        public string CurrencyName { get; set; }

        [Required(ErrorMessage = ErrorMessages.BeginPriceRequired)]
        [BeginPriceValidator]
        public decimal? BeginPrice { get; set; }

        [Required(ErrorMessage = ErrorMessages.CurrentPriceRequired)]
        public decimal? CurrentPrice { get; set; }

        [Required(ErrorMessage = ErrorMessages.ProductsRequired)]
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthGreaterThanZero)]
        public List<Product> Products { get; set; }

        public List<Bid> Bids { get; set; }

        [Required(ErrorMessage = ErrorMessages.BeginDateRequired)]
        [DataType(DataType.Date, ErrorMessage = ErrorMessages.DateNotValid)]
        [BeginDateValidator]
        public DateTime? BeginDate { get; set; }

        [Required(ErrorMessage = ErrorMessages.EndDateRequired)]
        [DataType(DataType.Date, ErrorMessage = ErrorMessages.DateNotValid)]
        [EndDateValidator]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = ErrorMessages.OwnerRequired)]
        public Seller Seller { get; set; }

        public bool Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) { MemberName = nameof(Name) }, results);
            Validator.TryValidateProperty(this.Address, new ValidationContext(this, null, null) { MemberName = nameof(Address) }, results);
            Validator.TryValidateProperty(this.BeginPrice, new ValidationContext(this, null, null) { MemberName = nameof(BeginPrice) }, results);
            Validator.TryValidateProperty(this.Products, new ValidationContext(this, null, null) { MemberName = nameof(Products) }, results);
            Validator.TryValidateProperty(this.Bids, new ValidationContext(this, null, null) { MemberName = nameof(Bids) }, results);
            Validator.TryValidateProperty(this.Seller, new ValidationContext(this, null, null) { MemberName = nameof(Seller) }, results);
            return results;
        }
    }
}
