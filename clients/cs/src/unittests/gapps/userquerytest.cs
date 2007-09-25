using System;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Apps;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class UserQueryTest
    {
        private UserQuery query;

        [SetUp]
        public void Init()
        {
            query = new UserQuery("example.com");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetDomainTest()
        {
            Assert.AreEqual(query.Domain, "example.com",
                "Domain should initially be example.com");
        }

        [Test]
        public void SetDomainTest()
        {
            String newDomain = "foo.com";
            query.Domain = newDomain;
            Assert.AreEqual(query.Domain, "foo.com",
                "Domain should be foo.com after setting");
        }

        [Test]
        public void GetStartUserNameTest()
        {
            Assert.IsNull(query.StartUserName, "Start username should initially be null");
        }

        [Test]
        public void SetStartUserNameTest()
        {
            String newStartUserName = "jdoe";
            query.StartUserName = newStartUserName;
            Assert.AreEqual(query.StartUserName, "jdoe",
                "Start username should be jdoe after setting");
        }
        
        [Test]
        public void GetUserTest()
        {
            Assert.IsNull(query.UserName, "Username should initially be null");
        }

        [Test]
        public void SetUserTest()
        {
            String newUser = "jdoe";
            query.UserName = newUser;
            Assert.AreEqual(query.UserName, "jdoe", "Username should be jdoe after setting");
        }

        [Test]
        public void GetRetrieveAllUsersTest()
        {
            Assert.IsTrue(query.RetrieveAllUsers,
                "RetrieveAllUsers should be true by default");
        }

        [Test]
        public void SetRetrieveAllUsersTest()
        {
            query.RetrieveAllUsers = false;
            Assert.IsFalse(query.RetrieveAllUsers,
                "RetrieveAllUsers should be false after setting");
        }

        [Test]
        public void GetAllUsersQueryUriTest()
        {
            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/user/2.0"),
                "URI must end with <domain>/user/2.0");
        }

        [Test]
        public void GetSingleUserQueryUriTest()
        {
            query.UserName = "jdoe";
            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain +
                          "/user/2.0/" + query.UserName),
                          "URI must end with <domain>/user/2.0/<userName>");
        }

        [Test]
        public void GetStartUserQueryUriTest()
        {
            query.StartUserName = "jdoe";
            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain +
              "/user/2.0?startUsername=" + query.StartUserName),
              "URI must end with <domain>/user/2.0?startUsername=<startUsername>");
        }

        [Test]
        public void GetErroneousQueryTest()
        {
            // This invalid query should throw a GDataRequestException.
            bool caughtException = false;

            try
            {
                query.StartUserName = "jdoe";
                query.UserName = "jdoe";
            }
            catch (GDataRequestException)
            {
                caughtException = true;
            }

            Assert.IsTrue(caughtException,
                "Setting both StartUserName and UserName should produce a GDataRequestException");
        }
    }
}
