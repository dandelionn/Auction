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
        [Key]
        public int Id { get; set; }

        public Person Person { get; set; }

        [Required(ErrorMessage = ErrorMessages.UsernameRequired)]
        [RegularExpression(@"^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = ErrorMessages.InvalidUsername)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = ErrorMessages.InvalidPassword)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmailAddress)]
        [Index(IsUnique = true)]
        [StringLength(100)]
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
