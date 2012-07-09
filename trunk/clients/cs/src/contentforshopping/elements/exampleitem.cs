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
    public class ExampleItem : SimpleContainer
    {
        /// <summary>
        /// default constructor for sc:example_item
        /// </summary>
        public ExampleItem()
            : base(ContentForShoppingNameTable.ExampleItem,
                ContentForShoppingNameTable.scDataPrefix,
                ContentForShoppingNameTable.BaseNamespace)
        {
            this.ExtensionFactories.Add(new ItemId());
            this.ExtensionFactories.Add(new ExampleItemLink());
            this.ExtensionFactories.Add(new ExampleItemTitle());
            this.ExtensionFactories.Add(new SubmittedValue());
            this.ExtensionFactories.Add(new ValueOnLandingPage());
        }

        /// <summary>
        /// returns the sc:item_id element
        /// </summary>
        public string ItemId
        {
            get
            {
                return GetStringValue<ItemId>(ContentForShoppingNameTable.ItemId,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set
            {
                SetStringValue<ItemId>(value.ToString(),
                    ContentForShoppingNameTable.ItemId,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// returns the sc:link element
        /// </summary>
        public string ExampleItemLink
        {
            get
            {
                return GetStringValue<ExampleItemLink>(AtomParserNameTable.XmlLinkElement,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set
            {
                SetStringValue<ExampleItemLink>(value.ToString(),
                    AtomParserNameTable.XmlLinkElement,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// returns the sc:title element
        /// </summary>
        public string ExampleItemTitle
        {
            get
            {
                return GetStringValue<ExampleItemTitle>(AtomParserNameTable.XmlLinkElement,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set
            {
                SetStringValue<ExampleItemTitle>(value.ToString(),
                    AtomParserNameTable.XmlLinkElement,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// returns the sc:submitted_value element
        /// </summary>
        public string SubmittedValue
        {
            get
            {
                return GetStringValue<SubmittedValue>(ContentForShoppingNameTable.SubmittedValue,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set
            {
                SetStringValue<SubmittedValue>(value.ToString(),
                    ContentForShoppingNameTable.SubmittedValue,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// returns the sc:value_on_landing_page element
        /// </summary>
        public string ValueOnLandingPage
        {
            get
            {
                return GetStringValue<ValueOnLandingPage>(ContentForShoppingNameTable.ValueOnLandingPage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set
            {
                SetStringValue<ValueOnLandingPage>(value.ToString(),
                    ContentForShoppingNameTable.ValueOnLandingPage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }
    }
}
