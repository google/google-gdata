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
using System.Net;
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
            if (base.SkipNode(node))
            {
                return true;
            }

            return node.NodeType == XmlNodeType.Attribute &&
				   node.Name.StartsWith("xmlns") && 
				   (String.Compare(node.Value, GDataSpreadsheetsNameTable.NSGSpreadsheets) == 0);
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
        /// returns an update URI for a given row/column combination
        /// in general that URI is based on the feeds POST feed plus the
        /// cell address in RnCn notation:
        /// https://spreadsheets.google.com/feeds/cells/key/worksheetId/private/full/cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>string</returns>
        [CLSCompliant(false)]
        public string CellUri(uint row, uint column)
        {
            string target = this.Post;
            if (!target.EndsWith("/"))
            {
                target += "/";
            }
            target += "R" + row.ToString() + "C" + column.ToString();
            return target;
        }
   
        /// <summary>
        /// returns the given CellEntry object. Note that the getter will go to the server
        /// to get CellEntries that are NOT yet on the client
        /// </summary>
        /// <returns>CellEntry</returns>
        [CLSCompliant(false)]
        public CellEntry this[uint row, uint column]
        {
            get 
            {
                // let's find the cell
                foreach (CellEntry entry in this.Entries )
                {
                    CellEntry.CellElement cell = entry.Cell;
                    if (cell.Row == row && cell.Column == column)
                    {
                        return entry; 
                    }
                }
                // if we are here, we need to get the entry from the service
                string url = CellUri(row, column);
                CellQuery query = new CellQuery(url);

                CellFeed feed = this.Service.Query(query) as CellFeed;
                CellEntry newEntry = feed.Entries[0] as CellEntry;
                this.Entries.Add(newEntry);
                // we don't want this one to show up in the batch feed on it's own
                newEntry.Dirty = false;
                return newEntry;
            }
        }


        /// <summary>
        /// deletes a cell by using row and column addressing
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        [CLSCompliant(false)]
        public void Delete(uint row, uint column)
        {
            // now we need to create a new guy
            string url = CellUri(row, column);
            this.Service.Delete(new Uri(url));
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>uses GData batch to batchupdate the cell feed. If the returned
        /// batch result set contained an error, it will throw a GDataRequestBatchException</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public override void Publish()
        {
            if (this.Batch == null)
            {
                throw new InvalidOperationException("This feed has no batch URI");
            }

            AtomFeed batchFeed = CreateBatchFeed(GDataBatchOperationType.update);
            if (batchFeed != null)
            {
                AtomFeed resultFeed = this.Service.Batch(batchFeed, new Uri(this.Batch));
                foreach (AtomEntry resultEntry in resultFeed.Entries )
                {
                    GDataBatchEntryData data = resultEntry.BatchData;
                    if (data.Status.Code != (int) HttpStatusCode.OK)
                    {
                        throw new GDataBatchRequestException(resultFeed);
                    }
                }

                // if we get here, everything is fine. So update the edit URIs in the original feed,
                // because those might have changed. 
                foreach (AtomEntry resultEntry in resultFeed.Entries )
                {
                    AtomEntry originalEntry = this.Entries.FindById(resultEntry.Id);
                    if (originalEntry == null)
                    {
                        throw new GDataBatchRequestException(resultFeed);
                    }
                    if (originalEntry != null)
                    {
                        originalEntry.EditUri = resultEntry.EditUri;
                    }
                }
               
            }
            this.Dirty = false; 
        }
        /////////////////////////////////////////////////////////////////////////////

    }
}
