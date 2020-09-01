//-----------------------------------------------------------------------
// <copyright file="UserProfileServiceTests.cs" company="Transilvania University of Brasov">    
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
    /// Defines the <see cref="UserProfileServiceTests" />.
    /// </summary>
    [TestClass]
    public class UserProfileServiceTests
    {
        /// <summary>
        /// Defines the mockRepository.
        /// </summary>
        private Mock<IUserProfileRepository> mockRepository;

        /// <summary>
        /// The TestInit.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this.mockRepository = new Mock<IUserProfileRepository>();
        }

        /// <summary>
        /// The Delete_Null.
        /// </summary>
        [TestMethod]
        public void Delete_Null()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<UserProfile>())).Throws<NullReferenceException>();

            var userProfileServices = new UserProfileService(this.mockRepository.Object);

            Action act = () => userProfileServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        /// <summary>
        /// The Delete_Entity.
        /// </summary>
        [TestMethod]
        public void Delete_Entity()
        {
            this.mockRepository.Setup(x => x.Delete(It.IsAny<UserProfile>()));

            var userProfileServices = new UserProfileService(this.mockRepository.Object);

            Action act = () => userProfileServices.Delete(new UserProfile());
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
                                           It.IsAny<Expression<Func<UserProfile, bool>>>(),
                                           It.IsAny<Func<IQueryable<UserProfile>, IOrderedQueryable<UserProfile>>>(),
                                           It.IsAny<string>())).Returns(GetUserProfiles());

            var services = new UserProfileService(this.mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        /// <summary>
        /// The GetAll_Empty.
        /// </summary>
        [TestMethod]
        public void GetAll_Empty()
        {
            this.mockRepository.Setup(x => x.Get(
                                            It.IsAny<Expression<Func<UserProfile, bool>>>(),
                                            It.IsAny<Func<IQueryable<UserProfile>, IOrderedQueryable<UserProfile>>>(),
                                            It.IsAny<string>())).Returns(new List<UserProfile>());

            var services = new UserProfileService(this.mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        /// <summary>
        /// The GetByID_EntityFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new UserProfile { Id = 0 });
            var services = new UserProfileService(this.mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        /// <summary>
        /// The GetByID_EntityNotFound.
        /// </summary>
        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            this.mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new UserProfileService(this.mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        /// <summary>
        /// The Insert_InvalidUserProfile_ValidationErrors.
        /// </summary>
        [TestMethod]
        public void Insert_InvalidUserProfile_ValidationErrors()
        {
            this.mockRepository.Setup(x => x.Insert(It.IsAny<UserProfile>()));

            var userProfileServices = new UserProfileService(this.mockRepository.Object);

            userProfileServices.Insert(new UserProfile()).Should().NotBeEmpty();
        }

        /// <summary>
        /// The Insert_ValidUserProfile_NoValidationErrors.
        /// </summary>
        [TestMethod]
        public void Insert_ValidUserProfile_NoValidationErrors()
        {
            var userProfile = new UserProfile
            {
                Username = "dandelionn",
                Password = "Password0&",
                Email = "dan@email.com"
            };
            this.mockRepository.Setup(x => x.Insert(It.IsAny<UserProfile>()));

            var userProfileServices = new UserProfileService(this.mockRepository.Object);

            userProfileServices.Insert(userProfile).Should().BeEmpty();
        }

        /// <summary>
        /// The Update_ValidUserProfile_NoValidationErrors.
        /// </summary>
        [TestMethod]
        public void Update_ValidUserProfile_NoValidationErrors()
        {
            var userProfile = new UserProfile
            {
                Username = "dandelionn",
                Password = "Password0&",
                Email = "dan@email.com"
            };
            this.mockRepository.Setup(x => x.Insert(It.IsAny<UserProfile>()));

            var userProfileServices = new UserProfileService(this.mockRepository.Object);

            userProfileServices.Update(userProfile).Should().BeEmpty();
        }

        /// <summary>
        /// The GetUserProfiles.
        /// </summary>
        /// <returns>The <see cref="IList{UserProfile}"/>.</returns>
        private static IList<UserProfile> GetUserProfiles()
        {
            var userProfiles = new List<UserProfile>();
            userProfiles.Add(
                new UserProfile
                {
                    Username = "paul.michea",
                    Email = "michea.paul@yahoo.com",
                    Password = "password",
                });

            return userProfiles;
        }
    }
}
