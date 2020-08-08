//-----------------------------------------------------------------------
// <copyright file="BidderTests.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TestDomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    class BidderTests
    {
        private Bidder bidder;

        private ValidationContext context;

        private List<ValidationResult> results;

        [TestInitialize]
        public void TestInit()
        {
            bidder = new Bidder();
            context = new ValidationContext(bidder);
            results = new List<ValidationResult>();
        }

        [TestMethod]
        public void TestMethodBidsNotNull()
        {
            Assert.IsNotNull(bidder.Bids);
        }
    }
}
