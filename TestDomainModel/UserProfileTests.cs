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
        public void Email_Null()
        {
            context.MemberName = nameof(UserProfile.Email);

            var result = Validator.TryValidateProperty(userProfile.Email, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.EmailRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Email_Valid()
        {
            userProfile.Email = "michea.paul@yahoo.com";
            context.MemberName = nameof(UserProfile.Email);

            var result = Validator.TryValidateProperty(userProfile.Email, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Email_Invalid()
        {
            userProfile.Email = "michea.paul.com";
            context.MemberName = nameof(UserProfile.Email);

            var result = Validator.TryValidateProperty(userProfile.Email, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidEmailAddress, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_Null()
        {
            userProfile.Username = null;
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.UsernameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_TooLong()
        {
            userProfile.Username = "usernameeeeeeeeeeeeeeee";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_TooShort()
        {
            userProfile.Username = "usern";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_PointAtBegin()
        {
            userProfile.Username = ".paul.michea";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_UnderlineAtBegin()
        {
            userProfile.Username = "_paul.michea";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_PointUnderlineInside()
        {
            userProfile.Username = "paul._michea";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_UnderlinePointInside()
        {
            userProfile.Username = "paul_.michea";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_MultipleUnderlineInside()
        {
            userProfile.Username = "paul__michea";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_MultiplePointInside()
        {
            userProfile.Username = "paul..michea";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_ForbiddenCharacter()
        {
            userProfile.Username = "paul&michea";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_PointAtEnd()
        {
            userProfile.Username = "paulmichea.";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_UnderlineAtEnd()
        {
            userProfile.Username = "paulmichea_";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidUsername, res.ErrorMessage);
        }

        [TestMethod]
        public void Username_Valid()
        {
            userProfile.Username = "paul.michea0";
            context.MemberName = nameof(UserProfile.Username);

            var result = Validator.TryValidateProperty(userProfile.Username, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Password_TooLong()
        {
            userProfile.Password = "Password0&aaaaaaaa";
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPassword, res.ErrorMessage);
        }

        [TestMethod]
        public void Password_TooShort()
        {
            userProfile.Password = "Pass0&";
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPassword, res.ErrorMessage);
        }

        [TestMethod]
        public void Password_NoUppercaseLetter()
        {
            userProfile.Password = "password0&";
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPassword, res.ErrorMessage);
        }

        [TestMethod]
        public void Password_NoLowercaseLetter()
        {
            userProfile.Password = "PASSWORD0&";
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPassword, res.ErrorMessage);
        }

        [TestMethod]
        public void Password_NoNumber()
        {
            userProfile.Password = "Password&";
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPassword, res.ErrorMessage);
        }

        [TestMethod]
        public void Password_NoSpecialCharacter()
        {
            userProfile.Password = "Password0";
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPassword, res.ErrorMessage);
        }

        [TestMethod]
        public void Password_Valid()
        {
            userProfile.Password = "Password0&";
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Password_Null()
        {
            userProfile.Password = null;
            context.MemberName = nameof(UserProfile.Password);

            var result = Validator.TryValidateProperty(userProfile.Password, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.PasswordRequired, res.ErrorMessage);
        }
    }
}
