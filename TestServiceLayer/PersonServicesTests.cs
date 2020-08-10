using DataMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestServiceLayer
{
    [TestClass]
    public class PersonServicesTests
    {
        internal Mock<IPersonRepository> mockRepository;

        [TestInitialize]
        public void TestInit()
        {
            mockRepository = new Mock<IPersonRepository>();
        }
    }
}
