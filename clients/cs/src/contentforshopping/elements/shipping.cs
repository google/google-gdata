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
    public class Shipping : SimpleContainer
    {
        /// <summary>
        /// default constructor for scp:shipping
        /// </summary>
        public Shipping()
            : base(ContentForShoppingNameTable.Shipping,
                ContentForShoppingNameTable.scpDataPrefix,
                ContentForShoppingNameTable.BaseNamespace)
        {
            this.ExtensionFactories.Add(new ShippingCountry());
            this.ExtensionFactories.Add(new ShippingRegion());
            this.ExtensionFactories.Add(new ShippingService());
            this.ExtensionFactories.Add(new ShippingPrice());
        }

        /// <summary>
        /// returns the scp:shipping_country element
        /// </summary>
        public string Country
        {
            get
            {
                return GetStringValue<ShippingCountry>(ContentForShoppingNameTable.ShippingCountry,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set
            {
                SetStringValue<ShippingCountry>(value.ToString(),
                    ContentForShoppingNameTable.ShippingCountry,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// returns the scp:shipping_region element
        /// </summary>
        public string Region
        {
            get
            {
                return GetStringValue<ShippingRegion>(ContentForShoppingNameTable.ShippingRegion,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set
            {
                SetStringValue<ShippingRegion>(value.ToString(),
                    ContentForShoppingNameTable.ShippingRegion,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// returns the scp:shipping_service element
        /// </summary>
        public string Service
        {
            get
            {
                return GetStringValue<ShippingService>(ContentForShoppingNameTable.ShippingService,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set
            {
                SetStringValue<ShippingService>(value.ToString(),
                    ContentForShoppingNameTable.ShippingService,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// returns the scp:shipping_price element
        /// </summary>
        public ShippingPrice Price
        {
            get
            {
                return FindExtension(ContentForShoppingNameTable.ShippingPrice,
                                     ContentForShoppingNameTable.ProductsNamespace) as ShippingPrice;
            }
            set
            {
                ReplaceExtension(ContentForShoppingNameTable.ShippingPrice,
                                ContentForShoppingNameTable.ProductsNamespace,
                                value);
            }
        }
    }
}
