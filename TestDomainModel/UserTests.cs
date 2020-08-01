//-----------------------------------------------------------------------
// <copyright file="UserTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="UserTests" />.
    /// </summary>
    [TestClass]
    public class UserTests
    {
        /// <summary>
        /// Defines the user.
        /// </summary>
        private User user;

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
            user = new User();
            context = new ValidationContext(user);
            results = new List<ValidationResult>();
        }

        /// <summary>
        /// The TestMethodValidName.
        /// </summary>
        [TestMethod]
        public void TestMethodValidName()
        {
            user.Name = "RandomName";
            context.MemberName = "Name";
            var result = Validator.TryValidateProperty(user.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodNameTooLong.
        /// </summary>
        [TestMethod]
        public void TestMethodNameTooLong()
        {
            user.Name = new string('a', 51);
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(user.Name, context, results);

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
            user.Name = new string('a', 1);
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(user.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNullName.
        /// </summary>
        [TestMethod]
        public void TestMethodNullName()
        {
            user.Name = null;
            context.MemberName = "Name";

            var result = Validator.TryValidateProperty(user.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodValidSurname.
        /// </summary>
        [TestMethod]
        public void TestMethodValidSurname()
        {
            user.Surname = "RandomSurname";
            context.MemberName = "Surname";

            var result = Validator.TryValidateProperty(user.Surname, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodSurnameTooLong.
        /// </summary>
        [TestMethod]
        public void TestMethodSurnameTooLong()
        {
            user.Surname = new string('a', 51);
            context.MemberName = "Surname";

            var result = Validator.TryValidateProperty(user.Surname, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodSurnameTooShort.
        /// </summary>
        [TestMethod]
        public void TestMethodSurnameTooShort()
        {
            user.Surname = new string('a', 1);
            context.MemberName = "Surname";

            var result = Validator.TryValidateProperty(user.Surname, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNullSurname.
        /// </summary>
        [TestMethod]
        public void TestMethodNullSurname()
        {
            user.Surname = null;
            context.MemberName = "Surname";

            var result = Validator.TryValidateProperty(user.Surname, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.SurnameRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodValidAddress.
        /// </summary>
        [TestMethod]
        public void TestMethodValidAddress()
        {
            user.Name = "RandomAdress";
            context.MemberName = "Address";

            var result = Validator.TryValidateProperty(user.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodTooLongAddress.
        /// </summary>
        [TestMethod]
        public void TestMethodTooLongAddress()
        {
            user.Address = new string('a', 101);
            context.MemberName = "Address";

            var result = Validator.TryValidateProperty(user.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodAddressTooShort.
        /// </summary>
        [TestMethod]
        public void TestMethodAddressTooShort()
        {
            user.Address = new string('a', 1);
            context.MemberName = "Address";

            var result = Validator.TryValidateProperty(user.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodValidPhoneNumber.
        /// </summary>
        [TestMethod]
        public void TestMethodValidPhoneNumber()
        {
            user.PhoneNumber = "0758988360";
            context.MemberName = "PhoneNumber";

            var result = Validator.TryValidateProperty(user.PhoneNumber, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodInvalidPhoneNumber.
        /// </summary>
        [TestMethod]
        public void TestMethodInvalidPhoneNumber()
        {
            user.PhoneNumber = "123";
            context.MemberName = "PhoneNumber";

            var result = Validator.TryValidateProperty(user.PhoneNumber, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NotAValidPhoneNumber, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNullPhoneNumber.
        /// </summary>
        [TestMethod]
        public void TestMethodNullPhoneNumber()
        {
            context.MemberName = "PhoneNumber";

            var result = Validator.TryValidateProperty(user.PhoneNumber, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.PhoneOrEmailRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodValidEmail.
        /// </summary>
        [TestMethod]
        public void TestMethodValidEmail()
        {
            user.Email = "michea.paul@yahoo.com";
            context.MemberName = "Email";

            var result = Validator.TryValidateProperty(user.Email, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The TestMethodInvalidEmail.
        /// </summary>
        [TestMethod]
        public void TestMethodInvalidEmail()
        {
            user.Email = "michea.paul.com";
            context.MemberName = "Email";

            var result = Validator.TryValidateProperty(user.Email, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NotAValidEmailAddress, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodNullEmail.
        /// </summary>
        [TestMethod]
        public void TestMethodNullEmail()
        {
            context.MemberName = "Email";

            var result = Validator.TryValidateProperty(user.Email, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.PhoneOrEmailRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodProductsNull.
        /// </summary>
        [TestMethod]
        public void TestMethodProductsNull()
        {
            user.Products = null;
            context.MemberName = "Products";

            var result = Validator.TryValidateProperty(user.Products, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ProductsRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The TestMethodBidsNull.
        /// </summary>
        [TestMethod]
        public void TestMethodBidsNull()
        {
            user.Bids = null;
            context.MemberName = "Bids";

            var result = Validator.TryValidateProperty(user.Bids, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BidsRequired, res.ErrorMessage);
        }
    }
}
