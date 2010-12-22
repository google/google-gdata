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
    /// The UserService class extends the AppsService abstraction
    /// to define a service that is preconfigured for access to Google Apps
    /// user accounts feeds.
    /// </summary>
    public class UserService : Service
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public UserService(string applicationName)
            : base(AppsNameTable.GAppsService, applicationName)
        {
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewUserEntry);

            this.NewFeed += new ServiceEventHandler(this.OnParsedNewFeed);

            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        /// <summary>
        /// overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved UserFeed</returns>
        public UserFeed Query(UserQuery feedQuery)
        {
            try
            {
                Stream feedStream = Query(feedQuery.Uri);
                UserFeed feed = new UserFeed(feedQuery.Uri, this);
                feed.Parse(feedStream, AlternativeFormat.Atom);
                feedStream.Close();

                if (feedQuery.RetrieveAllUsers)
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
        /// Inserts a new user account entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public UserEntry Insert(UserFeed feed, UserEntry entry)
        {
            try
            {
                return base.Insert(feed, entry) as UserEntry;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Inserts a new user account entry into the feed at the
        /// specified URI.
        /// </summary>
        /// <param name="feedUri">the URI of the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public UserEntry Insert(Uri feedUri, UserEntry entry)
        {
            try
            {
                return base.Insert(feedUri, entry) as UserEntry;
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
        /// <param name="entry">the entry to delete</param>
        public void Delete(UserEntry entry)
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
        protected void OnParsedNewUserEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry)
            {
                e.Entry = new UserEntry();
            }
        }



        /// <summary>

        /// Feed handler.  Instantiates a new <code>UserFeed</code>.

        /// </summary>

        /// <param name="sender">the object that's sending the evet</param>

        /// <param name="e"><code>ServiceEventArgs</code>, holds the feed</param>

        protected void OnParsedNewFeed(object sender, ServiceEventArgs e)

        {

            Tracing.TraceMsg("Created new user feed");

            if (e == null)

            {

                throw new ArgumentNullException("e");

            }

            e.Feed = new UserFeed(e.Uri, e.Service);

        }
    }
}
