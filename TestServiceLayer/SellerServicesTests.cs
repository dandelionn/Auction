using DataMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestServiceLayer
{
    [TestClass]
    public class SellerServicesTests
    {
        internal Mock<ISellerRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<ISellerRepository>();
        }
    }
}