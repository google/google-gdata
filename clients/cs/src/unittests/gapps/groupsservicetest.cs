using System;
using System.Text;
using System.Xml;
using System.IO;
using NUnit.Framework;
using Google.GData.Apps.Groups;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class GroupsServiceTest
    {
        private GroupsService service;

        [SetUp]
        public void Init()
        {
            service = new GroupsService("example.com", "apps-example.com");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetDomainTest()
        {
            Assert.AreEqual("example.com", service.DomainName, "Domain should initially be example.com");
        }
    }
}
