using DataMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestServiceLayer
{
    [TestClass]
    public class CategoryServicesImplementationTests
    {
        internal Mock<ICategoryRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<ICategoryRepository>();
        }
    }
}
