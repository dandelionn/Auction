using System;
using Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoggerTests
{
    [TestClass]
    public class LoggingTests
    {
        private Log4NetWrapper log4NetWrapper;

        [TestInitialize]
        public void Initialize()
        {
            this.log4NetWrapper = new Log4NetWrapper(typeof(LoggingTests));
        }

        [TestMethod]
        public void TestMethod1()
        {
            this.log4NetWrapper.Debug("test_message");
        }
    }
}
