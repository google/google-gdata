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
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Spreadsheets
{
    /// <summary>
    /// A subclass of FeedQuery, to create a Spreadsheets list query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public class ListQuery : FeedQuery
    {
        private string spreadsheetQuery;
        private string orderByColumn;
        private bool orderByPosition;
        private bool reverse;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="key">The spreadsheet key</param>
        /// <param name="worksheetId">The unique identifier or position of the worksheet</param>
        /// <param name="visibility">public or private</param>
        /// <param name="projection">full, values, or basic</param>
        public ListQuery(string key, string worksheetId, string visibility, string projection) 
        : base("https://spreadsheets.google.com/feeds/list/" + key + "/" + worksheetId + "/" 
               + visibility + "/" + projection)
        {
            Reset();
        }

        /// <summary>
        /// Constructor - Sets the base URI
        /// </summary>
        /// <param name="baseUri">The feed base</param>
        /// <param name="key">The spreadsheet key</param>
        /// <param name="worksheetId">The unique identifier or position of the worksheet</param>
        /// <param name="visibility">public or private</param>
        /// <param name="projection">full, values, or basic</param>
        public ListQuery(string baseUri, string key, string worksheetId, string visibility, string projection)
        : base(baseUri + "/" + key + "/" + worksheetId + "/" + visibility + "/" + projection)
        {
            Reset();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUri">The feed base with the key, worksheetId, visibility and 
        /// projections are appended and delimited by "/"</param>
        public ListQuery(string baseUri) : base(baseUri) 
        {
            Reset();
        }

        /// <summary>
        /// A spreadsheet query string, if set to a non-null value, 
        /// then the FullTextQuery will be set to null
        /// </summary>
        public string SpreadsheetQuery
        {
            get
            {
                return spreadsheetQuery;
            }

            set
            {
                spreadsheetQuery = value;
            }
        }

        /// <summary>
        /// The header of the column to sort results by.  Sets OrderByPosition to false.
        /// </summary>
        public string OrderByColumn
        {
            get
            {
                return orderByColumn;
            }

            set
            {
                if (value != null)
                {
                    orderByPosition = false;
                }
                orderByColumn = value;

            }
        }

        /// <summary>
        /// If true, then results will be ordered by the position in the spreadsheet.  
        /// Sets OrderByColumn to null.
        /// </summary>
        public bool OrderByPosition
        {
            get
            {
                return orderByPosition;
            }

            set
            {
                if (value)
                {
                    orderByColumn = null;
                }
                orderByPosition = value;
            }
        }

        /// <summary>
        /// If true, then however the results are ordered will be reversed.
        /// </summary>
        public bool Reverse
        {
            get
            {
                return reverse;
            }

            set
            {
                if (OrderByColumn != null || OrderByPosition)
                {
                    reverse = value;
                }
            }
        }
#if WindowsCE || PocketPC
#else

        /// <summary>
        /// Parses an incoming URI string and sets the instance variables
        /// of this object.
        /// </summary>
        /// <param name="targetUri">Takes an incoming Uri string and parses all the properties of it</param>
        /// <returns>Throws a query exception when it finds something wrong with the input, otherwise returns a baseuri.</returns>
        protected override Uri ParseUri(Uri targetUri)
        {
            base.ParseUri(targetUri);
            if (targetUri != null)
            {
                char[] delimiters = { '?', '&'};

                string source = HttpUtility.UrlDecode(targetUri.Query);
                TokenCollection tokens = new TokenCollection(source, delimiters);
                foreach (String token in tokens)
                {
                    if (token.Length > 0)
                    {
                        char[] otherDelimiters = { '='};
                        String[] parameters = token.Split(otherDelimiters, 2);
                        switch (parameters[0])
                        {
                            case "sq":
                                this.SpreadsheetQuery = parameters[1];
                                break;
                            case "orderby":
                                if (parameters[1].Equals("position"))
                                {
                                    OrderByPosition = true;
                                }
                                else if (parameters[1].StartsWith("column:"))
                                {
                                    OrderByColumn = parameters[1].Substring(("column:").Length);
                                }
                                else
                                {
                                    throw new ClientQueryException();
                                }
                                break;
                            case "reverse":
                                this.Reverse = bool.Parse(parameters[1]);
                                break;
                        }
                    }
                }
            }
            return this.Uri;
        }
#endif
        /// <summary>
        /// Resets object state to default, as if newly created.
        /// </summary>
        protected override void Reset()
        {
            base.Reset();
            SpreadsheetQuery = null;
            OrderByColumn = null;
            OrderByPosition = false;
            Reverse = false;
        }

        /// <summary>
        /// Creates the partial URI query string based on all set properties.
        /// </summary>
        /// <returns> string => the query part of the URI </returns>
        protected override string CalculateQuery(string basePath)
        {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path); 

            if (OrderByPosition)
            {
                newPath.Append(paramInsertion);
                newPath.Append("orderby=position");
                paramInsertion = '&';
            }
            else if (OrderByColumn != null && OrderByColumn.Length > 0)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "orderby=column:{0}", Utilities.UriEncodeReserved(OrderByColumn));
                paramInsertion = '&';
            }

            if (Reverse)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "reverse={0}", Utilities.UriEncodeReserved(true.ToString()));
                paramInsertion = '&';
            }

            if (SpreadsheetQuery != null && SpreadsheetQuery.Length > 0)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "sq={0}", Utilities.UriEncodeReserved(SpreadsheetQuery));
                paramInsertion = '&';
            }

            return newPath.ToString();
        }
    }
}
