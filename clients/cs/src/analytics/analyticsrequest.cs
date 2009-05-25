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

namespace Google.Analytics 
{
    public class Account : Entry
    {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new AccountEntry();
            }
        }
        /// <summary>
        /// readonly accessor to the typed underlying atom object
        /// </summary>
        public AccountEntry AccountEntry
        {
            get
            {
                return this.AtomEntry as AccountEntry;
            }
        }

        /// <summary>
        /// The web property ID associated with the profile.
        /// </summary>
        /// <returns></returns>
        public string WebPropertyId
        {
            get
            {
                if (this.AccountEntry != null)
                {
                    return this.AccountEntry.FindPropertyValue("ga:webPropertyId");
                }
                return null; 
            }
        }

        /// <summary>
        /// The name of the account associated with the profile
        /// </summary>
        /// <returns></returns>
        public string AccountName
        {
            get
            {
                if (this.AccountEntry != null)
                {
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
        public string AccountId
        {
            get
            {
                if (this.AccountEntry != null)
                {
                    return this.AccountEntry.FindPropertyValue("ga:accountId");
                }
                return null; 
            }
        }

        /// <summary>
        /// The numberic id of the profile
        /// </summary>
        /// <returns></returns>
        public string ProfileId
        {
            get
            {
                if (this.AccountEntry != null)
                {
                    return this.AccountEntry.FindPropertyValue("ga:profileId");
                }
                return null; 
            }
        }

        /// <summary>
        /// The unique, namespaced ID to be used when requesting data from a profile.
        /// </summary>
        /// <returns></returns>
        public string TableId
        {
            get
            {
                if (this.AccountEntry != null)
                {
                    TableId t = this.AccountEntry.ProfileId;
                    if (t != null)
                    {
                        return t.Value;
                    }
                }
                return null; 
            }
        }
    }

   

    //////////////////////////////////////////////////////////////////////
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
    //////////////////////////////////////////////////////////////////////
    public class AnalyticsRequest : FeedRequest<AnalyticsService>
    {

        /// <summary>
        /// default constructor for a YouTubeRequest
        /// </summary>
        /// <param name="settings"></param>
        public AnalyticsRequest(RequestSettings settings) : base(settings)
        {
            this.Service = new AnalyticsService(settings.Application);
            PrepareService();
        }

        /// <summary>
        /// returns a Feed of accounts for the authenticated user
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Account objects</returns>
        public Feed<Account> GetAccounts()
        {
            AccountQuery q = PrepareQuery<AccountQuery>(AccountQuery.HttpsFeedUrl);
            return PrepareFeed<Account>(q); 
        }
    }
}
