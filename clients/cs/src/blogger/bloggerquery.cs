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
using System.Text;
using System.Globalization;
using Google.GData.Client;

namespace Google.GData.Blogger
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of FeedQuery, to create a Blogger query URI.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class BloggerQuery : FeedQuery
    {
        /// <summary>
        /// constant string for the order by updated query
        /// </summary>
        public const string OrderByUpdated = "updated";
        /// <summary>
        /// constant string for the order by published query
        /// </summary>
        public const string OrderByPublished = "published";

        private string orderBy;
        /// <summary>
        /// default constructor, does nothing 
        /// </summary>
        public BloggerQuery() : base()
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public BloggerQuery(string queryUri)
        : base(queryUri)
        {
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>You can add orderby=published or orderby=updated to a GData query 
        /// to get the posts sorted in that order. 
        /// Some notes: 
        /// - updated is the default 
        /// - This has no effect on comments feeds, whose updated and published 
        ///     dates are the same 
        /// - Pagination in the by-updated feed is limited to the most recently 
        ///     published 500 posts. </summary>
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string OrderBy
        {
            get {return this.orderBy;}
            set {this.orderBy = value;}
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

                string source = HttpUtility.UrlDecode(targetUri.Query);
                TokenCollection tokens = new TokenCollection(source, deli);
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
            return newPath.ToString();
        }
    }
}
