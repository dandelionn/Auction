//-----------------------------------------------------------------------
// <copyright file="SellerTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="SellerTests" />.
    /// </summary>
    [TestClass]
    public class SellerTests
    {
        /// <summary>
        /// Defines the seller.
        /// </summary>
        private Seller seller;

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
            seller = new Seller();
            context = new ValidationContext(seller);
            results = new List<ValidationResult>();
        }

        /// <summary>
        /// The Auctions_NotNull.
        /// </summary>
        [TestMethod]
        public void Auctions_NotNull()
        {
            Assert.IsNotNull(seller.Auctions);
        }

        /// <summary>
        /// The Products_NotNull.
        /// </summary>
        [TestMethod]
        public void Products_NotNull()
        {
            Assert.IsNotNull(seller.Products);
        }
    }
}
