using System;
using System.Text;
using System.Xml;
using System.IO;
using NUnit.Framework;
using Google.GData.Extensions.Apps;

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
            Assert.IsTrue(login.IPWhitelisted, "User account should initially be IP whitelisted");
        }

        [Test]
        public void SetIpWhitelistedTest()
        {
            login.IPWhitelisted = false;
            Assert.IsFalse(login.IPWhitelisted, "User account should not be IP whitelisted after setting");
        }

        [Test]
        public void GetHashFunctionNameTest()
        {
            Assert.AreEqual("", login.HashFunctionName, "Hash function name should initially be empty");
        }

        [Test]
        public void SetHashFunctionNameTest()
        {
            login.HashFunctionName = "SHA-1";
            Assert.AreEqual("SHA-1", login.HashFunctionName, "Hash function name should be SHA-1 after setting");
        }

        [Test]
        public void GetAdminTest()
        {
            Assert.IsFalse(login.Admin, "Admin property should initially be false");
        }

        [Test]
        public void SetAdminTest()
        {
            login.Admin = true;
            Assert.IsTrue(login.Admin, "Admin property should be true after setting");
        }

        [Test]
        public void GetAgreedToTermsTest()
        {
            Assert.IsFalse(login.AgreedToTerms, "Agreed to terms property should initially be false");
        }

        [Test]
        public void SetAgreedToTermsTest()
        {
            login.AgreedToTerms = true;
            Assert.IsTrue(login.AgreedToTerms, "Agreed to terms property should be true after setting");
        }

        [Test]
        public void GetChangePasswordAtNextLoginTest()
        {
            Assert.IsFalse(login.ChangePasswordAtNextLogin,
                "Change password at next login property should initially be false");
        }

        [Test]
        public void SetChangePasswordAtNextLoginTest()
        {
            login.ChangePasswordAtNextLogin = true;
            Assert.IsTrue(login.ChangePasswordAtNextLogin,
                "Change password at next login property should be true after setting");
        }

        [Test]
        public void ParseXmlTest()
        {
            String inputXml = "<apps:login userName=\"jdoe\" password=\"mypasswd\" suspended=\"" +
                "true\" ipWhitelisted=\"true\" hashFunctionName=\"SHA-1\" admin=\"true\" " +
                "agreedToTerms=\"true\" changePasswordAtNextLogin=\"true\" " +
                "xmlns:apps=\"http://schemas.google.com/apps/2006\" />";

            XmlDocument document = new XmlDocument();
            document.LoadXml(inputXml);

            login.ProcessAttributes(document.DocumentElement);

            Assert.IsNotNull(login, "Parsed login element should not be null");
            Assert.AreEqual("jdoe", login.UserName, "Parsed login should have username=\"jdoe\"");
            Assert.AreEqual("mypasswd", login.Password, "Parsed login should have password=\"mypasswd\"");
            Assert.IsTrue(login.Suspended, "Parsed login should have suspended=true");
            Assert.IsTrue(login.IPWhitelisted, "Parsed login should have ipWhitedlisted=true");
           
            Assert.AreEqual("SHA-1", login.HashFunctionName, "Parsed login should have hashFunctionName=\"SHA-1\"");
            Assert.IsTrue(login.Admin, "Parsed login should have admin=true");
            Assert.IsTrue(login.AgreedToTerms, "Parsed login should have agreedToTerms=true");
            Assert.IsTrue(login.ChangePasswordAtNextLogin, "Parsed login should have changePasswordAtNextLogin=true");
        }

        [Test]
        public void SaveXmlTest()
        {
            StringWriter outString = new StringWriter();
            XmlWriter writer = new XmlTextWriter(outString);

            login.Save(writer);
            writer.Close();


            // Validate expected XML contents
            string serializedXml = outString.ToString();
            
            Assert.IsNotNull(serializedXml, "Serialized XML should not be null.");
            Assert.IsTrue(serializedXml.StartsWith("<apps:login"), "Serialized XML does not start " +
                " with correct element name.");
            Assert.IsTrue(serializedXml.IndexOf("userName=\"" + login.UserName + "\"") >= 0,
                "Serialized XML does not contain correct userName attribute.");
            Assert.IsTrue(serializedXml.IndexOf("password=\"" + login.Password + "\"") >= 0,
                "Serialized XML does not contain correct password attribute.");
            Assert.IsTrue(serializedXml.IndexOf("suspended=\"" + login.Suspended.ToString().ToLower() + "\"") >= 0,
                "Serialized XML does not contain correct suspended attribute.");
            Assert.IsTrue(serializedXml.IndexOf("ipWhitelisted=\"" +
                login.IPWhitelisted.ToString().ToLower() + "\"") >= 0,
                "Serialized XML does not contain correct ipWhitelisted attribute.");

            // Make sure that the "admin" and "changePasswordAtNextLogin" attributes
            // are NOT present here
            Assert.IsFalse(serializedXml.IndexOf("admin=") >= 0,
                "Serialized XML should not contain an \"admin\" attribute.");
            Assert.IsFalse(serializedXml.IndexOf("changePasswordAtNextLogin=") >= 0,
                "Serialized XML should not contain a \"changePasswordAtNextLogin\" attribute.");
        }

        [Test]
        public void SaveXmlWithOptionalAttributesTest()
        {
            StringWriter outString = new StringWriter();
            XmlWriter writer = new XmlTextWriter(outString);

            login.Admin = true;
            login.ChangePasswordAtNextLogin = true;

            login.Save(writer);
            writer.Close();

            // Validate expected XML contents
            string serializedXml = outString.ToString();

            Assert.IsNotNull(serializedXml, "Serialized XML should not be null.");
            Assert.IsTrue(serializedXml.StartsWith("<apps:login"), "Serialized XML does not start " +
                " with correct element name.");
            Assert.IsTrue(serializedXml.IndexOf("userName=\"" + login.UserName + "\"") >= 0,
                "Serialized XML does not contain correct userName attribute.");
            Assert.IsTrue(serializedXml.IndexOf("password=\"" + login.Password + "\"") >= 0,
                "Serialized XML does not contain correct password attribute.");
            Assert.IsTrue(serializedXml.IndexOf("suspended=\"" + login.Suspended.ToString().ToLower() + "\"") >= 0,
                "Serialized XML does not contain correct suspended attribute.");
            Assert.IsTrue(serializedXml.IndexOf("ipWhitelisted=\"" +
                login.IPWhitelisted.ToString().ToLower() + "\"") >= 0,
                "Serialized XML does not contain correct ipWhitelisted attribute.");

            // Make sure that the "admin" and "changePasswordAtNextLogin" attributes
            // ARE present here
            Assert.IsTrue(serializedXml.IndexOf("admin=\"" + login.Admin.ToString().ToLower() + "\"") >= 0,
                "Serialized XML does not contain an \"admin\" attribute.");
            Assert.IsTrue(serializedXml.IndexOf("changePasswordAtNextLogin=\"" +
                login.ChangePasswordAtNextLogin.ToString().ToLower() + "\"") >= 0,
                "Serialized XML does not contain a \"changePasswordAtNextLogin\" attribute.");
        }
    }
}
