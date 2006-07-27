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
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Extensions 
{

    /// <summary>
    /// GData Schema describing a place or location
    /// </summary>
    public class Where : IExtensionElement
    {

        /// <summary>
        /// Relation type. Describes the meaning of this location.
        /// </summary>
        public class RelType
        {
            /// <summary>
            /// The standard relationship EVENT_ALTERNATE
            /// </summary>
            public static string EVENT = null;
            /// <summary>
            /// the alternate EVENT location
            /// </summary>
            public static string EVENT_ALTERNATE = BaseNameTable.gNamespacePrefix + "event.alternate";
            /// <summary>
            ///  the parking location
            /// </summary>
            public static string EVENT_PARKING = BaseNameTable.gNamespacePrefix + "event.parking";
        }

        /// <summary>
        /// Constructs an empty Where instance
        /// </summary>
        public Where()
        {
        }

        /// <summary>
        /// default constructor, takes 3 parameters
        /// </summary>
        /// <param name="valueString">the valueString property value</param>
        /// <param name="label">label property value</param>
        /// <param name="rel">default for the Rel property value</param>
        public Where(String rel,
                                 String label, 
                                 String valueString)
        {
            this.Rel = rel;
            this.Label = label;
            this.ValueString = valueString;
        }

        private string rel;
        private string label;
        private string valueString;
        private EntryLink entryLink;

        /// <summary>
        /// Rel property accessor
        /// </summary>
        public string Rel
        {
            get { return rel; }
            set { rel = value; }
        }

        /// <summary>
        /// User-readable label that identifies this location.
        /// </summary>
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        /// <summary>
        /// String description of the event places.
        /// </summary>
        public string ValueString
        {
            get { return valueString; }
            set { valueString = value; }
        }

        /// <summary>
        ///  Nested entry (optional).
        /// </summary>
        public EntryLink EntryLink
        {
            get { return entryLink; }
            set { entryLink = value; }
        }

        #region Where Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an xml node to create a Where object</summary> 
        /// <param name="node">where node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns> the created Where object</returns>
        //////////////////////////////////////////////////////////////////////
        public static Where ParseWhere(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            Where where = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.XmlWhereElement))
            {
                where = new Where();
                if (node.Attributes != null)
                {
                    if (node.Attributes[GDataParserNameTable.XmlAttributeRel] != null)
                    {
                        where.Rel = node.Attributes[GDataParserNameTable.XmlAttributeRel].Value;
                    }

                    if (node.Attributes[GDataParserNameTable.XmlAttributeLabel] != null)
                    {
                        where.Label = node.Attributes[GDataParserNameTable.XmlAttributeLabel].Value;
                    }

                    if (node.Attributes[GDataParserNameTable.XmlAttributeValueString] != null)
                    {
                        where.ValueString = node.Attributes[GDataParserNameTable.XmlAttributeValueString].Value;
                    }
                }

                if (node.HasChildNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.LocalName == GDataParserNameTable.XmlEntryLinkElement)
                        {
                            if (where.EntryLink == null)
                            {
                                where.EntryLink = EntryLink.ParseEntryLink(childNode, parser);
                            }
                            else
                            {
                                throw new ArgumentException("Only one entryLink is allowed inside the g:where");
                            }
                        }
                    }
                }
            }

            return where;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.XmlWhereElement; }
        }


        /// <summary>
        /// Persistence method for the Where object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.Label) ||
                Utilities.IsPersistable(this.Rel) ||
                Utilities.IsPersistable(this.ValueString) ||
                entryLink != null)
            {

                writer.WriteStartElement(BaseNameTable.gDataPrefix, XmlName, BaseNameTable.gNamespace);

                if (Utilities.IsPersistable(this.Label))
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeLabel, this.Label);
                }

                if (Utilities.IsPersistable(this.Rel))
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeRel, this.Rel);
                }

                if (Utilities.IsPersistable(this.ValueString))
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeValueString, this.valueString);
                }

                if (entryLink != null)
                {
                    entryLink.Save(writer);
                }

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
