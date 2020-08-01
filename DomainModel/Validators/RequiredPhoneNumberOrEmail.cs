//-----------------------------------------------------------------------
// <copyright file="RequiredPhoneNumberOrEmail.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the .<see cref="RequiredPhoneNumberOrEmail" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class RequiredPhoneNumberOrEmail : ValidationAttribute
    {
        /// <summary>
        /// The IsValid.
        /// </summary>
        /// <param name="value">The value.<see cref="object"/></param>
        /// <param name="validationContext">The validationContext.<see cref="ValidationContext"/></param>
        /// <returns>The .<see cref="ValidationResult"/></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = (string)validationContext.ObjectType.GetProperty("PhoneNumber").GetValue(validationContext.ObjectInstance, null);
            var email = (string)validationContext.ObjectType.GetProperty("Email").GetValue(validationContext.ObjectInstance, null);

            if (string.IsNullOrEmpty(phoneNumber) && string.IsNullOrEmpty(email))
            {
                return new ValidationResult(ErrorMessages.PhoneOrEmailRequired);
            }

            return ValidationResult.Success;
        }
    }
}