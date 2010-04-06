using Google.GData.Photos;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System;
using Google.GData.Client;

namespace Google.GData.Client.UnitTests.Picasa
{
    
    
    /// <summary>
    ///This is a test class for PicasaFeedTest and is intended
    ///to contain all PicasaFeedTest Unit Tests
    ///</summary>
    [TestFixture][Category("Picasa")]
    public class PicasaFeedTest
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
        ///A test for CreateFeedEntry
        ///</summary>
        [Test]
        public void CreateFeedEntryTest()
        {
            Uri uriBase = new Uri("http://www.google.com"); 
            IService iService = null; // TODO: Initialize to an appropriate value
            PicasaFeed target = new PicasaFeed(uriBase, iService); // TODO: Initialize to an appropriate value
            PicasaEntry expected = null;
            expected = target.CreateFeedEntry() as PicasaEntry;
            Assert.IsNotNull(expected);
        }
    }
}
