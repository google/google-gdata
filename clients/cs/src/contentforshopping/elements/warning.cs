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
using Google.GData.Extensions;

namespace Google.GData.ContentForShopping.Elements {
    public class Warning : SimpleContainer {
        /// <summary>
        /// default constructor for sc:warning
        /// </summary>
        public Warning()
            : base(ContentForShoppingNameTable.Warning,
            ContentForShoppingNameTable.scDataPrefix,
            ContentForShoppingNameTable.BaseNamespace) {
            this.ExtensionFactories.Add(new WarningCode());
            this.ExtensionFactories.Add(new WarningDomain());
            this.ExtensionFactories.Add(new WarningLocation());
            this.ExtensionFactories.Add(new WarningMessage());
        }

        /// <summary>
        /// returns the sc:code element
        /// </summary>
        public string Code {
            get {
                return GetStringValue<WarningCode>(ContentForShoppingNameTable.WarningCode,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<WarningCode>(value.ToString(),
                    ContentForShoppingNameTable.WarningCode,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// returns the sc:domain element
        /// </summary>
        public string Domain {
            get {
                return GetStringValue<WarningDomain>(ContentForShoppingNameTable.WarningDomain,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<WarningDomain>(value.ToString(),
                    ContentForShoppingNameTable.WarningDomain,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// returns the sc:location element
        /// </summary>
        public string Location {
            get {
                return GetStringValue<WarningLocation>(ContentForShoppingNameTable.WarningLocation,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<WarningLocation>(value.ToString(),
                    ContentForShoppingNameTable.WarningLocation,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// returns the sc:message element
        /// </summary>
        public string Message {
            get {
                return GetStringValue<WarningMessage>(ContentForShoppingNameTable.WarningMessage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<WarningMessage>(value.ToString(),
                    ContentForShoppingNameTable.WarningMessage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }
    }
}
