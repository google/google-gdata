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
using System.Collections;
using System.Text;
using Google.GData.Client;

namespace Google.GData.Extensions 
{

    /// <summary>
    /// GData schema extension describing an extended property/value pair
    /// </summary>
    public class ExtendedProperty : IExtensionElement
    {

        /// <summary>
        /// the valueString (required).
        /// </summary>
        protected string value; 

        /// <summary>
        /// the property name
        /// </summary>
        protected string name; 

        
        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor for the value</summary> 
        /// <returns>the value as string </returns>
        //////////////////////////////////////////////////////////////////////
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method for the name</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }


        #region When Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create an ExtendedProperty object.</summary> 
        /// <param name="node">when node</param>
        /// <returns>the created ExtendedProperty object</returns>
        //////////////////////////////////////////////////////////////////////
        public static ExtendedProperty Parse(XmlNode node)
        {
            Tracing.TraceCall();
            ExtendedProperty prop = null;
            Tracing.Assert(node != null, "node should not be null");

            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            object localname = node.LocalName;

            if (localname.Equals(GDataParserNameTable.XmlExtendedPropertyElement))
            {
                prop = new ExtendedProperty();

                if (node.Attributes != null)
                {
                    prop.Value = node.Attributes[GDataParserNameTable.XmlValue] != null ? 
                         node.Attributes[GDataParserNameTable.XmlValue].Value : null; 
                    prop.Name = node.Attributes[AtomParserNameTable.XmlName]!= null ? 
                        node.Attributes[AtomParserNameTable.XmlName].Value : null; 
                }
            }
            return prop;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.XmlExtendedPropertyElement; }
        }

        /// <summary>
        /// Persistence method for the Extended property object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {

            if (Utilities.IsPersistable(this.Name))
            {
                writer.WriteStartElement(BaseNameTable.gDataPrefix, XmlName, BaseNameTable.gNamespace);
                writer.WriteAttributeString(AtomParserNameTable.XmlName, this.Name); 
                if (this.Value != null)
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlValue, this.Value); 
                }
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
