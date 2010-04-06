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
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using Google.GData.Client;
using Google.GData.Extensions;


namespace Google.GData.Spreadsheets 
{

    /// <summary>
    /// The SpreadsheetsService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
    /// Google Spreadsheets data API.
    /// </summary>
    public class SpreadsheetsService : Service 
    {
        /// <summary>The Spreadsheets service's name</summary>
        public const string GSpreadsheetsService = "wise";



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public SpreadsheetsService(string applicationName)
        : base(GSpreadsheetsService, applicationName)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed); 
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery touse</param>
        /// <returns>the retrieved CellFeed</returns>
        public CellFeed Query(CellQuery feedQuery)
        {
            return base.Query(feedQuery) as CellFeed;
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery touse</param>
        /// <returns>the retrieved ListFeed</returns>
        public ListFeed Query(ListQuery feedQuery)
        {
            return base.Query(feedQuery) as ListFeed;
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved SpreadsheetFeed</returns>
        public SpreadsheetFeed Query(SpreadsheetQuery feedQuery)
        {
            return base.Query(feedQuery) as SpreadsheetFeed;
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved WorksheetFeed</returns>
        public WorksheetFeed Query(WorksheetQuery feedQuery)
        {
            return base.Query(feedQuery) as WorksheetFeed;
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>eventchaining. We catch this by from the base service, which 
        /// would not by default create an atomFeed</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Spreadsheet Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }
            if (e.Uri.AbsoluteUri.IndexOf("/" + 
                  GDataSpreadsheetsNameTable.FeedCell + "/") != -1)
            {
                e.Feed = new CellFeed(e.Uri, e.Service);
            } 
            else if (e.Uri.AbsoluteUri.IndexOf("/" + 
                  GDataSpreadsheetsNameTable.FeedList + "/") != -1)
            {
                e.Feed = new ListFeed(e.Uri, e.Service);
            }
            else if(e.Uri.AbsoluteUri.IndexOf("/" + 
                  GDataSpreadsheetsNameTable.FeedSpreadsheet + "/") != -1)
            {
                e.Feed = new SpreadsheetFeed(e.Uri, e.Service);
            }
            else if(e.Uri.AbsoluteUri.IndexOf("/" + 
                  GDataSpreadsheetsNameTable.FeedWorksheet + "/") != -1)
            {
                e.Feed = new WorksheetFeed(e.Uri, e.Service);
            }
        }
        /////////////////////////////////////////////////////////////////////////////
    }
}
