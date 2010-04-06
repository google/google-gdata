using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;

    
    

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for FeedParserEventArgsTest and is intended
    ///to contain all FeedParserEventArgsTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class FeedParserEventArgsTest
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
        ///A test for Feed
        ///</summary>
        [Test]
        public void FeedTest()
        {
            FeedParserEventArgs target = new FeedParserEventArgs(); // TODO: Initialize to an appropriate value
            Assert.IsNull(target.Feed);
            Assert.IsNull(target.Entry);

            target = new FeedParserEventArgs(new AtomFeed(new Uri("http://www.test.com/"), null), new AtomEntry());
            Assert.IsNotNull(target.Feed);
            Assert.IsNotNull(target.Entry);
        }

        /// <summary>
        ///A test for Entry
        ///</summary>
        [Test]
        public void EntryTest()
        {
            FeedParserEventArgs target = new FeedParserEventArgs(); // TODO: Initialize to an appropriate value
            AtomEntry expected = new AtomEntry();
            AtomEntry actual;
            target.Entry = expected;
            actual = target.Entry;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for DiscardEntry
        ///</summary>
        [Test]
        public void DiscardEntryTest()
        {
            FeedParserEventArgs target = new FeedParserEventArgs(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.DiscardEntry = expected;
            actual = target.DiscardEntry;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for FeedParserEventArgs Constructor
        ///</summary>
        [Test]
        public void FeedParserEventArgsConstructorTest1()
        {
            FeedParserEventArgs target = new FeedParserEventArgs();
            Assert.IsNotNull(target);
            Assert.IsNull(target.Feed);
            Assert.IsNull(target.Entry);
            Assert.IsFalse(target.DoneParsing);
            Assert.IsTrue(target.CreatingEntry);
        }

    }
}
