using NUnit.Framework;
using Google.GData.Client.UnitTests;
using Google.GData.YouTube;

using Google.GData.Extensions;
using Google.GData.Extensions.Location;
using Google.GData.Extensions.MediaRss;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for YouTubeEntryTest and is intended
    ///to contain all YouTubeEntryTest Unit Tests
    ///</summary>
    [TestFixture]
[Category("YouTube")]
 
    public class YouTubeEntryTest
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
        ///A test for Media
        ///</summary>
        [Test]
        public void MediaTest()
        {
            YouTubeEntry target = new YouTubeEntry(); // TODO: Initialize to an appropriate value
            GData.YouTube.MediaGroup expected = new GData.YouTube.MediaGroup();
            GData.YouTube.MediaGroup actual;
            target.Media = expected;
            actual = target.Media;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Location
        ///</summary>
        [Test]
        public void LocationTest()
        {
            YouTubeEntry target = new YouTubeEntry(); // TODO: Initialize to an appropriate value
            GeoRssWhere expected = new GeoRssWhere(10, 11);
            GeoRssWhere actual;
            target.Location = expected;
            actual = target.Location;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for YouTubeEntry Constructor
        ///</summary>
        [Test]
        public void YouTubeEntryConstructorTest()
        {
            YouTubeEntry target = new YouTubeEntry();
            Assert.IsNotNull(target,  "Object should not be null after construction");
        }
    }
}
