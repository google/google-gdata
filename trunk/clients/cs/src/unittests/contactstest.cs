/* Copyright (c) 2006 Google Inc.
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
#define USE_TRACING
#define DEBUG

using System;
using System.IO;
using System.Xml; 
using System.Collections;
using System.Configuration;
using System.Net; 
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Client.UnitTests;
using Google.GData.Extensions;
using Google.GData.Contacts;





namespace Google.GData.Client.LiveTests
{
    [TestFixture] 
    [Category("LiveTest")]
    public class ContactsTestSuite : BaseLiveTestClass
    {
      //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public ContactsTestSuite()
        {
        }

        public override string ServiceName
        {
            get {
                return ContactsService.GContactService;
            }
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void ContactsAuthenticationTest()
        {
            Tracing.TraceMsg("Entering ContactsAuthenticationTest");

            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName));
            ContactsService service = new ContactsService("unittests");

            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }

            ContactsFeed feed = service.Query(query);

            ObjectModelHelper.DumpAtomObject(feed,CreateDumpFileName("ContactsAuthTest")); 

            if (feed != null && feed.Entries.Count > 0)
            {
                Tracing.TraceMsg("Found a Feed " + feed.ToString());

                foreach (ContactEntry entry in feed.Entries)
                {
                    Tracing.TraceMsg("Found an entry " + entry.ToString());
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        
        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new contact</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void InsertContactsTest()
        {
            Tracing.TraceMsg("Entering InsertContactsTest");

            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName));
            ContactsService service = new ContactsService("unittests");

            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }

            ContactsFeed feed = service.Query(query);
            if (feed != null)
            {
                Assert.IsTrue(feed.Entries != null, "the contacts needs entries");

                for (int i=0; i<3; i++)
                {
                    ContactEntry entry = ObjectModelHelper.CreateContactEntry(i);
                    feed.Insert(entry);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////

    }
}




