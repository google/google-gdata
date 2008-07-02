using NUnit.Framework;
using Google.GData.Client.UnitTests;
using Google.GData.YouTube;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for CommentEntryTest and is intended
    ///to contain all CommentEntryTest Unit Tests
    ///</summary>
    [TestFixture][Category("YouTube")]
    public class CommentEntryTest
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
        ///A test for CommentEntry Constructor
        ///</summary>
        [Test]
        public void CommentEntryConstructorTest()
        {
            CommentEntry target = new CommentEntry();
            Assert.IsNotNull(target,"object should not be null"); 
        }

         /// <summary>
        ///A test for replyto and replies properties
        ///</summary>
        [Test]
        public void CommentEntryReplyToTest()
        {
            CommentEntry target = new CommentEntry();
            Assert.AreEqual(target.Replies.Count, 0," list should be emptyl"); 
            CommentEntry other = new CommentEntry();
            other.SelfUri = new AtomUri("http://www.test.org");
            target.ReplyTo(other);
            Assert.AreEqual(target.Replies.Count, 1," list should be emptyl"); 


        }
    }
}
