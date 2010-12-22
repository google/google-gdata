/* Copyright (c) 2006-2008 Google Inc.
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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
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
    public class WebContent : IExtensionElementFactory
    {
        private string url;
        private string display;
        private uint width; 
        private uint height;
        private SortedList gadgetPrefs;


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
        /// <summary>Display property</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Display
        {
            get { return this.display; }
            set { this.display = value; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>width of the iframe/gif</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        [CLSCompliant(false)]
        public uint Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Height of the iframe/gif</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        [CLSCompliant(false)]
        public uint Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public SortedList GadgetPreferences</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public SortedList GadgetPreferences
        {
            get 
            {
                if (this.gadgetPrefs == null) 
                {
                    this.gadgetPrefs = new SortedList();
                }
                return this.gadgetPrefs;
            }
            set {this.gadgetPrefs = value;}
        }
        // end of accessor public SortedList GadgetPreferences


        #region WebContent Parser

        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a webcontent object.</summary> 
        /// <param name="node">xml node</param>
        /// <param name="parser">the atomfeedparser to use for deep dive parsing</param>
        /// <returns>the created SimpleElement object</returns>
        //////////////////////////////////////////////////////////////////////
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser) 
        {
            Tracing.TraceCall();

            if (node != null)
            {
                object localname = node.LocalName;
                if (!localname.Equals(this.XmlName) ||
                    !node.NamespaceURI.Equals(this.XmlNameSpace))
                {
                    return null;
                }
            }
            
            WebContent webContent = null;
          
            webContent = new WebContent();
            if (node.Attributes != null)
            {
                String value = node.Attributes[GDataParserNameTable.XmlAttributeUrl] != null ? 
                    node.Attributes[GDataParserNameTable.XmlAttributeUrl].Value : null; 
                if (value != null)
                {
                    webContent.Url = value;
                }

                value = node.Attributes[GDataParserNameTable.XmlAttributeDisplay] != null ?
                    node.Attributes[GDataParserNameTable.XmlAttributeDisplay].Value : null;
                if (value != null)
                {
                    webContent.Display = value;
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
              // single event, g:reminder is inside g:when
            if (node.HasChildNodes)
            {
                XmlNode gadgetPrefs = node.FirstChild;
                while (gadgetPrefs != null && gadgetPrefs is XmlElement)
                {
                    if (String.Compare(gadgetPrefs.NamespaceURI, XmlNameSpace, true) == 0)
                    {
                        if (String.Compare(gadgetPrefs.LocalName, GDataParserNameTable.XmlWebContentGadgetElement) == 0)
                        {
                            if (gadgetPrefs.Attributes != null)
                            {
                                string value = gadgetPrefs.Attributes[BaseNameTable.XmlValue] != null ? 
                                               gadgetPrefs.Attributes[BaseNameTable.XmlValue].Value : null;

                                string name  = gadgetPrefs.Attributes[BaseNameTable.XmlName] != null ?
                                               gadgetPrefs.Attributes[BaseNameTable.XmlName].Value : null;

                                if (name != null)
                                {
                                    webContent.GadgetPreferences.Add(name, value);
                                }
                            }
                        }
                    }
                    gadgetPrefs = gadgetPrefs.NextSibling;
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

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlNameSpace
        {
            get { return GDataParserNameTable.NSGCal; }
        }
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlPrefix
        {
            get { return GDataParserNameTable.gCalPrefix; }
        }

        /// <summary>
        /// Persistence method for the When object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.Url))
            {
                writer.WriteStartElement(XmlPrefix, XmlName, XmlNameSpace);
                writer.WriteAttributeString(GDataParserNameTable.XmlAttributeUrl, this.Url);
                writer.WriteAttributeString(GDataParserNameTable.XmlAttributeDisplay, this.Display);
                writer.WriteAttributeString(GDataParserNameTable.XmlAttributeHeight, this.Height.ToString());
                writer.WriteAttributeString(GDataParserNameTable.XmlAttributeWidth, this.Width.ToString());
                if (this.gadgetPrefs != null && this.gadgetPrefs.Count > 0)
                {
                    for (int i=0; i < this.gadgetPrefs.Count; i++)
                    {
                        string name = this.gadgetPrefs.GetKey(i) as string;
                        string value = this.gadgetPrefs.GetByIndex(i) as string;
                        if (name != null)
                        {
                            writer.WriteStartElement(XmlPrefix, 
                                                     GDataParserNameTable.XmlWebContentGadgetElement,
                                                     XmlNameSpace);
                            writer.WriteAttributeString(BaseNameTable.XmlName, name);
                            if (value != null)
                            {
                                writer.WriteAttributeString(BaseNameTable.XmlValue, value);
                            }
                            writer.WriteEndElement();
                        }
                    }
                }
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
