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
    public class Permission : SimpleElement
    {
        /// <summary>
        /// default constructor for sc:permission
        /// </summary>
        public Permission()
            : base(ContentForShoppingNameTable.Permission,
               ContentForShoppingNameTable.scDataPrefix,
               ContentForShoppingNameTable.BaseNamespace)
        {
        }

        /// <summary>
        /// constructor that takes status for sc:permission
        /// </summary>
        /// <param name="scope">The permission's scope.</param>
        /// <param name="value">The permission's value.</param>
        public Permission(string scope, string value)
            : base(ContentForShoppingNameTable.Permission,
               ContentForShoppingNameTable.scDataPrefix,
               ContentForShoppingNameTable.BaseNamespace)
        {
            this.Scope = scope;
            this.Value = value;
        }

        /// <summary>
        /// Scope property accessor
        /// </summary>
        public string Scope
        {
            get
            {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Scope]);
            }
            set
            {
                Attributes[ContentForShoppingNameTable.Scope] = value;
            }
        }
    }
}
