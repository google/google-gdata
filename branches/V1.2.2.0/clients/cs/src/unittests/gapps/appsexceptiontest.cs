using System;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class AppsExceptionTest
    {
        private AppsException exception;

        [SetUp]
        public void Init()
        {
            exception = new AppsException();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetErrorCodeTest()
        {
            Assert.IsNull(exception.ErrorCode, "Error code should initially be null.");
        }

        [Test]
        public void SetErrorCodeTest()
        {
            exception.ErrorCode = "1000";
            Assert.AreEqual("1000", exception.ErrorCode,
                "Error code should be 1000 after setting");
        }

        [Test]
        public void GetInvalidInputTest()
        {
            Assert.IsNull(exception.InvalidInput, "Invalid input should initially be null");
        }

        [Test]
        public void SetInvalidInputTest()
        {
            exception.InvalidInput = "jdoe";
            Assert.AreEqual("jdoe", exception.InvalidInput,
                "Invalid input should be jdoe after setting");
        }

        [Test]
        public void GetReasonTest()
        {
            Assert.IsNull(exception.Reason, "Reason should initially be null");
        }

        [Test]
        public void SetReasonTest()
        {
            exception.Reason = "User does not exist";
            Assert.AreEqual("User does not exist", exception.Reason,
                "Reason should be 'User does not exist' after setting");
        }
    }
}