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
/* Created by Morten Christensen, elpadrinodk@gmail.com, http://blog.sitereactor.dk */
using System;
using System.Globalization;
using System.Text;
using Google.GData.Client;

namespace Google.GData.Analytics
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of FeedQuery, to create an Analytics Data query URI.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class DataQuery : FeedQuery
    {
        /// <summary> Row keys.</summary>
		private string dimensions;

        /// <summary> Last day for which to retrieve data in form YYYY-MM-DD.</summary>
		private string endDate;

        /// <summary> Dimension value filters.</summary>
		private string filters;

        /// <summary> Google Analytics profile ID, prefixed by 'ga:'.</summary>
		private string ids;

        /// <summary> Comma separated list of numeric value fields.</summary>
		private string metrics;

        /// <summary> Comma separated list of sort keys in order of importance.</summary>
		private string sort;

        /// <summary> First day for which to retrieve data in form YYYY-MM-DD.</summary>
		private string startDate;

        /// <summary> Optional. Adds extra whitespace to the feed XML to make it more readable.</summary>
        private bool? prettyPrint;

        /// <summary>
        /// default constructor, does nothing 
        /// </summary>
        public DataQuery() : base()
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public DataQuery(string queryUri)
        : base(queryUri)
        {
        }
		
		
        //////////////////////////////////////////////////////////////////////
        /// <summary>Indicates the row keys</summary>
        /// <returns>Dimensions</returns>
        //////////////////////////////////////////////////////////////////////
        public string Dimensions
        {
            get {return this.dimensions;}
            set {this.dimensions = value;}
        }
        // end of accessor public string Dimensions
		
		//////////////////////////////////////////////////////////////////////
        /// <summary>Indicates the last day for which to retrieve data 
        /// in form YYYY-MM-DD.</summary>
        /// <returns>EndDate</returns>
        //////////////////////////////////////////////////////////////////////
        public string GAEndDate
        {
            get {return this.endDate;}
            set {this.endDate = value;}
        }
        // end of accessor public string EndDate
		
		//////////////////////////////////////////////////////////////////////
        /// <summary>Indicates the dimension value filters.</summary>
        /// <returns>Filters</returns>
        //////////////////////////////////////////////////////////////////////
        public string Filters
        {
            get {return this.filters;}
            set {this.filters = value;}
        }
        // end of accessor public string Filters
		
		//////////////////////////////////////////////////////////////////////
        /// <summary>Indicates the Google Analytics profile ID, 
        /// prefixed by 'ga:'.</summary>
        /// <returns>Ids</returns>
        //////////////////////////////////////////////////////////////////////
        public string Ids
        {
            get {return this.ids;}
            set {this.ids = value;}
        }
        // end of accessor public string Ids
		
		//////////////////////////////////////////////////////////////////////
        /// <summary>Indicates the comma separated list of numeric value 
        /// fields.</summary>
        /// <returns>Metrics</returns>
        //////////////////////////////////////////////////////////////////////
        public string Metrics
        {
            get {return this.metrics;}
            set {this.metrics = value;}
        }
        // end of accessor public string Metrics
		
		//////////////////////////////////////////////////////////////////////
        /// <summary>Indicates the comma separated list of sort keys 
        /// in order of importance.</summary>
        /// <returns>Sort</returns>
        //////////////////////////////////////////////////////////////////////
        public string Sort
        {
            get {return this.sort;}
            set {this.sort = value;}
        }
        // end of accessor public string Sort
		
		//////////////////////////////////////////////////////////////////////
        /// <summary>Indicates the first day for which to retrieve data 
        /// in form YYYY-MM-DD.</summary>
        /// <returns>StartDate</returns>
        //////////////////////////////////////////////////////////////////////
        public string GAStartDate
        {
            get {return this.startDate;}
            set {this.startDate = value;}
        }
        // end of accessor public string StartDate

        //////////////////////////////////////////////////////////////////////
        /// <summary>Adds extra whitespace to the feed XML to make it 
        /// more readable. This can be set to true or false, where the 
        /// default is false.</summary>
        /// <returns>PrettyPrint</returns>
        //////////////////////////////////////////////////////////////////////
        public bool? PrettyPrint
        {
            get { return this.prettyPrint; }
            set { this.prettyPrint = value; }
        }
        // end of accessor public bool PrettyPrint
		
#if WindowsCE || PocketPC
#else
        //////////////////////////////////////////////////////////////////////
        /// <summary>protected void ParseUri</summary> 
        /// <param name="targetUri">takes an incoming Uri string and parses 
        /// all the properties out of it</param>
        /// <returns>throws a query exception when it finds something 
        /// wrong with the input, otherwise returns a baseuri</returns>
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
                            case "dimensions":
                                this.dimensions = parameters[1];
                                break;
                            case "end-date":
                                this.endDate = parameters[1];
                                break;
                            case "filters":
                                this.filters = parameters[1];
                                break;
                            case "ids":
                                this.ids = parameters[1];
                                break;
                            case "metrics":
                                this.metrics = parameters[1];
                                break;
                            case "sort":
                                this.sort = parameters[1];
                                break;
                            case "start-date":
                                this.startDate = parameters[1];
                                break;
                            case "prettyprint":
                                this.prettyPrint = bool.Parse(parameters[1]);
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
			
            if (!string.IsNullOrEmpty(this.Dimensions))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "dimensions={0}", Utilities.UriEncodeReserved(this.Dimensions));
                paramInsertion = '&';
            }
            if (!string.IsNullOrEmpty(this.GAEndDate))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "end-date={0}", Utilities.UriEncodeReserved(this.GAEndDate));
                paramInsertion = '&';
            }
            if (!string.IsNullOrEmpty(this.Filters))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "filters={0}", Utilities.UriEncodeReserved(this.Filters));
                paramInsertion = '&';
            }
            if (!string.IsNullOrEmpty(this.Ids))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "ids={0}", Utilities.UriEncodeReserved(this.Ids));
                paramInsertion = '&';
            }
            if (!string.IsNullOrEmpty(this.Metrics))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "metrics={0}", Utilities.UriEncodeReserved(this.Metrics));
                paramInsertion = '&';
            }
            if (!string.IsNullOrEmpty(this.Sort))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "sort={0}", Utilities.UriEncodeReserved(this.Sort));
                paramInsertion = '&';
            }
            if (!string.IsNullOrEmpty(this.GAStartDate))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "start-date={0}", Utilities.UriEncodeReserved(this.GAStartDate));
                paramInsertion = '&';
            }
            if (this.PrettyPrint.HasValue)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "prettyprint={0}", Utilities.UriEncodeReserved(this.PrettyPrint.Value.ToString().ToLower()));
                paramInsertion = '&';
            }
            return newPath.ToString();
        }
    }
}
