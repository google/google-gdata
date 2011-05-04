/* Copyright (c) 2010 Google Inc.
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

using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions.Apps;
using System.IO;

namespace Google.GData.Apps
{
    /// <summary>
    /// A base Service for all property(name-value) based services. 
    /// <example><apps:property name='name' value='value'/></example>
    /// <code>AppsPropertyService</code> works with <code>AppsExtendedFeed</code> and 
    /// <code>AppsExtendedEntry</code>
    /// </summary>
    public class AppsPropertyService : Service
    {
        public AppsPropertyService(string domain, string applicationName)
            : base(AppsNameTable.GAppsService, applicationName)
        {
            this.domain = domain;
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewPropertyEntry);
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        protected string domain;

        /// <summary>
        /// Accessor for Domain property.
        /// </summary>
        public string DomainName
        {
            get { return domain; }
        }

        /// <summary>
        /// Event handler. Called when a new <code>AppsExtendedEntry</code> is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewPropertyEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry && !(e.Entry is AppsExtendedEntry))
            {
                e.Entry = new AppsExtendedEntry();
            }
        }

        /// <summary>
        /// Returns a single page of the feed at the specified URI.
        /// </summary>
        /// <param name="uri">the URI of the feed</param>
        /// <returns></returns>
        public AppsExtendedFeed QueryExtendedFeed(Uri uri)
        {
            //By default, get a single page
            return QueryExtendedFeed(uri, false);
        }

        /// <summary>
        /// Returns the feed at the end URI specified.
        /// </summary>
        /// <param name="uri">the URI of the feed</param>
        /// <param name="shouldGetAllPages">if true, returns all the pages</param>
        /// <returns></returns>
        public AppsExtendedFeed QueryExtendedFeed(Uri uri, Boolean shouldGetAllPages)
        {
            try
            {
                Stream feedStream = base.Query(uri);
                AppsExtendedFeed feed = getFeed(uri, this);
                feed.Parse(feedStream, AlternativeFormat.Atom);
                feedStream.Close();
                if (shouldGetAllPages)
                {
                    if (true)
                    {
                        AtomLink next, prev = null;
                        while ((next = feed.Links.FindService("next", null)) != null
                            && next != prev)
                        {
                            feedStream = base.Query(new Uri(next.HRef.ToString()));
                            feed.Parse(feedStream, AlternativeFormat.Atom);
                            feedStream.Close();
                            prev = next;
                        }
                    }
                }
                return feed;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Overridden so that new feeds are returned as <code>AppsExtendedFeed</code>s
        /// instead of base <code>AtomFeed</code>s.
        /// </summary>
        /// <param name="sender"> the object which sent the event</param>
        /// <param name="e">FeedParserEventArguments, holds the FeedEntry</param> 
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new AppsExtendedFeed");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            e.Feed = new AppsExtendedFeed(e.Uri, e.Service);
        }

        protected virtual AppsExtendedFeed getFeed(Uri uri, IService service) {
            return new AppsExtendedFeed(uri, service);
        }
    }
}
