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
    /// A Google Apps Owner entry.
    /// </summary>
    public class OwnerEntry : AppsExtendedEntry {
        /// <summary>
        /// Constructs a new <code>OwnerEntry</code> object.
        /// </summary>
        public OwnerEntry()
            : base() {
        }

        /// <summary>
        /// Email Property accessor
        /// </summary>
        public string Email {
            get {
                PropertyElement property = this.getPropertyByName(AppsGroupsNameTable.email);
                return property != null ? property.Value : null;
            }
            set {
                this.addOrUpdatePropertyValue(AppsGroupsNameTable.email, value);
            }
        }
    }
}
