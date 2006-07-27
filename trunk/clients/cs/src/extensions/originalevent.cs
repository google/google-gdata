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

namespace Google.GData.Extensions {

    /// <summary>
    /// GData schema extension describing the original recurring event.
    /// </summary>
    public class OriginalEvent : IExtensionElement
    {

        /// <summary>
        /// Href to the original recurring event entry.
        /// </summary>
        protected string href;

        /// <summary>
        /// ID to the orignal recurring event entry.
        /// </summary>
        protected string idOriginal;

        /// <summary>
        ///  holds the original starttime of the event
        /// </summary>
        protected When originalStartTime;

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public Href</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Href
        {
            get { return href; }
            set { href = value; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public IdOriginal</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string IdOriginal
        {
            get { return idOriginal; }
            set { idOriginal = value; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public original Start Time</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public When OriginalStartTime
        {
            get { return originalStartTime; }
            set { originalStartTime = value; }
        }

        #region Original Event Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create an OriginalEvent object.</summary> 
        /// <param name="node">originalEvent node</param>
        /// <returns> the created OriginalEvent object</returns>
        //////////////////////////////////////////////////////////////////////
        public static OriginalEvent ParseOriginal(XmlNode node)
        {
            Tracing.TraceCall();
            OriginalEvent originalEvent = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.XmlOriginalEventElement))
            {
                originalEvent = new OriginalEvent();
                if (node.Attributes != null)
                {
                    if (node.Attributes[GDataParserNameTable.XmlAttributeId] != null)
                    {
                        originalEvent.IdOriginal = node.Attributes[GDataParserNameTable.XmlAttributeId].Value;
                    }

                    if (node.Attributes[GDataParserNameTable.XmlAttributeHref] != null)
                    {
                        originalEvent.Href = node.Attributes[GDataParserNameTable.XmlAttributeHref].Value;
                    }
                }

                if (node.HasChildNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.LocalName == GDataParserNameTable.XmlWhenElement)
                        {
                            if (originalEvent.OriginalStartTime == null)
                            {
                                originalEvent.OriginalStartTime = When.ParseWhen(childNode);
                            }
                            else
                            {
                                throw new ArgumentException("Only one g:when is allowed inside the g:orginalEvent");
                            }
                        }
                    }
                }
            }

            if (originalEvent.IdOriginal == null)
            {
                throw new ArgumentException("g:originalEvent/@id is required.");
            }

            if (originalEvent.OriginalStartTime == null)
            {
                throw new ArgumentException("g:when inside g:originalEvent is required.");
            }

            return originalEvent;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.XmlOriginalEventElement; }
        }

        /// <summary>
        /// Persistence method for the OriginalEvent object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.Href) ||
                Utilities.IsPersistable(this.IdOriginal) ||
                this.OriginalStartTime != null)
            {
                writer.WriteStartElement(BaseNameTable.gDataPrefix, XmlName, BaseNameTable.gNamespace);
    
                if (Utilities.IsPersistable(this.Href))
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeHref, this.Href);
                }
    
                if (Utilities.IsPersistable(this.IdOriginal))
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeId, this.IdOriginal);
                } 
                else
                {
                    throw new ArgumentException("g:originalEvent/@id is required.");
                }
    
                if (this.OriginalStartTime != null)
                {
                    OriginalStartTime.Save(writer);
                } 
                else
                {
                    throw new ArgumentException("g:when inside g:originalEvent is required.");
                }
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
