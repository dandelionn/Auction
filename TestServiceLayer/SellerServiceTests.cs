//-----------------------------------------------------------------------
// <copyright file="SellerServiceTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="SellerServiceTests" />.
    /// </summary>
    [TestClass]
    public class SellerServiceTests
    {
        /// <summary>
        /// Defines the mockRepository.
        /// </summary>
        private Mock<ISellerRepository> mockRepository;

        /// <summary>
        /// The TestInit.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this.mockRepository = new Mock<ISellerRepository>();
        }

        /// <summary>
        /// The Delete_Null.
        /// </summary>
        [TestMethod]
        public void Delete_Null()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Seller>())).Throws<NullReferenceException>();

            var sellerServices = new SellerService(this.mockRepository.Object);

            Action act = () => sellerServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Delete_Entity.
        /// </summary>
        [TestMethod]
        public void Delete_Entity()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Seller>()));

            var sellerServices = new SellerService(this.mockRepository.Object);

            Action act = () => sellerServices.Delete(new Seller());
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
                                           It.IsAny<Expression<Func<Seller, bool>>>(),
                                           It.IsAny<Func<IQueryable<Seller>, IOrderedQueryable<Seller>>>(),
                                           It.IsAny<string>())).Returns(GetSellers());

            var services = new SellerService(this.mockRepository.Object);

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
                                           It.IsAny<Expression<Func<Seller, bool>>>(),
                                           It.IsAny<Func<System.Linq.IQueryable<Seller>, IOrderedQueryable<Seller>>>(),
                                           It.IsAny<string>())).Returns(new List<Seller>());

            var services = new SellerService(this.mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        /// <summary>
        /// The GetByID_EntityFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Seller { Id = 0 });
            var services = new SellerService(this.mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        /// <summary>
        /// The GetByID_EntityNotFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new SellerService(this.mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        /// <summary>
        /// The Insert_ValidSeller.
        /// </summary>
        [TestMethod]
        public void Insert_ValidSeller()
        {
            this.mockRepository.Setup(x => x.Update(It.IsAny<Seller>()));

            var services = new SellerService(this.mockRepository.Object);

            var seller = FakeEntityFactory.CreateSeller();
            seller.Person = FakeEntityFactory.CreatePerson();

            var results = services.Insert(seller);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Update_ValidSeller.
        /// </summary>
        [TestMethod]
        public void Update_ValidSeller()
        {
            this.mockRepository.Setup(x => x.Update(It.IsAny<Seller>()));

            var services = new SellerService(this.mockRepository.Object);

            var seller = FakeEntityFactory.CreateSeller();
            seller.Person = FakeEntityFactory.CreatePerson();

            var results = services.Update(seller);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The GetSellers.
        /// </summary>
        /// <returns>The <see cref="IList{Seller}"/>.</returns>
        private static IList<Seller> GetSellers()
        {
            var sellers = new List<Seller>();
            sellers.Add(
                new Seller
                {
                    Person = new Person(),
                });

            return sellers;
        }
    }
}
