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
    public class NicknameEntryTest
    {
        private NicknameEntry entry;

        [SetUp]
        public void Init()
        {
            entry = new NicknameEntry();
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
        public void GetNicknameTest()
        {
            Assert.IsNull(entry.Nickname, "Nickname should initially be null");
        }

        [Test]
        public void SetNicknameTest()
        {
            NicknameElement nickname = new NicknameElement("john");
            entry.Nickname = nickname;
            Assert.AreEqual(nickname, entry.Nickname, "Nickname should be updated after setting");
        }

        [Test]
        public void SaveAndReadTest()
        {
            LoginElement login = new LoginElement("jdoe");
            entry.Login = login;

            NicknameElement nickname = new NicknameElement("john");
            entry.Nickname = nickname;

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = new XmlTextWriter(new StringWriter(sb));
            entry.SaveToXml(writer);
            writer.Close();

            XmlDocument document = new XmlDocument();
            document.LoadXml(sb.ToString());

            NicknameEntry newEntry = new NicknameEntry();
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                ExtensionElementEventArgs args = new ExtensionElementEventArgs();
                args.ExtensionElement = node;
                args.Base = newEntry;
                newEntry.Parse(args, new AtomFeedParser());
            }

            Assert.AreEqual(login.UserName, newEntry.Login.UserName,
                "Parsed entry should have same username as original entry");
            Assert.AreEqual(nickname.Name, newEntry.Nickname.Name,
                "Parsed entry should have same nickname as original entry");
        }
    }
}