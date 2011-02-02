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

using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.ContentForShopping {
    /// <summary>
    /// The ContentForShoppingService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
	/// Google Content API for Shopping.
    /// </summary>
	public class ContentForShoppingService : Service {
        /// <summary>The Content for Shopping service's name</summary>
		public const String GContentForShoppingService = "structuredcontent";

        private string homepage = "";
        public string Homepage
        {
            get { return homepage; }
            set { homepage = value; }
        }

        private string rootUrl = "";
        public string RootUrl
        {
            get { return rootUrl; }
            set { rootUrl = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
		public ContentForShoppingService(String applicationName)
            : base(GContentForShoppingService, applicationName)
        {
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry);
            this.NewFeed += new ServiceEventHandler(this.OnParsedNewFeed);
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved ProductFeed</returns>
        public ProductFeed Query(ProductQuery feedQuery)
        {
            return base.Query(feedQuery) as ProductFeed;
        }

        /// <summary>
        /// Inserts a new product entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public ProductEntry Insert(ProductFeed feed, ProductEntry entry)
        {
            return base.Insert(feed, entry);
        }

        /// <summary>
        /// Updates an existing product entry with the new values
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the updated entry returned by the server</returns>
        public ProductEntry Update(ProductEntry entry)
        {
            return base.Update(entry);
        }

        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <param name="entry">the entry to delete</param>
        public void Delete(ProductEntry entry)
        {
            base.Delete(entry);
        }

        /// <summary>
        /// Event handler. Called when a new list entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                e.Entry = new ProductEntry();
            }
        }

        /// <summary>
        /// Feed handler. Instantiates a new <code>ProductFeed</code>.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e"><code>ServiceEventArgs</code>, holds the feed</param>
        protected void OnParsedNewFeed(object sender, ServiceEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            e.Feed = new ProductFeed(e.Uri, e.Service);
        }
	}
}
