using Google.GData.Photos;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;

namespace Google.GData.Client.UnitTests.Picasa
{
    
    
    /// <summary>
    ///This is a test class for AlbumQueryTest and is intended
    ///to contain all AlbumQueryTest Unit Tests
    ///</summary>
    [TestFixture][Category("Picasa")]
    public class AlbumQueryTest
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
        ///A test for AlbumQuery Constructor
        ///</summary>
        [Test]
        public void AlbumQueryConstructorTest1()
        {
            AlbumQuery target = new AlbumQuery();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for AlbumQuery Constructor
        ///</summary>
        [Test]
        public void AlbumQueryConstructorTest()
        {
            string queryUri = "http://www.google.com/test";
            string expected = "http://www.google.com/test?kind=album"; 
            AlbumQuery target = new AlbumQuery(queryUri);
            Assert.AreEqual(new Uri(expected), target.Uri);
        }
    }
}
