//-----------------------------------------------------------------------
// <copyright file="DatabaseTests.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TestDatabase
{
    using DataLayer.AccessLayer;
    using DataMapper.SqlServerDAO;
    using DomainModel;
    using DomainModel.Validators;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ServiceLayer.ServiceImplementation;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Validation;
    using System.Linq;
    using TestServiceLayer;

    /// <summary>
    /// Defines the <see cref="DatabaseTests" />.
    /// </summary>
    [TestClass]
    public class DatabaseTests
    {
        /// <summary>
        /// Defines the userProfile.
        /// </summary>
        internal UserProfile userProfile;

        /// <summary>
        /// Defines the person.
        /// </summary>
        internal Person person;

        /// <summary>
        /// Defines the seller.
        /// </summary>
        internal Seller seller;

        /// <summary>
        /// Defines the auction.
        /// </summary>
        internal Auction auction;

        /// <summary>
        /// Defines the bidder.
        /// </summary>
        internal Bidder bidder;

        /// <summary>
        /// Defines the product.
        /// </summary>
        internal Product product;

        /// <summary>
        /// Defines the category.
        /// </summary>
        internal Category category;

        /// <summary>
        /// Defines the userProfileServices.
        /// </summary>
        internal UserProfileService userProfileServices;

        /// <summary>
        /// Defines the personServices.
        /// </summary>
        internal PersonService personServices;

        /// <summary>
        /// Defines the bidderServices.
        /// </summary>
        internal BidderService bidderServices;

        /// <summary>
        /// Defines the sellerServices.
        /// </summary>
        internal SellerService sellerServices;

        /// <summary>
        /// Defines the categoryServices.
        /// </summary>
        internal CategoryService categoryServices;

        /// <summary>
        /// Defines the bidServices.
        /// </summary>
        internal BidService bidServices;

        /// <summary>
        /// Defines the productServices.
        /// </summary>
        internal ProductService productServices;

        /// <summary>
        /// Defines the auctionServices.
        /// </summary>
        internal AuctionService auctionServices;

        /// <summary>
        /// The Initialize.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.userProfile = FakeEntityFactory.CreateUserProfile();
            this.person = FakeEntityFactory.CreatePerson();
            this.seller = FakeEntityFactory.CreateSeller();
            this.auction = FakeEntityFactory.CreateAuction();
            this.bidder = FakeEntityFactory.CreateBidder();
            this.product = FakeEntityFactory.CreateProduct();
            this.category = FakeEntityFactory.CreateCategory();

            this.userProfileServices = new UserProfileService(new UserProfileRepository());
            this.personServices = new PersonService(new PersonRepository());
            this.bidderServices = new BidderService(new BidderRepository());
            this.sellerServices = new SellerService(new SellerRepository());
            this.categoryServices = new CategoryService(new CategoryRepository());
            this.bidServices = new BidService(new BidRepository());
            this.productServices = new ProductService(new ProductRepository());
            this.auctionServices = new AuctionService(new AuctionRepository());

            using (var auctionDBContext = new AuctionDBContext())
            {
                auctionDBContext.Database.Delete();
            }
        }

        /// <summary>
        /// The InsertUserProfileShouldNotFail.
        /// </summary>
        [TestMethod]
        public void InsertUserProfileShouldNotFail()
        {
            var failed = userProfileServices.Insert(userProfile);
            var all = userProfileServices.GetAll();
            var dbUserProfile = userProfileServices.GetByID(userProfile.Id);
            Assert.AreEqual(userProfile, dbUserProfile);
        }

        /// <summary>
        /// The InsertPersonShouldNotFail.
        /// </summary>
        [TestMethod]
        public void InsertPersonShouldNotFail()
        {
            person.Id = userProfile.Id;
            person.UserProfile = userProfile;
            var failed = personServices.Insert(person);
            var dbPerson = personServices.GetByID(person.Id);
            Assert.AreEqual(person, dbPerson);
        }

        /// <summary>
        /// The InsertSellerShouldNotFail.
        /// </summary>
        [TestMethod]
        public void InsertSellerShouldNotFail()
        {
            person.Id = userProfile.Id;
            person.UserProfile = userProfile;
            person.Seller = seller;
            seller.Id = person.Id;
            seller.Person = person;
            var failed = sellerServices.Insert(seller);

            var dbSeller = sellerServices.GetByID(seller.Id);
            Assert.AreEqual(seller, dbSeller);
        }

        /// <summary>
        /// The AddPersonToDb.
        /// </summary>
        private void AddPersonToDb()
        {
            var userProfileServices = new UserProfileService(new UserProfileRepository());
            userProfileServices.Insert(userProfile);
            var personServices = new PersonService(new PersonRepository());
            person.Id = userProfile.Id;
            personServices.Insert(person);
        }

        /// <summary>
        /// The InsertBidderShouldNotFail.
        /// </summary>
        [TestMethod]
        public void InsertBidderShouldNotFail()
        {
            person.Id = userProfile.Id;
            person.UserProfile = userProfile;
            person.Bidder = bidder;
            bidder.Id = person.Id;
            bidder.Person = person;
            var failed = bidderServices.Insert(bidder);

            var dbBidder = bidderServices.GetByID(bidder.Id);
            Assert.AreEqual(bidder, dbBidder);
        }

        /// <summary>
        /// The AddSellerToDb.
        /// </summary>
        private void AddSellerToDb()
        {
            AddPersonToDb();
            var sellerServices = new SellerService(new SellerRepository());
            seller.Id = person.Id;
            sellerServices.Insert(seller);
        }

        /// <summary>
        /// The AddCategoryToDb.
        /// </summary>
        private void AddCategoryToDb()
        {
            var categoryServices = new CategoryService(new CategoryRepository());
            categoryServices.Insert(category);
        }

        /// <summary>
        /// The InsertProductShouldNotFail.
        /// </summary>
        [TestMethod]
        public void InsertProductShouldNotFail()
        {
            person.Id = userProfile.Id;
            person.UserProfile = userProfile;
            person.Seller = seller;
            seller.Id = person.Id;
            seller.Person = person;
            product.Sellers.Add(seller);
            product.Categories.Add(category);

            var failed = productServices.Insert(product);

            var dbProduct = productServices.GetByID(product.Id);
            Assert.AreEqual(product, dbProduct);
        }

        /// <summary>
        /// The InsertAuctionShouldNotFail.
        /// </summary>
        [TestMethod]
        public void InsertAuctionShouldNotFail()
        {
            person.Id = userProfile.Id;
            person.UserProfile = userProfile;
            person.Seller = seller;
            seller.Id = person.Id;
            seller.Person = person;
            product.Sellers.Add(seller);
            product.Categories.Add(category);
            auction.Seller = seller;
            auction.Products.Add(product);

            var failed = auctionServices.Insert(auction);

            var dbAuction = auctionServices.GetByID(auction.Id);
            Assert.AreEqual(auction, dbAuction);
        }

        /// <summary>
        /// The InsertCategoryShouldNotFail.
        /// </summary>
        [TestMethod]
        public void InsertCategoryShouldNotFail()
        {
            var failed = categoryServices.Insert(category);

            var dbCategory = categoryServices.GetByID(category.Id);
            Assert.AreEqual(category, dbCategory);
        }

        /*
        private static IList<DomainModel.DataLayer> GetAuctions()
        {
            var auctions = new List<DomainModel.DataLayer>();
            auctions.Add(
                new DomainModel.DataLayer
                {
                    Name = "Name",
                    Address = "Surname",
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    CurrencyName = "Euro",
                    BeginPrice = 100,
                    CurrentPrice = 100,
                    Active = true
                }
            );

            return auctions;
        }
        */
        /// <summary>
        /// The GetAllAuctionsShouldBeEmpty.
        /// </summary>
        [TestMethod]
        public void GetAllAuctionsShouldBeEmpty()
        {
            auctionServices.GetAll().Should().BeEmpty();
        }

        /// <summary>
        /// The AuctionGetByIDShouldNotBeFound.
        /// </summary>
        [TestMethod]
        public void AuctionGetByIDShouldNotBeFound()
        {
            auctionServices.GetByID(0).Should().BeNull();
        }

        /// <summary>
        /// The CreateStartedAndNotFinalizedAuctions.
        /// </summary>
        /// <param name="n">The n<see cref="int"/>.</param>
        /// <returns>The <see cref="List{Auction}"/>.</returns>
        private static List<Auction> CreateStartedAndNotFinalizedAuctions(int n)
        {
            var auctions = new List<Auction>();
            for (int i = 0; i < n; ++i)
            {
                var auction = CreateActiveAuction();
                auctions.Add(auction);
            }

            return auctions;
        }

        /// <summary>
        /// The SetUpAuction.
        /// </summary>
        private void SetUpAuction()
        {
            person.Id = userProfile.Id;
            person.UserProfile = userProfile;
            person.Seller = seller;
            seller.Id = person.Id;
            seller.Person = person;
            product.Sellers.Add(seller);
            product.Categories.Add(category);
            auction.Seller = seller;
            auction.Products.Add(product);
        }

        /// <summary>
        /// The Insert_TooManyAuctionsStartedAndNotFinalized.
        /// </summary>
        [TestMethod]
        public void Insert_TooManyAuctionsStartedAndNotFinalized()
        {
            SetUpAuction();

            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalized"));
            auction.Seller.Auctions = CreateStartedAndNotFinalizedAuctions(limit + 1);

            var results = auctionServices.Insert(auction);
            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalized, res.ErrorMessage);
        }

        /// <summary>
        /// The CreateFutureAuction.
        /// </summary>
        /// <returns>The <see cref="Auction"/>.</returns>
        private static Auction CreateFutureAuction()
        {
            var userProfile = FakeEntityFactory.CreateUserProfile();
            var person = FakeEntityFactory.CreatePerson();
            person.Id = userProfile.Id;
            person.UserProfile = userProfile;
            var seller = FakeEntityFactory.CreateSeller();
            person.Seller = seller;
            seller.Id = person.Id;
            seller.Person = person;
            var product = FakeEntityFactory.CreateProduct();
            var category = FakeEntityFactory.CreateCategory();
            product.Sellers.Add(seller);
            product.Categories.Add(category);

            var auction = FakeEntityFactory.CreateAuction();
            auction.Seller = seller;
            auction.Products.Add(product);
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now.AddDays(2);
            auction.Active = false;
            return auction;
        }

        /// <summary>
        /// The CreateNotStartedAndNotFinalizedAuctions.
        /// </summary>
        /// <param name="n">The n<see cref="int"/>.</param>
        /// <returns>The <see cref="List{Auction}"/>.</returns>
        private static List<Auction> CreateNotStartedAndNotFinalizedAuctions(int n)
        {
            var auctions = new List<Auction>();
            for (int i = 0; i < n; ++i)
            {
                var auction = CreateFutureAuction();
                auctions.Add(auction);
            }
            return auctions;
        }

        /// <summary>
        /// The Insert_AuctionsNotStartedAndNotFinalized.
        /// </summary>
        [TestMethod]
        public void Insert_AuctionsNotStartedAndNotFinalized()
        {
            SetUpAuction();

            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalized"));
            auction.Seller.Auctions = CreateNotStartedAndNotFinalizedAuctions(limit + 1);

            try
            {
                var results = auctionServices.Insert(auction);
                Assert.AreEqual(0, results.Count);
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.EntityValidationErrors);
            }
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
        /// <returns>The <see cref="IList{Auction}"/>.</returns>
        private static IList<Auction> CreateActiveAuctionsWithOneCategoryInExcess(List<Category> commonCategories)
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<Auction>();
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

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory, res.ErrorMessage);
        }

        /// <summary>
        /// The CreateActiveAuctionsWithNoCategoryInExcess.
        /// </summary>
        /// <returns>The <see cref="IList{Auction}"/>.</returns>
        private static IList<Auction> CreateActiveAuctionsWithNoCategoryInExcess()
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<Auction>();
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

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The CreateActiveAuctionsWithCategoriesInExcess.
        /// </summary>
        /// <param name="commonCategories">The commonCategories<see cref="List{Category}"/>.</param>
        /// <returns>The <see cref="IList{Auction}"/>.</returns>
        private static IList<Auction> CreateActiveAuctionsWithCategoriesInExcess(List<Category> commonCategories)
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<Auction>();
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

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory, res.ErrorMessage);
        }

        /// <summary>
        /// The CreateActiveAuctionsWithMultipleCategoriesNoCategoriesInExcess.
        /// </summary>
        /// <returns>The <see cref="IList{Auction}"/>.</returns>
        private static IList<Auction> CreateActiveAuctionsWithMultipleCategoriesNoCategoriesInExcess()
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<Auction>();
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

            try
            {
                var results = auctionServices.Insert(auction);
                Assert.AreEqual(0, results.Count);
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.EntityValidationErrors);
            }
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

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.SellerIsBanned, res.ErrorMessage);
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

            auctionServices.Insert(auction).Should().BeEmpty();
        }

        /// <summary>
        /// The Update_Null.
        /// </summary>
        [TestMethod]
        public void Update_Null()
        {
            Action act = () => auctionServices.Update(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Update_ChangesInFutureAuctions.
        /// </summary>
        [TestMethod]
        public void Update_ChangesInFutureAuctions()
        {
            SetUpAuction();
            var insertResulsts = auctionServices.Insert(auction);
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now.AddDays(2);
            try
            {
                var results = auctionServices.Update(auction);
                Assert.AreEqual(0, results.Count);
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.EntityValidationErrors);
            }
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

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionPeriodIsTooLarge, res.ErrorMessage);
        }
    }
}
