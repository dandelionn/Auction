using DataMapper;
using DomainModel;
using DomainModel.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.ServiceImplementation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

namespace TestServiceLayer
{
    [TestClass]
    public class AuctionServicesTests
    {
        internal Mock<IAuctionRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<IAuctionRepository>();
        }

        [TestMethod]
        public void Delete_Null()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Auction>())).Throws(new NullReferenceException()); ;

            var auctionServices = new AuctionServices(mockRepository.Object);

            Action act = () => auctionServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Delete_Entity()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Auction>())); ;

            var auctionServices = new AuctionServices(mockRepository.Object);

            Action act = () => auctionServices.Delete(new Auction());
            act.Should().NotThrow();
        }

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
                }
            );

            return auctions;
        }

        [TestMethod]
        public void GetAll_NotEmpty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Auction, bool>>>(),
                                           It.IsAny<Func<System.Linq.IQueryable<Auction>, IOrderedQueryable<Auction>>>(),
                                           It.IsAny<string>())).Returns(GetAuctions());

            var services = new AuctionServices(mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetAll_Empty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Auction, bool>>>(),
                                           It.IsAny<Func<System.Linq.IQueryable<Auction>, IOrderedQueryable<Auction>>>(),
                                           It.IsAny<string>())).Returns(new List<Auction>());

            var services = new AuctionServices(mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        [TestMethod]
        public void GetByID_EntityFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Auction { Id = 0 });
            var services = new AuctionServices(mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new AuctionServices(mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        private static int sellerId = 0;
        public static Seller CreateSeller()
        {
            var seller = new Seller
            {
                Id = sellerId,
            };
            return new Seller();
        }

        private static int productId = 0;
        public static Product CreateProduct()
        {
            var product = new Product
            {
                Id = productId,
                Name = nameof(Product) + productId,
            };
            productId ++;
            return product;
        }

        private static int categoryId = 0;
        public static Category CreateCategory()
        {
            var category = new Category
            {
                Id = categoryId,
                Name = nameof(Category) + categoryId,
            };
            categoryId++;
            return category;
        }

        private static int bidderId = 0;
        public static Bidder CreateBidder()
        {
            var bidder = new Bidder
            {
                Id = bidderId,
            };
            bidderId = 0;
            return bidder;
        }

        private static int bidId = 0;
        public static Bid CreateBid()
        {
            var bid = new Bid
            {
                Id = bidId,
                Value = 120,
            };
            bidId = 0;
            return bid;
        }

        private static int personId = 0;
        public static Person CreatePerson()
        {
            var person = new Person
            {
                Id = personId,
                Name = nameof(Person) + nameof(Person.Name) + personId,
                Surname = nameof(Person) + nameof(Person.Surname) + personId,
                Address = nameof(Person) + nameof(Person.Address) + personId,
                PhoneNumber = "075898860",
                BanEndDate = new DateTime(1900, 1, 1),
            };
            personId++;
            return person;
        }

        private static int auctionId = 0;
        public static Auction CreateAuction()
        {
            var auction = new Auction
            {
                Id = auctionId,
                Name = nameof(Auction) + auctionId,
                Address = "online",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                BeginPrice = 100,
                CurrentPrice = 100,
                CurrencyName = "Euro",
                Active = true
            };
            auctionId++;
            return auction;
        }

        [TestMethod]
        public void Insert_InvalidProperty()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());
            auction.CurrencyName = "MyCoin";
            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameIsNotValid, res.ErrorMessage);
        }

        [TestMethod]
        public void Insert_MissingRequiredProperty()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());
            auction.Name = null;
            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        private static List<Auction> CreateStartedAndNotFinalizedAuctions(int n)
        {
            var auctions = new List<Auction>();
            for (int i=0; i<n; ++i)
            {
                var auction = CreateActiveAuction();
                auctions.Add(auction);
            }

            return auctions;
        }

        [TestMethod]
        public void Insert_TooManyAuctionsStartedAndNotFinalized()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalized"));
            auction.Seller.Auctions = CreateStartedAndNotFinalizedAuctions(limit + 1);

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);
            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalized, res.ErrorMessage);
        }


        private static Auction CreateFutureAuction()
        {
            var auction = CreateAuction();
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now.AddDays(2);
            auction.Active = false;
            return auction;
        }
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

        [TestMethod]
        public void Insert_AuctionsNotStartedAndNotFinalized()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());

            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalized"));
            auction.Seller.Auctions = CreateNotStartedAndNotFinalizedAuctions(limit + 1);
            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        private static Product CreateProduct(IList<Category> categories)
        {
            var product = CreateProduct();
            product.Categories.AddRange(categories);
            return product;
        }

        private static Auction CreateActiveAuction()
        {
            var auction = CreateAuction();
            auction.BeginDate = System.DateTime.Now;
            auction.EndDate = System.DateTime.Now.AddDays(1);
            auction.Active = true;
            return auction;
        }

        private static IList<Auction> CreateActiveAuctionsWithOneCategoryInExcess(List<Category> commonCategories)
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));  
            var activeAuctions = new List<Auction>();
            for (int i = 0; i <= limit; ++i)
            {
                var activeAuction = CreateActiveAuction();
                var product = CreateProduct(commonCategories);
                activeAuction.Products.Add(product);
                activeAuctions.Add(activeAuction);
            }
            return activeAuctions;
        }

        [TestMethod]
        public void Insert_AuctionWithSingleCategory_CategoryInExcess()
        {       
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            var commonCategories = new List<Category> { CreateCategory() };
            auction.Products.Add(CreateProduct(commonCategories));
            auction.Seller.Auctions = CreateActiveAuctionsWithOneCategoryInExcess(commonCategories).ToList();

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);
        
            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory, res.ErrorMessage);
        }

        private static IList<Auction> CreateActiveAuctionsWithNoCategoryInExcess()
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<Auction>();
            for (int i = 0; i < limit; ++i)
            {
                var categories = new List<Category> { CreateCategory() };
                var product = CreateProduct(categories);
                var activeAuction = CreateActiveAuction();
                activeAuction.Products.Add(product);
                activeAuctions.Add(activeAuction);
            }
            return activeAuctions;
        }

        [TestMethod]
        public void Insert_SingleCategory_NoCategoryInExcess()
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());
            auction.Seller.Auctions = CreateActiveAuctionsWithNoCategoryInExcess().ToList();

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        private static IList<Auction> CreateActiveAuctionsWithCategoriesInExcess(List<Category> commonCategories)
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<Auction>();
            for (int i = 0; i <= limit; ++i)
            {
                var withAddedCategories = commonCategories.Concat(new List<Category> { CreateCategory(), CreateCategory() });
                var product = CreateProduct(withAddedCategories.ToList());
                var activeAuction = CreateActiveAuction();
                activeAuction.Products.Add(product);
                activeAuctions.Add(activeAuction);
            }
            return activeAuctions;
        }

        [TestMethod]
        public void Insert_AuctionWithMultipleCategories_OneCategoryInExcess()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            var commonCategories = new List<Category> { CreateCategory() };
            auction.Products.Add(CreateProduct(commonCategories));
            auction.Products.Add(CreateProduct(new List<Category>()));
            auction.Seller.Auctions = CreateActiveAuctionsWithCategoriesInExcess(commonCategories).ToList();
          
            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory, res.ErrorMessage);
        }

        [TestMethod]
        public void Insert_AuctionWithMultipleCategories_BothCategoriesInExcess()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            var commonCategories = new List<Category> { CreateCategory(), CreateCategory()};
            auction.Products.Add(CreateProduct(commonCategories));
            auction.Seller.Auctions = CreateActiveAuctionsWithCategoriesInExcess(commonCategories).ToList();

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.TooManyAuctionsStartedAndNotFinalizedForCategory, res.ErrorMessage);
        }

        private static IList<Auction> CreateActiveAuctionsWithMultipleCategoriesNoCategoriesInExcess()
        {
            var limit = int.Parse(ConfigurationManager.AppSettings.Get("MaxAuctionsStartedAndNotFinalizedForCategory"));
            var activeAuctions = new List<Auction>();
            for (int i = 0; i < limit; ++i)
            {
                var categories = new List<Category> { CreateCategory(), CreateCategory() };
                var product = CreateProduct(categories);
                var activeAuction = CreateActiveAuction();
                activeAuction.Products.Add(product);
                activeAuctions.Add(activeAuction);
            }
            return activeAuctions;
        }

        [TestMethod]
        public void Insert_AuctionWithMultipleCategories_NoCategoryInExcess()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            var categories = new List<Category> { CreateCategory(), CreateCategory()};
            auction.Products.Add(CreateProduct(categories));
            auction.Seller.Auctions = CreateActiveAuctionsWithMultipleCategoriesNoCategoriesInExcess().ToList();

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Insert_SellerBanned()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());
            auction.Seller.Person.BanEndDate = DateTime.Now.Date.AddDays(1);

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.SellerIsBanned, res.ErrorMessage);
        }

        [TestMethod]
        public void Insert_Null()
        {
            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>())).Throws<NullReferenceException>();

            var auctionServices = new AuctionServices(mockRepository.Object);

            Action act = () => auctionServices.Insert(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Insert_ValidAuction()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));

            var auctionServices = new AuctionServices(mockRepository.Object);

            auctionServices.Insert(auction).Should().BeEmpty();
        }

        [TestMethod]
        public void Update_Null()
        {
            mockRepository.Setup(x => x.Update(It.IsAny<Auction>()));

            var auctionServices = new AuctionServices(mockRepository.Object);

            Action act = () => auctionServices.Update(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Update_InvalidProperty()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Products.Add(CreateProduct());
            auction.CurrencyName = "MyCoin";
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            mockRepository.Setup(x => x.Update(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.CurrencyNameIsNotValid, res.ErrorMessage);
        }

        [TestMethod]
        public void Update_MissingRequiredProperty()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Products.Add(CreateProduct());
            auction.Name = null;
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            mockRepository.Setup(x => x.Update(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.NameRequired, res.ErrorMessage);
        }

        [TestMethod]
        public void Update_ChangesInFutureAuctions()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Products.Add(CreateProduct());
            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now.AddDays(2);

            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            mockRepository.Setup(x => x.Update(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Update_ChangesInExpiredAuctions()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());
            auction.BeginDate = DateTime.Now.AddDays(-1);
            auction.EndDate = DateTime.Now;

            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            mockRepository.Setup(x => x.Update(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.ChangesNotAllowedInExpiredAuctions, res.ErrorMessage);
        }

        [TestMethod]
        public void Update_ChangesInActiveAuctions()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Products.Add(CreateProduct());

            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(auction);
            mockRepository.Setup(x => x.Update(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Update(auction);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Insert_BeginDateAfterEndDate()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());

            auction.BeginDate = DateTime.Now.AddDays(1);
            auction.EndDate = DateTime.Now;

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateIsAfterEndDate, res.ErrorMessage);
        }

        [TestMethod]
        public void Insert_BeginDateInThePast()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());

            auction.BeginDate = DateTime.Now.AddDays(-1);
            auction.EndDate = DateTime.Now;

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.BeginDateShouldNotBeInThePast, res.ErrorMessage);
        }

        [TestMethod]
        public void Insert_ValidAuctionPeriod()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());

            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddDays(1);


            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Insert_AuctionPeriodTooLarge()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(auctionMaxPeriodInMonths + 1);

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionPeriodIsTooLarge, res.ErrorMessage);
        }

        [TestMethod]
        public void Insert_LastValidEndDate()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(auctionMaxPeriodInMonths).AddDays(-1);

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Insert_FirstInvalidEndDate()
        {
            var auction = CreateAuction();
            auction.Seller = CreateSeller();
            auction.Seller.Person = CreatePerson();
            auction.Products.Add(CreateProduct());
            var auctionMaxPeriodInMonths = int.Parse(ConfigurationManager.AppSettings.Get("AuctionMaxPeriodInMonths"));
            auction.BeginDate = DateTime.Now;
            auction.EndDate = DateTime.Now.AddMonths(auctionMaxPeriodInMonths);

            mockRepository.Setup(x => x.Insert(It.IsAny<Auction>()));
            var auctionServices = new AuctionServices(mockRepository.Object);

            var results = auctionServices.Insert(auction);

            Assert.AreEqual(1, results.Count);
            var res = results[0];
            Assert.AreEqual(ErrorMessages.AuctionPeriodIsTooLarge, res.ErrorMessage);
        }
    }
}
