using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for ServiceEventArgsTest and is intended
    ///to contain all ServiceEventArgsTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class ServiceEventArgsTest
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
        ///A test for Uri
        ///</summary>
        [Test]
        public void UriTest()
        {
            Uri uri = new Uri("http://www.test.com/");
            IService service = new Service();
            ServiceEventArgs target = new ServiceEventArgs(uri, service); // TODO: Initialize to an appropriate value
            Uri actual;
            actual = target.Uri;
            Assert.AreEqual(actual, uri);
        }

        /// <summary>
        ///A test for Service
        ///</summary>
        [Test]
        public void ServiceTest()
        {
            Uri uri = new Uri("http://www.test.com/");
            Service service = new Service();
            ServiceEventArgs target = new ServiceEventArgs(uri, service); // TODO: Initialize to an appropriate value
            Service actual;
            actual = target.Service as Service;
            Assert.AreEqual(actual, service);
        }

        /// <summary>
        ///A test for Feed
        ///</summary>
        [Test]
        public void FeedTest()
        {
            Uri uri = new Uri("http://www.test.com/");
            IService service = new Service();
            ServiceEventArgs target = new ServiceEventArgs(uri, service); // TODO: Initialize to an appropriate value

            AtomFeed expected = new AtomFeed(uri, service);
            AtomFeed actual;
            target.Feed = expected;
            actual = target.Feed;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ServiceEventArgs Constructor
        ///</summary>
        [Test]
        public void ServiceEventArgsConstructorTest()
        {
            Uri uri = new Uri("http://www.test.com/");
            IService service = new Service();
            ServiceEventArgs target = new ServiceEventArgs(uri, service); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target);
        }
    }
}
