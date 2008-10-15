/* Copyright (c) 2006-2008 Google Inc.
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
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/
#region Using directives
using System;
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
#endregion

namespace Google.GData.GoogleBase {

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Queries, adds and removes google base attributes (Level 3)
    ///
    /// This class adds convenience methods to access common Google Base
    /// attributes to its super class
    /// <see cref="GBaseAttributeCollectionWithTypeConversion">Level 2</see>
    /// </summary>
    /// <seealso cref="GBaseAttributeCollectionWithTypeConversion"/>
    /// <seealso cref="GBaseAttributeCollection"/>
    /// <seealso cref="GBaseEntry"/>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseAttributes
                : GBaseAttributeCollectionWithTypeConversion
    {
        private const string LabelAttributeName = "label";

        private const string ItemTypeAttributeName = "item type";

        private const string ExpirationDateAttributeName = "expiration date";
        private const string ImageLinkAttributeName = "image link";

        private const string PaymentMethodAttributeName = "payment accepted";

        private const string PriceAttributeName = "price";

        private const string LocationAttributeName = "location";

        private const string PriceTypeAttributeName = "price type";

        private const string QuantityAttributeName = "quantity";

        private const string PriceUnitsAttributeName = "price units";

        private const string TaxPercentAttributeName = "tax percent";

        private const string TaxRegionAttributeName = "tax region";

        private const string DeliveryRadiusAttributeName = "delivery radius";

        private const string PickupAttributeName = "pickup";

        private const string DeliveryNotesAttributeName = "delivery notes";

        private const string PaymentNotesAttributeName = "payment notes";

        private const string ApplicationAttributeName = "application";

        private const string ShippingAttributeName = "shipping";


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a GBaseAttributes object that will access and
        /// modify the given extension list.</summary>
        /// <param name="baseList">a list that contains GBaseAttribute object,
        /// among others</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttributes(ExtensionList baseList)
                : base(baseList)
        {
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:label tags, a list of classifications the item may
        /// fall under.</summary>
        ///////////////////////////////////////////////////////////////////////
        public String[] Labels
        {
            get
            {
                return GetTextAttributes(LabelAttributeName);
            }
            set
            {
                RemoveAll(LabelAttributeName);
                foreach (string label in value)
                {
                    AddLabel(label);
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a g:label tag.</summary>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddLabel(String label)
        {
            return AddTextAttribute(LabelAttributeName, label);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:item_type tag, the type of the item.</summary>
        ///////////////////////////////////////////////////////////////////////
        public String ItemType
        {
            get
            {
                return GetTextAttribute(ItemTypeAttributeName);
            }
            set
            {
                RemoveAll(ItemTypeAttributeName);
                AddTextAttribute(ItemTypeAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:expiration_date the date/time at which the item
        /// will expire.</summary>
        ///////////////////////////////////////////////////////////////////////
        public DateTime ExpirationDate
        {
            get
            {
                return GetDateTimeAttribute(ExpirationDateAttributeName, DateTime.Now);
            }
            set
            {
                RemoveAll(ExpirationDateAttributeName);
                AddDateTimeAttribute(ExpirationDateAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:image_link tags, up to 10 images that illustrate
        /// the entry.</summary>
        ///////////////////////////////////////////////////////////////////////
        public String[] ImageLinks
        {
            get
            {
                return GetUrlAttributes(ImageLinkAttributeName);
            }
            set
            {
                RemoveAll(ImageLinkAttributeName);
                foreach (string imageLink in value)
                {
                    AddImageLink(imageLink);
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a g:image_link tag</summary>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddImageLink(string imageLink)
        {
            return AddUrlAttribute(ImageLinkAttributeName, imageLink);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:payment tags.</summary>
        ///////////////////////////////////////////////////////////////////////
        public String[] PaymentMethods
        {
            get
            {
                return GetTextAttributes(PaymentMethodAttributeName);
            }
            set
            {
                RemoveAll(PaymentMethodAttributeName);
                foreach (string method in value)
                {
                    AddPaymentMethod(method);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>g:shipping tags.</summary>
        //////////////////////////////////////////////////////////////////////
        public Shipping Shipping
        {
            get
            {
                return GetShippingAttribute(ShippingAttributeName);
            }
            set
            {
                RemoveAll(ShippingAttributeName);
                AddShippingAttribute(ShippingAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a g:payment tag.</summary>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddPaymentMethod(String method)
        {
            return AddTextAttribute(PaymentMethodAttributeName, method);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:price tag, the price of the item.</summary>
        ///////////////////////////////////////////////////////////////////////
        public FloatUnit Price
        {
            get
            {
                return GetFloatUnitAttribute(PriceAttributeName);
            }
            set
            {
                RemoveAll(PriceAttributeName);
                AddFloatUnitAttribute(PriceAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:location type, the location (address) of the item. </summary>
        ///////////////////////////////////////////////////////////////////////
        public String Location
        {
            get
            {
                return GetLocationAttribute(LocationAttributeName);
            }
            set
            {
                RemoveAll(LocationAttributeName);
                AddLocationAttribute(LocationAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:price_type tag, <c>fixed</c> or <c>starting</c></summary>
        ///////////////////////////////////////////////////////////////////////
        public String PriceType
        {
            get
            {
                return GetTextAttribute(PriceTypeAttributeName);
            }
            set
            {
                RemoveAll(PriceTypeAttributeName);
                AddTextAttribute(PriceTypeAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:quantity tag</summary>
        ///////////////////////////////////////////////////////////////////////
        public int Quantity
        {
            get
            {
                return GetIntAttribute(QuantityAttributeName, 0);
            }
            set
            {
                RemoveAll(QuantityAttributeName);
                AddIntAttribute(QuantityAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:price_unit tag, the unit that corresponds to
        /// the price.
        ///
        /// For example, if the item costs 10 dollars per kilo, you'd
        /// put:
        /// <code>
        /// &lt;g:price&gt;10 USD&lt;g:price&gt;
        /// &lt;g:price_unit&gt;kg&lt;g:price_unit&gt;
        /// </code>
        /// </summary>
        ///////////////////////////////////////////////////////////////////////
        public String PriceUnits
        {
            get
            {
                return GetTextAttribute(PriceUnitsAttributeName);
            }
            set
            {
                RemoveAll(PriceUnitsAttributeName);
                AddTextAttribute(PriceUnitsAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:tag_percent tag</summary>
        ///////////////////////////////////////////////////////////////////////
        public float TaxPercents
        {
            get
            {
                return GetFloatAttribute(TaxPercentAttributeName, 0);
            }
            set
            {
                RemoveAll(TaxPercentAttributeName);
                AddFloatAttribute(TaxPercentAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:tax_region tag</summary>
        ///////////////////////////////////////////////////////////////////////
        public String TaxRegion
        {
            get
            {
                return GetTextAttribute(TaxRegionAttributeName);
            }
            set
            {
                RemoveAll(TaxRegionAttributeName);
                AddTextAttribute(TaxRegionAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:delivery_radius tag</summary>
        ///////////////////////////////////////////////////////////////////////
        public FloatUnit DeliveryRadius
        {
            get
            {
                return GetFloatUnitAttribute(DeliveryRadiusAttributeName);
            }
            set
            {
                RemoveAll(DeliveryRadiusAttributeName);
                AddFloatUnitAttribute(DeliveryRadiusAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:pickup tag</summary>
        ///////////////////////////////////////////////////////////////////////
        public bool Pickup
        {
            get
            {
                return GetBooleanAttribute(PickupAttributeName, false);
            }
            set
            {
                RemoveAll(PickupAttributeName);
                AddBooleanAttribute(PickupAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:delivery_notes tag</summary>
        ///////////////////////////////////////////////////////////////////////
        public String DeliveryNotes
        {
            get
            {
                return GetTextAttribute(DeliveryNotesAttributeName);
            }
            set
            {
                RemoveAll(DeliveryNotesAttributeName);
                AddTextAttribute(DeliveryNotesAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:payment_notes tag</summary>
        ///////////////////////////////////////////////////////////////////////
        public String PaymentNotes
        {
            get
            {
                return GetTextAttribute(PaymentNotesAttributeName);
            }
            set
            {
                RemoveAll(PaymentNotesAttributeName);
                AddTextAttribute(PaymentNotesAttributeName, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g:application tag, which describes your application.
        ///
        /// This tag is set automatically by this client on all items
        /// before they are inserted or updated using the application
        /// name passed to the service.</summary>
        ///////////////////////////////////////////////////////////////////////
        public String Application
        {
            get
            {
                return GetTextAttribute(ApplicationAttributeName);
            }
            set
            {
                RemoveAll(ApplicationAttributeName);
                AddTextAttribute(ApplicationAttributeName, value);
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Interface for objects that contain a GBaseAttributes:
    /// feeds and entries.</summary>
    ///////////////////////////////////////////////////////////////////////
    interface GBaseAttributeContainer
    {
        GBaseAttributes GBaseAttributes { get; }
    }

}
