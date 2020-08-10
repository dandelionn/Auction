using DataMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
    }
}
