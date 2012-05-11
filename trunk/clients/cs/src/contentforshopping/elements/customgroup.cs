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
    public class CustomGroup : SimpleContainer
    {
        /// <summary>
        /// CustomAttribute collection
        /// </summary>
        private ExtensionCollection<CustomAttribute> attributes;

        /// <summary>
        /// default constructor for sc:group
        /// </summary>
        public CustomGroup()
            : base(ContentForShoppingNameTable.Group,
               ContentForShoppingNameTable.scDataPrefix,
               ContentForShoppingNameTable.BaseNamespace)
        {
        }

        /// <summary>
        /// default constructor for sc:group with attributes
        /// </summary>
        /// <param name="attributes">The list of attributes for the group.</param>
        public CustomGroup(ExtensionCollection<CustomAttribute> attributes)
            : base(ContentForShoppingNameTable.Group,
                ContentForShoppingNameTable.scDataPrefix,
                ContentForShoppingNameTable.BaseNamespace) {
            this.attributes = attributes;
        }

        /// <summary>
        /// Attributes property accessor
        /// </summary>
        public ExtensionCollection<CustomAttribute> Attributes {
            get {
                if (this.attributes == null) {
                    this.attributes = new ExtensionCollection<CustomAttribute>(this);
                }
                return this.attributes;
            }
        }
    }
}
