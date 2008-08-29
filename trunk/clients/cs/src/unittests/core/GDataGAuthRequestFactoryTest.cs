using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GDataGAuthRequestFactoryTest and is intended
    ///to contain all GDataGAuthRequestFactoryTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GDataGAuthRequestFactoryTest
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
        ///A test for UserAgent
        ///</summary>
        [Test]
        public void UserAgentTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.UserAgent = expected;
            actual = target.UserAgent;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StrictRedirect
        ///</summary>
        [Test]
        public void StrictRedirectTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            target.StrictRedirect = expected;
            actual = target.StrictRedirect;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Service
        ///</summary>
        [Test]
        public void ServiceTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Service = expected;
            actual = target.Service;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NumberOfRetries
        ///</summary>
        [Test]
        public void NumberOfRetriesTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            int expected = 2; // TODO: Initialize to an appropriate value
            int actual;
            target.NumberOfRetries = expected;
            actual = target.NumberOfRetries;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MethodOverride
        ///</summary>
        [Test]
        public void MethodOverrideTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            target.MethodOverride = expected;
            actual = target.MethodOverride;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Handler
        ///</summary>
        [Test]
        public void HandlerTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Handler = expected;
            actual = target.Handler;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GAuthToken
        ///</summary>
        [Test]
        public void GAuthTokenTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.GAuthToken = expected;
            actual = target.GAuthToken;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CaptchaToken
        ///</summary>
        [Test]
        public void CaptchaTokenTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.CaptchaToken = expected;
            actual = target.CaptchaToken;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CaptchaAnswer
        ///</summary>
        [Test]
        public void CaptchaAnswerTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.CaptchaAnswer = expected;
            actual = target.CaptchaAnswer;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ApplicationName
        ///</summary>
        [Test]
        public void ApplicationNameTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.ApplicationName = expected;
            actual = target.ApplicationName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AccountType
        ///</summary>
        [Test]
        public void AccountTypeTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GDataGAuthRequestFactory target = new GDataGAuthRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.AccountType = expected;
            actual = target.AccountType;
            Assert.AreEqual(expected, actual);
        }
    }
}
