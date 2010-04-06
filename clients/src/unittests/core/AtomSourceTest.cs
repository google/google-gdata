using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System.Xml;
using System;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomSourceTest and is intended
    ///to contain all AtomSourceTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomSourceTest
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
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            Assert.AreEqual(target.XmlName,  AtomParserNameTable.XmlSourceElement);
        }

        /// <summary>
        ///A test for Updated
        ///</summary>
        [Test]
        public void UpdatedTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            DateTime expected = DateTime.Now;
            DateTime actual;
            target.Updated = expected;
            actual = target.Updated;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        [Test]
        public void TitleTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            target.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, "test");
            Assert.AreEqual(target.Title.Text, "test"); 
        }

        /// <summary>
        ///A test for Subtitle
        ///</summary>
        [Test]
        public void SubtitleTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            target.Subtitle = new AtomTextConstruct(AtomTextConstructElementType.Subtitle, "test");
            Assert.AreEqual(target.Subtitle.Text, "test"); 
        }

        /// <summary>
        ///A test for Rights
        ///</summary>
        [Test]
        public void RightsTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            target.Rights = new AtomTextConstruct(AtomTextConstructElementType.Rights, "test");
            Assert.AreEqual(target.Rights.Text, "test"); 
        }

        /// <summary>
        ///A test for Logo
        ///</summary>
        [Test]
        public void LogoTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            AtomLogo expected = new AtomLogo();
            AtomLogo actual;
            target.Logo = expected;
            actual = target.Logo;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Links
        ///</summary>
        [Test]
        public void LinksTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.Links);
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [Test]
        public void IdTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            AtomId expected = new AtomId();
            AtomId actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Icon
        ///</summary>
        [Test]
        public void IconTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            AtomIcon expected = new AtomIcon();
            AtomIcon actual;
            target.Icon = expected;
            actual = target.Icon;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Generator
        ///</summary>
        [Test]
        public void GeneratorTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            AtomGenerator expected = new AtomGenerator();
            AtomGenerator actual;
            target.Generator = expected;
            actual = target.Generator;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Contributors
        ///</summary>
        [Test]
        public void ContributorsTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.Contributors);
        }

        /// <summary>
        ///A test for Categories
        ///</summary>
        [Test]
        public void CategoriesTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.Categories);
        }

        /// <summary>
        ///A test for Authors
        ///</summary>
        [Test]
        public void AuthorsTest()
        {
            AtomSource target = new AtomSource(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.Authors);
        }


        /// <summary>
        ///A test for AtomSource Constructor
        ///</summary>
        [Test]
        public void AtomSourceConstructorTest1()
        {
            AtomSource target = new AtomSource();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for AtomSource Constructor
        ///</summary>
        [Test]
        public void AtomSourceConstructorTest()
        {
            AtomFeed feed = new AtomFeed(new Uri("http://www.test.com/"), null);
            feed.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, "Title");
            AtomSource target = new AtomSource(feed);
            Assert.AreEqual(feed.Title.Text, target.Title.Text);
        }
    }
}
