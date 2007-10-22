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
using Google.GData.Extensions;

namespace Google.GData.Spreadsheets
{
    /// <summary>
    /// Feed API customization class for defining a Cells feed.
    /// </summary>
    public class CellFeed : AbstractFeed
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uriBase">The uri for this cells feed.</param>
        /// <param name="iService">The Spreadsheets service.</param>
        public CellFeed(Uri uriBase, IService iService) : base(uriBase, iService)
        {
            this.AddExtension(new ColCountElement());
            this.AddExtension(new RowCountElement());
        }

        /// <summary>
        /// The number of rows in this cell feed, as a RowCountElement
        /// </summary>
        public RowCountElement RowCount
        {
            get
            {
                return FindExtension(GDataSpreadsheetsNameTable.XmlRowCountElement,
                                     GDataSpreadsheetsNameTable.NSGSpreadsheets) as RowCountElement;
            }
        }

        /// <summary>
        /// The number of columns in this cell feed, as a ColCountElement
        /// </summary>
        public ColCountElement ColCount
        {
            get
            {
                return FindExtension(GDataSpreadsheetsNameTable.XmlColCountElement,
                                     GDataSpreadsheetsNameTable.NSGSpreadsheets) as ColCountElement;
            }
        }

   
        /// <summary>checks if this is a namespace 
        /// decl that we already added</summary> 
        /// <param name="node">XmlNode to check</param>
        /// <returns>true if this node should be skipped </returns>
        protected override bool SkipNode(XmlNode node)
        {
            if (base.SkipNode(node) == true)
            {
                return true;
            }

            return node.NodeType == XmlNodeType.Attribute
            && (node.Name.StartsWith("xmlns") == true)
            && (String.Compare(node.Value, GDataSpreadsheetsNameTable.NSGSpreadsheets) == 0);
        }

        /// <summary>
        /// creates our cellfeed type entry
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new CellEntry();
        }
    }
}
