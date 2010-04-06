using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System;
using System.Xml;
using System.IO;
using System.Collections.Specialized;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomFeedTest and is intended
    ///to contain all AtomFeedTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomFeedTest
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
        ///A test for XmlName
        ///</summary>
        [Test]
        public void XmlNameTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            Assert.AreEqual(AtomParserNameTable.XmlFeedElement, target.XmlName);
        }

        /// <summary>
        ///A test for TotalResults
        ///</summary>
        [Test]
        public void TotalResultsTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            int expected = 5; // TODO: Initialize to an appropriate value
            int actual;
            target.TotalResults = expected;
            actual = target.TotalResults;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StartIndex
        ///</summary>
        [Test]
        public void StartIndexTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            int expected = 5; // TODO: Initialize to an appropriate value
            int actual;
            target.StartIndex = expected;
            actual = target.StartIndex;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Service
        ///</summary>
        [Test]
        public void ServiceTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            IService expected = new Service();
            IService actual;
            target.Service = expected;
            actual = target.Service;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Self
        ///</summary>
        [Test]
        public void SelfTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null);
            string expected = "http://www.test.com/self/";         
            string actual;
            target.Self = expected;
            actual = target.Self;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ReadOnly
        ///</summary>
        [Test]
        public void ReadOnlyTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            Assert.IsTrue(target.ReadOnly);
        }

        /// <summary>
        ///A test for PrevChunk
        ///</summary>
        [Test]
        public void PrevChunkTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null);
            string expected = "http://www.test.com/prev/";          
            string actual;
            target.PrevChunk = expected;
            actual = target.PrevChunk;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Post
        ///</summary>
        [Test]
        public void PostTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            string expected = "http://www.test.com/post/";            
            string actual;
            target.Post = expected;
            actual = target.Post;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for NextChunk
        ///</summary>
        [Test]
        public void NextChunkTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null);
            string expected = "http://www.abc.com/feeds/";     
            string actual;
            target.NextChunk = expected;
            actual = target.NextChunk;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ItemsPerPage
        ///</summary>
        [Test]
        public void ItemsPerPageTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.ItemsPerPage = expected;
            actual = target.ItemsPerPage;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Feed
        ///</summary>
        [Test]
        public void FeedTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            string expected = "http://www.abc.com/feeds/";            
            string actual;
            target.Feed = expected;
            actual = target.Feed;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Entries
        ///</summary>
        [Test]
        public void EntriesTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            Assert.IsNotNull(target.Entries);
        }

        /// <summary>
        ///A test for BatchData
        ///</summary>
        [Test]
        public void BatchDataTest()
        {
            AtomFeed target = new AtomFeed(new Uri("http://www.test.com/"), null); 
            GDataBatchFeedData expected = new GDataBatchFeedData(); 
            GDataBatchFeedData actual;
            target.BatchData = expected;
            actual = target.BatchData;
            Assert.AreEqual(expected, actual);
        }
    }
}
