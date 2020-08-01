//-----------------------------------------------------------------------
// <copyright file="AuctionTests.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TestDomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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
        /// The TestMethodValidPrice.
        /// </summary>
        [TestMethod]
        public void TestMethodValidPrice()
        {
            auction.Price = 25.6;
            context.MemberName = "Price";

            var result = Validator.TryValidateProperty(auction.Price, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodNegativePrice.
        /// </summary>
        [TestMethod]
        public void TestMethodNegativePrice()
        {
            auction.Price = -25.6;
            context.MemberName = "Price";

            var result = Validator.TryValidateProperty(auction.Price, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NegativePrice, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNullPrice.
        /// </summary>
        [TestMethod]
        public void TestMethodNullPrice()
        {
            auction.Price = null;
            context.MemberName = "Price";

            var result = Validator.TryValidateProperty(auction.Price, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.PriceRequired, res.ErrorMessage);
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
    }
}
