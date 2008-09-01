using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System.Xml;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GDataBatchFeedDataTest and is intended
    ///to contain all GDataBatchFeedDataTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GDataBatchFeedDataTest
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
        ///A test for Type
        ///</summary>
        [Test]
        public void TypeTest()
        {
            GDataBatchFeedData target = new GDataBatchFeedData(); // TODO: Initialize to an appropriate value
            GDataBatchOperationType expected = GDataBatchOperationType.insert;
            GDataBatchOperationType actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for GDataBatchFeedData Constructor
        ///</summary>
        [Test]
        public void GDataBatchFeedDataConstructorTest()
        {
            GDataBatchFeedData target = new GDataBatchFeedData();
            Assert.IsNotNull(target);
            Assert.IsTrue(target.Type == GDataBatchOperationType.Default);
        }
    }
}
