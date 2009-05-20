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
/* Created by Alex Maitland, maitlandalex@gmail.com */
using System.Collections.Generic;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Analytics
{
    /// <summary>
    /// GData schema extension describing aggregate results.
    /// dxp:aggregates  contains aggregate data for all metrics requested in the feed.
    /// </summary>
    public class Aggregates : IExtensionElementFactory
    {
        private List<Metric> metrics;

        public List<Metric> Metrics
        {
            get { return metrics; }
            set { metrics = value; }
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

            if (node != null)
            {
                object localname = node.LocalName;
                if (localname.Equals(this.XmlName) == false ||
                    node.NamespaceURI.Equals(this.XmlNameSpace) == false)
                {
                    return null;
                }
            }
            Aggregates aggregates = new Aggregates();
            aggregates.Metrics = new List<Metric>();
            if (node != null)
            {
                if (node.InnerText != null)
                {
                    foreach (XmlNode xmlNode in node.ChildNodes)
                    {
                        Metric parsedMetric = new Metric().CreateInstance(xmlNode, parser) as Metric;
                        if (parsedMetric != null)
                        {
                            aggregates.Metrics.Add(parsedMetric);
                        }
                    }
                }
            }
            return aggregates;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return AnalyticsNameTable.XmlAggregatesElement; }
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
            writer.WriteStartElement(XmlPrefix, XmlName, XmlNameSpace);
            foreach (Metric metric in metrics)
            {
                metric.Save(writer);
            }
            writer.WriteEndElement();
        }
        #endregion
    }
}
