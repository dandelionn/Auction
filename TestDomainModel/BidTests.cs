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
        private Bid bid;

        private ValidationContext context;

        private List<ValidationResult> results;

        [TestInitialize]
        public void TestInit()
        {
            bid = new Bid();
            context = new ValidationContext(bid);
            results = new List<ValidationResult>();
        }

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

        [TestMethod]
        public void Bidder_NotNull()
        {
            bid.Bidder = new Bidder();
            context.MemberName = nameof(Bidder);

            var result = Validator.TryValidateProperty(bid.Bidder, context, results);

            Assert.AreEqual(0, results.Count);
        }

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

        [TestMethod]
        public void Auction_NotNull()
        {
            bid.Auction = new Auction();
            context.MemberName = nameof(Auction);

            var result = Validator.TryValidateProperty(bid.Auction, context, results);

            Assert.AreEqual(0, results.Count);
        }

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
