using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for QueryCategoryTest and is intended
    ///to contain all QueryCategoryTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class QueryCategoryTest
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
        ///A test for Operator
        ///</summary>
        [Test]
        public void OperatorTest()
        {
            AtomCategory category = new AtomCategory("term");
            QueryCategory target = new QueryCategory(category); // TODO: Initialize to an appropriate value
            QueryCategoryOperator expected = QueryCategoryOperator.AND;
            QueryCategoryOperator actual;
            target.Operator = expected;
            actual = target.Operator;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Excluded
        ///</summary>
        [Test]
        public void ExcludedTest()
        {
            AtomCategory category = new AtomCategory("term");
            QueryCategory target = new QueryCategory(category); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.Excluded = expected;
            actual = target.Excluded;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Category
        ///</summary>
        [Test]
        public void CategoryTest()
        {
            AtomCategory category = new AtomCategory("term");
            QueryCategory target = new QueryCategory(category); // TODO: Initialize to an appropriate value
            AtomCategory actual;
            target.Category = category;
            actual = target.Category;
            Assert.AreEqual(category, actual);
        }

        /// <summary>
        ///A test for QueryCategory Constructor
        ///</summary>
        [Test]
        public void QueryCategoryConstructorTest1()
        {
            AtomCategory category = new AtomCategory("term");
            QueryCategory target = new QueryCategory(category);
            Assert.IsNotNull(target);
            Assert.AreEqual(target.Category, category);
        }

        /// <summary>
        ///A test for QueryCategory Constructor
        ///</summary>
        [Test]
        public void QueryCategoryConstructorTest()
        {
            string strCategory = "TestValue"; // TODO: Initialize to an appropriate value
            QueryCategoryOperator op = QueryCategoryOperator.OR;
            QueryCategory target = new QueryCategory(strCategory, op);
            Assert.IsNotNull(target);
            Assert.AreEqual(target.Operator, op);
        }
    }
}
