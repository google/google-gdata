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
using Google.GData.Client;

namespace Google.GData.ContentForShopping.Elements
{
    public class Issue : SimpleContainer
    {
        /// <summary>
        /// ExampleItem collection
        /// </summary>
        private ExtensionCollection<ExampleItem> exampleItems;

        /// <summary>
        /// default constructor for sc:issue
        /// </summary>
        public Issue()
            : base(ContentForShoppingNameTable.Issue,
                ContentForShoppingNameTable.scDataPrefix,
                ContentForShoppingNameTable.BaseNamespace)
        {
            this.ExtensionFactories.Add(new ExampleItem());
        }

        /// <summary>
        /// id property accessor
        /// </summary>
        public string Id
        {
            get
            {
                return Convert.ToString(Attributes[AtomParserNameTable.XmlIdElement]);
            }
            set
            {
                Attributes[AtomParserNameTable.XmlIdElement] = value;
            }
        }

        /// <summary>
        /// Last checked property accessor
        /// </summary>
        public string LastChecked
        {
            get
            {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.LastChecked]);
            }
            set
            {
                Attributes[ContentForShoppingNameTable.LastChecked] = value;
            }
        }

        /// <summary>
        /// Num items property accessor
        /// </summary>
        public string NumItems
        {
            get
            {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.NumItems]);
            }
            set
            {
                Attributes[ContentForShoppingNameTable.NumItems] = value;
            }
        }

        /// <summary>
        /// Offending term property accessor
        /// </summary>
        public string OffendingTerm
        {
            get
            {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.OffendingTerm]);
            }
            set
            {
                Attributes[ContentForShoppingNameTable.OffendingTerm] = value;
            }
        }

        /// <summary>
        /// ExampleItem collection.
        /// </summary>
        public ExtensionCollection<ExampleItem> ExampleItems {
            get {
                if (this.exampleItems == null) {
                    this.exampleItems = new ExtensionCollection<ExampleItem>(this);
                }
                return this.exampleItems;
            }
        }
    }
}
