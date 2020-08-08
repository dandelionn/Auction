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
    }
}
