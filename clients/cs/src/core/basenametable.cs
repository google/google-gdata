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
#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.IO; 

#endregion

//////////////////////////////////////////////////////////////////////
// <summary>basenametable, holds common names for atom&rss parsing</summary> 
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>BaseNameTable. An initialized nametable for faster XML processing
    /// parses:  
    ///     *  opensearch:totalResults - the total number of search results available (not necessarily all present in the feed).
    ///     *  opensearch:startIndex - the 1-based index of the first result.
    ///     *  opensearch:itemsPerPage - the maximum number of items that appear on one page. This allows clients to generate direct links to any set of subsequent pages.
    ///     *  gData:processed
    ///  </summary>
    //////////////////////////////////////////////////////////////////////
    public class BaseNameTable
    {
        /// <summary>the nametable itself, based on XML core</summary> 
        private NameTable atomNameTable; 

        /// <summary>opensearch:totalResults</summary> 
        private object totalResults;
        /// <summary>opensearch:startIndex</summary> 
        private object startIndex;
        /// <summary>opensearch:itemsPerPage</summary> 
        private object itemsPerPage;
         /// <summary>xml base</summary> 
        private object baseUri;
        /// <summary>xml language</summary> 
        private object language;

        /// <summary>static namespace string declaration</summary> 
        public const string NSOpenSearchRss = "http://a9.com/-/spec/opensearchrss/1.0/";
        /// <summary>static namespace string declaration</summary> 
        public const string NSAtom = "http://www.w3.org/2005/Atom";
        /// <summary>xml namespace</summary> 
        public const string NSXml = "http://www.w3.org/XML/1998/namespace";
        /// <summary>GD namespace</summary> 
        public const string gNamespace = "http://schemas.google.com/g/2005";
        /// <summary>GD namespace prefix</summary> 
        public const string gNamespacePrefix = gNamespace+ "#";
        /// <summary>the post definiton in the link collection</summary> 
        public const string ServicePost = gNamespacePrefix + "post";
        /// <summary>the feed definition in the link collection</summary> 
        public const string ServiceFeed = gNamespacePrefix + "feed";
        /// <summary>GData Kind Scheme</summary> 
        public const string gKind = gNamespacePrefix + "kind";
        /// <summary>the edit definition in the link collection</summary> 
        public const string ServiceEdit = "edit";
        /// <summary>the next chunk URI in the link collection</summary> 
        public const string ServiceNext = "next";
        /// <summary>the previous chunk URI in the link collection</summary> 
        public const string ServicePrev = "previous";
        /// <summary>the self URI in the link collection</summary> 
        public const string ServiceSelf = "self";
        /// <summary>the alternate URI in the link collection</summary> 
        public const string ServiceAlternate = "alternate";

        /// <summary>prefix for atom if writing</summary> 
        public const string AtomPrefix = "atom"; 

        /// <summary>prefix for gdata if writing</summary> 
        public const string gDataPrefix = "gd"; 

        //////////////////////////////////////////////////////////////////////
        /// <summary>initializes the name table for use with atom parsing. This is the
        /// only place where strings are defined for parsing</summary> 
        //////////////////////////////////////////////////////////////////////
        public virtual void InitAtomParserNameTable()
        {
            // create the nametable object
            Tracing.TraceCall("Initializing basenametable support"); 
            this.atomNameTable = new NameTable(); 
            // <summary>add the keywords for the Feed
            this.totalResults = this.atomNameTable.Add("totalResults");
            this.startIndex   = this.atomNameTable.Add("startIndex");
            this.itemsPerPage = this.atomNameTable.Add("itemsPerPage");
            this.baseUri      = this.atomNameTable.Add("base");  
            this.language     = this.atomNameTable.Add("lang");  
        }
        /////////////////////////////////////////////////////////////////////////////

        #region Read only accessors 8/10/2005

        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for atomNameTable</summary> 
        //////////////////////////////////////////////////////////////////////
        internal NameTable Nametable
        {
            get {return this.atomNameTable;}
        }
        /////////////////////////////////////////////////////////////////////////////
        

        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for totalResults</summary> 
        //////////////////////////////////////////////////////////////////////
        public object TotalResults
        {
            get {return this.totalResults;}
        }
        /////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for startIndex</summary> 
        //////////////////////////////////////////////////////////////////////
        public object StartIndex
        {
            get {return this.startIndex;}
        }
        /////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for itemsPerPage</summary> 
        //////////////////////////////////////////////////////////////////////
        public object ItemsPerPage
        {
            get {return this.itemsPerPage;}
        }
        /////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for parameter</summary> 
        //////////////////////////////////////////////////////////////////////
        static public string Parameter
        {
            get {return "parameter";}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for baseUri</summary> 
        //////////////////////////////////////////////////////////////////////
        public object Base
        {
            get {return this.baseUri;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for language</summary> 
        //////////////////////////////////////////////////////////////////////
        public object Language
        {
            get {return this.language;}
        }
        /////////////////////////////////////////////////////////////////////////////
        

        #endregion end of Read only accessors

    }
    /////////////////////////////////////////////////////////////////////////////

}
/////////////////////////////////////////////////////////////////////////////

 
