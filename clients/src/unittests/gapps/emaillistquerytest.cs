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
    public class EmailListQueryTest
    {
        private EmailListQuery query;

        [SetUp]
        public void Init()
        {
            query = new EmailListQuery("example.com");
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
        public void GetRecipientTest()
        {
            Assert.IsNull(query.Recipient, "Recipient should initially be null");
        }

        [Test]
        public void SetRecipientTest()
        {
            String recipient = "johndoe";
            query.Recipient = recipient;
            Assert.AreEqual(query.Recipient, "johndoe", "Recipient should be johndoe after setting");
        }

        [Test]
        public void GetEmailListNameTest()
        {
            Assert.IsNull(query.EmailListName, "Email list name should initially be null");
        }

        [Test]
        public void SetEmailListNameTest()
        {
            String emailListName = "testList";
            query.EmailListName = emailListName;
            Assert.AreEqual(query.EmailListName, "testList", "List name should be testList after setting");
        }

        [Test]
        public void SetRetrieveAllEmailListsTest()
        {
            query.RetrieveAllEmailLists = false;
            Assert.IsFalse(query.RetrieveAllEmailLists, "RetrieveAllEmailLists should be false after setting");
        }

        [Test]
        public void GetStartEmailListNameTest()
        {
            Assert.IsNull(query.StartEmailListName, "Start email list name should initially be null");
        }

        [Test]
        public void SetStartEmailListNameTest()
        {
            String startEmailListName = "testList";
            query.StartEmailListName = startEmailListName;
            Assert.AreEqual(query.StartEmailListName, "testList",
                "Start email list name should be testList after setting");
        }

        [Test]
        public void GetRetrieveAllEmailListsUriTest()
        {
            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/emailList/2.0"),
                "URI must end with <domain>/emailList/2.0");
        }

        [Test]
        public void GetRetrieveOneEmailListUriTest()
        {
            query.EmailListName = "testList";
            
            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/emailList/2.0/" + query.EmailListName),
                "URI must end with <domain>/emailList/2.0/<emailListName>");
        }

        [Test]
        public void GetRetrieveStartEmailListUriTest()
        {
            query.StartEmailListName = "testList";

            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/emailList/2.0?startEmailListName=" +
                                                  query.StartEmailListName),
                "URI must end with <domain>/emailList/2.0?startEmailListName=<startEmailListName>");
        }

        [Test]
        public void GetRetrieveByRecipientUriTest()
        {
            query.Recipient = "jdoe";

            Uri uri = query.Uri;

            Assert.IsNotNull(uri, "Query URI should not be null");
            Assert.IsTrue(uri.ToString().StartsWith(AppsNameTable.appsBaseFeedUri),
                "URI must start with the base feed URI");
            Assert.IsTrue(uri.ToString().EndsWith(query.Domain + "/emailList/2.0?recipient=" + query.Recipient),
                "URI must end with <domain>/emailList/2.0?recipient=<emailAddress>");
        }

        [Test]
        public void GetErroneousQueryTest()
        {
            // This invalid query should throw a GDataRequestException.
            bool caughtException = false;

            try
            {
                query.EmailListName = "testList";
                query.StartEmailListName = "testList";
            }
            catch (GDataRequestException)
            {
                caughtException = true;
            }

            Assert.IsTrue(caughtException,
                "Setting both EmailListName and StartEmailListName should produce a GDataRequestException");
        }
    }
}
