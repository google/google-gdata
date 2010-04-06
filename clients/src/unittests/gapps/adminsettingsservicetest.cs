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
    class AdminSettingsServiceTest
    {
        private AdminSettingsService service;

        [SetUp]
        public void Init()
        {
            service = new AdminSettingsService("example.com", "apps-example.com");
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
