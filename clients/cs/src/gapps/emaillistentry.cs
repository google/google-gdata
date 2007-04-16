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

namespace Google.GData.Apps
{
    /// <summary>
    /// A Google Apps email list entry.  An EmailListEntry object
    /// contains information about a single email list.
    /// </summary>
    public class EmailListEntry : AtomEntry
    {
        /// <summary>
        /// Category used to label entries that contain email list
        /// extension data.
        /// </summary>
        public static AtomCategory EMAILLIST_CATEGORY =
            new AtomCategory(AppsNameTable.EmailList,
                             new AtomUri(BaseNameTable.gKind));

        private EmailListElement emailList;

        /// <summary>
        /// Constructs a new EmailListEntry instance with the appropriate category
        /// to indicate that it is a email list.
        /// </summary>
        public EmailListEntry()
            : base()
        {
            Categories.Add(EMAILLIST_CATEGORY);
        }

        /// <summary>
        /// Constructs a new EmailListEntry instance with the specified list name.
        /// </summary>
        /// <param name="emailListName">the name of the email list</param>
        public EmailListEntry(String emailListName)
            : base()
        {
            Categories.Add(EMAILLIST_CATEGORY);
            EmailList = new EmailListElement(emailListName);
        }

        /// <summary>
        /// The email list element in this entry.
        /// </summary>
        public EmailListElement EmailList
        {
            get { return emailList; }
            set
            {
                if (emailList != null)
                {
                    ExtensionElements.Remove(emailList);
                }
                emailList = value;
                ExtensionElements.Add(emailList);
            }
        }

        /// <summary>
        /// Overrides the base Update() method to throw a ClientFeedException,
        /// because email list entries cannot be updated.  This exception
        /// indicates that the client made an erroneous request to the email
        /// list feed.
        /// </summary>
        public new void Update()
        {
            throw new ClientFeedException("Email list entries cannot be updated.");
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
        /// <param name="emailListEntryNode">A g-scheme, xml node</param>
        /// <param name="parser">The AtomFeedParser that called this</param>
        public void ParseEmailListEntry(XmlNode emailListEntryNode, AtomFeedParser parser)
        {
            if (String.Compare(emailListEntryNode.NamespaceURI, AppsNameTable.appsNamespace, true) == 0)
            {
                if (emailListEntryNode.LocalName == AppsNameTable.XmlElementEmailList)
                {
                    EmailList = EmailListElement.ParseEmailList(emailListEntryNode);
                }
            }
        }
    }
}
