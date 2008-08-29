using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomTextConstructTest and is intended
    ///to contain all AtomTextConstructTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomTextConstructTest
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
            AtomTextConstruct target = new AtomTextConstruct(AtomTextConstructElementType.Rights);
            Assert.AreEqual(target.XmlName, AtomParserNameTable.XmlRightsElement);
            target = new AtomTextConstruct(AtomTextConstructElementType.Subtitle);
            Assert.AreEqual(target.XmlName, AtomParserNameTable.XmlSubtitleElement);
            target = new AtomTextConstruct(AtomTextConstructElementType.Title);
            Assert.AreEqual(target.XmlName, AtomParserNameTable.XmlTitleElement);
            target = new AtomTextConstruct(AtomTextConstructElementType.Summary);
            Assert.AreEqual(target.XmlName, AtomParserNameTable.XmlSummaryElement);
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [Test]
        public void TypeTest()
        {
            AtomTextConstruct target = new AtomTextConstruct(); // TODO: Initialize to an appropriate value
            AtomTextConstructType expected = AtomTextConstructType.xhtml;
            AtomTextConstructType actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [Test]
        public void TextTest()
        {
            AtomTextConstruct target = new AtomTextConstruct(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for AtomTextConstruct Constructor
        ///</summary>
        [Test]
        public void AtomTextConstructConstructorTest2()
        {
            AtomTextConstruct target = new AtomTextConstruct();
            Assert.IsNotNull(target);
            Assert.IsTrue(String.IsNullOrEmpty(target.Text)); 
        }

         /// <summary>
        ///A test for AtomTextConstruct Constructor
        ///</summary>
        [Test]
        public void AtomTextConstructConstructorTest()
        {
            string text = "TestValue"; // TODO: Initialize to an appropriate value
            AtomTextConstruct target = new AtomTextConstruct(AtomTextConstructElementType.Summary, text);
            Assert.AreEqual(target.Text, text);
        }
    }
}
