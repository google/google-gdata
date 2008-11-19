/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/
using Google.GData.YouTube;
using NUnit.Framework;
using Google.GData.Extensions;
using Google.GData.Extensions.MediaRss;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for ProfileEntryTest and is intended
    ///to contain all ProfileEntryTest Unit Tests
    ///</summary>
    [TestFixture][Category("YouTube")]
    public class ProfileEntryTest
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
        ///A test for UserName
        ///</summary>
        [Test]
        public void UserNameTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.UserName = expected;
            actual = target.UserName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Statistics
        ///</summary>
        [Test]
        public void StatisticsTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            Statistics expected =new Statistics();
            Statistics actual;
            target.Statistics = expected;
            actual = target.Statistics;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for School
        ///</summary>
        [Test]
        public void SchoolTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.School = expected;
            actual = target.School;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Occupation
        ///</summary>
        [Test]
        public void OccupationTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Occupation = expected;
            actual = target.Occupation;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Music
        ///</summary>
        [Test]
        public void MusicTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Music = expected;
            actual = target.Music;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Movies
        ///</summary>
        [Test]
        public void MoviesTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Movies = expected;
            actual = target.Movies;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for Location
        ///</summary>
        [Test]
        public void LocationTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Location = expected;
            actual = target.Location;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Lastname
        ///</summary>
        [Test]
        public void LastnameTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Lastname = expected;
            actual = target.Lastname;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Hobbies
        ///</summary>
        [Test]
        public void HobbiesTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Hobbies = expected;
            actual = target.Hobbies;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Gender
        ///</summary>
        [Test]
        public void GenderTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Gender = expected;
            actual = target.Gender;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Firstname
        ///</summary>
        [Test]
        public void FirstnameTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Firstname = expected;
            actual = target.Firstname;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FeedLinks
        ///</summary>
        [Test]
        public void FeedLinksTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            ExtensionCollection<FeedLink> actual;
            actual = target.FeedLinks;
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Company
        ///</summary>
        [Test]
        public void CompanyTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Company = expected;
            actual = target.Company;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Books
        ///</summary>
        [Test]
        public void BooksTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            string expected = "secret text string"; // TODO: Initialize to an appropriate value
            string actual;
            target.Books = expected;
            actual = target.Books;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Age
        ///</summary>
        [Test]
        public void AgeTest()
        {
            ProfileEntry target = new ProfileEntry(); // TODO: Initialize to an appropriate value
            int expected = 91; // TODO: Initialize to an appropriate value
            int actual;
            target.Age = expected;
            actual = target.Age;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProfileEntry Constructor
        ///</summary>
        [Test]
        public void ProfileEntryConstructorTest()
        {
            ProfileEntry target = new ProfileEntry();
            Assert.IsNotNull(target);
            Assert.AreEqual(target.Age, 0);
            Assert.IsNull(target.Books);
            Assert.IsNull(target.Statistics);
            Assert.IsNull(target.School);
            Assert.IsNull(target.Occupation);
            Assert.IsNull(target.Music);
            Assert.IsNull(target.Movies);
            Assert.IsNull(target.Location);
            Assert.IsNull(target.Lastname);
            Assert.IsNull(target.Hobbies);
            Assert.IsNull(target.Gender);
            Assert.IsNull(target.Firstname);
            Assert.IsNull(target.Company);
            Assert.IsNotNull(target.FeedLinks);
        }
    }
}
