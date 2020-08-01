//-----------------------------------------------------------------------
// <copyright file="CategoryTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="CategoryTests" />.
    /// </summary>
    [TestClass]
    public class CategoryTests
    {
        /// <summary>
        /// Defines the category.
        /// </summary>
        private Category category;

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
            category = new Category();
            context = new ValidationContext(category);
            results = new List<ValidationResult>();
        }

        /// <summary>
        /// The TestMethodValidName.
        /// </summary>
        [TestMethod]
        public void TestMethodValidName()
        {
            category.Name = "RandomName";
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(category.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodNameTooLong.
        /// </summary>
        [TestMethod]
        public void TestMethodNameTooLong()
        {
            category.Name = new string('a', 51);
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(category.Name, context, results);

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
            category.Name = new string('a', 1);
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(category.Name, context, results);

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
            category.Name = null;
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(category.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodProductsNull.
        /// </summary>
        [TestMethod]
        public void TestMethodProductsNull()
        {
            category.Products = null;
            context.MemberName = "Products";

            var result = Validator.TryValidateProperty(category.Products, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ProductsRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodParentCategoriesNull.
        /// </summary>
        [TestMethod]
        public void TestMethodParentCategoriesNull()
        {
            category.ParentCategories = null;
            context.MemberName = "ParentCategories";

            var result = Validator.TryValidateProperty(category.ParentCategories, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ParentCategoriesRequired, res.ErrorMessage);
        }
    }
}
