//-----------------------------------------------------------------------
// <copyright file="PersonTests.cs" company="Transilvania University of Brasov">    
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

    [TestClass]
    public class PersonTests
    {
        private Person person;

        private ValidationContext context;

        private List<ValidationResult> results;

        [TestInitialize]
        public void TestInit()
        {
            person = new Person();
            context = new ValidationContext(person);
            results = new List<ValidationResult>();
        }

        [TestMethod]
        public void TestMethodNullName()
        {
            person.Name = null;
            context.MemberName = nameof(Person.Name);

            var result = Validator.TryValidateProperty(person.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidName()
        {
            person.Name = "RandomName";
            context.MemberName = nameof(Person.Name);
            var result = Validator.TryValidateProperty(person.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodNameTooLong()
        {
            person.Name = new string('a', 51);
            context.MemberName = nameof(Person.Name);

            var result = Validator.TryValidateProperty(person.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodNameTooShort()
        {
            person.Name = new string('a', 1);
            context.MemberName = nameof(Person.Name);

            var result = Validator.TryValidateProperty(person.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodNullSurname()
        {
            person.Surname = null;
            context.MemberName = nameof(Person.Surname);

            var result = Validator.TryValidateProperty(person.Surname, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.SurnameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidSurname()
        {
            person.Surname = "RandomSurname";
            context.MemberName = nameof(Person.Surname);

            var result = Validator.TryValidateProperty(person.Surname, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodSurnameTooLong()
        {
            person.Surname = new string('a', 51);
            context.MemberName = nameof(Person.Surname);

            var result = Validator.TryValidateProperty(person.Surname, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodSurnameTooShort()
        {
            person.Surname = new string('a', 1);
            context.MemberName = nameof(Person.Surname);

            var result = Validator.TryValidateProperty(person.Surname, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidAddress()
        {
            person.Name = "RandomAdress";
            context.MemberName = nameof(Person.Address);

            var result = Validator.TryValidateProperty(person.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodTooLongAddress()
        {
            person.Address = new string('a', 101);
            context.MemberName = nameof(Person.Address);

            var result = Validator.TryValidateProperty(person.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodAddressTooShort()
        {
            person.Address = new string('a', 1);
            context.MemberName = nameof(Person.Address);

            var result = Validator.TryValidateProperty(person.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodNullPhoneNumber()
        {
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.PhoneNumberRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void TestMethodValidPhoneNumber()
        {
            person.PhoneNumber = "0758988360";
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestMethodInvalidPhoneNumber()
        {
            person.PhoneNumber = "000";
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPhoneNumber, res.ErrorMessage);
        }
    }
}
