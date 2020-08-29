//-----------------------------------------------------------------------
// <copyright file="PriceValidator.cs" company="Transilvania University of Brasov">
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DomainModel.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;

    /// <summary>
    /// Defines the <see cref="PriceValidator" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class PriceValidator : ValidationAttribute
    {
        /// <summary>
        /// Just a fake conversion function, there is no point in implementing a real one.
        /// </summary>
        /// <param name="fromCurrencyName">The fromCurrencyName<see cref="string"/>.</param>
        /// <param name="toCurrencyName">The toCurrencyName<see cref="string"/>.</param>
        /// <param name="value">The value<see cref="decimal"/>.</param>
        /// <returns>The <see cref="decimal"/>.</returns>
        public static decimal ConvertCurrency(string fromCurrencyName, string toCurrencyName, decimal value)
        {
            return value;
        }

        /// <summary>
        /// The GetMinAuctionBeginPrice.
        /// </summary>
        /// <param name="currencyName">The currencyName<see cref="string"/>.</param>
        /// <returns>The <see cref="decimal"/>.</returns>
        public static decimal GetMinAuctionBeginPrice(string currencyName)
        {
            var minAuctionBeginPrice = decimal.Parse(ConfigurationManager.AppSettings.Get("MinAuctionBeginPrice"));
            var minAuctionBeginPriceCurrencyName = ConfigurationManager.AppSettings.Get("MinAuctionBeginPriceCurrencyName");
            return ConvertCurrency(currencyName, minAuctionBeginPriceCurrencyName, minAuctionBeginPrice);
        }

        /// <summary>
        /// The IsValid.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="ValidationResult"/>.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var price = (decimal)value;
            var currencyName = (string)validationContext.ObjectType.GetProperty("CurrencyName").GetValue(validationContext.ObjectInstance, null);
            if (price < 0)
            {
                return new ValidationResult(ErrorMessages.NegativePrice);
            }

            if (price < GetMinAuctionBeginPrice(currencyName))
            {
                return new ValidationResult(ErrorMessages.AuctionStartPriceTooSmall);
            }

            return ValidationResult.Success;
        }
    }
}
