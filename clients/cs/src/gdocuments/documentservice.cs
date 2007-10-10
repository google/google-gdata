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


namespace Google.GData.Documents {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// The Google Documents List data API allows client applications to upload 
    /// documents to Google Documents and list them in the form of Google data 
    /// API ("GData") feeds. Your client application can request a list of a user's 
    /// documents, and query the content in an existing document.
    /// Here are some of the things you can do with the Documents List data API:
    ///     Upload the word processing documents and spreadsheets on
    ///         your computer to allow you to back them up or 
    ///         collaborate online when editing.
    ///     Find all of your documents that contain specific keywords.
    ///     Get a list of spreadsheets which can be accessed through the Google Spreadsheets data API. 
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class DocumentsService : Service
    {
       
        /// <summary>This service's User-Agent string</summary> 
        public const string GDocumentsAgent = "GDocuments-CS/1.0.0";
        /// <summary>The Calendar service's name</summary> 
        public const string GDocumentsService = "writely";

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public DocumentsService(string applicationName) : base(GDocumentsService, applicationName, GDocumentsAgent)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed); 
        }
   
        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public DocumentsFeed Query(DocumentsQuery feedQuery) 
        {
            return base.Query(feedQuery) as DocumentsFeed;
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
            Tracing.TraceMsg("Created new Documents Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }
            e.Feed = new DocumentsFeed(e.Uri, e.Service);
        }
        /////////////////////////////////////////////////////////////////////////////
    }
}
