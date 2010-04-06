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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Removed warnings
* 
*/
#region Using directives
using System;
#endregion

namespace Google.GData.GoogleBase {

    //////////////////////////////////////////////////////////////////////
    /// <summary>An object representation of the shipping type.</summary>
    //////////////////////////////////////////////////////////////////////
    public class Shipping
    {
        private string country;
        private string service;
        private float price;
        // currency may be null even if the shipping object is valid
        private string currency;

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates an unitialized shipping object.</summary>
        //////////////////////////////////////////////////////////////////////
        public Shipping()
        {
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates a shipping object</summary>
        /// <param name="country">destination country (ISO 3312 2-letter
        /// code)</param>
        /// <param name="service">shipping method</param>
        /// <param name="price">price</param>
        /// <param name="currency">price currency (ISO 4217 3-letter code)</param>
        //////////////////////////////////////////////////////////////////////
        public Shipping(string country, string service, float price, string currency)
        {
            this.country = country;
            this.service = service;
            this.price = price;
            this.currency = currency;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates a shipping object given a shipping attribute.
        /// </summary>
        /// <exception cref="ArgumentException">If the attribute
        /// type is not 'shipping' or unknown</exception>
        /// <exception cref="FormatException">If the attribute contains
        /// invalid sub-elements or lacks required sub-elements</exception>
        //////////////////////////////////////////////////////////////////////
        public Shipping(GBaseAttribute attribute)
        {
            GBaseAttributeType type = attribute.Type;
            if (type != null && type != GBaseAttributeType.Shipping)
            {
                throw new ArgumentException("Expected an attribute of " +
                    "type 'shipping', got an attribute of type '" + type + "'");
            }
            this.country = GetRequiredAttribute(attribute, "country");
            this.service = GetRequiredAttribute(attribute, "service");
            string priceString = GetRequiredAttribute(attribute, "price");
            try
            {
                FloatUnit priceUnit = new FloatUnit(priceString);
                this.price = priceUnit.Value;
                this.currency = priceUnit.Unit;
            }
            catch (FormatException)
            {
                this.price = NumberFormat.ToFloat(priceString);
                this.currency = null;
            }
        }

        private static string GetRequiredAttribute(GBaseAttribute attribute, string name)
        {
            string value = attribute[name];
            if (value == null)
            {
                throw new FormatException("Missing sub-element for shipping attribute: " + name);
            }
            return value;
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>String representation, for debugging.</summary>
        //////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "Shipping(country=" + country +", service=" + service + ", price= " +
                price + ", currency=" + currency + ")";
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates a GBaseAttribute of type shipping corresponding
        /// to the current object.</summary>
        /// <param name="name">attribute name</param>
        //////////////////////////////////////////////////////////////////////
        public GBaseAttribute CreateGBaseAttribute(string name)
        {
            GBaseAttribute retval = new GBaseAttribute(name, GBaseAttributeType.Shipping);
            retval["country"] = country;
            retval["service"] = service;
            if (currency != null)
            {
                retval["price"] = price + " " + currency;
            }
            else
            {
                retval["price"] = NumberFormat.ToString(price);
            }
            return retval;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Destination country (ISO 3312 2-letter code)</summary>
        //////////////////////////////////////////////////////////////////////
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Shipping method</summary>
        //////////////////////////////////////////////////////////////////////
        public string Service
        {
            get
            {
                return service;
            }
            set
            {
                service = value;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Price</summary>
        //////////////////////////////////////////////////////////////////////
        public float Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Currency (ISO 4217 3-letter code)</summary>
        //////////////////////////////////////////////////////////////////////
        public string Currency
        {
            get
            {
                return currency;
            }
            set
            {
                currency = value;
            }
        }
    }
}
