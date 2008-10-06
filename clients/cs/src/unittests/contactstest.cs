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
using System.Collections.Generic;





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
        [Test] public void ContactsObjectModelTest()
        {
            Tracing.TraceMsg("Entering ContactsObjectModelTest");

            EMail email = new EMail("joe@doe.com");
            Assert.AreEqual(email.Address, "joe@doe.com", "constructor should have set address field");
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void ContactsAuthenticationTest()
        {
            Tracing.TraceMsg("Entering ContactsAuthenticationTest");

            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName + "@googlemail.com"));
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
                    Tracing.TraceMsg("Found a photoUri " + entry.PhotoUri);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////
        /// <summary>runs an basic auth test against the groups feed test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GroupsAuthenticationTest()
        {
            Tracing.TraceMsg("Entering GroupsAuthenticationTest");

            GroupsQuery query = new GroupsQuery(ContactsQuery.CreateGroupsUri(this.userName + "@googlemail.com"));
            ContactsService service = new ContactsService("unittests");

            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }

            GroupsFeed feed = service.Query(query);

            ObjectModelHelper.DumpAtomObject(feed,CreateDumpFileName("GroupsAuthTest"));

            GroupEntry newGroup = new GroupEntry();
            newGroup.Title.Text = "Private Data";

            GroupEntry insertedGroup = feed.Insert(newGroup);

            GroupEntry g2 = new GroupEntry();
            g2.Title.Text = "Another Private Group";
            GroupEntry insertedGroup2 = feed.Insert(g2);

            // now insert a new contact that belongs to that group
            ContactsQuery q = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName + "@googlemail.com"));
            ContactsFeed cf = service.Query(q);
            ContactEntry entry = ObjectModelHelper.CreateContactEntry(1);
            GroupMembership member = new GroupMembership();
            member.HRef = insertedGroup.Id.Uri.ToString();
            GroupMembership member2 = new GroupMembership();
            member2.HRef = insertedGroup2.Id.Uri.ToString();

            ContactEntry insertedEntry = cf.Insert(entry);
            // now change the group membership
            insertedEntry.GroupMembership.Add(member);
            insertedEntry.GroupMembership.Add(member2);
            ContactEntry currentEntry = insertedEntry.Update();

            Assert.IsTrue(currentEntry.GroupMembership.Count == 2, "The entry should be in 2 groups");

            currentEntry.GroupMembership.Clear();
            currentEntry.Update();
            // now we should have 2 new groups and one new entry with no groups anymore


            int oldCountGroups = feed.Entries.Count;
            int oldCountContacts = cf.Entries.Count;

            currentEntry.Delete();

            insertedGroup.Delete();
            insertedGroup2.Delete();

            feed = service.Query(query);
            cf = service.Query(q);

            Assert.AreEqual(oldCountContacts, cf.Entries.Count, "Contacts count should be the same");
            Assert.AreEqual(oldCountGroups, feed.Entries.Count, "Groups count should be the same");
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new contact</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void ConflictContactsTest()
        {
            const int numberOfInserts = 50;
            const int numberWithAdds = 60; 
            Tracing.TraceMsg("Entering InsertContactsTest");

            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName + "@googlemail.com"));
            ContactsService service = new ContactsService("unittests");


            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }

            // clean the contacts feed
            DeleteAllContacts();

            ContactsFeed feed = service.Query(query);

            int originalCount = feed.Entries.Count;

            string email = Guid.NewGuid().ToString();

            List<ContactEntry> inserted = new List<ContactEntry>();

            // insert a number of guys
            for (int i = 0; i < numberOfInserts; i++)
            {
                ContactEntry entry = ObjectModelHelper.CreateContactEntry(i);
                entry.PrimaryEmail.Address = email + i.ToString() + "@doe.com";
                entry = feed.Insert(entry);
                AddContactPhoto(entry, service);
                inserted.Add(entry);

            }


            if (feed != null)
            {
                for (int x = numberOfInserts; x <= numberWithAdds; x++)
                {
                    for (int i = 0; i < x; i++)
                    {
                        ContactEntry entry = ObjectModelHelper.CreateContactEntry(i);
                        entry.PrimaryEmail.Address = email + i.ToString() + "@doe.com";

                        try
                        {
                            entry = feed.Insert(entry);
                            AddContactPhoto(entry, service);
                            inserted.Add(entry);
                        }
                        catch (GDataRequestException e)
                        {
                        }
                    }
                }
            }

            List<ContactEntry> list = new List<ContactEntry>();
            feed = service.Query(query);
            foreach (ContactEntry e in feed.Entries)
            {
                list.Add(e);
            }

            while (feed.NextChunk != null)
            {
                ContactsQuery nq = new ContactsQuery(feed.NextChunk);
                feed = service.Query(nq);
                foreach (ContactEntry e in feed.Entries)
                {
                    list.Add(e);
                }
            }

            Assert.AreEqual(list.Count, numberWithAdds - originalCount, "We should have added new entries");

            // clean the contacts feed
            DeleteAllContacts();

        }
        /////////////////////////////////////////////////////////////////////////////
        
        
        private void AddContactPhoto(ContactEntry entry, ContactsService contactService)
        {
            try
            {
                using (FileStream fs = new FileStream("contactphoto.jpg", System.IO.FileMode.Open))
                {
                    Stream res = contactService.StreamSend(entry.PhotoEditUri, fs, GDataRequestType.Update, "image/jpg", null);
                    res.Close();
                }
            } finally   
            {
            }
        } 



        private void DeleteAllContacts()
        {

            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName + "@googlemail.com"));
            ContactsService service = new ContactsService("unittests");

            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }

            ContactsFeed feed = service.Query(query);

            List<ContactEntry> list = new List<ContactEntry>();
            feed = service.Query(query);
            foreach (ContactEntry e in feed.Entries)
            {
                list.Add(e);
            }

            while (feed.NextChunk != null)
            {
                ContactsQuery nq = new ContactsQuery(feed.NextChunk);
                feed = service.Query(nq);
                foreach (ContactEntry e in feed.Entries)
                {
                    list.Add(e);
                }
            }

            foreach (ContactEntry e in list)
            {
                e.Delete();
            }
            feed = service.Query(query);

            Assert.IsTrue(feed.Entries.Count == 0, "Feed should be emtpy now");
        }
        
        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new contact</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void InsertContactsTest()
        {
            const int numberOfInserts = 37;
            Tracing.TraceMsg("Entering InsertContactsTest");

            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName + "@googlemail.com")); 
            ContactsService service = new ContactsService("unittests");

            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }

            ContactsFeed feed = service.Query(query);

            int originalCount = feed.Entries.Count;

            PhoneNumber p = null;

            List<ContactEntry> inserted = new List<ContactEntry>();
            if (feed != null)
            {
                Assert.IsTrue(feed.Entries != null, "the contacts needs entries");

                for (int i = 0; i < numberOfInserts; i++)
                {
                    ContactEntry entry = ObjectModelHelper.CreateContactEntry(i);
                    entry.PrimaryEmail.Address = "joe" + i.ToString() + "@doe.com";

                    p = entry.PrimaryPhonenumber;
                    inserted.Add(feed.Insert(entry));
                }
            }


            List<ContactEntry> list = new List<ContactEntry>();

            feed = service.Query(query);
            foreach (ContactEntry e in feed.Entries)
            {
                list.Add(e);
            }

            while (feed.NextChunk != null)
            {
                ContactsQuery nq = new ContactsQuery(feed.NextChunk); 
                feed = service.Query(nq);
                foreach (ContactEntry e in feed.Entries)
                {
                    list.Add(e);
                }
            }
            


            if (inserted.Count > 0)
            {
                int iVer = numberOfInserts;
                // let's find those guys
                for (int i = 0; i < inserted.Count; i++)
                {
                    ContactEntry test = inserted[i] as ContactEntry;
                    foreach (ContactEntry e in list)
                    {
                        if (e.Id == test.Id)
                        {
                            iVer--;
                            // verify we got the phonenumber back....
                            Assert.IsTrue(e.PrimaryPhonenumber != null, "They should have a primary phonenumber");
                            Assert.AreEqual(e.PrimaryPhonenumber.Value,p.Value, "They should be identical");
                        }
                    }
                }

                Assert.IsTrue(iVer == 0, "The new entries should all be part of the feed now");
            }

            // now delete them again

            foreach (ContactEntry e in inserted)
            {
                e.Delete();
            }

            // now make sure they are gone
            if (inserted.Count > 0)
            {
                int iVer = inserted.Count;
                feed = service.Query(query);

                // let's find those guys, we should not find ANY
                for (int i = 0; i < inserted.Count; i++)
                {
                    ContactEntry test = inserted[i] as ContactEntry;
                    foreach (ContactEntry e in feed.Entries)
                    {
                        Assert.IsTrue(e.Id != test.Id, "The new entries should all be deleted now");
                    }
                }
                Assert.IsTrue(feed.Entries.Count == originalCount, "The count should be correct as well");
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Tests the primary Accessors</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void TestPrimaryContactsProperties()
        {
            Tracing.TraceMsg("Entering TestPrimaryContactsProperties");

            ContactEntry entry = new ContactEntry();

            EMail e = new EMail();
            e.Primary = true;
            e.Address = "joe@smith.com";

            Assert.IsTrue(entry.PrimaryEmail == null, "Entry should have no primary Email");
            entry.Emails.Add(e);
            Assert.IsTrue(entry.PrimaryEmail == e, "Entry should have one primary Email");

            entry.Emails.Remove(e);
            Assert.IsTrue(entry.PrimaryEmail == null, "Entry should have no primary Email");

            entry.Emails.Add(e);
            Assert.IsTrue(entry.PrimaryEmail == e, "Entry should have one primary Email");

            entry.Emails.RemoveAt(0);
            Assert.IsTrue(entry.PrimaryEmail == null, "Entry should have no primary Email");

            foreach (Object o in entry.ExtensionElements)
            {
                if (o is EMail)
                {
                    Assert.IsTrue(o == null, "There should be no email in the collection");
                }
            }


            PostalAddress p = new PostalAddress("Testaddress");
            p.Primary = true;
            Assert.IsTrue(entry.PrimaryPostalAddress == null, "Entry should have no primary Postal");
            entry.PostalAddresses.Add(p);
            Assert.IsTrue(entry.PrimaryPostalAddress == p, "Entry should have one primary Postal");
            entry.PostalAddresses.Remove(p);
            Assert.IsTrue(entry.PrimaryPostalAddress == null, "Entry should have no primary Postal");

            PhoneNumber n = new PhoneNumber("123345");
            n.Primary = true;

            Assert.IsTrue(entry.PrimaryPhonenumber == null, "Entry should have no primary Phonenumber");
            entry.Phonenumbers.Add(n);
            Assert.IsTrue(entry.PrimaryPhonenumber == n, "Entry should have one primary Phonenumber");

            entry.Phonenumbers.Remove(n);
            Assert.IsTrue(entry.PrimaryPhonenumber == null, "Entry should have no primary Phonenumber");

            IMAddress i = new IMAddress("joe@smight.com");
            i.Primary = true;

            Assert.IsTrue(entry.PrimaryIMAddress == null, "Entry should have no primary IM");
            entry.IMs.Add(new IMAddress());
            entry.IMs.Add(i);
            Assert.IsTrue(entry.PrimaryIMAddress == i, "Entry should have one primary IMAddress");

            entry.IMs.Remove(i);
            Assert.IsTrue(entry.PrimaryIMAddress == null, "Entry should have no primary IM");
        }

    }
}




