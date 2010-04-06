using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GDataLoggingRequestFactoryTest and is intended
    ///to contain all GDataLoggingRequestFactoryTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GDataLoggingRequestFactoryTest
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
        ///A test for ResponseFileName
        ///</summary>
        [Test]
        public void ResponseFileNameTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataLoggingRequestFactory target = new GDataLoggingRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.ResponseFileName = expected;
            actual = target.ResponseFileName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RequestFileName
        ///</summary>
        [Test]
        public void RequestFileNameTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataLoggingRequestFactory target = new GDataLoggingRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.RequestFileName = expected;
            actual = target.RequestFileName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CombinedLogFileName
        ///</summary>
        [Test]
        public void CombinedLogFileNameTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataLoggingRequestFactory target = new GDataLoggingRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.CombinedLogFileName = expected;
            actual = target.CombinedLogFileName;
            Assert.AreEqual(expected, actual);
        }
    }
}
