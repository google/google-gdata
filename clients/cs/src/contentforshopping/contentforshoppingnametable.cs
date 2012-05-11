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
        /// XML element name for additional_image_link.
        /// </summary>
        public const string AdditionalImageLink = "additional_image_link";

        /// <summary>
        /// XML element name for adult.
        /// </summary>
        public const string Adult = "adult";

        /// <summary>
        /// XML element name for adwords_grouping.
        /// </summary>
        public const string AdwordsGrouping = "adwords_grouping";

        /// <summary>
        /// XML element name for adwords_labels.
        /// </summary>
        public const string AdwordsLabels = "adwords_labels";

        /// <summary>
        /// XML element name for adwords_queryparam.
        /// </summary>
        public const string AdwordsQueryParam = "adwords_queryparam";

        /// <summary>
        /// XML element name for adwords_redirect.
        /// </summary>
        public const string AdwordsRedirect = "adwords_redirect";

        /// <summary>
        /// XML element name for age_group.
        /// </summary>
        public const string AgeGroup = "age_group";

        /// <summary>
        /// XML element name for author.
        /// </summary>
        public const string Author = "author";

        /// <summary>
        /// XML element name for availability.
        /// </summary>
        public const string Availability = "availability";

        /// <summary>
        /// XML element name for brand.
        /// </summary>
        public const string Brand = "brand";

        /// <summary>
        /// XML element name for channel.
        /// </summary>
        public const string Channel = "channel";

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
        /// XML element name for sale price.
        /// </summary>
        public const string SalePrice = "sale_price";

        /// <summary>
        /// XML element name for sale price effective date.
        /// </summary>
        public const string SalePriceEffectiveDate = "sale_price_effective_date";

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
        /// XML element name for gender.
        /// </summary>
        public const string Gender = "gender";

        /// <summary>
        /// XML element name for google_product_category.
        /// </summary>
        public const string GoogleProductCategory = "google_product_category";

        /// <summary>
        /// XML element name for item_group_id.
        /// </summary>
        public const string ItemGroupId = "item_group_id";

        /// <summary>
        /// XML element name for manufacturer.
        /// </summary>
        public const string Manufacturer = "manufacturer";

        /// <summary>
        /// XML element name for material.
        /// </summary>
        public const string Material = "material";

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
        /// XML element name for pattern.
        /// </summary>
        public const string Pattern = "pattern";

        /// <summary>
        /// XML element name for product_review_average.
        /// </summary>
        public const string ProductReviewAverage = "product_review_average";

        /// <summary>
        /// XML element name for product_review_count.
        /// </summary>
        public const string ProductReviewCount = "product_review_count";

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
        /// XML element name for required destination.
        /// </summary>
        public const string RequiredDestination = "required_destination";

        /// <summary>
        /// XML element name for validate destination.
        /// </summary>
        public const string ValidateDestination = "validate_destination";

        /// <summary>
        /// XML element name for excluded destination.
        /// </summary>
        public const string ExcludedDestination = "excluded_destination";

        /// <summary>
        /// XML element name for dest.
        /// </summary>
        public const string Destination = "dest";

        /// <summary>
        /// XML element name for status.
        /// </summary>
        public const string Status = "status";

        /// <summary>
        /// XML element name for warnings.
        /// </summary>
        public const string Warnings = "warnings";

        /// <summary>
        /// XML element name for warning.
        /// </summary>
        public const string Warning = "warning";

        /// <summary>
        /// XML element name for warning code.
        /// </summary>
        public const string WarningCode = "code";

        /// <summary>
        /// XML element name for warning domain.
        /// </summary>
        public const string WarningDomain = "domain";

        /// <summary>
        /// XML element name for warning location.
        /// </summary>
        public const string WarningLocation = "location";

        /// <summary>
        /// XML element name for warning message.
        /// </summary>
        public const string WarningMessage = "message";

        /// <summary>
        /// XML element name for performance.
        /// </summary>
        public const string Performance = "performance";

        /// <summary>
        /// XML element name for datapoint.
        /// </summary>
        public const string Datapoint = "datapoint";

        /// <summary>
        /// XML element name for date.
        /// </summary>
        public const string Date = "date";

        /// <summary>
        /// XML element name for clicks.
        /// </summary>
        public const string Clicks = "clicks";

        /// <summary>
        /// XML element name for attribute.
        /// </summary>
        public const string Attribute = "attribute";

        /// <summary>
        /// XML element name for group.
        /// </summary>
        public const string Group = "group";

        /// <summary>
        /// XML element name for name.
        /// </summary>
        public const string Name = "name";

        /// <summary>
        /// XML element name for type.
        /// </summary>
        public const string Type = "type";

        /// <summary>
        /// XML element name for unit.
        /// </summary>
        public const string Unit = "unit";

        /// <summary>
        /// XML element name account status
        /// </summary>
        public const string AccountStatus = "account_status";

        /// <summary>
        /// XML element name for adult content.
        /// </summary>
        public const string AdultContent = "adult_content";

        /// <summary>
        /// XML element name for internal id.
        /// </summary>
        public const string InternalId = "internal_id";

        /// <summary>
        /// XML element name for reviews url.
        /// </summary>
        public const string ReviewsUrl = "reviews_url";

        /// <summary>
        /// The base URI for all Content API requests.
        /// </summary>
        public const string AllFeedsBaseUri = "https://content.googleapis.com/content/v1";

        /// <summary>
        /// The value of the show warnings URL parameter.
        /// </summary>
        public const string ShowWarningsParameter = "warnings";

        /// <summary>
        /// The value of the dry run URL parameter.
        /// </summary>
        public const string DryRunParameter = "dry-run";
    }
}
