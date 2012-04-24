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

namespace Google.GData.Analytics {    
    /// <summary>
    /// A subclass of FeedQuery, to create an Analytics Data query URI.
    /// </summary> 
    public class DataQuery : FeedQuery {
        public const string dataFeedUrl = "https://www.google.com/analytics/feeds/data";

        /// <summary> Row keys.</summary>
        private string dimensions;

        /// <summary> Last day for which to retrieve data in form YYYY-MM-DD.</summary>
        private string endDate;

        /// <summary> Dimension value filters.</summary>
        private string filters;

        /// <summary> Advanced Segment, either dynamic or by index</summary>
        private string segment;

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
        /// default constructor, constructs a query to the default analytics feed with no parameters
        /// </summary>
        public DataQuery()
            : base(DataQuery.dataFeedUrl) {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public DataQuery(string queryUri)
            : base(queryUri) {
        }

        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="ids">the account id</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataQuery(string id, DateTime startDate, DateTime endDate)
            : base(DataQuery.dataFeedUrl) {
            this.Ids = id;
            this.GAStartDate = startDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            this.GAEndDate = endDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="ids">the account id</param>
        /// <param name="metric"></param>
        /// <param name="dimension"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataQuery(string id, DateTime startDate, DateTime endDate, string metric, string dimension)
            : this(id, startDate, endDate) {
            this.Metrics = metric;
            this.Dimensions = dimension;
        }

        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="ids">the account id</param>
        /// <param name="metric"></param>
        /// <param name="dimension"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public DataQuery(string id, DateTime startDate, DateTime endDate, string metric, string dimension, string sorting) :
            this(id, startDate, endDate, metric, dimension) {
            this.Sort = sorting;
        }

        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="ids">the account id</param>
        /// <param name="metric"></param>
        /// <param name="dimension"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sorting"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public DataQuery(string id, DateTime startDate, DateTime endDate, string metric, string dimension, string sorting, string filters) :
            this(id, startDate, endDate, metric, dimension, sorting) {
            this.Filters = filters;
        }

        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="ids">the account id</param>
        /// <param name="metric"></param>
        /// <param name="dimension"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sorting"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public DataQuery(string id, DateTime startDate, DateTime endDate, string metric, string dimension, string sorting, string filters, string segment) :
            this(id, startDate, endDate, metric, dimension, sorting, filters) {
            this.Segment = segment;
        }

        /// <summary>The primary data keys from which Analytics reports
        ///  are constructed. Like metrics, dimensions are also categorized by type. 
        /// For example, ga:pageTitle and ga:page are two Content report dimensions
        ///  and ga:browser, ga:city, and ga:pageDepth are two visitor dimensions.</summary>
        /// <returns>Dimensions</returns>
        public string Dimensions {
            get { return this.dimensions; }
            set { this.dimensions = value; }
        }

        /// <summary>Indicates the last day for which to retrieve data 
        /// in form YYYY-MM-DD.</summary>
        /// <returns>EndDate</returns>
        public string GAEndDate {
            get { return this.endDate; }
            set { this.endDate = value; }
        }

        /// <summary>Indicates the dimension value filters.</summary>
        /// <returns>Filters</returns>
        public string Filters {
            get { return this.filters; }
            set { this.filters = value; }
        }

        /// <summary>Indicates the Segments.</summary>
        /// <returns>Segments</returns>
        public string Segment {
            get { return this.segment; }
            set { this.segment = value; }
        }

        /// <summary>Indicates the Google Analytics profile ID, 
        /// prefixed by 'ga:'.</summary>
        /// <returns>Ids</returns>
        public string Ids {
            get { return this.ids; }
            set { this.ids = value; }
        }

        /// <summary>Indicates the comma separated list of numeric value 
        /// fields.The aggregated statistics for user activity in a profile, 
        /// categorized by type. Examples of metrics include ga:clicks or ga:pageviews. 
        /// When queried by themselves, metrics provide an aggregate measure of user
        /// activity for your profile, such as overall page views or bounce rate. 
        /// However, when paired with dimensions, they provide information in the context
        /// of the dimension. For example, when pageviews are combined with 
        /// ga:countryOrTerritory, you see how many pageviews come from each country.</summary>
        /// <returns>Metrics</returns>
        public string Metrics {
            get { return this.metrics; }
            set { this.metrics = value; }
        }

        /// <summary>Indicates the comma separated list of sort keys 
        /// in order of importance.</summary>
        /// <returns>Sort</returns>
        public string Sort {
            get { return this.sort; }
            set { this.sort = value; }
        }

        /// <summary>Indicates the first day for which to retrieve data 
        /// in form YYYY-MM-DD.</summary>
        /// <returns>StartDate</returns>
        public string GAStartDate {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        /// <summary>Adds extra whitespace to the feed XML to make it 
        /// more readable. This can be set to true or false, where the 
        /// default is false.</summary>
        /// <returns>PrettyPrint</returns>
        public bool? PrettyPrint {
            get { return this.prettyPrint; }
            set { this.prettyPrint = value; }
        }

        /// <summary>protected void ParseUri</summary> 
        /// <param name="targetUri">takes an incoming Uri string and parses 
        /// all the properties out of it</param>
        /// <returns>throws a query exception when it finds something 
        /// wrong with the input, otherwise returns a baseuri</returns>
        protected override Uri ParseUri(Uri targetUri) {
            base.ParseUri(targetUri);
            if (targetUri != null) {
                char[] deli = { '?', '&' };

                string source = HttpUtility.UrlDecode(targetUri.Query);
                TokenCollection tokens = new TokenCollection(source, deli);
                foreach (String token in tokens) {
                    if (token.Length > 0) {
                        char[] otherDeli = { '=' };
                        String[] parameters = token.Split(otherDeli, 2);
                        switch (parameters[0]) {
                            case "dimensions":
                                this.dimensions = parameters[1];
                                break;
                            case "end-date":
                                this.endDate = parameters[1];
                                break;
                            case "filters":
                                this.filters = parameters[1];
                                break;
                            case "segment":
                                this.segment = parameters[1];
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

        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> string => the query part of the URI </returns>
        protected override string CalculateQuery(string basePath) {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path);

            paramInsertion = AppendQueryPart(this.Dimensions, "dimensions", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.GAEndDate, "end-date", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Filters, "filters", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Ids, "ids", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Metrics, "metrics", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Sort, "sort", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.GAStartDate, "start-date", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Segment, "segment", paramInsertion, newPath);

            if (this.PrettyPrint.HasValue && (bool)this.PrettyPrint) {
                paramInsertion = AppendQueryPart("true", "prettyprint", paramInsertion, newPath);
            }

            return newPath.ToString();
        }
    }
}
