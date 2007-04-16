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
    /// Google Apps GData extension describing a name.
    /// </summary>
    public class NameElement : IExtensionElement
    {
        /// <summary>
        /// Constructs an empty NameElement instance.
        /// </summary>
        public NameElement()
        {
        }

        /// <summary>
        /// Constructs a new NameElement instance with the specified values.
        /// </summary>
        /// <param name="familyName">Family name (surname).</param>
        /// <param name="givenName">Given name (first name).</param>
        public NameElement(String familyName,
                           String givenName)
        {
            this.FamilyName = familyName;
            this.GivenName = givenName;
        }
        private string familyName;
        private string givenName;

        /// <summary>
        /// FamilyName property accessor
        /// </summary>
        public string FamilyName
        {
            get { return familyName; }
            set { familyName = value; }
        }

        /// <summary>
        /// GivenName property accessor
        /// </summary>
        public string GivenName
        {
            get { return givenName; }
            set { givenName = value; }
        }

        #region NameElement Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an xml node to create a NameElement object</summary> 
        /// <param name="node">name node</param>
        /// <returns> the created NameElement object</returns>
        //////////////////////////////////////////////////////////////////////
        public static NameElement ParseName(XmlNode node)
        {
            Tracing.TraceCall();
            NameElement name = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;

            if (localname.Equals(AppsNameTable.XmlElementName))
            {
                name = new NameElement();
                if (node.Attributes != null)
                {
                    if (node.Attributes[AppsNameTable.XmlAttributeNameFamilyName] != null)
                    {
                        name.FamilyName = node.Attributes[AppsNameTable.XmlAttributeNameFamilyName].Value;
                    }

                    if (node.Attributes[AppsNameTable.XmlAttributeNameGivenName] != null)
                    {
                        name.GivenName = node.Attributes[AppsNameTable.XmlAttributeNameGivenName].Value;
                    }
                }
            }

            return name;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return AppsNameTable.XmlElementName; }
        }


        /// <summary>
        /// Persistence method for the NameElement object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.FamilyName) ||
                Utilities.IsPersistable(this.GivenName))
            {

                writer.WriteStartElement(AppsNameTable.appsPrefix, XmlName, AppsNameTable.appsNamespace);

                if (Utilities.IsPersistable(this.FamilyName))
                {
                    writer.WriteAttributeString(AppsNameTable.XmlAttributeNameFamilyName, this.FamilyName);
                }

                if (Utilities.IsPersistable(this.GivenName))
                {
                    writer.WriteAttributeString(AppsNameTable.XmlAttributeNameGivenName, this.GivenName);
                }

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
