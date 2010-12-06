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

/* 
 * Created by Morten Christensen, http://blog.sitereactor.dk | http://twitter.com/sitereactor
 */

using Google.GData.Client;
using Google.GData.Client.LiveTests;
using Google.GData.Client.UnitTests;
using Google.GData.WebmasterTools;
using NUnit.Framework;

namespace unittests.webmastertools
{
    [TestFixture]
    public class WebmasterToolsServiceTest : BaseLiveTestClass
    {
        private Google.GData.Client.UnitTests.TestContext TestContext1;

        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("userName") == true)
            {
                this.userName = (string)unitTestConfiguration["userName"];
                Tracing.TraceInfo("Read userName value: " + this.userName);
            }
            if (unitTestConfiguration.Contains("passWord") == true)
            {
                this.passWord = (string)unitTestConfiguration["passWord"];
                Tracing.TraceInfo("Read passWord value: " + this.passWord);
            }
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public Google.GData.Client.UnitTests.TestContext TestContext
        {
            get { return TestContext1; }
            set { TestContext1 = value; }
        }

        [Test]
        public void QuerySites()
        {
            WebmasterToolsService service = new WebmasterToolsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            SitesQuery feedQuery = new SitesQuery();
            SitesFeed feed = service.Query(feedQuery);

            Assert.GreaterOrEqual(feed.Entries.Count, 0);

            foreach (SitesEntry entry in feed.Entries)
            {
                Assert.IsNotNull(entry.Id);
                Assert.IsNotNull(entry.Verified);
            }
        }

        [Test]
        public void QuerySitemaps()
        {
            WebmasterToolsService service = new WebmasterToolsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            SitemapsQuery feedQuery = new SitemapsQuery(SitemapsQuery.CreateCustomUri("http%3A%2F%2Fwww%2Eexample%2Ecom%2F"));
            SitemapsFeed feed = service.Query(feedQuery);

            Assert.GreaterOrEqual(feed.Entries.Count, 0);

            foreach (SitemapsEntry entry in feed.Entries)
            {
                Assert.IsNotNull(entry.Id);
            }
        }

        [Test]
        public void QueryKeywords()
        {
            WebmasterToolsService service = new WebmasterToolsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            KeywordsQuery feedQuery = new KeywordsQuery(KeywordsQuery.CreateCustomUri("http%3A%2F%2Fwww%2Eexample%2Ecom%2F"));
            KeywordsFeed feed = service.Query(feedQuery);

            Assert.GreaterOrEqual(feed.Entries.Count, 0);

            foreach (KeywordsEntry entry in feed.Entries)
            {
                Assert.IsNotNull(entry.Id);
            }
        }

        [Test]
        public void QueryMessages()
        {
            WebmasterToolsService service = new WebmasterToolsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            MessagesQuery feedQuery = new MessagesQuery();
            MessagesFeed feed = service.Query(feedQuery);

            Assert.GreaterOrEqual(feed.Entries.Count, 0);

            foreach (MessagesEntry entry in feed.Entries)
            {
                Assert.IsNotNull(entry.Id);
            }
        }

        [Test]
        public void QueryCrawlIssues()
        {
            WebmasterToolsService service = new WebmasterToolsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            string slug =
                Utilities.EncodeSlugHeader(
                    "https://www.google.com/webmasters/tools/feeds/http%3A%2F%2Fwww%2Eexample%2Ecom%2F/crawlissues/");
            CrawlIssuesQuery feedQuery = new CrawlIssuesQuery(slug);
            CrawlIssuesFeed feed = service.Query(feedQuery);

            Assert.GreaterOrEqual(feed.Entries.Count, 0);

            foreach (CrawlIssuesEntry entry in feed.Entries)
            {
                Assert.IsNotNull(entry.Id);
            }
        }

        [Test]
        public void BasicQuery()
        {
            WebmasterToolsService service = new WebmasterToolsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            string slug =
                Utilities.EncodeSlugHeader(
                    "https://www.google.com/webmasters/tools/feeds/http%3A%2F%2Fwww%2Eexample%2Ecom%2F/crawlissues/");
            FeedQuery query = new FeedQuery(slug);
            AtomFeed feed = service.Query(query);

            Assert.IsNotNull(feed);
        }

    }
}
