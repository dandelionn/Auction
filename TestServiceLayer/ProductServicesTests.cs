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
    public class ProductServicesTests
    {
        internal Mock<IProductRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<IProductRepository>();
        }

        [TestMethod]
        public void Delete_Null()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Product>())).Throws<NullReferenceException>();

            var productServices = new ProductServices(mockRepository.Object);

            Action act = () => productServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Delete_Entity()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Product>())); ;

            var productServices = new ProductServices(mockRepository.Object);

            Action act = () => productServices.Delete(new Product());
            act.Should().NotThrow();
        }

        private static IList<Product> GetProducts()
        {
            var products = new List<Product>();
            products.Add(
                new Product
                {
                    Name = nameof(Product),
                }
            );

            return products;
        }

        [TestMethod]
        public void GetAll_NotEmpty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(),
                                           It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(),
                                           It.IsAny<string>())).Returns(GetProducts());

            var services = new ProductServices(mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetAll_Empty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>(),
                                           It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(),
                                           It.IsAny<string>())).Returns(new List<Product>());

            var services = new ProductServices(mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        [TestMethod]
        public void GetByID_EntityFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Product { Id = 0 });
            var services = new ProductServices(mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new ProductServices(mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }
    }
}
