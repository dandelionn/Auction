//-----------------------------------------------------------------------
// <copyright file="PersonServiceTests.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TestServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataMapper;
    using DomainModel;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ServiceLayer.ServiceImplementation;

    /// <summary>
    /// Defines the <see cref="PersonServiceTests" />.
    /// </summary>
    [TestClass]
    public class PersonServiceTests
    {
        /// <summary>
        /// Defines the mockRepository.
        /// </summary>
        private Mock<IPersonRepository> mockRepository;

        /// <summary>
        /// The TestInit.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this.mockRepository = new Mock<IPersonRepository>();
        }

        /// <summary>
        /// The Delete_Null.
        /// </summary>
        [TestMethod]
        public void Delete_Null()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Person>())).Throws<NullReferenceException>();

            var personServices = new PersonService(this.mockRepository.Object);

            Action act = () => personServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Delete_Entity.
        /// </summary>
        [TestMethod]
        public void Delete_Entity()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Person>()));

            var personServices = new PersonService(this.mockRepository.Object);

            Action act = () => personServices.Delete(new Person());
            act.Should().NotThrow();
        }

        /// <summary>
        /// The GetAll_NotEmpty.
        /// </summary>
        [TestMethod]
        public void GetAll_NotEmpty()
        {
            this.mockRepository.Setup(
                                x => x.Get(
                                           It.IsAny<Expression<Func<Person, bool>>>(),
                                           It.IsAny<Func<IQueryable<Person>, IOrderedQueryable<Person>>>(),
                                           It.IsAny<string>())).Returns(GetPersons());

            var services = new PersonService(this.mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        /// <summary>
        /// The GetAll_Empty.
        /// </summary>
        [TestMethod]
        public void GetAll_Empty()
        {
            this.mockRepository.Setup(
                                x => x.Get(
                                           It.IsAny<Expression<Func<Person, bool>>>(),
                                           It.IsAny<Func<IQueryable<Person>, IOrderedQueryable<Person>>>(),
                                           It.IsAny<string>())).Returns(new List<Person>());

            var services = new PersonService(this.mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        /// <summary>
        /// The GetByID_EntityFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Person { Id = 0 });
            var services = new PersonService(this.mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        /// <summary>
        /// The GetByID_EntityNotFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new PersonService(this.mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        /// <summary>
        /// The getPersons.
        /// </summary>
        /// <returns>The <see cref="IList{Person}"/>.</returns>
        private static IList<Person> GetPersons()
        {
            var persons = new List<Person>();
            persons.Add(
                new Person
                {
                    Name = nameof(Person),
                });

            return persons;
        }
    }
}
