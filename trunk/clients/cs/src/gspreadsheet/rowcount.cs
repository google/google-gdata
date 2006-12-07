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
using Google.GData.Client;

namespace Google.GData.Spreadsheets
{
    /// <summary>
    /// GData schema extension for rowCount element.
    /// </summary>
    public class RowCountElement : IExtensionElement
    {
        private uint count;

        /// <summary>
        /// Constructs an empty row count element.
        /// </summary>
        public RowCountElement()
        {
            Count = 0;
        }

        /// <summary>
        /// Gets or sets the count of rows.
        /// </summary>
        public uint Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }

        /// <summary>
        /// Parses an XML node to create a RowCount object
        /// </summary>
        /// <param name="node">RowCount node</param>
        /// <returns>The created RowCount object</returns>
        public static RowCountElement ParseRowCount(XmlNode node)
        {
            RowCountElement count = null;
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataSpreadsheetsNameTable.XmlRowCountElement))
            {
                count = new RowCountElement();
                if (node.Attributes.Count > 1)
                {
                    throw new ArgumentException("rowCount element should have 0 attributes.");
                }

                if (node.HasChildNodes && node.FirstChild.NodeType != XmlNodeType.Text)
                {
                    throw new ArgumentException("rowCount element should have 0 children.");
                }

                count.Count = UInt32.Parse(node.FirstChild.Value);
            }

            return count;
        }

#region overload for persistence
        /// <summary>
        /// Returns the constant representing the XML element.
        /// </summary>
        public string XmlName
        {
            get
            {
                return GDataSpreadsheetsNameTable.XmlRowCountElement;
            }
        }

        /// <summary>
        /// Used to save the RowCount instance into the passed in xmlwriter
        /// </summary>
        /// <param name="writer">the XmlWriter to write into</param>
        public void Save(XmlWriter writer)
        {
            writer.WriteStartElement(GDataSpreadsheetsNameTable.Prefix,
                                     XmlName, GDataSpreadsheetsNameTable.NSGSpreadsheets);
            writer.WriteString(Count.ToString());
            writer.WriteEndElement();
        }
#endregion
    } // class RowCount
}
