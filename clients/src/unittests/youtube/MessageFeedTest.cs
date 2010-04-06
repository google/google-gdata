using Google.GData.YouTube;
using NUnit.Framework;
using System;
using Google.GData.Client;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for MessageFeedTest and is intended
    ///to contain all MessageFeedTest Unit Tests
    ///</summary>
    [TestFixture][Category("YouTube")]
    public class MessageFeedTest
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
            MessageFeed target = new MessageFeed(uriBase, iService); 
            MessageEntry entry = target.CreateFeedEntry() as MessageEntry;
            Assert.IsNotNull(entry, "better have a message entry here");
        }

        /// <summary>
        ///A test for MessageFeed Constructor
        ///</summary>
        [Test]
        public void MessageFeedConstructorTest()
        {
            Uri uriBase = null; // TODO: Initialize to an appropriate value
            IService iService = null; // TODO: Initialize to an appropriate value
            MessageFeed target = new MessageFeed(uriBase, iService);
            Assert.IsNotNull(target, "better have an object");
            Assert.IsNull(target.Service, "better have no service yet");
        }
    }
}
