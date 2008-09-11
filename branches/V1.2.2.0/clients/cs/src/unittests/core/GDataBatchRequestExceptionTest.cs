using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System;
using System.Runtime.Serialization;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GDataBatchRequestExceptionTest and is intended
    ///to contain all GDataBatchRequestExceptionTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GDataBatchRequestExceptionTest
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
        ///A test for BatchResult
        ///</summary>
        [Test]
        public void BatchResultTest()
        {
            GDataBatchRequestException target = new GDataBatchRequestException(); // TODO: Initialize to an appropriate value
            AtomFeed actual;
            actual = target.BatchResult;
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GDataBatchRequestException Constructor
        ///</summary>
        [Test]
        public void GDataBatchRequestExceptionConstructorTest4()
        {
            AtomFeed batchResult = null; // TODO: Initialize to an appropriate value
            GDataBatchRequestException target = new GDataBatchRequestException(batchResult);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GDataBatchRequestException Constructor
        ///</summary>
        [Test]
        public void GDataBatchRequestExceptionConstructorTest3()
        {
            GDataBatchRequestException target = new GDataBatchRequestException();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GDataBatchRequestException Constructor
        ///</summary>
        [Test]
        public void GDataBatchRequestExceptionConstructorTest2()
        {
            string msg = "TestValue"; // TODO: Initialize to an appropriate value
            GDataBatchRequestException target = new GDataBatchRequestException(msg);
            Assert.IsNotNull(target);
            Assert.AreEqual(msg, target.Message);
        }

        /// <summary>
        ///A test for GDataBatchRequestException Constructor
        ///</summary>
        [Test]
        public void GDataBatchRequestExceptionConstructorTest()
        {
            string msg = "TestValue"; // TODO: Initialize to an appropriate value
            Exception innerException = new Exception("Test");
            GDataBatchRequestException target = new GDataBatchRequestException(msg, innerException);
            Assert.IsNotNull(target);
            Assert.AreEqual(msg, target.Message);
            Assert.AreEqual(innerException, target.InnerException);
        }

    }
}
