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
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Spreadsheets
{
    /// <summary>
    /// Feed API customization class for defining a List feed.
    /// </summary>
    public class ListFeed : AtomFeed
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uriBase">The uri for this list feed.</param>
        /// <param name="iService">The Spreadsheets service.</param>
        public ListFeed(Uri uriBase, IService iService)
        : base(uriBase, iService)
        {
            NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewListEntry);
            NewExtensionElement += new ExtensionElementEventHandler(this.OnNewListExtensionsElement);
        }

        /// <summary>Empty base implementation</summary> 
        /// <param name="writer">The xmlwriter, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer)
        {
            base.AddOtherNamespaces(writer);
            Utilities.EnsureGDataNamespace(writer);
        }

        /// <summary>
        /// Checks if this namespace declaration has already been added.
        /// </summary>
        /// <param name="node">XmlNode to check</param>
        /// <returns>True if this node should be skipped</returns>
        protected override bool SkipNode(XmlNode node)
        {
            if (base.SkipNode(node))
            {
                return true;
            }

            return(node.NodeType == XmlNodeType.Attribute
                   && node.Name.StartsWith("xmlns")
                   && String.Compare(node.Value, BaseNameTable.gNamespace) == 0
                   && String.Compare(node.Value, GDataSpreadsheetsNameTable.NSGSpreadsheets) == 0
                   && String.Compare(node.Value, GDataSpreadsheetsNameTable.NSGSpreadsheetsExtended) == 0);
        }

        /// <summary>
        /// Event handler called when a new list entry is parsed.
        /// </summary>
        /// <param name="sender">The object which sends the event.</param>
        /// <param name="e">FeedParserEventArguments holds the feed entry</param>
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
        /// Event handler called for list extension element
        /// </summary>
        /// <param name="sender">The object which sends the event</param>
        /// <param name="e">FeedParserEventArgument holds the feedEntry</param>
        protected void OnNewListExtensionsElement(object sender, ExtensionElementEventArgs e)
        {

            AtomFeedParser parser = sender as AtomFeedParser;

            if (String.Compare(e.ExtensionElement.NamespaceURI, GDataSpreadsheetsNameTable.NSGSpreadsheetsExtended, true) == 0)
            {
                e.DiscardEntry = true;

                if (e.Base.XmlName == AtomParserNameTable.XmlAtomEntryElement)
                {
                    ListEntry listEntry = e.Base as ListEntry;

                    if (listEntry != null)
                    {
                        listEntry.ParseList(e.ExtensionElement, parser);
                    }
                }
            }
        }
    }
}
