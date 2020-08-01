//-----------------------------------------------------------------------
// <copyright file="BidTests.cs" company="Transilvania University of Brasov">
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
    /// Defines the <see cref="BidTests" />.
    /// </summary>
    [TestClass]
    public class BidTests
    {
        /// <summary>
        /// Defines the bid.
        /// </summary>
        private Bid bid;

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
            bid = new Bid();
            context = new ValidationContext(bid);
            results = new List<ValidationResult>();
        }

        /// <summary>
        /// The TestReaderNull.
        /// </summary>
        [TestMethod]
        public void TestMethodUserNull()
        {
            bid.User = null;
            context.MemberName = nameof(User);

            var result = Validator.TryValidateProperty(bid.User, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.UserRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestReaderNotNull.
        /// </summary>
        [TestMethod]
        public void TestMethodUserNotNull()
        {
            bid.User = new User();
            context.MemberName = nameof(User);

            var result = Validator.TryValidateProperty(bid.User, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestAuctionNull.
        /// </summary>
        [TestMethod]
        public void TestMethodAuctionNull()
        {
            bid.Auction = null;
            context.MemberName = nameof(Auction);

            var result = Validator.TryValidateProperty(bid.Auction, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestAuctionNotNull.
        /// </summary>
        [TestMethod]
        public void TestMethodAuctionNotNull()
        {
            bid.Auction = new Auction();
            context.MemberName = nameof(Auction);

            var result = Validator.TryValidateProperty(bid.Auction, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodValidValue.
        /// </summary>
        [TestMethod]
        public void TestMethodValidValue()
        {
            bid.Value = 25.6;
            context.MemberName = "Value";

            var result = Validator.TryValidateProperty(bid.Value, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodNegativeValue.
        /// </summary>
        [TestMethod]
        public void TestMethodNegativeValue()
        {
            bid.Value = -25.6;
            context.MemberName = "Value";

            var result = Validator.TryValidateProperty(bid.Value, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NegativeValue, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNullValue.
        /// </summary>
        [TestMethod]
        public void TestMethodNullValue()
        {
            bid.Value = null;
            context.MemberName = "Value";

            var result = Validator.TryValidateProperty(bid.Value, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ValueRequired, res.ErrorMessage);
        }
    }
}
