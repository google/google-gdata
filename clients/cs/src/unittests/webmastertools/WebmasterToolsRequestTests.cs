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
using Google.WebmasterTools;
using NUnit.Framework;

namespace unittests.webmastertools
{
    [TestFixture]
    public class WebmasterToolsRequestTests : BaseLiveTestClass
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
        public void TestWebmasterToolsCrawlIssuesRequest()
        {
            Tracing.TraceMsg("Entering Webmaster Tools Crawl Issues RequestTest");

            RequestSettings settings = new RequestSettings("NETUnittests", this.userName, this.passWord);
            WebmasterToolsRequest f = new WebmasterToolsRequest(settings);

            Feed<CrawlIssues> crawlIssues = f.GetCrawlIssues("http%3A%2F%2Fwww%2Eexample%2Ecom%2F");
            foreach (CrawlIssues crawlIssue in crawlIssues.Entries)
            {
                Assert.IsTrue(crawlIssue.AtomEntry != null, "There should be an atomentry");
            }
        }

        [Test]
        public void TestWebmasterToolsKeywordsRequest()
        {
            Tracing.TraceMsg("Entering Webmaster Tools Keywords RequestTest");

            RequestSettings settings = new RequestSettings("NETUnittests", this.userName, this.passWord);
            WebmasterToolsRequest f = new WebmasterToolsRequest(settings);

            Feed<Keywords> keywords = f.GetKeywords("http%3A%2F%2Fwww%2Eexample%2Ecom%2F");
            foreach (Keywords keyword in keywords.Entries)
            {
                Assert.IsTrue(keyword.AtomEntry != null, "There should be an atomentry");
            }
        }

        [Test]
        public void TestWebmasterToolsMessagesRequest()
        {
            Tracing.TraceMsg("Entering Webmaster Tools Keywords RequestTest");

            RequestSettings settings = new RequestSettings("NETUnittests", this.userName, this.passWord);
            WebmasterToolsRequest f = new WebmasterToolsRequest(settings);

            Feed<Messages> messages = f.GetMessages();
            foreach (Messages message in messages.Entries)
            {
                Assert.IsTrue(message.AtomEntry != null, "There should be an atomentry");
            }
        }

        [Test]
        public void TestWebmasterToolsSitemapsRequest()
        {
            Tracing.TraceMsg("Entering Webmaster Tools Sitemap RequestTest");

            RequestSettings settings = new RequestSettings("NETUnittests", this.userName, this.passWord);
            WebmasterToolsRequest f = new WebmasterToolsRequest(settings);

            Feed<Sitemap> sitemaps = f.GetSitemaps("http%3A%2F%2Fwww%2Eexample%2Ecom%2F");
            foreach (Sitemap sitemap in sitemaps.Entries)
            {
                Assert.IsTrue(sitemap.AtomEntry != null, "There should be an atomentry");
            }
        }

        [Test]
        public void TestWebmasterToolsSitesRequest()
        {
            Tracing.TraceMsg("Entering Webmaster Tools Sites RequestTest");

            RequestSettings settings = new RequestSettings("NETUnittests", this.userName, this.passWord);
            WebmasterToolsRequest f = new WebmasterToolsRequest(settings);

            Feed<Sites> sites = f.GetSites();
            foreach (Sites site in sites.Entries)
            {
                Assert.IsTrue(site.AtomEntry != null, "There should be an atomentry");
            }
        }
    }
}
