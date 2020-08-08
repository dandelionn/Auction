//-----------------------------------------------------------------------
// <copyright file="AuctionTests.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TestDomainModel
{
    using DomainModel;
    using DomainModel.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;

    /// <summary>
    /// Defines the <see cref="AuctionTests" />.
    /// </summary>
    [TestClass]
    public class AuctionTests
    {
        private Auction auction;
        private ValidationContext context;
        private List<ValidationResult> results;

        [TestInitialize]
        public void TestInit()
        {
            auction = new Auction();
            context = new ValidationContext(auction);
            results = new List<ValidationResult>();
        }

        [TestMethod]
        public void TestMethodNullName()
        {
            auction.Name = null;
            context.MemberName = nameof(Auction.Name);

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidName()
        {
            auction.Name = "RandomName";
            context.MemberName = nameof(Auction.Name);

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodNameTooLong()
        {
            auction.Name = new string('a', 51);
            context.MemberName = nameof(Auction.Name);

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodNameTooShort()
        {
            auction.Name = new string('a', 1);
            context.MemberName = nameof(Auction.Name);

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodNullAddress()
        {
            auction.Address = null;
            context.MemberName = nameof(Auction.Address);

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AddressRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidAddress()
        {
            auction.Address = "Random adress";
            context.MemberName = nameof(Auction.Address);

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodTooLongAddress()
        {
            auction.Address = new string('a', 101);
            context.MemberName = nameof(Auction.Address);

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodAddressTooShort()
        {
            auction.Address = new string('a', 1);
            context.MemberName = nameof(Auction.Address);

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodBeginDateNull()
        {
            auction.BeginDate = null;
            context.MemberName = nameof(Auction.BeginDate);

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodBeginDateAfterEndDate()
        {
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now;
            context.MemberName = nameof(Auction.BeginDate);

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateIsAfterEndDate, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodBeginDateInThePast()
        {
            auction.BeginDate = new DateTime(2000, 1, 1);
            auction.EndDate = DateTime.Now;
            context.MemberName = nameof(Auction.BeginDate);

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateShouldNotBeInThePast, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidBeginDate()
        {
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddDays(1);
            context.MemberName = nameof(Auction.BeginDate);

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodEndDateNull()
        {
            auction.EndDate = null;
            context.MemberName = nameof(Auction.EndDate);

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.EndDateRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodEndDateBeforeBeginDate()
        {
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now;
            context.MemberName = nameof(Auction.EndDate);

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.EndDateIsBeforeBeginDate, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodAuctionPeriodTooLarge()
        {
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(auctionMaxPeriodInMonths + 1);
            context.MemberName = nameof(Auction.EndDate);

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionPeriodIsTooLarge, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidEndDate()
        {
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(auctionMaxPeriodInMonths);
            context.MemberName = nameof(Auction.EndDate);

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodNullCurrencyName()
        {
            auction.CurrencyName = null;
            context.MemberName = nameof(Auction.CurrencyName);

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidCurrencyName()
        {
            auction.CurrencyName = CurrencyNameValidator.GetCurrenciesEnglishNames()[0];
            context.MemberName = nameof(Auction.CurrencyName);

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodInvalidCurrencyName()
        {
            auction.CurrencyName = "InvalidCurrencyName";
            context.MemberName = nameof(Auction.CurrencyName);

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameIsNotValid, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodNullBeginPrice()
        {
            auction.BeginPrice = null;
            context.MemberName = nameof(Auction.BeginPrice);

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginPriceRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidBeginPrice()
        {
            auction.CurrencyName = "Euro";
            auction.BeginPrice = (decimal)25.6;
            context.MemberName = nameof(Auction.BeginPrice);

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodNegativeBeginPrice()
        {
            auction.CurrencyName = "Euro";
            auction.BeginPrice = (decimal)-25.6;
            context.MemberName = nameof(Auction.BeginPrice);

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NegativePrice, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodTooSmallAuctionBeginPrice()
        {
            auction.CurrencyName = "Euro";
            auction.BeginPrice = decimal.Parse(ConfigurationManager.AppSettings.Get("MinAuctionBeginPrice")) / 2;
            context.MemberName = nameof(Auction.BeginPrice);

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionPriceTooSmall, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidCurrentPrice()
        {
            auction.CurrencyName = "Euro";
            auction.CurrentPrice = (decimal)25.6;
            context.MemberName = nameof(Auction.CurrentPrice);

            var result = Validator.TryValidateProperty(auction.CurrentPrice, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodNegativeCurrentPrice()
        {
            auction.CurrencyName = "Euro";
            auction.CurrentPrice = (decimal)-25.6;
            context.MemberName = nameof(Auction.CurrentPrice);

            var result = Validator.TryValidateProperty(auction.CurrentPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NegativePrice, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodTooSmallAuctionCurrentPrice()
        {
            auction.CurrencyName = "Euro";
            auction.CurrentPrice = decimal.Parse(ConfigurationManager.AppSettings.Get("MinAuctionBeginPrice")) / 2;
            context.MemberName = nameof(Auction.CurrentPrice);

            var result = Validator.TryValidateProperty(auction.CurrentPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionPriceTooSmall, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodActiveFalse()
        {
            Assert.IsFalse(auction.Active);
        }

        [TestMethod]
        public void TestMethodProductsNull()
        {
            auction.Products = null;
            context.MemberName = nameof(Auction.Products);

            var result = Validator.TryValidateProperty(auction.Products, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ProductsRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodProductsListEmpty()
        {
            auction.Products = new List<Product>();
            context.MemberName = nameof(Auction.Products);

            var result = Validator.TryValidateProperty(auction.Products, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthMustBeGreaterThanZero, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodProductsValid()
        {
            auction.Products.Add(new Product());
            context.MemberName = nameof(Auction.Products);

            var result = Validator.TryValidateProperty(auction.Products, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodBidsNotNull()
        {
            Assert.IsNotNull(auction.Bids);
        }

        [TestMethod]
        public void TestMethodNullOwner()
        {
            auction.Seller = null;
            context.MemberName = nameof(Auction.Seller);

            var result = Validator.TryValidateProperty(auction.Seller, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.SellerRequired, res.ErrorMessage);
        }
    }
}
