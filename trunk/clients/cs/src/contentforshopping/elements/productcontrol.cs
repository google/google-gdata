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
        /// Required destination collection
        /// </summary>
        private ExtensionCollection<RequiredDestination> requiredDestinations;

        /// <summary>
        /// Validate destination collection
        /// </summary>
        private ExtensionCollection<ValidateDestination> validateDestinations;

        /// <summary>
        /// Excluded destination collection
        /// </summary>
        private ExtensionCollection<ExcludedDestination> excludedDestinations;

        /// <summary>
        /// default constructor for app:control in the Content for Shopping API
        /// </summary>
        public ProductControl()
            : base(BaseNameTable.NSAppPublishingFinal) {
            this.ExtensionFactories.Add(new RequiredDestination());
            this.ExtensionFactories.Add(new ValidateDestination());
            this.ExtensionFactories.Add(new ExcludedDestination());
            this.ExtensionFactories.Add(new Warnings());
            this.ExtensionFactories.Add(new Status());
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
        /// Returns the sc:required_destination elements
        /// </summary>
        public ExtensionCollection<RequiredDestination> RequiredDestinations {
            get {
                if (this.requiredDestinations == null) {
                    this.requiredDestinations = new ExtensionCollection<RequiredDestination>(this);
                }
                return this.requiredDestinations;
            }
        }

        /// <summary>
        /// Returns the sc:validate_destination elements
        /// </summary>
        public ExtensionCollection<ValidateDestination> ValidateDestinations {
            get {
                if (this.validateDestinations == null) {
                    this.validateDestinations = new ExtensionCollection<ValidateDestination>(this);
                }
                return this.validateDestinations;
            }
        }

        /// <summary>
        /// Returns the sc:excluded_destination elements
        /// </summary>
        public ExtensionCollection<ExcludedDestination> ExcludedDestinations {
            get {
                if (this.excludedDestinations == null) {
                    this.excludedDestinations = new ExtensionCollection<ExcludedDestination>(this);
                }
                return this.excludedDestinations;
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
