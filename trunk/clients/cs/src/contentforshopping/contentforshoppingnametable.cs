/* Copyright (c) 2007-2008 Google Inc.
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

namespace Google.GData.ContentForShopping
{
	class ContentForShoppingNameTable
    {
        /// <summary>
        /// The Structured Content namespace.
        /// </summary>
        public const string BaseNamespace = "http://schemas.google.com/structuredcontent/2009";

        /// <summary>
        /// prefix for Structured Content namespace
        /// </summary> 
        public const string scDataPrefix = "sc"; 

        /// <summary>
        /// The Structured Content Products namespace.
        /// </summary>
        public const string ProductsNamespace = BaseNamespace + "/products";

        /// <summary>
        /// prefix for Structured Content Products namespace
        /// </summary> 
        public const string scpDataPrefix = "scp"; 

        /// <summary>
        /// XML element name for product identifier.
        /// </summary>
        public const string ProductId = "id";

        /// <summary>
        /// XML element name for target country.
        /// </summary>
        public const string TargetCountry = "target_country";

        /// <summary>
        /// XML element name for content language.
        /// </summary>
        public const string ContentLanguage = "content_language";

        /// <summary>
        /// XML element name for expiration date.
        /// </summary>
        public const string ExpirationDate = "expiration_date";

        /// <summary>
        /// XML element name for image_link.
        /// </summary>
        public const string ImageLink = "image_link";

        /// <summary>
        /// XML element name for adult.
        /// </summary>
        public const string Adult = "adult";

        /// <summary>
        /// XML element name for brand.
        /// </summary>
        public const string Brand = "brand";

        /// <summary>
        /// XML element name for condition.
        /// </summary>
        public const string Condition = "condition";

        /// <summary>
        /// XML element name for color.
        /// </summary>
        public const string Color = "color";

        /// <summary>
        /// XML element name for gtin.
        /// </summary>
        public const string Gtin = "gtin";

        /// <summary>
        /// XML element name for publisher.
        /// </summary>
        public const string Publisher = "publisher";

        /// <summary>
        /// XML element name for size.
        /// </summary>
        public const string Size = "size";

        /// <summary>
        /// XML element name for year.
        /// </summary>
        public const string Year = "year";

        /// <summary>
        /// XML element name for price.
        /// </summary>
        public const string Price = "price";

        /// <summary>
        /// XML element name for price unit.
        /// </summary>
        public const string PriceUnit = "unit";

        /// <summary>
        /// XML element name for product type.
        /// </summary>
        public const string ProductType = "product_type";

        /// <summary>
        /// XML element name for product weight.
        /// </summary>
        public const string ProductWeight = "product_weight";

        /// <summary>
        /// XML element name for quantity.
        /// </summary>
        public const string Quantity = "quantity";

        /// <summary>
        /// XML element name for edition.
        /// </summary>
        public const string Edition = "edition";

        /// <summary>
        /// XML element name for genre.
        /// </summary>
        public const string Genre = "genre";

        /// <summary>
        /// XML element name for manufacturer.
        /// </summary>
        public const string Manufacturer = "manufacturer";

        /// <summary>
        /// XML element name for model_number.
        /// </summary>
        public const string ModelNumber = "model_number";

        /// <summary>
        /// XML element name for mpn.
        /// </summary>
        public const string Mpn = "mpn";

        /// <summary>
        /// XML element name for pages.
        /// </summary>
        public const string Pages = "pages";

        /// <summary>
        /// XML element name for feature.
        /// </summary>
        public const string Feature = "feature";

        /// <summary>
        /// XML element name for featured_product.
        /// </summary>
        public const string FeaturedProduct = "featured_product";

        /// <summary>
        /// XML element name for tax.
        /// </summary>
        public const string Tax = "tax";

        /// <summary>
        /// XML element name for tax_country.
        /// </summary>
        public const string TaxCountry = "tax_country";

        /// <summary>
        /// XML element name for tax_region.
        /// </summary>
        public const string TaxRegion = "tax_region";

        /// <summary>
        /// XML element name for tax_rate.
        /// </summary>
        public const string TaxRate = "tax_rate";

        /// <summary>
        /// XML element name for tax_ship.
        /// </summary>
        public const string TaxShip = "tax_ship";

        /// <summary>
        /// XML element name for shipping.
        /// </summary>
        public const string Shipping = "shipping";

        /// <summary>
        /// XML element name for shipping_country.
        /// </summary>
        public const string ShippingCountry = "shipping_country";

        /// <summary>
        /// XML element name for shipping_region.
        /// </summary>
        public const string ShippingRegion = "shipping_region";

        /// <summary>
        /// XML element name for shipping_service.
        /// </summary>
        public const string ShippingService = "shipping_service";

        /// <summary>
        /// XML element name for shipping_price.
        /// </summary>
        public const string ShippingPrice = "shipping_price";

        /// <summary>
        /// XML element name for shipping_weight.
        /// </summary>
        public const string ShippingWeight = "shipping_weight";

        /// <summary>
        /// XML element name for weight unit.
        /// </summary>
        public const string WeightUnit = "unit";
    }
}
