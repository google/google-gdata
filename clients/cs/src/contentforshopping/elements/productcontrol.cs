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
using Google.GData.Extensions.AppControl;
using Google.GData.Client;

namespace Google.GData.ContentForShopping.Elements {
    public class ProductControl : AppControl {
        /// <summary>
        /// Status collection
        /// </summary>
        private ExtensionCollection<Status> statusList;

        /// <summary>
        /// default constructor for app:control in the Content for Shopping API
        /// </summary>
        public ProductControl()
            : base(BaseNameTable.NSAppPublishingFinal) {
            this.ExtensionFactories.Add(new RequiredDestination());
            this.ExtensionFactories.Add(new ExcludedDestination());
            this.ExtensionFactories.Add(new Warnings());
            this.ExtensionFactories.Add(new Status());
        }

        /// <summary>
        /// returns the sc:required_destination element
        /// </summary>
        public RequiredDestination RequiredDestination {
            get {
                return FindExtension(ContentForShoppingNameTable.RequiredDestination,
                    ContentForShoppingNameTable.BaseNamespace) as RequiredDestination;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.RequiredDestination,
                    ContentForShoppingNameTable.BaseNamespace,
                    value);
            }
        }

        /// <summary>
        /// returns the sc:excluded_destination element
        /// </summary>
        public ExcludedDestination ExcludedDestination {
            get {
                return FindExtension(ContentForShoppingNameTable.ExcludedDestination,
                    ContentForShoppingNameTable.BaseNamespace) as ExcludedDestination;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.ExcludedDestination,
                    ContentForShoppingNameTable.BaseNamespace,
                    value);
            }
        }

        /// <summary>
        /// returns the sc:warnings element
        /// </summary>
        public Warnings Warnings {
            get {
                return FindExtension(ContentForShoppingNameTable.Warnings,
                    ContentForShoppingNameTable.BaseNamespace) as Warnings;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.Warnings,
                    ContentForShoppingNameTable.BaseNamespace,
                    value);
            }
        }

        /// <summary>
        /// Status collection.
        /// </summary>
        public ExtensionCollection<Status> StatusList {
            get {
                if (this.statusList == null) {
                    this.statusList = new ExtensionCollection<Status>(this);
                }
                return this.statusList;
            }
        }

        /// <summary>
        /// we need to ignore version changes because the namespace is hard-coded
        /// </summary>
        protected override void VersionInfoChanged() { }
    }
}
