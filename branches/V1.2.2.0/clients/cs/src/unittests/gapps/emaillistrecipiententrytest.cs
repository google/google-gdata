using System;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using Google.GData.Apps;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class EmailListRecipientEntryTest
    {
        private EmailListRecipientEntry entry;

        [SetUp]
        public void Init()
        {
            entry = new EmailListRecipientEntry();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetRecipientTest()
        {
            Assert.IsNull(entry.Recipient, "Recipient should initially be null");
        }

        [Test]
        public void SetEmailListTest()
        {
            Who recipient = new Who();
            entry.Recipient = recipient;
            Assert.AreEqual(recipient, entry.Recipient, "Recipient should be updated after setting");
        }

        [Test]
        public void SaveAndReadTest()
        {
            Who recipient = new Who();
            recipient.Email = "joe@example.com";
            recipient.Rel = Who.RelType.MESSAGE_TO;
            entry.Recipient = recipient;

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            entry.SaveToXml(writer);
            writer.Close();

            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());

            EmailListRecipientEntry newEntry = new EmailListRecipientEntry();

            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                ExtensionElementEventArgs args = new ExtensionElementEventArgs();
                args.ExtensionElement = node; 
                args.Base = newEntry;
                newEntry.Parse(args, new AtomFeedParser());
            }

            Assert.AreEqual(recipient.Email, newEntry.Recipient.Email,
                "Parsed entry should have same recipient as original entry");
        }
    }
}