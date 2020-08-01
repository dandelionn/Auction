//-----------------------------------------------------------------------
// <copyright file="LessOrEqualToBookCount.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the .<see cref="LessOrEqualToBookCount" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class LessOrEqualToBookCount : ValidationAttribute
    {
        /// <summary>
        /// The IsValid.
        /// </summary>
        /// <param name="value">The value.<see cref="object"/></param>
        /// <param name="validationContext">The validationContext.<see cref="ValidationContext"/></param>
        /// <returns>The .<see cref="ValidationResult"/></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var count = (int)validationContext.ObjectType.GetProperty(@"Count").GetValue(validationContext.ObjectInstance, null);
            var readingRoomBookCount = (int)validationContext.ObjectType.GetProperty(@"ReadingRoomBookCount").GetValue(validationContext.ObjectInstance, null);

            if (count < readingRoomBookCount)
            {
                return new ValidationResult(ErrorMessages.LessOrEqualToBookCount);
            }

            return ValidationResult.Success;
        }
    }
}
