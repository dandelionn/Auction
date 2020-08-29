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

    /// <summary>
    /// Defines the <see cref="PersonTests" />.
    /// </summary>
    [TestClass]
    public class PersonTests
    {
        /// <summary>
        /// Defines the person.
        /// </summary>
        private Person person;

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
            person = new Person();
            context = new ValidationContext(person);
            results = new List<ValidationResult>();
        }

        /// <summary>
        /// The Name_Null.
        /// </summary>
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

        /// <summary>
        /// The Name_Valid.
        /// </summary>
        [TestMethod]
        public void Name_Valid()
        {
            person.Name = "RandomName";
            context.MemberName = nameof(Person.Name);
            var result = Validator.TryValidateProperty(person.Name, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Name_TooLong.
        /// </summary>
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

        /// <summary>
        /// The Name_TooShort.
        /// </summary>
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

        /// <summary>
        /// The Surname_Null.
        /// </summary>
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

        /// <summary>
        /// The Surname_Valid.
        /// </summary>
        [TestMethod]
        public void Surname_Valid()
        {
            person.Surname = "RandomSurname";
            context.MemberName = nameof(Person.Surname);

            var result = Validator.TryValidateProperty(person.Surname, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Surname_TooLong.
        /// </summary>
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

        /// <summary>
        /// The Surname_TooShort.
        /// </summary>
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

        /// <summary>
        /// The Address_Null.
        /// </summary>
        [TestMethod]
        public void Address_Null()
        {
            person.Name = null;
            context.MemberName = nameof(Person.Address);

            var result = Validator.TryValidateProperty(person.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Address_Valid.
        /// </summary>
        [TestMethod]
        public void Address_Valid()
        {
            person.Name = "RandomAdress";
            context.MemberName = nameof(Person.Address);

            var result = Validator.TryValidateProperty(person.Address, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Address_TooLong.
        /// </summary>
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

        /// <summary>
        /// The Address_TooShort.
        /// </summary>
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

        /// <summary>
        /// The PhoneNumber_Null.
        /// </summary>
        [TestMethod]
        public void PhoneNumber_Null()
        {
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.PhoneNumberRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The PhoneNumber_Valid.
        /// </summary>
        [TestMethod]
        public void PhoneNumber_Valid()
        {
            person.PhoneNumber = "0758988360";
            context.MemberName = nameof(Person.PhoneNumber);

            var result = Validator.TryValidateProperty(person.PhoneNumber, context, results);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The PhoneNumber_TooShort.
        /// </summary>
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

        /// <summary>
        /// The PhoneNumber_TooLong.
        /// </summary>
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

        /// <summary>
        /// The PhoneNumber_InvalidCharacter.
        /// </summary>
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

        /// <summary>
        /// The Scores_NotNull.
        /// </summary>
        [TestMethod]
        public void Scores_NotNull()
        {
            Assert.IsNotNull(person.Scores);
        }

        /// <summary>
        /// The BanEndDate_NotNull.
        /// </summary>
        [TestMethod]
        public void BanEndDate_NotNull()
        {
            Assert.IsNotNull(person.BanEndDate);
        }
    }
}
