/* Copyright (c) 2007-2008 Google Inc.
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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
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
    /// A subclass of FeedQuery to query a Google Apps email list
    /// feed URI.
    /// 
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public class EmailListQuery : FeedQuery
    {
        private const string feedUriExtension = "/emailList/2.0";

        private string domain;
        private string recipient;
        private string emailListName;
        private string startEmailListName;
        private bool retrieveAllEmailLists;
        
        /// <summary>
        /// Constructs a new EmailListQuery to retrieve all email lists
        /// in the specified domain.
        /// 
        /// In addition to calling the constructor, you may set either
        /// Recipient or EmailListName (but not both) to restrict your query.
        /// </summary>
        /// <param name="domain">the domain to query</param>
        public EmailListQuery(string domain)
        {
            this.domain = domain;
            this.recipient = null;
            this.emailListName = null;
            this.startEmailListName = null;
            this.retrieveAllEmailLists = true;
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
        /// Accessor method for Recipient.  If this property is
        /// non-null, the query will retrieve all email list
        /// subscriptions for the specified email address.
        /// </summary>
        public string Recipient
        {
            get { return recipient; }
            set {
                if (emailListName != null || startEmailListName != null)
                {
                    throw new GDataRequestException("At most one of Recipient, EmailListName and " +
                                                    "StartEmailListName may be set.");
                }
                else
                {
                    recipient = value;
                }
            }
        }

        /// <summary>
        /// Accessor method for EmailListName.  If this property is
        /// non-null, the query will retrieve only the specified
        /// email list.
        /// </summary>
        public string EmailListName
        {
            get { return emailListName; }
            set {
                if (recipient != null || startEmailListName != null)
                {
                    throw new GDataRequestException("At most one of Recipient, EmailListName and " +
                                                    "StartEmailListName may be set.");
                }
                else
                {
                    emailListName = value;
                }
            }
        }

        /// <summary>
        /// Accessor method for StartEmailListName.  If this property
        /// is non-null, the query will retrieve a page of at most 100
        /// email lists, starting with the specified email list.
        /// </summary>
        public string StartEmailListName
        {
            get { return startEmailListName; }
            set {
                if (recipient != null || emailListName != null)
                {
                    throw new GDataRequestException("At most one of Recipient, EmailListName and " +
                                                    "StartEmailListName may be set.");
                }
                else
                {
                    startEmailListName = value;
                }
            }
        }

        /// <summary>
        /// Accessor method for RetrieveAllEmailLists.  If false,
        /// the query returns at most 100 matches; if it is
        /// true (default), all matches are returned.
        /// </summary>
        public bool RetrieveAllEmailLists
        {
            get { return retrieveAllEmailLists; }
            set { retrieveAllEmailLists = value; }
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

            if (Recipient != null)
            {
                path.Append("?recipient=");
                path.Append(Recipient);
            }
            else if (EmailListName != null)
            {
                path.Append("/");
                path.Append(EmailListName);
            }
            else if (StartEmailListName != null)
            {
                path.Append("?startEmailListName=");
                path.Append(StartEmailListName);
            }

            return path.ToString();
        }
    }
}
