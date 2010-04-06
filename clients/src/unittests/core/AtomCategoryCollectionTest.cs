using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;

    
    

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomCategoryCollectionTest and is intended
    ///to contain all AtomCategoryCollectionTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomCategoryCollectionTest
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
        ///A test for Item
        ///</summary>
        [Test]
        public void ItemTest()
        {
            AtomCategoryCollection target = new AtomCategoryCollection();
            int index = 0; 
            AtomCategory expected = new AtomCategory("test");
            AtomCategory actual;
            target.Add(expected);
            target[index] = expected;
            actual = target[index];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [Test]
        public void RemoveTest()
        {
            AtomCategoryCollection target = new AtomCategoryCollection(); // TODO: Initialize to an appropriate value
            AtomCategory expected = new AtomCategory("test");

            Assert.IsTrue(target.Count == 0);
            target.Add(expected);
            Assert.IsTrue(target.Count == 1);
            target.Remove(expected);
            Assert.IsTrue(target.Count == 0);
        }


        /// <summary>
        ///A test for Insert
        ///</summary>
        [Test]
        public void InsertTest()
        {
            AtomCategoryCollection target = new AtomCategoryCollection(); // TODO: Initialize to an appropriate value
            AtomCategory value = new AtomCategory("test");
            int index = 0; 
            Assert.IsTrue(target.Count == 0);
            target.Insert(index, value);
            Assert.IsTrue(target.Count == 1);
        }

        /// <summary>
        ///A test for IndexOf
        ///</summary>
        [Test]
        public void IndexOfTest()
        {
            AtomCategoryCollection target = new AtomCategoryCollection(); // TODO: Initialize to an appropriate value
            AtomCategory value = new AtomCategory("test");

            target.Add(value);
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.IndexOf(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Find
        ///</summary>
        [Test]
        public void FindTest1()
        {
            AtomCategoryCollection target = new AtomCategoryCollection(); // TODO: Initialize to an appropriate value
            AtomCategory value = new AtomCategory("test", "scheme");
            target.Add(value);
            string term = "test";
            AtomUri scheme = new AtomUri("scheme");
            AtomCategory actual;
            actual = target.Find(term, scheme);
            Assert.AreEqual(value, actual);
        }

        /// <summary>
        ///A test for Find
        ///</summary>
        [Test]
        public void FindTest()
        {
            AtomCategoryCollection target = new AtomCategoryCollection(); // TODO: Initialize to an appropriate value
            AtomCategory value = new AtomCategory("test");
            target.Add(value);
            string term = "test";
            AtomCategory actual;
            actual = target.Find(term);
            Assert.AreEqual(value, actual);
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        [Test]
        public void ContainsTest()
        {
            AtomCategoryCollection target = new AtomCategoryCollection(); // TODO: Initialize to an appropriate value
            AtomCategory value = new AtomCategory("test");
            target.Add(value);
            Assert.IsTrue(target.Contains(value));
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [Test]
        public void AddTest()
        {
            AtomCategoryCollection target = new AtomCategoryCollection(); // TODO: Initialize to an appropriate value
            AtomCategory value = new AtomCategory("test");
            target.Add(value);
            Assert.IsTrue(target.Contains(value)); 
        }
    }
}
