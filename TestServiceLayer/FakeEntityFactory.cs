//-----------------------------------------------------------------------
// <copyright file="FakeEntityFactory.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TestServiceLayer
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Defines the <see cref="FakeEntityFactory" />.
    /// </summary>
    public sealed class FakeEntityFactory
    {
        /// <summary>
        /// Defines the sellerId.
        /// </summary>
        private static int sellerId = 0;

        /// <summary>
        /// Defines the bidderId.
        /// </summary>
        private static int bidderId = 0;

        /// <summary>
        /// Defines the productId.
        /// </summary>
        private static int productId = 0;

        /// <summary>
        /// Defines the categoryId.
        /// </summary>
        private static int categoryId = 0;

        /// <summary>
        /// Defines the userProfileId.
        /// </summary>
        private static int userProfileId = 0;

        /// <summary>
        /// Defines the bidId.
        /// </summary>
        private static int bidId = 0;

        /// <summary>
        /// Defines the personId.
        /// </summary>
        private static int personId = 0;

        /// <summary>
        /// Defines the auctionId.
        /// </summary>
        private static int auctionId = 0;

        /// <summary>
        /// The CreateSeller.
        /// </summary>
        /// <returns>The <see cref="Seller"/>.</returns>
        public static Seller CreateSeller()
        {
            var seller = new Seller
            {
                Id = sellerId,
            };
            return new Seller();
        }

        /// <summary>
        /// The CreateProduct.
        /// </summary>
        /// <returns>The <see cref="Product"/>.</returns>
        public static Product CreateProduct()
        {
            var product = new Product
            {
                Id = productId,
                Name = nameof(Product) + productId,
            };
            productId++;
            return product;
        }

        /// <summary>
        /// The CreateProduct.
        /// </summary>
        /// <param name="categories">The categories<see cref="IList{Category}"/>.</param>
        /// <returns>The <see cref="Product"/>.</returns>
        public static Product CreateProduct(IList<Category> categories)
        {
            var product = FakeEntityFactory.CreateProduct();
            product.Categories.AddRange(categories);
            return product;
        }

        /// <summary>
        /// The CreateCategory.
        /// </summary>
        /// <returns>The <see cref="Category"/>.</returns>
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

        /// <summary>
        /// The CreateUserProfile.
        /// </summary>
        /// <returns>The <see cref="UserProfile"/>.</returns>
        public static UserProfile CreateUserProfile()
        {
            var userProfile = new UserProfile
            {
                Id = userProfileId,
                Username = "dandelionn",
                Password = "Password0&",
                Email = "dandelionn@gmail.com"
            };
            userProfileId++;
            return userProfile;
        }

        /// <summary>
        /// The CreateBidder.
        /// </summary>
        /// <returns>The <see cref="Bidder"/>.</returns>
        public static Bidder CreateBidder()
        {
            var bidder = new Bidder
            {
                Id = bidderId,
            };
            bidderId = 0;
            return bidder;
        }

        /// <summary>
        /// The CreateBid.
        /// </summary>
        /// <returns>The returns a new Bid instance.</returns>
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

        /// <summary>
        /// The CreatePerson.
        /// </summary>
        /// <returns>The <see cref="Person"/>.</returns>
        public static Person CreatePerson()
        {
            var person = new Person
            {
                Id = personId,
                Name = nameof(Person) + nameof(Person.Name) + personId,
                Surname = nameof(Person) + nameof(Person.Surname) + personId,
                Address = nameof(Person) + nameof(Person.Address) + personId,
                PhoneNumber = "0758988360",
                BanEndDate = new DateTime(1900, 1, 1),
            };
            personId++;
            return person;
        }

        /// <summary>
        /// The CreateAuction.
        /// </summary>
        /// <returns>The <see cref="Auction"/>.</returns>
        public static Auction CreateAuction()
        {
            var auction = new DomainModel.Auction
            {
                Id = auctionId,
                Name = nameof(DomainModel.Auction) + auctionId,
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
    }
}
