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
    public class QuotaTest
    {
        private QuotaElement quota;
        private Random rng;

        [SetUp]
        public void Init()
        {
            quota = new QuotaElement(0);
            rng = new Random();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetLimitTest()
        {
            Assert.AreEqual(0, quota.Limit, "Quota limit should initially be 0");
        }

        [Test]
        public void SetLimitTest()
        {
            int newLimit = rng.Next();
            quota.Limit = newLimit;
            Assert.AreEqual(newLimit, quota.Limit,
                "Quota limit should be " + newLimit.ToString() + " after setting");
        }

        [Test]
        public void SaveXmlTest()
        {
            StringWriter outString = new StringWriter();
            XmlWriter writer = new XmlTextWriter(outString);

            quota.Limit = 2048;
            quota.Save(writer);
            writer.Close();

            String expectedXml = "<apps:quota limit=\"" +
                quota.Limit.ToString() + "\" xmlns:apps=\"http://schemas.google.com/apps/2006\" />";
            Assert.IsTrue(outString.ToString().EndsWith(expectedXml),
                "Serialized XML does not match expected result: " + expectedXml);
        }
    }
}
