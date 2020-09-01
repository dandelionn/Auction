//-----------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using DomainModel.Validators;

    /// <summary>
    /// Defines the <see cref="UserProfile" />.
    /// </summary>
    public class UserProfile : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Person.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.UsernameRequired)]
        [RegularExpression(@"^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = ErrorMessages.InvalidUsername)]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = ErrorMessages.InvalidPassword)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmailAddress)]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>True if objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            var profile = obj as UserProfile;
            return profile != null &&
                   Id == profile.Id &&
                   Username == profile.Username &&
                   Password == profile.Password &&
                   Email == profile.Email;
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>The hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1556482632;
            hashCode = (hashCode * -1521134295) + Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Username);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Email);
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
            Validator.TryValidateProperty(this.Username, new ValidationContext(this, null, null) { MemberName = nameof(Username) }, results);
            Validator.TryValidateProperty(this.Password, new ValidationContext(this, null, null) { MemberName = nameof(Password) }, results);
            Validator.TryValidateProperty(this.Email, new ValidationContext(this, null, null) { MemberName = nameof(Email) }, results);
            return results;
        }
    }
}
