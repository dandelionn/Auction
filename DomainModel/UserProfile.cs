//-----------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel
{
    using DomainModel.Validators;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserProfile
    {
        [ForeignKey(nameof(Person))]
        public int Id { get; set; }

        //https://stackoverflow.com/questions/12018245/regular-expression-to-validate-username
        [Required(ErrorMessage = ErrorMessages.UsernameRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmailAddress)]
        public string Email { get; set; }
    }
}
