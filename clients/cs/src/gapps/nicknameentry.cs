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
    public class NicknameEntry : AtomEntry
    {
        /// <summary>
        /// Category used to label entries that contain nickname
        /// extension data.
        /// </summary>
        public static AtomCategory NICKNAME_CATEGORY =
            new AtomCategory(AppsNameTable.Nickname,
                             new AtomUri(BaseNameTable.gKind));

        private LoginElement login;
        private NicknameElement nickname;

        /// <summary>
        /// Constructs a new NicknameEntry instance with the appropriate category
        /// to indicate that it is a nickname.
        /// </summary>
        public NicknameEntry()
            : base()
        {
            Categories.Add(NICKNAME_CATEGORY);
        }

        /// <summary>
        /// The login element in this entry.
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
        /// The nickname element in this entry.
        /// </summary>
        public NicknameElement Nickname
        {
            get { return nickname; }
            set
            {
                if (nickname != null)
                {
                    ExtensionElements.Remove(nickname);
                }
                nickname = value;
                ExtensionElements.Add(nickname);
            }
        }

        /// <summary>
        /// Overrides the base Update() method to throw an
        /// exception, because nickname entries cannot be
        /// updated.
        /// </summary>
        public new void Update()
        {
            throw new GDataRequestException("Nickname entries cannot be updated.");
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
        /// <param name="nicknameEntryNode">A g-scheme, xml node</param>
        /// <param name="parser">The AtomFeedParser that called this</param>
        public void ParseNicknameEntry(XmlNode nicknameEntryNode, AtomFeedParser parser)
        {
            if (String.Compare(nicknameEntryNode.NamespaceURI, AppsNameTable.appsNamespace, true) == 0)
            {
                if (nicknameEntryNode.LocalName == AppsNameTable.XmlElementLogin)
                {
                    Login = LoginElement.ParseLogin(nicknameEntryNode);
                }
                else if (nicknameEntryNode.LocalName == AppsNameTable.XmlElementNickname)
                {
                    Nickname = NicknameElement.ParseNickname(nicknameEntryNode);
                }
            }
        }
    }
}
