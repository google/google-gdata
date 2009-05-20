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
    /// GData schema extension describing a metric.
    /// A metric is part of a DataEntry (entry).
    /// One element for each metric in the query.
    /// A metric contains the following attributes:
    /// # namethe name of the metric
    /// # typeeither integer or string
    /// # valuethe aggregate value for the query for that metric (e.g. 24 for 24 pageviews)
    /// # cithe confidence interval, or range of values likely to include the correct value.
    /// </summary>
    public class Metric : IExtensionElementFactory
    {
        /// <summary>
        /// Constructs an empty Metric instance
        /// </summary>
        public Metric()
        {
        }

        /// <summary>
        /// default constructor, takes 4 parameters
        /// </summary>
        /// <param name="confidenceInterval">the confidenceInterval property value</param>
        /// <param name="name">the name property value</param>
        /// <param name="type">the type property value</param>
        /// <param name="value">the value property value</param>
        public Metric(String confidenceInterval, String name, String type, String value)
        {
            this.ConfidenceInterval = confidenceInterval;
            this.Name = name;
            this.Type = type;
            this.Value = value;
        }

        private string confidenceInterval;
        private string nameString;
        private string typeString;
        private string valueString;

        /// <summary>
        /// confidenceInterval property accessor.
        /// </summary>
        public string ConfidenceInterval
        {
            get { return confidenceInterval; }
            set { confidenceInterval = value; }
        }

        /// <summary>
        /// name property accessor.
        /// </summary>
        public string Name
        {
            get { return nameString; }
            set { nameString = value; }
        }

        /// <summary>
        /// type property accessor.
        /// </summary>
        public string Type
        {
            get { return typeString; }
            set { typeString = value; }
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
        /// <summary>Parses an xml node to create a Metric object.</summary> 
        /// <param name="node">the node to parse node</param>
        /// <param name="parser">the xml parser to use if we need to dive deeper</param>
        /// <returns>the created Metric object</returns>
        //////////////////////////////////////////////////////////////////////
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            Metric metric = null;

            if (node != null)
            {
                object localname = node.LocalName;
                if (localname.Equals(this.XmlName) == false ||
                  node.NamespaceURI.Equals(this.XmlNameSpace) == false)
                {
                    return null;
                }
            }

            metric = new Metric();
            if (node != null)
            {

                if (node.Attributes != null)
                {
                    if (node.Attributes[AnalyticsNameTable.XmlAttributeConfidenceInterval] != null)
                    {
                        metric.ConfidenceInterval =
                            node.Attributes[AnalyticsNameTable.XmlAttributeConfidenceInterval].Value;
                    }
                    if (node.Attributes[AnalyticsNameTable.XmlAttributeName] != null)
                    {
                        metric.Name = node.Attributes[AnalyticsNameTable.XmlAttributeName].Value;
                    }
                    if (node.Attributes[BaseNameTable.XmlAttributeType] != null)
                    {
                        metric.Type = node.Attributes[BaseNameTable.XmlAttributeType].Value;
                    }
                    if (node.Attributes[AnalyticsNameTable.XmlAttributeValue] != null)
                    {
                        metric.Value = node.Attributes[AnalyticsNameTable.XmlAttributeValue].Value;
                    }
                }
            }
            return metric;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return AnalyticsNameTable.XmlMetricElement; }
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
            if (Utilities.IsPersistable(this.ConfidenceInterval) || Utilities.IsPersistable(this.Name) || Utilities.IsPersistable(this.Type) || Utilities.IsPersistable(this.Value))
            {

                writer.WriteStartElement(AnalyticsNameTable.gAnalyticsPrefix, XmlName, AnalyticsNameTable.gAnalyticsNamspace);

                if (Utilities.IsPersistable(this.ConfidenceInterval))
                {
                    writer.WriteAttributeString(AnalyticsNameTable.XmlAttributeConfidenceInterval, this.ConfidenceInterval);
                }

                if (Utilities.IsPersistable(this.Name))
                {
                    writer.WriteAttributeString(AnalyticsNameTable.XmlAttributeName, this.Name);
                }

                if (Utilities.IsPersistable(this.Type))
                {
                    writer.WriteAttributeString(BaseNameTable.XmlAttributeType, this.Type);
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
