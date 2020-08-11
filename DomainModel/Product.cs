//-----------------------------------------------------------------------
// <copyright file="Product.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Validators;

    public class Product : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.NameRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And50)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.CategoriesRequired)]
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthMustBeGreaterThanZero)]
        public List<Category> Categories { get; set; } = new List<Category>();

        [Required(ErrorMessage = ErrorMessages.SellersRequired)]
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthMustBeGreaterThanZero)]
        public List<Seller> Sellers { get; set; } = new List<Seller>();

        public List<Auction> Auctions { get; set; } = new List<Auction>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) { MemberName = nameof(Name) }, results);
            Validator.TryValidateProperty(this.Categories, new ValidationContext(this, null, null) { MemberName = nameof(Categories) }, results);
            Validator.TryValidateProperty(this.Sellers, new ValidationContext(this, null, null) { MemberName = nameof(Sellers) }, results);
            return results;
        }
    }
}
