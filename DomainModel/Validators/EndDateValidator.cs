//-----------------------------------------------------------------------
// <copyright file="EndDateValidator.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DomainModel.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;

    /// <summary>
    /// Defines the .<see cref="EndDateValidator" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class EndDateValidator : ValidationAttribute
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
            
            if (endDate < beginDate)
            {
                return new ValidationResult(ErrorMessages.EndDateIsBeforeBeginDate);
            }

            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            var lastPossibleEndDate = beginDate.AddMonths(auctionMaxPeriodInMonths);
            if (lastPossibleEndDate < endDate)
            {
                return new ValidationResult(ErrorMessages.AuctionPeriodIsTooLarge);
            }

            return ValidationResult.Success;
        }
    }
}
