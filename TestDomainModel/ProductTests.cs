//-----------------------------------------------------------------------
// <copyright file="ProductTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="ProductTests" />.
    /// </summary>
    [TestClass]
    public class ProductTests
    {
        /// <summary>
        /// Defines the product.
        /// </summary>
        private Product product;

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
            product = new Product();
            context = new ValidationContext(product);
            results = new List<ValidationResult>();
        }

        /// <summary>
        /// The Name_Null.
        /// </summary>
        [TestMethod]
        public void Name_Null()
        {
            product.Name = null;
            context.MemberName = nameof(Product.Name);

            var result = Validator.TryValidateProperty(product.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The Name_TooLong.
        /// </summary>
        [TestMethod]
        public void Name_TooLong()
        {
            product.Name = new string('a', 51);
            context.MemberName = nameof(Product.Name);

            var result = Validator.TryValidateProperty(product.Name, context, results);

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
            product.Name = new string('a', 1);
            context.MemberName = nameof(Product.Name);

            var result = Validator.TryValidateProperty(product.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        /// <summary>
        /// The Name_Valid.
        /// </summary>
        [TestMethod]
        public void Name_Valid()
        {
            product.Name = "RandomName";
            context.MemberName = nameof(Product.Name);

            var result = Validator.TryValidateProperty(product.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Categories_Null.
        /// </summary>
        [TestMethod]
        public void Categories_Null()
        {
            product.Categories = null;
            context.MemberName = nameof(Product.Categories);

            var result = Validator.TryValidateProperty(product.Categories, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CategoriesRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The Categories_ListEmpty.
        /// </summary>
        [TestMethod]
        public void Categories_ListEmpty()
        {
            product.Categories = new List<Category>();
            context.MemberName = nameof(Product.Categories);

            var result = Validator.TryValidateProperty(product.Categories, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthMustBeGreaterThanZero, res.ErrorMessage);
        }

        /// <summary>
        /// The Categories_Valid.
        /// </summary>
        [TestMethod]
        public void Categories_Valid()
        {
            product.Categories.Add(new Category());
            context.MemberName = nameof(Product.Categories);

            var result = Validator.TryValidateProperty(product.Categories, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Sellers_Null.
        /// </summary>
        [TestMethod]
        public void Sellers_Null()
        {
            product.Sellers = null;
            context.MemberName = nameof(Product.Sellers);

            var result = Validator.TryValidateProperty(product.Sellers, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.SellersRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The Sellers_ListEmpty.
        /// </summary>
        [TestMethod]
        public void Sellers_ListEmpty()
        {
            product.Sellers = new List<Seller>();
            context.MemberName = nameof(Product.Sellers);

            var result = Validator.TryValidateProperty(product.Sellers, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthMustBeGreaterThanZero, res.ErrorMessage);
        }

        /// <summary>
        /// The Sellers_Valid.
        /// </summary>
        [TestMethod]
        public void Sellers_Valid()
        {
            product.Sellers.Add(new Seller());
            context.MemberName = nameof(Product.Sellers);

            var result = Validator.TryValidateProperty(product.Sellers, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Auctions_NotNull.
        /// </summary>
        [TestMethod]
        public void Auctions_NotNull()
        {
            Assert.IsNotNull(product.Auctions);
        }
    }
}
