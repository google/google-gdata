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
    public class Color : SimpleElement
    {
        /// <summary>
        /// default constructor for scp:color 
        /// </summary>
        public Color()
            : base(ContentForShoppingNameTable.Color,
               ContentForShoppingNameTable.scpDataPrefix,
               ContentForShoppingNameTable.ProductsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new Color instance with the specified value.
        /// </summary>
        /// <param name="value">The color's value.</param>
        public Color(string value)
            : base(ContentForShoppingNameTable.Color,
               ContentForShoppingNameTable.scpDataPrefix,
               ContentForShoppingNameTable.ProductsNamespace)
        {
            this.Value = value;
        }
    }
}
