//-----------------------------------------------------------------------
// <copyright file="AuctionTests.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TestDomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using DomainModel;
    using DomainModel.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines the <see cref="AuctionTests" />.
    /// </summary>
    [TestClass]
    public class AuctionTests
    {
        /// <summary>
        /// Defines the auction.
        /// </summary>
        private Auction auction;

        /// <summary>
        /// Defines the context.
        /// </summary>
        private ValidationContext context;

        /// <summary>
        /// Defines the results.
        /// </summary>
        private List<ValidationResult> results;

        /// <summary>
        /// The TestInit.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            auction = new Auction();
            context = new ValidationContext(auction);
            results = new List<ValidationResult>();
        }

        /// <summary>
        /// The Name_Null.
        /// </summary>
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

        /// <summary>
        /// The Name_Valid.
        /// </summary>
        [TestMethod]
        public void Name_Valid()
        {
            auction.Name = "RandomName";
            context.MemberName = nameof(Auction.Name);

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Name_TooLong.
        /// </summary>
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

        /// <summary>
        /// The Name_TooShort.
        /// </summary>
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

        /// <summary>
        /// The Address_Null.
        /// </summary>
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

        /// <summary>
        /// The Address_Valid.
        /// </summary>
        [TestMethod]
        public void Address_Valid()
        {
            auction.Address = "Random adress";
            context.MemberName = nameof(Auction.Address);

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Address_TooLong.
        /// </summary>
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

        /// <summary>
        /// The Address_TooShort.
        /// </summary>
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

        /// <summary>
        /// The BeginDate_Null.
        /// </summary>
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

        /// <summary>
        /// The BeginDate_Valid.
        /// </summary>
        [TestMethod]
        public void BeginDate_Valid()
        {
            auction.BeginDate = DateTime.Now;
            context.MemberName = nameof(Auction.BeginDate);

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The EndDate_Null.
        /// </summary>
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

        /// <summary>
        /// The EndDate_Valid.
        /// </summary>
        [TestMethod]
        public void EndDate_Valid()
        {
            auction.EndDate = DateTime.Now.AddDays(1);
            context.MemberName = nameof(Auction.EndDate);

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The CurrencyName_Null.
        /// </summary>
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

        /// <summary>
        /// The CurrencyName_Valid.
        /// </summary>
        [TestMethod]
        public void CurrencyName_Valid()
        {
            auction.CurrencyName = CurrencyNameValidator.GetCurrenciesEnglishNames()[0];
            context.MemberName = nameof(Auction.CurrencyName);

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The CurrencyName_Invalid.
        /// </summary>
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

        /// <summary>
        /// The BeginPrice_Null.
        /// </summary>
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

        /// <summary>
        /// The BeginPrice_Valid.
        /// </summary>
        [TestMethod]
        public void BeginPrice_Valid()
        {
            auction.CurrencyName = "Euro";
            auction.BeginPrice = (decimal)25.6;
            context.MemberName = nameof(Auction.BeginPrice);

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The BeginPrice_Negative.
        /// </summary>
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

        /// <summary>
        /// The AuctionBeginPrice_TooSmall.
        /// </summary>
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

        /// <summary>
        /// The CurrentPrice_Valid.
        /// </summary>
        [TestMethod]
        public void CurrentPrice_Valid()
        {
            auction.CurrencyName = "Euro";
            auction.CurrentPrice = (decimal)25.6;
            context.MemberName = nameof(Auction.CurrentPrice);

            var result = Validator.TryValidateProperty(auction.CurrentPrice, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The CurrentPrice_Negative.
        /// </summary>
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

        /// <summary>
        /// The CurrentPrice_TooSmall.
        /// </summary>
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

        /// <summary>
        /// The CurrentPrice_Null.
        /// </summary>
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

        /// <summary>
        /// The Active_False.
        /// </summary>
        [TestMethod]
        public void Active_False()
        {
            Assert.IsFalse(auction.Active);
        }

        /// <summary>
        /// The Products_Null.
        /// </summary>
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

        /// <summary>
        /// The Products_ListEmpty.
        /// </summary>
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

        /// <summary>
        /// The Products_Valid.
        /// </summary>
        [TestMethod]
        public void Products_Valid()
        {
            auction.Products.Add(new Product());
            context.MemberName = nameof(Auction.Products);

            var result = Validator.TryValidateProperty(auction.Products, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Bids_NotNull.
        /// </summary>
        [TestMethod]
        public void Bids_NotNull()
        {
            Assert.IsNotNull(auction.Bids);
        }

        /// <summary>
        /// The Owner_Null.
        /// </summary>
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

        /// <summary>
        /// The Owner_NotNull.
        /// </summary>
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
