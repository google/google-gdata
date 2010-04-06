using Google.GData.Photos;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
namespace Google.GData.Client.UnitTests.Picasa
{
    
    
    /// <summary>
    ///This is a test class for TagAccessorTest and is intended
    ///to contain all TagAccessorTest Unit Tests
    ///</summary>
    [TestFixture][Category("Picasa")]
    public class TagAccessorTest
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
        ///A test for Weight
        ///</summary>
        [Test]
        public void WeightTest()
        {
            PicasaEntry entry = new PicasaEntry();
            entry.IsTag = true;
            TagAccessor target = new TagAccessor(entry); // TODO: Initialize to an appropriate value
            uint expected = 5; // TODO: Initialize to an appropriate value
            uint actual;
            target.Weight = expected;
            actual = target.Weight;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TagAccessor Constructor
        ///</summary>
        [Test]
        public void TagAccessorConstructorTest()
        {
            PicasaEntry entry = new PicasaEntry();
            entry.IsTag = true;
            TagAccessor target = new TagAccessor(entry);
            Assert.IsNotNull(target);
        }
    }
}
