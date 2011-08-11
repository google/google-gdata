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
    public class Status : SimpleElement {
        /// <summary>
        /// default constructor for sc:status
        /// </summary>
        public Status()
            : base(ContentForShoppingNameTable.Status,
            ContentForShoppingNameTable.scDataPrefix,
            ContentForShoppingNameTable.BaseNamespace) {
        }

        /// <summary>
        /// Constructs a new Status instance with the specified values.
        /// </summary>
        /// <param name="destination">destination</param>
        /// <param name="status">status</param>
        public Status(string destination, string status)
            : base(ContentForShoppingNameTable.Status,
            ContentForShoppingNameTable.scDataPrefix,
            ContentForShoppingNameTable.BaseNamespace) {
            this.Destination = destination;
            this.Value = status;
        }

        /// <summary>
        /// Destination property accessor
        /// </summary>
        public string Destination {
            get {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Destination]);
            }
            set {
                Attributes[ContentForShoppingNameTable.Destination] = value;
            }
        }

        /// <summary>
        /// Status property accessor
        /// </summary>
        public string Value {
            get {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Status]);
            }
            set {
                Attributes[ContentForShoppingNameTable.Status] = value;
            }
        }
    }
}
