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
using Google.GData.AccessControl;
using Google.GData.Documents;
using System.Collections.Generic;

namespace Google.Documents
{

    public class Document : Entry
    {

        /// <summary>
        /// descripes the type of the document entry
        /// </summary>
        public enum DocumentType
        {
            Document,
            Spreadsheet,
            PDF,
            Presentation,
            Folder,
            Unknown
        }

        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new DocumentEntry();
            }
        }



        /// <summary>
        /// readonly accessor for the DocumentEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public  DocumentEntry DocumentEntry
        {
            get
            {
                EnsureInnerObject();
                return this.AtomEntry as DocumentEntry;
            }
        }

        /// <summary>
        /// the type of the document entry
        /// </summary>
        /// <returns></returns>
        public Document.DocumentType Type
        {
            get
            {
                EnsureInnerObject();
                if (this.DocumentEntry.IsDocument)
                {
                    return Document.DocumentType.Document;
                } 
                else if (this.DocumentEntry.IsPDF)
                {
                    return Document.DocumentType.PDF;
                }
                else if (this.DocumentEntry.IsSpreadsheet)
                {
                    return Document.DocumentType.Spreadsheet;
                }
                else if (this.DocumentEntry.IsFolder)
                {
                    return Document.DocumentType.Folder;
                }
                else if (this.DocumentEntry.IsPresentation)
                {
                    return Document.DocumentType.Presentation;
                }
                return Document.DocumentType.Unknown;
            }
            set
            {
                EnsureInnerObject();
                switch (value)
                {
                    case Document.DocumentType.Document:
                        this.DocumentEntry.IsDocument = true;
                        break;
                    case Document.DocumentType.Folder:
                        this.DocumentEntry.IsFolder = true;
                        break;
                    case Document.DocumentType.PDF:
                        this.DocumentEntry.IsPDF = true;
                        break;
                    case Document.DocumentType.Presentation:
                        this.DocumentEntry.IsPresentation = true;
                        break;
                    case Document.DocumentType.Spreadsheet:
                        this.DocumentEntry.IsSpreadsheet = true;
                        break;
                    case Document.DocumentType.Unknown:
                        throw new ArgumentException("Type.Unknown is not allowed to be set");
                }
            }
        }

        /// <summary>
        /// returns the href values of the parent link releationships
        /// can be used to retrieve the parent folder
        /// </summary>
        /// <returns></returns>
        public List<string> ParentFolders
        {
            get
            {
                EnsureInnerObject();
                List<string> strings = new List<string>();
                foreach (AtomLink l in this.DocumentEntry.ParentFolders)
                {
                    strings.Add(l.HRef.ToString());
                }
                return strings;
            }
        }
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// The Google Documents List Data API allows client applications 
    /// to view and update documents (spreadsheets and word processor) 
    /// using a Google Data API feed. Your client application can request
    /// a list of a user's documents, query the content of a 
    /// user's documents, and upload new documents.
    /// </summary>
    ///  <example>
    ///         The following code illustrates a possible use of   
    ///          the <c>DocumentsRequest</c> object:  
    ///          <code>    
    ///            RequestSettings settings = new RequestSettings("yourApp");
    ///            settings.PageSize = 50; 
    ///            settings.AutoPaging = true;
    ///            DocumentsRequest c = new DocumentsRequest(settings);
    ///            Feed<Dcouments> feed = c.GetDocuments();
    ///     
    ///         foreach (Document d in feed.Entries)
    ///         {
    ///              Console.WriteLine(d.Title);
    ///         }
    ///  </code>
    ///  </example>
    //////////////////////////////////////////////////////////////////////
    public class DocumentsRequest : FeedRequest<DocumentsService>
    {

        private string baseUri = DocumentsListQuery.documentsBaseUri;
        /// <summary>
        /// default constructor for a DocumentsRequest
        /// </summary>
        /// <param name="settings"></param>
        public DocumentsRequest(RequestSettings settings) : base(settings)
        {
            this.Service = new DocumentsService(settings.Application);
            PrepareService();
        }

        /// <summary>
        /// the base string to use for queries. Defaults to 
        /// DocumentsListQuery.documentsBaseUri
        /// </summary>
        /// <returns></returns>
        public string BaseUri
        {
            get
            {
                return this.baseUri;
            }
            set
            {
                this.BaseUri = value;
            }
        }

        /// <summary>
        /// returns a Feed of all documents and folders for the authorized user
        /// </summary>
        /// <returns>a feed of everyting</returns>
        public Feed<Document> GetEverything()
        {
            DocumentsListQuery q = PrepareQuery<DocumentsListQuery>(this.BaseUri);
            q.ShowFolders = true; 
            return PrepareFeed<Document>(q); 
        }

        /// <summary>
        /// returns a Feed of all documents for the authorized user
        /// </summary>
        /// <returns>a feed of Documents</returns>
        public Feed<Document> GetDocuments()
        {
            TextDocumentQuery q = PrepareQuery<TextDocumentQuery>(this.BaseUri);
            return PrepareFeed<Document>(q); 
        }

        /// <summary>
        /// returns a Feed of all presentations for the authorized user
        /// </summary>
        /// <returns>a feed of Documents</returns>
        public Feed<Document> GetPresentations()
        {
            PresentationsQuery q = PrepareQuery<PresentationsQuery>(this.BaseUri);
            return PrepareFeed<Document>(q); 
        }

        /// <summary>
        /// returns a Feed of all spreadsheets for the authorized user
        /// </summary>
        /// <returns>a feed of Documents</returns>
        public Feed<Document> GetSpreadsheets()
        {
            SpreadsheetQuery q = PrepareQuery<SpreadsheetQuery>(this.BaseUri);
            return PrepareFeed<Document>(q); 
        }

        /// <summary>
        /// returns a Feed of all pdf files for the authorized user
        /// </summary>
        /// <returns>a feed of Documents</returns>
        public Feed<Document> GetPDFs()
        {
            PDFsQuery q = PrepareQuery<PDFsQuery>(this.BaseUri);
            return PrepareFeed<Document>(q); 
        }

        /// <summary>
        /// returns a Feed of all folders for the authorized user
        /// </summary>
        /// <returns>a feed of Documents</returns>
        public Feed<Document> GetFolders()
        {
            FolderQuery q = PrepareQuery<FolderQuery>(this.BaseUri);
            return PrepareFeed<Document>(q); 
        }

        public Feed<Document> GetFolderContent(Document folder)
        {
            if (folder.Type != Document.DocumentType.Folder)
            {
                throw new ArgumentException("The parameter folder is not a folder");
            }

            
            return null; 
        }



        /// <summary>
        /// this will create an empty document or folder, pending
        /// the content of the newDocument parameter
        /// </summary>
        /// <param name="newDocument"></param>
        /// <returns>the created document from the server</returns>
        public Document CreateDocument(Document newDocument)
        {
            return Insert(new Uri(DocumentsListQuery.documentsBaseUri), newDocument);
        }

        /// <summary>
        /// moves a document or a folder into a folder
        /// </summary>
        /// <param name="parent">this has to be a folder</param>
        /// <param name="child">can be a folder or a document</param>
        /// <returns></returns>
        public Document MoveDocumentTo(Document parent, Document child)
        {
            if (parent == null || child == null)
            {
                throw new ArgumentNullException("parent or child can not be null");
            }
            if (parent.AtomEntry.Content == null || parent.AtomEntry.Content.AbsoluteUri == null)
            {
                throw new ArgumentException("parent has no content uri");
            }
            if (parent.Type != Document.DocumentType.Folder)
            {
                throw new ArgumentException("wrong parent type");
            }

            Document payload = new Document();
            payload.DocumentEntry.Id = new AtomId(child.Id);
            payload.Type = child.Type;

            // to do that, we just need to post the CHILD 
            // against the URI of the parent
            return Insert(new Uri(parent.AtomEntry.Content.AbsoluteUri), payload);

        }
    }
}
