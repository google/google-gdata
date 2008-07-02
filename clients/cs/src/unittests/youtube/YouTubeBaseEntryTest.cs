using Google.GData.YouTube;
using NUnit.Framework;
using Google.GData.Extensions;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for YouTubeBaseEntryTest and is intended
    ///to contain all YouTubeBaseEntryTest Unit Tests
    ///</summary>
    [TestFixture][Category("YouTube")]
    public class YouTubeBaseEntryTest
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
        ///A test for setYouTubeExtension
        ///</summary>
        [Test]
        public void setYouTubeExtensionTest()
        {
            YouTubeBaseEntry target = CreateYouTubeBaseEntry(); // TODO: Initialize to an appropriate value
            string newValue = "3";
            string  actual;
            target.setYouTubeExtension(YouTubeNameTable.Location, newValue);
            actual = target.getYouTubeExtensionValue(YouTubeNameTable.Location);
            Assert.AreEqual(newValue, actual);
        }

        
        internal virtual YouTubeBaseEntry CreateYouTubeBaseEntry()
        {
            // TODO: Instantiate an appropriate concrete class.
            YouTubeBaseEntry target = new YouTubeEntry();
            return target;
        }

        /// <summary>
        ///A test for getYouTubeExtension
        ///</summary>
        [Test]
        public void getYouTubeExtensionTest()
        {
            YouTubeBaseEntry target = CreateYouTubeBaseEntry(); // TODO: Initialize to an appropriate value
            SimpleElement age = new Age();
            SimpleElement actual ;
            
            target.ExtensionElements.Add(age);
            actual = target.getYouTubeExtension(YouTubeNameTable.Age);
            Assert.AreEqual(age, actual);
        }
    }
}
