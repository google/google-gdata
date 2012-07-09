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
    public class FileFormat : SimpleContainer
    {
        /// <summary>
        /// default constructor for sc:file_format
        /// </summary>
        public FileFormat()
            : base(ContentForShoppingNameTable.FileFormat,
                ContentForShoppingNameTable.scDataPrefix,
                ContentForShoppingNameTable.BaseNamespace)
        {
            this.ExtensionFactories.Add(new Delimiter());
            this.ExtensionFactories.Add(new Encoding());
            this.ExtensionFactories.Add(new UseQuotedFields());
        }

        /// <summary>
        /// Format property accessor
        /// </summary>
        public string Format
        {
            get
            {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Format]);
            }
            set
            {
                Attributes[ContentForShoppingNameTable.Format] = value;
            }
        }

        /// <summary>
        /// returns the sc:delimiter element
        /// </summary>
        public string Delimiter
        {
            get
            {
                return GetStringValue<Delimiter>(ContentForShoppingNameTable.Delimiter,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set
            {
                SetStringValue<Delimiter>(value.ToString(),
                    ContentForShoppingNameTable.Delimiter,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// returns the sc:encoding element
        /// </summary>
        public string Encoding
        {
            get
            {
                return GetStringValue<Encoding>(ContentForShoppingNameTable.Encoding,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set
            {
                SetStringValue<Encoding>(value.ToString(),
                    ContentForShoppingNameTable.Encoding,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// returns the sc:use_quoted_fields element
        /// </summary>
        public string UseQuotedFields
        {
            get
            {
                return GetStringValue<UseQuotedFields>(ContentForShoppingNameTable.UseQuotedFields,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set
            {
                SetStringValue<UseQuotedFields>(value.ToString(),
                    ContentForShoppingNameTable.UseQuotedFields,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }
    }
}
