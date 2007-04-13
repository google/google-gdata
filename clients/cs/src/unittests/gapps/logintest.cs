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
    public class LoginTest
    {
        private LoginElement login;

        [SetUp]
        public void Init()
        {
            login = new LoginElement("userName", "password", true, true);
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetUserNameTest()
        {
            Assert.AreEqual("userName", login.UserName, "Username should initially be 'userName'");
        }

        [Test]
        public void SetUserNameTest()
        {
            String newUserName = "newUserName";
            login.UserName = newUserName;
            Assert.AreEqual("newUserName", login.UserName, "Username should be 'newUserName' after setting");
        }

        [Test]
        public void GetPasswordTest()
        {
            Assert.AreEqual("password", login.Password, "Password should initially be 'password'");
        }

        [Test]
        public void SetPasswordTest()
        {
            String newPassword = "newPassword";
            login.Password = newPassword;
            Assert.AreEqual("newPassword", login.Password, "Password should be 'newPassword' after setting");
        }

        [Test]
        public void GetSuspendedTest()
        {
            Assert.IsTrue(login.Suspended, "User account should initially be suspended");
        }

        [Test]
        public void SetSuspendedTest()
        {
            login.Suspended = false;
            Assert.IsFalse(login.Suspended, "User account should not be suspended after setting");
        }

        [Test]
        public void GetIpWhitelistedTest()
        {
            Assert.IsTrue(login.IpWhitelisted, "User account should initially be IP whitelisted");
        }

        [Test]
        public void SetIpWhitelistedTest()
        {
            login.IpWhitelisted = false;
            Assert.IsFalse(login.IpWhitelisted, "User account should not be IP whitelisted after setting");
        }

        [Test]
        public void SaveXmlTest()
        {
            StringWriter outString = new StringWriter();
            XmlWriter writer = new XmlTextWriter(outString);

            login.Save(writer);
            writer.Close();

            String expectedXml = "<apps:login userName=\"" +
                login.UserName + "\" password=\"" + login.Password + "\" suspended=\"" +
                login.Suspended.ToString().ToLower() + "\" ipWhitelisted=\"" + 
                login.IpWhitelisted.ToString().ToLower() +
                "\" xmlns:apps=\"http://schemas.google.com/apps/2006\" />";

            Assert.IsTrue(outString.ToString().EndsWith(expectedXml),
                "Serialized XML does not match expected result: " + expectedXml);
        }
    }
}
