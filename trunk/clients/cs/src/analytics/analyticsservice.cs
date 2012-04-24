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
using Google.GData.Client;

namespace Google.GData.Analytics {

    #region Analytics specific constants

    public class AnalyticsNameTable {
        // <summary>GData GA extension namespace</summary>
        public const string gaNamespace = "http://schemas.google.com/ga/2009";
        /// <summary>prefix for GA namespace if writing</summary>
        public const string gaPrefix = "ga";
        // <summary>GData analytics extension namespace</summary>
        public const string gAnalyticsNamspace = "http://schemas.google.com/analytics/2009";
        /// <summary>prefix for gAnalyticsNamspace if writing</summary>
        public const string gAnalyticsPrefix = "dxp";
        /** Dxp (Dxp) namespace prefix */
        public const string DxpPrefix = gAnalyticsNamspace + "#";
        /// <summary>xmlelement for dxp:aggregates</summary> 
        public const string XmlAggregatesElement = "aggregates";
        /// <summary>xmlelement for dxp:dimension</summary> 
        public const string XmlDimensionElement = "dimension";
        /// <summary>xmlelement for dxp:metric</summary> 
        public const string XmlMetricElement = "metric";
        /// <summary>xmlelement for dxp:property</summary> 
        public const string XmlPropertyElement = "property";
        /// <summary>xmlelement for dxp:tableId</summary> 
        public const string XmlTableIdElement = "tableId";
        /// <summary>xmlelement for dxp:tableName</summary> 
        public const string XmlTableNameElement = "tableName";
        /// <summary>xmlelement for dxp:dataSource</summary> 
        public const string XmlDataSourceElement = "dataSource";
        /// <summary>xml attribute name for dxp:dimension, dxp:metric and dxp:property</summary> 
        public const string XmlAttributeName = "name";
        /// <summary>xml attribute value for dxp:dimension, dxp:metric and dxp:property</summary> 
        public const string XmlAttributeValue = "value";
        /// <summary>xml attribute confidenceInterval for dxp:metric</summary> 
        public const string XmlAttributeConfidenceInterval = "confidenceInterval";

        /// <summary>xml attribute number for ga:goal and ga:step</summary> 
        public const string XmlAttributeNumber = "number";
        /// <summary>xml attribute active for ga:goal</summary> 
        public const string XmlAttributeActive = "active";
        /// <summary>xmlelement for dxp:segment</summary> 
        public const string XmlSegmentElement = "segment";
        /// <summary>xml attribute id for dxp:segment</summary>
        public const string XmlAttributeId = "id";
        /// <summary>xmlelement definition for ga:goal</summary> 
        public const string XmlGoalElement = "goal";
        /// <summary>xmlelement definition for ga:engagement</summary> 
        public const string XmlDefinitionElement = "definition";

        /// <summary>xmlelement destination for ga:destination</summary> 
        public const string XmlDestinationElement = "destination";
        /// <summary>xml attribute caseSensitive for ga:destination</summary> 
        public const string XmlAttributeCaseSensitive = "caseSensitive";
        /// <summary>xmlelement expression for ga:destination</summary> 
        public const string XmlAttributeExpression = "expression";
        /// <summary>xmlelement matchType for ga:destination</summary> 
        public const string XmlAttributeMatchType = "matchType";
        /// <summary>xmlelement step1Required for ga:destination</summary> 
        public const string XmlAttributeStep1Required = "step1Required";

        /// <summary>xmlelement definition for ga:step</summary> 
        public const string XmlStepElement = "step";
        /// <summary>xml attribute path for ga:step</summary> 
        public const string XmlAttributePath = "path";

        /// <summary>xmlelement engagement for ga:engagement</summary> 
        public const string XmlEngagementElement = "engagement";
        /// <summary>xml attribute comparison for ga:destination</summary> 
        public const string XmlAttributeComparison = "comparison";
        /// <summary>xml attribute thresholdValue for ga:destination</summary> 
        public const string XmlAttributeThresholdValue = "thresholdValue";
        /// <summary>xml attribute type for ga:destination</summary> 
        public const string XmlAttributeType = "type";

        /// <summary>xmlelement customVariable for ga:customVariable</summary> 
        public const string XmlCustomVariableElement = "customVariable";
        /// <summary>xml attribute index for ga:customVariable</summary> 
        public const string XmlAttributeIndex = "index";
        /// <summary>xml attribute scope for ga:customVariable</summary> 
        public const string XmlAttributeScope = "scope";
    }

    #endregion

    /// <summary>
    /// The AnalyticsService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
    /// Google Analytics data API.
    /// </summary>
    public class AnalyticsService : Service {
        /// <summary>The Analytics service's name</summary> 
        public const string GAnalyticsService = "analytics";

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public AnalyticsService(string applicationName)
            : base(GAnalyticsService, applicationName) {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>AccountFeed</returns>
        public AccountFeed Query(AccountQuery feedQuery) {
            return base.Query(feedQuery) as AccountFeed;
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>DataFeed</returns>
        public DataFeed Query(DataQuery feedQuery) {
            return base.Query(feedQuery) as DataFeed;
        }

        /// <summary>
        /// This library implements the Google Analytics API version 2.
        /// </summary>
        /// <returns></returns>
        protected override void InitVersionInformation() {
            this.ProtocolMajor = VersionDefaults.VersionTwo;
        }

        /// <summary>eventchaining. We catch this by from the base service, which 
        /// would not by default create an atomFeed</summary> 
        /// <param name="sender">the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        protected void OnNewFeed(object sender, ServiceEventArgs e) {
            Tracing.TraceMsg("Created new Analytics Feed");
            if (e == null) {
                throw new ArgumentNullException("e");
            }
            // do not use string.contains, does not exist on CF framework
            if (e.Uri.AbsolutePath.IndexOf("/analytics/feeds/accounts/") != -1) {
                //https://www.google.com/analytics/feeds/accounts/default
                e.Feed = new AccountFeed(e.Uri, e.Service);
            } else {
                //https://www.google.com/analytics/feeds/data
                e.Feed = new DataFeed(e.Uri, e.Service);
            }
        }
    }
}
