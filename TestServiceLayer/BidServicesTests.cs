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
    public class BidServicesTests
    {
        internal Mock<IBidRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<IBidRepository>();
        }

        [TestMethod]
        public void Delete_Null()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Bid>())).Throws<NullReferenceException>();

            var bidServices = new BidServices(mockRepository.Object);

            Action act = () => bidServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Delete_Entity()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Bid>())); ;

            var bidServices = new BidServices(mockRepository.Object);

            Action act = () => bidServices.Delete(new Bid());
            act.Should().NotThrow();
        }

        private static IList<Bid> getBids()
        {
            var bids = new List<Bid>();
            bids.Add(
                new Bid
                {
                    Value = 120,
                }
            );

            return bids;
        }

        [TestMethod]
        public void GetAll_NotEmpty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Bid, bool>>>(),
                                           It.IsAny<Func<IQueryable<Bid>, IOrderedQueryable<Bid>>>(),
                                           It.IsAny<string>())).Returns(getBids());

            var services = new BidServices(mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetAll_Empty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Bid, bool>>>(),
                                           It.IsAny<Func<IQueryable<Bid>, IOrderedQueryable<Bid>>>(),
                                           It.IsAny<string>())).Returns(new List<Bid>());

            var services = new BidServices(mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        [TestMethod]
        public void GetByID_EntityFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Bid { Id = 0 });
            var services = new BidServices(mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new BidServices(mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        [TestMethod]
        public void Insert_InvalidBid()
        {
            mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidServices(mockRepository.Object);

            bidServices.Insert(new Bid()).Should().NotBeEmpty();
        }

        [TestMethod]
        public void Insert_ValidBid()
        {
            var bid = new Bid
            {
                Bidder = new Bidder(),
                Auction = new Auction(),
                Value = 200
            };
            mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidServices(mockRepository.Object);

            bidServices.Insert(bid).Should().BeEmpty();
        }

        [TestMethod]
        public void Insert_BidWithTooLowValue()
        {
            var auction = new Auction
            {
                CurrentPrice = 200
            };
            var bid = new Bid
            {
                Bidder = new Bidder(),
                Auction = auction,
                Value = 200
            };

            mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidServices(mockRepository.Object);

            bidServices.Insert(bid).Should().NotBeEmpty();
        }

        [TestMethod]
        public void Insert_BidWithTooHighValue()
        {
            var auction = new Auction
            {
                CurrentPrice = 200
            };
            var bid = new Bid
            {
                Bidder = new Bidder(),
                Auction = auction,
                Value = 300
            };

            mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidServices(mockRepository.Object);

            bidServices.Insert(bid).Should().NotBeEmpty();
        }

        [TestMethod]
        public void Insert_BidderOwnsThePreviousBid()
        {
            var bidder = new Bidder();
            var prevBid = new Bid
            {
                Bidder = bidder
            };
            var auction = new Auction
            {
                CurrentPrice = 200,
                Bids = {prevBid}
            };

            var bid = new Bid
            {
                Bidder = bidder,
                Auction = auction,
                Value = 210
            };

            mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidServices(mockRepository.Object);

            bidServices.Insert(bid).Should().NotBeEmpty();
        }
    }
}
