using Google.GData.Photos;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using Google.GData.Extensions;
using Google.GData.Extensions.Exif;
using Google.GData.Extensions.Location;
using Google.GData.Extensions.MediaRss;

namespace Google.GData.Client.UnitTests.Picasa
{
    
    
    /// <summary>
    ///This is a test class for PicasaEntryTest and is intended
    ///to contain all PicasaEntryTest Unit Tests
    ///</summary>
    [TestFixture][Category("Picasa")]
    public class PicasaEntryTest
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
        ///A test for Media
        ///</summary>
        [Test]
        public void MediaTest()
        {
            PicasaEntry target = new PicasaEntry(); // TODO: Initialize to an appropriate value
            MediaGroup expected = new MediaGroup();
            MediaGroup actual;
            target.Media = expected;
            actual = target.Media;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Location
        ///</summary>
        [Test]
        public void LocationTest()
        {
            PicasaEntry target = new PicasaEntry(); // TODO: Initialize to an appropriate value
            GeoRssWhere expected = new GeoRssWhere(); 
            GeoRssWhere actual;
            target.Location = expected;
            actual = target.Location;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsTag
        ///</summary>
        [Test]
        public void IsTagTest()
        {
            PicasaEntry target = new PicasaEntry(); // TODO: Initialize to an appropriate value
            bool expected = true;
            bool actual;
            target.IsTag = expected;
            actual = target.IsTag;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Categories.Contains(PicasaEntry.TAG_CATEGORY));
        }

        /// <summary>
        ///A test for IsPhoto
        ///</summary>
        [Test]
        public void IsPhotoTest()
        {
            PicasaEntry target = new PicasaEntry(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsPhoto = expected;
            actual = target.IsPhoto;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Categories.Contains(PicasaEntry.PHOTO_CATEGORY));
        }

        /// <summary>
        ///A test for IsComment
        ///</summary>
        [Test]
        public void IsCommentTest()
        {
            PicasaEntry target = new PicasaEntry(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsComment = expected;
            actual = target.IsComment;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Categories.Contains(PicasaEntry.COMMENT_CATEGORY));
        }

        /// <summary>
        ///A test for IsAlbum
        ///</summary>
        [Test]
        public void IsAlbumTest()
        {
            PicasaEntry target = new PicasaEntry(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsAlbum = expected;
            actual = target.IsAlbum;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Categories.Contains(PicasaEntry.ALBUM_CATEGORY));
        }

        /// <summary>
        ///A test for Exif
        ///</summary>
        [Test]
        public void ExifTest()
        {
            PicasaEntry target = new PicasaEntry(); // TODO: Initialize to an appropriate value
            ExifTags expected = new ExifTags();
            ExifTags actual;
            target.Exif = expected;
            actual = target.Exif;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for setPhotoExtension
        ///</summary>
        [Test]
        public void setPhotoExtensionTest()
        {
            PicasaEntry target = new PicasaEntry(); // TODO: Initialize to an appropriate value
            string extension = GPhotoNameTable.Photoid;
            string newValue = "theid";
            SimpleElement actual;
            actual = target.SetPhotoExtensionValue(extension, newValue);
            Assert.IsTrue(actual is GPhotoPhotoId);
            Assert.AreEqual(newValue, actual.Value);
        }

        /// <summary>
        ///A test for getPhotoExtensionValue
        ///</summary>
        [Test]
        public void getPhotoExtensionValueTest()
        {
            PicasaEntry target = new PicasaEntry(); // TODO: Initialize to an appropriate value
            string extension = GPhotoNameTable.Photoid;
            string newValue = "theid";
            string actual = null; 
            target.SetPhotoExtensionValue(extension, newValue);
            actual = target.GetPhotoExtensionValue(extension);
            Assert.AreEqual(newValue, actual);
        }
    }
}
