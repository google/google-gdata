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
            anotherEntry.MediaSource = new MediaFileSource(this.resourcePath + "test.mp4", "video/mp4");
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



        ///////////////////////// START OF REQUEST TESTS 



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeRequestTest()
        {
            Tracing.TraceMsg("Entering YouTubeRequestTest");


            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);

            YouTubeRequest f = new YouTubeRequest(settings);
            // GetVideoFeed get's you a users video feed
            Feed<Video> feed = f.GetVideoFeed(null);
            // this will get you just the first 25 videos. 
            foreach (Video v in feed.Entries)
            {
                Assert.IsTrue(v.AtomEntry != null, "There should be an atomentry");
                Assert.IsTrue(v.Title != null, "There should be a title");
                Assert.IsTrue(v.VideoId != null, "There should be a videoID");
            }

            Feed<Video> sfeed = f.GetStandardFeed(YouTubeQuery.MostPopular);

            int iCountOne=0; 
            // this loop get's you all videos in the mostpopular video feeed
            foreach (Video v in sfeed.Entries)
            {
                Assert.IsTrue(v.AtomEntry != null, "There should be an atomentry");
                Assert.IsTrue(v.Title != null, "There should be a title");
                Assert.IsTrue(v.VideoId != null, "There should be a videoID");
                iCountOne++; 
            }
            int iCountTwo = 0; 
            sfeed.AutoPaging = true;
            sfeed.Maximum = 50; 

            foreach (Video v in sfeed.Entries)
            {
                Assert.IsTrue(v.AtomEntry != null, "There should be an atomentry");
                Assert.IsTrue(v.Title != null, "There should be a title");
                Assert.IsTrue(v.VideoId != null, "There should be a videoID");
                iCountTwo++; 
            }
            Assert.IsTrue(iCountTwo > iCountOne); 
        }
        /////////////////////////////////////////////////////////////////////////////
        // 

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubePlaylistRequestTest()
        {
            Tracing.TraceMsg("Entering YouTubePlaylistRequestTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);

            YouTubeRequest f = new YouTubeRequest(settings);
            // GetVideoFeed get's you a users video feed
            Feed<Playlist> feed = f.GetPlaylistsFeed(null);
            // this will get you just the first 25 videos. 
            foreach (Playlist p in feed.Entries)
            {
                Assert.IsTrue(p.AtomEntry != null);
                Assert.IsTrue(p.Title != null);
                Feed<Video> list = f.GetPlaylist(p);
                foreach (Video v in list.Entries )
                {
                    Assert.IsTrue(v.AtomEntry != null, "There should be an atomentry");
                    Assert.IsTrue(v.Title != null, "There should be a title");
                    Assert.IsTrue(v.VideoId != null, "There should be a videoID"); 
                    // there might be no watchpage (not published yet)
                    // Assert.IsTrue(v.WatchPage != null, "There should be a watchpage");
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


         //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeCommentRequestTest()
        {
            Tracing.TraceMsg("Entering YouTubeCommentRequestTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
            // this will get you just the first 25 videos. 
            foreach (Video v in feed.Entries)
            {
                Feed<Comment> list = f.GetComments(v);
                foreach (Comment c in list.Entries)
                {
                    Assert.IsTrue(c.AtomEntry != null);
                    Assert.IsTrue(c.Title != null);
                }
            }

        }

        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeMaximumTest()
        {
            Tracing.TraceMsg("Entering YouTubeMaximumTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);
            settings.Maximum = 15;
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
            int iCount = 0; 
            // this will get you just the first 15 videos. 
            foreach (Video v in feed.Entries)
            {
                iCount++;
            }

            Assert.AreEqual(iCount, 15);

        }
        /////////////////////////////////////////////////////////////////////////////


          //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeUnAuthenticatedRequestTest()
        {
            Tracing.TraceMsg("Entering YouTubeUnAuthenticatedRequestTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey);
            settings.AutoPaging = true;
            settings.Maximum = 50; 

            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
            // this will get you just the first 25 videos. 
            foreach (Video v in feed.Entries)
            {
                Feed<Comment> list= f.GetComments(v);
                foreach (Comment c in list.Entries)
                {
                    Assert.IsTrue(c.AtomEntry != null);
                    Assert.IsTrue(c.Title != null);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube Feed object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeRequestInsertTest()
        {
            Tracing.TraceMsg("Entering YouTubeRequestInsertTest");
            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);
            YouTubeRequest f = new YouTubeRequest(settings);

            Video v = Video.CreateInstance();
            v.Title = "Sample upload";
            v.Description = "This is a test with and & in it";

            MediaCategory category = new MediaCategory("Nonprofit");
            category.Attributes["scheme"] = YouTubeService.DefaultCategory;
            v.Tags.Add(category);
            v.Keywords = "math"; 
            v.YouTubeEntry.MediaSource = new MediaFileSource(this.resourcePath + "test_movie.mov", "video/quicktime");

            Video newVideo = f.Upload(this.ytUser, v); 

            newVideo.Title = "This test upload will soon be deleted";
            Video updatedVideo = f.Update(newVideo);

            Assert.AreEqual(updatedVideo.Description, newVideo.Description, "Description should be equal");
            Assert.AreEqual(updatedVideo.Keywords, newVideo.Keywords, "Keywords should be equal");

            newVideo.YouTubeEntry.MediaSource = new MediaFileSource(this.resourcePath + "test.mp4", "video/mp4");
            Video last = f.Update(updatedVideo);
            f.Delete(last);
        }
        /////////////////////////////////////////////////////////////////////////////
        // 

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubePageSizeTest()
        {
            Tracing.TraceMsg("Entering YouTubePageSizeTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);
            settings.PageSize = 15;
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
            int iCount = 0; 
            // this will get you just the first 15 videos. 
            foreach (Video v in feed.Entries)
            {
                iCount++;
                f.Settings.PageSize = 5; 
                Feed<Comment> list = f.GetComments(v);
                int i = 0; 
                foreach (Comment c in list.Entries)
                {
                    i++;
                }
                Assert.IsTrue(i <= 5, "the count should be smaller/equal 5"); 
                Assert.IsTrue(list.PageSize == -1 || list.PageSize == 5, "the returned pagesize should be 5 or -1 as well"); 
            }

            Assert.AreEqual(iCount, 15, "the outer feed should count 15");
            Assert.AreEqual(feed.PageSize, 15, "outer feed pagesize should be 15"); 

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubePagingTest()
        {
            Tracing.TraceMsg("Entering YouTubePagingTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);
            settings.PageSize = 15;
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
            Feed<Video> prev = f.Get<Video>(feed, FeedRequestType.Prev);
            Assert.IsTrue(prev == null, "the first chunk should not have a prev"); 

            Feed<Video> next = f.Get<Video>(feed, FeedRequestType.Next); 
            Assert.IsTrue(next != null, "the next chunk should exist"); 

            prev = f.Get<Video>(next, FeedRequestType.Prev);
            Assert.IsTrue(prev != null, "the prev chunk should exist now"); 

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeGetTest()
        {
            Tracing.TraceMsg("Entering YouTubeGetTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);
            settings.PageSize = 15;
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);

            foreach (Video v in feed.Entries)
            {
                Video refresh = f.Get<Video>(v);

                Assert.AreEqual(refresh.VideoId, v.VideoId, "The ID values should be equal");
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeGetCategoriesTest()
        {
            Tracing.TraceMsg("Entering YouTubeGetCategoriesTest");

            AtomCategoryCollection collection = YouTubeQuery.GetYouTubeCategories();

            foreach (YouTubeCategory cat in collection)
            {
                Assert.IsTrue(cat.Term != null);
                Assert.IsTrue(cat.Assignable || cat.Deprecated || cat.Browsable != null);
                if (cat.Assignable == true)
                {
                    Assert.IsTrue(cat.Browsable != null, "Assumption, if its assignable, it's browsable");
                }

            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeGetActivitiesTest()
        {
            ActivitiesQuery query = new ActivitiesQuery();
            YouTubeService service = new YouTubeService("NETUnittests", this.ytClient, this.ytDevKey);

            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }
            ActivitiesFeed feed = service.Query(query, new DateTime(1980, 12, 1)) as ActivitiesFeed;

            foreach (ActivityEntry e in feed.Entries )
            {
                Assert.IsTrue(e.VideoId != null, "There should be a videoid");
            }
            service = null;
           
        }
        /////////////////////////////////////////////////////////////////////////////
        // 

                //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeRequestActivitiesTest()
        {
            Tracing.TraceMsg("Entering YouTubeRequestActivitiesTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);
            // settings.PageSize = 15;
            YouTubeRequest f = new YouTubeRequest(settings);

            // this returns the server default answer
            Feed<Activity> feed = f.GetActivities();

            foreach (Activity a in feed.Entries)
            {
                Assert.IsTrue(a.VideoId != null, "There should be a VideoId");
            }

            // now let's find all that happened in the last 24 hours

            DateTime t = DateTime.Now.AddDays(-1);

            // this returns the all activities for the last 24 hours  default answer
            Feed<Activity> yesterday = f.GetActivities(t);

            foreach (Activity a in yesterday.Entries)
            {
                Assert.IsTrue(a.VideoId != null, "There should be a VideoId");
            }

            t = DateTime.Now.AddMinutes(-1);


            // this returns the all activities for the last 1 minute, should be empty

            try
            {

                Feed<Activity> lastmin = f.GetActivities(t);
                int iCount = 0;

                foreach (Activity a in lastmin.Entries)
                {
                    iCount++;
                }
                Assert.IsTrue(iCount == 0, "There should be no activity for the last minute");
            }
            catch (GDataNotModifiedException e)
            {
                Assert.IsTrue(e != null);
            }
        }
        /////////////////////////////////////////////////////////////////////////////




    }



    [TestFixture] 
    [Category("LiveTest")]
    public class YouTubeVerticalTestSuite : BaseLiveTestClass
    {

        private string ytClient;
        private string ytDevKey;
        private string ytUser;
        private string ytPwd;

        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public YouTubeVerticalTestSuite()
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
        /// <summary>runs a test on the YouTube factory object</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void YouTubeSubscriptionsTest()
        {
            Tracing.TraceMsg("Entering YouTubeSubscriptionsTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);
            // settings.PageSize = 15;
            YouTubeRequest f = new YouTubeRequest(settings);

            // this returns the server default answer
            Feed<Subscription> feed = f.GetSubscriptionsFeed(null);

            foreach (Subscription s in feed.Entries)
            {
                Assert.IsTrue(s.PlaylistId != null, "There should be a PlaylistId");
                Assert.IsTrue(s.PlaylistTitle != null, "There should be a PlaylistTitle");
            }

            Subscription sub = Subscription.CreateInstance();
            sub.Type = SubscriptionEntry.SubscriptionType.channel;
            sub.PlaylistId = "dXNzb2NjZXJkb3Rjb20";
            sub.UserName = this.ytUser;

            f.Insert(feed, sub);

        }
        /////////////////////////////////////////////////////////////////////////////



    }
}




