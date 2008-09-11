using System;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Apps;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class EmailListRecipientQueryTest
    {
        private EmailListRecipientQuery query;

        [SetUp]
        public void Init()
        {
            query = new EmailListRecipientQuery("example.com", "testList");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetDomainTest()
        {
            Assert.AreEqual(query.Domain, "example.com", "Domain should initially be example.com");
        }

        [Test]
        public void SetDomainTest()
        {
            String domain = "foo.com";
            query.Domain = domain;
            Assert.AreEqual(query.Domain, "foo.com", "Domain should be foo.com after setting");
        }

        [Test]
        public void GetEmailListNameTest()
        {
            Assert.AreEqual(query.EmailListName, "testList",
                "List name should initially be testList");
        }

        [Test]
        public void SetEmailListNameTest()
        {
            String emailListName = "newTestList";
            query.EmailListName = emailListName;
            Assert.AreEqual(query.EmailListName, "newTestList",
                "List name should be newTestList after setting");
        }

        [Test]
        public void GetRecipientTest()
        {
            Assert.IsNull(query.Recipient, "Recipient should initially be null");
        }

        [Test]
        public void SetRecipientTest()
        {
            String recipient = "jdoe@example.com";
            query.Recipient = recipient;
            Assert.AreEqual("jdoe@example.com", query.Recipient,
                "Recipient should be jdoe@example.com after setting");
        }

        [Test]
        public void GetStartRecipientTest()
        {
            Assert.IsNull(query.StartRecipient, "Start recipient should initially be null");
        }

        [Test]
        public void SetStartRecipientTest()
        {
            String startRecipient = "jdoe@example.com";
            query.StartRecipient = startRecipient;
            Assert.AreEqual("jdoe@example.com", query.StartRecipient,
                "Start recipient should be jdoe@example.com after setting");
        }

        [Test]
        public void GetRetrieveAllRecipientsUriTest()
        {
            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/emailList/2.0/" +
                query.EmailListName + "/recipient/"),
                "URI must end with <domain>/emailList/2.0/<emailListName>/recipient/");
        }

        [Test]
        public void GetRetrieveOneRecipientUriTest()
        {
            query.Recipient = "jdoe@example.com";

            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/emailList/2.0/" +
                query.EmailListName + "/recipient/" + query.Recipient),
                "URI must end with <domain>/emailList/2.0/<emailListName>/recipient/<recipient>");
        }

        [Test]
        public void GetRetrieveStartRecipientUriTest()
        {
            query.StartRecipient = "jdoe@example.com";

            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/emailList/2.0/" +
                query.EmailListName + "?startRecipient=" + query.StartRecipient),
                "URI must end with <domain>/emailList/2.0/<emailListName>?startRecipient=<startRecipient>");
        }

        [Test]
        public void GetErroneousQueryTest()
        {
            // This invalid query should throw a GDataRequestException.
            bool caughtException = false;

            try
            {
                query.Recipient = "testList";
                query.StartRecipient = "testList";
            }
            catch (GDataRequestException)
            {
                caughtException = true;
            }

            Assert.IsTrue(caughtException,
                "Setting both Recipient and StartRecipient should produce a GDataRequestException");
        }
    }
}
