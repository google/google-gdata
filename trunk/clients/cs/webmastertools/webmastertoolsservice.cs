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

using System;
using Google.GData.Client;

namespace Google.GData.WebmasterTools
{
    public class WebmasterToolsService : Service
    {
        /// <summary>The Webmaster Tools service's name</summary> 
        public const string GWebmasterToolsService = "sitemaps";

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public WebmasterToolsService(string applicationName)
            : base(GWebmasterToolsService, applicationName)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed); 
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>CrawlIssuesFeed</returns>
        public CrawlIssuesFeed Query(CrawlIssuesQuery feedQuery)
        {
            return base.Query(feedQuery) as CrawlIssuesFeed;
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>KeywordsFeed</returns>
        public KeywordsFeed Query(KeywordsQuery feedQuery)
        {
            return base.Query(feedQuery) as KeywordsFeed;
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>MessagesFeed</returns>
        public MessagesFeed Query(MessagesQuery feedQuery)
        {
            return base.Query(feedQuery) as MessagesFeed;
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>SitemapsFeed</returns>
        public SitemapsFeed Query(SitemapsQuery feedQuery)
        {
            return base.Query(feedQuery) as SitemapsFeed;
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>SitesFeed</returns>
        public SitesFeed Query(SitesQuery feedQuery)
        {
            return base.Query(feedQuery) as SitesFeed;
        }

        /// <summary>
        /// by default all services now use version 1 for the protocol.
        /// this needs to be overridden by a service to specify otherwise. 
        /// YouTube uses version 2
        /// </summary>
        /// <returns></returns>
        protected override void InitVersionInformation()
        {
            this.ProtocolMajor = VersionDefaults.VersionTwo;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>eventchaining. We catch this by from the base service, which 
        /// would not by default create an atomFeed</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Webmaster Tools Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            if (e.Uri.AbsolutePath.IndexOf("feeds") != -1 && e.Uri.AbsolutePath.IndexOf("keywords") != -1)
            {
                // keywords base url: https://www.google.com/webmasters/tools/feeds/siteID/keywords/
                e.Feed = new KeywordsFeed(e.Uri, e.Service);
            }
            else if (e.Uri.AbsolutePath.IndexOf("feeds/messages/") != -1)
            {
                // messages feeds are https://www.google.com/webmasters/tools/feeds/messages/
                e.Feed = new MessagesFeed(e.Uri, e.Service);
            }
            else if (e.Uri.AbsolutePath.IndexOf("feeds/sites/") != -1)
            {
                // messages feeds are https://www.google.com/webmasters/tools/feeds/sites/
                e.Feed = new SitesFeed(e.Uri, e.Service);
            }
            else if (e.Uri.AbsolutePath.IndexOf("feeds") != -1 && e.Uri.AbsolutePath.IndexOf("crawlissues") != -1)
            {
                // crawl issues are https://www.google.com/webmasters/tools/feeds/siteID/crawlissues/
                e.Feed = new CrawlIssuesFeed(e.Uri, e.Service);
            }
            else if (e.Uri.AbsolutePath.IndexOf("feeds") != -1 &&
                    e.Uri.AbsolutePath.IndexOf("sitemaps") != -1)
            {
                // crawl issues are https://www.google.com/webmasters/tools/feeds/siteID/sitemaps/
                e.Feed = new SitemapsFeed(e.Uri, e.Service);
            }
            
        }
        /////////////////////////////////////////////////////////////////////////////
    }
}
