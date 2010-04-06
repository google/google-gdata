/* Copyright (c) 2006-2008 Google Inc.
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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/
using System;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Apps;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class EmailListEntryTest
    {
        private EmailListEntry entry;

        [SetUp]
        public void Init()
        {
            entry = new EmailListEntry();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetEmailListTest()
        {
            Assert.IsNull(entry.EmailList, "Email list element should initially be null");
        }

        [Test]
        public void SetEmailListTest()
        {
            EmailListElement emailList = new EmailListElement("foo");
            entry.EmailList = emailList;
            Assert.AreEqual(emailList, entry.EmailList, "Email list element should be updated after setting");
        }

        [Test]
        public void SaveAndReadTest()
        {
            EmailListElement emailList = new EmailListElement("foo");
            entry.EmailList = emailList;

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            entry.SaveToXml(writer);
            writer.Close();

            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());

            EmailListEntry newEntry = new EmailListEntry();
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                ExtensionElementEventArgs args = new ExtensionElementEventArgs();
                args.ExtensionElement = node;
                args.Base = newEntry;
                newEntry.Parse(args, new AtomFeedParser());
            }

            Assert.AreEqual(emailList.Name, newEntry.EmailList.Name,
                "Parsed entry should have same email list name as original entry");
        }
    }
}