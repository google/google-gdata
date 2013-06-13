/* Copyright (c) 2013 Google Inc.
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
    public class UnitPricingMeasure : SimpleElement
    {
        /// <summary>
        /// default constructor for scp:unit_pricing_measure
        /// </summary>
        public UnitPricingMeasure ()
            : base(ContentForShoppingNameTable.UnitPricingMeasure,
               ContentForShoppingNameTable.scpDataPrefix,
               ContentForShoppingNameTable.ProductsNamespace)
        {
        }
     
        public UnitPricingMeasure (string unit, string val)
            : base(ContentForShoppingNameTable.UnitPricingMeasure,
               ContentForShoppingNameTable.scpDataPrefix,
               ContentForShoppingNameTable.ProductsNamespace)
        {
            this.Value = val;
            this.Unit = unit;
        }
     
        /// <summary>
        /// Unit property accessor
        /// </summary>
        public string Unit {
            get {
                return Convert.ToString (Attributes [ContentForShoppingNameTable.Unit]);
            }
            set {
                Attributes [ContentForShoppingNameTable.Unit] = value;
            }
        }
    }
}
