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
    /// Feed API customization class for defining managed accounts entry.
    /// </summary>
    public class ManagedAccountsEntry : AbstractEntry {
        public ManagedAccountsEntry()
            : base() {
            this.AddExtension(new AdultContent());
            this.AddExtension(new InternalId());
            this.AddExtension(new ReviewsUrl());
            this.AddExtension(new AccountStatus());
            this.AddExtension(new AdwordsAccounts());
        }

        /// <summary>
        /// AdultContent.
        /// </summary>
        public string AdultContent {
            get {
                return GetStringValue<AdultContent>(ContentForShoppingNameTable.AdultContent,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<AdultContent>(value,
                    ContentForShoppingNameTable.AdultContent,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// InternalId.
        /// </summary>
        public string InternalId {
            get {
                return GetStringValue<InternalId>(ContentForShoppingNameTable.InternalId,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<InternalId>(value,
                    ContentForShoppingNameTable.InternalId,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// ReviewsUrl.
        /// </summary>
        public string ReviewsUrl {
            get {
                return GetStringValue<ReviewsUrl>(ContentForShoppingNameTable.ReviewsUrl,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<ReviewsUrl>(value,
                    ContentForShoppingNameTable.ReviewsUrl,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// AccountStatus.
        /// </summary>
        public string AccountStatus {
            get {
                return GetStringValue<AccountStatus>(ContentForShoppingNameTable.AccountStatus,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<AccountStatus>(value,
                    ContentForShoppingNameTable.AccountStatus,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }
                /// <summary>
        /// AdwordsAccounts.
        /// </summary>        public AdwordsAccounts AdwordsAccounts {            get {                return FindExtension(ContentForShoppingNameTable.AdwordsAccounts,                    ContentForShoppingNameTable.BaseNamespace) as AdwordsAccounts;            }            set {                ReplaceExtension(ContentForShoppingNameTable.AdwordsAccounts,                    ContentForShoppingNameTable.BaseNamespace,                    value);            }        }
    }
}
