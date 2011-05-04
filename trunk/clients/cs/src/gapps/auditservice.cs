/* Copyright (c) 2011 Google Inc.
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
using System.Collections.Generic;
using System.Web;
using Google.GData.Extensions.Apps;
using Google.GData.Client;

namespace Google.GData.Apps {
    /// <summary>
    /// Service class to allow Google Apps administrators to audit users' emails, drafts and archived chats, retrieve account login information and download users' mailboxes. 
    /// </summary>
    public class AuditService : AppsPropertyService {

        private readonly String baseMailMonitorUri = String.Format("{0}/{1}/{2}",
                                                                   AuditNameTable.AuditBaseFeedUri,
                                                                   AuditNameTable.mail,
                                                                   AuditNameTable.monitor);
        private readonly String baseMailboxDumpUri = String.Format("{0}/{1}/{2}",
                                                                   AuditNameTable.AuditBaseFeedUri,
                                                                   AuditNameTable.mail,
                                                                   AuditNameTable.export);
        private readonly String baseAccountInfoUri = String.Format("{0}/{1}",
                                                                   AuditNameTable.AuditBaseFeedUri,
                                                                   AuditNameTable.account);

        public AuditService(String domain, String applicationName)
            : base(domain, applicationName) {
            this.NewFeed += new ServiceEventHandler(this.OnParsedNewFeed);
        }

        /// <summary>
        /// Upload a public key for signing mailbox dump archives. This public encryption key should be a PGP format ascii-encoded RSA key.
        /// Before uploading the public key, convert it to a base64 encoded string.
        /// </summary>
        /// <param name="base64encodedKey">The base64-encoded, PGP format ASCII read RSA key</param>
        /// <returns>The inserted entry</returns>
        public AppsExtendedEntry UploadPublicKey(String base64encodedKey) {
            Uri keyUri = new Uri(String.Format("{0}/{1}/{2}",
                                               AuditNameTable.AuditBaseFeedUri,
                                               AuditNameTable.publicKeyUri,
                                               domain));
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(new PropertyElement(AuditNameTable.publicKeyProperty, base64encodedKey));
            return Insert(keyUri, entry);
        }

        /// <summary>
        /// Creates a new monitoring task to begin an audit.
        /// </summary>
        /// <param name="sourceUser">The user who receives or sends messages that are being audited</param>
        /// <param name="mailMonitor">The details of the monitoring task</param>
        /// <returns>The audit task being created</returns>
        public MailMonitor CreateMailMonitor(String sourceUser, MailMonitor mailMonitor) {
            Uri requestUri = new Uri(String.Format("{0}/{1}/{2}",
                                                   baseMailMonitorUri,
                                                   domain,
                                                   sourceUser));

            return Insert(requestUri, mailMonitor) as MailMonitor;
        }

        /// <summary>
        /// Retrieves all monitors for a given user.
        /// </summary>
        /// <param name="sourceUser">The user whose monitors are to be retrieved</param>
        /// <returns>The details of the existing monitors for the user</returns>
        public GenericFeed<MailMonitor> RetrieveMailMonitors(String sourceUser) {
            Uri requestUri = new Uri(String.Format("{0}/{1}/{2}",
                                                   baseMailMonitorUri,
                                                   domain,
                                                   sourceUser));

            return QueryExtendedFeed(requestUri, true) as GenericFeed<MailMonitor>;
        }

        /// <summary>
        /// Removes the monitor configured for the given source and destination user.
        /// </summary>
        /// <param name="sourceUser">The user who is being monitored</param>
        /// <param name="destUser">The user who receives the audited email messages</param>
        public void DeleteMailMonitor(String sourceUser, String destUser) {
            Uri requestUri = new Uri(String.Format("{0}/{1}/{2}/{3}",
                                                   baseMailMonitorUri,
                                                   domain,
                                                   sourceUser,
                                                   destUser));

            Delete(requestUri);
        }

        /// <summary>
        /// Creates a new request for obtaining a user mailbox dump. The mailbox files are encrypted using
        /// the key uploaded and are available in mbox format.
        /// </summary>
        /// <param name="sourceUser">The user to export the mailbox for</param>
        /// <param name="mailboxDumpRequest">The details of the request</param>
        /// <returns>The entry being created</returns>
        public MailboxDumpRequest CreateMailboxDumpRequest(String sourceUser, MailboxDumpRequest mailboxDumpRequest) {
            Uri requestUri = new Uri(String.Format("{0}/{1}/{2}",
                                                   baseMailboxDumpUri,
                                                   domain,
                                                   sourceUser));

            return Insert(requestUri, mailboxDumpRequest);
        }

        /// <summary>
        /// Retrieves the mailbox dump request for the given ID and user.
        /// </summary>
        /// <param name="sourceUser">The user whose dump requests need to be retrieved</param>
        /// <param name="requestId">The id of the mailbox dump request</param>
        /// <returns>The details of the mailbox dump request</returns>
        public MailboxDumpRequest RetrieveMailboxDumpRequest(String sourceUser, String requestId) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}",
                                              baseMailboxDumpUri,
                                              domain,
                                              sourceUser,
                                              requestId);

            return Get(requestUri) as MailboxDumpRequest;
        }

        /// <summary>
        /// Retrieves all mailbox dump requests.
        /// </summary>
        /// <param name="fromDate">The starting date for the mailbox dump requests to be retrieved</param>
        /// <returns>All mailbox dump requests for the domain</returns>
        public GenericFeed<MailboxDumpRequest> RetrieveAllMailboxDumpRequests(DateTime? fromDate) {
            String requestUri = String.Format("{0}/{1}",
                                              baseMailboxDumpUri,
                                              domain);

            if (fromDate != null) {
                requestUri = String.Format("{0}?{1}={2}",
                                           requestUri,
                                           AuditNameTable.fromDate,
                                           ((DateTime)fromDate).ToString(AuditNameTable.dateFormat));
            }

            return QueryExtendedFeed(new Uri(requestUri), true) as GenericFeed<MailboxDumpRequest>;
        }

        /// <summary>
        /// Retrieves all mailbox dump requests.
        /// </summary>
        /// <returns>All mailbox dump requests for the domain</returns>
        public GenericFeed<MailboxDumpRequest> RetrieveAllMailboxDumpRequests() {
            return RetrieveAllMailboxDumpRequests(null);
        }

        /// <summary>
        /// Removes the mailbox dump request for the given ID and user.
        /// </summary>
        /// <param name="sourceUser">The user whose dump requests need to be deleted</param>
        /// <param name="requestId">The id of the mailbox dump request to be deleted</param>
        public void DeleteMailboxDumpRequest(String sourceUser, String requestId) {
            Uri requestUri = new Uri(String.Format("{0}/{1}/{2}/{3}",
                                                   baseMailboxDumpUri,
                                                   domain,
                                                   sourceUser,
                                                   requestId));

            Delete(requestUri);
        }

        /// <summary>
        /// Creates a new Account Information request. When completed, the account info is available for download.
        /// </summary>
        /// <param name="sourceUser">The user whose account information is to be audited</param>
        /// <returns>The entry being created</returns>
        public AccountInfo CreateAccountInfoRequest(String sourceUser) {
            Uri requestUri = new Uri(String.Format("{0}/{1}/{2}/{3}",
                                                   AuditNameTable.AuditBaseFeedUri,
                                                   AuditNameTable.account,
                                                   domain,
                                                   sourceUser));

            // The request wants an empty body, but we need to add some dummy content which will be ignored
            AccountInfo dummy = new AccountInfo();
            dummy.addOrUpdatePropertyValue("dummy", "");

            return Insert(requestUri, dummy) as AccountInfo;
        }

        /// <summary>
        /// Retrieves a previously created account/services related information request for the given user.
        /// </summary>
        /// <param name="sourceUser">The user whose account info needs to be retrieved</param>
        /// <param name="requestId">The id of the account info request</param>
        /// <returns>The details of the mailbox dump request</returns>
        public AccountInfo RetrieveAccountInfoRequest(String sourceUser, String requestId) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}",
                                              baseAccountInfoUri,
                                              domain,
                                              sourceUser,
                                              requestId);

            return Get(requestUri) as AccountInfo;
        }

        /// <summary>
        /// Retrieves all account info requests.
        /// </summary>
        /// <param name="fromDate">The starting date for the account info requests to be retrieved</param>
        /// <returns>All account info requests for the domain</returns>
        public GenericFeed<AccountInfo> RetrieveAllAccountInfoRequests(DateTime? fromDate) {
            String requestUri = String.Format("{0}/{1}",
                                              baseAccountInfoUri,
                                              domain);

            if (fromDate != null) {
                String date = HttpUtility.HtmlEncode(((DateTime)fromDate).ToString(AuditNameTable.dateFormat));
                requestUri = String.Format("{0}?{1}={2}",
                                           requestUri,
                                           AuditNameTable.fromDate,
                                           date);
            }

            return QueryExtendedFeed(new Uri(requestUri), true) as GenericFeed<AccountInfo>;
        }

        /// <summary>
        /// Retrieves all account info requests.
        /// </summary>
        /// <returns>All account info requests for the domain</returns>
        public GenericFeed<AccountInfo> RetrieveAllAccountInfoRequests() {
            return RetrieveAllAccountInfoRequests(null);
        }

        /// <summary>
        /// Removes an account info request for the given user.
        /// </summary>
        /// <param name="sourceUser">The user whose account info request needs to be deleted</param>
        /// <param name="requestId">The id of the mailbox dump request to be deleted</param>
        public void DeleteAccountInfoRequest(String sourceUser, String requestId) {
            Uri requestUri = new Uri(String.Format("{0}/{1}/{2}/{3}",
                                                   baseAccountInfoUri,
                                                   domain,
                                                   sourceUser,
                                                   requestId));

            Delete(requestUri);
        }

        /// <summary>
        /// Feed handler. Instantiates a new <code>GenericFeed</code>.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e"><code>ServiceEventArgs</code>, holds the feed</param>
        protected void OnParsedNewFeed(object sender, ServiceEventArgs e) {
            if (e == null) {
                throw new ArgumentNullException("e");
            }

            e.Feed = getFeed(e.Uri, e.Service);
        }

        protected override AppsExtendedFeed getFeed(Uri uri, IService service) {
            String baseUri = uri.ToString();
            if (baseUri.StartsWith(baseMailMonitorUri)) {
                return new GenericFeed<MailMonitor>(uri, service);
            } else if (baseUri.StartsWith(baseMailboxDumpUri)) {
                return new GenericFeed<MailboxDumpRequest>(uri, service);
            } else if (baseUri.StartsWith(baseAccountInfoUri)) {
                return new GenericFeed<AccountInfo>(uri, service);
            } else {
                return new AppsExtendedFeed(uri, this);
            }
        }
    }
}
