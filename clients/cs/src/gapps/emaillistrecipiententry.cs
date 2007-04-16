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
using System.Xml;
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Apps
{
    /// <summary>
    /// A Google Apps email list recipient entry.  This class
    /// defines a single recipient on an email list using a Who
    /// (gd:who) object.
    /// </summary>
    public class EmailListRecipientEntry : AtomEntry
    {
        /// <summary>
        /// Category used to label entries that contain email list
        /// recipient extension data.
        /// </summary>
        public static AtomCategory EMAILLIST_RECIPIENT_CATEGORY =
            new AtomCategory(AppsNameTable.EmailListRecipient,
                             new AtomUri(BaseNameTable.gKind));

        private Who recipient;

        /// <summary>
        /// Constructs a new EmailListRecipientEntry without
        /// setting the Recipient property.
        /// </summary>
        public EmailListRecipientEntry()
            : base()
        {
            Categories.Add(EMAILLIST_RECIPIENT_CATEGORY);
        }

        /// <summary>
        /// Constructs a new EmailListRecipientEntry, using
        /// the specified email address to initialize the
        /// Recipient property.
        /// </summary>
        /// <param name="recipientAddress">the recipient's
        /// email address</param>
        public EmailListRecipientEntry(String recipientAddress)
            : base()
        {
            Categories.Add(EMAILLIST_RECIPIENT_CATEGORY);
            
            Recipient = new Who();
            Recipient.Rel = Who.RelType.MESSAGE_TO;
            Recipient.Email = recipientAddress;
        }

        /// <summary>
        /// The recipient element in this entry.
        /// </summary>
        public Who Recipient
        {
            get { return recipient; }
            set
            {
                if (recipient != null)
                {
                    ExtensionElements.Remove(recipient);
                }
                recipient = value;
                ExtensionElements.Add(recipient);
            }
        }

        /// <summary>
        /// Overrides the base Update() method to throw a ClientFeedException,
        /// because email list recipient entries cannot be updated.  This exception
        /// indicates that the client made an erroneous request to the email
        /// list feed.
        /// </summary>
        public new void Update()
        {
            throw new ClientFeedException("Email list recipient entries cannot be updated.");
        }

        /// <summary>
        /// Checks if this is a namespace declaration that we already added
        /// </summary>
        /// <param name="node">XmlNode to check</param>
        /// <returns>True if this node should be skipped</returns>
        protected override bool SkipNode(XmlNode node)
        {
            if (base.SkipNode(node))
            {
                return true;
            }

            return (node.NodeType == XmlNodeType.Attribute
                   && node.Name.StartsWith("xmlns")
                   && String.Compare(node.Value, BaseNameTable.gNamespace) == 0);
        }

        /// <summary>
        /// Parses the inner state of the element
        /// </summary>
        /// <param name="emailListRecipientEntryNode">A g-scheme, xml node</param>
        /// <param name="parser">The AtomFeedParser that called this</param>
        public void ParseEmailListRecipientEntry(XmlNode emailListRecipientEntryNode, AtomFeedParser parser)
        {
            if (String.Compare(emailListRecipientEntryNode.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
            {
                if (emailListRecipientEntryNode.LocalName == GDataParserNameTable.XmlWhoElement)
                {
                    Recipient = Who.ParseWho(emailListRecipientEntryNode, parser);
                }
            }
        }
    }
}
