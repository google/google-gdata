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
using System.Globalization;
using System.Text;
using Google.GData.Client;

namespace Google.GData.Contacts {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of FeedQuery, to create an Contacts query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// The ContactsQuery supports the following GData parameters:
    /// The Contacts Data API supports the following standard Google Data API query parameters:
    /// Name              Description
    /// alt               The type of feed to return, such as atom (the default), rss, or json.
    /// max-results       The maximum number of entries to return. If you want to receive all of
    ///                   the contacts, rather than only the default maximum, you can specify a very 
    ///                   large number for max-results.
    /// start-index       The 1-based index of the first result to be retrieved (for paging).
    /// updated-min       The lower bound on entry update dates.
    /// 
    /// For more information about the standard parameters, see the Google Data APIs protocol reference document.
    /// In addition to the standard query parameters, the Contacts Data API supports the following parameters:
    /// 
    /// Name              Description
    /// orderby           Sorting criterion. The only supported value is lastmodified.
    /// showdeleted       Include deleted contacts in the returned contacts feed. 
    ///                   Deleted contacts are shown as entries that contain nothing but an 
    ///                   atom:id element and a gd:deleted element. 
    ///                   (Google retains placeholders for deleted contacts for 30 days after 
    ///                   deletion; during that time, you can request the placeholders 
    ///                   using the showdeleted query parameter.) Valid values are true or false.
    /// sortorder         Sorting order direction. Can be either ascending or descending.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class ContactsQuery : FeedQuery
    {
        /// <summary>
        /// contacts base URI 
        /// </summary>
        public static string contactsBaseUri = "http://www.google.com/m8/feeds/contacts/";

        /// <summary>
        /// sortoder value for sorting by lastmodified
        /// </summary>
        public static string OrderByLastModified = "lastmodified";

        /// <summary>
        /// sortoder value for sorting ascending
        /// </summary>
        public static string SortOrderAscending = "ascending";

        /// <summary>
        /// sortoder value for sorting descending
        /// </summary>
        public static string SortOrderDescending = "ascending";

        private string orderBy; 
        private bool showDeleted;
        private string sortOrder;

        /// <summary>
        /// base constructor
        /// </summary>
        public ContactsQuery()
        : base()
        {
        }



        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public ContactsQuery(string queryUri)
        : base(queryUri)
        {
        }

        /// <summary>
        /// convienience method to create an URI based on a userID for a picasafeed
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>string</returns>
        public static string CreateContactsUri(string userID) 
        {
            return ContactsQuery.contactsBaseUri + Utilities.UriEncodeReserved(userID)+ "/base";
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string SortOder</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string SortOrder
        {
            get {return this.sortOrder;}
            set {this.sortOrder = value;}
        }
        // end of accessor public string SortOder
        //////////////////////////////////////////////////////////////////////
        /// <summary>indicates the thumbsize required</summary>
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string OrderBy
        {
            get {return this.orderBy;}
            set {this.orderBy = value;}
        }
        // end of accessor public Thumbsize

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public bool ShowDeleted</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public bool ShowDeleted
        {
            get {return this.showDeleted;}
            set {this.showDeleted = value;}
        }
        // end of accessor public bool ShowDeleted
   
#if WindowsCE || PocketPC
#else
        //////////////////////////////////////////////////////////////////////
        /// <summary>protected void ParseUri</summary> 
        /// <param name="targetUri">takes an incoming Uri string and parses all the properties out of it</param>
        /// <returns>throws a query exception when it finds something wrong with the input, otherwise returns a baseuri</returns>
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
                            case "orderby":
                                this.OrderBy = parameters[1];
                                break;
                            case "sortorder":
                                this.SortOrder = parameters[1];
                                break;
                            case "showdeleted":
                                if (String.Compare("true", parameters[1], false, CultureInfo.InvariantCulture) == 0)
                                {
                                    this.ShowDeleted = true;
                                }
                                break;
                        }
                    }
                }

        
            }
            return this.Uri;
        }
#endif

        //////////////////////////////////////////////////////////////////////
        /// <summary>Resets object state to default, as if newly created.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        protected override void Reset()
        {
            base.Reset();
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> string => the query part of the URI </returns>
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


            if (this.OrderBy != null && this.OrderBy.Length > 0)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "orderby={0}", Utilities.UriEncodeReserved(this.OrderBy));
                paramInsertion = '&';
            }

            if (this.SortOrder != null && this.SortOrder.Length > 0)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "sortorder={0}", Utilities.UriEncodeReserved(this.SortOrder));
                paramInsertion = '&';
            }

            if (this.ShowDeleted == true)
            {
                newPath.Append(paramInsertion);
                newPath.Append("showdeleted=true");
                paramInsertion = '&';
            }
            return newPath.ToString();
        }
    }
}
