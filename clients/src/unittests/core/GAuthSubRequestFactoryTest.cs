using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

using System;
using System.Security.Cryptography;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for GAuthSubRequestFactoryTest and is intended
    ///to contain all GAuthSubRequestFactoryTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class GAuthSubRequestFactoryTest
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
        ///A test for Token
        ///</summary>
        [Test]
        public void TokenTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GAuthSubRequestFactory target = new GAuthSubRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Token = expected;
            actual = target.Token;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PrivateKey
        ///</summary>
        [Test]
        public void PrivateKeyTest()
        {
            string service = "TestValue"; // TODO: Initialize to an appropriate value
            string applicationName = "TestValue"; // TODO: Initialize to an appropriate value
            GAuthSubRequestFactory target = new GAuthSubRequestFactory(service, applicationName); // TODO: Initialize to an appropriate value
            AsymmetricAlgorithm expected = new DSACryptoServiceProvider();
            AsymmetricAlgorithm actual;
            target.PrivateKey = expected;
            actual = target.PrivateKey;
            Assert.AreEqual(expected, actual);
        }
    }
}
