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
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Analytics;
using System.Collections.Generic;

namespace Google.Analytics {
    /// <summary>
    /// simple class to cover the AcountEntry specific extensions. Gives you access to all Account
    /// specific properties inisde an account feed. 
    /// </summary>
    /// <returns></returns>
    public class Account : Entry {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject() {
            if (this.AtomEntry == null) {
                this.AtomEntry = new AccountEntry();
            }
        }
        /// <summary>
        /// readonly accessor to the typed underlying atom object
        /// </summary>
        public AccountEntry AccountEntry {
            get {
                return this.AtomEntry as AccountEntry;
            }
        }

        /// <summary>
        /// The web property ID associated with the profile.
        /// </summary>
        /// <returns></returns>
        public string WebPropertyId {
            get {
                if (this.AccountEntry != null) {
                    return this.AccountEntry.FindPropertyValue("ga:webPropertyId");
                }
                return null;
            }
        }

        /// <summary>
        /// The name of the account associated with the profile
        /// </summary>
        /// <returns></returns>
        public string AccountName {
            get {
                if (this.AccountEntry != null) {
                    return this.AccountEntry.FindPropertyValue("ga:accountName");
                }
                return null;
            }
        }

        /// <summary>
        /// The id of the account associated with the profile
        /// used in the tracking code for your web property (e.g. UA-30481-22).
        /// </summary>
        /// <returns></returns>
        public string AccountId {
            get {
                if (this.AccountEntry != null) {
                    return this.AccountEntry.FindPropertyValue("ga:accountId");
                }
                return null;
            }
        }

        /// <summary>
        /// The numberic id of the profile
        /// </summary>
        /// <returns></returns>
        public string ProfileId {
            get {
                if (this.AccountEntry != null) {
                    return this.AccountEntry.FindPropertyValue("ga:profileId");
                }
                return null;
            }
        }

        /// <summary>
        /// The unique, namespaced ID to be used when requesting data from a profile.
        /// </summary>
        /// <returns></returns>
        public string TableId {
            get {
                if (this.AccountEntry != null) {
                    TableId t = this.AccountEntry.ProfileId;
                    if (t != null) {
                        return t.Value;
                    }
                }
                return null;
            }
        }
    }

    /// <summary>
    /// simple class to cover the DataEntry specific extensions. Gives you access to all Data
    /// specific properties inisde an data feed. 
    /// </summary>
    /// <returns></returns>
    public class Data : Entry {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject() {
            if (this.AtomEntry == null) {
                this.AtomEntry = new DataEntry();
            }
        }
        /// <summary>
        /// readonly accessor to the typed underlying atom object
        /// </summary>
        public DataEntry DataEntry {
            get {
                return this.AtomEntry as DataEntry;
            }
        }

        /// <summary>
        /// This field controls the dimensions.
        /// </summary>
        public List<Dimension> Dimensions {
            get {
                if (this.DataEntry != null) {
                    return this.DataEntry.Dimensions;
                }
                return null;
            }
        }

        /// <summary>
        /// This field controls the metrics.
        /// </summary>
        public List<Metric> Metrics {
            get {
                if (this.DataEntry != null) {
                    return this.DataEntry.Metrics;
                }
                return null;
            }
        }
    }

    /// <summary>
    /// subclass of a Feed, specific to Data entries
    /// </summary>
    /// <returns></returns>
    public class Dataset : Feed<Data> {
        /// <summary>
        /// default constructor that takes the underlying atomfeed
        /// </summary>
        /// <param name="af"></param>
        public Dataset(AtomFeed af)
            : base(af) {
        }

        /// <summary>
        /// constructs a new feed object based on a service and a query
        /// </summary>
        /// <param name="service"></param>
        /// <param name="q"></param>
        public Dataset(Service service, FeedQuery q)
            : base(service, q) {
        }

        /// <summary>
        /// returns the Aggregates object that contains aggregate data for all metrics requested in the feed
        /// </summary>
        /// <returns></returns>
        public Aggregates Aggregates {
            get {
                Google.GData.Analytics.DataFeed f = this.AtomFeed as Google.GData.Analytics.DataFeed;
                if (f != null) {
                    return f.Aggregates;
                }
                return null;
            }
        }

        /// <summary>
        /// returns the Aggregates object that contains aggregate data for all metrics requested in the feed
        /// </summary>
        /// <returns></returns>
        public DataSource DataSource {
            get {
                DataFeed f = this.AtomFeed as DataFeed;
                if (f != null) {
                    return f.DataSource;
                }
                return null;
            }
        }
    }

    /// <summary>
    /// With the Google Analytics Data Export API, you can develop client 
    /// applications that download Analytics data in the form of Google Data API feeds. 
    /// Your client application can use the Data Export API to request data from
    /// an existing Analytics profile for an authorized user, and refine the
    /// results of the request using query parameters. Currently, the Data Export API
    /// supports read-only access to your Google Analytics data. 
    /// </summary>
    ///  <example>
    ///         The following code illustrates a possible use of   
    ///          the <c>AnalyticsRequest</c> object:  
    ///          <code>    
    ///            RequestSettings settings = new RequestSettings("yourApp");
    ///            settings.PageSize = 50; 
    ///            settings.AutoPaging = true;
    ///             AnalyticsRequest f = new AnalyticsRequest(settings);
    ///         Feed<Account> feed = f.GetAccounts();
    ///     
    ///         foreach (Account a in feed.Entries)
    ///         {
    ///         }
    ///  </code>
    ///  </example>
    public class AnalyticsRequest : FeedRequest<AnalyticsService> {
        /// <summary>
        /// default constructor for a AnalyticsRequest
        /// </summary>
        /// <param name="settings"></param>
        public AnalyticsRequest(RequestSettings settings)
            : base(settings) {
            this.Service = new AnalyticsService(settings.Application);
            PrepareService();
        }

        /// <summary>
        /// returns a Feed of accounts for the authenticated user
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Account objects</returns>
        public Feed<Account> GetAccounts() {
            AccountQuery q = PrepareQuery<AccountQuery>(AccountQuery.HttpsFeedUrl);
            return PrepareFeed<Account>(q);
        }

        /// <summary>
        /// returns a Data Feed per passed in Query. The AnalyticsRequestObject 
        /// will modify the passed in Query object to take the default RequestObject settings into 
        /// account (like paging). if you do not want that behaviour, use the Get method
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public Dataset GetReport(DataQuery q) {
            PrepareQuery(q);
            return PrepareFeed<Data>(q) as Dataset;
        }

        /// <summary>
        /// gets a feed object of type T
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="uri">The Uri to retrieve</param>
        /// <returns></returns>
        public Dataset Get(DataQuery q) {
            return PrepareFeed<Data>(q) as Dataset;
        }

        /// <summary>
        /// the virtual creator function for feeds, so that we can create feedsubclasses in
        /// in subclasses of the request
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        protected override Feed<Y> CreateFeed<Y>(FeedQuery q) {
            if (typeof(Y) == typeof(Data)) {
                return new Dataset(this.AtomService, q) as Feed<Y>;
            }
            return base.CreateFeed<Y>(q);
        }
    }
}
