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

        private ValidationContext context;

        private List<ValidationResult> results;

        [TestInitialize]
        public void TestInit()
        {
            category = new Category();
            context = new ValidationContext(category);
            results = new List<ValidationResult>();
        }

        [TestMethod]
        public void Name_Valid()
        {
            category.Name = "RandomName";
            context.MemberName = nameof(Category.Name);

            var result = Validator.TryValidateProperty(category.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Name_TooLong()
        {
            category.Name = new string('a', 51);
            context.MemberName = nameof(Category.Name);

            var result = Validator.TryValidateProperty(category.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void Name_TooShort()
        {
            category.Name = new string('a', 1);
            context.MemberName = nameof(Category.Name);

            var result = Validator.TryValidateProperty(category.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void Name_Null()
        {
            category.Name = null;
            context.MemberName = nameof(Category.Name);

            var result = Validator.TryValidateProperty(category.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Products_NotNull()
        {
            Assert.IsNotNull(category.Products);
        }

        [TestMethod]
        public void ParentCategories_NotNull()
        {
            Assert.IsNotNull(category.ParentCategories);
        }
    }
}
