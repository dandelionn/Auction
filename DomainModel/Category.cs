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

    /// <summary>
    /// Defines the <see cref="Category" />.
    /// </summary>
    public class Category : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category()
        {
            this.Products = new List<Product>();
            this.ParentCategories = new List<Category>();
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
        /// Gets or sets the Products.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.ProductsRequired)]
        public List<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the ParentCategories.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.ParentCategoriesRequired)]
        public List<Category> ParentCategories { get; set; }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="IEnumerable{ValidationResult}"/>.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) { MemberName = nameof(Name) }, results);
            return results;
        }
    }
}
