//-----------------------------------------------------------------------
// <copyright file="BeginDateValidator.cs" company="Transilvania University of Brasov">
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the .<see cref="BeginDateValidator" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class BeginDateValidator : ValidationAttribute
    {
        /// <summary>
        /// The IsValid.
        /// </summary>
        /// <param name="value">The value.<see cref="object"/></param>
        /// <param name="validationContext">The validationContext.<see cref="ValidationContext"/></param>
        /// <returns>The .<see cref="ValidationResult"/></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var beginDate = (DateTime)validationContext.ObjectType.GetProperty("BeginDate").GetValue(validationContext.ObjectInstance, null);
            var endDate = (DateTime)validationContext.ObjectType.GetProperty("EndDate").GetValue(validationContext.ObjectInstance, null);
            if (beginDate < DateTime.Now)
            {
                return new ValidationResult(ErrorMessages.BeginDateShouldNotBeInThePast);
            }

            if (beginDate > endDate)
            {
                return new ValidationResult(ErrorMessages.BeginDateIsAfterEndDate);
            }

            return ValidationResult.Success;
        }
    }
}
