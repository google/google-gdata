using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System.Xml;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomBaseLinkTest and is intended
    ///to contain all AtomBaseLinkTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomBaseLinkTest
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
        ///A test for Uri
        ///</summary>
        [Test]
        public void UriTest()
        {
            AtomBaseLink target = CreateAtomBaseLink(); 
            AtomUri expected = new AtomUri("http://www.test.com/");
            AtomUri actual;
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AbsoluteUri
        ///</summary>
        [Test]
        public void AbsoluteUriTest()
        {
            AtomBaseLink target = CreateAtomBaseLink(); // TODO: Initialize to an appropriate value
            AtomUri actual = new AtomUri(target.AbsoluteUri);
            Assert.AreEqual(target.Uri, actual);
        }

        internal virtual AtomBaseLink CreateAtomBaseLink()
        {
            return new AtomId("http://www.test.com/");
        }

        /// <summary>
        ///A test for ShouldBePersisted
        ///</summary>
        [Test]
        public void ShouldBePersistedTest()
        {
            AtomBaseLink target = CreateAtomBaseLink(); // TODO: Initialize to an appropriate value
            Assert.IsTrue(target.ShouldBePersisted());
        }
   }
}
