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
using Google.GData.WebmasterTools;

namespace Google.WebmasterTools
{
    public class CrawlIssues : Entry
    {
        #region Overrides of Entry

        /// <summary>
        /// creates the crawlissues object when needed
        /// </summary>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new CrawlIssuesEntry();
            }
        }

        /// <summary>
        /// readonly accessor to the typed underlying atom object
        /// </summary>
        public CrawlIssuesEntry CrawlIssuesEntry
        {
            get
            {
                return this.AtomEntry as CrawlIssuesEntry;
            }
        }
        #endregion
    }

    public class Keywords : Entry
    {
        #region Overrides of Entry

        /// <summary>
        /// creates the keywords object when needed
        /// </summary>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new KeywordsEntry();
            }
        }

        /// <summary>
        /// readonly accessor to the typed underlying atom object
        /// </summary>
        public KeywordsEntry KeywordsEntry
        {
            get
            {
                return this.AtomEntry as KeywordsEntry;
            }
        }
        #endregion
    }

    public class Messages : Entry
    {
        #region Overrides of Entry

        /// <summary>
        /// creates the messages object when needed
        /// </summary>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new MessagesEntry();
            }
        }

        /// <summary>
        /// readonly accessor to the typed underlying atom object
        /// </summary>
        public MessagesEntry MessagesEntry
        {
            get
            {
                return this.AtomEntry as MessagesEntry;
            }
        }
        #endregion
    }

    public class Sitemap : Entry
    {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new SitemapsEntry();
            }
        }
        /// <summary>
        /// readonly accessor to the typed underlying atom object
        /// </summary>
        public SitemapsEntry SitemapsEntry
        {
            get
            {
                return this.AtomEntry as SitemapsEntry;
            }
        }

        public string Mobile
        {
            get
            {
                EnsureInnerObject();
                return this.SitemapsEntry.Mobile;
            }
            set
            {
                EnsureInnerObject();
                this.SitemapsEntry.Mobile = value;
            }
        }

        public string SitemapLastDownloaded
        {
            get
            {
                EnsureInnerObject();
                return this.SitemapsEntry.SitemapLastDownloaded;
            }
            set
            {
                EnsureInnerObject();
                this.SitemapsEntry.SitemapLastDownloaded = value;
            }
        }

        public string SitemapMobile
        {
            get
            {
                EnsureInnerObject();
                return this.SitemapsEntry.SitemapMobile;
            }
            set
            {
                EnsureInnerObject();
                this.SitemapsEntry.SitemapMobile = value;
            }
        }

        public string SitemapMobileMarkupLanguage
        {
            get
            {
                EnsureInnerObject();
                return this.SitemapsEntry.SitemapMobileMarkupLanguage;
            }
            set
            {
                EnsureInnerObject();
                this.SitemapsEntry.SitemapMobileMarkupLanguage = value;
            }
        }

        public string SitemapNews
        {
            get
            {
                EnsureInnerObject();
                return this.SitemapsEntry.SitemapNews;
            }
            set
            {
                EnsureInnerObject();
                this.SitemapsEntry.SitemapNews = value;
            }
        }

        public string SitemapNewsPublicationLabel
        {
            get
            {
                EnsureInnerObject();
                return this.SitemapsEntry.SitemapNewsPublicationLabel;
            }
            set
            {
                EnsureInnerObject();
                this.SitemapsEntry.SitemapNewsPublicationLabel = value;
            }
        }

        public string SitemapType
        {
            get
            {
                EnsureInnerObject();
                return this.SitemapsEntry.SitemapType;
            }
            set
            {
                EnsureInnerObject();
                this.SitemapsEntry.SitemapType = value;
            }
        }

        public string SitemapUrlCount
        {
            get
            {
                EnsureInnerObject();
                return this.SitemapsEntry.SitemapUrlCount;
            }
            set
            {
                EnsureInnerObject();
                this.SitemapsEntry.SitemapUrlCount = value;
            }
        }
    }

    public class Sites : Entry
    {
        /// <summary>
        /// creates the sites object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new SitesEntry();
            }
        }

        /// <summary>
        /// readonly accessor to the typed underlying atom object
        /// </summary>
        public SitesEntry SitesEntry
        {
            get
            {
                return this.AtomEntry as SitesEntry;
            }
        }

        /// <summary>
        /// getter/setter for Crawl Rate subelement
        /// </summary>
        public string CrawlRate
        {
            get
            {
                EnsureInnerObject();
                return this.SitesEntry.CrawlRate;
            }
            set
            {
                EnsureInnerObject();
                this.SitesEntry.CrawlRate = value;
            }
        }

        /// <summary>
        /// getter/setter for Geolocation subelement
        /// </summary>
        public string GeoLocation
        {
            get
            {
                EnsureInnerObject();
                return this.SitesEntry.GeoLocation;
            }
            set
            {
                EnsureInnerObject();
                this.SitesEntry.GeoLocation = value;
            }
        }

        /// <summary>
        /// getter/setter for Preferred-Domain subelement
        /// </summary>
        public string PreferredDomain
        {
            get
            {
                EnsureInnerObject();
                return this.SitesEntry.PreferredDomain;
            }
            set
            {
                EnsureInnerObject();
                this.SitesEntry.PreferredDomain = value;
            }
        }

        /// <summary>
        /// getter/setter for Verification-Method subelement
        /// </summary>
        public VerificationMethod VerificationMethod
        {
            get
            {
                EnsureInnerObject();
                return this.SitesEntry.VerificationMethod;
            }
            set
            {
                EnsureInnerObject();
                this.SitesEntry.VerificationMethod = value;
            }
        }

        /// <summary>
        /// getter/setter for Verified subelement
        /// </summary>
        public string Verified
        {
            get
            {
                EnsureInnerObject();
                return this.SitesEntry.Verified;
            }
            set
            {
                EnsureInnerObject();
                this.SitesEntry.Verified = value;
            }
        }
    }

    public class WebmasterToolsRequest : FeedRequest<WebmasterToolsService>
    {
        /// <summary>
        /// default constructor for a WebmasterToolsRequest
        /// </summary>
        /// <param name="settings"></param>
        public WebmasterToolsRequest(RequestSettings settings)
            : base(settings)
        {
            this.Service = new WebmasterToolsService(settings.Application);
            PrepareService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteId">the id of the site</param>
        /// <returns>a feed of CrawlIssues objects</returns>
        public Feed<CrawlIssues> GetCrawlIssues(string siteId)
        {
            CrawlIssuesQuery q = PrepareQuery<CrawlIssuesQuery>(CrawlIssuesQuery.CreateCustomUri(siteId));
            return PrepareFeed<CrawlIssues>(q);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteId">the id of the site</param>
        /// <returns>a feed of Keywords objects</returns>
        public Feed<Keywords> GetKeywords(string siteId)
        {
            KeywordsQuery q = PrepareQuery<KeywordsQuery>(KeywordsQuery.CreateCustomUri(siteId));
            return PrepareFeed<Keywords>(q);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>a feed of Messages objects</returns>
        public Feed<Messages> GetMessages()
        {
            MessagesQuery q = PrepareQuery<MessagesQuery>(MessagesQuery.HttpsFeedUrl);
            return PrepareFeed<Messages>(q);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteId">the id of the site</param>
        /// <returns>a feed of Sitemap objects</returns>
        public Feed<Sitemap> GetSitemaps(string siteId)
        {
            SitemapsQuery q = PrepareQuery<SitemapsQuery>(SitemapsQuery.CreateCustomUri(siteId));
            return PrepareFeed<Sitemap>(q);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteId">the id of the site</param>
        /// <param name="sitemap"></param>
        /// <returns></returns>
        public Sitemap AddSitemap(string siteId, Sitemap sitemap)
        {
            Sitemap s = null;

            if (sitemap.AtomEntry != null)
            {
                Uri target = CreateUri(SitemapsQuery.CreateCustomUri(siteId));
                s = new Sitemap();
                s.AtomEntry = this.Service.Insert(target, sitemap.AtomEntry);
            }
            return s;
        }

        /// <summary>
        /// returns a Feed of sites for the authenticated user
        /// </summary>
        /// <returns>a feed of Sites objects</returns>
        public Feed<Sites> GetSites()
        {
            SitesQuery q = PrepareQuery<SitesQuery>(SitesQuery.HttpsFeedUrl);
            return PrepareFeed<Sites>(q);
        }

        /// <summary>
        /// add a new site to account
        /// </summary>
        /// <param name="s">the sites to add</param>
        /// <returns>the created site</returns>
        public Sites AddSite(Sites s)
        {
            Sites site = null;

            if(s != null)
            {
                Uri target = CreateUri(SitesQuery.HttpsFeedUrl);
                site = new Sites();
                site.AtomEntry = this.Service.Insert(target, s.AtomEntry);
            }

            return site;
        }

        /// <summary>
        /// updates a site entry
        /// </summary>
        /// <param name="entry">the entry to update</param>
        /// <param name="siteId">the id of the site</param>
        /// <returns>the updated site entry</returns>
        public SitesEntry UpdateSiteEntry(Sites entry, string siteId)
        {
            SitesEntry sites = null;
            if (entry != null)
            {
                Uri target = CreateUri(SitesQuery.CreateCustomUri(siteId));
                entry.AtomEntry.EditUri = target;
                sites = this.Service.Update(entry.SitesEntry);
            }
            return sites;
        }
    }
}
