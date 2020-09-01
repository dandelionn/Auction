//-----------------------------------------------------------------------
// <copyright file="ProductServiceTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="ProductServiceTests" />.
    /// </summary>
    [TestClass]
    public class ProductServiceTests
    {
        /// <summary>
        /// Defines the mockRepository.
        /// </summary>
        private Mock<IProductRepository> mockRepository;

        /// <summary>
        /// The TestInit.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this.mockRepository = new Mock<IProductRepository>();
        }

        /// <summary>
        /// The Delete_Null.
        /// </summary>
        [TestMethod]
        public void Delete_Null()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Product>())).Throws<NullReferenceException>();

            var productServices = new ProductService(this.mockRepository.Object);

            Action act = () => productServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Delete_Entity.
        /// </summary>
        [TestMethod]
        public void Delete_Entity()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Product>()));

            var productServices = new ProductService(this.mockRepository.Object);

            Action act = () => productServices.Delete(new Product());
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
                                           It.IsAny<Expression<Func<Product, bool>>>(),
                                           It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(),
                                           It.IsAny<string>())).Returns(GetProducts());

            var services = new ProductService(this.mockRepository.Object);

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
                                           It.IsAny<Expression<Func<Product, bool>>>(),
                                           It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(),
                                           It.IsAny<string>())).Returns(new List<Product>());

            var services = new ProductService(this.mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        /// <summary>
        /// The GetByID_EntityFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Product { Id = 0 });
            var services = new ProductService(this.mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        /// <summary>
        /// The GetByID_EntityNotFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new ProductService(this.mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        /// <summary>
        /// The Insert_ValidProduct.
        /// </summary>
        [TestMethod]
        public void Insert_ValidProduct()
        {
            this.mockRepository.Setup(x => x.Update(It.IsAny<Product>()));

            var services = new ProductService(this.mockRepository.Object);

            var product = FakeEntityFactory.CreateProduct();
            product.Categories.Add(FakeEntityFactory.CreateCategory());
            product.Sellers.Add(FakeEntityFactory.CreateSeller());

            var results = services.Insert(product);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The Update_ValidProduct.
        /// </summary>
        [TestMethod]
        public void Update_ValidProduct()
        {
            this.mockRepository.Setup(x => x.Update(It.IsAny<Product>()));

            var services = new ProductService(this.mockRepository.Object);

            var product = FakeEntityFactory.CreateProduct();
            product.Categories.Add(FakeEntityFactory.CreateCategory());
            product.Sellers.Add(FakeEntityFactory.CreateSeller());

            var results = services.Update(product);

            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// The GetProducts.
        /// </summary>
        /// <returns>The <see cref="IList{Product}"/>.</returns>
        private static IList<Product> GetProducts()
        {
            var products = new List<Product>();
            products.Add(
                new Product
                {
                    Name = nameof(Product),
                });

            return products;
        }
    }
}
