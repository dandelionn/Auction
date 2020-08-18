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
        public void Name_Null()
        {
            person.Name = null;
            context.MemberName = nameof(Person.Name);

            var result = Validator.TryValidateProperty(person.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Name_Valid()
        {
            person.Name = "RandomName";
            context.MemberName = nameof(Person.Name);
            var result = Validator.TryValidateProperty(person.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Name_TooLong()
        {
            person.Name = new string('a', 51);
            context.MemberName = nameof(Person.Name);

            var result = Validator.TryValidateProperty(person.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void Name_TooShort()
        {
            person.Name = new string('a', 1);
            context.MemberName = nameof(Person.Name);

            var result = Validator.TryValidateProperty(person.Name, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void Surname_Null()
        {
            person.Surname = null;
            context.MemberName = nameof(Person.Surname);

            var result = Validator.TryValidateProperty(person.Surname, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.SurnameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Surname_Valid()
        {
            person.Surname = "RandomSurname";
            context.MemberName = nameof(Person.Surname);

            var result = Validator.TryValidateProperty(person.Surname, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Surname_TooLong()
        {
            person.Surname = new string('a', 51);
            context.MemberName = nameof(Person.Surname);

            var result = Validator.TryValidateProperty(person.Surname, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void Surname_TooShort()
        {
            person.Surname = new string('a', 1);
            context.MemberName = nameof(Person.Surname);

            var result = Validator.TryValidateProperty(person.Surname, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And50, res.ErrorMessage);
        }

        [TestMethod]
        public void Address_Null()
        {
            person.Name = null;
            context.MemberName = nameof(Person.Address);

            var result = Validator.TryValidateProperty(person.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Address_Valid()
        {
            person.Name = "RandomAdress";
            context.MemberName = nameof(Person.Address);

            var result = Validator.TryValidateProperty(person.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Address_TooLong()
        {
            person.Address = new string('a', 101);
            context.MemberName = nameof(Person.Address);

            var result = Validator.TryValidateProperty(person.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        [TestMethod]
        public void Address_TooShort()
        {
            person.Address = new string('a', 1);
            context.MemberName = nameof(Person.Address);

            var result = Validator.TryValidateProperty(person.Address, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.LengthBetween2And100, res.ErrorMessage);
        }

        [TestMethod]
        public void PhoneNumber_Null()
        {
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.PhoneNumberRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void PhoneNumber_Valid()
        {
            person.PhoneNumber = "0758988360";
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void PhoneNumber_TooShort()
        {
            person.PhoneNumber = "000";
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPhoneNumber, res.ErrorMessage);
        }

        [TestMethod]
        public void PhoneNumber_TooLong()
        {
            person.PhoneNumber = "1230003420000000430002340000000000";
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPhoneNumber, res.ErrorMessage);
        }

        [TestMethod]
        public void PhoneNumber_InvalidCharacter()
        {
            person.PhoneNumber = "00%34324223423432";
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.InvalidPhoneNumber, res.ErrorMessage);
        }

        [TestMethod]
        public void Scores_NotNull()
        {
            Assert.IsNotNull(person.Scores); ///test for pozitive numbers, between 1 and 10
        }
        
        [TestMethod]
        public void BanEndDate_NotNull()
        {
            Assert.IsNotNull(person.BanEndDate);
        }
    }
}
