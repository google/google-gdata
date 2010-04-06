using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;

    
    

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomIdTest and is intended
    ///to contain all AtomIdTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomIdTest
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
        ///A test for XmlName
        ///</summary>
        [Test]
        public void XmlNameTest()
        {
            AtomId target = new AtomId();
            Assert.AreEqual(AtomParserNameTable.XmlIdElement, target.XmlName);
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [Test]
        public void op_InequalityTest()
        {
            AtomId a = new AtomId("www.a.com");
            AtomId b = new AtomId("www.b.com");
            bool expected = true; 
            bool actual;
            actual = (a != b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [Test]
        public void op_EqualityTest()
        {
            AtomId a = new AtomId("www.a.com");
            AtomId b = new AtomId("www.b.com");
            bool expected = false; 
            bool actual;
            actual = (a == b);
            Assert.AreEqual(expected, actual);
        }

  
        /// <summary>
        ///A test for Equals
        ///</summary>
        [Test]
        public void EqualsTest()
        {
            AtomId a = new AtomId("www.a.com");
            AtomId b = new AtomId("www.b.com");
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = a.Equals(b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [Test]
        public void CompareToTest()
        {
            AtomId a = new AtomId("www.a.com");
            AtomId b = new AtomId("www.a.com");

            int expected = 0;
            int actual;
            actual = a.CompareTo(b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AtomId Constructor
        ///</summary>
        [Test]
        public void AtomIdConstructorTest1()
        {
            AtomId target = new AtomId();
            Assert.IsNotNull(target);
            Assert.IsNull(target.Uri);
        }

        /// <summary>
        ///A test for AtomId Constructor
        ///</summary>
        [Test]
        public void AtomIdConstructorTest()
        {
            string link = "TestValue"; // TODO: Initialize to an appropriate value
            AtomId target = new AtomId(link);
            Assert.AreEqual(link, target.Uri.ToString());
        }
    }
}
