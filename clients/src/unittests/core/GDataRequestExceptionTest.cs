using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System.Net;
using System.Runtime.Serialization;
using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GDataRequestExceptionTest and is intended
    ///to contain all GDataRequestExceptionTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GDataRequestExceptionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ResponseString
        ///</summary>
        [Test]
        public void ResponseStringTest()
        {
            GDataRequestException target = new GDataRequestException(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ResponseString;
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for Response
        ///</summary>
        [Test]
        public void ResponseTest()
        {
            GDataRequestException target = new GDataRequestException(); // TODO: Initialize to an appropriate value
            WebResponse actual;
            actual = target.Response;
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GDataRequestException Constructor
        ///</summary>
        [Test]
        public void GDataRequestExceptionConstructorTest()
        {
            string msg = "TestValue"; // TODO: Initialize to an appropriate value
            WebException exception = new WebException();
            GDataRequestException target = new GDataRequestException(msg, exception);

            Assert.IsNotNull(target);
            Assert.AreEqual(msg, target.Message);
            Assert.AreEqual(exception, target.InnerException);
        }
    }
}
