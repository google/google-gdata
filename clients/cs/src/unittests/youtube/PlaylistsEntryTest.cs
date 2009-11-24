using Google.GData.YouTube;
using NUnit.Framework;
using Google.GData.Extensions;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for PlaylistsEntryTest and is intended
    ///to contain all PlaylistsEntryTest Unit Tests
    ///</summary>
    [TestFixture][Category("YouTube")]
    public class PlaylistsEntryTest
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
        ///A test for FeedLink
        ///</summary>
        [Test]
        public void FeedLinkTest()
        {
            PlaylistsEntry target = new PlaylistsEntry(); // TODO: Initialize to an appropriate value
            string expected =  "secret text string";
            string actual;
            target.Content.Src =  new AtomUri(expected);
            actual = target.Content.Src.Content;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Description
        ///</summary>
        [Test]
        public void DescriptionTest()
        {
            PlaylistsEntry target = new PlaylistsEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Summary.Text = expected;
            actual = target.Summary.Text;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PlaylistsEntry Constructor
        ///</summary>
        [Test]
        public void PlaylistsEntryConstructorTest()
        {
            PlaylistsEntry target = new PlaylistsEntry();
            Assert.IsNotNull(target);
            Assert.IsNull(target.Content.Src);
            Assert.IsNull(target.Summary.Text);
        }
    }
}
