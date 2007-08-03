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

namespace Google.GData.Extensions {

    /// <summary>
    /// GData schema extension describing a comments feed.
    /// </summary>
    public class Comments : IExtensionElement
    {

        /// <summary>
        ///  holds the feedLink property
        /// </summary>
        protected FeedLink feedLink;

        /// <summary>
        /// Comments feed link.
        /// </summary>
        public FeedLink FeedLink
        {
            get { return feedLink;}
            set { feedLink = value;}
        }

#region Comments Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an xml node to create an Comments object</summary> 
        /// <param name="node">comments node</param>
        /// <returns> the created Comments object</returns>
        //////////////////////////////////////////////////////////////////////
        public static Comments ParseComments(XmlNode node)
        {
            Tracing.TraceCall();
            Comments comments = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.XmlCommentsElement))
            {
                comments = new Comments();
                if (node.HasChildNodes)
                {
                    XmlNode commentsChild = node.FirstChild;
                    while (commentsChild != null && commentsChild is XmlElement)
                    {
                        if (commentsChild.LocalName == GDataParserNameTable.XmlFeedLinkElement &&
                            commentsChild.NamespaceURI == BaseNameTable.gNamespace)
                        {
                            if (comments.FeedLink == null)
                            {
                                comments.FeedLink = FeedLink.ParseFeedLink(commentsChild);
                            }
                            else
                            {
                                throw new ArgumentException("Only one feedLink is allowed inside the gd:comments");
                            }
                        }
                        commentsChild = commentsChild.NextSibling;
                    }
                }
            }

            if (comments.FeedLink == null)
            {
                throw new ArgumentException("gd:comments/gd:feedLink is required.");
            }

            return comments;
        }

#endregion

#region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.XmlCommentsElement;}
        }

        /// <summary>
        /// Persistence method for the Comment  object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (FeedLink != null)
            {
                // only save out if there is something to save
                writer.WriteStartElement(BaseNameTable.gDataPrefix, XmlName, BaseNameTable.gNamespace);
                FeedLink.Save(writer);
                writer.WriteEndElement();
            }
        }
#endregion
    }
}
