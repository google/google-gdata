/* Copyright (c) 2006-2008 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/
using System;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Apps;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class NicknameQueryTest
    {
        private NicknameQuery query;

        [SetUp]
        public void Init()
        {
            query = new NicknameQuery("example.com");
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
            String newDomain = "foo.com";
            query.Domain = newDomain;
            Assert.AreEqual(query.Domain, "foo.com", "Domain should be foo.com after setting");
        }

        [Test]
        public void GetNicknameTest()
        {
            Assert.IsNull(query.Nickname, "Nickname should initially be null");
        }

        [Test]
        public void SetNicknameTest()
        {
            String nickname = "johndoe";
            query.Nickname = nickname;
            Assert.AreEqual(query.Nickname, "johndoe", "Nickname should be johndoe after setting");
        }

        [Test]
        public void GetStartNicknameTest()
        {
            Assert.IsNull(query.StartNickname, "Start nickname should initially be null");
        }

        [Test]
        public void SetStartNicknameTest()
        {
            String startNickname = "johndoe";
            query.StartNickname = startNickname;
            Assert.AreEqual(query.StartNickname, "johndoe", "Start nickname should be johndoe after setting");
        }

        [Test]
        public void GetUsernameTest()
        {
            Assert.IsNull(query.UserName, "UserName should initially be null");
        }

        [Test]
        public void SetUsernameTest()
        {
            String userName = "jdoe";
            query.UserName = userName;
            Assert.AreEqual(query.UserName, "jdoe", "UserName should be jdoe after setting");

            query.UserName = null;  // Reset to null before next tests
        }

        [Test]
        public void GetRetrieveAllNicknamesTest()
        {
            Assert.IsTrue(query.RetrieveAllNicknames,
                "RetrieveAllNicknames should initially be true");
        }

        [Test]
        public void SetRetrieveAllNicknamesTest()
        {
            query.RetrieveAllNicknames = false;
            Assert.IsFalse(query.RetrieveAllNicknames,
                "RetrieveAllNicknames should be false after setting");
        }

        [Test]
        public void GetRetrieveAllNicknamesUriTest()
        {
            query.Nickname = null;
            query.UserName = null;

            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/nickname/2.0"),
                "URI must end with <domain>/nickname/2.0");
        }

        [Test]
        public void GetRetrieveOneNicknameUriTest()
        {
            query.Nickname = "johndoe";

            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/nickname/2.0/" + query.Nickname),
                "URI must end with <domain>/nickname/2.0/<nickname>");
        }

        [Test]
        public void GetRetrieveStartUserNameTest()
        {
            query.StartNickname = "johndoe";

            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/nickname/2.0?startNickname=" +
                                                  query.StartNickname),
                "URI must end with <domain>/nickname/2.0/?startNickname=<startNickname>");
        }

        [Test]
        public void GetRetrieveByUserNameUriTest()
        {
            query.UserName = "jdoe";

            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/nickname/2.0?username=" + query.UserName),
                "URI must end with <domain>/nickname/2.0?username=<username>");
        }

        [Test]
        public void GetErroneousQueryTest()
        {
            // This invalid query should throw a GDataRequestException.
            bool caughtException = false;

            try
            {
                query.StartNickname = "johndoe";
                query.Nickname = "johndoe";
            }
            catch (GDataRequestException)
            {
                caughtException = true;
            }

            Assert.IsTrue(caughtException,
                "Setting both StartNickname and Nickname should produce a GDataRequestException");
        }
    }
}
