//-----------------------------------------------------------------------
// <copyright file="CurrencyNameValidator.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DomainModel.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Defines the .<see cref="CurrencyNameValidator" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CurrencyNameValidator : ValidationAttribute
    {
        /// <summary>
        /// The GetCurrenciesEnglishNames.
        /// </summary>
        /// <returns>The <see cref="List{string}"/>.</returns>
        public static List<string> GetCurrenciesEnglishNames()
        {
            var currenciesNames = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                  .Select(ci => ci.LCID).Distinct()
                  .Select(id => new RegionInfo(id))
                  .GroupBy(r => r.ISOCurrencySymbol)
                  .Select(g => g.First())
                  .Select(r => r.CurrencyEnglishName).ToList();
            return currenciesNames;
        }

        /// <summary>
        /// The IsValid.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="ValidationResult"/>.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currencyName = (string)validationContext.ObjectType.GetProperty("CurrencyName").GetValue(validationContext.ObjectInstance, null);

            var currenciesNames = GetCurrenciesEnglishNames();
            if (!currenciesNames.Contains(currencyName))
            {
                return new ValidationResult(ErrorMessages.CurrencyNameIsNotValid);
            }

            return ValidationResult.Success;
        }
    }
}
