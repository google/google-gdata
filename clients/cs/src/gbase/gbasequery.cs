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
using System.Web;
using Google.GData.Client;

namespace Google.GData.GoogleBase
{

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Creates a Google Base query Uri.
    ///
    /// To create a query, first get the Uri of the feed you want
    /// from <see cref="GBaseUriFactory"/> and then pass it to the
    /// constructor.</summary>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseQuery : FeedQuery
    {
        private const string BqParameter = "bq";
        private const string MaxValuesParameter = "max-values";

        private string bq;
        private int maxValues = -1;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>max-values parameter, which sets the maximum number
        /// of value examples returned by the attributes feed (0 by default)</summary>
        ///////////////////////////////////////////////////////////////////////
        public int MaxValues
        {
            get
            {
                return maxValues;
            }
            set
            {
                maxValues = value;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>The <c>gb</c> query string.</summary>
        ///////////////////////////////////////////////////////////////////////
        public string GoogleBaseQuery
        {
            get
            {
                return bq;
            }
            set
            {
                bq = value;
            }
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a query for the given feed.</summary>
        /// <param name="feed">a feed that must have its Feed link set</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseQuery(GBaseFeed feed)
                : this(feed.Feed)
        {
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a query for the given feed Uri.</summary>
        /// <param name="uri">feed uri, usually created by
        /// <see cref="GBaseUriFactory"/></param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseQuery(Uri uri)
        {
            this.Uri = uri;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a query for the given feed Uri string.</summary>
        /// <param name="uri">feed uri string</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseQuery(string uri)
        {
            this.Uri = new Uri(uri);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Parses the Google-Base specific parameters
        /// in the Uri.</summary>
        ///////////////////////////////////////////////////////////////////////
        protected override Uri ParseUri(Uri targetUri)
        {
            base.ParseUri(targetUri);
            if (targetUri != null)
            {
                char[] deli = { '?', '&'};

                TokenCollection tokens = new TokenCollection(targetUri.Query, deli);
                foreach (String token in tokens)
                {
                    if (token.Length > 0)
                    {
                        char[] otherDeli = { '='};
                        String[] parameters = token.Split(otherDeli, 2);
                        switch (parameters[0])
                        {
                        case BqParameter:
                            this.bq = System.Web.HttpUtility.UrlDecode(parameters[1]);
                            break;
                        case MaxValuesParameter:
                            this.maxValues = NumberFormat.ToInt(parameters[1]);
                            break;
                        }
                    }
                }
            }
            return this.Uri;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Resets the bq and max-values field state</summary>
        ///////////////////////////////////////////////////////////////////////
        protected override void Reset()
        {
            base.Reset();
            bq = null;
            maxValues = -1;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates the Google Base specific parameters</summary>
        ///////////////////////////////////////////////////////////////////////
        protected override string CalculateQuery()
        {
            string path = base.CalculateQuery();
            if (bq == null && maxValues < 0)
            {
                return path;
            }

            StringBuilder newPath = new StringBuilder(path, 2048);

            char paramInsertion = (path.IndexOf('?') == -1) ? '?' : '&';

            if (this.bq != null)
            {
                newPath.Append(paramInsertion);
                newPath.Append(BqParameter);
                newPath.Append('=');
                newPath.Append(Utilities.UriEncodeReserved(bq));
                paramInsertion = '&';
            }
            if (this.maxValues >= 0)
            {
                newPath.Append(paramInsertion);
                newPath.Append(MaxValuesParameter);
                newPath.Append('=');
                newPath.Append(NumberFormat.ToString(maxValues));
            }
            return newPath.ToString();
        }
    }

}
