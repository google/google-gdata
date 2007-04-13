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

namespace Google.GData.Apps
{
    /// <summary>
    /// The NicknameService class extends the AppsService abstraction
    /// to define a service that is preconfigured for access to Google Apps
    /// nickname feeds.
    /// </summary>
    public class NicknameService : Service
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public NicknameService(string applicationName)
            : base(AppsNameTable.GAppsService, applicationName, AppsNameTable.GAppsAgent)
        {
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewNicknameEntry);
            this.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewNicknameExtensionElement);

            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        /// <summary>
        /// overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved NicknameFeed</returns>
        public NicknameFeed Query(NicknameQuery feedQuery)
        {
            try
            {
                Stream feedStream = Query(feedQuery.Uri);
                NicknameFeed feed = new NicknameFeed(feedQuery.Uri, this);
                feed.Parse(feedStream, AlternativeFormat.Atom);
                feedStream.Close();

                if (feedQuery.RetrieveAllNicknames)
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
        /// Inserts a new nickname entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public NicknameEntry Insert(NicknameFeed feed, NicknameEntry entry)
        {
            try
            {
                return base.Insert(feed, entry) as NicknameEntry;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Inserts a new nickname entry into the feed at the
        /// specified URI.
        /// </summary>
        /// <param name="feedUri">the URI of the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public NicknameEntry Insert(Uri feedUri, NicknameEntry entry)
        {
            try
            {
                return base.Insert(feedUri, entry) as NicknameEntry;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Overrides the base Update() method to throw an
        /// exception, because nickname entries cannot be
        /// updated.
        /// </summary>
        /// <param name="entry">the entry the user is trying to update</param>
        public new AtomEntry Update(AtomEntry entry)
        {
            throw new GDataRequestException("Nickname entries cannot be updated.");
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
        /// <param name="entry">the NicknameEntry to delete</param>
        public void Delete(NicknameEntry entry)
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
        protected void OnParsedNewNicknameEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                e.Entry = new NicknameEntry();
            }
        }

        /// <summary>
        /// Event handler.  Called for a nickname extension element.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnNewNicknameExtensionElement(object sender, ExtensionElementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (String.Compare(e.ExtensionElement.NamespaceURI, AppsNameTable.appsNamespace, true) == 0)
            {
                // found G namespace
                e.DiscardEntry = true;
                AtomFeedParser parser = sender as AtomFeedParser;

                if (e.Base.XmlName == AtomParserNameTable.XmlFeedElement)
                {
                }
                else if (e.Base.XmlName == AtomParserNameTable.XmlAtomEntryElement)
                {
                    NicknameEntry entry = e.Base as NicknameEntry;
                    if (entry != null)
                    {
                        entry.ParseNicknameEntry(e.ExtensionElement, parser);
                    }
                }
            }
        }
    }
}
