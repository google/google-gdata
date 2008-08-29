using Google.GData.Client;
using Google.GData.Calendar;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;


namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AbstractFeedTest and is intended
    ///to contain all AbstractFeedTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AbstractFeedTest
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

        internal virtual AbstractFeed CreateAbstractFeed()
        {
            return new EventFeed(new Uri("http://www.google.com"), null);
        }

        /// <summary>
        ///A test for CreateFeedEntry
        ///</summary>
        [Test]
        public void CreateFeedEntryTest()
        {
            AbstractFeed target = CreateAbstractFeed(); // TODO: Initialize to an appropriate value
            AtomEntry actual;
            actual = target.CreateFeedEntry();

            Assert.IsTrue(actual is EventEntry, "This should create an eventEntry"); 
        }
    }
}
