using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System.Xml;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GDataBatchEntryDataTest and is intended
    ///to contain all GDataBatchEntryDataTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GDataBatchEntryDataTest
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
            GDataBatchEntryData target = new GDataBatchEntryData(); // TODO: Initialize to an appropriate value
            GDataBatchOperationType expected = GDataBatchOperationType.update;
            GDataBatchOperationType actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Status
        ///</summary>
        [Test]
        public void StatusTest()
        {
            GDataBatchEntryData target = new GDataBatchEntryData(); // TODO: Initialize to an appropriate value
            GDataBatchStatus expected = new GDataBatchStatus();
            GDataBatchStatus actual;
            target.Status = expected;
            actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Interrupt
        ///</summary>
        [Test]
        public void InterruptTest()
        {
            GDataBatchEntryData target = new GDataBatchEntryData(); // TODO: Initialize to an appropriate value
            GDataBatchInterrupt expected = new GDataBatchInterrupt();
            GDataBatchInterrupt actual;
            target.Interrupt = expected;
            actual = target.Interrupt;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [Test]
        public void IdTest()
        {
            GDataBatchEntryData target = new GDataBatchEntryData(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for GDataBatchEntryData Constructor
        ///</summary>
        [Test]
        public void GDataBatchEntryDataConstructorTest2()
        {
            GDataBatchEntryData target = new GDataBatchEntryData();
            Assert.IsNotNull(target);
            Assert.AreEqual(GDataBatchOperationType.Default, target.Type);
        }

        /// <summary>
        ///A test for GDataBatchEntryData Constructor
        ///</summary>
        [Test]
        public void GDataBatchEntryDataConstructorTest1()
        {
            GDataBatchOperationType type = GDataBatchOperationType.update;
            GDataBatchEntryData target = new GDataBatchEntryData(type);
            Assert.IsNotNull(target);
            Assert.AreEqual(type, target.Type);
        }

        /// <summary>
        ///A test for GDataBatchEntryData Constructor
        ///</summary>
        [Test]
        public void GDataBatchEntryDataConstructorTest()
        {
            string ID = "TestValue"; // TODO: Initialize to an appropriate value
            GDataBatchOperationType type = GDataBatchOperationType.update;
            GDataBatchEntryData target = new GDataBatchEntryData(ID, type);
            Assert.IsNotNull(target);
            Assert.AreEqual(type, target.Type);
            Assert.AreEqual(ID, target.Id);
        }
    }
}
