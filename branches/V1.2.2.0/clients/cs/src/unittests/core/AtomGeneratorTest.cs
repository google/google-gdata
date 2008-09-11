using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomGeneratorTest and is intended
    ///to contain all AtomGeneratorTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomGeneratorTest
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
            AtomGenerator target = new AtomGenerator(); // TODO: Initialize to an appropriate value
            Assert.AreEqual(AtomParserNameTable.XmlGeneratorElement, target.XmlName);
        }

        /// <summary>
        ///A test for Version
        ///</summary>
        [Test]
        public void VersionTest()
        {
            AtomGenerator target = new AtomGenerator(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Version = expected;
            actual = target.Version;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Uri
        ///</summary>
        [Test]
        public void UriTest()
        {
            AtomGenerator target = new AtomGenerator(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/");
            AtomUri actual;
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [Test]
        public void TextTest()
        {
            AtomGenerator target = new AtomGenerator(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ShouldBePersisted
        ///</summary>
        [Test]
        public void ShouldBePersistedTest()
        {
            AtomGenerator target = new AtomGenerator(); // TODO: Initialize to an appropriate value
            target.Text = "dirty";
            Assert.IsTrue(target.ShouldBePersisted());
        }


        /// <summary>
        ///A test for AtomGenerator Constructor
        ///</summary>
        [Test]
        public void AtomGeneratorConstructorTest1()
        {
            AtomGenerator target = new AtomGenerator();
            Assert.IsNotNull(target);
            Assert.IsTrue(String.IsNullOrEmpty(target.Text));
        }

        /// <summary>
        ///A test for AtomGenerator Constructor
        ///</summary>
        [Test]
        public void AtomGeneratorConstructorTest()
        {
            string text = "test";
            AtomGenerator target = new AtomGenerator(text);
            Assert.AreEqual(text, target.Text);
        }
    }
}
