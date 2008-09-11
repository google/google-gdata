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
    public class WorksheetEntryTest
    {
        private WorksheetEntry entry;

        [SetUp]
        public void Init()
        {
            entry = new WorksheetEntry();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetColCountTest()
        {
            Assert.IsNull(entry.ColCount);
        }

        [Test]
        public void GetRowCountTest()
        {
            Assert.IsNull(entry.RowCount);
        }

        [Test]
        public void SetColCountTest()
        {
            ColCountElement count = new ColCountElement();
            count.Count = (uint)new Random().Next();
            entry.ColCount = count;

            Assert.IsNotNull(entry.ColCount);
            Assert.AreEqual(entry.ColCount.Count, count.Count);
        }

        [Test]
        public void SetRowCountTest()
        {
            RowCountElement count = new RowCountElement();
            count.Count = (uint)new Random().Next();
            entry.RowCount = count;

            Assert.IsNotNull(entry.RowCount);
            Assert.AreEqual(entry.RowCount.Count, count.Count);
        }

        [Test]
        public void SaveAndReadColTest()
        {
            ColCountElement count = new ColCountElement();
            count.Count = (uint)new Random().Next();

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            count.Save(writer);
            writer.Close();

            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());

            ExtensionElementEventArgs e = new ExtensionElementEventArgs();
            e.ExtensionElement = document.FirstChild;
            entry.Parse(e, new AtomFeedParser());

            Assert.IsNotNull(entry.ColCount);
            Assert.AreEqual(entry.ColCount.Count, count.Count);
        }

        [Test]
        public void SaveAndReadRowTest()
        {
            RowCountElement count = new RowCountElement();
            count.Count = (uint)new Random().Next();

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            count.Save(writer);
            writer.Close();

            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());

            ExtensionElementEventArgs e = new ExtensionElementEventArgs();
            e.ExtensionElement = document.FirstChild;
            entry.Parse(e, new AtomFeedParser());

            Assert.IsNotNull(entry.RowCount);
            Assert.AreEqual(entry.RowCount.Count, count.Count);
        }
    }
}
