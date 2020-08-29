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

    /// <summary>
    /// Defines the <see cref="BidderTests" />.
    /// </summary>
    [TestClass]
    internal class BidderTests
    {
        /// <summary>
        /// Defines the bidder.
        /// </summary>
        private Bidder bidder;

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
            bidder = new Bidder();
            context = new ValidationContext(bidder);
            results = new List<ValidationResult>();
        }

        /// <summary>
        /// The Bids_NotNull.
        /// </summary>
        [TestMethod]
        public void Bids_NotNull()
        {
            Assert.IsNotNull(bidder.Bids);
        }
    }
}
