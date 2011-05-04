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
    /// A Google Apps Email Monitor entry.
    /// </summary>
    public class MailMonitor : AppsExtendedEntry {
        /// <summary>
        /// Constructs a new <code>MailMonitor</code> object.
        /// </summary>
        public MailMonitor()
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
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.requestId, value);
            }
        }

        /// <summary>
        /// DestinationUserName Property accessor
        /// </summary>
        public string DestinationUserName {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.destUserName);
                return property != null ? property.Value : null;
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.destUserName, value);
            }
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
                return DateTime.Now;
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
        /// IncomingEmailMonitorLevel Property accessor
        /// </summary>
        public MonitorLevel IncomingEmailMonitorLevel {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.incomingEmailMonitorLevel);
                if (property == null || property.Value == null) {
                    return MonitorLevel.FULL_MESSAGE;
                }

                return (MonitorLevel)Enum.Parse(typeof(MonitorLevel), property.Value, true);
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.incomingEmailMonitorLevel, value.ToString());
            }
        }

        /// <summary>
        /// OutgoingEmailMonitorLevel Property accessor
        /// </summary>
        public MonitorLevel OutgoingEmailMonitorLevel {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.outgoingEmailMonitorLevel);
                if (property == null || property.Value == null) {
                    return MonitorLevel.FULL_MESSAGE;
                }

                return (MonitorLevel)Enum.Parse(typeof(MonitorLevel), property.Value, true);
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.outgoingEmailMonitorLevel, value.ToString());
            }
        }

        /// <summary>
        /// DraftMonitorLevel Property accessor
        /// </summary>
        public MonitorLevel? DraftMonitorLevel {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.draftMonitorLevel);
                if (property == null || property.Value == null) {
                    return null;
                }

                return (MonitorLevel)Enum.Parse(typeof(MonitorLevel), property.Value, true);
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.draftMonitorLevel, value.ToString());
            }
        }

        /// <summary>
        /// ChatMonitorLevel Property accessor
        /// </summary>
        public MonitorLevel? ChatMonitorLevel {
            get {
                PropertyElement property = this.getPropertyByName(AuditNameTable.chatMonitorLevel);
                if (property == null || property.Value == null) {
                    return null;
                }

                return (MonitorLevel)Enum.Parse(typeof(MonitorLevel), property.Value, true);
            }
            set {
                this.addOrUpdatePropertyValue(AuditNameTable.chatMonitorLevel, value.ToString());
            }
        }
    }
}
