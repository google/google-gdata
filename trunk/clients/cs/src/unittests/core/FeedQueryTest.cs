using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System.Text;
using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for FeedQueryTest and is intended
    ///to contain all FeedQueryTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class FeedQueryTest
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
        ///A test for UseSSL
        ///</summary>
        [Test]
        public void UseSSLTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            bool expected = true; 
            bool actual;
            target.UseSSL = expected;
            actual = target.UseSSL;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Uri
        ///</summary>
        [Test]
        public void UriTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            Uri expected = new Uri("http://www.test.com/"); 
            Uri actual;
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StartIndex
        ///</summary>
        [Test]
        public void StartIndexTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            int expected = 12; // TODO: Initialize to an appropriate value
            int actual;
            target.StartIndex = expected;
            actual = target.StartIndex;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StartDate
        ///</summary>
        [Test]
        public void StartDateTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            DateTime expected = DateTime.Now; // TODO: Initialize to an appropriate value
            DateTime actual;
            target.StartDate = expected;
            actual = target.StartDate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Query
        ///</summary>
        [Test]
        public void QueryTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Query = expected;
            actual = target.Query;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NumberToRetrieve
        ///</summary>
        [Test]
        public void NumberToRetrieveTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            int expected = 12; // TODO: Initialize to an appropriate value
            int actual;
            target.NumberToRetrieve = expected;
            actual = target.NumberToRetrieve;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MinPublication
        ///</summary>
        [Test]
        public void MinPublicationTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            DateTime expected = DateTime.Now;
            DateTime actual;
            target.MinPublication = expected;
            actual = target.MinPublication;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MaxPublication
        ///</summary>
        [Test]
        public void MaxPublicationTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            DateTime expected = DateTime.Now;
            DateTime actual;
            target.MaxPublication = expected;
            actual = target.MaxPublication;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FeedFormat
        ///</summary>
        [Test]
        public void FeedFormatTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            AlternativeFormat expected = AlternativeFormat.Rss;
            AlternativeFormat actual;
            target.FeedFormat = expected;
            actual = target.FeedFormat;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ExtraParameters
        ///</summary>
        [Test]
        public void ExtraParametersTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.ExtraParameters = expected;
            actual = target.ExtraParameters;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EndDate
        ///</summary>
        [Test]
        public void EndDateTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            DateTime expected = DateTime.Now;
            DateTime actual;
            target.EndDate = expected;
            actual = target.EndDate;
            Assert.AreEqual(expected, actual);
        }

        
        /// <summary>
        ///A test for Categories
        ///</summary>
        [Test]
        public void CategoriesTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.Categories);
        }

        /// <summary>
        ///A test for Author
        ///</summary>
        [Test]
        public void AuthorTest()
        {
            FeedQuery target = new FeedQuery(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Author = expected;
            actual = target.Author;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [Test]
        public void ParseTest()
        {
            Uri uri = new Uri("http://www.google.com/feeds?max-results=20&start-index=12");
            FeedQuery query = null; // TODO: Initialize to an appropriate value
            Service service = null;
            FeedQuery.Parse(uri, out service, out query);
            Assert.IsNotNull(service);
            Assert.AreEqual(12, query.StartIndex);
            Assert.AreEqual(20, query.NumberToRetrieve);
           
        }


        /// <summary>
        ///A test for FeedQuery Constructor
        ///</summary>
        [Test]
        public void FeedQueryConstructorTest1()
        {
            FeedQuery target = new FeedQuery();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for FeedQuery Constructor
        ///</summary>
        [Test]
        public void FeedQueryConstructorTest()
        {
            string baseUri = "http://www.test.com/"; // TODO: Initialize to an appropriate value
            FeedQuery target = new FeedQuery(baseUri);
            Assert.AreEqual(target.Uri, new Uri(baseUri));
        }

		/// <summary>
		///A test for FeedQuery URI copy
		///</summary>
		[Test]
		public void FeedQueryCopyUriTest() {
			string baseUri = "http://www.test.com/";
			FeedQuery source = new FeedQuery(baseUri);
			source.Author = "Firstname Lastname";

			FeedQuery target = new FeedQuery();
			target.Uri = source.Uri;
			Assert.AreEqual(source.Author, target.Author);
		}
    }
}
