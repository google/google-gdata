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

        public static AtomCategory ATOMCATEGORY_STARRED = new AtomCategory("starred");
        /// <summary>
        /// predefined query category for starred documents
        /// </summary>
        public static QueryCategory STARRED = new QueryCategory(ATOMCATEGORY_STARRED);

        //Local variable to hold the contents of a title query
        public string title;
        //Local variable to hold if the title query we are doing should be exact.
        public bool titleExact;

        /// <summary>
        /// base constructor
        /// </summary>
        public DocumentsListQuery()
        : base(documentsBaseUri)
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public DocumentsListQuery(string queryUri)
        : base(queryUri)
        {
        }

        /// <summary>
        /// Restricts the results to only starred documents
        /// </summary>
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
                        }
                    }
                }
            }
            return this.Uri;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> A string representing the query part of the URI.</returns>
        //////////////////////////////////////////////////////////////////////
        protected override string CalculateQuery()
        {
            string path = base.CalculateQuery();
            StringBuilder newPath = new StringBuilder(path, 2048);

            char paramInsertion;

            if (path.IndexOf('?') == -1)
            {
                paramInsertion = '?';
            }
            else
            {
                paramInsertion = '&';
            }

            if (Utilities.IsPersistable(this.Title))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "title={0}", Utilities.UriEncodeReserved(this.Title));
                paramInsertion = '&';
            }
            if (this.TitleExact == true)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "title-exact=true");
                paramInsertion = '&';
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

}
