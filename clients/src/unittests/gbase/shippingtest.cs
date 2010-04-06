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
using NUnit.Framework;

namespace Google.GData.GoogleBase.UnitTests
{
    [TestFixture]
    [Category("GoogleBase")]
    public class ShippingTest
    {
        /// <summary>A complete and valid definition.</summary>
        private GBaseAttribute completeAttr;

        [SetUp]
        public void SetUp()
        {
            completeAttr = new GBaseAttribute("shipping", GBaseAttributeType.Shipping);
            completeAttr["country"] = "FR";
            completeAttr["price"] = "199.95 eur";
            completeAttr["service"] = "YPS";
        }

        [Test]
        public void CreateTest()
        {
            Shipping shipping = new Shipping(completeAttr);
            Assert.AreEqual("FR", shipping.Country, "country");
            // Avoid rounding errors
            Assert.AreEqual(19995, Math.Round(shipping.Price*100.0), "price");
            Assert.AreEqual("eur", shipping.Currency);
            Assert.AreEqual("YPS", shipping.Service);
        }

        [Test]
        public void MissingCurrencyTest()
        {
            completeAttr["price"] = "22";
            Shipping shipping = new Shipping(completeAttr);
            Assert.AreEqual(22, shipping.Price, "price");
            Assert.IsNull(shipping.Currency, "currency");
        }

        [Test]
        public void CreateAttributeTest()
        {
            Shipping shipping = new Shipping("FR", "YPS", 199.95f, "eur");
            GBaseAttribute created = shipping.CreateGBaseAttribute("shipping");
            Assert.AreEqual(completeAttr, created);
        }

        [Test]
        public void CreateAttributeWithoutCurrencyTest()
        {
            Shipping shipping = new Shipping("FR", "YPS", 10.0f, null);
            GBaseAttribute created = shipping.CreateGBaseAttribute("shipping");
            completeAttr["price"] = "10.00";
            Assert.AreEqual(completeAttr, created);
        }
    }
}
