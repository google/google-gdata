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
using Google.GData.YouTube;
using Google.GData.Extensions.Location;
using Google.GData.Extensions.MediaRss;
using Google.YouTube;

 namespace Google.GData.Client.LiveTests
{
    [TestFixture] 
    [Category("LiveTest")]
    public class YouTubeTestSuite : BaseLiveTestClass
    {

        private string ytClient;
        private string ytDevKey;
        private string ytUser;
        private string ytPwd;

        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public YouTubeTestSuite()
        {
        }

        public override string ServiceName
        {
            get {
                return YouTubeService.YTService; 
            }
        }


                //////////////////////////////////////////////////////////////////////
        /// <summary>private void ReadConfigFile()</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("youTubeClientID") == true)
            {
                this.ytClient = (string) unitTestConfiguration["youTubeClientID"];
            }
            if (unitTestConfiguration.Contains("youTubeDevKey") == true)
            {
                this.ytDevKey = (string) unitTestConfiguration["youTubeDevKey"];
            }
            if (unitTestConfiguration.Contains("youTubeUser") == true)
            {
                this.ytUser = (string) unitTestConfiguration["youTubeUser"];
            }
            if (unitTestConfiguration.Contains("youTubePwd") == true)
            {
                this.ytPwd = (string) unitTestConfiguration["youTubePwd"];
            }
        }
        /////////////////////////////////////////////////////////////////////////////




        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube Query object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeQueryTest()
        {
            Tracing.TraceMsg("Entering YouTubeQueryTest");

            YouTubeQuery query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);

            query.Formats.Add(YouTubeQuery.VideoFormat.RTSP);
            query.Formats.Add(YouTubeQuery.VideoFormat.Mobile);

            query.Time = YouTubeQuery.UploadTime.ThisWeek;

            Assert.AreEqual(query.Uri.AbsoluteUri, YouTubeQuery.DefaultVideoUri + "?format=1%2C6&time=this_week", "Video query should be identical");

            query = new YouTubeQuery();
            query.Uri = new Uri("http://www.youtube.com/feeds?format=1&time=this_week&racy=included");

