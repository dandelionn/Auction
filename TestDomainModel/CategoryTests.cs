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
        /// The Name_Valid.
        /// </summary>
        [TestMethod]
        public void Name_Valid()
        {
            category.Name = "RandomName";
            context.MemberName = nameof(Category.Name);

            var result = Validator.TryValidateProperty(category.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Name_TooLong.
        /// </summary>
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

        /// <summary>
        /// The Name_TooShort.
        /// </summary>
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

        /// <summary>
        /// The Name_Null.
        /// </summary>
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

        /// <summary>
        /// The Products_NotNull.
        /// </summary>
        [TestMethod]
        public void Products_NotNull()
        {
            Assert.IsNotNull(category.Products);
        }

        /// <summary>
        /// The ParentCategories_NotNull.
        /// </summary>
        [TestMethod]
        public void ParentCategories_NotNull()
        {
            Assert.IsNotNull(category.ParentCategories);
        }
    }
}
