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
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Spreadsheets
{
    /// <summary>
    /// Entry API customization class for defining entries in a Cells feed.
    /// </summary>
    public class CellEntry : AtomEntry
    {
        /// <summary>
        /// Category used to label entries that contain Cell extension data.
        /// </summary>
        public static AtomCategory CELL_CATEGORY 
        = new AtomCategory(GDataSpreadsheetsNameTable.Cell, 
                           new AtomUri(BaseNameTable.gKind));

#region Schema Extensions

        /// <summary>
        /// GData schema extension describing a Cell in a spreadsheet.
        /// </summary>
        public class CellElement : IExtensionElement
        {
            private uint row;
            private uint column;
            private string inputValue;
            private string numericValue;
            private string value;

            /// <summary>
            /// Constructs an empty cell
            /// </summary>
            public CellElement()
            {
                Row = 0;
                Column = 0;
                InputValue = null;
                NumericValue = null;
                Value = null;
            }

            /// <summary>
            /// The row the cell lies in
            /// </summary>
            public uint Row
            {
                get
                {
                    return row;
                }

                set
                {
                    row = value;
                }
            }

            /// <summary>
            /// The column the cell lies in
            /// </summary>
            public uint Column
            {
                get
                {
                    return column;
                }

                set
                {
                    column = value;
                }
            }

            /// <summary>
            /// The input (uncalculated) value for the cell
            /// </summary>
            public string InputValue
            {
                get
                {
                    return inputValue;
                }

                set
                {
                    if (value == null)
                    {
                        inputValue = "";
                    }
                    else
                    {
                        inputValue = value;
                    }
                }
            }

            /// <summary>
            /// The numeric (calculated) value for the cell
            /// </summary>
            public string NumericValue
            {
                get
                {
                    return numericValue;
                }

                set
                {
                    if (value == null)
                    {
                        numericValue = "";
                    }
                    else
                    {
                        numericValue = value;
                    }
                }
            }

            /// <summary>
            /// The display value of the cell
            /// </summary>
            public string Value
            {
                get
                {
                    return value;
                }

                set
                {
                    this.value = value;
                }
            }

            /// <summary>
            /// Parses an XML node to create a Cell object
            /// </summary>
            /// <param name="node">Cell node</param>
            /// <param name="parser">AtomFeedParser to use</param>
            /// <returns>The created Cell object</returns>
            public static CellElement ParseCell(XmlNode node, AtomFeedParser parser)
            {
                CellElement cell = null;
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }

                object localname = node.LocalName;
                if (localname.Equals(GDataSpreadsheetsNameTable.XmlCellElement))
                {
                    cell = new CellElement();
                    if (node.Attributes != null)
                    {
                        if (node.Attributes[GDataSpreadsheetsNameTable.XmlAttributeRow] != null)
                        {
                            cell.Row = uint.Parse(node.Attributes[GDataSpreadsheetsNameTable.XmlAttributeRow].Value);
                        }

                        if (node.Attributes[GDataSpreadsheetsNameTable.XmlAttributeColumn] != null)
                        {
                            cell.Column = uint.Parse(node.Attributes[GDataSpreadsheetsNameTable.XmlAttributeColumn].Value);
                        }

                        if (node.Attributes[GDataSpreadsheetsNameTable.XmlAttributeInputValue] != null)
                        {
                            cell.InputValue = node.Attributes[GDataSpreadsheetsNameTable.XmlAttributeInputValue].Value;
                        }

                        if (node.Attributes[GDataSpreadsheetsNameTable.XmlAttributeNumericValue] != null)
                        {
                            cell.NumericValue = node.Attributes[GDataSpreadsheetsNameTable.XmlAttributeNumericValue].Value;
                        }

                    }

                    if (node.HasChildNodes && node.FirstChild.NodeType != XmlNodeType.Text)
                    {
                        throw new ArgumentException("Cell entry allows for 0 children.");
                    }

                    cell.Value = node.FirstChild.Value;
                }

                return cell;
            }

#region overload for persistence
            /// <summary>
            /// Returns the constant representing the XML element.
            /// </summary>
            public string XmlName
            {
                get
                {
                    return GDataSpreadsheetsNameTable.XmlCellElement;
                }
            }

            /// <summary>
            /// Used to save the EntryLink instance into the passed in xmlwriter
            /// </summary>
            /// <param name="writer">the XmlWriter to write into</param>
            public void Save(XmlWriter writer)
            {
                writer.WriteStartElement(GDataSpreadsheetsNameTable.Prefix, 
                                         XmlName, GDataSpreadsheetsNameTable.NSGSpreadsheets);
                writer.WriteAttributeString(GDataSpreadsheetsNameTable.XmlAttributeRow, Row.ToString());
                writer.WriteAttributeString(GDataSpreadsheetsNameTable.XmlAttributeColumn, Column.ToString());
                if (InputValue != null && InputValue.Length > 0)
                {
                    writer.WriteAttributeString(GDataSpreadsheetsNameTable.XmlAttributeInputValue, InputValue);
                }

                if (NumericValue != null && NumericValue.Length > 0)
                {
                    writer.WriteAttributeString(GDataSpreadsheetsNameTable.XmlAttributeNumericValue, NumericValue);
                }
                writer.WriteString(Value);
                writer.WriteEndElement();
            }
#endregion
        } // class Cell

#endregion

        private CellElement cell;

        /// <summary>
        /// Constructs a new CellEntry instance with the appropriate category
        /// to indicate that it is a cell.
        /// </summary>
        public CellEntry() : base()
        {
            Categories.Add(CELL_CATEGORY);
            cell = new CellElement();
        }

        /// <summary>
        /// The cell element in this cell entry
        /// </summary>


        public CellElement Cell
        {
            get { return cell;}
            set
            {
                if (cell != null)
                {
                    ExtensionElements.Remove(cell);
                }
                cell = value; 
                ExtensionElements.Add(cell);
            }
        }

        /// <summary>
        /// Empty base implementation
        /// </summary>
        /// <param name="writer">The XmlWrite, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer)
        {
            base.AddOtherNamespaces(writer);
            Utilities.EnsureGDataNamespace(writer);
        }

        /// <summary>
        /// Checks if this is a namespace declaration that we already added
        /// </summary>
        /// <param name="node">XmlNode to check</param>
        /// <returns>True if this node should be skipped</returns>
        protected override bool SkipNode(XmlNode node)
        {
            if (base.SkipNode(node))
            {
                return true;
            }

            return(node.NodeType == XmlNodeType.Attribute
                   && node.Name.StartsWith("xmlns")
                   && String.Compare(node.Value, BaseNameTable.gNamespace) == 0
                   && String.Compare(node.Value, GDataSpreadsheetsNameTable.NSGSpreadsheets) == 0);
        }

        /// <summary>
        /// Parses the inner state of the element
        /// </summary>
        /// <param name="cellNode">A g-scheme, xml node</param>
        /// <param name="parser">The AtomFeedParser that called this</param>
        public void ParseCell(XmlNode cellNode, AtomFeedParser parser)
        {
            if (String.Compare(cellNode.NamespaceURI, GDataSpreadsheetsNameTable.NSGSpreadsheets, true) == 0)
            {
                if (cellNode.LocalName == GDataSpreadsheetsNameTable.XmlCellElement)
                {
                    Cell = CellElement.ParseCell(cellNode, parser);
                }
            }
        }
    }
}
