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
//using System.Collections;
//using System.Text;
using Google.GData.Client;

namespace Google.GData.Extensions.Location {
  /// <summary>
    /// GEORSS schema extension describing a location.
    /// </summary>
    public class GeoRssWhere : IExtensionElement, IExtensionElementFactory
    {
        GeoKmlPoint point;

        #region GeoRSS where Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a GeoRSS object.</summary> 
        /// <param name="node">georsswhere node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns>the created GeoRSSWhere object</returns>
        //////////////////////////////////////////////////////////////////////
        public IExtensionElement CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            GeoRssWhere geoWhere = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.GeoRssWhereElement))
            {
                geoWhere = new GeoRssWhere();
                if (node.HasChildNodes)
                {
                    XmlNode childNode = node.FirstChild;
                    while (childNode != null && childNode is XmlElement)
                    {
                        if (childNode.LocalName == GDataParserNameTable.GeoKmlPointElement)
                        {
                            geoWhere.point = GeoKmlPoint.Parse(childNode, parser);
                        }
                        // additional KML elements should go here
                        childNode = childNode.NextSibling;
                    }
                }
            }

            return geoWhere;
        }
        #endregion

        #region overloaded from IExtensionElementFactory

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.GeoRssWhereElement; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlNameSpace
        {
            get { return GDataParserNameTable.NSGeoRss; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlPrefix
        {
            get { return GDataParserNameTable.geoRssPrefix; }
        }

    
        /// <summary>
        /// Persistence method for the Who object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {

            if (this.point != null)
            {
                writer.WriteStartElement(XmlPrefix, XmlName, XmlNameSpace);
                this.point.Save(writer);
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
