using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomUriTest and is intended
    ///to contain all AtomUriTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomUriTest
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
        ///A test for Content
        ///</summary>
        [Test]
        public void ContentTest()
        {
            Uri uri = new Uri("http://www.test.com/");
            AtomUri target = new AtomUri(uri); // TODO: Initialize to an appropriate value
            string expected = "http://www.test.com/";            
            string actual;
            target.Content = expected;
            actual = target.Content;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [Test]
        public void ToStringTest()
        {
            Uri uri = new Uri("http://www.test.com/");
            AtomUri target = new AtomUri(uri); // TODO: Initialize to an appropriate value
            string expected = "http://www.test.com/";            
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_LessThanOrEqual
        ///</summary>
        [Test]
        public void op_LessThanOrEqualTest()
        {
            AtomUri a = new AtomUri("A");
            AtomUri b = new AtomUri("A");
            bool expected = true;
            bool actual;
            actual = (a <= b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_LessThan
        ///</summary>
        [Test]
        public void op_LessThanTest()
        {
            AtomUri a = new AtomUri("A");
            AtomUri b = new AtomUri("A");
            bool expected = false; 
            bool actual;
            actual = (a < b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [Test]
        public void op_InequalityTest()
        {
            AtomUri a = new AtomUri("A");
            AtomUri b = new AtomUri("B");
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (a != b);
            Assert.AreEqual(expected, actual);
        }
    }
}
