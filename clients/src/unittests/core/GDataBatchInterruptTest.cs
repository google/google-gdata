using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System.Xml;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GDataBatchInterruptTest and is intended
    ///to contain all GDataBatchInterruptTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GDataBatchInterruptTest
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
        ///A test for Unprocessed
        ///</summary>
        [Test]
        public void UnprocessedTest()
        {
            GDataBatchInterrupt target = new GDataBatchInterrupt(); // TODO: Initialize to an appropriate value
            int expected = 5; // TODO: Initialize to an appropriate value
            int actual;
            target.Unprocessed = expected;
            actual = target.Unprocessed;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Successes
        ///</summary>
        [Test]
        public void SuccessesTest()
        {
            GDataBatchInterrupt target = new GDataBatchInterrupt(); // TODO: Initialize to an appropriate value
            int expected = 6; // TODO: Initialize to an appropriate value
            int actual;
            target.Successes = expected;
            actual = target.Successes;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Reason
        ///</summary>
        [Test]
        public void ReasonTest()
        {
            GDataBatchInterrupt target = new GDataBatchInterrupt(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Reason = expected;
            actual = target.Reason;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parsed
        ///</summary>
        [Test]
        public void ParsedTest()
        {
            GDataBatchInterrupt target = new GDataBatchInterrupt(); // TODO: Initialize to an appropriate value
            int expected = 7; // TODO: Initialize to an appropriate value
            int actual;
            target.Parsed = expected;
            actual = target.Parsed;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Failures
        ///</summary>
        [Test]
        public void FailuresTest()
        {
            GDataBatchInterrupt target = new GDataBatchInterrupt(); // TODO: Initialize to an appropriate value
            int expected = 9; // TODO: Initialize to an appropriate value
            int actual;
            target.Failures = expected;
            actual = target.Failures;
            Assert.AreEqual(expected, actual);
        }
    }
}
