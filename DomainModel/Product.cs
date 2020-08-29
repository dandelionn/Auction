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
        /// Gets or sets the Categories.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.CategoriesRequired)]
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthMustBeGreaterThanZero)]
        public virtual List<Category> Categories { get; set; } = new List<Category>();

        /// <summary>
        /// Gets or sets the Sellers.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.SellersRequired)]
        [MustHaveOneElement(ErrorMessage = ErrorMessages.LengthMustBeGreaterThanZero)]
        public virtual List<Seller> Sellers { get; set; } = new List<Seller>();

        /// <summary>
        /// Gets or sets the Auctions.
        /// </summary>
        public virtual List<Auction> Auctions { get; set; } = new List<Auction>();

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>Tests if this class is equal to another.</returns>
        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null &&
                   Id == product.Id &&
                   Name == product.Name;
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>Computes hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = -1919740922;
            hashCode = (hashCode * -1521134295) + Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
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
            Validator.TryValidateProperty(this.Categories, new ValidationContext(this, null, null) { MemberName = nameof(Categories) }, results);
            Validator.TryValidateProperty(this.Sellers, new ValidationContext(this, null, null) { MemberName = nameof(Sellers) }, results);
            return results;
        }
    }
}
