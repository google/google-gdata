using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System;
using System.IO;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for ServiceTest and is intended
    ///to contain all ServiceTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class ServiceTest
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
        ///A test for RequestFactory
        ///</summary>
        [Test]
        public void RequestFactoryTest()
        {
            Service target = new Service(); // TODO: Initialize to an appropriate value
            IGDataRequestFactory expected = new GDataGAuthRequestFactory("cl", "test");
            IGDataRequestFactory actual;
            target.RequestFactory = expected;
            actual = target.RequestFactory;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Credentials
        ///</summary>
        [Test]
        public void CredentialsTest()
        {
            Service target = new Service(); // TODO: Initialize to an appropriate value
            GDataCredentials expected = new GDataCredentials("test", "pwd");
            GDataCredentials actual;
            target.Credentials = expected;
            actual = target.Credentials;
            Assert.AreEqual(expected, actual);
        }
    }
}
