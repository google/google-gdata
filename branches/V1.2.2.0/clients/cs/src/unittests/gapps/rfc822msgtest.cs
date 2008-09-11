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
using System.Text;
using System.Xml;
using System.IO;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class Rfc822MsgTest
    {
        private Rfc822MsgElement rfc822Msg;

        public const string xmlElementAsText =
            "<apps:rfc822Msg encoding=\"NONE\" xmlns:apps=\"http://schemas.google.com/apps/2006\">Hi</apps:rfc822Msg>";

        [SetUp]
        public void Init()
        {
            rfc822Msg = new Rfc822MsgElement();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetMessageTest()
        {
            Assert.IsNull(rfc822Msg.Value, "Value should initially be null");
        }

        [Test]
        public void SetMessageByteArrayTest()
        {
            rfc822Msg = new Rfc822MsgElement(new byte[] { (byte)'H', (byte)'i' });
            Assert.AreEqual("Hi", Encoding.ASCII.GetString(rfc822Msg.Value), "Message does not have correct value after setting");
            Assert.AreEqual("Hi", rfc822Msg.ToString(), "Message does not have correct value after setting");
        }

        [Test]
        public void SetMessageStringTest()
        {
            rfc822Msg = new Rfc822MsgElement("Hi");
            Assert.AreEqual("Hi", Encoding.ASCII.GetString(rfc822Msg.Value), "Message does not have correct value after setting");
            Assert.AreEqual("Hi", rfc822Msg.ToString(), "Message does not have correct value after setting");
        }

        [Test]
        public void GetEncodingTest()
        {
            Assert.AreEqual(Rfc822MsgElement.EncodingMethod.NONE, rfc822Msg.MessageEncoding,
                "Message encoding should initially be EncodingMethod.NONE");
        }

        [Test]
        public void SetEncodingTest()
        {
            rfc822Msg = new Rfc822MsgElement(Encoding.ASCII.GetBytes("SGVsbG8h"), Rfc822MsgElement.EncodingMethod.BASE64);
            Assert.AreEqual(Rfc822MsgElement.EncodingMethod.BASE64, rfc822Msg.MessageEncoding,
                "Message does not have correct encoding after setting");
        }

        [Test]
        public void ParseXmlTest()
        {
            // Test parsing an XML document into an Rfc822MsgElement.
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlElementAsText);

            rfc822Msg = rfc822Msg.CreateInstance(document.FirstChild,
                new AtomFeedParser()) as Rfc822MsgElement;

            Assert.IsNotNull(rfc822Msg, "Parsed Rfc822MsgElement should not be null");
            Assert.AreEqual("Hi", Encoding.ASCII.GetString(rfc822Msg.Value), "Message does not have correct value after setting");
        }

        [Test]
        public void SaveXmlTest()
        {
            // Test saving an Rfc822MsgElement using an XmlWriter.
            rfc822Msg = new Rfc822MsgElement("Hi");

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            rfc822Msg.Save(writer);
            writer.Close();

            Assert.AreEqual(xmlElementAsText, sb.ToString(), "Saved XML form of Rfc822MsgElement is not correct");
        }
    }
}
