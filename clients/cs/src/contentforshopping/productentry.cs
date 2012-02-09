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
    /// Feed API customization class for defining Product feed.
    /// </summary>
    public class ProductEntry : AbstractEntry {
        private const string dateFormat8601 = "yyyy-MM-ddTHH:mm:ss.fffzzz";

        /// <summary>
        /// AdditionalImageLink collection
        /// </summary>
        private ExtensionCollection<AdditionalImageLink> additionalImageLinks;

        /// <summary>
        /// Tax collection
        /// </summary>
        private ExtensionCollection<Tax> taxRules;

        /// <summary>
        /// Shipping collection
        /// </summary>
        private ExtensionCollection<Shipping> shippingRules;

        /// <summary>
        /// Color collection
        /// </summary>
        private ExtensionCollection<Color> colors;

        /// <summary>
        /// Feature collection
        /// </summary>
        private ExtensionCollection<Feature> features;

        /// <summary>
        /// Size collection
        /// </summary>
        private ExtensionCollection<Size> sizes;

        /// <summary>
        /// Custom attributes collection
        /// </summary>
        private ExtensionCollection<CustomAttribute> customAttributes;

        public ProductEntry()
            : base() {
            this.AddExtension(new ProductId());
            this.AddExtension(new TargetCountry());
            this.AddExtension(new ContentLanguage());
            this.AddExtension(new ExpirationDate());
            this.AddExtension(new AdditionalImageLink());
            this.AddExtension(new Adult());
            this.AddExtension(new AgeGroup());
            this.AddExtension(new Author());
            this.AddExtension(new Availability());
            this.AddExtension(new Brand());
            this.AddExtension(new Channel());
            this.AddExtension(new Color());
            this.AddExtension(new Condition());
            this.AddExtension(new CustomAttribute());
            this.AddExtension(new Edition());
            this.AddExtension(new Feature());
            this.AddExtension(new FeaturedProduct());
            this.AddExtension(new Gender());
            this.AddExtension(new Genre());
            this.AddExtension(new GoogleProductCategory());
            this.AddExtension(new Gtin());
            this.AddExtension(new ImageLink());
            this.AddExtension(new ItemGroupId());
            this.AddExtension(new Manufacturer());
            this.AddExtension(new Material());
            this.AddExtension(new ModelNumber());
            this.AddExtension(new Mpn());
            this.AddExtension(new Pages());
            this.AddExtension(new Pattern());
            this.AddExtension(new Price());
            this.AddExtension(new ProductReviewAverage());
            this.AddExtension(new ProductReviewCount());
            this.AddExtension(new ProductType());
            this.AddExtension(new ProductWeight());
            this.AddExtension(new Publisher());
            this.AddExtension(new Quantity());
            this.AddExtension(new Shipping());
            this.AddExtension(new ShippingWeight());
            this.AddExtension(new Size());
            this.AddExtension(new Tax());
            this.AddExtension(new Year());

            // replacing the default app:control extension with the API-specific one
            this.RemoveExtension(new AppControl());
            this.AddExtension(new ProductControl());
        }

        /// <summary>
        /// returns the Content for Shopping API-specific app:control element
        /// </summary>
        /// <returns></returns>
        public new ProductControl AppControl {
            get {
                return FindExtension(BaseNameTable.XmlElementPubControl,
                    BaseNameTable.NSAppPublishingFinal) as ProductControl;
            }
            set {
                ReplaceExtension(BaseNameTable.XmlElementPubControl,
                    BaseNameTable.NSAppPublishingFinal,
                    value);
            }
        }

        /// <summary>
        /// Product Identifier.
        /// </summary>
        public string ProductId {
            get {
                return GetStringValue<ProductId>(ContentForShoppingNameTable.ProductId,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<ProductId>(value,
                    ContentForShoppingNameTable.ProductId,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Target Country.
        /// </summary>
        public string TargetCountry {
            get {
                return GetStringValue<TargetCountry>(ContentForShoppingNameTable.TargetCountry,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<TargetCountry>(value,
                    ContentForShoppingNameTable.TargetCountry,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Content Language.
        /// </summary>
        public string ContentLanguage {
            get {
                return GetStringValue<ContentLanguage>(ContentForShoppingNameTable.ContentLanguage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
            set {
                SetStringValue<ContentLanguage>(value,
                    ContentForShoppingNameTable.ContentLanguage,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Expiration Date.
        /// </summary>
        public DateTime ExpirationDate {
            get {
                string date = GetStringValue<ExpirationDate>(ContentForShoppingNameTable.ExpirationDate,
                    ContentForShoppingNameTable.BaseNamespace);
                return DateTime.ParseExact(date, dateFormat8601, CultureInfo.InvariantCulture);
            }
            set {
                SetStringValue<ExpirationDate>(value.ToString(dateFormat8601),
                    ContentForShoppingNameTable.ExpirationDate,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Adult.
        /// </summary>
        public bool Adult {
            get {
                bool value;
                if (!bool.TryParse(GetStringValue<Adult>(ContentForShoppingNameTable.Adult,
                    ContentForShoppingNameTable.BaseNamespace), out value)) {
                    value = false;
                }

                return value;
            }
            set {
                SetStringValue<Adult>(value.ToString(),
                    ContentForShoppingNameTable.Adult,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// AgeGroup.
        /// </summary>
        public string AgeGroup {
            get {
                return GetStringValue<AgeGroup>(ContentForShoppingNameTable.AgeGroup,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<AgeGroup>(value,
                    ContentForShoppingNameTable.AgeGroup,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Author.
        /// </summary>
        public string Author {
            get {
                return GetStringValue<AgeGroup>(ContentForShoppingNameTable.Author,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Author>(value,
                    ContentForShoppingNameTable.Author,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
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
        /// FeaturedProduct.
        /// </summary>
        public bool FeaturedProduct {
            get {
                return (GetStringValue<FeaturedProduct>(ContentForShoppingNameTable.FeaturedProduct,
                    ContentForShoppingNameTable.BaseNamespace) == "y");
            }
            set {
                string stringValue = value ? "y" : "n";
                SetStringValue<FeaturedProduct>(stringValue,
                    ContentForShoppingNameTable.FeaturedProduct,
                    ContentForShoppingNameTable.BaseNamespace);
            }
        }

        /// <summary>
        /// Brand.
        /// </summary>
        public string Brand {
            get {
                return GetStringValue<Brand>(ContentForShoppingNameTable.Brand,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Brand>(value,
                    ContentForShoppingNameTable.Brand,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Channel.
        /// </summary>
        public string Channel {
            get {
                return GetStringValue<Channel>(ContentForShoppingNameTable.Channel,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Channel>(value,
                    ContentForShoppingNameTable.Channel,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Condition.
        /// </summary>
        public string Condition {
            get {
                return GetStringValue<Condition>(ContentForShoppingNameTable.Condition,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Condition>(value,
                    ContentForShoppingNameTable.Condition,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Gtin.
        /// </summary>
        public string Gtin {
            get {
                return GetStringValue<Gtin>(ContentForShoppingNameTable.Gtin,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Gtin>(value,
                    ContentForShoppingNameTable.Gtin,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// ItemGroupId.
        /// </summary>
        public string ItemGroupId {
            get {
                return GetStringValue<ItemGroupId>(ContentForShoppingNameTable.ItemGroupId,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<ItemGroupId>(value,
                    ContentForShoppingNameTable.ItemGroupId,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Product Type.
        /// </summary>
        public string ProductType {
            get {
                return GetStringValue<ProductType>(ContentForShoppingNameTable.ProductType,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<ProductType>(value,
                    ContentForShoppingNameTable.ProductType,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Edition.
        /// </summary>
        public string Edition {
            get {
                return GetStringValue<Edition>(ContentForShoppingNameTable.Edition,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Edition>(value,
                    ContentForShoppingNameTable.Edition,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Gender.
        /// </summary>
        public string Gender {
            get {
                return GetStringValue<Gender>(ContentForShoppingNameTable.Gender,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Gender>(value,
                    ContentForShoppingNameTable.Gender,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Genre.
        /// </summary>
        public string Genre {
            get {
                return GetStringValue<Genre>(ContentForShoppingNameTable.Genre,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Genre>(value,
                    ContentForShoppingNameTable.Genre,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// GoogleProductCategory.
        /// </summary>
        public string GoogleProductCategory {
            get {
                return GetStringValue<GoogleProductCategory>(ContentForShoppingNameTable.GoogleProductCategory,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<GoogleProductCategory>(value,
                    ContentForShoppingNameTable.GoogleProductCategory,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Manufacturer.
        /// </summary>
        public string Manufacturer {
            get {
                return GetStringValue<Manufacturer>(ContentForShoppingNameTable.Manufacturer,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Manufacturer>(value,
                    ContentForShoppingNameTable.Manufacturer,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Material.
        /// </summary>
        public string Material {
            get {
                return GetStringValue<Material>(ContentForShoppingNameTable.Material,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Material>(value,
                    ContentForShoppingNameTable.Material,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Model Number.
        /// </summary>
        public string ModelNumber {
            get {
                return GetStringValue<ModelNumber>(ContentForShoppingNameTable.ModelNumber,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<ModelNumber>(value,
                    ContentForShoppingNameTable.ModelNumber,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Manufacturer's Part Number.
        /// </summary>
        public string Mpn {
            get {
                return GetStringValue<Mpn>(ContentForShoppingNameTable.Mpn,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Mpn>(value,
                    ContentForShoppingNameTable.Mpn,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Publisher.
        /// </summary>
        public string Publisher {
            get {
                return GetStringValue<Publisher>(ContentForShoppingNameTable.Publisher,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Publisher>(value,
                    ContentForShoppingNameTable.Publisher,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Pages.
        /// </summary>
        public int Pages {
            get {
                return Convert.ToInt32(GetStringValue<Pages>(ContentForShoppingNameTable.Pages,
                    ContentForShoppingNameTable.ProductsNamespace));
            }
            set {
                SetStringValue<Pages>(value.ToString(),
                    ContentForShoppingNameTable.Pages,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Pattern.
        /// </summary>
        public string Pattern {
            get {
                return GetStringValue<Pattern>(ContentForShoppingNameTable.Pattern,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<Pattern>(value,
                    ContentForShoppingNameTable.Pattern,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// ProductReviewAverage.
        /// </summary>
        // TODO: use a float instead of a string
        public string ProductReviewAverage {
            get {
                return GetStringValue<ProductReviewAverage>(ContentForShoppingNameTable.ProductReviewAverage,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
            set {
                SetStringValue<ProductReviewAverage>(value,
                    ContentForShoppingNameTable.ProductReviewAverage,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// ProductReviewCount.
        /// </summary>
        public int ProductReviewCount {
            get {
                return Convert.ToInt32(GetStringValue<ProductReviewCount>(ContentForShoppingNameTable.ProductReviewCount,
                    ContentForShoppingNameTable.ProductsNamespace));
            }
            set {
                SetStringValue<ProductReviewCount>(value.ToString(),
                    ContentForShoppingNameTable.ProductReviewCount,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Year.
        /// </summary>
        public int Year {
            get {
                return Convert.ToInt32(GetStringValue<Year>(ContentForShoppingNameTable.Year,
                    ContentForShoppingNameTable.ProductsNamespace));
            }
            set {
                SetStringValue<Year>(value.ToString(),
                    ContentForShoppingNameTable.Year,
                    ContentForShoppingNameTable.ProductsNamespace);
            }
        }

        /// <summary>
        /// Quantity.
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
        /// Shipping Weight.
        /// </summary>
        public ShippingWeight ShippingWeight {
            get {
                return FindExtension(ContentForShoppingNameTable.ShippingWeight,
                    ContentForShoppingNameTable.ProductsNamespace) as ShippingWeight;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.ShippingWeight,
                    ContentForShoppingNameTable.ProductsNamespace,
                    value);
            }
        }

        /// <summary>
        /// Product Weight.
        /// </summary>
        public ProductWeight ProductWeight {
            get {
                return FindExtension(ContentForShoppingNameTable.ProductWeight,
                    ContentForShoppingNameTable.ProductsNamespace) as ProductWeight;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.ProductWeight,
                    ContentForShoppingNameTable.ProductsNamespace,
                    value);
            }
        }

        /// <summary>
        /// ImageLink.
        /// </summary>
        public ImageLink ImageLink {
            get {
                return FindExtension(ContentForShoppingNameTable.ImageLink,
                    ContentForShoppingNameTable.BaseNamespace) as ImageLink;
            }
            set {
                ReplaceExtension(ContentForShoppingNameTable.ImageLink,
                    ContentForShoppingNameTable.BaseNamespace,
                    value);
            }
        }

        /// <summary>
        /// AdditionalImageLink collection.
        /// </summary>
        public ExtensionCollection<AdditionalImageLink> AdditionalImageLinks {
            get {
                if (this.additionalImageLinks == null) {
                    this.additionalImageLinks = new ExtensionCollection<AdditionalImageLink>(this);
                }
                return this.additionalImageLinks;
            }
        }

        /// <summary>
        /// Tax collection.
        /// </summary>
        public ExtensionCollection<Tax> TaxRules {
            get {
                if (this.taxRules == null) {
                    this.taxRules = new ExtensionCollection<Tax>(this);
                }
                return this.taxRules;
            }
        }

        /// <summary>
        /// Shipping collection.
        /// </summary>
        public ExtensionCollection<Shipping> ShippingRules {
            get {
                if (this.shippingRules == null) {
                    this.shippingRules = new ExtensionCollection<Shipping>(this);
                }
                return this.shippingRules;
            }
        }

        /// <summary>
        /// Color collection.
        /// </summary>
        public ExtensionCollection<Color> Colors {
            get {
                if (this.colors == null) {
                    this.colors = new ExtensionCollection<Color>(this);
                }
                return this.colors;
            }
        }

        /// <summary>
        /// Feature collection.
        /// </summary>
        public ExtensionCollection<Feature> Features {
            get {
                if (this.features == null) {
                    this.features = new ExtensionCollection<Feature>(this);
                }
                return this.features;
            }
        }

        /// <summary>
        /// Size collection.
        /// </summary>
        public ExtensionCollection<Size> Sizes {
            get {
                if (this.sizes == null) {
                    this.sizes = new ExtensionCollection<Size>(this);
                }
                return this.sizes;
            }
        }

        /// <summary>
        /// Custom attribute collection.
        /// </summary>
        public ExtensionCollection<CustomAttribute> CustomAttributes {
            get {
                if (this.customAttributes == null) {
                    this.customAttributes = new ExtensionCollection<CustomAttribute>(this);
                }
                return this.customAttributes;
            }
        }
    }
}
