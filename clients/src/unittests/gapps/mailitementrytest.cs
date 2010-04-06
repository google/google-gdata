/* Copyright (c) 2007 Google Inc.
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

using Google.GData.Apps;
using Google.GData.Apps.Migration;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.Apps;
using System;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class MailItemEntryTest
    {
        private MailItemEntry entry;

        [SetUp]
        public void Init()
        {
            entry = new MailItemEntry();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetRfc822MsgTest()
        {
            Assert.IsNull(entry.Rfc822Msg, "Rfc822Msg should initially be null after construction");
        }

        [Test]
        public void SetRfc822MsgTest()
        {
            entry.Rfc822Msg = new Rfc822MsgElement("Hi");

            Assert.IsNotNull(entry.Rfc822Msg, "Rfc822Msg should not be null after setting");
            Assert.AreEqual("Hi", entry.Rfc822Msg.ToString(),
                "Entry's Rfc822Msg does not have correct value after setting");
        }

        [Test]
        public void GetLabelsTest()
        {
            Assert.IsNotNull(entry.Labels, "Labels should initially not be null after construction");
        }

        [Test]
        public void AddLabelTest()
        {
            LabelElement label = new LabelElement("Friends");
            entry.Labels.Add(label);

            Assert.AreEqual(1, entry.Labels.Count, "Entry's label collection should have size 1 after insertion");
            Assert.IsTrue(entry.Labels.Contains(label), "Entry's label collection does not contain label after insertion");
        }

        [Test]
        public void GetMailItemPropertiesTest()
        {
            Assert.IsNotNull(entry.MailItemProperties,
                "MailItemProperties should initially not be null after construction");
        }

        [Test]
        public void AddMailItemPropertiesTest()
        {
            entry.MailItemProperties.Add(MailItemPropertyElement.INBOX);

            Assert.AreEqual(1, entry.MailItemProperties.Count,
                "Entry's mail item property collection should have size 1 after insertion");
            Assert.IsTrue(entry.MailItemProperties.Contains(MailItemPropertyElement.INBOX),
                "Entry's mail item property collection does not contain INBOX property after insertion");
        }

        [Test]
        public void ParseXmlTest()
        {
            string xmlElementText = "<entry xmlns=\"http://www.w3.org/2005/Atom\" " +
                "xmlns:gd=\"http://schemas.google.com/g/2005\">" + Rfc822MsgTest.xmlElementAsText +
                LabelTest.xmlElementAsText + LabelTest.xmlElementAsText.Replace("Friends", "Acquaintances") +
                MailItemPropertyTest.xmlElementAsText +
                MailItemPropertyTest.xmlElementAsText.Replace("SENT", "STARRED") +
                "<gd:when startTime=\"2005-10-06\"/></entry>";

            // Parse the XML back into a new MigrationEntry
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlElementText);

            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                ExtensionElementEventArgs e = new ExtensionElementEventArgs();
                e.ExtensionElement = node;
                entry.Parse(e, new AtomFeedParser());
            }

            // Verify that the labels are correct
            Assert.AreEqual(2, entry.Labels.Count, "Parsed MailItemEntry should have exactly two labels");
            Assert.AreEqual("Friends", entry.Labels[0].LabelName,
                "Parsed MailItemEntry does not contain a \"Friends\" label");
            Assert.AreEqual("Acquaintances", entry.Labels[1].LabelName,
                "Parsed MailItemEntry does not contain an \"Acquaintances\" label");

            // Verify that the mail item properties are correct
            Assert.AreEqual(2, entry.MailItemProperties.Count,
                "Parsed MailItemEntry should have exactly two mail item properties");
            Assert.AreEqual("IS_SENT", entry.MailItemProperties[0].Value.ToString(),
                "Parsed MailItemEntry does not contain a SENT property.");
            Assert.AreEqual("IS_STARRED", entry.MailItemProperties[1].Value.ToString(),
                "Parsed MailItemEntry does not contain the STARRED property.");

            // Verify that the RFC822 message is correct
            Assert.AreEqual("Hi", entry.Rfc822Msg.ToString(), "Parsed MailItemEntry has incorrect Rfc822Msg");
        }

        [Test]
        public void SaveXmlTest()
        {
            // Set up the MailItemEntry.
            entry.Rfc822Msg = new Rfc822MsgElement("Hi");
            entry.Labels.Add(new LabelElement("Friends"));
            entry.Labels.Add(new LabelElement("Acquaintances"));
            entry.MailItemProperties.Add(MailItemPropertyElement.SENT);
            entry.MailItemProperties.Add(MailItemPropertyElement.TRASH);

            // Save it as a string using an XmlWriter.
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            entry.SaveToXml(writer);
            writer.Close();

            string savedXml = sb.ToString();

            // Assertions
            Assert.IsTrue(savedXml.IndexOf(Rfc822MsgTest.xmlElementAsText) != 0,
                "Saved XML form of MailItemEntry does not contain correct rfc822Msg element");
            Assert.IsTrue(savedXml.IndexOf(LabelTest.xmlElementAsText) != 0,
                "Saved XML form of MailItemEntry does not contain \"Friends\" label element");
            Assert.IsTrue(savedXml.IndexOf(LabelTest.xmlElementAsText.Replace("Friends", "Acquaintances"))!=0,
                "Saved XML form of MailItemEntry does not contain \"Acquaintances\" label element");
            Assert.IsTrue(savedXml.IndexOf(MailItemPropertyTest.xmlElementAsText) !=0,
                "Saved XML form of MailItemEntry does not contain SENT property");
            Assert.IsTrue(savedXml.IndexOf(MailItemPropertyTest.xmlElementAsText.Replace("SENT", "TRASH")) !=0,
                "Saved XML form of MailItemEntry does not contain TRASH property");
        }
    }
}
