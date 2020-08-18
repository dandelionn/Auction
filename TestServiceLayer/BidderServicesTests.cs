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
    public class BidderServicesTests
    {
        internal Mock<IBidderRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<IBidderRepository>();
        }

        [TestMethod]
        public void Delete_Null()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Bidder>())).Throws<NullReferenceException>();

            var bidderServices = new BidderServices(mockRepository.Object);

            Action act = () => bidderServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Delete_Entity()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Bidder>())); ;

            var bidderServices = new BidderServices(mockRepository.Object);

            Action act = () => bidderServices.Delete(new Bidder());
            act.Should().NotThrow();
        }

        private static IList<Bidder> getBidders()
        {
            var bidders = new List<Bidder>();
            bidders.Add(
                new Bidder
                {
                    Person = new Person(),
                }
            );

            return bidders;
        }

        [TestMethod]
        public void GetAll_NotEmpty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Bidder, bool>>>(),
                                           It.IsAny<Func<IQueryable<Bidder>, IOrderedQueryable<Bidder>>>(),
                                           It.IsAny<string>())).Returns(getBidders());

            var services = new BidderServices(mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetAll_Empty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Bidder, bool>>>(),
                                           It.IsAny<Func<IQueryable<Bidder>, IOrderedQueryable<Bidder>>>(),
                                           It.IsAny<string>())).Returns(new List<Bidder>());

            var services = new BidderServices(mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        [TestMethod]
        public void GetByID_EntityFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Bidder { Id = 0 });
            var services = new BidderServices(mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new BidderServices(mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }
    }
}
