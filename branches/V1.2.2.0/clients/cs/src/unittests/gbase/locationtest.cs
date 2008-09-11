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
    public class LocationTest
    {
        [Test]
        public void LocationFromAttributeTest()
        {
            GBaseAttribute completeAttr = new GBaseAttribute("location", GBaseAttributeType.Location);
            completeAttr.Content = "Nowhere";
            completeAttr["latitude"] = "3.0";
            completeAttr["longitude"] = "4.5";

            Location location = new Location(completeAttr);
            Assert.AreEqual("Nowhere", location.Address, "country");
            Assert.AreEqual(3.0f, location.Latitude, 0.1);
            Assert.AreEqual(4.5f, location.Longitude, 0.1);
            Assert.IsTrue(location.HasCoordinates);
        }

        [Test]
        public void NoCoordinatesTest()
        {
            Location location = new Location("North Pole");
            Assert.AreEqual("North Pole", location.Address);
            Assert.IsFalse(location.HasCoordinates);
        }


        [Test]
        public void LocationToAttributeWithCoordinatesTest()
        {
            GBaseAttribute created = new Location("Nowhere", 3.0f, 0.0f).CreateGBaseAttribute("location");
            Assert.AreEqual("Nowhere", created.Content);
            Assert.AreEqual("3.00", created["latitude"], "latitude");
            Assert.AreEqual("0.00", created["longitude"], "longitude");
        }


        [Test]
        public void LocationToAttributeNoCoordinatesTest()
        {
            GBaseAttribute created = new Location("Nowhere").CreateGBaseAttribute("location");
            Assert.AreEqual("Nowhere", created.Content);
            Assert.IsNull(created["latitude"], "latitude");
            Assert.IsNull(created["longitude"], "longitude");
        }

    }

}
