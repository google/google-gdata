/* Copyright (c) 2006 Google Inc.
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
using Google.GData.Client;
using Google.GData.Extensions;
using System.Globalization;
using Google.GData.ContentForShopping.Elements;
using Google.GData.Extensions.AppControl;

namespace Google.GData.ContentForShopping {
    /// <summary>
    /// Feed API customization class for defining Product feed.
    /// </summary>
    public class UsersEntry : AbstractEntry {
        /// <summary>
        /// Permission collection
        /// </summary>
        private ExtensionCollection<Permission> permissions;

        public UsersEntry()
            : base() {
            this.AddExtension(new Admin());
            this.AddExtension(new Permission());
        }

        /// <summary>
        /// Admin
        /// </summary>
        public bool Admin {
            get {
                bool value;
                if (!bool.TryParse(GetStringValue<Admin>(ContentForShoppingNameTable.Admin,
                    ContentForShoppingNameTable.BaseNamespace), out value)) {
                    value = false;
                }

                return value;
            }
            set {
                SetStringValue<Admin>(value.ToString().ToLower(),
                    ContentForShoppingNameTable.Admin,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Permission collection.
        /// </summary>
        public ExtensionCollection<Permission> Permissions {
            get {
                if (this.permissions == null) {
                    this.permissions = new ExtensionCollection<Permission>(this);
                }
                return this.permissions;
            }
        }
    }
}
