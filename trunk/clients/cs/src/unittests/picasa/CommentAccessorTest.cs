using Google.GData.Photos;
using Google.Picasa;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
namespace Google.GData.Client.UnitTests.Picasa
{
    
    
    /// <summary>
    ///This is a test class for CommentAccessorTest and is intended
    ///to contain all CommentAccessorTest Unit Tests
    ///</summary>
    [TestFixture][Category("Picasa")]
    public class CommentAccessorTest
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
        ///A test for PhotoId
        ///</summary>
        [Test]
        public void PhotoIdTest()
        {
            Comment target = new Comment();
            string expected = "TestValue"; 
            string actual;
            target.PhotoId = expected;
            actual = target.PhotoId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [Test]
        public void IdTest()
        {
            Comment target = new Comment();
            string expected = "http://www.test.com/TestValue"; 
            string actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AlbumId
        ///</summary>
        [Test]
        public void AlbumIdTest()
        {
            Comment target = new Comment();
            string expected = "TestValue"; 
            string actual;
            target.AlbumId = expected;
            actual = target.AlbumId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CommentAccessor Constructor
        ///</summary>
        [Test]
        public void CommentAccessorConstructorTest()
        {
            Comment target = new Comment();
            Assert.IsNotNull(target);
        }
    }
}
