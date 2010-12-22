/* Copyright (c) 2006-2008 Google Inc.
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

#define USE_TRACING
#define DEBUG

using System;
using System.IO;
using System.Xml; 
using System.Collections;
using System.Configuration;
using System.Net; 
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Client.UnitTests;
using Google.GData.Extensions;
using Google.GData.Photos;
using Google.GData.Extensions.Exif;
using Google.GData.Extensions.Location;
using Google.GData.Extensions.MediaRss;





namespace Google.GData.Client.LiveTests
{
    [TestFixture] 
    [Category("LiveTest")]
    public class PhotosTestSuite : BaseLiveTestClass
    {
        /// <summary>
        ///  test Uri for google calendarURI
        /// </summary>
        protected string defaultPhotosUri; 

      //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public PhotosTestSuite()
        {
        }

        public override string ServiceName
        {
            get {
                return PicasaService.GPicasaService; 
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>private void ReadConfigFile()</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("photosUri"))
            {
                this.defaultPhotosUri = (string) unitTestConfiguration["photosUri"];
                Tracing.TraceInfo("Read photosUri value: " + this.defaultPhotosUri);
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void PhotosAuthenticationTest()
        {
            Tracing.TraceMsg("Entering PhotosAuthenticationTest");

            PicasaQuery query = new PicasaQuery();
            PicasaService service = new PicasaService("unittests");
            query.KindParameter = "album,tag"; 
            
            if (this.defaultPhotosUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                query.Uri = new Uri(this.defaultPhotosUri);
                AtomFeed feed = service.Query(query);

                ObjectModelHelper.DumpAtomObject(feed,CreateDumpFileName("PhotoAuthTest")); 

                if (feed != null && feed.Entries.Count > 0)
                {
                    Tracing.TraceMsg("Found a Feed " + feed.ToString());
                    DisplayExtensions(feed);

                    foreach (AtomEntry entry in feed.Entries)
                    {
                        Tracing.TraceMsg("Found an entry " + entry.ToString());
                        DisplayExtensions(entry);
                    }
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, iterates all entries</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void QueryPhotosTest()
        {
            Tracing.TraceMsg("Entering PhotosQueryPhotosTest");

            PhotoQuery query = new PhotoQuery();
            PicasaService service = new PicasaService("unittests");
           
            if (this.defaultPhotosUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultPhotosUri);
                PicasaFeed feed = service.Query(query);

                ObjectModelHelper.DumpAtomObject(feed,CreateDumpFileName("PhotoAuthTest")); 
     
                if (feed != null && feed.Entries.Count > 0)
                {
                    Tracing.TraceMsg("Found a Feed " + feed.ToString());
                    DisplayExtensions(feed);

                    foreach (PicasaEntry entry in feed.Entries)
                    {
                        Tracing.TraceMsg("Found an entry " + entry.ToString());
                        DisplayExtensions(entry);

                        GeoRssWhere w = entry.Location;
                        if (w != null)
                        {
                            Tracing.TraceMsg("Found an location " + w.Latitude + w.Longitude);
                        }

                        ExifTags tags = entry.Exif;
                        if (tags != null)
                        {
                            Tracing.TraceMsg("Found an exif block ");
                        }

                        MediaGroup group = entry.Media;
                        if (group != null)
                        {
                            Tracing.TraceMsg("Found a media Group");
                            if (group.Title != null)
                            {
                                Tracing.TraceMsg(group.Title.Value);
                            }
                            if (group.Keywords != null)
                            {
                                Tracing.TraceMsg(group.Keywords.Value);
                            }
                            if (group.Credit != null)
                            {
                                Tracing.TraceMsg(group.Credit.Value);
                            }
                            if (group.Description != null)
                            {
                                Tracing.TraceMsg(group.Description.Value);
                            }
                        }


                        PhotoAccessor photo = new PhotoAccessor(entry);

                        Assert.IsTrue(entry.IsPhoto, "this is a photo entry, it should have the kind set");
                        Assert.IsTrue(photo != null, "this is a photo entry, it should convert to PhotoEntry");

                        Assert.IsTrue(photo.AlbumId != null);
                        Assert.IsTrue(photo.Height > 0);
                        Assert.IsTrue(photo.Width > 0);
                    }
                }

                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        
        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new photo</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void InsertPhotoTest()
        {
            Tracing.TraceMsg("Entering InsertPhotoTest");

            AlbumQuery query = new AlbumQuery();
            PicasaService service = new PicasaService("unittests");
           
            if (this.defaultPhotosUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                query.Uri = new Uri(this.defaultPhotosUri);

                PicasaFeed feed = service.Query(query);
                if (feed != null)
                {
                    Assert.IsTrue(feed.Entries != null, "the albumfeed needs entries");
                    Assert.IsTrue(feed.Entries[0] != null, "the albumfeed needs at least ONE entry");
                    PicasaEntry album = feed.Entries[0] as PicasaEntry;
                    Assert.IsTrue(album != null, "should be an album there");
                    Assert.IsTrue(album.FeedUri != null, "the albumfeed needs a feed URI, no photo post on that one");
                    Uri postUri = new Uri(album.FeedUri.ToString());
                    FileStream fs = File.OpenRead(this.resourcePath + "testnet.jpg");
                    PicasaEntry entry = service.Insert(postUri, fs, "image/jpeg", "testnet.jpg") as PicasaEntry;
                    Assert.IsTrue(entry.IsPhoto, "the new entry should be a photo entry");

                    entry.Title.Text = "This is a new Title";
                    entry.Summary.Text = "A lovely shot in the shade";
                    PicasaEntry updatedEntry = entry.Update() as PicasaEntry;
                    Assert.IsTrue(updatedEntry.IsPhoto, "the new entry should be a photo entry");
                    Assert.IsTrue(updatedEntry.Title.Text == "This is a new Title", "The titles should be identical");
                    Assert.IsTrue(updatedEntry.Summary.Text == "A lovely shot in the shade", "The summariesa should be identical");


                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>tries to insert a photo using MIME multipart</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void InsertMimePhotoTest()
        {
            Tracing.TraceMsg("Entering InsertMimePhotoTest");

            AlbumQuery query = new AlbumQuery();
            PicasaService service = new PicasaService("unittests");
           
            if (this.defaultPhotosUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                query.Uri = new Uri(this.defaultPhotosUri);

                PicasaFeed feed = service.Query(query);
                if (feed != null)
                {
                    
                    Assert.IsTrue(feed.Entries != null, "the albumfeed needs entries");
                    Assert.IsTrue(feed.Entries[0] != null, "the albumfeed needs at least ONE entry");

                    PicasaEntry album = feed.Entries[0] as PicasaEntry;

                    PhotoEntry newPhoto = new PhotoEntry();
                    newPhoto.Title.Text = "this is a title";
                    newPhoto.Summary.Text = "A lovely shot in the ocean";

                    newPhoto.MediaSource = new MediaFileSource(this.resourcePath + "testnet.jpg", "image/jpeg");

                    Uri postUri = new Uri(album.FeedUri.ToString());

                    PicasaEntry entry = service.Insert(postUri, newPhoto) as PicasaEntry;

                    Assert.IsTrue(entry.IsPhoto, "the new entry should be a photo entry");

                    entry.Title.Text = "This is a new Title";
                    entry.Summary.Text = "A lovely shot in the shade";
                    entry.MediaSource = new MediaFileSource(this.resourcePath + "testnet.jpg", "image/jpeg");
                    PicasaEntry updatedEntry = entry.Update() as PicasaEntry;
                    Assert.IsTrue(updatedEntry.IsPhoto, "the new entry should be a photo entry");
                    Assert.IsTrue(updatedEntry.Title.Text == "This is a new Title", "The titles should be identical");
                    Assert.IsTrue(updatedEntry.Summary.Text == "A lovely shot in the shade", "The summariesa should be identical");


                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        protected void DisplayExtensions(AtomBase obj) 
        {
            foreach (Object o in obj.ExtensionElements)
            {

                IExtensionElementFactory e = o as IExtensionElementFactory;
                SimpleElement s = o as SimpleElement;
                XmlElement x = o as XmlElement;
                if (s != null)
                {
                    DumpSimpleElement(s);

                    SimpleContainer sc = o as SimpleContainer;

                    if (sc != null)
                    {
                        foreach (SimpleElement se in sc.ExtensionElements)
                        {
                            DumpSimpleElement(se);
                        }
                    }
                }  
                else if (e != null)
                {
                    Tracing.TraceMsg("Found an extension Element " + e.ToString());
                } 
                else if (x != null)
                {
                    Tracing.TraceMsg("Found an XmlElement " + x.ToString() + " " + x.LocalName + " " + x.NamespaceURI);
                }
                else 
                {
                    Tracing.TraceMsg("Found an object " + o.ToString());
                }
            }
        }

        protected void DumpSimpleElement(SimpleElement s)
        {
            Tracing.TraceMsg("Found a simple Element " + s.ToString() + " " + s.Value);
            if (s.Attributes.Count > 0)
            {
                for (int i=0; i < s.Attributes.Count; i++)
                {
                    string name = s.Attributes.GetKey(i) as string;
                    string value = s.Attributes.GetByIndex(i) as string;
                    Tracing.TraceMsg("--- Attributes on element: " + name + ":" + value);
                }
            }

        }

    } /////////////////////////////////////////////////////////////////////////////
}




