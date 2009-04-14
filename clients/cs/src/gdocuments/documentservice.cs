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
       
        /// <summary>The Calendar service's name</summary> 
        public const string GDocumentsService = "writely";

        /// <summary>
        /// A Hashtable that expresses the allowed content types.
        /// </summary>
        public static Hashtable GDocumentsAllowedTypes;

        /// <summary>
        /// Static constructor used to initialize GDocumentsAllowedTypes.
        /// </summary>
        static DocumentsService()
        {
            GDocumentsAllowedTypes = new Hashtable();
            GDocumentsAllowedTypes.Add("CSV", "text/csv");
            GDocumentsAllowedTypes.Add("TAB", "text/tab-separated-values");
            GDocumentsAllowedTypes.Add("TSV", "text/tab-separated-values");
            GDocumentsAllowedTypes.Add("TXT", "text/plain");
            GDocumentsAllowedTypes.Add("HTML", "text/html");
            GDocumentsAllowedTypes.Add("HTM", "text/html");
            GDocumentsAllowedTypes.Add("DOC", "application/msword");
            GDocumentsAllowedTypes.Add("DOCX", "application/msword");
            GDocumentsAllowedTypes.Add("ODS", "application/x-vnd.oasis.opendocument.spreadsheet");
            GDocumentsAllowedTypes.Add("ODT", "application/vnd.oasis.opendocument.text");
            GDocumentsAllowedTypes.Add("RTF", "application/rtf");
            GDocumentsAllowedTypes.Add("SXW", "application/vnd.sun.xml.writer");
            GDocumentsAllowedTypes.Add("XLSX", "application/vnd.ms-excel");
            GDocumentsAllowedTypes.Add("XLS", "application/vnd.ms-excel");
            GDocumentsAllowedTypes.Add("PPT", "application/vnd.ms-powerpoint");
            GDocumentsAllowedTypes.Add("PPS", "application/vnd.ms-powerpoint");
            GDocumentsAllowedTypes.Add("PDF", "application/pdf");
        }

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public DocumentsService(string applicationName) : base(GDocumentsService, applicationName)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed); 
        }
   
        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public DocumentsFeed Query(DocumentsListQuery feedQuery) 
        {
            return base.Query(feedQuery) as DocumentsFeed;
        }


        /// <summary>
        /// Simple method to upload a document, presentation, or spreadsheet
        /// based upon the file extension.
        /// </summary>
        /// <param name="fileName">The full path to the file.</param>
        /// <param name="documentName">The desired name of the document on the server.</param>
        /// <returns>A DocumentEntry describing the created document.</returns>
        public DocumentEntry UploadDocument(string fileName, string documentName)
        {
            DocumentEntry entry = null;

            FileInfo fileInfo = new FileInfo(fileName);
            FileStream stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            try
            {
                Uri postUri = new Uri(DocumentsListQuery.documentsBaseUri);
    
                if (documentName == null)
                {
                    documentName = fileInfo.Name;
                }
    
                //convert the extension to caps and strip the "." off the front
                string ext = fileInfo.Extension.ToUpper().Substring(1);
    
                String contentType = (String) GDocumentsAllowedTypes[ext];
    
                if (contentType == null)
                {
                    throw new ArgumentException("File extension '"+ext+"' is not recognized as valid.");
                }
    
                entry = this.Insert(postUri, stream, contentType, documentName) as DocumentEntry;
            }
            finally
            {
                stream.Close();
            }

            return entry;
        }


        /// <summary>
        /// by default all services now use version 1 for the protocol.
        /// this needs to be overridden by a service to specify otherwise. 
        /// YouTube uses version 2
        /// </summary>
        /// <returns></returns>
        protected override void InitVersionInformation()
        {
             this.ProtocolMajor = VersionDefaults.VersionTwo;
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
