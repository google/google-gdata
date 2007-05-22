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
using Google.GData.Client;
using Google.GData.Extensions;


namespace Google.GData.Spreadsheets
{
    /// <summary>
    /// Name Table for string constants used in the Spreadsheets
    /// GData feeds.
    /// </summary>
    public class GDataSpreadsheetsNameTable : GDataParserNameTable
    {
        /// <summary>indicates a spreadsheet feed in the URI</summary>
        public const string FeedSpreadsheet = "spreadsheets";
       /// <summary>indicates a worksheet feed in the URI</summary>
        public const string FeedWorksheet = "worksheets";
       /// <summary>indicates a list feed in the URI</summary>
        public const string FeedList = "list";
       /// <summary>indicates a cell feed in the URI</summary>
        public const string FeedCell = "cells";

        /// <summary>Spreadsheets namespace</summary>
        public const string NSGSpreadsheets = "http://schemas.google.com/spreadsheets/2006";

        /// <summary>Spreadsheets extended namespace</summary>
        public const string NSGSpreadsheetsExtended = NSGSpreadsheets + "/extended";

        /// <summary>Spreadsheets namespace prefix</summary>
        public const string NSGSpreadsheetsPrefix = NSGSpreadsheets + "#";

        /// <summary>Prefix for spreadsheets extensions if writing</summary>
        public const string Prefix = "gs";

        /// <summary>Prefix for spreadsheets custom extensions if writing</summary>
        public const string ExtendedPrefix = "gsx";

        /// <summary>Link "rel" for cells feed</summary>
        public const string CellRel = NSGSpreadsheetsPrefix + "cellsfeed";

        /// <summary>Link "rel" for list feed</summary>
        public const string ListRel = NSGSpreadsheetsPrefix + "listfeed";

        /// <summary>Link "rel" for worksheet feed</summary>
        public const string WorksheetRel = NSGSpreadsheetsPrefix + "worksheetsfeed";

        /// <summary>The spreadsheet prefix </summary>
        public const string Spreadsheet = NSGSpreadsheetsPrefix + FeedSpreadsheet;

        /// <summary>The cell prefix </summary>
        public const string Cell = NSGSpreadsheetsPrefix + FeedCell;
        /// <summary>The list prefix </summary>
        public const string List = NSGSpreadsheetsPrefix + FeedList;

        /// <summary>The worksheet prefix </summary>
        public const string Worksheet = NSGSpreadsheetsPrefix + FeedWorksheet;
        /// <summary>The sources prefix </summary>
        public const string Source = NSGSpreadsheetsPrefix + "source"; 

#region Elements

        /// <summary>Static string for parsing</summary>
        public const string XmlCellElement = "cell";
        /// <summary>Static string for parsing</summary>
        public const string XmlColCountElement = "colCount";
        /// <summary>Static string for parsing</summary>
        public const string XmlRowCountElement = "rowCount";
#endregion

#region Attributes

        /// <summary>String for row attribute</summary>
        public const string XmlAttributeRow = "row";
        /// <summary>String for col attibute</summary>
        public const string XmlAttributeColumn = "col";
        /// <summary>String for inputValue attibute</summary>
        public const string XmlAttributeInputValue = "inputValue";
        /// <summary>String for numericValue attribute</summary>
        public const string XmlAttributeNumericValue = "numericValue";

#endregion
    }
}
