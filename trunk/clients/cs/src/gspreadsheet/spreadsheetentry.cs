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
    public class SpreadsheetEntry : AtomEntry
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
        /// Convenience method for getting the Link for the spreadsheet that could be used
        /// for viewing the spreadsheet in the web browser.
        /// </summary>
        public AtomLink Link
        {
            get
            {
                return base.Links.FindService(BaseNameTable.ServiceAlternate, null);
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
        /// <param name="spreadsheetNode">A g-scheme, xml node</param>
        /// <param name="parser">The AtomFeedParser that called this</param>
        public void ParseSpreadsheet(XmlNode spreadsheetNode, AtomFeedParser parser)
        {
            // TODO: Add handling if this entry type becomes more complex
        }
    }
}
