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
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Net;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.GoogleBase;
using System.Collections.Generic;

namespace Google.GData.GoogleBase.UnitTests
{
    public abstract class TypeConversionTestBase
    {
        protected ExtensionList list;
        protected GBaseAttributeCollectionWithTypeConversion attrs;

        [SetUp]
        public virtual void SetUp()
        {
            list = ExtensionList.NotVersionAware();
            attrs = new GBaseAttributeCollectionWithTypeConversion(list);
        }
    }

    [TestFixture]
    [Category("GoogleBase")]
    public class NumberAttributesTest : TypeConversionTestBase
    {
        private GBaseAttribute aInt;
        private GBaseAttribute aFloat;
        private GBaseAttribute largeFloat;
        private GBaseAttribute aNumber;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            aInt = new GBaseAttribute("a", GBaseAttributeType.Int, "12");
            aFloat = new GBaseAttribute("a", GBaseAttributeType.Float, "3.14");
            largeFloat = new GBaseAttribute("large", GBaseAttributeType.Float, "10000000");
            aNumber = new GBaseAttribute("a", GBaseAttributeType.Number, "2.7");

            attrs.Add(aInt);
            attrs.Add(aFloat);
            attrs.Add(aNumber);
            attrs.Add(largeFloat);
        }

        [Test]
        public void ExtractIntAttribute()
        {
            int ivalue;
            Assert.IsTrue(attrs.ExtractIntAttribute("a", out ivalue));
            Assert.AreEqual(12, ivalue);
        }

        [Test]
        public void ExtractFloatAttribute()
        {
            float fvalue;
            Assert.IsTrue(attrs.ExtractFloatAttribute("a", out fvalue));
            Assert.AreEqual((float)3.14, fvalue);
        }

        [Test]
        public void AddLargeFloatAttribute()
        {
            attrs.AddFloatAttribute("largeB", 1e9f);
            Assert.AreEqual("1000000000.00", attrs.GetAttribute("largeB", GBaseAttributeType.Float).Content);
        }

        [Test]
        public void ExtractLargeFloatAttribute()
        {
            float fvalue;
            Assert.IsTrue(attrs.ExtractFloatAttribute("large", out fvalue));
            Assert.AreEqual(1e7f, fvalue);
        }

        [Test]
        public void ExtractNumberAttribute()
        {
            float fvalue;

            Assert.IsTrue(attrs.ExtractNumberAttribute("a", out fvalue));
            Assert.AreEqual((float) 12, fvalue);

            attrs.Remove(aInt);
            Assert.IsTrue(attrs.ExtractNumberAttribute("a", out fvalue));
            Assert.AreEqual((float)3.14, fvalue);

            attrs.Remove(aFloat);
            Assert.IsTrue(attrs.ExtractNumberAttribute("a", out fvalue));
            Assert.AreEqual((float) 2.7, fvalue);
        }

        [Test]
        public void GetNumberAttributes()
        {
            List<float> values = attrs.GetNumberAttributes("a");
            Assert.AreEqual(3, values.Count);
            Assert.AreEqual(12, values[0]);
            Assert.AreEqual(3.14f, values[1]);
            Assert.AreEqual(2.7f, values[2]);
        }

        [Test]
        public void GetFloatAttributes()
        {
            List<float> values = attrs.GetFloatAttributes("a");
            Assert.AreEqual(1, values.Count);
            Assert.AreEqual(3.14f, values[0]);
        }

