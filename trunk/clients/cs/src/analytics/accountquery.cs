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
using Google.GData.Client;

namespace Google.GData.Analytics {
    /// <summary>
    /// A subclass of FeedQuery, to create an Analytics Account query URI.
    /// </summary> 
    public class AccountQuery : FeedQuery {
        /// <summary>
        /// Analytics account service url, http and https 
        /// </summary>

        public const string HttpsFeedUrl = "https://www.google.com/analytics/feeds/accounts/default";

        /// <summary>
        /// default constructor, does nothing 
        /// </summary>
        public AccountQuery()
            : base(HttpsFeedUrl) {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public AccountQuery(string queryUri)
            : base(queryUri) {
        }
    }
}
