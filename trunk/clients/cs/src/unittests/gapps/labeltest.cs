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
using System.IO;
using System.Text;
using System.Xml;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class LabelTest
    {
        private LabelElement label;

        public const string xmlElementAsText =
            "<apps:label labelName=\"Friends\" xmlns:apps=\"http://schemas.google.com/apps/2006\" />";

        [SetUp]
        public void Init()
        {
            label = new LabelElement();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetLabelNameTest()
        {
            Assert.IsTrue(label.LabelName.Length ==0,  "Label name should initially be empty");
        }

        [Test]
        public void SetLabelNameTest()
        {
            label = new LabelElement("Friends");
            Assert.AreEqual("Friends", label.LabelName,
                "Label does not have correct value after setting");
        }

        [Test]
        public void ParseTest()
        {
            // Test parsing an XML document into a LabelElement.
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlElementAsText);

            label.ProcessAttributes(document.FirstChild);

            Assert.IsNotNull(label, "Parsed LabelElement should not be null");
            Assert.AreEqual("Friends", label.LabelName, "Label does not have correct labelName property after parsing");
        }

        [Test]
        public void SaveTest()
        {
            label = new LabelElement("Friends");

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            label.Save(writer);
            writer.Close();

            Assert.AreEqual(xmlElementAsText, sb.ToString(),
                "Saved XML form of LabelElement does not match expected value");
        }
    }
}
