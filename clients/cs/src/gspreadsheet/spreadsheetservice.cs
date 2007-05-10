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


namespace Google.GData.Spreadsheets 
{

    /// <summary>
    /// The SpreadsheetsService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
    /// Google Spreadsheets data API.
    /// </summary>
    public class SpreadsheetsService : Service 
    {
        /// <summary>This service's User-Agent string</summary>
        public const string GSpreadsheetsAgent = "GSpreadsheets-CS/1.0.0";
        /// <summary>The Spreadsheets service's name</summary>
        public const string GSpreadsheetsService = "wise";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public SpreadsheetsService(string applicationName)
        : base(GSpreadsheetsService, applicationName, GSpreadsheetsAgent)
        {
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewCellEntry);
            this.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewCellExtensionElement);

            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewListEntry);
            this.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewListExtensionElement);

            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery touse</param>
        /// <returns>the retrieved CellFeed</returns>
        public CellFeed Query(CellQuery feedQuery)
        {
            Stream feedStream = Query(feedQuery.Uri);
            CellFeed feed = new CellFeed(feedQuery.Uri, this);
            feed.Parse(feedStream, AlternativeFormat.Atom);
            feedStream.Close(); 
            return feed;
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery touse</param>
        /// <returns>the retrieved ListFeed</returns>
        public ListFeed Query(ListQuery feedQuery)
        {
            Stream feedStream = Query(feedQuery.Uri);
            ListFeed feed = new ListFeed(feedQuery.Uri, this);
            feed.Parse(feedStream, AlternativeFormat.Atom);
            feedStream.Close(); 
            return feed;
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved SpreadsheetFeed</returns>
        public SpreadsheetFeed Query(SpreadsheetQuery feedQuery)
        {
            Stream feedStream = Query(feedQuery.Uri);
            SpreadsheetFeed feed = new SpreadsheetFeed(feedQuery.Uri, this);
            feed.Parse(feedStream, AlternativeFormat.Atom);
            feedStream.Close(); 
            return feed;
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved WorksheetFeed</returns>
        public WorksheetFeed Query(WorksheetQuery feedQuery)
        {
            Stream feedStream = Query(feedQuery.Uri);
            WorksheetFeed feed = new WorksheetFeed(feedQuery.Uri, this);
            feed.Parse(feedStream, AlternativeFormat.Atom);
            feedStream.Close(); 
            return feed;
        }

        /// <summary>
        /// Event handler. Called when a new cell entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the evet</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewCellEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                e.Entry = new CellEntry();
            }
        }

        /// <summary>
        /// Event handler.  Called for a cell extension element.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnNewCellExtensionElement(object sender, ExtensionElementEventArgs e)
        {
            AtomFeedParser parser = sender as AtomFeedParser;

            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (String.Compare(e.ExtensionElement.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
            {
                // found G namespace
                e.DiscardEntry = true;

                if (e.Base.XmlName == AtomParserNameTable.XmlFeedElement)
                {
                }
                else if (e.Base.XmlName == AtomParserNameTable.XmlAtomEntryElement)
                {
                    CellEntry cellEntry = e.Base as CellEntry;
                    if (cellEntry != null)
                    {
                        cellEntry.ParseCell(e.ExtensionElement, parser);
                    }
                }
            }
        }

        /// <summary>
        /// Event handler. Called when a new list entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the evet</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewListEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                e.Entry = new ListEntry();
            }
        }

        /// <summary>
        /// Event handler.  Called for a list extension element.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnNewListExtensionElement(object sender, ExtensionElementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (String.Compare(e.ExtensionElement.NamespaceURI, GDataSpreadsheetsNameTable.NSGSpreadsheetsExtended, true) == 0)
            {
                // found G namespace
                e.DiscardEntry = true;
                AtomFeedParser parser = sender as AtomFeedParser;

                if (e.Base.XmlName == AtomParserNameTable.XmlFeedElement)
                {
                }
                else if (e.Base.XmlName == AtomParserNameTable.XmlAtomEntryElement)
                {
                    ListEntry ListEntry = e.Base as ListEntry;
                    if (ListEntry != null)
                    {
                        ListEntry.ParseList(e.ExtensionElement, parser);
                    }
                }
            }
        }
    }
}
