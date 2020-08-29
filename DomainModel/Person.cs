//-----------------------------------------------------------------------
// <copyright file="Person.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using DomainModel.Validators;

    /// <summary>
    /// Defines the <see cref="Person" />.
    /// </summary>
    public class Person : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the UserProfile.
        /// </summary>
        public virtual UserProfile UserProfile { get; set; }

        /// <summary>
        /// Gets or sets the Bidder.
        /// </summary>
        public virtual Bidder Bidder { get; set; }

        /// <summary>
        /// Gets or sets the Seller.
        /// </summary>
        public virtual Seller Seller { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [ForeignKey(nameof(UserProfile))]
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
        [Required(ErrorMessage = ErrorMessages.PhoneNumberRequired)]
        [RegularExpression(@"^(\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\s|\.|\-)?([0-9]{3}(\s|\.|\-|)){2}$", ErrorMessage = ErrorMessages.InvalidPhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Scores.
        /// </summary>
        public virtual List<int> Scores { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets the BanEndDate.
        /// </summary>
        public DateTime BanEndDate { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>Tests if this class is equal to another.</returns>
        public override bool Equals(object obj)
        {
            var person = obj as Person;
            return person != null &&
                   Id == person.Id &&
                   Name == person.Name &&
                   Surname == person.Surname &&
                   Address == person.Address &&
                   PhoneNumber == person.PhoneNumber &&
                   BanEndDate == person.BanEndDate;
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>Compute hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = -1665730508;
            hashCode = (hashCode * -1521134295) + Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            hashCode = (hashCode * -1521134295) + BanEndDate.GetHashCode();
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
            Validator.TryValidateProperty(this.Surname, new ValidationContext(this, null, null) { MemberName = nameof(Surname) }, results);
            Validator.TryValidateProperty(this.Address, new ValidationContext(this, null, null) { MemberName = nameof(Address) }, results);
            Validator.TryValidateProperty(this.PhoneNumber, new ValidationContext(this, null, null) { MemberName = nameof(PhoneNumber) }, results);
            return results;
        }
    }
}
