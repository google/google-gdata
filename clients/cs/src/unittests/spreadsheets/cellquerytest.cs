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
    public class CellQueryTest
    {
        private CellQuery query;
        private Random rng;

        [SetUp]
        public void Init()
        {
            query = new CellQuery("");
            rng = new Random();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetMinimumRowTest()
        {
            Assert.AreEqual(uint.MinValue, query.MinimumRow);
        }

        [Test]
        public void GetMaximumRowTest()
        {
            Assert.AreEqual(uint.MaxValue, query.MaximumRow);
        }

        [Test]
        public void GetMinimumColumnTest()
        {
            Assert.AreEqual(uint.MinValue, query.MinimumColumn);
        }

        [Test]
        public void GetMaximumColumnTest()
        {
            Assert.AreEqual(uint.MaxValue, query.MaximumColumn);
        }

        [Test]
        public void SetMinimumRowZeroTest()
        {
            query.MinimumRow = 0;
            Assert.AreEqual(0, query.MinimumRow);
        }

        [Test]
        public void SetMinimumRowOneTest()
        {
            query.MinimumRow = 1;
            Assert.AreEqual(1, query.MinimumRow);
        }

        [Test]
        public void SetMinimumRowRandomTest()
        {
            uint number = (uint)rng.Next();
            query.MinimumRow = number;
            Assert.AreEqual(number, query.MinimumRow);
        }

        [Test]
        public void SetMinimumRowMaxTest()
        {
            query.MinimumRow = uint.MaxValue;
            Assert.AreEqual(uint.MaxValue, query.MinimumRow);
        }

        [Test]
        public void SetMaximumRowZeroTest()
        {
            query.MaximumRow = 0;
            Assert.AreEqual(0, query.MaximumRow);
        }

        [Test]
        public void SetMaximumRowOneTest()
        {
            query.MaximumRow = 1;
            Assert.AreEqual(1, query.MaximumRow);
        }

        [Test]
        public void SetMaximumRowRandomTest()
        {
            uint number = (uint)rng.Next();
            query.MaximumRow = number;
            Assert.AreEqual(number, query.MaximumRow);
        }

        [Test]
        public void SetMaximumRowMaxTest()
        {
            query.MaximumRow = uint.MaxValue;
            Assert.AreEqual(uint.MaxValue, query.MaximumRow);
        }

        [Test]
        public void SetMinimumColumnZeroTest()
        {
            query.MinimumColumn = 0;
            Assert.AreEqual(0, query.MinimumColumn);
        }

        [Test]
        public void SetMinimumColumnOneTest()
        {
            query.MinimumColumn = 1;
            Assert.AreEqual(1, query.MinimumColumn);
        }

        [Test]
        public void SetMinimumColumnRandomTest()
        {
            uint number = (uint)rng.Next();
            query.MinimumColumn = number;
            Assert.AreEqual(number, query.MinimumColumn);
        }

        [Test]
        public void SetMinimumColumnMaxTest()
        {
            query.MinimumColumn = uint.MaxValue;
            Assert.AreEqual(uint.MaxValue, query.MinimumColumn);
        }

        [Test]
        public void SetMaximumColumnZeroTest()
        {
            query.MaximumColumn = 0;
            Assert.AreEqual(0, query.MaximumColumn);
        }

        [Test]
        public void SetMaximumColumnOneTest()
        {
            query.MaximumColumn = 1;
            Assert.AreEqual(1, query.MaximumColumn);
        }

        [Test]
        public void SetMaximumColumnRandomTest()
        {
            uint number = (uint)rng.Next();
            query.MaximumColumn = number;
            Assert.AreEqual(number, query.MaximumColumn);
        }

        [Test]
        public void SetMaximumColumnMaxTest()
        {
            query.MaximumColumn = uint.MaxValue;
            Assert.AreEqual(uint.MaxValue, query.MaximumColumn);
        }

        [Test]
        public void SetBoundsDifferentTest()
        {
            // This avoids typos!
            query.MaximumColumn = 256;
            query.MaximumRow = 10000;
            query.MinimumColumn = 0;
            query.MinimumRow = 1;

            Assert.IsTrue(query.MaximumColumn != query.MaximumRow);
            Assert.IsTrue(query.MaximumColumn != query.MinimumColumn);
            Assert.IsTrue(query.MaximumColumn != query.MinimumRow);
            Assert.IsTrue(query.MaximumRow !=  query.MinimumColumn);
            Assert.IsTrue(query.MaximumRow != query.MinimumRow);
            Assert.IsTrue(query.MinimumColumn != query.MinimumRow);
            Assert.AreEqual(query.MaximumColumn, 256);
            Assert.AreEqual(query.MaximumRow, 10000);
            Assert.AreEqual(query.MinimumColumn, 0);
            Assert.AreEqual(query.MinimumRow, 1);
        }

        [Test]
        public void SetReturnEmptyTrueTest()
        {
            query.ReturnEmpty = ReturnEmptyCells.yes;
            Assert.IsTrue(query.ReturnEmpty == ReturnEmptyCells.yes);
        }

        [Test]
        public void SetReturnEmptyFalseTest()
        {
            query.ReturnEmpty = ReturnEmptyCells.no;
            Assert.IsTrue(query.ReturnEmpty == ReturnEmptyCells.no);
        }
        [Test]
        public void SetReturnEmptyDefaultTest()
        {
            query.ReturnEmpty = ReturnEmptyCells.serverDefault;
            Assert.IsTrue(query.ReturnEmpty == ReturnEmptyCells.serverDefault);
        }
    }
}
