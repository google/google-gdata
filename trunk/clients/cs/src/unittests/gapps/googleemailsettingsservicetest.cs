using System;
using System.Text;
using System.Xml;
using System.IO;
using NUnit.Framework;
using Google.GData.Apps.GoogleMailSettings;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class GoogleMailSettingsServiceTest
    {
        private GoogleMailSettingsService service;

        [SetUp]
        public void Init()
        {
            service = new GoogleMailSettingsService("example.com", "apps-example.com");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetDomainTest()
        {
            Assert.AreEqual("example.com", service.Domain, "Domain should initially be example.com");
        }
    }
}
