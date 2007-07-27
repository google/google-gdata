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
using System.Globalization;
using Google.GData.Client;

namespace Google.GData.Extensions.Location {

  

    /// <summary>
    /// GEORSS schema extension describing a location.
    /// </summary>
    public class GeoKmlPosition : IExtensionElement
    {
        private double lattitude;
        private double longitude;

        #region GeoRSS Position Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a GeoKmlPosition object.</summary> 
        /// <param name="node">pos node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns>the created Position object</returns>
        //////////////////////////////////////////////////////////////////////
        public static GeoKmlPosition Parse(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            GeoKmlPosition pos = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.GeoKmlPosElement))
            {
                pos = new GeoKmlPosition();
                String text = node.InnerText;
                String [] parts = text.Split(new char[] {' '});
               
            }

            return pos;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.GeoKmlPosElement; }
        }



        /// <summary>
        /// Persistence method for the Who object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            writer.WriteElementString(XmlName, BaseNameTable.gDataPrefix, 
                                    this.lattitude.ToString(CultureInfo.InvariantCulture) +
                                    " " +
                                    this.longitude.ToString(CultureInfo.InvariantCulture));

        }
        #endregion
    }
}
