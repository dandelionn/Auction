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
        /// The TestMethodValidName.
        /// </summary>
        [TestMethod]
        public void TestMethodValidName()
        {
            auction.Name = "RandomName";
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodNameTooLong.
        /// </summary>
        [TestMethod]
        public void TestMethodNameTooLong()
        {
            auction.Name = new string('a', 51);
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNameTooShort.
        /// </summary>
        [TestMethod]
        public void TestMethodNameTooShort()
        {
            auction.Name = new string('a', 1);
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNullName.
        /// </summary>
        [TestMethod]
        public void TestMethodNullName()
        {
            auction.Name = null;
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(auction.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodValidAddress.
        /// </summary>
        [TestMethod]
        public void TestMethodValidAddress()
        {
            auction.Address = "Random adress";
            context.MemberName = "Address";

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodTooLongAddress.
        /// </summary>
        [TestMethod]
        public void TestMethodTooLongAddress()
        {
            auction.Address = new string('a', 101);
            context.MemberName = "Address";

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodAddressTooShort.
        /// </summary>
        [TestMethod]
        public void TestMethodAddressTooShort()
        {
            auction.Address = new string('a', 1);
            context.MemberName = "Address";

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNullAddress.
        /// </summary>
        [TestMethod]
        public void TestMethodNullAddress()
        {
            auction.Address = null;
            context.MemberName = "Address";

            var result = Validator.TryValidateProperty(auction.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AddressRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodValidBeginPrice.
        /// </summary>
        [TestMethod]
        public void TestMethodValidBeginPrice()
        {
            auction.BeginPrice = (decimal)25.6;
            context.MemberName = "BeginPrice";

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodNegativeBeginPrice.
        /// </summary>
        [TestMethod]
        public void TestMethodNegativeBeginPrice()
        {
            auction.BeginPrice = (decimal)-25.6;
            context.MemberName = "BeginPrice";

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NegativePrice, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodTooSmallAuctionBeginPrice()
        {
            auction.BeginPrice = decimal.Parse(ConfigurationManager.AppSettings.Get("MinAuctionBeginPrice")) / 2;
            context.MemberName = "BeginPrice";

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooSmallAuctionBeginPrice, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNullBeginPrice.
        /// </summary>
        [TestMethod]
        public void TestMethodNullBeginPrice()
        {
            auction.BeginPrice = null;
            context.MemberName = "BeginPrice";

            var result = Validator.TryValidateProperty(auction.BeginPrice, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginPriceRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodProductsListNotEmpty.
        /// </summary>
        [TestMethod]
        public void TestMethodProductsListNotEmpty()
        {
            auction.Products.Add(new Product());
            context.MemberName = "Products";

            var result = Validator.TryValidateProperty(auction.Products, context, results);

            Assert.AreEqual(0, results.Count); // ErrorMessage.LenghtNotValid
        }

        /// <summary>
        /// The TestMethodProductsNull.
        /// </summary>
        [TestMethod]
        public void TestMethodProductsNull()
        {
            auction.Products = null;
            context.MemberName = "Products";

            var result = Validator.TryValidateProperty(auction.Products, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ProductsRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodBidsListNotEmpty.
        /// </summary>
        [TestMethod]
        public void TestMethodBidsListNotEmpty()
        {
            auction.Bids.Add(new Bid());
            context.MemberName = "Bids";

            var result = Validator.TryValidateProperty(auction.Bids, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodBidsNull.
        /// </summary>
        [TestMethod]
        public void TestMethodBidsNull()
        {
            auction.Bids = null;
            context.MemberName = "Bids";

            var result = Validator.TryValidateProperty(auction.Bids, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BidsRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodBeginDateNull.
        /// </summary>
        [TestMethod]
        public void TestMethodBeginDateNull()
        {
            auction.BeginDate = null;
            context.MemberName = "BeginDate";

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodBeginDateAfterEndDate.
        /// </summary>
        [TestMethod]
        public void TestMethodBeginDateAfterEndDate()
        {
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now;
            context.MemberName = "BeginDate";

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateIsAfterEndDate, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodBeginDateInThePast.
        /// </summary>
        [TestMethod]
        public void TestMethodBeginDateInThePast()
        {
            auction.BeginDate = new DateTime(2000, 1, 1);
            auction.EndDate = DateTime.Now;
            context.MemberName = "BeginDate";

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateShouldNotBeInThePast, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodValidBeginDate.
        /// </summary>
        [TestMethod]
        public void TestMethodValidBeginDate()
        {
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddDays(1);
            context.MemberName = "BeginDate";

            var result = Validator.TryValidateProperty(auction.BeginDate, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodEndDateNull.
        /// </summary>
        [TestMethod]
        public void TestMethodEndDateNull()
        {
            auction.EndDate = null;
            context.MemberName = "EndDate";

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.EndDateRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodEndDateBeforeBeginDate.
        /// </summary>
        [TestMethod]
        public void TestMethodEndDateBeforeBeginDate()
        {
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now;
            context.MemberName = "EndDate";

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.EndDateIsBeforeBeginDate, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodAuctionPeriodTooLarge.
        /// </summary>
        [TestMethod]
        public void TestMethodAuctionPeriodTooLarge()
        {
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(5);
            context.MemberName = "EndDate";

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionPeriodIsTooLarge, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodValidEndDate.
        /// </summary>
        [TestMethod]
        public void TestMethodValidEndDate()
        {
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(4);
            context.MemberName = "EndDate";

            var result = Validator.TryValidateProperty(auction.EndDate, context, results);

            Assert.AreEqual(0, results.Count);
        }


        [TestMethod]
        public void TestMethodNullCurrencyName()
        {
            auction.CurrencyName = null;
            context.MemberName = "CurrencyName";

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidCurrencyName()
        {
            auction.CurrencyName = CurrencyNameValidator.GetCurrenciesEnglishNames()[0];
            context.MemberName = "CurrencyName";

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodInvalidCurrencyName()
        {
            auction.CurrencyName = "InvalidCurrencyName";
            context.MemberName = "CurrencyName";

            var result = Validator.TryValidateProperty(auction.CurrencyName, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameIsNotValid, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodNullOwner()
        {
            auction.Owner = null;
            context.MemberName = "Owner";

            var result = Validator.TryValidateProperty(auction.Owner, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.OwnerRequired, res.ErrorMessage);
        }

    }
}
