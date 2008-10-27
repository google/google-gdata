using System;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Apps;
using Google.GData.Extensions.Apps;
using Google.GData.Apps.GoogleMailSettings;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class GoogleMailSettingsEntryTest
    {
        private GoogleMailSettingsEntry entry;

        [SetUp]
        public void Init()
        {
            entry = new GoogleMailSettingsEntry();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetPropertiesTest()
        {
            Assert.AreEqual(entry.Properties.Count, 0, "Properties count should initially be zero");            
        }
        
        [Test]
        public void SetPropertyTest()
        {
            entry.Properties.Add(new PropertyElement("name", "value"));            
            Assert.AreEqual(entry.Properties[0].Name, "name", "Property name should be updated after setting");
            Assert.AreEqual(entry.Properties[0].Value, "value", "Property value should be updated after setting");
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

            GoogleMailSettingsEntry newEntry = new GoogleMailSettingsEntry();
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
