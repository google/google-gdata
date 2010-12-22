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
        private string              randomEmailListName;

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
            this.randomEmailListName = "test_emaillist_" + Guid.NewGuid().ToString();
        }

        #region Email Lists test functions
        [Test]
        public void        TestEmailListFunctions()
        {
            EmailList_Clean();
            EmailList_CreateEmailList();
            EmailList_VerifyEmailList();
            EmailList_AddRecipientToEmailList();
            EmailList_AddWrongRecipientToEmailList();
            EmailList_VerifyRecipientOfEmailList();
            EmailList_DeleteRecipientFromEmailList();
            EmailList_DeleteEmailList();
        }

        public void     EmailList_Clean()
        {
            EmailListFeed    feed = this.service.RetrieveAllEmailLists();

            if (feed.Entries.Count > 0)
            {
                foreach (EmailListEntry entry in feed.Entries)
                    this.service.DeleteEmailList(entry.EmailList.Name);

                feed = this.service.RetrieveAllEmailLists();
            }

            Assert.AreEqual(feed.Entries.Count, 0, "Failed to clean email lists.");
        }

        public void     EmailList_CreateEmailList()
        {
            EmailListEntry entry = this.service.CreateEmailList(this.randomEmailListName);
            
            Assert.AreEqual(this.randomEmailListName, entry.EmailList.Name);
        }

        public void     EmailList_VerifyEmailList()
        {
            EmailListEntry entry = this.service.RetrieveEmailList(this.randomEmailListName);
            
            Assert.AreEqual(this.randomEmailListName, entry.EmailList.Name);
        }

        public void     EmailList_AddRecipientToEmailList()
        {
            EmailListRecipientEntry entry = this.service.AddRecipientToEmailList(this.adminUsername + "@" + this.domainName, this.randomEmailListName);

            Assert.AreEqual(this.adminUsername + "@" + this.domainName, entry.Recipient.Email);
        }

        public void     EmailList_AddWrongRecipientToEmailList()
        {
            try
            {
                this.service.AddRecipientToEmailList(Guid.NewGuid().ToString() + "@" + this.domainName, this.randomEmailListName);
            }
            catch (AppsException e)
            {
                Assert.AreEqual("1301", e.ErrorCode);
                Assert.AreEqual("EntityDoesNotExist", e.Reason);
            }
        }

        public void     EmailList_VerifyRecipientOfEmailList()
        {
            EmailListRecipientFeed  feed = this.service.RetrieveAllRecipients(this.randomEmailListName);

            Assert.AreEqual(1, feed.Entries.Count);
            Assert.AreEqual(this.adminUsername + "@" + this.domainName, ((EmailListRecipientEntry)feed.Entries[0]).Recipient.Email);
        }

        public void     EmailList_DeleteRecipientFromEmailList()
        {
            this.service.RemoveRecipientFromEmailList(this.adminUsername + "@" + this.domainName, this.randomEmailListName);

            EmailListRecipientFeed  feed = this.service.RetrieveAllRecipients(this.randomEmailListName);

            Assert.AreEqual(0, feed.Entries.Count, "The recipient wasn't properly deleted from " + this.randomEmailListName);
        }

        public void     EmailList_DeleteEmailList()
        {
            this.service.DeleteEmailList(this.randomEmailListName);

            try
            {
                this.service.RetrieveEmailList(this.randomEmailListName);
                Assert.Fail("The email list wasn't properly deleted.");
            }
            catch (AppsException e)
            {
                Assert.AreEqual("1301", e.ErrorCode);
                Assert.AreEqual("EntityDoesNotExist", e.Reason);
            }
        }
        #endregion

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