            Assert.AreEqual(query.Time, YouTubeQuery.UploadTime.ThisWeek, "Should be this week");
            Assert.AreEqual(query.Formats[0], YouTubeQuery.VideoFormat.RTSP, "Should be RTSP");
            Assert.AreEqual(query.Racy, "included", "Racy should be included");


        }
        /////////////////////////////////////////////////////////////////////////////


       //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube feed, trying to find private videos</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeQueryPrivateTest()
        {
            Tracing.TraceMsg("Entering YouTubeQueryPrivateTest");

            YouTubeQuery query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
            YouTubeService service = new YouTubeService("NETUnittests", this.ytClient, this.ytDevKey);

            query.Query = "Education expertvillage"; 
            query.NumberToRetrieve = 50; 
            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }

            YouTubeFeed feed = service.Query(query);

            int counter = 0; 
            foreach (YouTubeEntry e in feed.Entries )
            {
                Assert.IsTrue(e.Media.Title.Value != null, "There should be a title");
                if (e.Private == true)
                {
                    counter++;
                }
            }
            Assert.IsTrue(counter == 0, "counter was " + counter);
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube Feed object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeFeedTest()
        {
            Tracing.TraceMsg("Entering YouTubeFeedTest");

            YouTubeQuery query = new YouTubeQuery(YouTubeQuery.TopRatedVideo);
            YouTubeService service = new YouTubeService("NETUnittests", this.ytClient, this.ytDevKey);
            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }


            query.Formats.Add(YouTubeQuery.VideoFormat.RTSP);
            query.Time = YouTubeQuery.UploadTime.ThisWeek;

            YouTubeFeed feed = service.Query(query);

            foreach (YouTubeEntry e in feed.Entries )
            {
                Assert.IsTrue(e.Media.Title.Value != null, "There should be a title");
            }
        }
        /////////////////////////////////////////////////////////////////////////////


     



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube Feed object using the read only service</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeReadOnlyTest()
        {
            Tracing.TraceMsg("Entering YouTubeFeedTest");

            YouTubeQuery query = new YouTubeQuery(YouTubeQuery.TopRatedVideo);
            YouTubeService service = new YouTubeService("NETUnittests");

            query.Formats.Add(YouTubeQuery.VideoFormat.RTSP);
            query.Time = YouTubeQuery.UploadTime.ThisWeek;

            YouTubeFeed feed = service.Query(query);

            foreach (YouTubeEntry e in feed.Entries )
            {
                Assert.IsTrue(e.Media.Title.Value != null, "There should be a title");
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube Feed object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeInsertTest()
        {
            Tracing.TraceMsg("Entering YouTubeFeedTest");

            YouTubeService service = new YouTubeService("NETUnittests", this.ytClient, this.ytDevKey);
            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }

            GDataRequestFactory factory = service.RequestFactory as GDataRequestFactory;
            factory.Timeout = 1000000; 

            YouTubeEntry entry = new YouTubeEntry();

            entry.MediaSource = new MediaFileSource(this.resourcePath + "test_movie.mov", "video/quicktime");
            entry.Media = new YouTube.MediaGroup();
            entry.Media.Description = new MediaDescription("This is a test with and & in it");
            entry.Media.Title = new MediaTitle("Sample upload");
            entry.Media.Keywords = new MediaKeywords("math");

            // entry.Media.Categories

            MediaCategory category = new MediaCategory("Nonprofit");
            category.Attributes["scheme"] = YouTubeService.DefaultCategory;

            entry.Media.Categories.Add(category);

            YouTubeEntry newEntry = service.Upload(this.ytUser, entry);

            Assert.AreEqual(newEntry.Media.Description.Value, entry.Media.Description.Value, "Description should be equal");
            Assert.AreEqual(newEntry.Media.Keywords.Value, entry.Media.Keywords.Value, "Keywords should be equal");

            // now change the entry

            newEntry.Title.Text = "This test upload will soon be deleted";
            YouTubeEntry anotherEntry = newEntry.Update() as YouTubeEntry;

            // bugbug in YouTube server. Returns empty category that the service DOES not like on reuse. so remove
            ExtensionList a = ExtensionList.NotVersionAware();
            foreach (MediaCategory m in anotherEntry.Media.Categories)
            {
                if (String.IsNullOrEmpty(m.Value))
                {
                    a.Add(m);
                }
            }

            foreach (MediaCategory m in a)
            {
                anotherEntry.Media.Categories.Remove(m);
            }

            Assert.AreEqual(newEntry.Media.Description.Value, anotherEntry.Media.Description.Value, "Description should be equal");
            Assert.AreEqual(newEntry.Media.Keywords.Value, anotherEntry.Media.Keywords.Value, "Keywords should be equal");

            // now update the video
            anotherEntry.MediaSource = new MediaFileSource("test.mp4", "video/mp4");
            anotherEntry.Update();


            // now delete the guy again

            newEntry.Delete();
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube Feed object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeRatingsTest()
        {
            Tracing.TraceMsg("Entering YouTubeRatingsTest");

            YouTubeService service = new YouTubeService("NETUnittests", this.ytClient, this.ytDevKey);
            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }

            YouTubeEntry entry = new YouTubeEntry();

            entry.MediaSource = new MediaFileSource(this.resourcePath + "test_movie.mov", "video/quicktime");
            entry.Media = new YouTube.MediaGroup();
            entry.Media.Description = new MediaDescription("This is a test");
            entry.Media.Title = new MediaTitle("Sample upload");
            entry.Media.Keywords = new MediaKeywords("math");

            // entry.Media.Categories

            MediaCategory category = new MediaCategory("Nonprofit");
            category.Attributes["scheme"] = YouTubeService.DefaultCategory;

            entry.Media.Categories.Add(category);

            YouTubeEntry newEntry = service.Upload(this.ytUser, entry);

            Assert.AreEqual(newEntry.Media.Description.Value, entry.Media.Description.Value, "Description should be equal");
            Assert.AreEqual(newEntry.Media.Keywords.Value, entry.Media.Keywords.Value, "Keywords should be equal");


            Rating rating = new Rating();
            rating.Value = 1;
            newEntry.Rating = rating;

            YouTubeEntry ratedEntry = newEntry.Update();
            ratedEntry.Delete();
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeFactoryTest()
        {
            Tracing.TraceMsg("Entering YouTubeFactoryTest");

            YouTubeFactory f = new YouTubeFactory("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);

            Feed<Video> feed = f.GetVideoFeed(null);

            foreach (Video v in feed.Entries)
            {
                Assert.IsTrue(v.AtomEntry != null);
                Assert.IsTrue(v.Title != null);
                Assert.IsTrue(v.Id != null); 

            }
        }
        /////////////////////////////////////////////////////////////////////////////



        [Test] public void YouTubeUploaderTest()
        {
            YouTubeQuery query = new YouTubeQuery();
            query.Uri = new Uri(CreateUri(this.resourcePath + "uploaderyt.xml"));

            YouTubeService service = new YouTubeService("NETUnittests", this.ytClient, this.ytDevKey);
            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }

            YouTubeFeed feed = service.Query(query);
            YouTubeEntry entry = feed.Entries[0] as YouTubeEntry;

            YouTube.MediaCredit uploader = entry.Uploader;
            Assert.IsTrue(uploader != null); 
            Assert.IsTrue(uploader.Role == "uploader");
            Assert.IsTrue(uploader.Scheme == "urn:youtube");
            Assert.IsTrue(uploader.Value == "GoogleDevelopers");

        }
            



    } /////////////////////////////////////////////////////////////////////////////
}




