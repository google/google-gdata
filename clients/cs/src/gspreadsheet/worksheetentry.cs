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
    /// Entry API customization class for defining entries in a Worksheets feed.
    /// </summary>
    public class WorksheetEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain Cell extension data.
        /// </summary>
        public static AtomCategory WORKSHEET_CATEGORY
                = new AtomCategory(GDataSpreadsheetsNameTable.Worksheet,
                           new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new WorksheetEntry instance with the appropriate category
        /// to indicate that it is a worksheet.
        /// </summary>
        public WorksheetEntry() : base()
        {
            Categories.Add(WORKSHEET_CATEGORY);
            this.AddExtension(new RowCountElement());
            this.AddExtension(new ColCountElement());
        }

        /// <summary>
        /// Constructs a new WorksheetEntry instance with the given number of
        /// rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="cols">The number of columns.</param>
        [CLSCompliant(false)]
        public WorksheetEntry(uint rows, uint cols)
            : this()
        {
            this.ColCount = new ColCountElement(cols);
            this.RowCount = new RowCountElement(rows);
        }

        /// <summary>
        /// Constructs a new WorksheetEntry instance with a given number of
        /// rows and columns and with a set title.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="cols">The number of columns.</param>
        /// <param name="title">The title of the worksheet.</param>
        [CLSCompliant(false)]
        public WorksheetEntry(uint rows, uint cols, string title)
            : this(rows, cols)
        {
            this.Title = new AtomTextConstruct(AtomTextConstructElementType.Title,title);
        }

        /// <summary>
        /// The colCount element in this worksheet entry
        /// </summary>
        public ColCountElement ColCount
        {
            get
            {
                return FindExtension(GDataSpreadsheetsNameTable.XmlColCountElement,
                                     GDataSpreadsheetsNameTable.NSGSpreadsheets) as ColCountElement;
            }

            set
            {
                ReplaceExtension(GDataSpreadsheetsNameTable.XmlColCountElement,
                                     GDataSpreadsheetsNameTable.NSGSpreadsheets, value);
            }
        }

        /// <summary>
        /// Sets the number of columns for this worksheet entry
        /// </summary>
        [CLSCompliant(false)]
        public uint Cols
        {
            get { return this.ColCount.Count; }
            set { this.ColCount.Count = value; }
        }


        /// <summary>
        /// The rowCount element in this cell entry
        /// </summary>
        public RowCountElement RowCount
        {
            get
            {
                return FindExtension(GDataSpreadsheetsNameTable.XmlRowCountElement,
                                     GDataSpreadsheetsNameTable.NSGSpreadsheets) as RowCountElement;
            }

            set
            {
                ReplaceExtension(GDataSpreadsheetsNameTable.XmlRowCountElement,
                                     GDataSpreadsheetsNameTable.NSGSpreadsheets, value);
            }
        }

        /// <summary>
        /// Sets the number of rows for this worksheet entry
        /// </summary>
        [CLSCompliant(false)]
        public uint Rows
        {
            get { return this.RowCount.Count; }
            set { this.RowCount.Count = value; }
        }


        /// <summary>
        /// Retrieves the cell-based metafeed of the cells within the worksheet.
        /// </summary>
        /// <returns>The CellsFeed of the cells in this worksheet.</returns>
        public CellFeed QueryCellFeed() 
        {
            return QueryCellFeed(ReturnEmptyCells.serverDefault); 
        }

        /// <summary>
        /// Retrieves the cell-based metafeed of the cells within the worksheet.
        /// </summary>
        /// <param name="returnEmpty">indicates if a full sheet should be returned</param> 
        /// <returns>The CellsFeed of the cells in this worksheet.</returns>
        public CellFeed QueryCellFeed(ReturnEmptyCells returnEmpty) 
        {
            CellQuery query = new CellQuery(this.CellFeedLink);
            query.ReturnEmpty = returnEmpty;
            return this.Service.Query(query) as CellFeed; 
        }

        /// <summary>
        /// Retrieves the URI for the cells feed of the worksheet.
        /// </summary>
        /// <returns>The URI of the cells feed for this worksheet.</returns>
        public string CellFeedLink
        {
            get 
            {
                AtomLink cellFeedLink = this.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null);
                return cellFeedLink.HRef.ToString();
            }
        }
    }
}
