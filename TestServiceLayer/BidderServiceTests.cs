//-----------------------------------------------------------------------
// <copyright file="BidderServiceTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="BidderServiceTests" />.
    /// </summary>
    [TestClass]
    public class BidderServiceTests
    {
        /// <summary>
        /// Defines the mockRepository.
        /// </summary>
        private Mock<IBidderRepository> mockRepository;

        /// <summary>
        /// The TestInit.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this.mockRepository = new Mock<IBidderRepository>();
        }

        /// <summary>
        /// The Delete_Null.
        /// </summary>
        [TestMethod]
        public void Delete_Null()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Bidder>())).Throws<NullReferenceException>();

            var bidderServices = new BidderService(this.mockRepository.Object);

            Action act = () => bidderServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Delete_Entity.
        /// </summary>
        [TestMethod]
        public void Delete_Entity()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Bidder>()));

            var bidderServices = new BidderService(this.mockRepository.Object);

            Action act = () => bidderServices.Delete(new Bidder());
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
                                           It.IsAny<Expression<Func<Bidder, bool>>>(),
                                           It.IsAny<Func<IQueryable<Bidder>, IOrderedQueryable<Bidder>>>(),
                                           It.IsAny<string>())).Returns(GetBidders());

            var services = new BidderService(this.mockRepository.Object);

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
                                           It.IsAny<Expression<Func<Bidder, bool>>>(),
                                           It.IsAny<Func<IQueryable<Bidder>, IOrderedQueryable<Bidder>>>(),
                                           It.IsAny<string>())).Returns(new List<Bidder>());

            var services = new BidderService(this.mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        /// <summary>
        /// The GetByID_EntityFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Bidder { Id = 0 });
            var services = new BidderService(this.mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        /// <summary>
        /// The GetByID_EntityNotFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new BidderService(this.mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        /// <summary>
        /// The Insert_Null.
        /// </summary>
        [TestMethod]
        public void Insert_Null()
        {
            this.mockRepository.Setup(x => x.Insert(It.IsAny<Bidder>())).Throws<ArgumentNullException>();

            var bidderServices = new BidderService(this.mockRepository.Object);

            Action act = () => bidderServices.Insert(null);
            act.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// The Insert_ValidBidder.
        /// </summary>
        [TestMethod]
        public void Insert_ValidBidder()
        {
            this.mockRepository.Setup(x => x.Update(It.IsAny<Bidder>()));

            var services = new BidderService(this.mockRepository.Object);

            var bidder = FakeEntityFactory.CreateBidder();
            bidder.Person = FakeEntityFactory.CreatePerson();

            var results = services.Insert(bidder);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Update_ValidBidder.
        /// </summary>
        [TestMethod]
        public void Update_ValidBidder()
        {
            this.mockRepository.Setup(x => x.Update(It.IsAny<Bidder>()));

            var services = new BidderService(this.mockRepository.Object);

            var bidder = FakeEntityFactory.CreateBidder();
            bidder.Person = FakeEntityFactory.CreatePerson();

            var results = services.Update(bidder);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The getBidders.
        /// </summary>
        /// <returns>The <see cref="IList{Bidder}"/>.</returns>
        private static IList<Bidder> GetBidders()
        {
            var bidders = new List<Bidder>();
            bidders.Add(
                new Bidder
                {
                    Person = new Person(),
                });

            return bidders;
        }
    }
}
