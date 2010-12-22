/* Copyright (c) 2007 Google Inc.
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
using System.Text;
using System.Xml;
using System.Collections;
using System.Net;
using System.IO;
using Google.GData.Client;

using Google.GData.Extensions.Apps;

namespace Google.GData.Apps
{
    /// <summary>
    /// The EmailListService class extends the AppsService abstraction
    /// to define a service that is preconfigured for access to Google Apps
    /// email list feeds.
    /// </summary>
    public class EmailListService : Service
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public EmailListService(string applicationName)
            : base(AppsNameTable.GAppsService, applicationName)
        {
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEmailListEntry);

            this.NewFeed += new ServiceEventHandler(this.OnParsedNewFeed);

            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        /// <summary>
        /// overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved EmailListFeed</returns>
        public EmailListFeed Query(EmailListQuery feedQuery)
        {
            try
            {
                Stream feedStream = Query(feedQuery.Uri);
                EmailListFeed feed = new EmailListFeed(feedQuery.Uri, this);
                feed.Parse(feedStream, AlternativeFormat.Atom);
                feedStream.Close();

                if (feedQuery.RetrieveAllEmailLists)
                {
                    AtomLink next, prev = null;
                    while ((next = feed.Links.FindService("next", null)) != null && next != prev)
                    {
                        feedStream = Query(new Uri(next.HRef.ToString()));
                        feed.Parse(feedStream, AlternativeFormat.Atom);
                        feedStream.Close();

                        prev = next;
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
        /// Inserts a new email list entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public EmailListEntry Insert(EmailListFeed feed, EmailListEntry entry)
        {
            try
            {
                return base.Insert(feed, entry) as EmailListEntry;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Inserts a new email list entry into the feed at the
        /// specified URI.
        /// </summary>
        /// <param name="feedUri">the URI of the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public EmailListEntry Insert(Uri feedUri, EmailListEntry entry)
        {
            try
            {
                return base.Insert(feedUri, entry) as EmailListEntry;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Overrides the base Update() method to throw an
        /// exception, because email list entries cannot be
        /// updated.
        /// </summary>
        /// <param name="entry">the entry the user is trying to update</param>
        public new AtomEntry Update(AtomEntry entry)
        {
            throw new GDataRequestException("Email list entries cannot be updated.");
        }

        /// <summary>
        /// Overridden Delete method that throws AppsException
        /// </summary>
        /// <param name="uri">the URI to delete</param>
        public new void Delete(Uri uri)
        {
            try
            {
                base.Delete(uri);
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Overridden Delete method that throws AppsException
        /// </summary>
        /// <param name="entry">the EmailListEntry to delete</param>
        public void Delete(EmailListEntry entry)
        {
            try
            {
                base.Delete(entry);
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Event handler. Called when a new list entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the evet</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewEmailListEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry)
            {
                e.Entry = new EmailListEntry();
            }
        }



        /// <summary>

        /// Feed handler.  Instantiates a new <code>EmailListFeed</code>.

        /// </summary>

        /// <param name="sender">the object that's sending the evet</param>

        /// <param name="e"><code>ServiceEventArgs</code>, holds the feed</param>

        protected void OnParsedNewFeed(object sender, ServiceEventArgs e)

        {

            Tracing.TraceMsg("Created new email list feed");

            if (e == null)

            {

                throw new ArgumentNullException("e");

            }

            e.Feed = new EmailListFeed(e.Uri, e.Service);

        }
    }
}
