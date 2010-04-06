using Google.GData.Photos;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using Google.Picasa;



namespace Google.GData.Client.UnitTests.Picasa
{
    
    
    /// <summary>
    ///This is a test class for AlbumAccessorTest and is intended
    ///to contain all AlbumAccessorTest Unit Tests
    ///</summary>
    [TestFixture][Category("Picasa")]
    public class AlbumAccessorTest
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
        ///A test for NumPhotosRemaining
        ///</summary>
        [Test]
        public void NumPhotosRemainingTest()
        {
            Album target = new Album(); 
            uint expected = 4; // TODO: Initialize to an appropriate value
            uint actual;
            target.NumPhotosRemaining = expected;
            actual = target.NumPhotosRemaining;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NumPhotos
        ///</summary>
        [Test]
        public void NumPhotosTest()
        {
            Album target = new Album(); 
            uint expected = 4; // TODO: Initialize to an appropriate value
            uint actual;
            target.NumPhotos = expected;
            actual = target.NumPhotos;
            Assert.AreEqual(expected, actual);
        }

        

        /// <summary>
        ///A test for Longitude
        ///</summary>
        [Test]
        public void LongitudeTest()
        {
            Album target = new Album(); 
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            target.Longitude = expected;
            actual = target.Longitude;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Location
        ///</summary>
        [Test]
        public void LocationTest()
        {
            Album target = new Album(); 
            string expected = "TestValue"; 
            string actual;
            target.Location = expected;
            actual = target.Location;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Latitude
        ///</summary>
        [Test]
        public void LatitudeTest()
        {
            Album target = new Album(); 
            double expected = 0F; // TODO: Initialize to an appropriate value
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
            Album target = new Album(); 
            string expected = "TestValue"; 
            string actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CommentingEnabled
        ///</summary>
        [Test]
        public void CommentingEnabledTest()
        {
            Album target = new Album(); 
            bool expected = false; // TODO: Initialize to an appropriate value
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
            Album target = new Album(); 
            uint expected = 12; // TODO: Initialize to an appropriate value
            uint actual;
            target.CommentCount = expected;
            actual = target.CommentCount;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for BytesUsed
        ///</summary>
        [Test]
        public void BytesUsedTest()
        {
            Album target = new Album(); 
            uint expected = 12; // TODO: Initialize to an appropriate value
            uint actual;
            target.BytesUsed = expected;
            actual = target.BytesUsed;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AlbumTitle
        ///</summary>
        [Test]
        public void AlbumTitleTest()
        {
            Album target = new Album(); 
            string expected = "TestValue"; 
            string actual;
            target.Title = expected;
            actual = target.Title;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AlbumSummary
        ///</summary>
        [Test]
        public void AlbumSummaryTest()
        {
            Album target = new Album(); 
            string expected = "TestValue"; 
            string actual;
            target.Summary = expected;
            actual = target.Summary;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AlbumAuthorNickname
        ///</summary>
        [Test]
        public void AlbumAuthorNicknameTest()
        {
            Album target = new Album(); 
            string expected = "TestValue"; 
            string actual;
            target.AlbumAuthorNickname = expected;
            actual = target.AlbumAuthorNickname;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AlbumAuthor
        ///</summary>
        [Test]
        public void AlbumAuthorTest()
        {
            Album target = new Album(); 
            string expected = "TestValue"; 
            string actual;
            target.Author = expected;
            actual = target.Author;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Access
        ///</summary>
        [Test]
        public void AccessTest()
        {
            Album target = new Album(); 
            string expected = "TestValue"; 
            string actual;
            target.Access = expected;
            actual = target.Access;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AlbumAccessor Constructor
        ///</summary>
        [Test]
        public void AlbumAccessorConstructorTest()
        {
            Album target = new Album(); 
            Assert.IsNotNull(target);
        }
    }
}
