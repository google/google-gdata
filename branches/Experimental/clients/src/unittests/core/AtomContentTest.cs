using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomContentTest and is intended
    ///to contain all AtomContentTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomContentTest
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
            AtomContent target = new AtomContent(); // TODO: Initialize to an appropriate value
            Assert.AreEqual(AtomParserNameTable.XmlContentElement, target.XmlName);
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [Test]
        public void TypeTest()
        {
            AtomContent target = new AtomContent(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Src
        ///</summary>
        [Test]
        public void SrcTest()
        {
            AtomContent target = new AtomContent(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/");
            AtomUri actual;
            target.Src = expected;
            actual = target.Src;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Content
        ///</summary>
        [Test]
        public void ContentTest()
        {
            AtomContent target = new AtomContent(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Content = expected;
            actual = target.Content;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AbsoluteUri
        ///</summary>
        [Test]
        public void AbsoluteUriTest()
        {
            AtomContent target = new AtomContent(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/");
            target.Src = expected;
            string actual;
            actual = target.AbsoluteUri;
            Assert.AreEqual(actual, "http://www.test.com/");
        }

        /// <summary>
        ///A test for AtomContent Constructor
        ///</summary>
        [Test]
        public void AtomContentConstructorTest()
        {
            AtomContent target = new AtomContent();
            Assert.IsNotNull(target);
            Assert.IsNull(target.Content);
            Assert.IsNull(target.Src);
            Assert.IsTrue(target.Type == "text");
        }
    }
}
