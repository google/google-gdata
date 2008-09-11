using Google.GData.Photos;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;

namespace Google.GData.Client.UnitTests.Picasa
{
    
    
    /// <summary>
    ///This is a test class for PicasaQueryTest and is intended
    ///to contain all PicasaQueryTest Unit Tests
    ///</summary>
    [TestFixture][Category("Picasa")]
    public class PicasaQueryTest
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
        ///A test for Thumbsize
        ///</summary>
        [Test]
        public void ThumbsizeTest()
        {
            PicasaQuery target = new PicasaQuery(); // TODO: Initialize to an appropriate value
            string expected = "TestValue"; 
            string actual;
            target.Thumbsize = expected;
            actual = target.Thumbsize;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Tags
        ///</summary>
        [Test]
        public void TagsTest()
        {
            PicasaQuery target = new PicasaQuery(); // TODO: Initialize to an appropriate value
            string expected = "TestValue"; 
            string actual;
            target.Tags = expected;
            actual = target.Tags;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for KindParameter
        ///</summary>
        [Test]
        public void KindParameterTest()
        {
            PicasaQuery target = new PicasaQuery(); // TODO: Initialize to an appropriate value
            string expected = "TestValue"; 
            string actual;
            target.KindParameter = expected;
            actual = target.KindParameter;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Access
        ///</summary>
        [Test]
        public void AccessTest()
        {
            PicasaQuery target = new PicasaQuery(); // TODO: Initialize to an appropriate value
            PicasaQuery.AccessLevel expected = PicasaQuery.AccessLevel.AccessPrivate;
            PicasaQuery.AccessLevel actual;
            target.Access = expected;
            actual = target.Access;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CreatePicasaUri
        ///</summary>
        [Test]
        public void CreatePicasaUriTest2()
        {
            string userID = "john.smith@test.com";
            Uri test = null; 
            try
            {
                test = new Uri(PicasaQuery.CreatePicasaUri(userID));
            }
            catch (Exception e)
            {
            }

            Assert.IsNotNull(test);
        }

        /// <summary>
        ///A test for CreatePicasaUri
        ///</summary>
        [Test]
        public void CreatePicasaUriTest1()
        {
            string userID = "john.smith@test.com";
            string albumName = "album";
            string photoID = "photoid";
            Uri test = null;
            try
            {
                test = new Uri(PicasaQuery.CreatePicasaUri(userID, albumName, photoID));
            }
            catch (Exception e)
            {
            }

            Assert.IsNotNull(test);
        }

        /// <summary>
        ///A test for CreatePicasaUri
        ///</summary>
        [Test]
        public void CreatePicasaUriTest()
        {
            string userID = "john.smith@test.com";
            string albumName = "album";
            Uri test = null;
            try
            {
                test = new Uri(PicasaQuery.CreatePicasaUri(userID, albumName));
            }
            catch (Exception e)
            {
            }

            Assert.IsNotNull(test);
        }


        /// <summary>
        ///A test for PicasaQuery Constructor
        ///</summary>
        [Test]
        public void PicasaQueryConstructorTest1()
        {
            PicasaQuery target = new PicasaQuery();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for PicasaQuery Constructor
        ///</summary>
        [Test]
        public void PicasaQueryConstructorTest()
        {
            string queryUri = "http://www.google.com/test";
            string expected = "http://www.google.com/test?kind=tag"; 

            PicasaQuery target = new PicasaQuery(queryUri);
            Assert.AreEqual(new Uri(expected), target.Uri);
        }
    }
}
