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
    public class EmailListTest
    {
        private EmailListElement emailList;

        [SetUp]
        public void Init()
        {
            emailList = new EmailListElement("testList");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetNameTest()
        {
            Assert.AreEqual("testList", emailList.Name, "List name should initially be testList");
        }

        [Test]
        public void SetNameTest()
        {
            String newName = "newListName";
            emailList.Name = newName;
            Assert.AreEqual(emailList.Name, "newListName", "List name should be newListName after setting");
        }

        [Test]
        public void SaveXmlTest()
        {
            StringWriter outString = new StringWriter();
            XmlWriter writer = new XmlTextWriter(outString);

            emailList.Save(writer);
            writer.Close();

            String expectedXml = "<apps:emailList name=\"" +
                emailList.Name + "\" xmlns:apps=\"http://schemas.google.com/apps/2006\" />";
            Assert.IsTrue(outString.ToString().EndsWith(expectedXml),
                "Serialized XML does not match expected result: " + expectedXml);
        }
    }
}
