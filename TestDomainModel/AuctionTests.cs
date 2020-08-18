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
        public void Name_Null()
        {
            auction.Name = null;
            context.MemberName = nameof(Auction.Name);

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Name_Valid()
        {
            auction.Name = "RandomName";
            context.MemberName = nameof(Auction.Name);

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Name_TooLong()
        {
            auction.Name = new string('a', 51);
            context.MemberName = nameof(Auction.Name);

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void Name_TooShort()
        {
            auction.Name = new string('a', 1);
            context.MemberName = nameof(Auction.Name);

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void Address_Null()
        {
            auction.Address = null;
            context.MemberName = nameof(Auction.Address);

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AddressRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Address_Valid()
        {
            auction.Address = "Random adress";
            context.MemberName = nameof(Auction.Address);

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Address_TooLong()
        {
            auction.Address = new string('a', 101);
            context.MemberName = nameof(Auction.Address);

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        [TestMethod]
        public void Address_TooShort()
        {
            auction.Address = new string('a', 1);
            context.MemberName = nameof(Auction.Address);

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        [TestMethod]
        public void BeginDate_Null()
        {
            auction.BeginDate = null;
            context.MemberName = nameof(Auction.BeginDate);

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void BeginDate_Valid()
        {
            auction.BeginDate = DateTime.Now;
            context.MemberName = nameof(Auction.BeginDate);

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void EndDate_Null()
        {
            auction.EndDate = null;
            context.MemberName = nameof(Auction.EndDate);

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.EndDateRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void EndDate_Valid()
        {
            auction.EndDate = DateTime.Now.AddDays(1);
            context.MemberName = nameof(Auction.EndDate);

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void CurrencyName_Null()
        {
            auction.CurrencyName = null;
            context.MemberName = nameof(Auction.CurrencyName);

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void CurrencyName_Valid()
        {
            auction.CurrencyName = CurrencyNameValidator.GetCurrenciesEnglishNames()[0];
            context.MemberName = nameof(Auction.CurrencyName);

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void CurrencyName_Invalid()
        {
            auction.CurrencyName = "InvalidCurrencyName";
            context.MemberName = nameof(Auction.CurrencyName);

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameIsNotValid, res.ErrorMessage);
        }

        [TestMethod]
        public void BeginPrice_Null()
        {
            auction.BeginPrice = null;
            context.MemberName = nameof(Auction.BeginPrice);

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginPriceRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void BeginPrice_Valid()
        {
            auction.CurrencyName = "Euro";
            auction.BeginPrice = (decimal)25.6;
            context.MemberName = nameof(Auction.BeginPrice);

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void BeginPrice_Negative()
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
        public void AuctionBeginPrice_TooSmall()
        {
            auction.CurrencyName = "Euro";
            auction.BeginPrice = decimal.Parse(ConfigurationManager.AppSettings.Get("MinAuctionBeginPrice")) / 2;
            context.MemberName = nameof(Auction.BeginPrice);

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionStartPriceTooSmall, res.ErrorMessage);
        }

        [TestMethod]
        public void CurrentPrice_Valid()
        {
            auction.CurrencyName = "Euro";
            auction.CurrentPrice = (decimal)25.6;
            context.MemberName = nameof(Auction.CurrentPrice);

            var result = Validator.TryValidateProperty(auction.CurrentPrice, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void CurrentPrice_Negative()
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
        public void CurrentPrice_TooSmall()
        {
            auction.CurrencyName = "Euro";
            auction.CurrentPrice = decimal.Parse(ConfigurationManager.AppSettings.Get("MinAuctionBeginPrice")) / 2;
            context.MemberName = nameof(Auction.CurrentPrice);

            var result = Validator.TryValidateProperty(auction.CurrentPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionStartPriceTooSmall, res.ErrorMessage);
        }

        [TestMethod]
        public void CurrentPrice_Null()
        {
            auction.CurrencyName = "Euro";
            auction.CurrentPrice = null;
            context.MemberName = nameof(Auction.CurrentPrice);

            var result = Validator.TryValidateProperty(auction.CurrentPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrentPriceRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Active_False()
        {
            Assert.IsFalse(auction.Active);
        }

        [TestMethod]
        public void Products_Null()
        {
            auction.Products = null;
            context.MemberName = nameof(Auction.Products);

            var result = Validator.TryValidateProperty(auction.Products, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ProductsRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Products_ListEmpty()
        {
            auction.Products = new List<Product>();
            context.MemberName = nameof(Auction.Products);

            var result = Validator.TryValidateProperty(auction.Products, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthMustBeGreaterThanZero, res.ErrorMessage);
        }

        [TestMethod]
        public void Products_Valid()
        {
            auction.Products.Add(new Product());
            context.MemberName = nameof(Auction.Products);

            var result = Validator.TryValidateProperty(auction.Products, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Bids_NotNull()
        {
            Assert.IsNotNull(auction.Bids);
        }

        [TestMethod]
        public void Owner_Null()
        {
            auction.Seller = null;
            context.MemberName = nameof(Auction.Seller);

            var result = Validator.TryValidateProperty(auction.Seller, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.SellerRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Owner_NotNull()
        {
            auction.Seller = new Seller();
            context.MemberName = nameof(Auction.Seller);

            var result = Validator.TryValidateProperty(auction.Seller, context, results);

            Assert.AreEqual(0, results.Count);
        }
    }
}
