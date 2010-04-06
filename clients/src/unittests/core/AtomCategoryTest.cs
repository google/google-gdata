using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomCategoryTest and is intended
    ///to contain all AtomCategoryTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomCategoryTest
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
            AtomCategory target = new AtomCategory(); // TODO: Initialize to an appropriate value
            Assert.AreEqual(AtomParserNameTable.XmlCategoryElement, target.XmlName);
        }

        /// <summary>
        ///A test for UriString
        ///</summary>
        [Test]
        public void UriStringTest()
        {
            AtomCategory target = new AtomCategory("term", "scheme");
            Assert.AreEqual("{scheme}term", target.UriString);
        }

        /// <summary>
        ///A test for Term
        ///</summary>
        [Test]
        public void TermTest()
        {
            AtomCategory target = new AtomCategory(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Term = expected;
            actual = target.Term;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Scheme
        ///</summary>
        [Test]
        public void SchemeTest()
        {
            AtomCategory target = new AtomCategory(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("scheme");
            AtomUri actual;
            target.Scheme = expected;
            actual = target.Scheme;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Label
        ///</summary>
        [Test]
        public void LabelTest()
        {
            AtomCategory target = new AtomCategory(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Label = expected;
            actual = target.Label;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ShouldBePersisted
        ///</summary>
        [Test]
        public void ShouldBePersistedTest()
        {
            AtomCategory target = new AtomCategory(); // TODO: Initialize to an appropriate value
            target.Label = "new label"; 
            Assert.IsTrue(target.ShouldBePersisted()); 
        }

        
        /// <summary>
        ///A test for AtomCategory Constructor
        ///</summary>
        [Test]
        public void AtomCategoryConstructorTest2()
        {
            AtomCategory target = new AtomCategory();
            Assert.IsNotNull(target);
            Assert.IsTrue(String.IsNullOrEmpty(target.Label));
            Assert.IsTrue(String.IsNullOrEmpty(target.Term));
            Assert.IsNull(target.Scheme);
        }

        /// <summary>
        ///A test for AtomCategory Constructor
        ///</summary>
        [Test]
        public void AtomCategoryConstructorTest1()
        {
            string term = "Test";
            AtomCategory target = new AtomCategory(term);
            Assert.AreEqual(target.Term, term);
        }

        /// <summary>
        ///A test for AtomCategory Constructor
        ///</summary>
        [Test]
        public void AtomCategoryConstructorTest()
        {
            string term = "term";
            AtomUri scheme = new AtomUri("scheme");
            AtomCategory target = new AtomCategory(term, scheme);
            Assert.AreEqual(target.Term, term);
            Assert.AreEqual(target.Scheme.ToString(), "scheme");
        }
    }
}
