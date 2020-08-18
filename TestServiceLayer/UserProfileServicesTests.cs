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
    public class UserProfileServicesTests
    {
        internal Mock<IUserProfileRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<IUserProfileRepository>();
        }

        [TestMethod]
        public void Delete_Null()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<UserProfile>())).Throws<NullReferenceException>();

            var UserProfileServices = new UserProfileServices(mockRepository.Object);

            Action act = () => UserProfileServices.Delete(null);
            act.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void Delete_Entity()
        {
            mockRepository.Setup(x => x.Delete(It.IsAny<UserProfile>())); ;

            var UserProfileServices = new UserProfileServices(mockRepository.Object);

            Action act = () => UserProfileServices.Delete(new UserProfile());
            act.Should().NotThrow();
        }

        private static IList<UserProfile> GetUserProfiles()
        {
            var userProfiles = new List<UserProfile>();
            userProfiles.Add(
                new UserProfile
                {
                    Username = "paul.michea",
                    Email = "michea.paul@yahoo.com",
                    Password = "password"
                }
            );

            return userProfiles;
        }

        [TestMethod]
        public void GetAll_NotEmpty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<UserProfile, bool>>>(),
                                           It.IsAny<Func<IQueryable<UserProfile>, IOrderedQueryable<UserProfile>>>(),
                                           It.IsAny<string>())).Returns(GetUserProfiles());

            var services = new UserProfileServices(mockRepository.Object);

            services.GetAll().Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetAll_Empty()
        {
            mockRepository.Setup(x => x.Get(It.IsAny<Expression<Func<UserProfile, bool>>>(),
                                           It.IsAny<Func<IQueryable<UserProfile>, IOrderedQueryable<UserProfile>>>(),
                                           It.IsAny<string>())).Returns(new List<UserProfile>());

            var services = new UserProfileServices(mockRepository.Object);

            services.GetAll().Should().BeEmpty();
        }

        [TestMethod]
        public void GetByID_EntityFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>())).Returns(new UserProfile { Id = 0 });
            var services = new UserProfileServices(mockRepository.Object);

            services.GetByID(0).Should().NotBeNull();
        }

        [TestMethod]
        public void GetByID_EntityNotFound()
        {
            mockRepository.Setup(x => x.GetByID(It.IsAny<object>()));

            var services = new UserProfileServices(mockRepository.Object);

            services.GetByID(0).Should().BeNull();
        }

        [TestMethod]
        public void Insert_InvalidUserProfile_ValidationErrors()
        { 
            mockRepository.Setup(x => x.Insert(It.IsAny<UserProfile>()));

            var userProfileServices = new UserProfileServices(mockRepository.Object);

            userProfileServices.Insert(new UserProfile()).Should().NotBeEmpty();
        }

        [TestMethod]
        public void Insert_ValidUserProfile_NoValidationErrors()
        {
            var userProfile = new UserProfile
            {
                Username = "dandelionn",
                Password = "Password0&",
                Email = "dan@email.com"
            };
            mockRepository.Setup(x => x.Insert(It.IsAny<UserProfile>()));

            var userProfileServices = new UserProfileServices(mockRepository.Object);

            userProfileServices.Insert(userProfile).Should().BeEmpty();
        }

        [TestMethod]
        public void MakeABid()
        {

        }

        [TestMethod]
        public void Insert_StartAuction()
        {

        }

    }
}