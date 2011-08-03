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
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Client.UnitTests;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.GData.Extensions.Location;
using Google.GData.Extensions.MediaRss;
using Google.YouTube;

namespace Google.GData.Client.LiveTests {
    [TestFixture]
    [Category("LiveTest")]
    public class YouTubeTestSuite : BaseLiveTestClass {
        private string ytDevKey;
        private string ytUser;
        private string ytPwd;

        public YouTubeTestSuite() {
        }

        public override string ServiceName {
            get {
                return ServiceNames.YouTube;
            }
        }

        protected override void ReadConfigFile() {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("youTubeDevKey")) {
                this.ytDevKey = (string)unitTestConfiguration["youTubeDevKey"];
            }
            if (unitTestConfiguration.Contains("youTubeUser")) {
                this.ytUser = (string)unitTestConfiguration["youTubeUser"];
            }
            if (unitTestConfiguration.Contains("youTubePwd")) {
                this.ytPwd = (string)unitTestConfiguration["youTubePwd"];
            }
        }

        [Test]
        public void YouTubeQueryTest() {
            Tracing.TraceMsg("Entering YouTubeQueryTest");

            YouTubeQuery query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);

            query.Formats.Add(YouTubeQuery.VideoFormat.RTSP);
            query.Formats.Add(YouTubeQuery.VideoFormat.Mobile);

            query.Time = YouTubeQuery.UploadTime.ThisWeek;

            Assert.AreEqual(query.Uri.AbsoluteUri, YouTubeQuery.DefaultVideoUri + "?format=1%2C6&time=this_week", "Video query should be identical");

            query = new YouTubeQuery();
            query.Uri = new Uri("https://www.youtube.com/feeds?format=1&time=this_week&racy=included");

