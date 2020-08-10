//-----------------------------------------------------------------------
// <copyright file="SellerTests.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestDomainModel
{
    [TestClass]
    public class SellerTests
    {
        private Seller seller;

        private ValidationContext context;

        private List<ValidationResult> results;

        [TestInitialize]
        public void TestInit()
        {
            seller = new Seller();
            context = new ValidationContext(seller);
            results = new List<ValidationResult>();
        }

        [TestMethod]
        public void TestMethodAuctionsNotNull()
        {
            Assert.IsNotNull(seller.Auctions);
        }

        [TestMethod]
        public void TestMethodBidsNotNull()
        {
            Assert.IsNotNull(seller.Products);
        }
    }
}
