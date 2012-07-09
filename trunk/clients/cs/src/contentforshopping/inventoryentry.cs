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
    /// Feed API customization class for defining inventory feed.
    /// </summary>
    public class InventoryEntry : AbstractEntry {
        public InventoryEntry()
            : base() {
            this.AddExtension(new Availability());
            this.AddExtension(new Price());
            this.AddExtension(new Quantity());
            this.AddExtension(new SalePrice());
            this.AddExtension(new SalePriceEffectiveDate());
        }

        /// <summary>
        /// Availability.
        /// </summary>
        public string Availability {
            get {
                return GetStringValue<Availability>(ContentForShoppingNameTable.Availability,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Availability>(value,
                    ContentForShoppingNameTable.Availability,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Price.
        /// </summary>
        public Price Price {
            get {
                return FindExtension(ContentForShoppingNameTable.Price,
                    ContentForShoppingNameTable.ProductsNamespace) as Price;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.Price,
                    ContentForShoppingNameTable.ProductsNamespace,
                    value);
            }
        }

        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity {
            get {
                return Convert.ToInt32(GetStringValue<Quantity>(ContentForShoppingNameTable.Quantity,
                    ContentForShoppingNameTable.ProductsNamespace));
            }
            set {
                SetStringValue<Quantity>(value.ToString(),
                    ContentForShoppingNameTable.Quantity,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Sale Price.
        /// </summary>
        public SalePrice SalePrice {
            get {
                return FindExtension(ContentForShoppingNameTable.SalePrice,
                    ContentForShoppingNameTable.ProductsNamespace) as SalePrice;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.SalePrice,
                    ContentForShoppingNameTable.ProductsNamespace,
                    value);
            }
        }

        /// <summary>
        /// Sale Price Effective Date.
        /// </summary>
        public string SalePriceEffectiveDate {
            get {
                return GetStringValue<SalePriceEffectiveDate>(ContentForShoppingNameTable.SalePriceEffectiveDate,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<SalePriceEffectiveDate>(value,
                    ContentForShoppingNameTable.SalePriceEffectiveDate,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }
    }
}
