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
using Google.GData.Spreadsheets;

namespace Google.GData.Spreadsheets.UnitTests
{
    [TestFixture]
    [Category("GoogleSpreadsheets")]
    public class ListQueryTest
    {
        private ListQuery query;

        [SetUp]
        public void Init()
        {
            query = new ListQuery("");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void SetSpreadsheetQuerySingleWordTest()
        {
            query.SpreadsheetQuery = "singleWord";
            Assert.AreEqual("singleWord", query.SpreadsheetQuery);
        }

        [Test]
        public void SetSpreadsheetQueryMultipleWordsTest()
        {
            query.SpreadsheetQuery = "multiple word query";
            Assert.AreEqual("multiple word query", query.SpreadsheetQuery);
        }

        [Test]
        public void SetSpreadsheetQueryValidTest()
        {
            query.SpreadsheetQuery = "name = \"sonja\" and state == \"Georgia\"";
            Assert.AreEqual("name = \"sonja\" and state == \"Georgia\"", query.SpreadsheetQuery);
        }

        [Test]
        public void SetSpreadsheetQueryNullTest()
        {
            query.SpreadsheetQuery = null;
            Assert.IsNull(query.SpreadsheetQuery);
        }

        [Test]
        public void GetSpreadsheetQueryTest()
        {
            Assert.IsNull(query.SpreadsheetQuery);
        }

        [Test]
        public void GetReverseTest()
        {
            Assert.IsFalse(query.Reverse);
        }

        [Test]
        public void SetReverseTrueTest()
        {
            query.Reverse = true;
            Assert.IsFalse(query.Reverse);
        }

        [Test]
        public void SetReversePositionTrueTest()
        {
            query.OrderByPosition = true;
            Assert.IsFalse(query.Reverse);
            query.Reverse = true;
            Assert.IsTrue(query.Reverse);
        }

        [Test]
        public void SetReverseColumnTrueTest()
        {
            query.OrderByColumn = "column";
            Assert.IsFalse(query.Reverse);
            query.Reverse = true;
            Assert.IsTrue(query.Reverse);
        }

        [Test]
        public void GetOrderByPositionTest()
        {
            Assert.IsFalse(query.OrderByPosition);
        }

        [Test]
        public void SetOrderByPositionTrueTest()
        {
            query.OrderByPosition = true;
            Assert.IsTrue(query.OrderByPosition);
        }

        [Test]
        public void GetOrderByColumnTest()
        {
            Assert.IsNull(query.OrderByColumn);
        }

        [Test]
        public void SetOrderByColumnTest()
        {
            query.OrderByColumn = "column";
            Assert.AreEqual("column", query.OrderByColumn);
        }

        [Test]
        public void SetOrderByPositionThenColumnTest()
        {
            query.OrderByPosition = true;
            query.OrderByColumn = "column";
            Assert.IsFalse(query.OrderByPosition);
            Assert.AreEqual("column", query.OrderByColumn);
        }

        [Test]
        public void SetOrderByColumnThenPositionTest()
        {
            query.OrderByColumn = "column";
            query.OrderByPosition = true;
            Assert.IsNull(query.OrderByColumn);
            Assert.IsTrue(query.OrderByPosition);
        }

        [Test]
        public void SetOrderByPositionThenColumnNullTest()
        {
            query.OrderByPosition = true;
            query.OrderByColumn = null;
            Assert.IsNull(query.OrderByColumn);
            Assert.IsTrue(query.OrderByPosition);
        }

        [Test]
        public void SetOrderByColumnThenPostionFalseTest()
        {
            query.OrderByColumn = "column";
            query.OrderByPosition = false;
            Assert.IsFalse(query.OrderByPosition);
            Assert.AreEqual("column", query.OrderByColumn);
        }   

    }
}
