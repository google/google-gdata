using Google.GData.YouTube;
using NUnit.Framework;
using System;
using Google.GData.Client;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for SubscriptionFeedTest and is intended
    ///to contain all SubscriptionFeedTest Unit Tests
    ///</summary>
    [TestFixture][Category("YouTube")]
    public class SubscriptionFeedTest
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
        ///A test for CreateFeedEntry
        ///</summary>
        [Test]
        public void CreateFeedEntryTest()
        {
            Uri uriBase = null; // TODO: Initialize to an appropriate value
            IService iService = null; // TODO: Initialize to an appropriate value
            SubscriptionFeed target = new SubscriptionFeed(uriBase, iService); // TODO: Initialize to an appropriate value
            SubscriptionEntry expected = target.CreateFeedEntry() as SubscriptionEntry;
            Assert.IsNotNull(expected);
        }

        /// <summary>
        ///A test for SubscriptionFeed Constructor
        ///</summary>
        [Test]
        public void SubscriptionFeedConstructorTest()
        {
            Uri uriBase = null; // TODO: Initialize to an appropriate value
            IService iService = null; // TODO: Initialize to an appropriate value
            SubscriptionFeed target = new SubscriptionFeed(uriBase, iService);
            Assert.IsNotNull(target);
        }
    }
}
