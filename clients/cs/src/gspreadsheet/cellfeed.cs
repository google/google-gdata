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

        private RowCountElement rowCount;
        private ColCountElement colCount;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uriBase">The uri for this cells feed.</param>
        /// <param name="iService">The Spreadsheets service.</param>
        public CellFeed(Uri uriBase, IService iService) : base(uriBase, iService)
        {
        }

        /// <summary>
        /// The number of rows in this cell feed
        /// </summary>
        public RowCountElement RowCount
        {
            get
            {
                return rowCount;
            }

            set
            {
                rowCount = value;
            }
        }

        /// <summary>
        /// The number of columns in this cell feed
        /// </summary>
        public ColCountElement ColCount
        {
            get
            {
                return colCount;
            }

            set
            {
                colCount = value;
            }
        }

        /// <summary>Parses the inner state of the element.</summary>
        /// <param name="worksheetNode">a g-scheme, xml node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        public void parseWorksheet(XmlNode worksheetNode, AtomFeedParser parser)
        {
            if (String.Compare(worksheetNode.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
            {
                if (worksheetNode.LocalName == GDataSpreadsheetsNameTable.XmlColCountElement)
                {
                    if (this.ColCount == null)
                    {
                        this.ColCount = ColCountElement.ParseColCount(worksheetNode);
                    }
                    else
                    {
                        throw new ArgumentException("Only one gs:colcount element is valid in the Cell Feeds");
                    }
                }
                else if (worksheetNode.LocalName == GDataSpreadsheetsNameTable.XmlRowCountElement)
                {
                    if (this.RowCount == null)
                    {
                        this.RowCount = RowCountElement.ParseRowCount(worksheetNode);
                    }
                    else
                    {
                        throw new ArgumentException("Only one gs:rowcount element is valid in the Cell Feeds");
                    }
                }
            }
        }

        /// <summary>empty base implementation</summary> 
        /// <param name="writer">the xmlwriter, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer)
        {
            base.AddOtherNamespaces(writer);
            Utilities.EnsureGDataNamespace(writer);
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
            && (String.Compare(node.Value, BaseNameTable.gNamespace) == 0)
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

    
        /// <summary>
        /// get's called after we already handled the custom entry, to handle all 
        /// other potential parsing tasks
        /// </summary>
        /// <param name="e"></param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (String.Compare(e.ExtensionElement.NamespaceURI, GDataSpreadsheetsNameTable.NSGSpreadsheets, true) == 0)
            {
                if (e.ExtensionElement.LocalName == GDataSpreadsheetsNameTable.XmlColCountElement)
                {
                    this.ColCount = ColCountElement.ParseColCount(e.ExtensionElement);
                    e.DiscardEntry = true;
                }
                else if (e.ExtensionElement.LocalName == GDataSpreadsheetsNameTable.XmlRowCountElement)
                {
                    this.RowCount = RowCountElement.ParseRowCount(e.ExtensionElement);
                    e.DiscardEntry = true;
                }
            }
        }
    }
}
