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
    public class CategoryServicesTests
    {
        internal Mock<ICategoryRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<ICategoryRepository>();
        }

        [TestMethod]
        public void Delete_Null()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Category>())).Throws<NullReferenceException>();

            var categoryServices = new CategoryServices(mockRepository.Object);

            Action act = () => categoryServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Delete_Entity()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<Category>())); ;

            var categoryServices = new CategoryServices(mockRepository.Object);

            Action act = () => categoryServices.Delete(new Category());
            act.Should().NotThrow();
        }

        private static IList<Category> getCategories()
        {
            var categories = new List<Category>();
            categories.Add(
                new Category
                {
                    Name = nameof(Category),
                }
            );

            return categories;
        }

        [TestMethod]
        public void GetAll_NotEmpty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Category, bool>>>(),
                                           It.IsAny<Func<IQueryable<Category>, IOrderedQueryable<Category>>>(),
                                           It.IsAny<string>())).Returns(getCategories());

            var services = new CategoryServices(mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetAll_Empty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Category, bool>>>(),
                                           It.IsAny<Func<IQueryable<Category>, IOrderedQueryable<Category>>>(),
                                           It.IsAny<string>())).Returns(new List<Category>());

            var services = new CategoryServices(mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        [TestMethod]
        public void GetByID_EntityFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Category { Id = 0 });
            var services = new CategoryServices(mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new CategoryServices(mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }
    }
}
