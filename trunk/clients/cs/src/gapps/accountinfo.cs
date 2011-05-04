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
    /// A Google Apps Account Info entry.
    /// </summary>
    public class AccountInfo : AppsExtendedEntry {
        /// <summary>
        /// Constructs a new <code>AccountInfo</code> object.
        /// </summary>
        public AccountInfo()
            : base() {
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
        /// CompletedDate Property accessor
        /// </summary>
        public DateTime CompletedDate {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.completedDate);
                DateTime dt;
                if (property != null && DateTime.TryParse(property.Value, out dt)) {
                    return dt;
                }
                return DateTime.Now;
            }
            internal set {
                this.addOrUpdatePropertyValue(AuditNameTable.completedDate, value.ToString(AuditNameTable.dateFormat));
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
