using DataMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestServiceLayer
{
    [TestClass]
    public class BidderServicesTests
    {
        internal Mock<IBidderRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<IBidderRepository>();
        }
    }
}
