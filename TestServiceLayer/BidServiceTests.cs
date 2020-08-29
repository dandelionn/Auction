//-----------------------------------------------------------------------
// <copyright file="BidServiceTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="BidServiceTests" />.
    /// </summary>
    [TestClass]
    public class BidServiceTests
    {
        /// <summary>
        /// Defines the mockRepository.
        /// </summary>
        private Mock<IBidRepository> mockRepository;

        /// <summary>
        /// The TestInit.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this.mockRepository = new Mock<IBidRepository>();
        }

        /// <summary>
        /// The Delete_Null.
        /// </summary>
        [TestMethod]
        public void Delete_Null()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Bid>())).Throws<NullReferenceException>();

            var bidServices = new BidService(this.mockRepository.Object);

            Action act = () => bidServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Delete_Entity.
        /// </summary>
        [TestMethod]
        public void Delete_Entity()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Bid>()));

            var bidServices = new BidService(this.mockRepository.Object);

            Action act = () => bidServices.Delete(new Bid());
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
                                           It.IsAny<Expression<Func<Bid, bool>>>(),
                                           It.IsAny<Func<IQueryable<Bid>, IOrderedQueryable<Bid>>>(),
                                           It.IsAny<string>())).Returns(GetBids());

            var services = new BidService(this.mockRepository.Object);

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
                                           It.IsAny<Expression<Func<Bid, bool>>>(),
                                           It.IsAny<Func<IQueryable<Bid>, IOrderedQueryable<Bid>>>(),
                                           It.IsAny<string>())).Returns(new List<Bid>());

            var services = new BidService(this.mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        /// <summary>
        /// The GetByID_EntityFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Bid { Id = 0 });
            var services = new BidService(this.mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        /// <summary>
        /// The GetByID_EntityNotFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new BidService(this.mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        /// <summary>
        /// The Insert_InvalidBid.
        /// </summary>
        [TestMethod]
        public void Insert_InvalidBid()
        {
            this.mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidService(this.mockRepository.Object);

            bidServices.Insert(new Bid()).Should().NotBeEmpty();
        }

        /// <summary>
        /// The Insert_ValidBid.
        /// </summary>
        [TestMethod]
        public void Insert_ValidBid()
        {
            var bid = new Bid
            {
                Bidder = new Bidder(),
                Auction = new DomainModel.Auction(),
                Value = 200
            };
            this.mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidService(this.mockRepository.Object);

            bidServices.Insert(bid).Should().BeEmpty();
        }

        /// <summary>
        /// The Insert_BidWithTooLowValue.
        /// </summary>
        [TestMethod]
        public void Insert_BidWithTooLowValue()
        {
            var auction = new DomainModel.Auction
            {
                CurrentPrice = 200
            };
            var bid = new Bid
            {
                Bidder = new Bidder(),
                Auction = auction,
                Value = 200
            };

            this.mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidService(this.mockRepository.Object);

            bidServices.Insert(bid).Should().NotBeEmpty();
        }

        /// <summary>
        /// The Insert_BidWithTooHighValue.
        /// </summary>
        [TestMethod]
        public void Insert_BidWithTooHighValue()
        {
            var auction = new DomainModel.Auction
            {
                CurrentPrice = 200
            };
            var bid = new Bid
            {
                Bidder = new Bidder(),
                Auction = auction,
                Value = 300
            };

            this.mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidService(this.mockRepository.Object);

            bidServices.Insert(bid).Should().NotBeEmpty();
        }

        /// <summary>
        /// The Insert_BidderOwnsThePreviousBid.
        /// </summary>
        [TestMethod]
        public void Insert_BidderOwnsThePreviousBid()
        {
            var bidder = new Bidder();
            var prevBid = new Bid
            {
                Bidder = bidder
            };
            var auction = new DomainModel.Auction
            {
                CurrentPrice = 200,
                Bids = { prevBid }
            };

            var bid = new Bid
            {
                Bidder = bidder,
                Auction = auction,
                Value = 210
            };

            this.mockRepository.Setup(x => x.Insert(It.IsAny<Bid>()));

            var bidServices = new BidService(this.mockRepository.Object);

            bidServices.Insert(bid).Should().NotBeEmpty();
        }

        /// <summary>
        /// The getBids.
        /// </summary>
        /// <returns>The <see cref="IList{Bid}"/>.</returns>
        private static IList<Bid> GetBids()
        {
            var bids = new List<Bid>();
            bids.Add(
                new Bid
                {
                    Value = 120,
                });

            return bids;
        }
    }
}
