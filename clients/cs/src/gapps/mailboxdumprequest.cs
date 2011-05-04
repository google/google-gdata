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
using System.Xml;
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps {
    /// <summary>
    /// A Google Apps Mailbox Dump Request entry.
    /// </summary>
    public class MailboxDumpRequest : AppsExtendedEntry {
        /// <summary>
        /// Constructs a new <code>MailboxDumpRequest</code> object.
        /// </summary>
        public MailboxDumpRequest()
            : base() {
        }

        /// <summary>
        /// BeginDate Property accessor
        /// </summary>
        public DateTime BeginDate {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.beginDate);
                DateTime dt;
                if (property != null && DateTime.TryParse(property.Value, out dt)) {
                    return dt;
                }
                return DateTime.MinValue;
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.beginDate, value.ToString(AuditNameTable.dateFormat));
            }
        }

        /// <summary>
        /// EndDate Property accessor
        /// </summary>
        public DateTime EndDate {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.endDate);
                DateTime dt;
                if (property != null && DateTime.TryParse(property.Value, out dt)) {
                    return dt;
                }
                return DateTime.Now;
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.endDate, value.ToString(AuditNameTable.dateFormat));
            }
        }

        /// <summary>
        /// SearchQuery Property accessor
        /// </summary>
        public string SearchQuery {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.searchQuery);
                return property != null ? property.Value : null;
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.searchQuery, value);
            }
        }

        /// <summary>
        /// IncludeDeleted Property accessor
        /// </summary>
        public bool IncludeDeleted {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.includeDeleted);
                if (property == null || property.Value == null) {
                    return false;
                }

                bool value;
                if (!bool.TryParse(property.Value, out value)) {
                    value = false;
                }

                return value;
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.includeDeleted, value.ToString());
            }
        }
       
        /// <summary>
        /// PackageContent Property accessor
        /// </summary>
        public MonitorLevel PackageContent {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.packageContent);
                if (property == null || property.Value == null) {
                    return MonitorLevel.FULL_MESSAGE;
                }

                return (MonitorLevel)Enum.Parse(typeof(MonitorLevel), property.Value, true);
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.packageContent, value.ToString());
            }
        }

        /// <summary>
        /// Status Property accessor
        /// </summary>
        public RequestStatus Status {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.status);
                if (property == null || property.Value == null) {
                    return RequestStatus.PENDING;
                }

                return (RequestStatus)Enum.Parse(typeof(RequestStatus), property.Value, true);
            }
            internal set {
                this.addOrUpdatePropertyValue(AuditNameTable.status, value.ToString());
            }
        }

        /// <summary>
        /// RequestId Property accessor
        /// </summary>
        public string RequestId {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.requestId);
                return property != null ? property.Value : null;
            }
            internal set {
                this.addOrUpdatePropertyValue(AuditNameTable.requestId, value);
            }
        }

        /// <summary>
        /// UserEmailAddress Property accessor
        /// </summary>
        public string UserEmailAddress {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.userEmailAddress);
                return property != null ? property.Value : null;
            }
            internal set {
                this.addOrUpdatePropertyValue(AuditNameTable.userEmailAddress, value);
            }
        }

        /// <summary>
        /// AdminEmailAddress Property accessor
        /// </summary>
        public string AdminEmailAddress {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.adminEmailAddress);
                return property != null ? property.Value : null;
            }
            internal set {
                this.addOrUpdatePropertyValue(AuditNameTable.adminEmailAddress, value);
            }
        }

        /// <summary>
        /// RequestDate Property accessor
        /// </summary>
        public DateTime RequestDate {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.requestDate);
                DateTime dt;
                if (property != null && DateTime.TryParse(property.Value, out dt)) {
                    return dt;
                }
                return DateTime.Now;
            }
            internal set {
                this.addOrUpdatePropertyValue(AuditNameTable.requestDate, value.ToString(AuditNameTable.dateFormat));
            }
        }

        /// <summary>
        /// NumberOfFiles Property accessor
        /// </summary>
        public int NumberOfFiles {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.numberOfFiles);
                if (property == null || property.Value == null) {
                    return 0;
                }

                return Convert.ToInt32(property.Value);
            }
            internal set {
                this.addOrUpdatePropertyValue(AuditNameTable.numberOfFiles, value.ToString());
            }
        }

        /// <summary>
        /// Returns the url of file at the given position in the results
        /// </summary>
        /// <param name="index">The index of the url to be returned</param>
        /// <returns>The url of account info results</returns>
        public String getFileDownloadUrl(int index) {
            return getPropertyValueByName(String.Format("{0}{1}",
                                                        AuditNameTable.fileUrl,
                                                        index));
        }
    }
}
