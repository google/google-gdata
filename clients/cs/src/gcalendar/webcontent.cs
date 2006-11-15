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
using System.Globalization;

namespace Google.GData.Extensions 
{

    /// <summary>
    /// GData schema extension describing a webcontent for the calendar
    /// </summary>
    public class WebContent : IExtensionElement
    {
        private string url;
        private uint width; 
        private uint height;


        //////////////////////////////////////////////////////////////////////
        /// <summary>url of content</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>width of the iframe/gif</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public uint Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Height of the iframe/gif</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public uint Height
        {
            get { return this.height; }
            set { this.height = value; }
        }


        #region WebContent Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create an WebContent object.</summary> 
        /// <param name="node">WebContent node</param>
        /// <returns>the created WebContent object</returns>
        //////////////////////////////////////////////////////////////////////
        public static WebContent ParseWebContent(XmlNode node)
        {
            Tracing.TraceCall();
            WebContent webContent = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.XmlWebContentElement))
            {
                webContent = new WebContent();
                if (node.Attributes != null)
                {
                    String value = node.Attributes[GDataParserNameTable.XmlAttributeUrl] != null ? 
                        node.Attributes[GDataParserNameTable.XmlAttributeUrl].Value : null; 
                    if (value != null)
                    {
                        webContent.Url = value;
                    }

                    value = node.Attributes[GDataParserNameTable.XmlAttributeWidth] != null ? 
                        node.Attributes[GDataParserNameTable.XmlAttributeWidth].Value : null; 

                    if (value != null)
                    {
                        webContent.Width = uint.Parse(value, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture);
                    }

                    value = node.Attributes[GDataParserNameTable.XmlAttributeHeight] != null ? 
                        node.Attributes[GDataParserNameTable.XmlAttributeHeight].Value : null; 

                    if (value != null)
                    {
                        webContent.Height = uint.Parse(value, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture);
                    }

                }
            }

            return webContent;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.XmlWebContentElement; }
        }

        /// <summary>
        /// Persistence method for the When object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.Url))
            {
                writer.WriteStartElement(GDataParserNameTable.gCalPrefix, XmlName, GDataParserNameTable.NSGCal);
                writer.WriteAttributeString(GDataParserNameTable.XmlAttributeUrl, this.Url);
                writer.WriteAttributeString(GDataParserNameTable.XmlAttributeHeight, this.Height.ToString());
                writer.WriteAttributeString(GDataParserNameTable.XmlAttributeWidth, this.Width.ToString());
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
