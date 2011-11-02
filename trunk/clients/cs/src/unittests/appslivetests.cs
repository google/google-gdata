using System;
using System.Text;

using Google.GData.Apps;
using Google.GData.Extensions;
using Google.GData.Client;
using Google.GData.Client.UnitTests;

using NUnit.Framework;

namespace Google.GData.Client.LiveTests
{
	/// <summary>
	/// Summary description for apps.
	/// </summary>
	[TestFixture]
    [Category("LiveTest")]
    public class GoogleAFYDTestSuite : BaseLiveTestClass
	{
        private string              domainName;
        private string              adminUsername;
        private string              adminPassword;
        private AppsService         service;

        private string              randomUserName;

        private const string        FirstName = "testFirstname";
        private const string        LastName = "testLastname";
        private const string        Password = "test1234";

        
        public GoogleAFYDTestSuite()
		{
		}

        [SetUp]
        public override void     InitTest()
        {
            base.InitTest();

            if (unitTestConfiguration.Contains("domainName"))
            {
                this.domainName = (string)unitTestConfiguration["domainName"];
                Tracing.TraceInfo("Read userName value: " + this.domainName);
            }
            if (unitTestConfiguration.Contains("domainAdminUsername"))
            {
                this.adminUsername = (string)unitTestConfiguration["domainAdminUsername"];
                Tracing.TraceInfo("Read userName value: " + this.adminUsername);
            }
            if (unitTestConfiguration.Contains("domainAdminPassword"))
            {
                this.adminPassword = (string)unitTestConfiguration["domainAdminPassword"];
                Tracing.TraceInfo("Read userName value: " + this.adminPassword);
            }

            this.service = new AppsService(this.domainName, this.adminUsername + "@" + this.domainName, this.adminPassword);

            this.randomUserName = "test_user_" + Guid.NewGuid().ToString();
        }

        #region Users test functions
        [Test]
        public void        TestUserFunctions()
        {
            Users_Clean();
            Users_Create();
            Users_Verify();
            Users_Suspend();
            Users_Restore();
            Users_Update();
            Users_Delete();
        }

        public void     Users_Clean()
        {
            UserFeed    feed = this.service.RetrieveAllUsers();

            if (feed.Entries.Count > 1)
            {
                foreach (UserEntry entry in feed.Entries)
                    if (entry.Login.UserName != "admin")
                        this.service.DeleteUser(entry.Login.UserName);

                feed = this.service.RetrieveAllUsers();
            }

            Assert.AreEqual(1, feed.Entries.Count, "Failed to clean users list.");
        }

        public void     Users_Create()
        {
            UserEntry entry = this.service.CreateUser(this.randomUserName, FirstName, LastName, Password);
            
            Assert.AreEqual(this.randomUserName, entry.Login.UserName);
            Assert.IsFalse(entry.Login.Suspended);
            Assert.AreEqual(FirstName, entry.Name.GivenName);
            Assert.AreEqual(LastName, entry.Name.FamilyName);
        }

        public void     Users_Verify()
        {
            UserEntry entry = this.service.RetrieveUser(this.randomUserName);
            
            Assert.AreEqual(this.randomUserName, entry.Login.UserName);
            Assert.IsFalse(entry.Login.Suspended);
            Assert.AreEqual(FirstName, entry.Name.GivenName);
            Assert.AreEqual(LastName, entry.Name.FamilyName);
        }

        public void     Users_Suspend()
        {
            UserEntry entry = this.service.SuspendUser(this.randomUserName);
            
            Assert.AreEqual(this.randomUserName, entry.Login.UserName);
            Assert.IsTrue(entry.Login.Suspended);
        }

        public void     Users_Restore()
        {
            UserEntry entry = this.service.RestoreUser(this.randomUserName);
            
            Assert.AreEqual(this.randomUserName, entry.Login.UserName);
            Assert.IsFalse(entry.Login.Suspended);
        }

        public void     Users_Update()
        {
            UserEntry entry = this.service.RetrieveUser(this.randomUserName);
            entry.Name.GivenName = "Jamie";
            entry = this.service.UpdateUser(entry);
            
            Assert.AreEqual(this.randomUserName, entry.Login.UserName);
            Assert.IsFalse(entry.Login.Suspended);
            Assert.AreEqual("Jamie", entry.Name.GivenName);
            Assert.AreEqual(LastName, entry.Name.FamilyName);
        }

        public void     Users_Delete()
        {
            this.service.DeleteUser(this.randomUserName);

            try
            {
                this.service.RetrieveUser(this.randomUserName);
                Assert.Fail("We shouldn't be able to retrieve the user!");
            }
            catch (AppsException e)
            {
                Assert.AreEqual("1301", e.ErrorCode);
                Assert.AreEqual(this.randomUserName, e.InvalidInput);
                Assert.AreEqual("EntityDoesNotExist", e.Reason);
            }
        }
        #endregion

	}
}
