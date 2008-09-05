using Google.GData.Client;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using System.Xml;
using System.Collections;
using System.IO;
using System;
using System.Text;
using Google.GData.Extensions.AppControl;

namespace Google.GData.Client.UnitTests.Core
{
    
    
    /// <summary>
    ///This is a test class for AtomBaseTest and is intended
    ///to contain all AtomBaseTest Unit Tests
    ///</summary>
    [TestFixture][Category("CoreClient")]
    public class AtomBaseTest
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
        ///A test for Language
        ///</summary>
        [Test]
        public void LanguageTest()
        {
            AtomBase target = CreateAtomBase(); // TODO: Initialize to an appropriate value
            string expected = "TestValue";            
            string actual;
            target.Language = expected;
            actual = target.Language;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for ExtensionFactories
        ///</summary>
        [Test]
        public void ExtensionFactoriesTest()
        {
            AtomBase target = CreateAtomBase(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.ExtensionFactories);
        }

        /// <summary>
        ///A test for ExtensionElements
        ///</summary>
        [Test]
        public void ExtensionElementsTest()
        {
            AtomBase target = CreateAtomBase(); // TODO: Initialize to an appropriate value
            Assert.IsNotNull(target.ExtensionElements);
        }

        /// <summary>
        ///A test for Dirty
        ///</summary>
        [Test]
        public void DirtyTest()
        {
            AtomBase target = CreateAtomBase(); // TODO: Initialize to an appropriate value
            bool expected = true; 
            bool actual;
            target.Dirty = expected;
            actual = target.Dirty;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Base
        ///</summary>
        [Test]
        public void BaseTest()
        {
            AtomBase target = CreateAtomBase(); // TODO: Initialize to an appropriate value
            AtomUri expected =new AtomUri("http://www.test.com/");
            AtomUri actual;
            target.Base = expected;
            actual = target.Base;
            Assert.AreEqual(expected, actual);
        }


        

        /// <summary>
        ///A test for ShouldBePersisted
        ///</summary>
        [Test]
        public void ShouldBePersistedTest()
        {
            AtomBase target = CreateAtomBase(); 
            Assert.IsTrue(target.ShouldBePersisted()==true);
        }

        /// <summary>
        ///A test for SaveToXml
        ///</summary>
        [Test]
        public void SaveToXmlTestStream()
        {
            AtomBase target = CreateAtomBase(); 
            Stream stream = new MemoryStream(); 
            target.SaveToXml(stream);
            Assert.IsTrue(stream.Length  > 0);
        }

    


        /// <summary>
        ///A test for ReplaceExtension
        ///</summary>
        [Test]
        public void ReplaceExtensionTest()
        {
            AtomBase target = CreateAtomBase(); 
            Assert.IsTrue(target.ExtensionElements.Count == 0);

            AppDraft draft = new AppDraft();
            target.ExtensionElements.Add(draft);
            Assert.IsTrue(target.ExtensionElements.Count == 1);

            AppDraft draft2 = new AppDraft();
            target.ReplaceExtension(draft2.XmlName, draft2.XmlNameSpace, draft2);
            Assert.IsTrue(target.ExtensionElements.Count == 1);
            Assert.AreEqual(draft2, target.ExtensionElements[0]);
        }


        /// <summary>
        ///A test for FindExtensionFactory
        ///</summary>
        [Test]
        public void FindExtensionFactoryTest()
        {
            AtomBase target = CreateAtomBase(); // TODO: Initialize to an appropriate value
            Assert.IsTrue(target.ExtensionFactories.Count == 0);
            AppDraft draft = new AppDraft();
            target.ExtensionFactories.Add(draft);
            Assert.AreEqual(draft, target.FindExtensionFactory(draft.XmlName, draft.XmlNameSpace));
        }

        /// <summary>
        ///A test for FindExtension
        ///</summary>
        [Test]
        public void FindExtensionTest()
        {
            AtomBase target = CreateAtomBase(); // TODO: Initialize to an appropriate value
            Assert.IsTrue(target.ExtensionElements.Count == 0);
            AppDraft draft = new AppDraft();
            target.ExtensionElements.Add(draft);
            Assert.AreEqual(draft, target.FindExtension(draft.XmlName, draft.XmlNameSpace));
        }

        /// <summary>
        ///A test for DeleteExtensions
        ///</summary>
        [Test]
        public void DeleteExtensionsTest()
        {
            AtomBase target = CreateAtomBase(); 
            Assert.IsTrue(target.ExtensionElements.Count == 0);

            AppDraft draft = new AppDraft();
            target.ExtensionElements.Add(draft);
            Assert.IsTrue(target.ExtensionElements.Count == 1);
            target.DeleteExtensions(draft.XmlName, draft.XmlNameSpace);
            Assert.IsTrue(target.ExtensionElements.Count == 0);
        }

        /// <summary>
        ///A test for CreateExtension
        ///</summary>
        [Test]
        public void CreateExtensionTest()
        {
            AtomBase target = CreateAtomBase(); 

            Assert.IsTrue(target.ExtensionFactories.Count == 0);
            AppDraft draft = new AppDraft();
            target.ExtensionFactories.Add(draft);

            Object actual = target.CreateExtension(draft.XmlName, draft.XmlNameSpace);
            Assert.IsTrue(actual is AppDraft);
        }


        internal virtual AtomBase CreateAtomBase()
        {
            AtomTextConstruct entry = new AtomTextConstruct();
            entry.Text = "test";
            return entry;
        }

        /// <summary>
        ///A test for AddExtension
        ///</summary>
        [Test]
        public void AddExtensionTest()
        {
            AtomBase target = CreateAtomBase(); 

            Assert.IsTrue(target.ExtensionFactories.Count == 0);
            AppDraft draft = new AppDraft();
            target.AddExtension(draft);
            Assert.AreEqual(draft, target.FindExtensionFactory(draft.XmlName, draft.XmlNameSpace));
        }
    }
}
