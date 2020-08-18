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
    public class SellerServicesTests
    {
        internal Mock<ISellerRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<ISellerRepository>();
        }

        [TestMethod]
        public void Delete_Null()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Seller>())).Throws<NullReferenceException>();

            var sellerServices = new SellerServices(mockRepository.Object);

            Action act = () => sellerServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Delete_Entity()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Seller>())); ;

            var sellerServices = new SellerServices(mockRepository.Object);

            Action act = () => sellerServices.Delete(new Seller());
            act.Should().NotThrow();
        }

        private static IList<Seller> GetSellers()
        {
            var sellers = new List<Seller>();
            sellers.Add(
                new Seller
                {
                    Person = new Person(),
                }
            );

            return sellers;
        }

        [TestMethod]
        public void GetAll_NotEmpty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Seller, bool>>>(),
                                           It.IsAny<Func<IQueryable<Seller>, IOrderedQueryable<Seller>>>(),
                                           It.IsAny<string>())).Returns(GetSellers());

            var services = new SellerServices(mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetAll_Empty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Seller, bool>>>(),
                                           It.IsAny<Func<System.Linq.IQueryable<Seller>, IOrderedQueryable<Seller>>>(),
                                           It.IsAny<string>())).Returns(new List<Seller>());

            var services = new SellerServices(mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        [TestMethod]
        public void GetByID_EntityFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Seller { Id = 0 });
            var services = new SellerServices(mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new SellerServices(mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }
    }
}