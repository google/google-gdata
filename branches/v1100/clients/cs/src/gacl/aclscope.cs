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
using System.Xml;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.AccessControl 
{

     /// <summary>
    /// GData schema extension describing an account role
    /// </summary>
    public class AclScope : IExtensionElement
    {
        private string type;
        private string value;

        /// <summary>string constant for the user scope</summary>
        public const string SCOPE_USER =  "user";
        /// <summary>string constant for the user scope</summary>
        public const string SCOPE_DOMAIN  =  "domain";
        /// <summary>string constant for the user scope</summary>
        public const string SCOPE_DEFAULT =  "default";

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return AclNameTable.XmlAclScopeElement; }
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public String Value</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Value
        {
            get {return this.value;}
            set {this.value = value;}
        }
        // end of accessor public String Value


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Type</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public String Type
        {
            get {return this.type;}
            set {
                if (String.Compare(value, SCOPE_USER) == 0 ||
                    String.Compare(value, SCOPE_DOMAIN) == 0 || 
                    String.Compare(value, SCOPE_DEFAULT) == 0)
                {
                    this.type = value;
                    if (String.Compare(value, SCOPE_DEFAULT) == 0)
                    {
                        // empty the value
                        this.Value = null;
                    }
                }
                else 
                {
                    throw new System.ArgumentException("Type should be one of the predifined values", value);
                }
            }
        }
        // end of accessor public string Type

        /// <summary>
        ///  parse method is called from the atom parser to populate an Transparency node
        /// </summary>
        /// <param name="node">the xmlnode to parser</param>
        /// <returns>Notifications object</returns>
        public static AclScope parse(XmlNode node)
        {
            AclScope scope = null;
            Tracing.TraceMsg("Parsing a gAcl:AclScope" + node);
            if (String.Compare(node.NamespaceURI, AclNameTable.gAclNamespace, true) == 0
                && String.Compare(node.LocalName, AclNameTable.XmlAclScopeElement) == 0)
            {
                scope = new AclScope();
                if (node.Attributes != null)
                {
                    if (node.Attributes[AclNameTable.XmlValue] != null)
                    {
                        scope.Value = node.Attributes[AclNameTable.XmlValue].Value;
                    }
                    if (node.Attributes[AclNameTable.XmlAttributeType] != null)
                    {
                        scope.Type = node.Attributes[AclNameTable.XmlAttributeType].Value;
                    }
                    Tracing.TraceMsg("AclScope parsed, value = " + scope.Value + ", type= " + scope.Type);
                }
            }
            return scope;
        }

        /// <summary>
        /// Persistence method for the When object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {

            if (Utilities.IsPersistable(this.type) ||
                Utilities.IsPersistable(this.value))
            {
                writer.WriteStartElement(AclNameTable.gAclAlias, XmlName, AclNameTable.gAclNamespace);
                if (Utilities.IsPersistable(this.type))
                {
                    writer.WriteAttributeString(AclNameTable.XmlAttributeType, this.type);
                }
                if (Utilities.IsPersistable(this.value))
                {
                    writer.WriteAttributeString(AclNameTable.XmlValue, this.value);
                }
                writer.WriteEndElement();
            }
        }
    }
}
