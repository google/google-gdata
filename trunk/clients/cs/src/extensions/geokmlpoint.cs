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
    public class GeoKmlPoint : IExtensionElement
    {
        GeoKmlPosition pos;
        

        #region GeoRSS where Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a GeoKmlPoint object.</summary> 
        /// <param name="node">Point node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns>the created Point object</returns>
        //////////////////////////////////////////////////////////////////////
        public static GeoKmlPoint Parse(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            GeoKmlPoint geoPoint = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.GeoKmlPointElement))
            {
                geoPoint = new GeoKmlPoint();
                if (node.HasChildNodes)
                {
                    XmlNode childNode = node.FirstChild;
                    while (childNode != null && childNode is XmlElement)
                    {
                        if (childNode.LocalName == GDataParserNameTable.GeoKmlPosElement)
                        {
                            geoPoint.pos = GeoKmlPosition.Parse(childNode, parser);
                        }
                        // additional KML elements should go here
                        childNode = childNode.NextSibling;
                    }
                }
            }

            return geoPoint;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.GeoRssWhereElement; }
        }

        /// <summary>
        /// Persistence method for the Who object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {

            if (this.pos != null)
            {
                writer.WriteStartElement(BaseNameTable.gDataPrefix, XmlName, BaseNameTable.gNamespace);
                this.pos.Save(writer);
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
