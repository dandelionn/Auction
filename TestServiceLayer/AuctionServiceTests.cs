//-----------------------------------------------------------------------
// <copyright file="AuctionServiceTests.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TestServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;
    using DataMapper;
    using DomainModel;
    using DomainModel.Validators;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ServiceLayer.ServiceImplementation;

    /// <summary>
    /// Defines the <see cref="AuctionServiceTests" />.
    /// </summary>
    [TestClass]
    public class AuctionServiceTests
    {
        /// <summary>
        /// Defines the mockRepository.
        /// </summary>
        private Mock<IAuctionRepository> mockRepository;

        /// <summary>
        /// The TestInit.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this.mockRepository = new Mock<IAuctionRepository>();
        }

        /// <summary>
        /// The Delete_Null.
        /// </summary>
        [TestMethod]
        public void Delete_Null()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<DomainModel.Auction>())).Throws(new NullReferenceException());

            var auctionServices = new AuctionService(this.mockRepository.Object);

            Action act = () => auctionServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Delete_Entity.
        /// </summary>
        [TestMethod]
        public void Delete_Entity()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Auction>()));

            var auctionServices = new AuctionService(this.mockRepository.Object);

            Action act = () => auctionServices.Delete(new Auction());
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
                                           It.IsAny<Expression<Func<DomainModel.Auction, bool>>>(),
                                           It.IsAny<Func<IQueryable<DomainModel.Auction>, IOrderedQueryable<DomainModel.Auction>>>(),
                                           It.IsAny<string>())).Returns(GetAuctions());

            var services = new AuctionService(this.mockRepository.Object);

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
                                    It.IsAny<Expression<Func<DomainModel.Auction, bool>>>(),
                                    It.IsAny<Func<IQueryable<DomainModel.Auction>, IOrderedQueryable<DomainModel.Auction>>>(),
                                    It.IsAny<string>())).Returns(new List<DomainModel.Auction>());

            var services = new AuctionService(this.mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        /// <summary>
        /// The GetByID_EntityFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new DomainModel.Auction { Id = 0 });
            var services = new AuctionService(this.mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        /// <summary>
        /// The GetByID_EntityNotFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new AuctionService(this.mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        /// <summary>
        /// The Insert_InvalidProperty.
        /// </summary>
        [TestMethod]
        public void Insert_InvalidProperty()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            auction.CurrencyName = "MyCoin";
            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameIsNotValid, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_MissingRequiredProperty.
        /// </summary>
        [TestMethod]
        public void Insert_MissingRequiredProperty()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            auction.Name = null;
            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_TooManyAuctionsStartedAndNotFinalized.
        /// </summary>
        [TestMethod]
        public void Insert_TooManyAuctionsStartedAndNotFinalized()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalized"));
            auction.Seller.Auctions = CreateStartedAndNotFinalizedAuctions(limit + 1);

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);
            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalized, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_AuctionsNotStartedAndNotFinalized.
        /// </summary>
        [TestMethod]
        public void Insert_AuctionsNotStartedAndNotFinalized()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());

            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalized"));
            auction.Seller.Auctions = CreateNotStartedAndNotFinalizedAuctions(limit + 1);
            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Insert_AuctionWithSingleCategory_CategoryInExcess.
        /// </summary>
        [TestMethod]
        public void Insert_AuctionWithSingleCategory_CategoryInExcess()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            var commonCategories = new List<Category> { FakeEntityFactory.CreateCategory() };
            auction.Products.Add(FakeEntityFactory.CreateProduct(commonCategories));
            auction.Seller.Auctions = CreateActiveAuctionsWithOneCategoryInExcess(commonCategories).ToList();

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_SingleCategory_NoCategoryInExcess.
        /// </summary>
        [TestMethod]
        public void Insert_SingleCategory_NoCategoryInExcess()
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            auction.Seller.Auctions = CreateActiveAuctionsWithNoCategoryInExcess().ToList();

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Insert_AuctionWithMultipleCategories_OneCategoryInExcess.
        /// </summary>
        [TestMethod]
        public void Insert_AuctionWithMultipleCategories_OneCategoryInExcess()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            var commonCategories = new List<Category> { FakeEntityFactory.CreateCategory() };
            auction.Products.Add(FakeEntityFactory.CreateProduct(commonCategories));
            auction.Products.Add(FakeEntityFactory.CreateProduct(new List<Category>()));
            auction.Seller.Auctions = CreateActiveAuctionsWithCategoriesInExcess(commonCategories).ToList();

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_AuctionWithMultipleCategories_BothCategoriesInExcess.
        /// </summary>
        [TestMethod]
        public void Insert_AuctionWithMultipleCategories_BothCategoriesInExcess()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            var commonCategories = new List<Category> { FakeEntityFactory.CreateCategory(), FakeEntityFactory.CreateCategory() };
            auction.Products.Add(FakeEntityFactory.CreateProduct(commonCategories));
            auction.Seller.Auctions = CreateActiveAuctionsWithCategoriesInExcess(commonCategories).ToList();

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_AuctionWithMultipleCategories_NoCategoryInExcess.
        /// </summary>
        [TestMethod]
        public void Insert_AuctionWithMultipleCategories_NoCategoryInExcess()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            var categories = new List<Category> { FakeEntityFactory.CreateCategory(), FakeEntityFactory.CreateCategory() };
            auction.Products.Add(FakeEntityFactory.CreateProduct(categories));
            auction.Seller.Auctions = CreateActiveAuctionsWithMultipleCategoriesNoCategoriesInExcess().ToList();

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Insert_SellerBanned.
        /// </summary>
        [TestMethod]
        public void Insert_SellerBanned()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            auction.Seller.Person.BanEndDate = DateTime.Now.Date.AddDays(1);

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.SellerIsBanned, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_Null.
        /// </summary>
        [TestMethod]
        public void Insert_Null()
        {
            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>())).Throws<NullReferenceException>();

            var auctionServices = new AuctionService(this.mockRepository.Object);

            Action act = () => auctionServices.Insert(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Insert_ValidAuction.
        /// </summary>
        [TestMethod]
        public void Insert_ValidAuction()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));

            var auctionServices = new AuctionService(this.mockRepository.Object);

            auctionServices.Insert(auction).Should().BeEmpty();
        }

        /// <summary>
        /// The Update_Null.
        /// </summary>
        [TestMethod]
        public void Update_Null()
        {
            this.mockRepository.Setup(x => x.Update(It.IsAny<DomainModel.Auction>()));

            var auctionServices = new AuctionService(this.mockRepository.Object);

            Action act = () => auctionServices.Update(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Update_InvalidProperty.
        /// </summary>
        [TestMethod]
        public void Update_InvalidProperty()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            auction.CurrencyName = "MyCoin";
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            this.mockRepository.Setup(x => x.Update(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameIsNotValid, res.ErrorMessage);
        }

        /// <summary>
        /// The Update_MissingRequiredProperty.
        /// </summary>
        [TestMethod]
        public void Update_MissingRequiredProperty()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            auction.Name = null;
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            this.mockRepository.Setup(x => x.Update(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        /// <summary>
        /// The Update_ChangesInFutureAuctions.
        /// </summary>
        [TestMethod]
        public void Update_ChangesInFutureAuctions()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now.AddDays(2);

            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            this.mockRepository.Setup(x => x.Update(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Update_ChangesInExpiredAuctions.
        /// </summary>
        [TestMethod]
        public void Update_ChangesInExpiredAuctions()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            auction.BeginDate = DateTime.Now.AddDays(-1);
            auction.EndDate = DateTime.Now;

            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            this.mockRepository.Setup(x => x.Update(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ChangesNotAllowedInExpiredAuctions, res.ErrorMessage);
        }

        /// <summary>
        /// The Update_ChangesInActiveAuctions.
        /// </summary>
        [TestMethod]
        public void Update_ChangesInActiveAuctions()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Products.Add(FakeEntityFactory.CreateProduct());

            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            this.mockRepository.Setup(x => x.Update(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Insert_BeginDateAfterEndDate.
        /// </summary>
        [TestMethod]
        public void Insert_BeginDateAfterEndDate()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());

            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now;

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateIsAfterEndDate, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_BeginDateInThePast.
        /// </summary>
        [TestMethod]
        public void Insert_BeginDateInThePast()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());

            auction.BeginDate = DateTime.Now.AddDays(-1);
            auction.EndDate = DateTime.Now;

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateShouldNotBeInThePast, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_ValidAuctionPeriod.
        /// </summary>
        [TestMethod]
        public void Insert_ValidAuctionPeriod()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());

            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddDays(1);

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Insert_AuctionPeriodTooLarge.
        /// </summary>
        [TestMethod]
        public void Insert_AuctionPeriodTooLarge()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(auctionMaxPeriodInMonths + 1);

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionPeriodIsTooLarge, res.ErrorMessage);
        }

        /// <summary>
        /// The Insert_LastValidEndDate.
        /// </summary>
        [TestMethod]
        public void Insert_LastValidEndDate()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(auctionMaxPeriodInMonths).AddDays(-1);

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Insert_FirstInvalidEndDate.
        /// </summary>
        [TestMethod]
        public void Insert_FirstInvalidEndDate()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = FakeEntityFactory.CreateSeller();
            auction.Seller.Person = FakeEntityFactory.CreatePerson();
            auction.Products.Add(FakeEntityFactory.CreateProduct());
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(auctionMaxPeriodInMonths);

            this.mockRepository.Setup(x => x.Insert(It.IsAny<DomainModel.Auction>()));
            var auctionServices = new AuctionService(this.mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionPeriodIsTooLarge, res.ErrorMessage);
        }

        /// <summary>
        /// The CreateStartedAndNotFinalizedAuctions.
        /// </summary>
        /// <param name="n">The n<see cref="int"/>.</param>
        /// <returns>The <see cref="List{DomainModel.Auction}"/>.</returns>
        private static List<DomainModel.Auction> CreateStartedAndNotFinalizedAuctions(int n)
        {
            var auctions = new List<DomainModel.Auction>();
            for (int i = 0; i < n; ++i)
            {
                var auction = CreateActiveAuction();
                auctions.Add(auction);
            }

            return auctions;
        }

        /// <summary>
        /// The CreateFutureAuction.
        /// </summary>
        /// <returns>The <see cref="Auction"/>.</returns>
        private static Auction CreateFutureAuction()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now.AddDays(2);
            auction.Active = false;
            return auction;
        }

        /// <summary>
        /// The CreateNotStartedAndNotFinalizedAuctions.
        /// </summary>
        /// <param name="n">The n<see cref="int"/>.</param>
        /// <returns>The <see cref="List{DomainModel.Auction}"/>.</returns>
        private static List<DomainModel.Auction> CreateNotStartedAndNotFinalizedAuctions(int n)
        {
            var auctions = new List<DomainModel.Auction>();
            for (int i = 0; i < n; ++i)
            {
                var auction = CreateFutureAuction();
                auctions.Add(auction);
            }

            return auctions;
        }

        /// <summary>
        /// The GetAuctions.
        /// </summary>
        /// <returns>The <see cref="IList{Auction}"/>.</returns>
        private static IList<Auction> GetAuctions()
        {
            var auctions = new List<Auction>();
            auctions.Add(
                new Auction
                {
                    Name = "Name",
                    Address = "Surname",
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    CurrencyName = "Euro",
                    BeginPrice = 100,
                    CurrentPrice = 100,
                    Active = true
                });

            return auctions;
        }

        /// <summary>
        /// The CreateActiveAuction.
        /// </summary>
        /// <returns>The <see cref="Auction"/>.</returns>
        private static Auction CreateActiveAuction()
        {
            var auction = FakeEntityFactory.CreateAuction();
            auction.BeginDate = System.DateTime.Now;
            auction.EndDate = System.DateTime.Now.AddDays(1);
            auction.Active = true;
            return auction;
        }

        /// <summary>
        /// The CreateActiveAuctionsWithOneCategoryInExcess.
        /// </summary>
        /// <param name="commonCategories">The commonCategories<see cref="List{Category}"/>.</param>
        /// <returns>The <see cref="IList{DomainModel.Auction}"/>.</returns>
        private static IList<DomainModel.Auction> CreateActiveAuctionsWithOneCategoryInExcess(List<Category> commonCategories)
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<DomainModel.Auction>();
            for (int i = 0; i <= limit; ++i)
            {
                var activeAuction = CreateActiveAuction();
                var product = FakeEntityFactory.CreateProduct(commonCategories);
                activeAuction.Products.Add(product);
                activeAuctions.Add(activeAuction);
            }

            return activeAuctions;
        }

        /// <summary>
        /// The CreateActiveAuctionsWithCategoriesInExcess.
        /// </summary>
        /// <param name="commonCategories">The commonCategories<see cref="List{Category}"/>.</param>
        /// <returns>The <see cref="IList{DomainModel.Auction}"/>.</returns>
        private static IList<DomainModel.Auction> CreateActiveAuctionsWithCategoriesInExcess(List<Category> commonCategories)
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<DomainModel.Auction>();
            for (int i = 0; i <= limit; ++i)
            {
                var withAddedCategories = commonCategories.Concat(new List<Category> { FakeEntityFactory.CreateCategory(), FakeEntityFactory.CreateCategory() });
                var product = FakeEntityFactory.CreateProduct(withAddedCategories.ToList());
                var activeAuction = CreateActiveAuction();
                activeAuction.Products.Add(product);
                activeAuctions.Add(activeAuction);
            }

            return activeAuctions;
        }

        /// <summary>
        /// The CreateActiveAuctionsWithMultipleCategoriesNoCategoriesInExcess.
        /// </summary>
        /// <returns>The <see cref="IList{DomainModel.Auction}"/>.</returns>
        private static IList<DomainModel.Auction> CreateActiveAuctionsWithMultipleCategoriesNoCategoriesInExcess()
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<DomainModel.Auction>();
            for (int i = 0; i < limit; ++i)
            {
                var categories = new List<Category> { FakeEntityFactory.CreateCategory(), FakeEntityFactory.CreateCategory() };
                var product = FakeEntityFactory.CreateProduct(categories);
                var activeAuction = CreateActiveAuction();
                activeAuction.Products.Add(product);
                activeAuctions.Add(activeAuction);
            }

            return activeAuctions;
        }

        /// <summary>
        /// The CreateActiveAuctionsWithNoCategoryInExcess.
        /// </summary>
        /// <returns>The <see cref="IList{DomainModel.Auction}"/>.</returns>
        private static IList<DomainModel.Auction> CreateActiveAuctionsWithNoCategoryInExcess()
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<DomainModel.Auction>();
            for (int i = 0; i < limit; ++i)
            {
                var categories = new List<Category> { FakeEntityFactory.CreateCategory() };
                var product = FakeEntityFactory.CreateProduct(categories);
                var activeAuction = CreateActiveAuction();
                activeAuction.Products.Add(product);
                activeAuctions.Add(activeAuction);
            }

            return activeAuctions;
        }
    }
}
