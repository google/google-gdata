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
    //////////////////////////////////////////////////////////////////////
    public class DocumentsQuery : FeedQuery
    {

  
        /// <summary>
        /// picasa base URI 
        /// </summary>
        public static string documentsBaseUri = "http://docs.google.com/feeds/documents/private/full";

        /// <summary>
        /// base constructor
        /// </summary>
        public DocumentsQuery()
        : base()
        {
        }



        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public DocumentsQuery(string queryUri)
        : base(queryUri)
        {
        }
    }

}
