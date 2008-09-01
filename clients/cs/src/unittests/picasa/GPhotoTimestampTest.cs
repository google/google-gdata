using Google.GData.Photos;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;

namespace Google.GData.Client.UnitTests.Picasa
{
    
    
    /// <summary>
    ///This is a test class for GPhotoTimestampTest and is intended
    ///to contain all GPhotoTimestampTest Unit Tests
    ///</summary>
    [TestFixture][Category("Picasa")]
    public class GPhotoTimestampTest
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
        ///A test for GPhotoTimestamp Constructor
        ///</summary>
        [Test]
        public void GPhotoTimestampConstructorTest1()
        {
            GPhotoTimestamp target = new GPhotoTimestamp();
            Assert.IsNotNull(target);
            Assert.IsTrue(String.IsNullOrEmpty(target.Value));
        }

        /// <summary>
        ///A test for GPhotoTimestamp Constructor
        ///</summary>
        [Test]
        public void GPhotoTimestampConstructorTest()
        {
            string initValue = "TestValue"; 
            GPhotoTimestamp target = new GPhotoTimestamp(initValue);
            Assert.AreEqual(initValue, target.Value);

        }
    }
}
