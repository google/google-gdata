using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System.Xml;
using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomEntryTest and is intended
    ///to contain all AtomEntryTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomEntryTest
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
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            Assert.AreEqual(AtomParserNameTable.XmlAtomEntryElement, target.XmlName);
        }

        /// <summary>
        ///A test for Updated
        ///</summary>
        [Test]
        public void UpdatedTest()
        {
            AtomEntry target = new AtomEntry(); 
            DateTime expected = DateTime.Now; 
            DateTime actual;
            target.Updated = expected;
            actual = target.Updated;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        [Test]
        public void TitleTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            target.Title.Text = "test";
            Assert.AreEqual("test", target.Title.Text); 
        }

        /// <summary>
        ///A test for Summary
        ///</summary>
        [Test]
        public void SummaryTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            target.Summary.Text = "summary"; 
            Assert.AreEqual(target.Summary.Text, "summary"); 
        }

        /// <summary>
        ///A test for Source
        ///</summary>
        [Test]
        public void SourceTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            AtomSource expected = new AtomSource();
            AtomSource actual;
            target.Source = expected;
            actual = target.Source;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Service
        ///</summary>
        [Test]
        public void ServiceTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            IService expected =new Service(); 
            IService actual;
            target.Service = expected;
            actual = target.Service;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SelfUri
        ///</summary>
        [Test]
        public void SelfUriTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/"); 
            AtomUri actual;
            target.SelfUri = expected;
            actual = target.SelfUri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Rights
        ///</summary>
        [Test]
        public void RightsTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            target.Rights.Text = "Right"; 
            Assert.AreEqual(target.Rights.Text, "Right"); 
        }

        /// <summary>
        ///A test for ReadOnly
        ///</summary>
        [Test]
        public void ReadOnlyTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            Assert.IsTrue(target.ReadOnly); 
        }

        /// <summary>
        ///A test for Published
        ///</summary>
        [Test]
        public void PublishedTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            DateTime expected = DateTime.Now;
            DateTime actual;
            target.Published = expected;
            actual = target.Published;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MediaUri
        ///</summary>
        [Test]
        public void MediaUriTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/"); 
            AtomUri actual;
            target.MediaUri = expected;
            actual = target.MediaUri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Links
        ///</summary>
        [Test]
        public void LinksTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.Links);
        }

        /// <summary>
        ///A test for IsDraft
        ///</summary>
        [Test]
        public void IsDraftTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            Assert.IsFalse(target.IsDraft);
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [Test]
        public void IdTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            AtomId expected = new AtomId("http://www.test.com/");
            AtomId actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FeedUri
        ///</summary>
        [Test]
        public void FeedUriTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            string expected = "http://www.test.com/";            
            string actual;
            target.FeedUri = expected;
            actual = target.FeedUri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Feed
        ///</summary>
        [Test]
        public void FeedTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            Assert.IsNull(target.Feed);
        }

        /// <summary>
        ///A test for EditUri
        ///</summary>
        [Test]
        public void EditUriTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/"); 
            AtomUri actual;
            target.EditUri = expected;
            actual = target.EditUri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Contributors
        ///</summary>
        [Test]
        public void ContributorsTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.Contributors);
        }

        /// <summary>
        ///A test for Content
        ///</summary>
        [Test]
        public void ContentTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            AtomContent expected = new AtomContent();
            AtomContent actual;
            target.Content = expected;
            actual = target.Content;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Categories
        ///</summary>
        [Test]
        public void CategoriesTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.Categories);
        }

        /// <summary>
        ///A test for BatchData
        ///</summary>
        [Test]
        public void BatchDataTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            GDataBatchEntryData expected = new GDataBatchEntryData();
            GDataBatchEntryData actual;
            target.BatchData = expected;
            actual = target.BatchData;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Authors
        ///</summary>
        [Test]
        public void AuthorsTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.Authors);
        }

        /// <summary>
        ///A test for AlternateUri
        ///</summary>
        [Test]
        public void AlternateUriTest()
        {
            AtomEntry target = new AtomEntry(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/"); 
            AtomUri actual;
            target.AlternateUri = expected;
            actual = target.AlternateUri;
            Assert.AreEqual(expected, actual);
        }
    }
}
