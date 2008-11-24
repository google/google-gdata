using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;
using System.Collections.Generic;
using System.Net;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GDataRequestFactoryTest and is intended
    ///to contain all GDataRequestFactoryTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GDataRequestFactoryTest
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
            string userAgent = "TestValue"; // TODO: Initialize to an appropriate value
            GDataRequestFactory target = new GDataRequestFactory(userAgent); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            string actual;
            target.UserAgent = expected;
            actual = target.UserAgent;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UseGZip
        ///</summary>
        [Test]
        public void UseGZipTest()
        {
            string userAgent = "TestValue"; // TODO: Initialize to an appropriate value
            GDataRequestFactory target = new GDataRequestFactory(userAgent); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            target.UseGZip = expected;
            actual = target.UseGZip;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Timeout
        ///</summary>
        [Test]
        public void TimeoutTest()
        {
            string userAgent = "TestValue"; // TODO: Initialize to an appropriate value
            GDataRequestFactory target = new GDataRequestFactory(userAgent); // TODO: Initialize to an appropriate value
            int expected = 250000; // TODO: Initialize to an appropriate value
            int actual;
            target.Timeout = expected;
            actual = target.Timeout;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Slug
        ///</summary>
        [Test]
        public void SlugTest()
        {
            string userAgent = "TestValue"; // TODO: Initialize to an appropriate value
            GDataRequestFactory target = new GDataRequestFactory(userAgent); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Slug = expected;
            actual = target.Slug;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Proxy
        ///</summary>
        [Test]
        public void ProxyTest()
        {
            string userAgent = "TestValue"; // TODO: Initialize to an appropriate value
            GDataRequestFactory target = new GDataRequestFactory(userAgent); // TODO: Initialize to an appropriate value
            IWebProxy expected = new WebProxy();
            IWebProxy actual;
            target.Proxy = expected;
            actual = target.Proxy;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for KeepAlive
        ///</summary>
        [Test]
        public void KeepAliveTest()
        {
            string userAgent = "TestValue"; // TODO: Initialize to an appropriate value
            GDataRequestFactory target = new GDataRequestFactory(userAgent); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.KeepAlive = expected;
            actual = target.KeepAlive;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for CustomHeaders
        ///</summary>
        [Test]
        public void CustomHeadersTest()
        {
            string userAgent = "TestValue"; // TODO: Initialize to an appropriate value
            GDataRequestFactory target = new GDataRequestFactory(userAgent); // TODO: Initialize to an appropriate value
            List<string> actual;
            actual = target.CustomHeaders;
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 0); 
        }


        /// <summary>
        ///A test for ContentType
        ///</summary>
        [Test]
        public void ContentTypeTest()
        {
            string userAgent = "TestValue"; // TODO: Initialize to an appropriate value
            GDataRequestFactory target = new GDataRequestFactory(userAgent); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.ContentType = expected;
            actual = target.ContentType;
            Assert.AreEqual(expected, actual);
        }

          /// <summary>
        ///A test for GDataRequestFactory Constructor
        ///</summary>
        [Test]
        public void GDataRequestFactoryConstructorTest()
        {
            string userAgent = "TestValue"; // TODO: Initialize to an appropriate value
            GDataRequestFactory target = new GDataRequestFactory(userAgent);
            Assert.IsNotNull(target);
        }
    }
}
