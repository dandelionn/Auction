//-----------------------------------------------------------------------
// <copyright file="UserProfileTests.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DomainModel;
using DomainModel.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestDomainModel
{
    [TestClass]
    public class UserProfileTests
    {
        private UserProfile userProfile;

        private ValidationContext context;

        private List<ValidationResult> results;

        [TestInitialize]
        public void TestInit()
        {
            userProfile = new UserProfile();
            context = new ValidationContext(userProfile);
            results = new List<ValidationResult>();
        }

        [TestMethod]
        public void TestMethodNullEmail()
        {
            context.MemberName = nameof(UserProfile.Email);

            var result = Validator.TryValidateProperty(userProfile.Email, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.EmailRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidEmail()
        {
            userProfile.Email = "michea.paul@yahoo.com";
            context.MemberName = nameof(UserProfile.Email);

            var result = Validator.TryValidateProperty(userProfile.Email, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodInvalidEmail()
        {
            userProfile.Email = "michea.paul.com";
            context.MemberName = nameof(UserProfile.Email);

            var result = Validator.TryValidateProperty(userProfile.Email, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidEmailAddress, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodInvalidUsername()
        {
            userProfile.Username = "._us_.er_";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidUsername()
        {
            userProfile.Username = "paul.michea";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodInvalidPasswod()
        {
            userProfile.Password = "123456";
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPassword, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidPassword()
        {
            userProfile.Password = "Password0&";
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(0, results.Count);
        }
    }
}
