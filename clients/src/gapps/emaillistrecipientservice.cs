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
    /// The EmailListRecipientService class extends the AppsService abstraction
    /// to define a service that is preconfigured for access to Google Apps
    /// email list feeds.
    /// </summary>
    public class EmailListRecipientService : Service
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public EmailListRecipientService(string applicationName)
            : base(AppsNameTable.GAppsService, applicationName)
        {
            this.NewFeed += new ServiceEventHandler(this.OnParsedNewFeed);
        }

        /// <summary>
        /// overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved EmailListRecipientFeed</returns>
        public EmailListRecipientFeed Query(EmailListRecipientQuery feedQuery)
        {
            try
            {
                Stream feedStream = Query(feedQuery.Uri);
                EmailListRecipientFeed feed = new EmailListRecipientFeed(feedQuery.Uri, this);
                feed.Parse(feedStream, AlternativeFormat.Atom);
                feedStream.Close();

                if (feedQuery.RetrieveAllRecipients)
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

         //////////////////////////////////////////////////////////////////////
        /// <summary>eventchaining. We catch this by from the base service, which 
        /// would not by default create an atomFeed</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Picasa Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }

         
            e.Feed = new EmailListRecipientFeed(e.Uri, e.Service);
        }
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Inserts a new email list recipient entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public EmailListRecipientEntry Insert(EmailListRecipientFeed feed, EmailListRecipientEntry entry)
        {
            try
            {
                return base.Insert(feed, entry) as EmailListRecipientEntry;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Inserts a new email list recipient entry into the feed at the
        /// specified URI.
        /// </summary>
        /// <param name="feedUri">the URI of the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public EmailListRecipientEntry Insert(Uri feedUri, EmailListRecipientEntry entry)
        {
            try
            {
                return base.Insert(feedUri, entry) as EmailListRecipientEntry;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Overrides the base Update() method to throw an
        /// exception, because email list recipient entries cannot be
        /// updated.
        /// </summary>
        /// <param name="entry">the entry the user is trying to update</param>
        public new AtomEntry Update(AtomEntry entry)
        {
            throw new GDataRequestException("Email list recipient entries cannot be updated.");
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
        /// <param name="entry"></param>
        public void Delete(EmailListRecipientEntry entry)
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
        /// Feed handler.  Instantiates a new <code>EmailListRecipientFeed</code>.
        /// </summary>
        /// <param name="sender">the object that's sending the evet</param>
        /// <param name="e"><code>ServiceEventArgs</code>, holds the feed</param>
        protected void OnParsedNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new email list recipient feed");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            e.Feed = new EmailListRecipientFeed(e.Uri, e.Service);
        }
    }
}
