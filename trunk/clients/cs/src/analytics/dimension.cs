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
/* Created by Morten Christensen, elpadrinodk@gmail.com, http://blog.sitereactor.dk */
using System;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Analytics
{
    /// <summary>
    /// GData schema extension describing a dimension.
    /// A dimension is part of a DataEntry (entry).
    /// dxp:dimensionone element for each dimension in the query, 
    /// which includes the name and value of the dimension
    /// </summary>
    public class Dimension : IExtensionElementFactory
    {
        /// <summary>
        /// Constructs an empty Dimension instance
        /// </summary>
        public Dimension()
        {

        }

        /// <summary>
        /// default constructor, takes 2 parameters
        /// </summary>
        /// <param name="name">name property value</param>
        /// <param name="value">the value property value</param>
        public Dimension(String name, String value)
        {
            this.Name = name;
            this.Value = value;
        }

        private string nameString;
        private string valueString;

        /// <summary>
        /// name property accessor.
        /// </summary>
        public string Name
        {
            get { return nameString; }
            set { nameString = value; }
        }

        /// <summary>
        /// value property accessor.
        /// </summary>
        public string Value
        {
            get { return valueString; }
            set { valueString = value; }
        }

        #region overloaded from IExtensionElementFactory
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a Dimension object.</summary> 
        /// <param name="node">the node to parse node</param>
        /// <param name="parser">the xml parser to use if we need to dive deeper</param>
        /// <returns>the created Dimension object</returns>
        //////////////////////////////////////////////////////////////////////
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            Dimension dimension = null;

            if (node != null)
            {
                object localname = node.LocalName;
                if (localname.Equals(this.XmlName) == false ||
                  node.NamespaceURI.Equals(this.XmlNameSpace) == false)
                {
                    return null;
                }
            }

            dimension = new Dimension();
            if (node != null)
            {

                if (node.Attributes != null)
                {
                    if (node.Attributes[AnalyticsNameTable.XmlAttributeName] != null)
                    {
                        dimension.Name = node.Attributes[AnalyticsNameTable.XmlAttributeName].Value;
                    }
                    if (node.Attributes[AnalyticsNameTable.XmlAttributeValue] != null)
                    {
                        dimension.Value = node.Attributes[AnalyticsNameTable.XmlAttributeValue].Value;
                    }
                }
            }
            return dimension;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return AnalyticsNameTable.XmlDimensionElement; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing namespace of this XML.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlNameSpace
        {
            get { return AnalyticsNameTable.gAnalyticsNamspace; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing the prefix of this XML.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlPrefix
        {
            get { return AnalyticsNameTable.gAnalyticsPrefix; }
        }

        #endregion

        #region overloaded for persistence

        /// <summary>
        /// Persistence method for the Dimension object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.Name) ||
                Utilities.IsPersistable(this.Value))
            {

                writer.WriteStartElement(AnalyticsNameTable.gAnalyticsPrefix, XmlName, AnalyticsNameTable.gAnalyticsNamspace);

                if (Utilities.IsPersistable(this.Name))
                {
                    writer.WriteAttributeString(AnalyticsNameTable.XmlAttributeName, this.Name);
                }

                if (Utilities.IsPersistable(this.Value))
                {
                    writer.WriteAttributeString(AnalyticsNameTable.XmlAttributeValue, this.Value);
                }

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
