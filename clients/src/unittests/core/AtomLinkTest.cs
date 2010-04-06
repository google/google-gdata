using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;
    
namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomLinkTest and is intended
    ///to contain all AtomLinkTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomLinkTest
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
            AtomLink target = new AtomLink(); // TODO: Initialize to an appropriate value
            Assert.AreEqual(target.XmlName, AtomParserNameTable.XmlLinkElement);
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [Test]
        public void TypeTest()
        {
            AtomLink target = new AtomLink(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        [Test]
        public void TitleTest()
        {
            AtomLink target = new AtomLink(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Title = expected;
            actual = target.Title;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Rel
        ///</summary>
        [Test]
        public void RelTest()
        {
            AtomLink target = new AtomLink(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Rel = expected;
            actual = target.Rel;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Length
        ///</summary>
        [Test]
        public void LengthTest()
        {
            AtomLink target = new AtomLink(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.Length = expected;
            actual = target.Length;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for HRefLang
        ///</summary>
        [Test]
        public void HRefLangTest()
        {
            AtomLink target = new AtomLink(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.HRefLang = expected;
            actual = target.HRefLang;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for HRef
        ///</summary>
        [Test]
        public void HRefTest()
        {
            AtomLink target = new AtomLink(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/");
            AtomUri actual;
            target.HRef = expected;
            actual = target.HRef;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AbsoluteUri
        ///</summary>
        [Test]
        public void AbsoluteUriTest()
        {
            AtomLink target = new AtomLink(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/");
            target.HRef = expected;
            Assert.AreEqual(target.AbsoluteUri, "http://www.test.com/");
        }


        /// <summary>
        ///A test for AtomLink Constructor
        ///</summary>
        [Test]
        public void AtomLinkConstructorTest2()
        {
            AtomLink target = new AtomLink();
            Assert.IsNotNull(target);
            Assert.IsNull(target.AbsoluteUri);
        }

        /// <summary>
        ///A test for AtomLink Constructor
        ///</summary>
        [Test]
        public void AtomLinkConstructorTest1()
        {
            string link = "http://www.test.com/";
            AtomLink target = new AtomLink(link);
            Assert.AreEqual(target.AbsoluteUri, link);
        }

        /// <summary>
        ///A test for AtomLink Constructor
        ///</summary>
        [Test]
        public void AtomLinkConstructorTest()
        {
            string type = "TestValue"; // TODO: Initialize to an appropriate value
            string rel = "TestValue"; // TODO: Initialize to an appropriate value
            AtomLink target = new AtomLink(type, rel);
            Assert.AreEqual(target.Type, type);
            Assert.AreEqual(target.Rel, rel);

        }
    }
}
