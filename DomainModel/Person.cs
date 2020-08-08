//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using DomainModel.Validators;

    public class Person : IValidatableObject
    {
        public Person()
        {
        }

        public Bidder Bidder { get; set; }

        public Seller Seller { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.NameRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And50)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.SurnameRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And50)]
        public string Surname { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = ErrorMessages.LengthBetween2And100)]
        public string Address { get; set; }

        [Required(ErrorMessage = ErrorMessages.PhoneNumberRequired)]
        [RegularExpression(@"^(\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\s|\.|\-)?([0-9]{3}(\s|\.|\-|)){2}$", ErrorMessage = ErrorMessages.InvalidPhoneNumber)]
        public string PhoneNumber { get; set; }

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
