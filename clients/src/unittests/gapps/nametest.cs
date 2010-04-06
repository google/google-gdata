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
    public class NameTest
    {
        private NameElement name;

        [SetUp]
        public void Init()
        {
            name = new NameElement("Doe", "John");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetFamilyNameTest()
        {
            Assert.AreEqual("Doe", name.FamilyName, "Family name should initially be Doe");
        }

        [Test]
        public void SetFamilyNameTest()
        {
            String newFamilyName = "Smith";
            name.FamilyName = newFamilyName;
            Assert.AreEqual("Smith", name.FamilyName, "Family name should be Smith after setting");
        }

        [Test]
        public void GetGivenNameTest()
        {
            Assert.AreEqual("John", name.GivenName, "Given name should initially be John");
        }

        [Test]
        public void SetGivenNameTest()
        {
            String newGivenName = "Jane";
            name.GivenName = newGivenName;
            Assert.AreEqual("Jane", name.GivenName, "Given name should be Jane after setting");
        }

        [Test]
        public void SaveXmlTest()
        {
            StringWriter outString = new StringWriter();
            XmlWriter writer = new XmlTextWriter(outString);

            name.Save(writer);
            writer.Close();

            String expectedXml = "<apps:name familyName=\"" + name.FamilyName +
                "\" givenName=\"" + name.GivenName +
                "\" xmlns:apps=\"http://schemas.google.com/apps/2006\" />";

            Assert.IsTrue(outString.ToString().EndsWith(expectedXml),
                "Serialized XML does not match expected result: " + expectedXml);
        }
    }
}
