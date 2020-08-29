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
        /// The Bidder_Null.
        /// </summary>
        [TestMethod]
        public void Bidder_Null()
        {
            bid.Bidder = null;
            context.MemberName = nameof(Bidder);

            var result = Validator.TryValidateProperty(bid.Bidder, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BidderRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The Bidder_NotNull.
        /// </summary>
        [TestMethod]
        public void Bidder_NotNull()
        {
            bid.Bidder = new Bidder();
            context.MemberName = nameof(Bidder);

            var result = Validator.TryValidateProperty(bid.Bidder, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Auction_Null.
        /// </summary>
        [TestMethod]
        public void Auction_Null()
        {
            bid.Auction = null;
            context.MemberName = nameof(Auction);

            var result = Validator.TryValidateProperty(bid.Auction, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The Auction_NotNull.
        /// </summary>
        [TestMethod]
        public void Auction_NotNull()
        {
            bid.Auction = new Auction();
            context.MemberName = nameof(Auction);

            var result = Validator.TryValidateProperty(bid.Auction, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Value_Null.
        /// </summary>
        [TestMethod]
        public void Value_Null()
        {
            bid.Value = null;
            context.MemberName = nameof(Bid.Value);

            var result = Validator.TryValidateProperty(bid.Value, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ValueRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The Value_Zero.
        /// </summary>
        [TestMethod]
        public void Value_Zero()
        {
            bid.Value = (decimal)0;
            context.MemberName = nameof(Bid.Value);

            var result = Validator.TryValidateProperty(bid.Value, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ValueShouldNotBeZero, res.ErrorMessage);
        }

        /// <summary>
        /// The Value_Negative.
        /// </summary>
        [TestMethod]
        public void Value_Negative()
        {
            bid.Value = (decimal)-25.6;
            context.MemberName = nameof(Bid.Value);

            var result = Validator.TryValidateProperty(bid.Value, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NegativeValue, res.ErrorMessage);
        }

        /// <summary>
        /// The Value_Valid.
        /// </summary>
        [TestMethod]
        public void Value_Valid()
        {
            bid.Value = (decimal)25.6;
            context.MemberName = nameof(Bid.Value);

            var result = Validator.TryValidateProperty(bid.Value, context, results);

            Assert.AreEqual(0, results.Count);
        }
    }
}
