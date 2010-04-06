using System;
using System.Text;
using System.Xml;
using System.IO;
using NUnit.Framework;
using Google.GData.Apps;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class NicknameTest
    {
        private NicknameElement nickname;

        [SetUp]
        public void Init()
        {
            nickname = new NicknameElement("foo");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetNameTest()
        {
            Assert.AreEqual("foo", nickname.Name, "Name should initially be foo");
        }

        [Test]
        public void SetNameTest()
        {
            String newName = "bar";
            nickname.Name = newName;
            Assert.AreEqual(nickname.Name, "bar", "Name should be bar after setting");
        }

        [Test]
        public void SaveXmlTest()
        {
            StringWriter outString = new StringWriter();
            XmlWriter writer = new XmlTextWriter(outString);

            nickname.Save(writer);
            writer.Close();

            String expectedXml = "<apps:nickname name=\"" +
                nickname.Name + "\" xmlns:apps=\"http://schemas.google.com/apps/2006\" />";
            Assert.IsTrue(outString.ToString().EndsWith(expectedXml),
                "Serialized XML does not match expected result: " + expectedXml);
        }
    }
}
