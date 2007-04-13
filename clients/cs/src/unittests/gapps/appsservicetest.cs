using System;
using System.Text;
using System.Xml;
using System.IO;
using NUnit.Framework;
using Google.GData.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class AppsServiceTest
    {
        private AppsService service;

        [SetUp]
        public void Init()
        {
            service = new AppsService("example.com", "admin", "password");
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

        [Test]
        public void SetDomainTest()
        {
            string domain = "foo.com";
            service.Domain = domain;
            Assert.AreEqual("foo.com", service.Domain, "Domain should be foo.com after setting");
        }

        [Test]
        public void GetApplicationName()
        {
            Assert.AreEqual("apps-example.com", service.ApplicationName,
                "Application name should initially be apps-example.com");
        }

        [Test]
        public void SetApplicationName()
        {
            string applicationName = "apps-foo.com";
            service.ApplicationName = applicationName;
            Assert.AreEqual("apps-foo.com", service.ApplicationName,
                "Application name should be apps-foo.com after setting");
        }
    }
}
