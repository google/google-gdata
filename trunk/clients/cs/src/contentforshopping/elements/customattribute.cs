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
    public class CustomAttribute : SimpleElement
    {
        /// <summary>
        /// default constructor for sc:attribute
        /// </summary>
        public CustomAttribute()
            : base(ContentForShoppingNameTable.Attribute,
               ContentForShoppingNameTable.scDataPrefix,
               ContentForShoppingNameTable.BaseNamespace)
        {
        }

        /// <summary>
        /// default constructor for sc:attribute with an initial value
        /// </summary>
        /// <param name="name">The attribute's name.</param>
        /// <param name="value">The attribute's initial value.</param>
        public CustomAttribute(string name, string value)
            : base(ContentForShoppingNameTable.Attribute,
                ContentForShoppingNameTable.scDataPrefix,
                ContentForShoppingNameTable.BaseNamespace,
                value) {
            this.Name = name;
        }

        /// <summary>
        /// Constructs a new CustomAttribute instance with the specified values.
        /// </summary>
        /// <param name="name">The attribute's name.</param>
        /// <param name="value">The attribute's initial value.</param>
        /// <param name="type">The attribute's type.</param>
        /// <param name="unit">The attribute's unit.</param>
        public CustomAttribute(string name, string value, string type, string unit)
            : base(ContentForShoppingNameTable.Attribute,
               ContentForShoppingNameTable.scDataPrefix,
               ContentForShoppingNameTable.BaseNamespace,
               value)
        {
            this.Name = name;
            this.Type = type;
            this.Unit = unit;
        }

        /// <summary>
        /// Name property accessor
        /// </summary>
        public string Name
        {
            get
            {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Name]);
            }
            set
            {
                Attributes[ContentForShoppingNameTable.Name] = value;
            }
        }

        /// <summary>
        /// Type property accessor
        /// </summary>
        public string Type {
            get {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Type]);
            }
            set {
                Attributes[ContentForShoppingNameTable.Type] = value;
            }
        }

        /// <summary>
        /// Unit property accessor
        /// </summary>
        public string Unit {
            get {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Unit]);
            }
            set {
                Attributes[ContentForShoppingNameTable.Unit] = value;
            }
        }
    }
}
