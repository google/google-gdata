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
    public class UserEntryTest
    {
        private UserEntry entry;

        [SetUp]
        public void Init()
        {
            entry = new UserEntry();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetLoginTest()
        {
            Assert.IsNull(entry.Login, "Login should initially be null");
        }

        [Test]
        public void SetLoginTest()
        {
            LoginElement login = new LoginElement("jdoe");
            entry.Login = login;
            Assert.AreEqual(login, entry.Login, "Login should be updated after setting");
        }

        [Test]
        public void GetQuotaTest()
        {
            Assert.IsNull(entry.Quota, "Quota should initially be null");
        }

        [Test]
        public void SetQuotaTest()
        {
            QuotaElement quota = new QuotaElement(2048);
            entry.Quota = quota;
            Assert.AreEqual(quota, entry.Quota, "Quota should be updated after setting");
        }

        [Test]
        public void GetNameTest()
        {
            Assert.IsNull(entry.Name, "Name should initially be null");
        }

        [Test]
        public void SetNameTest()
        {
            NameElement name = new NameElement("Doe", "John");
            entry.Name = name;
            Assert.AreEqual(name, entry.Name, "Name should be updated after setting");
        }

        [Test]
        public void SaveAndReadTest()
        {
            LoginElement login = new LoginElement("jdoe");
            login.Admin = true;
            login.HashFunctionName = "SHA-1";
            entry.Login = login;

            QuotaElement quota = new QuotaElement(2048);
            entry.Quota = quota;

            NameElement name = new NameElement("Doe", "John");
            entry.Name = name;

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            entry.SaveToXml(writer);
            writer.Close();

            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());

            UserEntry newEntry = new UserEntry();
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                ExtensionElementEventArgs args = new ExtensionElementEventArgs();
                args.ExtensionElement = node;
                args.Base = newEntry;
                newEntry.Parse(args, new AtomFeedParser());
            }

            Assert.AreEqual(login.UserName, newEntry.Login.UserName,
                "Parsed entry should have same username as original entry");
            Assert.IsTrue(newEntry.Login.Admin,
                "Parsed entry should have admin property set to true");
            Assert.AreEqual(login.HashFunctionName, newEntry.Login.HashFunctionName,
                "Parsed entry should have same hash function name as original entry");
            Assert.AreEqual(quota.Limit, newEntry.Quota.Limit,
                "Parsed entry should have same quota as original entry");
            Assert.AreEqual(name.FamilyName, newEntry.Name.FamilyName,
                "Parsed entry should have same family name as original entry");
            Assert.AreEqual(name.GivenName, newEntry.Name.GivenName,
                "Parsed entry should have same given name as original entry");
        }
    }
}