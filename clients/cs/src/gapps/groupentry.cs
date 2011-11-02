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
    /// A Google Apps Group entry.
    /// </summary>
    public class GroupEntry : AppsExtendedEntry {
        /// <summary>
        /// Constructs a new <code>GroupEntry</code> object.
        /// </summary>
        public GroupEntry()
            : base() {
        }

        /// <summary>
        /// GroupId Property accessor
        /// </summary>
        public string GroupId {
            get {
                PropertyElement property = this.getPropertyByName(AppsGroupsNameTable.groupId);
                return property != null ? property.Value : null;
            }
            set {
                this.addOrUpdatePropertyValue(AppsGroupsNameTable.groupId, value);
            }
        }

        /// <summary>
        /// GroupName Property accessor
        /// </summary>
        public string GroupName {
            get {
                PropertyElement property = this.getPropertyByName(AppsGroupsNameTable.groupName);
                return property != null ? property.Value : null;
            }
            set {
                this.addOrUpdatePropertyValue(AppsGroupsNameTable.groupName, value);
            }
        }

        /// <summary>
        /// Description Property accessor
        /// </summary>
        public string Description {
            get {
                PropertyElement property = this.getPropertyByName(AppsGroupsNameTable.description);
                return property != null ? property.Value : null;
            }
            set {
                this.addOrUpdatePropertyValue(AppsGroupsNameTable.description, value);
            }
        }

        /// <summary>
        /// EmailPermission Property accessor
        /// </summary>
        public PermissionLevel? EmailPermission {
            get {
                PropertyElement property = this.getPropertyByName(AppsGroupsNameTable.emailPermission);
                if (property == null || property.Value == null) {
                    return null;
                }

                return (PermissionLevel)Enum.Parse(typeof(PermissionLevel), property.Value, true);
            }
            set {
                this.addOrUpdatePropertyValue(AppsGroupsNameTable.emailPermission, value.ToString());
            }
        }
    }
}
