//-----------------------------------------------------------------------
// <copyright file="Category.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Validators;

    public class Category : IValidatableObject
    {
        public Category()
        {
            this.Products = new List<Product>();
            this.ParentCategories = new List<Category>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.NameRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And50)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public List<Category> ParentCategories { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) { MemberName = nameof(Name) }, results);
            return results;
        }
    }
}
