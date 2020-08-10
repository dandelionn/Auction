//-----------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using DomainModel.Validators;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserProfile : IValidatableObject
    {
        [ForeignKey(nameof(Person))]
        public int Id { get; set; }

        //https://stackoverflow.com/questions/12018245/regular-expression-to-validate-username
        [RegularExpression(@"^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = ErrorMessages.InvalidUsername)]
        public string Username { get; set; }

        //https://stackoverflow.com/questions/19605150/regex-for-password-must-contain-at-least-eight-characters-at-least-one-number-a
        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = ErrorMessages.InvalidPassword)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmailAddress)]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Username, new ValidationContext(this, null, null) { MemberName = nameof(Username) }, results);
            Validator.TryValidateProperty(this.Password, new ValidationContext(this, null, null) { MemberName = nameof(Password) }, results);
            Validator.TryValidateProperty(this.Email, new ValidationContext(this, null, null) { MemberName = nameof(Email) }, results);
            return results;
        }
    }
}
