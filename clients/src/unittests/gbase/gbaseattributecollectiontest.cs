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

    public class GBaseAttributeCollectionTestBase
    {
        protected readonly GBaseAttribute attr1 =
            new GBaseAttribute("attr1", GBaseAttributeType.Int);
        protected readonly GBaseAttribute attr2 =
            new GBaseAttribute("attr2", GBaseAttributeType.Int);
        protected readonly GBaseAttribute attr3 =
            new GBaseAttribute("attr3", GBaseAttributeType.Int);


        protected ExtensionList extList;
        protected GBaseAttributeCollection attrs;

        [SetUp]
        public virtual void setUp()
        {
            extList = ExtensionList.NotVersionAware();
            attrs = new GBaseAttributeCollection(extList);
        }

        protected void AddThreeAttributes()
        {
            //extList.Add("a");
            extList.Add(attr1);
            //extList.Add("b");
            //extList.Add("c");
            extList.Add(attr2);
            extList.Add(attr3);
            //extList.Add("d");
        }
    }

    [TestFixture]
    [Category("GoogleBase")]
    public class GBaseAttributeCollectionIterationTest
                : GBaseAttributeCollectionTestBase
    {
        [Test]
        public void IterateEmpty()
        {
            Assert.IsFalse(attrs.GetEnumerator().MoveNext());
        }

        [Test]
        public void IterateSkipsNonAttributes()
        {
            AddThreeAttributes();

            IEnumerator enumerator = attrs.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(attr1, enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(attr2, enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(attr3, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void IterateUsingForEach()
        {
            extList.Add(attr1);
            extList.Add(attr2);
            extList.Add(attr3);

            int index = 0;
            foreach (GBaseAttribute attr in attrs)
            {
                Assert.AreSame(extList[index], attr);
                index++;
            }
        }
    }

    [TestFixture]
    [Category("GoogleBase")]
    public class GBaseAttributeCollectionTest
                : GBaseAttributeCollectionTestBase
    {

        private readonly GBaseAttribute aInt
        = new GBaseAttribute("a", GBaseAttributeType.Int);
        private readonly GBaseAttribute aInt2
        = new GBaseAttribute("a", GBaseAttributeType.Int);
        private readonly GBaseAttribute aBool
        = new GBaseAttribute("a", GBaseAttributeType.Boolean);

        [SetUp]
        public override void setUp()
        {
            base.setUp();
            AddThreeAttributes();
        }

        [Test]
        [Ignore("This test is no longer valid since we can't put an item in the list unless it is the correct type.")]
        public void ClearTest()
        {
            attrs.Clear();

            Assert.AreEqual(4, extList.Count, "count");
            Assert.AreEqual("a", extList[0]);
            Assert.AreEqual("b", extList[1]);
            Assert.AreEqual("c", extList[2]);
            Assert.AreEqual("d", extList[3]);
        }

        [Test]
        public void TestGetWithName()
        {
            attrs.Add(aInt);
            attrs.Add(aBool);

            List<GBaseAttribute> got = attrs.GetAttributes("a");
            Assert.AreEqual(2, got.Count, "count");
            Assert.AreEqual(aInt, got[0]);
            Assert.AreEqual(aBool, got[1]);
        }

        [Test]
        public void TestGetWithNameAndType()
        {
            attrs.Add(aInt);
            attrs.Add(aInt2);
            attrs.Add(aBool);

            List<GBaseAttribute> got = attrs.GetAttributes("a", GBaseAttributeType.Int);
            Assert.AreEqual(2, got.Count, "count");
            Assert.AreEqual(aInt, got[0]);
            Assert.AreEqual(aInt2, got[1]);
        }

        [Test]
        public void TestGetOneWithName()
        {
            Assert.AreEqual(attr1, attrs.GetAttribute("attr1"));
            Assert.AreEqual(null, attrs.GetAttribute("attrX"));
        }

        [Test]
        public void TestGetOneWithNameAndType()
        {
            attrs.Add(aInt);
            attrs.Add(aBool);

            Assert.AreEqual(aInt, attrs.GetAttribute("a", GBaseAttributeType.Int));
            Assert.AreEqual(aBool, attrs.GetAttribute("a", GBaseAttributeType.Boolean));
            Assert.AreEqual(null, attrs.GetAttribute("a", GBaseAttributeType.Text));
        }

        [Test]
        public void TestContains()
        {
            Assert.IsTrue(attrs.Contains(attr1));
            Assert.IsTrue(attrs.Contains(attr2));
            Assert.IsTrue(attrs.Contains(attr3));
            Assert.IsFalse(attrs.Contains(aInt));
        }

        public void TestContainsAndEqual()
        {
            GBaseAttribute anAttr =
                new GBaseAttribute("w", GBaseAttributeType.Boolean);
            GBaseAttribute sameAttr =
                new GBaseAttribute("w", GBaseAttributeType.Boolean);

            attrs.Add(anAttr);
            Assert.IsTrue(attrs.Contains(sameAttr));
        }
    }


    [TestFixture]
    [Category("GoogleBase")]
    public class GBaseAttributeCollectionSubtypesTest
                : GBaseAttributeCollectionTestBase
    {

        [SetUp]
        public override void setUp()
        {
            base.setUp();
        }

        [Test]
        public void GetAttributesWithSubtypes()
        {
            GBaseAttribute aInt =
                new GBaseAttribute("a", GBaseAttributeType.Int, "12");
            GBaseAttribute aFloat =
                new GBaseAttribute("a", GBaseAttributeType.Float, "3.14");
            GBaseAttribute aNumber =
                new GBaseAttribute("a", GBaseAttributeType.Number, "2.7");

            attrs.Add(aInt);
            attrs.Add(aFloat);
            attrs.Add(aNumber);

            Assert.AreEqual(3, attrs.GetAttributes("a").Count, "by name");
            Assert.AreEqual(3, attrs.GetAttributes("a", GBaseAttributeType.Number).Count,
                            "type=number");
            Assert.AreEqual(1, attrs.GetAttributes("a", GBaseAttributeType.Float).Count,
                            "type=float");
            Assert.AreEqual(1, attrs.GetAttributes("a", GBaseAttributeType.Int).Count,
                            "type=int");

            Assert.AreEqual(aInt, attrs.GetAttribute("a", GBaseAttributeType.Int));
            Assert.AreEqual(aFloat, attrs.GetAttribute("a", GBaseAttributeType.Float));
            Assert.AreEqual(aInt, attrs.GetAttribute("a", GBaseAttributeType.Number));

            attrs.Remove(aInt);
            Assert.AreEqual(null, attrs.GetAttribute("a", GBaseAttributeType.Int));
            Assert.AreEqual(aFloat, attrs.GetAttribute("a", GBaseAttributeType.Number));

            attrs.Remove(aFloat);
            Assert.AreEqual(null, attrs.GetAttribute("a", GBaseAttributeType.Float));
            Assert.AreEqual(aNumber, attrs.GetAttribute("a", GBaseAttributeType.Number));
        }



    }
}
