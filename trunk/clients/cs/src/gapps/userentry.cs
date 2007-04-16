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
    /// A Google Apps user entry.  A UserEntry object encapsulates
    /// the login, name and quota information for a single user.
    /// </summary>
    public class UserEntry : AtomEntry
    {
        /// <summary>
        /// Category used to label entries that contain user
        /// extension data.
        /// </summary>
        public static AtomCategory USER_ACCOUNT_CATEGORY =
            new AtomCategory(AppsNameTable.User,
                             new AtomUri(BaseNameTable.gKind));

        private LoginElement login;
        private QuotaElement quota;
        private NameElement name;

        /// <summary>
        /// Constructs a new UserEntry instance with the appropriate category
        /// to indicate that it is a user account.
        /// </summary>
        public UserEntry()
            : base()
        {
            Categories.Add(USER_ACCOUNT_CATEGORY);
        }

        /// <summary>
        /// The login element in this user account entry.
        /// </summary>
        public LoginElement Login
        {
            get { return login; }
            set
            {
                if (login != null)
                {
                    ExtensionElements.Remove(login);
                }
                login = value;
                ExtensionElements.Add(login);
            }
        }

        /// <summary>
        /// The quota element in this user account entry.
        /// </summary>
        public QuotaElement Quota
        {
            get { return quota; }
            set
            {
                if (quota != null)
                {
                    ExtensionElements.Remove(quota);
                }
                quota = value;
                ExtensionElements.Add(quota);
            }
        }

        /// <summary>
        /// The name element in this user account entry.
        /// </summary>
        public NameElement Name
        {
            get { return name; }
            set
            {
                if (name != null)
                {
                    ExtensionElements.Remove(name);
                }
                name = value;
                ExtensionElements.Add(name);
            }
        }

        /// <summary>
        /// Updates this UserEntry.
        /// </summary>
        /// <returns>the updated UserEntry</returns>
        public new UserEntry Update()
        {
            try
            {
                return base.Update() as UserEntry;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Empty base implementation
        /// </summary>
        /// <param name="writer">The XmlWrite, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer)
        {
            base.AddOtherNamespaces(writer);
            Utilities.EnsureGDataNamespace(writer);
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
        /// <param name="userEntryNode">A g-scheme, xml node</param>
        /// <param name="parser">The AtomFeedParser that called this</param>
        public void ParseUserEntry(XmlNode userEntryNode, AtomFeedParser parser)
        {
            if (String.Compare(userEntryNode.NamespaceURI, AppsNameTable.appsNamespace, true) == 0)
            {
                if (userEntryNode.LocalName == AppsNameTable.XmlElementLogin)
                {
                    Login = LoginElement.ParseLogin(userEntryNode);
                }
                else if (userEntryNode.LocalName == AppsNameTable.XmlElementQuota)
                {
                    Quota = QuotaElement.ParseQuota(userEntryNode);
                }
                else if (userEntryNode.LocalName == AppsNameTable.XmlElementName)
                {
                    Name = NameElement.ParseName(userEntryNode);
                }
            }
        }
    }
}
