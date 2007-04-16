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
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Apps
{
    /// <summary>
    /// Feed API customization class for defining nickname feed.
    /// </summary>
    public class NicknameFeed : AtomFeed
    {
        /// <summary>
        /// Constructs a new NicknameFeed.
        /// </summary>
        /// <param name="uriBase">the URI of the feed</param>
        /// <param name="iService">the service with which this
        /// feed will be associated</param>
        public NicknameFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
            NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewNicknameEntry);
            NewExtensionElement += new ExtensionElementEventHandler(this.OnNewNicknameExtensionsElement);
        }

        /// <summary>empty base implementation</summary> 
        /// <param name="writer">the xmlwriter, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer)
        {
            base.AddOtherNamespaces(writer);
            Utilities.EnsureGDataNamespace(writer);
        }

        /// <summary>checks if this is a namespace 
        /// decl that we already added</summary> 
        /// <param name="node">XmlNode to check</param>
        /// <returns>true if this node should be skipped </returns>
        protected override bool SkipNode(XmlNode node)
        {
            if (base.SkipNode(node) == true)
            {
                return true;
            }

            return node.NodeType == XmlNodeType.Attribute
                    && (node.Name.StartsWith("xmlns") == true)
                    && (String.Compare(node.Value, BaseNameTable.gNamespace) == 0);
        }

        /// <summary>
        /// Event handling. Called when a new nickname entry is parsed.
        /// </summary>
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
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

        /// <summary>eventhandler - called for event extension element
        /// </summary>
        /// <param name="sender">the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedEntry</param> 
        /// <returns> </returns>
        protected void OnNewNicknameExtensionsElement(object sender, ExtensionElementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            if (String.Compare(e.ExtensionElement.NamespaceURI, AppsNameTable.appsNamespace, true) == 0)
            {
                e.DiscardEntry = true;

                AtomFeedParser parser = sender as AtomFeedParser;

                if (e.Base.XmlName == AtomParserNameTable.XmlAtomEntryElement)
                {
                    NicknameEntry nicknameEntry = e.Base as NicknameEntry;

                    if (nicknameEntry != null)
                    {
                        nicknameEntry.ParseNicknameEntry(e.ExtensionElement, parser);
                    }
                }
            }
        }
    }
}
