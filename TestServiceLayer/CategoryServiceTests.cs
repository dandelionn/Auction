//-----------------------------------------------------------------------
// <copyright file="CategoryServiceTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="CategoryServiceTests" />.
    /// </summary>
    [TestClass]
    public class CategoryServiceTests
    {
        /// <summary>
        /// Defines the mockRepository.
        /// </summary>
        private Mock<ICategoryRepository> mockRepository;

        /// <summary>
        /// The TestInit.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this.mockRepository = new Mock<ICategoryRepository>();
        }

        /// <summary>
        /// The Delete_Null.
        /// </summary>
        [TestMethod]
        public void Delete_Null()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Category>())).Throws<NullReferenceException>();

            var categoryServices = new CategoryService(this.mockRepository.Object);

            Action act = () => categoryServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Delete_Entity.
        /// </summary>
        [TestMethod]
        public void Delete_Entity()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<Category>()));

            var categoryServices = new CategoryService(this.mockRepository.Object);

            Action act = () => categoryServices.Delete(new Category());
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
                                           It.IsAny<Expression<Func<Category, bool>>>(),
                                           It.IsAny<Func<IQueryable<Category>, IOrderedQueryable<Category>>>(),
                                           It.IsAny<string>())).Returns(GetCategories());

            var services = new CategoryService(this.mockRepository.Object);

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
                                           It.IsAny<Expression<Func<Category, bool>>>(),
                                           It.IsAny<Func<IQueryable<Category>, IOrderedQueryable<Category>>>(),
                                           It.IsAny<string>())).Returns(new List<Category>());

            var services = new CategoryService(this.mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        /// <summary>
        /// The GetByID_EntityFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new Category { Id = 0 });
            var services = new CategoryService(this.mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        /// <summary>
        /// The GetByID_EntityNotFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new CategoryService(this.mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        /// <summary>
        /// The getCategories.
        /// </summary>
        /// <returns>The <see cref="IList{Category}"/>.</returns>
        private static IList<Category> GetCategories()
        {
            var categories = new List<Category>();
            categories.Add(
                new Category
                {
                    Name = nameof(Category),
                });

            return categories;
        }
    }
}
