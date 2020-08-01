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
        /// The TestMethodValidName.
        /// </summary>
        [TestMethod]
        public void TestMethodValidName()
        {
            product.Name = "RandomName";
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(product.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodNameTooLong.
        /// </summary>
        [TestMethod]
        public void TestMethodNameTooLong()
        {
            product.Name = new string('a', 51);
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(product.Name, context, results);

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
            product.Name = new string('a', 1);
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(product.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        /// <summary>
        /// The TestNullName.
        /// </summary>
        [TestMethod]
        public void TestMethodNullName()
        {
            product.Name = null;
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(product.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodAuctionsNull.
        /// </summary>
        [TestMethod]
        public void TestMethodAuctionsNull()
        {
            product.Auctions = null;
            context.MemberName = "Auctions";

            var result = Validator.TryValidateProperty(product.Auctions, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionsRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodCategoriesListNotEmpty.
        /// </summary>
        [TestMethod]
        public void TestMethodCategoriesListNotEmpty()
        {
            product.Categories.Add(new Category());
            context.MemberName = "Categories";

            var result = Validator.TryValidateProperty(product.Categories, context, results);

            Assert.AreEqual(0, results.Count); // ErrorMessage.LenghtNotValid
        }

        /// <summary>
        /// The TestMethodCategoriesNull.
        /// </summary>
        [TestMethod]
        public void TestMethodCategoriesNull()
        {
            product.Categories = null;
            context.MemberName = "Categories";

            var result = Validator.TryValidateProperty(product.Categories, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CategoriesRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodUsersListNotEmpty.
        /// </summary>
        [TestMethod]
        public void TestMethodUsersListNotEmpty()
        {
            product.Users.Add(new User());
            context.MemberName = "Users";

            var result = Validator.TryValidateProperty(product.Users, context, results);

            Assert.AreEqual(0, results.Count); // ErrorMessage.LenghtNotValid
        }

        /// <summary>
        /// The TestMethodUsersNull.
        /// </summary>
        [TestMethod]
        public void TestMethodUsersNull()
        {
            product.Users = null;
            context.MemberName = "Users";

            var result = Validator.TryValidateProperty(product.Users, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.UsersRequired, res.ErrorMessage);
        }
    }
}
