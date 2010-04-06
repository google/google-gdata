using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System.Xml;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for ExtensionElementEventArgsTest and is intended
    ///to contain all ExtensionElementEventArgsTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class ExtensionElementEventArgsTest
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
        ///A test for ExtensionElement
        ///</summary>
        [Test]
        public void ExtensionElementTest()
        {
            ExtensionElementEventArgs target = new ExtensionElementEventArgs(); // TODO: Initialize to an appropriate value
            XmlNode expected = null; // TODO: Initialize to an appropriate value
            XmlNode actual;
            target.ExtensionElement = expected;
            actual = target.ExtensionElement;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DiscardEntry
        ///</summary>
        [Test]
        public void DiscardEntryTest()
        {
            ExtensionElementEventArgs target = new ExtensionElementEventArgs(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.DiscardEntry = expected;
            actual = target.DiscardEntry;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Base
        ///</summary>
        [Test]
        public void BaseTest()
        {
            ExtensionElementEventArgs target = new ExtensionElementEventArgs(); // TODO: Initialize to an appropriate value
            AtomBase expected = new AtomEntry();
            AtomBase actual;
            target.Base = expected;
            actual = target.Base;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ExtensionElementEventArgs Constructor
        ///</summary>
        [Test]
        public void ExtensionElementEventArgsConstructorTest()
        {
            ExtensionElementEventArgs target = new ExtensionElementEventArgs();
            Assert.IsNotNull(target);
            Assert.IsNull(target.Base);
            Assert.IsNull(target.ExtensionElement);
            Assert.IsFalse(target.DiscardEntry);
         }
    }
}