            Assert.AreEqual(query.Time, YouTubeQuery.UploadTime.ThisWeek, "Should be this week");
            Assert.AreEqual(query.Formats[0], YouTubeQuery.VideoFormat.RTSP, "Should be RTSP");
        }

        [Test]
        public void YouTubeQueryPrivateTest() {
            Tracing.TraceMsg("Entering YouTubeQueryPrivateTest");

            YouTubeQuery query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
            YouTubeService service = new YouTubeService("NETUnittests", this.ytDevKey);

            query.Query = "Education expertvillage";
            query.NumberToRetrieve = 50;
            if (this.userName != null) {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }

            YouTubeFeed feed = service.Query(query);

            int counter = 0;
            foreach (YouTubeEntry e in feed.Entries) {
                Assert.IsTrue(e.Media.Title.Value != null, "There should be a title");
                if (e.Private) {
                    counter++;
                }
            }
            Assert.IsTrue(counter == 0, "counter was " + counter);
        }

        [Test]
        public void YouTubeFeedTest() {
            Tracing.TraceMsg("Entering YouTubeFeedTest");

            YouTubeQuery query = new YouTubeQuery(YouTubeQuery.TopRatedVideo);
            YouTubeService service = new YouTubeService("NETUnittests", this.ytDevKey);
            if (this.userName != null) {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }

            query.Formats.Add(YouTubeQuery.VideoFormat.RTSP);
            query.Time = YouTubeQuery.UploadTime.ThisWeek;

            YouTubeFeed feed = service.Query(query);

            foreach (YouTubeEntry e in feed.Entries) {
                Assert.IsTrue(e.Media.Title.Value != null, "There should be a title");
            }
        }

        [Test]
        public void YouTubeReadOnlyTest() {
            Tracing.TraceMsg("Entering YouTubeReadOnlyTest");

            YouTubeQuery query = new YouTubeQuery(YouTubeQuery.TopRatedVideo);
            YouTubeService service = new YouTubeService("NETUnittests");

            query.Formats.Add(YouTubeQuery.VideoFormat.RTSP);
            query.Time = YouTubeQuery.UploadTime.ThisWeek;

            YouTubeFeed feed = service.Query(query);

            foreach (YouTubeEntry e in feed.Entries) {
                Assert.IsTrue(e.Media.Title.Value != null, "There should be a title");
            }
        }

        [Test]
        public void YouTubeInsertTest() {
            Tracing.TraceMsg("Entering YouTubeInsertTest");

            YouTubeService service = new YouTubeService("NETUnittests", this.ytDevKey);
            if (!string.IsNullOrEmpty(this.ytUser)) {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }

            GDataRequestFactory factory = service.RequestFactory as GDataRequestFactory;
            factory.Timeout = 1000000;

            YouTubeEntry entry = new YouTubeEntry();

            entry.MediaSource = new MediaFileSource(Path.Combine(this.resourcePath, "test_movie.mov"), "video/quicktime");
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
            foreach (MediaCategory m in anotherEntry.Media.Categories) {
                if (String.IsNullOrEmpty(m.Value)) {
                    a.Add(m);
                }
            }

            foreach (MediaCategory m in a) {
                anotherEntry.Media.Categories.Remove(m);
            }

            Assert.AreEqual(newEntry.Media.Description.Value, anotherEntry.Media.Description.Value, "Description should be equal");
            Assert.AreEqual(newEntry.Media.Keywords.Value, anotherEntry.Media.Keywords.Value, "Keywords should be equal");

            // now update the video
            anotherEntry.MediaSource = new MediaFileSource(Path.Combine(this.resourcePath, "test.mp4"), "video/mp4");
            anotherEntry.Update();

            // now delete the guy again

            newEntry.Delete();
        }

        [Test]
        public void YouTubeRatingsTest() {
            Tracing.TraceMsg("Entering YouTubeRatingsTest");
            string videoOwner = "GoogleDevelopers";

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);

            YouTubeRequest f = new YouTubeRequest(settings);
            // GetVideoFeed gets you a users video feed
            Feed<Video> feed = f.GetVideoFeed(videoOwner);
            // this will get you just the first 25 videos.

            foreach (Video v in feed.Entries) {
                Rating rating = new Rating();
                rating.Value = 1;
                v.YouTubeEntry.Rating = rating;
                YouTubeEntry ratedEntry = f.Service.Insert(new Uri(v.YouTubeEntry.RatingsLink.ToString()), v.YouTubeEntry);
                Assert.AreEqual(1, ratedEntry.Rating.Value, "Rating should be equal to 1");
                break; // we can stop after one
            }
        }

        [Test]
        public void YouTubeYtRatingsTest() {
            Tracing.TraceMsg("Entering YouTubeYtRatingsTest");
            string videoOwner = "GoogleDevelopers";

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);

            YouTubeRequest f = new YouTubeRequest(settings);
            // GetVideoFeed gets you a users video feed
            Feed<Video> feed = f.GetVideoFeed(videoOwner);
            // this will get you just the first 25 videos.

            foreach (Video v in feed.Entries) {
                YtRating rating = new YtRating(YtRating.Like);
                v.YouTubeEntry.YtRating = rating;
                YouTubeEntry ratedEntry = f.Service.Insert(new Uri(v.YouTubeEntry.RatingsLink.ToString()), v.YouTubeEntry);
                Assert.AreEqual(YtRating.Like, ratedEntry.YtRating.RatingValue, "YtRating should be equal to like");
                break; // we can stop after one
            }
        }

        [Test]
        public void YouTubeUploaderTest() {
            Tracing.TraceMsg("Entering YouTubeUploaderTest");

            YouTubeQuery query = new YouTubeQuery();
            query.Uri = new Uri(CreateUri(Path.Combine(this.resourcePath, "uploaderyt.xml")));

            YouTubeService service = new YouTubeService("NETUnittests", this.ytDevKey);
            if (!string.IsNullOrEmpty(this.ytUser)) {
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

        [Test]
        public void YouTubeRequestTest() {
            Tracing.TraceMsg("Entering YouTubeRequestTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);

            YouTubeRequest f = new YouTubeRequest(settings);
            // GetVideoFeed gets you a users video feed
            Feed<Video> feed = f.GetVideoFeed(null);
            // this will get you just the first 25 videos. 
            foreach (Video v in feed.Entries) {
                Assert.IsTrue(v.AtomEntry != null, "There should be an atomentry");
                Assert.IsTrue(v.Title != null, "There should be a title");
                Assert.IsTrue(v.VideoId != null, "There should be a videoID");
            }

            Feed<Video> sfeed = f.GetStandardFeed(YouTubeQuery.MostPopular);

            int iCountOne = 0;
            // this loop gets you all videos in the mostpopular video feeed
            foreach (Video v in sfeed.Entries) {
                Assert.IsTrue(v.AtomEntry != null, "There should be an atomentry");
                Assert.IsTrue(v.Title != null, "There should be a title");
                Assert.IsTrue(v.VideoId != null, "There should be a videoID");
                iCountOne++;
            }
            int iCountTwo = 0;
            sfeed.AutoPaging = true;
            sfeed.Maximum = 50;

            foreach (Video v in sfeed.Entries) {
                Assert.IsTrue(v.AtomEntry != null, "There should be an atomentry");
                Assert.IsTrue(v.Title != null, "There should be a title");
                Assert.IsTrue(v.VideoId != null, "There should be a videoID");
                iCountTwo++;
            }
            Assert.IsTrue(iCountTwo > iCountOne);
        } 

        [Test]
        public void YouTubePlaylistRequestTest() {
            Tracing.TraceMsg("Entering YouTubePlaylistRequestTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);

            YouTubeRequest f = new YouTubeRequest(settings);
            // GetVideoFeed gets you a users video feed
            Feed<Playlist> feed = f.GetPlaylistsFeed(null);

            // this will get you just the first 25 videos. 
            foreach (Playlist p in feed.Entries) {
                Assert.IsTrue(p.AtomEntry != null);
                Assert.IsTrue(p.Title != null);
                Feed<PlayListMember> list = f.GetPlaylist(p);
                foreach (PlayListMember v in list.Entries) {
                    Assert.IsTrue(v.AtomEntry != null, "There should be an atomentry");
                    Assert.IsTrue(v.Title != null, "There should be a title");
                    Assert.IsTrue(v.VideoId != null, "There should be a videoID");
                    // there might be no watchpage (not published yet)
                    // Assert.IsTrue(v.WatchPage != null, "There should be a watchpage");
                }
            }
        }

        [Ignore("not ready yet")]
        [Test]
        public void YouTubePlaylistBatchTest() {
            Tracing.TraceMsg("Entering YouTubePlaylistBatchTest");
            string playlistOwner = "GoogleDevelopers";

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);

            YouTubeRequest f = new YouTubeRequest(settings);
            // GetVideoFeed gets you a users video feed
            Feed<Playlist> feed = f.GetPlaylistsFeed(playlistOwner);
            // this will get you just the first 25 playlists. 

            List<Playlist> list = new List<Playlist>();
            foreach (Playlist p in feed.Entries) {
                list.Add(p);
            }

            Feed<PlayListMember> videos = f.GetPlaylist(list[0]);

            List<PlayListMember> lvideo = new List<PlayListMember>();

            foreach (PlayListMember v in videos.Entries) {
                lvideo.Add(v);
            }

            List<PlayListMember> batch = new List<PlayListMember>();

            PlayListMember toBatch = new PlayListMember();
            toBatch.Id = lvideo[0].Id;
            toBatch.VideoId = lvideo[0].VideoId;
            toBatch.BatchData = new GDataBatchEntryData();
            toBatch.BatchData.Id = "NEWGUY";
            toBatch.BatchData.Type = GDataBatchOperationType.insert;
            batch.Add(toBatch);

            toBatch = lvideo[0];
            toBatch.BatchData = new GDataBatchEntryData();
            toBatch.BatchData.Id = "DELETEGUY";
            toBatch.BatchData.Type = GDataBatchOperationType.delete;
            batch.Add(toBatch);

            Feed<PlayListMember> updatedVideos = f.Batch(batch, videos);

            foreach (Video v in updatedVideos.Entries) {
                Assert.IsTrue(v.BatchData.Status.Code < 300, "one batch operation failed: " + v.BatchData.Status.Reason);
            }
        }

        [Test]
        public void YouTubeCommentRequestTest() {
            Tracing.TraceMsg("Entering YouTubeCommentRequestTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
            // this will get you the first 25 videos, let's retrieve comments for the first one only
            foreach (Video v in feed.Entries) {
                Feed<Comment> list = f.GetComments(v);
                foreach (Comment c in list.Entries) {
                    Assert.IsTrue(c.AtomEntry != null);
                    Assert.IsTrue(c.Title != null);
                }
                break;
            }
        }

        [Test]
        public void YouTubeMaximumTest() {
            Tracing.TraceMsg("Entering YouTubeMaximumTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);
            settings.Maximum = 15;
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
            int iCount = 0;
            // this will get you just the first 15 videos. 
            foreach (Video v in feed.Entries) {
                iCount++;
            }

            Assert.AreEqual(iCount, 15);
        }

        [Test]
        public void YouTubeUnAuthenticatedRequestTest() {
            Tracing.TraceMsg("Entering YouTubeUnAuthenticatedRequestTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey);
            settings.AutoPaging = true;
            settings.Maximum = 50;

            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
            // this will get you the first 25 videos, let's retrieve comments for the first one only 
            foreach (Video v in feed.Entries) {
                Feed<Comment> list = f.GetComments(v);
                foreach (Comment c in list.Entries) {
                    Assert.IsTrue(c.AtomEntry != null);
                    Assert.IsTrue(c.Title != null);
                }
                break;
            }
        }

        [Test]
        public void YouTubeRequestInsertTest() {
            Tracing.TraceMsg("Entering YouTubeRequestInsertTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);
            YouTubeRequest f = new YouTubeRequest(settings);

            Video v = new Video();
            v.Title = "Sample upload";
            v.Description = "This is a test with and & in it";

            MediaCategory category = new MediaCategory("Nonprofit");
            category.Attributes["scheme"] = YouTubeService.DefaultCategory;
            v.Tags.Add(category);
            v.Keywords = "math";
            v.YouTubeEntry.MediaSource = new MediaFileSource(Path.Combine(this.resourcePath, "test_movie.mov"), "video/quicktime");

            Video newVideo = f.Upload(this.ytUser, v);

            newVideo.Title = "This test upload will soon be deleted";
            Video updatedVideo = f.Update(newVideo);

            Assert.AreEqual(updatedVideo.Description, newVideo.Description, "Description should be equal");
            Assert.AreEqual(updatedVideo.Keywords, newVideo.Keywords, "Keywords should be equal");

            newVideo.YouTubeEntry.MediaSource = new MediaFileSource(Path.Combine(this.resourcePath, "test.mp4"), "video/mp4");
            Video last = f.Update(updatedVideo);
            f.Delete(last);
        }

        [Test]
        public void YouTubePageSizeTest() {
            Tracing.TraceMsg("Entering YouTubePageSizeTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);
            settings.PageSize = 15;
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
            int iCount = 0;
            // this will get you just the first 15 videos. 
            foreach (Video v in feed.Entries) {
                iCount++;
                f.Settings.PageSize = 5;
                Feed<Comment> list = f.GetComments(v);
                int i = 0;
                foreach (Comment c in list.Entries) {
                    i++;
                }
                Assert.IsTrue(i <= 5, "the count should be smaller/equal 5");
                Assert.IsTrue(list.PageSize == -1 || list.PageSize == 5, "the returned pagesize should be 5 or -1 as well");
            }

            Assert.AreEqual(iCount, 15, "the outer feed should count 15");
            Assert.AreEqual(feed.PageSize, 15, "outer feed pagesize should be 15");
        }

        [Test]
        public void YouTubePagingTest() {
            Tracing.TraceMsg("Entering YouTubePagingTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);
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

        [Test]
        public void YouTubeGetTest() {
            Tracing.TraceMsg("Entering YouTubeGetTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);
            settings.PageSize = 15;
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetStandardFeed(YouTubeQuery.MostPopular);

            foreach (Video v in feed.Entries) {
                // remove the etag to force a refresh
                v.YouTubeEntry.Etag = null;
                Video refresh = f.Retrieve(v);

                Assert.AreEqual(refresh.VideoId, v.VideoId, "The ID values should be equal");
            }
        }

        [Test]
        public void YouTubePrivateTest() {
            Tracing.TraceMsg("Entering YouTubePrivateTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);
            settings.PageSize = 15;
            settings.AutoPaging = true;
            YouTubeRequest f = new YouTubeRequest(settings);

            Feed<Video> feed = f.GetVideoFeed(null);
            Video privateVideo = null;

            foreach (Video v in feed.Entries) {
                if (v.IsDraft == false) {
                    v.YouTubeEntry.Private = true;
                    privateVideo = f.Update(v);
                } else {
                    // there should be a state as well
                    State s = v.YouTubeEntry.State;
                    Assert.IsNotNull(s, "state should not be null");
                    Assert.IsNotNull(s.Reason, "State.Reason should not be null");
                }
            }

            Assert.IsTrue(privateVideo != null, "we should have one private video");
            Assert.IsTrue(privateVideo.YouTubeEntry.Private, "that video should be private");
            privateVideo.YouTubeEntry.Private = false;

            Video ret = f.Update(privateVideo);
            Assert.IsTrue(ret != null, "we should have one private video");
            Assert.IsTrue(!ret.YouTubeEntry.Private, "that video should be not private");
        }

        [Test]
        public void YouTubeGetCategoriesTest() {
            Tracing.TraceMsg("Entering YouTubeGetCategoriesTest");

            AtomCategoryCollection collection = YouTubeQuery.GetYouTubeCategories();

            foreach (YouTubeCategory cat in collection) {
                Assert.IsTrue(cat.Term != null);
                Assert.IsTrue(cat.Assignable || cat.Deprecated || cat.Browsable != null);
                if (cat.Assignable) {
                    Assert.IsTrue(cat.Browsable != null, "Assumption, if its assignable, it's browsable");
                }
            }
        }

        [Test]
        public void YouTubeGetActivitiesTest() {
            ActivitiesQuery query = new ActivitiesQuery();
            query.ModifiedSince = new DateTime(1980, 12, 1);
            YouTubeService service = new YouTubeService("NETUnittests", this.ytDevKey);

            if (!string.IsNullOrEmpty(this.ytUser)) {
                service.Credentials = new GDataCredentials(this.ytUser, this.ytPwd);
            }
            ActivitiesFeed feed = service.Query(query) as ActivitiesFeed;

            foreach (ActivityEntry e in feed.Entries) {
                Assert.IsTrue(e.VideoId != null, "There should be a videoid");
            }
            service = null;
        }

        [Test]
        public void YouTubeRequestActivitiesTest() {
            Tracing.TraceMsg("Entering YouTubeRequestActivitiesTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);
            YouTubeRequest f = new YouTubeRequest(settings);

            // this returns the server default answer
            Feed<Activity> feed = f.GetActivities();

            foreach (Activity a in feed.Entries) {
                Assert.IsTrue(a.VideoId != null, "There should be a VideoId");
            }

            // now let's find all that happened in the last 24 hours

            DateTime t = DateTime.Now.AddDays(-1);

            // this returns the all activities for the last 24 hours
            try {
                Feed<Activity> yesterday = f.GetActivities(t);

                foreach (Activity a in yesterday.Entries) {
                    Assert.IsTrue(a.VideoId != null, "There should be a VideoId");
                }
            } catch (GDataNotModifiedException e) {
                Assert.IsTrue(e != null);
            }

            t = DateTime.Now.AddMinutes(-1);

            // this returns the all activities for the last 1 minute, should be empty or throw a not modified

            try {
                Feed<Activity> lastmin = f.GetActivities(t);
                int iCount = 0;

                foreach (Activity a in lastmin.Entries) {
                    iCount++;
                }

                Assert.IsTrue(iCount == 0, "There should be no activity for the last minute");
            } catch (GDataNotModifiedException e) {
                Assert.IsTrue(e != null);
            }
        }

        [Test]
        public void YouTubeSubscriptionsTest() {
            Tracing.TraceMsg("Entering YouTubeSubscriptionsTest");
            string channelUsername = "GoogleDevelopers";

            YouTubeRequestSettings settings = new YouTubeRequestSettings(this.ApplicationName, this.ytDevKey, this.ytUser, this.ytPwd);
            YouTubeRequest f = new YouTubeRequest(settings);

            // this returns the server default answer
            Feed<Subscription> feed = f.GetSubscriptionsFeed(null);

            foreach (Subscription s in feed.Entries) {
                if (!string.IsNullOrEmpty(s.UserName) && s.UserName == channelUsername) {
                    f.Delete(s);
                }
            }

            Subscription sub = new Subscription();
            sub.Type = SubscriptionEntry.SubscriptionType.channel;
            sub.UserName = "GoogleDevelopers";

            f.Insert(feed, sub);

            // this returns the server default answer
            feed = f.GetSubscriptionsFeed(null);
            List<Subscription> list = new List<Subscription>();

            foreach (Subscription s in feed.Entries) {
                if (!string.IsNullOrEmpty(s.UserName) && s.UserName == channelUsername) {
                    list.Add(s);
                }
            }

            Assert.IsTrue(list.Count > 0, "There should be one subscription matching");

            foreach (Subscription s in list) {
                f.Delete(s);
            }

            feed = f.GetSubscriptionsFeed(null);
            int iCount = 0;

            foreach (Subscription s in feed.Entries) {
                iCount++;
            }

            Assert.IsTrue(iCount == 0, "There should be no subscriptions in the feed");
        }

        [Test]
        public void YouTubeUserActivitiesTest() {
            Tracing.TraceMsg("Entering YouTubeUserActivitiesTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey);
            YouTubeRequest f = new YouTubeRequest(settings);

            List<string> users = new List<string>();

            users.Add("whiskeytonsils");
            users.Add("joelandberry");

            // this returns the server default answer
            Feed<Activity> feed = f.GetActivities(users);

            foreach (Activity a in feed.Entries) {
                VerifyActivity(a);
            }

            // now let's find all that happened in the last 24 hours

            DateTime t = DateTime.Now.AddDays(-1);

            // this returns the all activities for the last 24 hours
            try {
                Feed<Activity> yesterday = f.GetActivities(users, t);

                foreach (Activity a in yesterday.Entries) {
                    VerifyActivity(a);
                }
            } catch (GDataNotModifiedException e) {
                Assert.IsTrue(e != null);
            }

            t = DateTime.Now.AddMinutes(-1);

            // this returns all activities for the last 1 minute, should be empty or throw a not modified

            try {
                Feed<Activity> lastmin = f.GetActivities(users, t);
                int iCount = 0;

                foreach (Activity a in lastmin.Entries) {
                    iCount++;
                }
                Assert.IsTrue(iCount == 0, "There should be no activity for the last minute");
            } catch (GDataNotModifiedException e) {
                Assert.IsTrue(e != null);
            }
        }

        [Test]
        public void YouTubeAccessControlTest() {
            Tracing.TraceMsg("Entering YouTubeAccessControlTest");

            YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytDevKey, this.ytUser, this.ytPwd);
            YouTubeRequest f = new YouTubeRequest(settings);

            Video v = new Video();
            v.Title = "Sample upload";
            v.Description = "This is a test with different access control values";

            MediaCategory category = new MediaCategory("Nonprofit");
            category.Attributes["scheme"] = YouTubeService.DefaultCategory;
            v.Tags.Add(category);
            v.Keywords = "math";
            v.YouTubeEntry.MediaSource = new MediaFileSource(Path.Combine(this.resourcePath, "test_movie.mov"), "video/quicktime");

            v.YouTubeEntry.AccessControls.Add(new YtAccessControl(YtAccessControl.RateAction, YtAccessControl.DeniedPermission));
            v.YouTubeEntry.AccessControls.Add(new YtAccessControl(YtAccessControl.CommentAction, YtAccessControl.ModeratedPermission));

            Video newVideo = f.Upload(this.ytUser, v);
            ExtensionCollection<YtAccessControl> acl = newVideo.YouTubeEntry.AccessControls;
            for (int i = 0; i < acl.Count; i++) {
                YtAccessControl ac = acl[i];
                switch (ac.Action) {
                    case YtAccessControl.RateAction:
                        Assert.AreEqual(ac.Permission, YtAccessControl.DeniedPermission, "Rating should be denied");
                        break;
                    case YtAccessControl.CommentAction:
                        Assert.AreEqual(ac.Permission, YtAccessControl.ModeratedPermission, "Comments should be moderated");
                        break;
                    case YtAccessControl.CommentVoteAction:
                        Assert.AreEqual(ac.Permission, YtAccessControl.AllowedPermission, "Comment rating should be allowed");
                        break;
                    case YtAccessControl.VideoRespondAction:
                        Assert.AreEqual(ac.Permission, YtAccessControl.ModeratedPermission, "Video responses should be moderated");
                        break;
                    case YtAccessControl.ListAction:
                        Assert.AreEqual(ac.Permission, YtAccessControl.AllowedPermission, "Video listing should be allowed");
                        break;
                    case YtAccessControl.EmbedAction:
                        Assert.AreEqual(ac.Permission, YtAccessControl.AllowedPermission, "Video embed should be allowed");
                        break;
                    case YtAccessControl.SyndicateAction:
                        Assert.AreEqual(ac.Permission, YtAccessControl.AllowedPermission, "Video syndicate should be allowed");
                        break;
                }
            }

            f.Delete(newVideo);
        }

        private void VerifyActivity(Activity a) {
            switch (a.Type) {
                case ActivityType.Favorited:
                case ActivityType.Rated:
                case ActivityType.Shared:
                case ActivityType.Commented:
                case ActivityType.Uploaded:
                    Assert.IsTrue(a.VideoId != null, "There should be a VideoId");
                    break;
                case ActivityType.FriendAdded:
                case ActivityType.SubscriptionAdded:
                    Assert.IsTrue(a.Username != null, "There should be a username");
                    break;
            }
        }

        static void printVideoEntry(Video video) {
            Console.WriteLine("Title: " + video.Title);
            Console.WriteLine(video.Description);
            Console.WriteLine("Keywords: " + video.Keywords);
            Console.WriteLine("Uploaded by: " + video.Uploader);

            if (video.YouTubeEntry.Location != null) {
                Console.WriteLine("Latitude: " + video.YouTubeEntry.Location.Latitude);
                Console.WriteLine("Longitude: " + video.YouTubeEntry.Location.Longitude);
            }

            if (video.Media != null && video.Media.Rating != null) {
                Console.WriteLine("Restricted in: " + video.Media.Rating.Country);
            }

            if (video.IsDraft) {
                Console.WriteLine("Video is not live.");

                string stateName = video.Status.Name;
                if (stateName == "processing") {
                    Console.WriteLine("Video is still being processed.");
                } else if (stateName == "rejected") {
                    Console.Write("Video has been rejected because: ");
                    Console.WriteLine(video.Status.Value);
                    Console.Write("For help visit: ");
                    Console.WriteLine(video.Status.Help);
                } else if (stateName == "failed") {
                    Console.Write("Video failed uploading because:");
                    Console.WriteLine(video.Status.Value);
                    Console.Write("For help visit: ");
                    Console.WriteLine(video.Status.Help);
                }
            }

            if (video.AtomEntry.EditUri != null) {
                Console.WriteLine("Video is editable by the current user.");
            }

            if (video.Rating != -1) {
                Console.WriteLine("Average rating: " + video.Rating);
            }

            if (video.ViewCount != -1) {
                Console.WriteLine("View count: " + video.ViewCount);
            }

            Console.WriteLine("Thumbnails:");
            foreach (MediaThumbnail thumbnail in video.Thumbnails) {
                Console.WriteLine("\tThumbnail URL: " + thumbnail.Url);
                Console.WriteLine("\tThumbnail time index: " + thumbnail.Time);
            }

            Console.WriteLine("Media:");
            foreach (Google.GData.YouTube.MediaContent mediaContent in video.Contents) {
                Console.WriteLine("\tMedia Location: " + mediaContent.Url);
                Console.WriteLine("\tMedia Type: " + mediaContent.Format);
                Console.WriteLine("\tDuration: " + mediaContent.Duration);
            }
        }
    }

    [TestFixture]
    [Category("LiveTest")]
    public class YouTubeVerticalTestSuite : BaseLiveTestClass {

        private string ytClient;
        private string ytDevKey;
        private string ytUser;
        private string ytPwd;

        public YouTubeVerticalTestSuite() {
        }

        public override string ServiceName {
            get {
                return ServiceNames.YouTube;
            }
        }

        protected override void ReadConfigFile() {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("youTubeClientID")) {
                this.ytClient = (string)unitTestConfiguration["youTubeClientID"];
            }
            if (unitTestConfiguration.Contains("youTubeDevKey")) {
                this.ytDevKey = (string)unitTestConfiguration["youTubeDevKey"];
            }
            if (unitTestConfiguration.Contains("youTubeUser")) {
                this.ytUser = (string)unitTestConfiguration["youTubeUser"];
            }
            if (unitTestConfiguration.Contains("youTubePwd")) {
                this.ytPwd = (string)unitTestConfiguration["youTubePwd"];
            }
        }
    }
}
