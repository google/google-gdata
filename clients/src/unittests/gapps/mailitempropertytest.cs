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

using System;
using NUnit.Framework;
using System.Text;
using System.Xml;
using System.IO;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class MailItemPropertyTest
    {
        private MailItemPropertyElement mailItemProperty;

        public const string xmlElementAsText =
            "<apps:mailItemProperty value=\"IS_SENT\" xmlns:apps=\"http://schemas.google.com/apps/2006\" />";

        [SetUp]
        public void Init()
        {
            mailItemProperty = MailItemPropertyElement.DRAFT;
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual("IS_DRAFT", mailItemProperty.Value.ToString(),
                "Mail item property's value should be IS_DRAFT");
        }

        [Test]
        public void SetValueTest()
        {
            mailItemProperty = new MailItemPropertyElement(MailItemPropertyElement.MailItemProperty.IS_TRASH);
            Assert.AreEqual("IS_TRASH", mailItemProperty.Value.ToString(),
                "Mail item property's value should be IS_TRASH");
        }

        [Test]
        public void ParseTest()
        {
            // Test parsing an XML document into a MailItemPropertyElement.
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlElementAsText);

            mailItemProperty.ProcessAttributes(document.FirstChild);

            Assert.IsNotNull(mailItemProperty, "Parsed MailItemPropertyElement should not be null");
            Assert.AreEqual("IS_SENT", mailItemProperty.Value.ToString(),
                "Mail item property's value should be IS_SENT");
        }

        [Test]
        public void SaveTest()
        {
            mailItemProperty = MailItemPropertyElement.SENT;

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            mailItemProperty.Save(writer);
            writer.Close();

            Assert.AreEqual(xmlElementAsText, sb.ToString(),
                "Saved XML form of MailItemPropertyElement does not match expected value");
        }
    }
}
