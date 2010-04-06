using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Apps;
using Google.GData.Apps.AdminSettings;
using Google.GData.Extensions.Apps;


namespace Google.GData.Apps.UnitTests
{
    class AdminSettingsEntryTest
    {
        private AdminSettingsEntry entry;

        [SetUp]
        public void Init()
        {
            entry = new AdminSettingsEntry();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void SaveAndReadTest()
        {
            entry.Properties.Add(new PropertyElement("name", "value"));

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            entry.SaveToXml(writer);
            writer.Close();

            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());

            AdminSettingsEntry newEntry = new AdminSettingsEntry();
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                ExtensionElementEventArgs args = new ExtensionElementEventArgs();
                args.ExtensionElement = node;
                args.Base = newEntry;
                newEntry.Parse(args, new AtomFeedParser());
            }

            Assert.AreEqual(entry.Properties[0].Name, newEntry.Properties[0].Name,
                "Parsed entry should have same name for property[0] as original entry");
            Assert.AreEqual(entry.Properties[0].Value, newEntry.Properties[0].Value,
                "Parsed entry should have same value for property[0] as original entry");
        }
    }
}
