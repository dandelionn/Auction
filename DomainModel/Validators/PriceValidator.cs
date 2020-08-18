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

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class PriceValidator : ValidationAttribute
    {
        // Just a fake conversion function, there is no point in implementing a real one.
        public static decimal ConvertCurrency(string fromCurrencyName, string toCurrencyName, decimal value)
        {
            return value;
        }

        public static decimal GetMinAuctionBeginPrice(string currencyName)
        {
            var minAuctionBeginPrice = decimal.Parse(ConfigurationManager.AppSettings.Get("MinAuctionBeginPrice"));
            var minAuctionBeginPriceCurrencyName = ConfigurationManager.AppSettings.Get("MinAuctionBeginPriceCurrencyName");
            return ConvertCurrency(currencyName, minAuctionBeginPriceCurrencyName, minAuctionBeginPrice);   
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var price = (decimal) value;
            var currencyName = (string) validationContext.ObjectType.GetProperty("CurrencyName").GetValue(validationContext.ObjectInstance, null);
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
