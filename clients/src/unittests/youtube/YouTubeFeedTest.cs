using NUnit.Framework;
using Google.GData.Client.UnitTests;
using Google.GData.YouTube;

using System;
using Google.GData.Client;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for YouTubeFeedTest and is intended
    ///to contain all YouTubeFeedTest Unit Tests
    ///</summary>
    [TestFixture]
    [Category("YouTube")]
     public class YouTubeFeedTest
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
            YouTubeFeed target = new YouTubeFeed(uriBase, iService); // TODO: Initialize to an appropriate value
            YouTubeEntry entry  = target.CreateFeedEntry() as YouTubeEntry;
            Assert.IsNotNull(entry);
        }

        /// <summary>
        ///A test for YouTubeFeed Constructor
        ///</summary>
        [Test]
        public void YouTubeFeedConstructorTest()
        {
            Uri uriBase = null; // TODO: Initialize to an appropriate value
            IService iService = null; // TODO: Initialize to an appropriate value
            YouTubeFeed target = new YouTubeFeed(uriBase, iService);
            Assert.IsNotNull(target);
        }
    }
}
