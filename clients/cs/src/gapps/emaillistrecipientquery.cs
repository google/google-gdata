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
    /// recipient feed URI.
    /// 
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public class EmailListRecipientQuery : FeedQuery
    {
        private const string feedUriExtension = "/emailList/2.0";

        private string domain;
        private string emailListName;
        private string recipient;
        private string startRecipient;
        private bool retrieveAllRecipients;

        /// <summary>
        /// Constructs a new EmailListRecipientQuery to retrieve
        /// the list of recipients for the specified email list on
        /// the specified domain.
        /// </summary>
        /// <param name="domain">the domain to query</param>
        /// <param name="emailListName">the email list whose recipients
        /// we wish to retrieve</param>
        public EmailListRecipientQuery(string domain, string emailListName)
        {
            this.domain = domain;
            this.emailListName = emailListName;
            this.retrieveAllRecipients = true;
            this.startRecipient = null;
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
        /// Accessor method for EmailListName.
        /// </summary>
        public string EmailListName
        {
            get { return emailListName; }
            set { emailListName = value; }
        }

        /// <summary>
        /// Accessor method for RetrieveAllEmailLists.  If false,
        /// the query returns at most 100 matches; if it is
        /// true (default), all matches are returned.
        /// </summary>
        public bool RetrieveAllRecipients
        {
            get { return retrieveAllRecipients; }
            set { retrieveAllRecipients = value; }
        }

        /// <summary>
        /// Accessor method for Recipient.
        /// </summary>
        public string Recipient
        {
            get { return recipient; }
            set {
                if (startRecipient != null)
                {
                    throw new GDataRequestException("Recipient and StartRecipient cannot both be set.");
                }
                else
                {
                    recipient = value;
                }
            }
        }

        /// <summary>
        /// Accessor method for StartRecipient.
        /// </summary>
        public string StartRecipient
        {
            get { return startRecipient; }
            set {
                if (recipient != null)
                {
                    throw new GDataRequestException("Recipient and StartRecipient cannot both be set.");
                }
                else
                {
                    startRecipient = value;
                }
            }
        }

        /// <summary>
        /// Creates the URI query string based on all set properties.
        /// </summary>
        /// <returns>the URI query string</returns>
        protected override string CalculateQuery(string basePath)
        {
            StringBuilder path = new StringBuilder(AppsNameTable.appsBaseFeedUri, 2048);

            path.Append(Domain);
            path.Append(feedUriExtension);

            path.Append("/");
            path.Append(EmailListName);
            if (StartRecipient != null)
            {
                path.Append("?startRecipient=");
                path.Append(StartRecipient);
            } 
            else 
            {
                path.Append("/recipient/");
                if (Recipient != null) 
                {
                    path.Append(Recipient);
                }
            }

            return path.ToString();
        }
    }
}
