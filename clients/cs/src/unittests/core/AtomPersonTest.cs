using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System.Xml;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomPersonTest and is intended
    ///to contain all AtomPersonTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomPersonTest
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
            AtomPerson target = new AtomPerson(); 
            Assert.AreEqual(target.XmlName, AtomParserNameTable.XmlAuthorElement);

            target = new AtomPerson(AtomPersonType.Contributor);
            Assert.AreEqual(target.XmlName, AtomParserNameTable.XmlContributorElement);
            
        }

        /// <summary>
        ///A test for Uri
        ///</summary>
        [Test]
        public void UriTest()
        {
            AtomPerson target = new AtomPerson(); // TODO: Initialize to an appropriate value
            AtomUri expected = new AtomUri("http://www.test.com/");
            AtomUri actual;
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [Test]
        public void NameTest()
        {
            AtomPerson target = new AtomPerson(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Email
        ///</summary>
        [Test]
        public void EmailTest()
        {
            AtomPerson target = new AtomPerson(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Email = expected;
            actual = target.Email;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for AtomPerson Constructor
        ///</summary>
        [Test]
        public void AtomPersonConstructorTest()
        {
            string name = "TestValue"; 
            AtomPerson target = new AtomPerson(AtomPersonType.Contributor, name);
            Assert.AreEqual(target.Name, name);
        }
    }
}
