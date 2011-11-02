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
    /// A Google Apps Member entry.
    /// </summary>
    public class MemberEntry : AppsExtendedEntry {
        /// <summary>
        /// Constructs a new <code>MemberEntry</code> object.
        /// </summary>
        public MemberEntry()
            : base() {
        }

        /// <summary>
        /// MemberId Property accessor
        /// </summary>
        public string MemberId {
            get {
                PropertyElement property = this.getPropertyByName(AppsGroupsNameTable.memberId);
                return property != null ? property.Value : null;
            }
            set {
                this.addOrUpdatePropertyValue(AppsGroupsNameTable.memberId, value);
            }
        }

        /// <summary>
        /// MemberType Property accessor
        /// </summary>
        public string MemberType {
            get {
                PropertyElement property = this.getPropertyByName(AppsGroupsNameTable.memberType);
                return property != null ? property.Value : null;
            }
            set {
                this.addOrUpdatePropertyValue(AppsGroupsNameTable.memberType, value);
            }
        }

        /// <summary>
        /// DirectMember Property accessor
        /// </summary>
        public bool DirectMember {
            get {
                PropertyElement property = this.getPropertyByName(AppsGroupsNameTable.directMember);
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
                this.addOrUpdatePropertyValue(AppsGroupsNameTable.directMember, value.ToString());
            }
        }
    }
}
