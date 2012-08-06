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

namespace Google.GData.ContentForShopping.Elements
{
    public class AdwordsAccount : SimpleElement
    {
        /// <summary>
        /// default constructor for sc:adwords_account
        /// </summary>
        public AdwordsAccount()
            : base(ContentForShoppingNameTable.AdwordsAccount,
               ContentForShoppingNameTable.scDataPrefix,
               ContentForShoppingNameTable.BaseNamespace)
        {
        }

        /// <summary>
        /// Constructs a new AdwordsAccount instance with the specified values.
        /// </summary>
        /// <param name="unit">The status of the Adwords Account (active or inactive).</param>
        /// <param name="value">The Adwords Account ID.</param>
        public AdwordsAccount(string status, string value)
            : base(ContentForShoppingNameTable.AdwordsAccount,
               ContentForShoppingNameTable.scDataPrefix,
               ContentForShoppingNameTable.BaseNamespace)
        {
            this.Status = status;
            this.Value = value;
        }

        /// <summary>
        /// Status property accessor
        /// </summary>
        public string Status
        {
            get
            {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Status]);
            }
            set
            {
                Attributes[ContentForShoppingNameTable.Status] = value;
            }
        }
    }
}
