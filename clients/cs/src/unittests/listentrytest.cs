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
    public class ListEntryTest
    {
        private ListEntry entry;

        [SetUp]
        public void Init()
        {
            entry = new ListEntry();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void SaveAndReadOneTest()
        {
            ListEntry.Custom element = new ListEntry.Custom();
            element.LocalName = "local_name";
            element.Value = "value";

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            element.Save(writer);
            writer.Close();

            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());

            ExtensionElementEventArgs e = new ExtensionElementEventArgs();
            e.ExtensionElement = document.FirstChild;

            entry.Parse(e, new AtomFeedParser());

            Assert.AreEqual(1, entry.Elements.Count);
            Assert.AreEqual(element.LocalName, entry.Elements[0].LocalName);
            Assert.AreEqual(element.Value, entry.Elements[0].Value);
        }

        [Test]
        public void SaveAndReadMultipleTest()
        {
            for (int i = 0; i < 256; i++)
            {
                ListEntry.Custom element = new ListEntry.Custom();
                element.LocalName = "local_name_" + i;
                element.Value = "value_" + i;

                StringBuilder sb = new StringBuilder();
                XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
                element.Save(writer);
                writer.Close();

                XmlDocument document = new XmlDocument();
                document.LoadXml(sb.ToString());

                ExtensionElementEventArgs e = new ExtensionElementEventArgs();
                e.ExtensionElement = document.FirstChild;
                entry.Parse(e, new AtomFeedParser());


                Assert.AreEqual(i + 1, entry.Elements.Count);
                Assert.AreEqual(element.LocalName, entry.Elements[i].LocalName);
                Assert.AreEqual(element.Value, entry.Elements[i].Value);
            }   
        }
    }
}
