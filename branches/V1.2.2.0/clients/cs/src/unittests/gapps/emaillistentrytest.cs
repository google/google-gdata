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