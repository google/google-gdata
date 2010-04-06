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
using System.Globalization;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Spreadsheets
{
    /// <summary>
    /// Entry API customization class for defining entries in a Cells feed.
    /// </summary>
    public class CellEntry : AbstractEntry
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
        public class CellElement : SimpleElement
        {
            /// <summary>
            /// default constructor for the Cell element
            /// </summary>
            public CellElement()
            : base(GDataSpreadsheetsNameTable.XmlCellElement, 
               GDataSpreadsheetsNameTable.Prefix,
               GDataSpreadsheetsNameTable.NSGSpreadsheets)
            {
                this.Attributes.Add(GDataSpreadsheetsNameTable.XmlAttributeRow, null);
                this.Attributes.Add(GDataSpreadsheetsNameTable.XmlAttributeColumn, null);
                this.Attributes.Add(GDataSpreadsheetsNameTable.XmlAttributeInputValue, null);
                this.Attributes.Add(GDataSpreadsheetsNameTable.XmlAttributeNumericValue, null);
            }



            /// <summary>
            /// The row the cell lies in
            /// </summary>
            [CLSCompliant(false)]
            public uint Row
            {
                get
                {
                    return Convert.ToUInt32(this.Attributes[GDataSpreadsheetsNameTable.XmlAttributeRow], CultureInfo.InvariantCulture); 
                }

                set
                {
                    this.Attributes[GDataSpreadsheetsNameTable.XmlAttributeRow] = value.ToString();
                }
            }

            /// <summary>
            /// The column the cell lies in
            /// </summary>
            [CLSCompliant(false)]
            public uint Column
            {
                get
                {
                    return Convert.ToUInt32(this.Attributes[GDataSpreadsheetsNameTable.XmlAttributeColumn], CultureInfo.InvariantCulture); 
                }

                set
                {
                    this.Attributes[GDataSpreadsheetsNameTable.XmlAttributeColumn] = value.ToString();
                }
            }

            /// <summary>
            /// The input (uncalculated) value for the cell
            /// </summary>
            public string InputValue
            {
                get
                {
                    return this.Attributes[GDataSpreadsheetsNameTable.XmlAttributeInputValue] as string;
                }

                set
                {
                    this.Attributes[GDataSpreadsheetsNameTable.XmlAttributeInputValue] = value;
                }
            }

            /// <summary>
            /// The numeric (calculated) value for the cell
            /// </summary>
            public string NumericValue
            {
                get
                {
                    return this.Attributes[GDataSpreadsheetsNameTable.XmlAttributeNumericValue] as string;
                }

                set
                {
                    this.Attributes[GDataSpreadsheetsNameTable.XmlAttributeNumericValue] = value;
                }
            }
        } // class Cell

#endregion

        /// <summary>
        /// Constructs a new CellEntry instance with the appropriate category
        /// to indicate that it is a cell.
        /// </summary>
        public CellEntry() : base()
        {
            Categories.Add(CELL_CATEGORY);
            this.AddExtension(new CellElement());
        }

        /// <summary>
        /// Constructs a new CellEntry instance with the provided content.
        /// </summary>
        /// <param name="inputValue">The uncalculated content of the cell.</param>
        public CellEntry(string inputValue): this()
        {
            this.Cell = new CellElement();
            this.Cell.InputValue = inputValue;
        }

        /// <summary>
        /// create a CellEntry for a given row/column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        [CLSCompliant(false)]
        public CellEntry(uint row, uint column)
            : this()
        {
            this.Cell = new CellElement();
            this.Cell.Column = column;
            this.Cell.Row = row; 
        }

        /// <summary>
        /// create a CellEntry for a given row/column and 
        /// initial value
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="inputValue">The uncalculated content of the cell.</param>
        [CLSCompliant(false)]
        public CellEntry(uint row, uint column, string inputValue)
            : this(row, column)
        {
            this.Cell.InputValue = inputValue;
        }


        /// <summary>
        /// The cell element in this cell entry
        /// </summary>
        public CellElement Cell
        {
            get
            {
                CellElement c = FindExtension(GDataSpreadsheetsNameTable.XmlCellElement,
                                     GDataSpreadsheetsNameTable.NSGSpreadsheets) as CellElement;
                if (c == null)
                {
                    c = new CellElement();
                    this.Cell = c; 
                }
                return c; 
            }
            set
            {
                ReplaceExtension(GDataSpreadsheetsNameTable.XmlCellElement,
                                     GDataSpreadsheetsNameTable.NSGSpreadsheets, value); 
                this.Dirty = true;
            }
        }


        /// <summary>
        /// The row the cell lies in
        /// </summary>
        [CLSCompliant(false)]
        public uint Row
        {
            get
            {
                return this.Cell.Row; 
            }
        
            set
            {
                this.Cell.Row = value;
                this.Dirty = true;
            }
        }
        
        /// <summary>
        /// The column the cell lies in
        /// </summary>
        [CLSCompliant(false)]
        public uint Column
        {
            get
            {
                return this.Cell.Column;
            }
        
            set
            {
                this.Cell.Column = value;
                this.Dirty = true;
            }
        }
        
        /// <summary>
        /// The input (uncalculated) value for the cell
        /// </summary>
        public string InputValue
        {
            get
            {
                return this.Cell.InputValue;
            }
        
            set
            {
                this.Cell.InputValue = value;
                this.Dirty = true;
            }
        }
        
        /// <summary>
        /// The numeric (calculated) value for the cell
        /// </summary>
        public string NumericValue
        {
            get
            {
                return this.Cell.NumericValue;
            }
        
            set
            {
                this.Cell.NumericValue = value;
                this.Dirty = true;
            }
        }

        /// <summary>
        /// The numeric (calculated) value for the cell
        /// </summary>
        public string Value
        {
            get
            {
                return this.Cell.Value;
            }
        
            set
            {
                this.Cell.Value = value;
                this.Dirty = true;
            }
        }
        

        /// <summary>
        /// add the spreadsheet NS
        /// </summary>
        /// <param name="writer">The XmlWrite, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer)
        {
            base.AddOtherNamespaces(writer);
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }
            string strPrefix = writer.LookupPrefix(GDataSpreadsheetsNameTable.NSGSpreadsheets);
            if (strPrefix == null)
            {
                writer.WriteAttributeString("xmlns", GDataSpreadsheetsNameTable.Prefix, null, GDataSpreadsheetsNameTable.NSGSpreadsheets);
            }
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
                   && String.Compare(node.Value, GDataSpreadsheetsNameTable.NSGSpreadsheets) == 0);
        }

    }
}
