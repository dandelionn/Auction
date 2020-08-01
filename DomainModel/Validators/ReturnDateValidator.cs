//-----------------------------------------------------------------------
// <copyright file="ReturnDateValidator.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the .<see cref="ReturnDateValidator" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ReturnDateValidator : ValidationAttribute
    {
        /// <summary>
        /// The IsValid.
        /// </summary>
        /// <param name="value">The value.<see cref="object"/></param>
        /// <param name="validationContext">The validationContext.<see cref="ValidationContext"/></param>
        /// <returns>The .<see cref="ValidationResult"/></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var loanDate = (DateTime)validationContext.ObjectType.GetProperty("LoanDate").GetValue(validationContext.ObjectInstance, null);

            var returnDate = (DateTime)validationContext.ObjectType.GetProperty("ReturnDate").GetValue(validationContext.ObjectInstance, null);

            if (returnDate < loanDate)
            {
                return new ValidationResult(ErrorMessages.ReturnDateLaterOrEqualLoanDate);
            }

            return ValidationResult.Success;
        }
    }
}
