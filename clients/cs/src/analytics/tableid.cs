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
    /// GData schema extension describing a tableId.
    /// Part of a feedentry (account feed entry or DataEntry SourceEntry/dxp:dataSource).
    /// dxp:tableId  The unique, namespaced ID to be used when requesting data from a profile.
    /// </summary>
    public class TableId : IExtensionElementFactory
    {
        /// <summary>
        /// Constructs an empty Property instance
        /// </summary>
        public TableId()
        {
        }

        /// <summary>
        /// default constructor, takes 1 parameters
        /// </summary>
        /// <param name="value">the value property value</param>
        public TableId(String value)
        {
            this.Value = value;
        }

        private string value;

        /// <summary>
        ///  Value property accessor
        /// </summary>
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        #region overloaded from IExtensionElementFactory
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a Property object.</summary> 
        /// <param name="node">the node to parse node</param>
        /// <param name="parser">the xml parser to use if we need to dive deeper</param>
        /// <returns>the created Property object</returns>
        //////////////////////////////////////////////////////////////////////
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            TableId tableId = null;

            if (node != null)
            {
                object localname = node.LocalName;
                if (localname.Equals(this.XmlName) == false ||
                  node.NamespaceURI.Equals(this.XmlNameSpace) == false)
                {
                    return null;
                }
            }
            tableId = new TableId();
            if (node != null)
            {
                if (node.InnerText != null)
                {
                    tableId.Value = node.InnerText.Trim();
                }
            }
            return tableId;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return AnalyticsNameTable.XmlTableIdElement; }
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
        /// Persistence method for the Property object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.value))
            {
                writer.WriteStartElement(AnalyticsNameTable.gAnalyticsPrefix, XmlName, AnalyticsNameTable.gAnalyticsNamspace);
                writer.WriteString(Value);
                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
