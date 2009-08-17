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
using System.Text;
using System.Globalization;
using System.Diagnostics;
using Google.GData.Client;

namespace Google.GData.Documents {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of FeedQuery, to create an Documents query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// 
    /// Documents List supports the following standard GData query parameters: 
    ///     alt, author, q, start-index, max-results, updated-min, updated-max, /category
    /// For more information about the standard parameters, see the GData protocol reference document.
    /// In addition to the standard GData query parameters, the Documents List data API uses the following parameters.
    /// Parameter	Meaning	                                                
    /// title	    Specifies the search terms for the title of a document.	
    ///             This parameter used without title-exact will only submit partial queries, not exact queries.
    /// 
    /// title-exact	Specifies whether the title query should be taken as an exact string.	
    ///             Meaningless without title. Possible values are true and false.
    /// 
    /// The Documents List data API supports the following categories.
    ///     Category: Document Type	
    ///             Scheme: http://schemas.google.com/g/2005#kind	
    ///             Term: http://schemas.google.com/docs/2007#type	
    ///             Label: type	    
    ///             All documents of the corresponding type in the requesting users document list. 
    ///             Type is currently one of (document|spreadsheet|presentation)
    ///     Category: Starred Status
    ///         	Scheme: http://schemas.google.com/g/2005/labels	
    ///             Term: starred	                                    
    ///             Label: starred	    
    ///             All documents that have been starred by the requesting user
    ///     Category: Containing Folders	
    ///             Scheme: http://schemas.google.com/docs/2007/folders/user-email	
    ///             Term: folder-id	
    ///             Label: folder-name	
    ///             All documents inside the given folder for the requesting user
    ///  </summary>
    //////////////////////////////////////////////////////////////////////
    public class DocumentsListQuery : FeedQuery
    {

  
        /// <summary>
        /// document feed base URI 
        /// </summary>
        public static string documentsBaseUri = "http://docs.google.com/feeds/documents/private/full";

        /// <summary>
        /// document feed base URI with ACLs
        /// </summary>
        public static string documentsAclUri = "http://docs.google.com/feeds/documents/private/expandAcl";



        /// <summary>
        /// folder base uri
        /// </summary>
        /// <returns></returns>
        public static string folderBaseUri = "http://docs.google.com/feeds/folders/private/full/folder%3A";

        private static AtomCategory ATOMCATEGORY_DOCUMENTS = new AtomCategory("document");
        /// <summary>
        /// predefined query category for documents
        /// </summary>
        public static QueryCategory DOCUMENTS = new QueryCategory(ATOMCATEGORY_DOCUMENTS);

        private static AtomCategory ATOMCATEGORY_SPREADSHEETS = new AtomCategory("spreadsheet");
        /// <summary>
        /// predefined query category for spreadsheets
        /// </summary>
        public static QueryCategory SPREADSHEETS = new QueryCategory(ATOMCATEGORY_SPREADSHEETS);

        private static AtomCategory ATOMCATEGORY_PRESENTATIONS = new AtomCategory("presentation");
        /// <summary>
        /// predefined query category for presentations
        /// </summary>
        public static QueryCategory PRESENTATIONS = new QueryCategory(ATOMCATEGORY_PRESENTATIONS);

        private static AtomCategory ATOMCATEGORY_PDFS = new AtomCategory("pdf");
        /// <summary>
        /// predefined query category for presentations
        /// </summary>
        public static QueryCategory PDFS = new QueryCategory(ATOMCATEGORY_PDFS);
        
        
        /// <summary>
        /// predefined query category for starred documents
        /// </summary>
        public static AtomCategory ATOMCATEGORY_STARRED = new AtomCategory("starred");
        /// <summary>
        /// predefined query category for starred documents
        /// </summary>
        public static QueryCategory STARRED = new QueryCategory(ATOMCATEGORY_STARRED);


        /// <summary>
        /// predefined query category for starred documents
        /// </summary>
        public static AtomCategory ATOMCATEGORY_FOLDER = new AtomCategory("folder");
        /// <summary>
        /// predefined query category for starred documents
        /// </summary>
        public static QueryCategory FOLDER = new QueryCategory(ATOMCATEGORY_FOLDER);

        //Local variable to hold the contents of a title query
        private string title;
        //Local variable to hold if the title query we are doing should be exact.
        private bool titleExact;