        [Test]
        public void GetIntAttributes()
        {
            List<int> values = attrs.GetIntAttributes("a");
            Assert.AreEqual(1, values.Count);
            Assert.AreEqual(12, values[0]);
        }
    }

    [TestFixture]
    [Category("GoogleBase")]
    public class WithUnitAttributesTest : TypeConversionTestBase
    {
        private GBaseAttribute aIntUnit;
        private readonly IntUnit aIntUnitValue = new IntUnit(12, "minutes");
        private GBaseAttribute aFloatUnit;
        private readonly FloatUnit aFloatUnitValue = new FloatUnit(3.14f, "mm");
        private GBaseAttribute aNumberUnit;
        private readonly FloatUnit aNumberUnitValue = new FloatUnit(2.7f, "km");

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            aFloatUnit = new GBaseAttribute("a",
                                            GBaseAttributeType.FloatUnit,
                                            aFloatUnitValue.ToString());
            aIntUnit = new GBaseAttribute("a",
                                          GBaseAttributeType.IntUnit,
                                          aIntUnitValue.ToString());
            aNumberUnit = new GBaseAttribute("a",
                                             GBaseAttributeType.NumberUnit,
                                             aNumberUnitValue.ToString());

            attrs.Add(aFloatUnit);
            attrs.Add(aIntUnit);
            attrs.Add(aNumberUnit);
        }

        [Test]
        public void GetIntUnitAttribute()
        {
            Assert.AreEqual(aIntUnitValue, attrs.GetIntUnitAttribute("a"));
        }

        [Test]
        public void GetFloatUnitAttribute()
        {
            Assert.AreEqual(aFloatUnitValue, attrs.GetFloatUnitAttribute("a"));
        }

        [Test]
        public void AddLargeFloatUnitAttribute()
        {
            GBaseAttribute attribute = attrs.AddFloatUnitAttribute("large", 1e7f, "usd");
            Assert.AreEqual("10000000.00 usd", attribute.Content);
        }

        [Test]
        public void GetNumberUnitAttribute()
        {
            Assert.AreEqual(aFloatUnitValue, attrs.GetNumberUnitAttribute("a"));
        }

        [Test]
        public void GetIntUnitAttributes()
        {
            List<IntUnit> values = attrs.GetIntUnitAttributes("a");
            Assert.AreEqual(1, values.Count);
            Assert.AreEqual(aIntUnitValue, values[0]);
        }

        [Test]
        public void GetFloatUnitAttributes()
        {
            List<FloatUnit> values = attrs.GetFloatUnitAttributes("a");
            Assert.AreEqual(1, values.Count);
            Assert.AreEqual(aFloatUnitValue, values[0]);
        }

        [Test]
        public void GetNumberUnitAttributes()
        {
            List<NumberUnit> values = attrs.GetNumberUnitAttributes("a");
            Assert.AreEqual(3, values.Count);
            Assert.AreEqual(aFloatUnitValue, values[0]);
            Assert.AreEqual(aIntUnitValue, values[1]);
            Assert.AreEqual(aNumberUnitValue, values[2]);
        }
    }

    [TestFixture]
    [Category("GoogleBase")]
    public class DateTimeAttributeTest : TypeConversionTestBase
    {
        private static readonly string ADateString = "2005-09-02";
        private static readonly string BDateString = "2005-10-20";
        private static readonly string ADateTimeString = ADateString + "T17:32:10Z";
        private static readonly string BDateTimeString = BDateString + "T13:24:08Z";

        private static readonly DateTime ADate = DateTime.Parse(ADateString);
        private static readonly DateTime ADateTime = DateTime.Parse(ADateTimeString);
        private static readonly DateTime BDateTime = DateTime.Parse(BDateTimeString);

        private static readonly GBaseAttribute dateAttr =
            new GBaseAttribute("a", GBaseAttributeType.Date, ADateString);
        private static readonly GBaseAttribute dateTimeAttr =
            new GBaseAttribute("a", GBaseAttributeType.DateTime, ADateTimeString);
        private static readonly GBaseAttribute dateTimeRangeAttr =
            new GBaseAttribute("a", GBaseAttributeType.DateTimeRange,
                               ADateTimeString + " " + BDateTimeString);

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            attrs.Add(dateTimeAttr);
            attrs.Add(dateAttr);
            attrs.Add(dateTimeRangeAttr);
        }

        [Test]
        public void GetDateAttributeTest()
        {
            DateTime value;
            Assert.IsTrue(attrs.ExtractDateAttribute("a", out value));
            Assert.AreEqual(ADateTime, value);

            attrs.Remove(dateTimeAttr);

            Assert.IsTrue(attrs.ExtractDateAttribute("a", out value));
            Assert.AreEqual(ADate, value);
            attrs.Remove(dateAttr);

            Assert.IsFalse(attrs.ExtractDateAttribute("a", out value));
        }

        [Test]
        public void GetDateTimeAttributeTest()
        {
            DateTime value;

            Assert.IsTrue(attrs.ExtractDateTimeAttribute("a", out value));
            Assert.AreEqual(ADateTime, value);
            attrs.Remove(dateTimeAttr);

            Assert.IsFalse(attrs.ExtractDateTimeAttribute("a", out value));
        }

        [Test]
        public void GetDateTimeRangeAttributeTest()
        {
            Assert.AreEqual(new DateTimeRange(ADateTime),
                            attrs.GetDateTimeRangeAttribute("a"));
            attrs.Remove(dateTimeAttr);

            Assert.AreEqual(new DateTimeRange(ADate),
                            attrs.GetDateTimeRangeAttribute("a"));
            attrs.Remove(dateAttr);

            Assert.AreEqual(new DateTimeRange(ADateTime, BDateTime),
                            attrs.GetDateTimeRangeAttribute("a"));
            attrs.Remove(dateTimeRangeAttr);

            Assert.AreEqual(null,
                            attrs.GetDateTimeRangeAttribute("a"));
        }

        [Test]
        public void AddDateTimeAttributeTest()
        {
            GBaseAttribute attr = attrs.AddDateTimeAttribute("x", ADateTime);
            Assert.AreEqual(Utilities.LocalDateTimeInUTC(ADateTime),
                            attr.Content);
            Assert.AreEqual(GBaseAttributeType.DateTime, attr.Type);
            Assert.IsTrue(attrs.Contains(attr));
        }

        [Test]
        public void AddDateAttributeTest()
        {
            GBaseAttribute attr = attrs.AddDateAttribute("x", ADateTime);
            Assert.AreEqual(Utilities.LocalDateInUTC(ADateTime),
                            attr.Content);
            Assert.AreEqual(GBaseAttributeType.Date, attr.Type);
            Assert.IsTrue(attrs.Contains(attr));
        }

        [Test]
        public void AddDateTimeRangeAttributeTest()
        {
            DateTimeRange range = new DateTimeRange(ADateTime, BDateTime);
            GBaseAttribute attr = attrs.AddDateTimeRangeAttribute("x", range);
            Assert.AreEqual(range.ToString(),
                            attr.Content);
            Assert.AreEqual(GBaseAttributeType.DateTimeRange, attr.Type);
            Assert.IsTrue(attrs.Contains(attr));
        }

        [Test]
        public void AddEmpytDateTimeRangeAttributeTest()
        {
            try
            {
                attrs.AddDateTimeRangeAttribute("x", new DateTimeRange(ADateTime));
                Assert.Fail("expected exception");
            }
            catch(ArgumentException e)
            {
                Tracing.TraceInfo(e.ToString());
            }
        }
    }

}
