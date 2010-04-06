using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AccountDeletedExceptionTest and is intended
    ///to contain all AccountDeletedExceptionTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AccountDeletedExceptionTest
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
        ///A test for AccountDeletedException Constructor
        ///</summary>
        [Test]
        public void AccountDeletedExceptionConstructorTest1()
        {
            AccountDeletedException target = new AccountDeletedException();
            Assert.IsNotNull(target); 
        }

        /// <summary>
        ///A test for AccountDeletedException Constructor
        ///</summary>
        [Test]
        public void AccountDeletedExceptionConstructorTest()
        {
            string msg ="Something"; 
            AccountDeletedException target = new AccountDeletedException(msg);
            Assert.IsNotNull(target); 
            Assert.AreEqual(target.Message , msg); 
        }
    }
}
