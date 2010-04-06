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
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Spreadsheets.UnitTests
{
    [TestFixture]
    [Category("GoogleSpreadsheets")]
    public class CellEntryTest
    {
        private CellEntry entry;
        private CellEntry.CellElement cell;
        private Random rng;

        [SetUp]
        public void Init()
        {
            entry = new CellEntry();
            cell = new CellEntry.CellElement();
            rng = new Random();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetCellTest()
        {
            Assert.IsNotNull(entry.Cell);
        }

        [Test]
        public void SetCellTest()
        {
            entry.Cell = cell;
            Assert.AreEqual(cell, entry.Cell);
        }

        [Test]
        public void SaveAndReadTest()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            uint row = (uint)rng.Next();
            cell.Row = row;
            uint column = (uint)rng.Next();
            cell.Column = column;
            cell.InputValue = "input string";
            cell.NumericValue = "numeric value";
            cell.Value = "display value";
            cell.Save(writer);
            writer.Close();

            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());

            ExtensionElementEventArgs e = new ExtensionElementEventArgs();
            e.ExtensionElement = document.FirstChild;

            entry.Parse(e, new AtomFeedParser());

            Assert.AreEqual(row, entry.Cell.Row, "Rows should be equal");
            Assert.AreEqual(column, entry.Cell.Column, "Columns should be equal");
            Assert.AreEqual("input string", entry.Cell.InputValue, "Input value should be equal");
            Assert.AreEqual("numeric value", entry.Cell.NumericValue, "Numeric value should be equal");
            Assert.AreEqual("display value", entry.Cell.Value, "Cell value should be equal");
        }
    }
}
