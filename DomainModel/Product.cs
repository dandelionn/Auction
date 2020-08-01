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

    /// <summary>
    /// Defines the <see cref="Product" />.
    /// </summary>
    public class Product : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            this.Auctions = new List<Auction>();
            this.Categories = new List<Category>();
            this.Users = new List<User>();
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
        /// Gets or sets the Auctions.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.AuctionsRequired)]
        public List<Auction> Auctions { get; set; }

        /// <summary>
        /// Gets or sets the Categories.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.CategoriesRequired)]
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthGreaterThanZero)]
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.UsersRequired)]
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthGreaterThanZero)]
        public List<User> Users { get; set; }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="IEnumerable{ValidationResult}"/>.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) { MemberName = nameof(Name) }, results);
            Validator.TryValidateProperty(this.Categories, new ValidationContext(this, null, null) { MemberName = nameof(Categories) }, results);
            Validator.TryValidateProperty(this.Users, new ValidationContext(this, null, null) { MemberName = nameof(Users) }, results);
            return results;
        }
    }
}
