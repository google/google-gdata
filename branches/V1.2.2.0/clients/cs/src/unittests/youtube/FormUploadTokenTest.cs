using Google.GData.YouTube;
using NUnit.Framework;
using System.IO;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for FormUploadTokenTest and is intended
    ///to contain all FormUploadTokenTest Unit Tests
    ///</summary>
    [TestFixture][Category("YouTube")]
    public class FormUploadTokenTest
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
        ///A test for Url
        ///</summary>
        [Test]
        [Ignore("not yet written")]
        public void UrlTest()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            FormUploadToken target = new FormUploadToken(stream); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Url = expected;
            actual = target.Url;
            Assert.AreEqual(expected, actual);
            Assert.Fail("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Token
        ///</summary>
        [Test]
        [Ignore("not yet written")]
        public void TokenTest()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            FormUploadToken target = new FormUploadToken(stream); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Token = expected;
            actual = target.Token;
            Assert.AreEqual(expected, actual);
            Assert.Fail("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FormUploadToken Constructor
        ///</summary>
        [Test]
        public void FormUploadTokenConstructorTest1()
        {
            string url ="http://www.google.com";
            string token = "token";
            FormUploadToken target = new FormUploadToken(url, token);
            Assert.AreEqual(target.Token, token, "token better be the same");
            Assert.AreEqual(target.Url, url, "Url better be the same");
        }

        /// <summary>
        ///A test for FormUploadToken Constructor
        ///</summary>
        [Test]
        [Ignore("not yet written")]
        public void FormUploadTokenConstructorTest()
        {
            Stream stream = null; // TODO: Initialize to an appropriate value
            FormUploadToken target = new FormUploadToken(stream);
            Assert.Fail("TODO: Implement code to verify target");
        }
    }
}
