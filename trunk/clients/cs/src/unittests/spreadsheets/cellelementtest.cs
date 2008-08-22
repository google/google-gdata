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
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Spreadsheets.UnitTests
{
    [TestFixture]
    [Category("GoogleSpreadsheets")]
    public class CellElementTest
    {
        private CellEntry.CellElement cell;
        private Random rng;

        [SetUp]
        public void Init()
        {
            cell = new CellEntry.CellElement();
            rng = new Random();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetRowTest()
        {
            Assert.AreEqual(0, cell.Row);
        }

        [Test]
        public void SetRowTest()
        {
            uint row = (uint)rng.Next();
            cell.Row = row;
            Assert.AreEqual(row, cell.Row);
        }

        [Test]
        public void GetColumnTest()
        {
            Assert.AreEqual(0, cell.Column);
        }

        [Test]
        public void SetColumnTest()
        {
            uint column = (uint)rng.Next();
            cell.Column = column;
            Assert.AreEqual(column, cell.Column);
        }

        [Test]
        public void GetInputValueTest()
        {
            Assert.AreEqual(cell.InputValue, null);
        }

        [Test]
        public void SetInputValueTest()
        {
            cell.InputValue = "input string";
            Assert.AreEqual("input string", cell.InputValue);
        }

        [Test]
        public void SetInputValueNullTest()
        {
            cell.InputValue = null;
            Assert.IsNull(cell.InputValue);
        }

        [Test]
        public void GetNumericValueTest()
        {
            Assert.AreEqual(null, cell.NumericValue);
        } 

        [Test]
        public void SetNumericValueTest()
        {
            String numericValue = rng.Next().ToString();
            cell.NumericValue = numericValue;
            Assert.AreEqual(numericValue, cell.NumericValue);
        }

        [Test]
        public void GetValueTest()
        {
            Assert.IsNull(cell.Value);
        }

        [Test]
        public void SetValueTest()
        {
            cell.Value = "some value";
            Assert.AreEqual("some value", cell.Value);
        }
    }
}
