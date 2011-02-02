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
    public class Tax : SimpleContainer
    {
        /// <summary>
        /// default constructor for scp:tax
        /// </summary>
        public Tax()
            : base(ContentForShoppingNameTable.Tax,
                ContentForShoppingNameTable.scpDataPrefix,
                ContentForShoppingNameTable.BaseNamespace)
        {
            this.ExtensionFactories.Add(new TaxCountry());
            this.ExtensionFactories.Add(new TaxRegion());
            this.ExtensionFactories.Add(new TaxRate());
            this.ExtensionFactories.Add(new TaxShip());
        }

        /// <summary>
        /// returns the scp:tax_country element
        /// </summary>
        public string Country
        {
            get
            {
                return GetStringValue<TaxCountry>(ContentForShoppingNameTable.TaxCountry,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set
            {
                SetStringValue<TaxCountry>(value.ToString(),
                    ContentForShoppingNameTable.TaxCountry,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// returns the scp:tax_region element
        /// </summary>
        public string Region
        {
            get
            {
                return GetStringValue<TaxRegion>(ContentForShoppingNameTable.TaxRegion,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set
            {
                SetStringValue<TaxRegion>(value.ToString(),
                    ContentForShoppingNameTable.TaxRegion,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// returns the scp:tax_rate element
        /// </summary>
        public string Rate
        {
            get
            {
                return GetStringValue<TaxRate>(ContentForShoppingNameTable.TaxRate,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set
            {
                SetStringValue<TaxRate>(value.ToString(),
                    ContentForShoppingNameTable.TaxRate,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// returns the scp:tax_ship element
        /// </summary>
        public bool Ship
        {
            get
            {
                bool value;
                if (!bool.TryParse(GetStringValue<TaxShip>(ContentForShoppingNameTable.TaxShip,
                    ContentForShoppingNameTable.ProductsNamespace), out value))
                {
                    value = false;
                }

                return value;
            }
            set
            {
                SetStringValue<TaxShip>(value.ToString(),
                    ContentForShoppingNameTable.TaxShip,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }
    }
}
