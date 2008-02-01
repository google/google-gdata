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
#region Using directives

using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using System.Xml;
using Google.GData.Client;

#endregion

namespace Google.GData.GoogleBase
{

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Object model for gm:attribute XML tags
    ///
    /// The histogram feeds returns entries with exactly one gm:attribute
    /// tag.
    ///
    /// AttributeHistogram usually come from GoogleBaseEntry found in
    /// an histogram feed using: GoogleBaseEntry.AttributeHistogram
    /// </summary>
    ///////////////////////////////////////////////////////////////////////
    public class AttributeHistogram : IExtensionElement
    {
        private readonly string name;
        private readonly GBaseAttributeType type;
        private readonly int count;
        private readonly HistogramValue[] values;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an AttributeHistogram with no example values.
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">attribute type</param>
        /// <param name="count">number of times this attribute appeared
        /// in the histogram query results</param>
        ///////////////////////////////////////////////////////////////////////
        public AttributeHistogram(string name,
                                  GBaseAttributeType type,
                                  int count)
                :this(name, type, count, null)
        {

        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an AttributeHistogram.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">attribute type</param>
        /// <param name="count">number of times this attribute appeared
        /// in the histogram query results</param>
        /// <param name="values">example values, may be empty or null
        /// in the histogram query results</param>
        ///////////////////////////////////////////////////////////////////////
        public AttributeHistogram(string name,
                                  GBaseAttributeType type,
                                  int count,
                                  HistogramValue[] values)
        {
            this.name = name;
            this.type = type;
            this.count = count;
            this.values = values == null ? new HistogramValue[0] : values;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Partial string representation, for debugging.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "AttributeHistogram:" + name + "(" + type + ")";
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Attribute name</summary>
        ///////////////////////////////////////////////////////////////////////
        public string Name
        {
            get
            {
                return name;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Attribute type</summary>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttributeType Type
        {
            get
            {
                return type;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Number of times this attribute appeared in the histogram
        /// query results.</summary>
        ///////////////////////////////////////////////////////////////////////
        public int Count
        {
            get
            {
                return count;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>The most common values found for this attribute in
        /// the histogram query results. It might be empty.</summary>
        ///////////////////////////////////////////////////////////////////////
        public HistogramValue[] Values
        {
            get
            {
                return values;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Parses a gm:attribute tag and create the corresponding
        /// AttributeHistogram object.</summary>
        ///////////////////////////////////////////////////////////////////////
        public static AttributeHistogram Parse(XmlNode node)
        {
            string name = node.Attributes["name"].Value;
            GBaseAttributeType type =
                GBaseAttributeType.ForName(node.Attributes["type"].Value);
            int count = NumberFormat.ToInt(node.Attributes["count"].Value);

            ArrayList values = new ArrayList();
            for (XmlNode child = node.FirstChild;
                    child != null;
                    child = child.NextSibling)
            {
                if (child.LocalName == "value")
                {
                    int valueCount = NumberFormat.ToInt(child.Attributes["count"].Value);
                    values.Add(new HistogramValue(child.InnerText, valueCount));
                }
            }
            return new AttributeHistogram(name, type, count,
                                          (HistogramValue[])values.ToArray(typeof(HistogramValue)));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates XML code for the current tag.</summary>
        ///////////////////////////////////////////////////////////////////////
        public void Save(XmlWriter writer)
        {
            writer.WriteStartElement(GBaseNameTable.GBaseMetaPrefix,
                                     "attribute",
                                     GBaseNameTable.NSGBaseMeta);
            writer.WriteAttributeString("name", name);
            writer.WriteAttributeString("type", type.Name);
            writer.WriteAttributeString("count", NumberFormat.ToString(count));

            foreach (HistogramValue value in values)
            {
                writer.WriteStartElement(GBaseNameTable.GBaseMetaPrefix,
                                         "value",
                                         GBaseNameTable.NSGBaseMeta);
                writer.WriteAttributeString("count", NumberFormat.ToString(value.Count));
                writer.WriteString(value.Content);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

    }

    ///////////////////////////////////////////////////////////////////////
    /// <summary>An example value of an AttributeHistogram.</summary>
    ///////////////////////////////////////////////////////////////////////
    public struct HistogramValue
    {
        private readonly string content;
        private readonly int count;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an example value.</summary>
        /// <param name="content">the example itself, as a string</param>
        /// <param name="count">number of times this particular value
        /// appeared in the histogram query results</param>
        ///////////////////////////////////////////////////////////////////////
        public HistogramValue(string content, int count)
        {
            this.content = content;
            this.count = count;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the example itself, as a string.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return content;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>The example itself, as a string</summary>
        ///////////////////////////////////////////////////////////////////////
        public string Content
        {
            get
            {
                return content;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Number of times this particular value appeared
        /// in the histogram query</summary>
        ///////////////////////////////////////////////////////////////////////
        public int Count
        {
            get
            {
                return count;
            }
        }
    }

}
