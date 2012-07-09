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
    public class DatafeedEntry : AbstractEntry {

        public DatafeedEntry()
            : base() {
            this.AddExtension(new AttributeLanguage());
            this.AddExtension(new ContentLanguage());
            this.AddExtension(new FeedFileName());
            this.AddExtension(new FeedType());
            this.AddExtension(new FileFormat());
            this.AddExtension(new TargetCountry());
            this.AddExtension(new ProcessingStatus());
        }

        /// <summary>
        /// Attribute language
        /// </summary>
        public string AttributeLanguage {
            get {
                return GetStringValue<AttributeLanguage>(ContentForShoppingNameTable.AttributeLanguage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<AttributeLanguage>(value,
                    ContentForShoppingNameTable.AttributeLanguage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Content Language.
        /// </summary>
        public string ContentLanguage {
            get {
                return GetStringValue<ContentLanguage>(ContentForShoppingNameTable.ContentLanguage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<ContentLanguage>(value,
                    ContentForShoppingNameTable.ContentLanguage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Feed file name
        /// </summary>
        public string FeedFileName {
            get {
                return GetStringValue<FeedFileName>(ContentForShoppingNameTable.FeedFileName,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<FeedFileName>(value,
                    ContentForShoppingNameTable.FeedFileName,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Feed type
        /// </summary>
        public string FeedType {
            get {
                return GetStringValue<FeedType>(ContentForShoppingNameTable.FeedType,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<FeedType>(value,
                    ContentForShoppingNameTable.FeedType,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// File format
        /// </summary>
        public FileFormat FileFormat {
            get {
                return FindExtension(ContentForShoppingNameTable.FileFormat,
                    ContentForShoppingNameTable.BaseNamespace) as FileFormat;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.FileFormat,
                    ContentForShoppingNameTable.BaseNamespace,
                    value);
            }
        }

        /// <summary>
        /// Target Country.
        /// </summary>
        public string TargetCountry {
            get {
                return GetStringValue<TargetCountry>(ContentForShoppingNameTable.TargetCountry,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<TargetCountry>(value,
                    ContentForShoppingNameTable.TargetCountry,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Processing status
        /// </summary>
        public ProcessingStatus ProcessingStatus {
            get {
                return FindExtension(ContentForShoppingNameTable.ProcessingStatus,
                    ContentForShoppingNameTable.BaseNamespace) as ProcessingStatus;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.ProcessingStatus,
                    ContentForShoppingNameTable.BaseNamespace,
                    value);
            }
        }
    }
}
