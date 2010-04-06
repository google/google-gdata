using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System.Xml;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GDataBatchStatusTest and is intended
    ///to contain all GDataBatchStatusTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GDataBatchStatusTest
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
        ///A test for Reason
        ///</summary>
        [Test]
        public void ReasonTest()
        {
            GDataBatchStatus target = new GDataBatchStatus(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Reason = expected;
            actual = target.Reason;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentType
        ///</summary>
        [Test]
        public void ContentTypeTest()
        {
            GDataBatchStatus target = new GDataBatchStatus(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.ContentType = expected;
            actual = target.ContentType;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Code
        ///</summary>
        [Test]
        public void CodeTest()
        {
            GDataBatchStatus target = new GDataBatchStatus(); // TODO: Initialize to an appropriate value
            int expected = 27; // TODO: Initialize to an appropriate value
            int actual;
            target.Code = expected;
            actual = target.Code;
            Assert.AreEqual(expected, actual);
        }
    }
}
