using Google.GData.Photos;
using NUnit.Framework;
using Google.GData.Client.UnitTests;



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
            PicasaEntry entry = new AlbumEntry(); 
            AlbumAccessor target = new AlbumAccessor(entry); 
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
            uint expected = 4; // TODO: Initialize to an appropriate value
            uint actual;
            target.NumPhotos = expected;
            actual = target.NumPhotos;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [Test]
        public void NameTest()
        {
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
            string expected = "TestValue"; 
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Longitude
        ///</summary>
        [Test]
        public void LongitudeTest()
        {
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
            string expected = "TestValue"; 
            string actual;
            target.AlbumTitle = expected;
            actual = target.AlbumTitle;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AlbumSummary
        ///</summary>
        [Test]
        public void AlbumSummaryTest()
        {
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
            string expected = "TestValue"; 
            string actual;
            target.AlbumSummary = expected;
            actual = target.AlbumSummary;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AlbumAuthorNickname
        ///</summary>
        [Test]
        public void AlbumAuthorNicknameTest()
        {
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
            string expected = "TestValue"; 
            string actual;
            target.AlbumAuthor = expected;
            actual = target.AlbumAuthor;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Access
        ///</summary>
        [Test]
        public void AccessTest()
        {
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
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
            PicasaEntry entry = new AlbumEntry();
            AlbumAccessor target = new AlbumAccessor(entry);
            Assert.IsNotNull(target);
        }
    }
}
