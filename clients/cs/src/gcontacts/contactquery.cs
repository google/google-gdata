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
    /// group	          Constrains the results to only the contacts belonging to the group specified. 
    ///                   Value of this parameter specifies group ID (see also: gContact:groupMembershipInfo).
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class GroupsQuery : FeedQuery
    {
         /// <summary>
        /// contacts group base URI 
        /// </summary>
        public const string groupsBaseUri = "http://www.google.com/m8/feeds/groups/";

        /// <summary>
        /// sortoder value for sorting by lastmodified
        /// </summary>
        public const string OrderByLastModified = "lastmodified";

        /// <summary>
        /// sortoder value for sorting ascending
        /// </summary>
        public const string SortOrderAscending = "ascending";

        /// <summary>
        /// sortoder value for sorting descending
        /// </summary>
        public const string SortOrderDescending = "ascending";

        /// <summary>
        /// base projection value
        /// </summary>
        public const string baseProjection = "base";
        /// <summary>
        /// thin projection value
        /// </summary>
        public const string thinProjection = "thin";        
        /// <summary>
        /// property-key projection value
        /// </summary>
        public const string propertyProjection = "property-";
        /// <summary>
        /// full projection value
        /// </summary>
        public const string fullProjection = "full";

        private string orderBy; 
        private bool showDeleted;
        private string sortOrder;

        /// <summary>
        /// base constructor
        /// </summary>
        public GroupsQuery()
        : base()
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public GroupsQuery(string queryUri)
        : base(queryUri)
        {
        }


        /// <summary>
        /// convienience method to create an URI based on a userID for a groups feed
        /// this returns a FULL projection by default
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>string</returns>
        public static string CreateGroupsUri(string userID) 
        {
            return CreateGroupsUri(userID, ContactsQuery.fullProjection);
        }

        /// <summary>
        /// convienience method to create an URI based on a userID for a groups feed
        /// </summary>
        /// <param name="userID">if the parameter is NULL, uses the default user</param>
        /// <param name="projection">the projection to use</param>
        /// <returns>string</returns>
        public static string CreateGroupsUri(string userID, string projection) 
        {
            return ContactsQuery.groupsBaseUri + ContactsQuery.UserString(userID) + projection;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Sorting order direction. Can be either ascending or descending</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string SortOrder
        {
            get {return this.sortOrder;}
            set {this.sortOrder = value;}
        }
        // end of accessor public string SortOder


        //////////////////////////////////////////////////////////////////////
        /// <summary>Sorting criterion. The only supported value is lastmodified</summary>
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string OrderBy
        {
            get {return this.orderBy;}
            set {this.orderBy = value;}
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Include deleted contacts in the returned contacts feed. 
        /// Deleted contacts are shown as entries that contain nothing but
        ///  an atom:id element and a gd:deleted element. (Google retains placeholders 
        /// for deleted contacts for 30 days after deletion; during that time, 
        /// you can request the placeholders using the showdeleted query
        ///  parameter.) Valid values are true or false.</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public bool ShowDeleted
        {
            get {return this.showDeleted;}
            set {this.showDeleted = value;}
        }
        // end of accessor public bool ShowDeleted

   

        /// <summary>
        /// helper to create the userstring for a query
        /// </summary>
        /// <param name="user">the user to encode, or NULL if default</param>
        /// <returns></returns>
        protected static string UserString(string user)
        {
            if (user == null)
            {
                return "default/";
            }
            return Utilities.UriEncodeReserved(user)+ "/"; 
        }
   
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
        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> string => the query part of the URI </returns>
        //////////////////////////////////////////////////////////////////////
        protected override string CalculateQuery(string basePath)
        {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path); 

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
            if (this.ShowDeleted)
            {
                newPath.Append(paramInsertion);
                newPath.Append("showdeleted=true");
                paramInsertion = '&';
            }
            return newPath.ToString();
        }
    }



    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of GroupsQuery, to create an Contacts query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// The ContactsQuery supports the following GData parameters:
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
    /// group	          Constrains the results to only the contacts belonging to the group specified. 
    ///                   Value of this parameter specifies group ID (see also: gContact:groupMembershipInfo).
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class ContactsQuery : GroupsQuery
    {
        /// <summary>
        /// contacts base URI 
        /// </summary>
        public const string contactsBaseUri = "http://www.google.com/m8/feeds/contacts/";

        private string group;

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
        /// convienience method to create an URI based on a userID for a contacts feed
        /// this returns a FULL projection by default
        /// </summary>
        /// <param name="userID">if the parameter is NULL, uses the default user</param>
        /// <returns>string</returns>
        public static string CreateContactsUri(string userID) 
        {
            return CreateContactsUri(userID, ContactsQuery.fullProjection);
        }

        /// <summary>
        /// convienience method to create an URI based on a userID for a contacts feed
        /// this returns a FULL projection by default
        /// </summary>
        /// <param name="userID">if the parameter is NULL, uses the default user</param>
        /// <param name="projection">the projection to use</param>
        /// <returns>string</returns>
        public static string CreateContactsUri(string userID, string projection) 
        {
            return ContactsQuery.contactsBaseUri + UserString(userID) + projection;
        }


        /////////////////////////////////////////////////////////////////////
        /// <summary>Constrains the results to only the contacts belonging to the 
        /// group specified. Value of this parameter specifies group ID</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Group
        {
            get {return this.group;}
            set {this.group = value;}
        }
        // end of accessor public string SortOder
   

   
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
                            case "group":
                                this.Group = parameters[1];
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
        /// <returns> string => the query part of the URI </returns>
        //////////////////////////////////////////////////////////////////////
        protected override string CalculateQuery(string basePath)
        {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path); 

            if (this.Group != null && this.Group.Length > 0)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "group={0}", Utilities.UriEncodeReserved(this.Group));
                paramInsertion = '&';
            }
            return newPath.ToString();
        }
    }
}

