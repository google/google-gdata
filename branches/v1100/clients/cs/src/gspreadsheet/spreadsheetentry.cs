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
    /// Entry API customization class for defining entries in a Spreadsheets feed.
    /// </summary>
    public class SpreadsheetEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain Cell extension data.
        /// </summary>
        public static AtomCategory SPREADSHEET_CATEGORY
        = new AtomCategory(GDataSpreadsheetsNameTable.Spreadsheet,
                           new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new SpreadsheetEntry instance with the appropriate category
        /// to indicate that it is a worksheet.
        /// </summary>
        public SpreadsheetEntry()
        : base()
        {
            Categories.Add(SPREADSHEET_CATEGORY);
        }

        /// <summary>
        /// Parses the inner state of the element. TODO. 
        /// </summary>
        /// <param name="e">The extension element that should be added to this entry</param>
        /// <param name="parser">The AtomFeedParser that called this</param>
        public override void Parse(ExtensionElementEventArgs e, AtomFeedParser parser)  
        {
        }
    }
}
