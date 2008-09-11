using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System.Globalization;
using System.Xml;
using System.Text;
using System;
using System.IO;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for UtilitiesTest and is intended
    ///to contain all UtilitiesTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class UtilitiesTest
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
        ///A test for EmptyDate
        ///</summary>
        [Test]
        public void EmptyDateTest()
        {
            DateTime actual;
            DateTime expected = new DateTime(1,1,1);
            actual = Utilities.EmptyDate;
            Assert.AreEqual(expected, actual);
        }

      
        /// <summary>
        ///A test for ParseValueFormStream
        ///</summary>
        [Test]
        public void ParseValueFormStreamTest()
        {
            String inputString = "Key=TestValue";
            Stream inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString));
            string expected = "TestValue";            string actual;
            actual = Utilities.ParseValueFormStream(inputStream, "Key");
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for LocalDateTimeInUTC
        ///</summary>
        [Test]
        public void LocalDateTimeInUTCTest()
        {
            DateTime dateTime = new DateTime(2008, 6, 20, 12, 30, 15, DateTimeKind.Utc); 
            string expected = "2008-06-20T12:30:15Z";            
            string actual;
            actual = Utilities.LocalDateTimeInUTC(dateTime);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LocalDateInUTC
        ///</summary>
        [Test]
        public void LocalDateInUTCTest()
        {
            DateTime dateTime = new DateTime(2008, 6, 20, 12, 30, 15, DateTimeKind.Utc); 
            string expected = "2008-06-20";            
            string actual;
            actual = Utilities.LocalDateInUTC(dateTime);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsPersistable
        ///</summary>
        [Test]
        public void IsPersistableTest3()
        {
            int iNum = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Utilities.IsPersistable(iNum);
            Assert.AreEqual(expected, actual);
            iNum = 2;
            expected = true;
            actual = Utilities.IsPersistable(iNum);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsPersistable
        ///</summary>
        [Test]
        public void IsPersistableTest2()
        {
            string s = "TestValue"; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Utilities.IsPersistable(s);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsPersistable
        ///</summary>
        [Test]
        public void IsPersistableTest1()
        {
            DateTime testDate = DateTime.Now;
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Utilities.IsPersistable(testDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsPersistable
        ///</summary>
        [Test]
        public void IsPersistableTest()
        {
            AtomUri uriString = new AtomUri("http://www.test.com/");
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Utilities.IsPersistable(uriString);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAttributeValue
        ///</summary>
        [Test]
        public void GetAttributeValueTest()
        {
            string attributeName = "TestValue"; // TODO: Initialize to an appropriate value
            XmlNode xmlNode = null; // TODO: Initialize to an appropriate value
            string expected = null; 
            string actual;
            actual = Utilities.GetAttributeValue(attributeName, xmlNode);
            Assert.AreEqual(expected, actual);
        }


       
      
        /// <summary>
        ///A test for EncodeString
        ///</summary>
        [Test]
        public void EncodeStringTest()
        {
            string content = "TestValue"; // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            actual = Utilities.EncodeString(content);
            Assert.AreEqual(expected, actual);
        }

         /// <summary>
        ///A test for DecodedValue
        ///</summary>
        [Test]
        public void DecodedValueTest()
        {
            string value = "Test&amp;Value"; // TODO: Initialize to an appropriate value
            string expected = "Test&Value";            
            string actual;
            actual = Utilities.DecodedValue(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertToXSDString
        ///</summary>
        [Test]
        public void ConvertToXSDStringTest()
        {
            bool flag = false;
            string expected = Utilities.XSDFalse;        
            string actual;
            actual = Utilities.ConvertToXSDString(flag);
            Assert.AreEqual(expected, actual);  
            flag = true;
            expected = Utilities.XSDTrue;
            actual = Utilities.ConvertToXSDString(flag);
            Assert.AreEqual(expected, actual);

            expected = "Frank";
            actual = Utilities.ConvertToXSDString("Frank");
            Assert.AreEqual(expected, actual);
        }
    }
}
