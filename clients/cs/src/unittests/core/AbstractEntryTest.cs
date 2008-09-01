using Google.GData.Client;
using Google.GData.Calendar;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System.Xml;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AbstractEntryTest and is intended
    ///to contain all AbstractEntryTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AbstractEntryTest
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
        ///A test for ToggleCategory
        ///</summary>
        [Test]
        public void ToggleCategoryTest()
        {
            AbstractEntry target = CreateAbstractEntry(); 
            AtomCategory cat = new AtomCategory("testcat");

            target.ToggleCategory(cat, true);
            Assert.IsTrue(target.Categories.Contains(cat), "Category should now be part of it");
            target.ToggleCategory(cat, false);
            Assert.IsFalse(target.Categories.Contains(cat), "Category should be gone");
        }


        internal virtual AbstractEntry CreateAbstractEntry()
        {
            return new EventEntry();
        }

    }
}