        /// <summary>
        /// base constructor
        /// </summary>
        public DocumentsListQuery()
        : base(documentsBaseUri)
        {
            this.CategoryQueriesAsParameter = true;
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public DocumentsListQuery(string queryUri)
        : base(queryUri)
        {
            this.CategoryQueriesAsParameter = true;
        }

        /// <summary>
        /// Restricts the results to only starred documents
        /// </summary>
        [CLSCompliant(false)]
        public bool Starred
        {
            get
            {
                return this.Categories.Contains(DocumentsListQuery.STARRED);
            }
            set
            {
                if (value)
                {
                    this.Categories.Add(DocumentsListQuery.STARRED);
                }
                else
                {
                    this.Categories.Remove(DocumentsListQuery.STARRED);
                }
            }
        }

        private bool showFolders;
        /// <summary>
        /// shows folders if true in the result
        /// </summary>
        [CLSCompliant(false)]
        public bool ShowFolders
        {
            get
            {
                return this.showFolders;
            }
            set
            {
                this.showFolders = value;
            }
        }

        /// <summary>
        /// Restricts the results to only documents with titles matching a string.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Restricts the results to only documents matching a string provided
        /// by the Title property exactly. (No partial matches.)
        /// </summary>
        public bool TitleExact
        {
            get
            {
                return this.titleExact;
            }
            set
            {
                this.titleExact = value;
            }
        }


        /// <summary>
        /// tries to parse a document id iri and return just the 
        /// ID portion of it
        /// </summary>
        /// <param name="documentUri">the document id link as a string</param>
        /// <returns>the document id or null if failed</returns>
        public static string DocumentId(string documentUri)
        {
            const string token="%3A"; 
            if (documentUri == null)

            {
                throw new ArgumentNullException("documentUri");
            }
            int pos = documentUri.LastIndexOf(token);
            if (pos > 0)
            {
                pos += 3;
                if (pos < documentUri.Length)
                {
                    return documentUri.Substring(pos);
                }
            }
            return null;
        }

#if WindowsCE || PocketPC
#else
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses custom properties out of the incoming URI</summary> 
        /// <param name="targetUri">A URI representing a query on a feed</param>
        /// <returns>returns the base uri</returns>
        //////////////////////////////////////////////////////////////////////
        protected override Uri ParseUri(Uri targetUri)
        {
            base.ParseUri(targetUri);
            if (targetUri != null)
            {
                char[] deli = { '?', '&' };

                TokenCollection tokens = new TokenCollection(targetUri.Query, deli);
                foreach (String token in tokens)
                {
                    if (token.Length > 0)
                    {
                        char[] otherDeli = { '=' };
                        String[] parameters = token.Split(otherDeli, 2);
                        switch (parameters[0])
                        {
                            case "title-exact":
                                this.TitleExact = bool.Parse(parameters[1]);
                                break;
                            case "title":
                                this.Title = parameters[1];
                                break;
                            case "showfolders":
                                this.ShowFolders = bool.Parse(parameters[1]);
                                break;
                        }
                    }
                }
            }
            return this.Uri;
        }
#endif
        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> A string representing the query part of the URI.</returns>
        //////////////////////////////////////////////////////////////////////
        protected override string CalculateQuery(string basePath)
        {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path); 

            paramInsertion = AppendQueryPart(this.Title, "title", paramInsertion, newPath);            

            if (this.TitleExact == true)
            {
                paramInsertion = AppendQueryPart("true", "title-exact", paramInsertion, newPath);            
            }
            if (this.ShowFolders == true)
            {
                paramInsertion = AppendQueryPart("true", "showfolders", paramInsertion, newPath);            
            }
            return newPath.ToString();
        }
    }



    /// <summary>
    /// a subclass setup to just retrieve all word processor documents
    /// </summary>
    public class TextDocumentQuery : DocumentsListQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public TextDocumentQuery()
        : base()
        {
            this.Categories.Add(DocumentsListQuery.DOCUMENTS);
        }
   }


    /// <summary>
    /// a subclass setup to just retrieve all spreadsheets
    /// </summary>
    public class SpreadsheetQuery : DocumentsListQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public SpreadsheetQuery()
        : base()
        {
            this.Categories.Add(DocumentsListQuery.SPREADSHEETS);
        }
   }

     /// <summary>
    /// a subclass setup to just retrieve all presentations
    /// </summary>
    public class PresentationsQuery : DocumentsListQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public PresentationsQuery()
        : base()
        {
            this.Categories.Add(DocumentsListQuery.PRESENTATIONS);
        }
   }

     /// <summary>
    /// a subclass setup to just retrieve all PDFs
    /// </summary>
    public class PDFsQuery : DocumentsListQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public PDFsQuery()
        : base()
        {
            this.Categories.Add(DocumentsListQuery.PDFS);
        }
   }    


    /// <summary>
    /// a subclass setup to just retrieve all Folders
    /// </summary>
    public class FolderQuery : DocumentsListQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public FolderQuery()
        : base()
        {
            this.Categories.Add(DocumentsListQuery.FOLDER);
            this.ShowFolders = true;
        }
   }    
    
}
