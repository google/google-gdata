/* Copyright (c) 2007 Google Inc.
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
using System.Xml;
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps
{
    /// <summary>
    /// A subclass of FeedQuery to query a Google Apps user
    /// accounts feed URI.
    /// 
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public class UserQuery : FeedQuery
    {
        private const string feedUriExtension = "/user/2.0";

        private string domain;
        private string startUserName;
        private string userName;
        private bool retrieveAllUsers;

        /// <summary>
        /// Constructs a new UserQuery to retrieve all users
        /// in the specified domain.
        /// </summary>
        /// <param name="domain">the domain to access</param>
        public UserQuery(string domain)
        {
            this.domain = domain;
            this.userName = null;
            this.startUserName = null;
            this.retrieveAllUsers = true;
        }

        /// <summary>
        /// Constructs a new UserQuery to retrieve users in
        /// the specified domain.  Use this constructor if you only
        /// wish to retrieve the first 100 users, instead of the
        /// entire list.
        /// </summary>
        /// <param name="domain">the domain to access</param>
        /// <param name="retrieveAllUsers">true to retrieve all matches,
        /// false to return a maximum of 100 users</param>
        public UserQuery(string domain, bool retrieveAllUsers)
        {
            this.domain = domain;
            this.userName = null;
            this.retrieveAllUsers = retrieveAllUsers;
        }

        /// <summary>
        /// Accessor method for Domain.
        /// </summary>
        public string Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        /// <summary>
        /// Accessor method for StartUserName.  If set,
        /// the query will return a feed of at most 100
        /// users beginning at this username.
        /// </summary>
        public string StartUserName
        {
            get { return startUserName; }
            set {
                if (userName != null)
                {
                    throw new GDataRequestException("Username and start username cannot both be set.");
                }
                else
                {
                    startUserName = value;
                }
            }
        }

        /// <summary>
        /// Accessor method for UserName.  If set, this
        /// query will return a feed containing only the
        /// specified user.  If both UserName and StartUserName
        /// are null, the query returns the feed of all users
        /// in the domain.
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set {
                if (startUserName != null)
                {
                    throw new GDataRequestException("Username and start username cannot both be set.");
                }
                else
                {
                    userName = value;
                }
            }
        }

        /// <summary>
        /// Accessor method for RetrieveAllUsers.  If false,
        /// the query returns at most 100 matches; if it is
        /// true (default), all matches are returned.
        /// </summary>
        public bool RetrieveAllUsers
        {
            get { return retrieveAllUsers; }
            set { retrieveAllUsers = value; }
        }

        /// <summary>
        /// Creates the URI query string based on all set properties.
        /// </summary>
        /// <returns>the URI query string</returns>
        protected override string CalculateQuery(string basePath)
        {
            StringBuilder path = new StringBuilder(AppsNameTable.appsBaseFeedUri, 2048);

            path.Append(domain);
            path.Append(feedUriExtension);

            if (startUserName != null)
            {
                path.Append("?startUsername=");
                path.Append(startUserName);
            }
            else if (userName != null)
            {
                path.Append("/");
                path.Append(userName);
            }

            return path.ToString();   
        }
    }
}
