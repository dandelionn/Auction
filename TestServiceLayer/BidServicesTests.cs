using DataMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestServiceLayer
{
    [TestClass]
    public class BidServicesTests
    {
        internal Mock<IBidRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<IBidRepository>();
        }
    }
}
