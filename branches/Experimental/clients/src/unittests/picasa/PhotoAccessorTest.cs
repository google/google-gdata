using Google.GData.Photos;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using Google.Picasa;

namespace Google.GData.Client.UnitTests.Picasa
{
    
    
    /// <summary>
    ///This is a test class for PhotoAccessorTest and is intended
    ///to contain all PhotoAccessorTest Unit Tests
    ///</summary>
    [TestFixture][Category("Picasa")]
    public class PhotoAccessorTest
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
        ///A test for Width
        ///</summary>
        [Test]
        public void WidthTest()
        {
            Photo target = new Photo();
            int expected = 5; // TODO: Initialize to an appropriate value
            int actual;
            target.Width = expected;
            actual = target.Width;
            Assert.AreEqual(expected, actual);
        }

       

        /// <summary>
        ///A test for Timestamp
        ///</summary>
        [Test]
        public void TimestampTest()
        {
            Photo target = new Photo();
            ulong expected = 122; // TODO: Initialize to an appropriate value
            ulong actual;
            target.Timestamp = expected;
            actual = target.Timestamp;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Size
        ///</summary>
        [Test]
        public void SizeTest()
        {
            Photo target = new Photo();
            long expected = 12; // TODO: Initialize to an appropriate value
            long actual;
            target.Size = expected;
            actual = target.Size;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Rotation
        ///</summary>
        [Test]
        public void RotationTest()
        {
            Photo target = new Photo();
            int expected = 45; // TODO: Initialize to an appropriate value
            int actual;
            target.Rotation = expected;
            actual = target.Rotation;
            Assert.AreEqual(expected, actual);
        }

        

        /// <summary>
        ///A test for PhotoTitle
        ///</summary>
        [Test]
        public void PhotoTitleTest()
        {
            Photo target = new Photo();
            string expected = "TestValue"; 
            string actual;
            target.Title = expected;
            actual = target.Title;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PhotoSummary
        ///</summary>
        [Test]
        public void PhotoSummaryTest()
        {
            Photo target = new Photo();
            string expected = "TestValue"; 
            string actual;
            target.Summary = expected;
            actual = target.Summary;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Longitude
        ///</summary>
        [Test]
        public void LongitudeTest()
        {
            Photo target = new Photo();
            double expected = 12.5F; // TODO: Initialize to an appropriate value
            double actual;
            target.Longitude = expected;
            actual = target.Longitude;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Latitude
        ///</summary>
        [Test]
        public void LatitudeTest()
        {
            Photo target = new Photo();
            double expected = 12.5F; // TODO: Initialize to an appropriate value
            double actual;
            target.Latitude = expected;
            actual = target.Latitude;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [Test]
        public void IdTest()
        {
            Photo target = new Photo();
            string expected = "TestValue"; 
            string actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Height
        ///</summary>
        [Test]
        public void HeightTest()
        {
            Photo target = new Photo();
            int expected = 12; // TODO: Initialize to an appropriate value
            int actual;
            target.Height = expected;
            actual = target.Height;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CommentingEnabled
        ///</summary>
        [Test]
        public void CommentingEnabledTest()
        {
            Photo target = new Photo();
            bool expected = true;
            bool actual;
            target.CommentingEnabled = expected;
            actual = target.CommentingEnabled;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CommentCount
        ///</summary>
        [Test]
        public void CommentCountTest()
        {
            Photo target = new Photo();
            uint expected = 12; // TODO: Initialize to an appropriate value
            uint actual;
            target.CommentCount = expected;
            actual = target.CommentCount;
            Assert.AreEqual(expected, actual);
        }

      

        /// <summary>
        ///A test for Checksum
        ///</summary>
        [Test]
        public void ChecksumTest()
        {
            Photo target = new Photo();
            string expected = "TestValue"; 
            string actual;
            target.Checksum = expected;
            actual = target.Checksum;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AlbumId
        ///</summary>
        [Test]
        public void AlbumIdTest()
        {
            Photo target = new Photo();
            string expected = "TestValue"; 
            string actual;
            target.AlbumId = expected;
            actual = target.AlbumId;
            Assert.AreEqual(expected, actual);
        }

    }
}
