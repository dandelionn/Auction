//-----------------------------------------------------------------------
// <copyright file="ActualReturnDateValidator.cs" company="Transilvania University of Brasov">
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the .<see cref="ActualReturnDateValidator" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ActualReturnDateValidator : ValidationAttribute
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
            try
            {
                var actualReturnDate = (DateTime)validationContext.ObjectType.GetProperty("ActualReturnDate").GetValue(validationContext.ObjectInstance, null);

                if (actualReturnDate < loanDate)
                {
                    return new ValidationResult(ErrorMessages.ActualReturnDateLaterOrEqualLoanDate);
                }
            }
            catch (NullReferenceException)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}
