/* Copyright (c) 2007-2008 Google Inc.
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
using System;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions.Apps;
using System.Collections.Generic;

namespace Google.GData.Apps.Migration
{
    /// <summary>
    /// Base service for accessing mail item feeds from the
    /// Google Apps Domain Migration API.
    /// </summary>
    public class MailItemService : Service
    {
        /// <summary>
        /// Feed for inserting mail messages.
        /// </summary>
        public const string mailFeedUriSuffix = "/mail";

        /// <summary>
        /// Suffix to insert mail messages in a batch.
        /// </summary>
        public const string batchFeedUriSuffix = "/batch";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="domain">The hosted domain in which a migration is
        /// being set up</param>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public MailItemService(string domain, string applicationName)
            : base(AppsNameTable.GAppsService, applicationName)
        {
            this.domain = domain;

            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewMailItemEntry);
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);

            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        private string domain;

        /// <summary>
        /// Accessor for Domain property.
        /// </summary>
        public string Domain
        {
            get { return domain; }
        }
            /// <summary>
        /// Inserts one or more mail item entries in a single batched request.
        /// Use this method to reduce HTTP overhead when inserting many emails
        /// in a single transfer.
        /// </summary>
        /// <param name="domain">the domain into which to migrate mail</param>
        /// <param name="entries">the mail messages to batch insert</param>
        /// <param name="userName">the user for whom this should be done</param>
        /// <returns>a <code>MailItemFeed</code> containing the results of the
        /// batch insertion</returns>
        public MailItemFeed Batch(string domain, string userName, MailItemEntry[] entries)
        {
            return Batch(domain, userName, new List<MailItemEntry>(entries));
        }
        
        /// <summary>
        /// Inserts one or more mail item entries in a single batched request.
        /// Use this method to reduce HTTP overhead when inserting many emails
        /// in a single transfer.
        /// </summary>
        /// <param name="domain">the domain into which to migrate mail</param>
        /// <param name="entries">the mail messages to batch insert</param>
        /// <param name="userName">the user for whom this should be done</param>
        /// <returns>a <code>MailItemFeed</code> containing the results of the
        /// batch insertion</returns>
        public MailItemFeed Batch(string domain, string userName, List<MailItemEntry> entries)
        {
            Uri batchUri = new Uri(AppsMigrationNameTable.AppsMigrationBaseFeedUri + "/" + domain +
                "/" + userName + mailFeedUriSuffix + batchFeedUriSuffix);
            MailItemFeed feed = new MailItemFeed(batchUri, this);

            foreach (MailItemEntry entry in entries)
            {
                feed.Entries.Add(entry);
            }

            return base.Batch(feed, batchUri) as MailItemFeed;
        }

        /// <summary>
        /// Event handler. Called when a new migration entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the evet</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewMailItemEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry)
            {
                e.Entry = new MailItemEntry();
            }
        }

        /// <summary>
        /// Overridden so that new feeds are returned as <code>MailItemFeed</code>s
        /// instead of base <code>AtomFeed</code>s.
        /// </summary>
        /// <param name="sender"> the object which sent the event</param>
        /// <param name="e">FeedParserEventArguments, holds the FeedEntry</param> 
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Mail Item Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            e.Feed = new MailItemFeed(e.Uri, e.Service);
        }
    }
}
