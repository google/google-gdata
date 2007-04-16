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

namespace Google.GData.Apps
{
    /// <summary>
    /// Extension element sed to model a Google Apps email list.
    /// Has attribute "name".
    /// </summary>
    public class EmailListElement : IExtensionElement
    {
        /// <summary>
        /// Constructs an empty EmailListElement instance.
        /// </summary>
        public EmailListElement()
        {
        }

        /// <summary>
        /// Constructs a new EmailListElement instance with the specified value.
        /// </summary>
        /// <param name="name">the name attribute of this EmailListElement</param>
        public EmailListElement(String name)
        {
            this.Name = name;
        }

        private string name;

        /// <summary>
        /// NameElement property accessor
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #region EmailListElement Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an xml node to create an EmailList object</summary> 
        /// <param name="node">emailList node</param>
        /// <returns> the created EmailList object</returns>
        //////////////////////////////////////////////////////////////////////
        public static EmailListElement ParseEmailList(XmlNode node)
        {
            Tracing.TraceCall();
            EmailListElement emailList = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;

            if (localname.Equals(AppsNameTable.XmlElementEmailList))
            {
                emailList = new EmailListElement();
                if (node.Attributes != null)
                {
                    if (node.Attributes[AppsNameTable.XmlAttributeEmailListName] != null)
                    {
                        emailList.Name = node.Attributes[AppsNameTable.XmlAttributeEmailListName].Value;
                    }
                }
            }

            return emailList;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return AppsNameTable.XmlElementEmailList; }
        }


        /// <summary>
        /// Persistence method for the EmailListElement object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.Name))
            {
                writer.WriteStartElement(AppsNameTable.appsPrefix, XmlName, AppsNameTable.appsNamespace);
                writer.WriteAttributeString(AppsNameTable.XmlAttributeEmailListName, this.Name);
                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
