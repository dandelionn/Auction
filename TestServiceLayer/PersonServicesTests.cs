using DataMapper;
using DomainModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.ServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestServiceLayer
{
    [TestClass]
    public class PersonServicesTests
    {
        internal Mock<IPersonRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<IPersonRepository>();
        }

        [TestMethod]
        public void Delete_Null()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Person>())).Throws<NullReferenceException>();

            var personServices = new PersonServices(mockRepository.Object);

            Action act = () => personServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Delete_Entity()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Person>())); ;

            var personServices = new PersonServices(mockRepository.Object);

            Action act = () => personServices.Delete(new Person());
            act.Should().NotThrow();
        }

        private static IList<Person> getPersons()
        {
            var persons = new List<Person>();
            persons.Add(
                new Person
                {
                    Name = nameof(Person),
                }
            );

            return persons;
        }

        [TestMethod]
        public void GetAll_NotEmpty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Person, bool>>>(),
                                           It.IsAny<Func<IQueryable<Person>, IOrderedQueryable<Person>>>(),
                                           It.IsAny<string>())).Returns(getPersons());

            var services = new PersonServices(mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetAll_Empty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Person, bool>>>(),
                                           It.IsAny<Func<IQueryable<Person>, IOrderedQueryable<Person>>>(),
                                           It.IsAny<string>())).Returns(new List<Person>());

            var services = new PersonServices(mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        [TestMethod]
        public void GetByID_EntityFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Person { Id = 0 });
            var services = new PersonServices(mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new PersonServices(mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }
    }
}
