using NUnit.Framework;
using Google.GData.Client.UnitTests;
using Google.GData.YouTube;

using System;
using System.Collections.Generic;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for YouTubeQueryTest and is intended
    ///to contain all YouTubeQueryTest Unit Tests
    ///</summary>
    [TestFixture]
[Category("YouTube")]
 
    public class YouTubeQueryTest
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
        ///A test for Time
        ///</summary>
        [Test]
        public void TimeTest()
        {
            YouTubeQuery target = new YouTubeQuery(); // TODO: Initialize to an appropriate value
            YouTubeQuery.UploadTime expected = new YouTubeQuery.UploadTime(); // TODO: Initialize to an appropriate value
            YouTubeQuery.UploadTime actual;
            target.Time = expected;
            actual = target.Time;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Restriction
        ///</summary>
        [Test]
        public void RestrictionTest()
        {
            YouTubeQuery target = new YouTubeQuery(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Restriction = expected;
            actual = target.Restriction;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for OrderBy
        ///</summary>
        [Test]
        public void OrderByTest()
        {
            YouTubeQuery target = new YouTubeQuery(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.OrderBy = expected;
            actual = target.OrderBy;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LR
        ///</summary>
        [Test]
        public void LRTest()
        {
            YouTubeQuery target = new YouTubeQuery(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.LR = expected;
            actual = target.LR;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Formats
        ///</summary>
        [Test]
        public void FormatsTest()
        {
            YouTubeQuery target = new YouTubeQuery(); // TODO: Initialize to an appropriate value

            target.Formats.Add(YouTubeQuery.VideoFormat.Embeddable);
            target.Formats.Add(YouTubeQuery.VideoFormat.Mobile);

            Assert.AreEqual(target.Formats[0], YouTubeQuery.VideoFormat.Embeddable);
            Assert.AreEqual(target.Formats[1], YouTubeQuery.VideoFormat.Mobile);

        }

        /// <summary>
        ///A test for Client
        ///</summary>
        [Test]
        public void ClientTest()
        {
            YouTubeQuery target = new YouTubeQuery(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Client = expected;
            actual = target.Client;
            Assert.AreEqual(expected,actual);
        }

    

        /// <summary>
        ///A test for YouTubeQuery Constructor
        ///</summary>
        [Test]
        public void YouTubeQueryConstructorTest1()
        {
            YouTubeQuery target = new YouTubeQuery();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for YouTubeQuery Constructor
        ///</summary>
        [Test]
        public void YouTubeQueryConstructorTest()
        {
            string queryUri = "http://www.google.com/";
            YouTubeQuery target = new YouTubeQuery(queryUri);
            Assert.IsNotNull(target);
            Assert.AreEqual(target.Uri, new Uri(queryUri), "Object value should be identical after construction");
        }
    }
}
