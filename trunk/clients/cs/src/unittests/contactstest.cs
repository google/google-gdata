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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Removed warnings
* 
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
using Google.Contacts;





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
                    Assert.IsTrue(entry.Etag != null, "contact entries should have etags");
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

            GroupsQuery query = new GroupsQuery(ContactsQuery.CreateGroupsUri(this.userName));
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
            ContactsQuery q = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName));
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
            currentEntry = currentEntry.Update();
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


        /////////////////////////////////////////////////////////////////////
        /// <summary>runs an basic auth test against the groups feed test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GroupsSystemTest()
        {
            Tracing.TraceMsg("Entering GroupsSystemTest");

            GroupsQuery query = new GroupsQuery(ContactsQuery.CreateGroupsUri(this.userName));
            ContactsService service = new ContactsService("unittests");

            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }

            GroupsFeed feed = service.Query(query);

            int i = 0; 
            foreach (GroupEntry g in feed.Entries )
            {
               if (g.SystemGroup != null)
               {
                   i++;
               }
            }

            Assert.IsTrue(i==4, "There should be 4 system groups in the groups feed");


            ObjectModelHelper.DumpAtomObject(feed,CreateDumpFileName("GroupsAuthTest"));

            GroupEntry newGroup = new GroupEntry();
            newGroup.Title.Text = "Private Data";

            GroupEntry insertedGroup = feed.Insert(newGroup);

            GroupEntry g2 = new GroupEntry();
            g2.Title.Text = "Another Private Group";
            GroupEntry insertedGroup2 = feed.Insert(g2);

            // now insert a new contact that belongs to that group
            ContactsQuery q = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName));
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
            currentEntry = currentEntry.Update();
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
        [Ignore("with v2, it is not clear anymore how to force a conflict")]
        public void ConflictContactsTest()
        {
            const int numberOfInserts = 50;
            const int numberWithAdds = 60; 
            Tracing.TraceMsg("Entering InsertContactsTest");

            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName));
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
                        catch (GDataRequestException)
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
                using (FileStream fs = new FileStream(this.resourcePath + "contactphoto.jpg", System.IO.FileMode.Open))
                {
                    Stream res = contactService.StreamSend(entry.PhotoUri, fs, GDataRequestType.Update, "image/jpg", null);
                    res.Close();
                }
            } finally   
            {
            }
        }



        private void DeleteAllContacts()
        {
            RequestSettings rs = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            ContactsTestSuite.DeleteAllContacts(rs);
     
        }

        internal static void DeleteAllContacts(RequestSettings rs)
        {

            rs.AutoPaging = true; 
            ContactsRequest cr = new ContactsRequest(rs);
            Feed<Contact> f = cr.GetContacts();

            List<Contact> list = new List<Contact>();
            int i=0; 

            foreach (Contact c in f.Entries)
            {
                c.BatchData = new GDataBatchEntryData();
                c.BatchData.Id = i.ToString();
                c.BatchData.Type = GDataBatchOperationType.delete;
                i++;
                list.Add(c);
            }

            cr.Batch(list, new Uri(f.AtomFeed.Batch), GDataBatchOperationType.delete);

            f = cr.GetContacts();


            Assert.IsTrue(f.TotalResults == 0, "Feed should be empty now");
        }

        
        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new contact</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void InsertContactsTest()
        {
            const int numberOfInserts = 37;
            Tracing.TraceMsg("Entering InsertContactsTest");

            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri(this.userName)); 
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

                Assert.IsTrue(iVer == 0, "The new entries should all be part of the feed now, we have " + iVer + " now");
            }

            // now delete them again

            foreach (ContactEntry e in inserted)
            {
                e.Delete();
            }

            // now make sure they are gone
            if (inserted.Count > 0)
            {
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
        /// <summary>runs an authentication test, inserts a new contact with an extended property</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void InsertExtendedPropertyContactsTest()
        {
            Tracing.TraceMsg("Entering InsertExtendedPropertyContactsTest");

            DeleteAllContacts();

            RequestSettings rs = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            rs.AutoPaging = true;

            FeedQuery query = new FeedQuery();
            query.Uri = new Uri(CreateUri(this.resourcePath + "contactsextendedprop.xml"));

         

            ContactsRequest cr = new ContactsRequest(rs);

            Feed<Contact> f = cr.Get<Contact>(query);

            Contact newEntry = null;

            foreach (Contact c in f.Entries)
            {
                ExtendedProperty e = c.ExtendedProperties[0];
                Assert.IsTrue(e != null);
                newEntry = c;
            }

            f = cr.GetContacts();

            Contact createdEntry = cr.Insert<Contact>(f, newEntry);

            cr.Delete(createdEntry);



   
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


            StructuredPostalAddress p = CreatePostalAddress();
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

        public static StructuredPostalAddress CreatePostalAddress()
        {
            StructuredPostalAddress p = new StructuredPostalAddress();
            p.City = "TestTown";
            p.Street = "Rosanna Drive";
            p.Postcode = "12345";
            p.Country = "The good ole Country";

            p.Primary = true;

            return p;
        }
      
        
        //////////////////////////////////////////////////////////////////////
        /// <summary>Tests the primary Accessors</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void ModelPrimaryContactsProperties()
        {
            Tracing.TraceMsg("Entering TestModelPrimaryContactsProperties");

            Contact c = new Contact();

            EMail e = new EMail();
            e.Primary = true;
            e.Address = "joe@smith.com";

            Assert.IsTrue(c.PrimaryEmail == null, "Contact should have no primary Email");
            c.Emails.Add(e);
            Assert.IsTrue(c.PrimaryEmail == e, "Contact should have one primary Email");

            c.Emails.Remove(e);
            Assert.IsTrue(c.PrimaryEmail == null, "Contact should have no primary Email");

            c.Emails.Add(e);
            Assert.IsTrue(c.PrimaryEmail == e, "Contact should have one primary Email");

            c.Emails.RemoveAt(0);
            Assert.IsTrue(c.PrimaryEmail == null, "Contact should have no primary Email");

            foreach (Object o in c.ContactEntry.ExtensionElements)
            {
                if (o is EMail)
                {
                    Assert.IsTrue(o == null, "There should be no email in the collection");
                }
            }

            StructuredPostalAddress p = CreatePostalAddress();
            Assert.IsTrue(c.PrimaryPostalAddress == null, "Contact should have no primary Postal");
            c.PostalAddresses.Add(p);
            Assert.IsTrue(c.PrimaryPostalAddress == p, "Contact should have one primary Postal");
            c.PostalAddresses.Remove(p);
            Assert.IsTrue(c.PrimaryPostalAddress == null, "Contact should have no primary Postal");

            PhoneNumber n = new PhoneNumber("123345");
            n.Primary = true;

            Assert.IsTrue(c.PrimaryPhonenumber == null, "Contact should have no primary Phonenumber");
            c.Phonenumbers.Add(n);
            Assert.IsTrue(c.PrimaryPhonenumber == n, "Contact should have one primary Phonenumber");

            c.Phonenumbers.Remove(n);
            Assert.IsTrue(c.PrimaryPhonenumber == null, "Contact should have no primary Phonenumber");

            IMAddress i = new IMAddress("joe@smight.com");
            i.Primary = true;

            Assert.IsTrue(c.PrimaryIMAddress == null, "Contact should have no primary IM");
            c.IMs.Add(new IMAddress());
            c.IMs.Add(i);
            Assert.IsTrue(c.PrimaryIMAddress == i, "Contact should have one primary IMAddress");

            c.IMs.Remove(i);
            Assert.IsTrue(c.PrimaryIMAddress == null, "Contact should have no primary IM");
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new contact</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void ModelInsertContactsTest()
        {
            const int numberOfInserts = 37;
            Tracing.TraceMsg("Entering ModelInsertContactsTest");

            DeleteAllContacts();


            RequestSettings rs = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            rs.AutoPaging = true; 

            ContactsRequest cr = new ContactsRequest(rs);

            Feed<Contact> f = cr.GetContacts();

            int originalCount = f.TotalResults;

            PhoneNumber p = null;
            List<Contact> inserted = new List<Contact>();

            if (f != null)
            {
                Assert.IsTrue(f.Entries != null, "the contacts needs entries");

                for (int i = 0; i < numberOfInserts; i++)
                {
                    Contact entry = new Contact();
                    entry.AtomEntry = ObjectModelHelper.CreateContactEntry(i);
                    entry.PrimaryEmail.Address = "joe" + i.ToString() + "@doe.com";
                    p = entry.PrimaryPhonenumber;
                    inserted.Add(cr.Insert(f, entry));
                }
            }


            List<Contact> list = new List<Contact>();

            f = cr.GetContacts();
            foreach (Contact e in f.Entries)
            {
                list.Add(e);
            }

            if (inserted.Count > 0)
            {
                int iVer = numberOfInserts;
                // let's find those guys
                for (int i = 0; i < inserted.Count; i++)
                {
                    Contact test = inserted[i];
                    foreach (Contact e in list)
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

                Assert.IsTrue(iVer == 0, "The new entries should all be part of the feed now, " + iVer + " left over");
            }

            // now delete them again
            DeleteList(inserted, cr, new Uri(f.AtomFeed.Batch));

            // now make sure they are gone
            if (inserted.Count > 0)
            {
                f = cr.GetContacts();
                Assert.IsTrue(f.TotalResults == originalCount, "The count should be correct as well");
                foreach (Contact e in f.Entries)
                {
                    // let's find those guys, we should not find ANY
                    for (int i = 0; i < inserted.Count; i++)
                    {
                        Contact test = inserted[i] as Contact;
                        Assert.IsTrue(e.Id != test.Id, "The new entries should all be deleted now");
                    }
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////

 
        internal static void DeleteList(List<Contact> list, ContactsRequest cr, Uri batch)
        {
            int i = 0; 
            foreach (Contact c in list)
            {
                c.BatchData = new GDataBatchEntryData();
                c.BatchData.Id = i.ToString();
                i++;
            }

            cr.Batch(list, batch, GDataBatchOperationType.delete);
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new contact</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void ModelUpdateContactsTest()
        {
            const int numberOfInserts = 5;
            Tracing.TraceMsg("Entering ModelInsertContactsTest");

            DeleteAllContacts();

            RequestSettings rs = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            rs.AutoPaging = true; 

            ContactsRequest cr = new ContactsRequest(rs);

            Feed<Contact> f = cr.GetContacts();

            int originalCount = f.TotalResults;

            PhoneNumber p = null;
            List<Contact> inserted = new List<Contact>();

            if (f != null)
            {
                Assert.IsTrue(f.Entries != null, "the contacts needs entries");

                for (int i = 0; i < numberOfInserts; i++)
                {
                    Contact entry = new Contact();
                    entry.AtomEntry = ObjectModelHelper.CreateContactEntry(i);
                    entry.PrimaryEmail.Address = "joe" + i.ToString() + "@doe.com";
                    p = entry.PrimaryPhonenumber;
                    inserted.Add(cr.Insert(f, entry));
                }
            }

            string newTitle = "This is an update to the title";
            f = cr.GetContacts();
            if (inserted.Count > 0)
            {
                int iVer = numberOfInserts;
                // let's find those guys
                foreach (Contact e in f.Entries )
                {
                    for (int i = 0; i < inserted.Count; i++)
                    {
                        Contact test = inserted[i];
                        if (e.Id == test.Id)
                        {
                            iVer--;
                            // verify we got the phonenumber back....
                            Assert.IsTrue(e.PrimaryPhonenumber != null, "They should have a primary phonenumber");
                            Assert.AreEqual(e.PrimaryPhonenumber.Value,p.Value, "They should be identical");
                            e.Name.FamilyName = newTitle;
                            inserted[i] = cr.Update(e);
                        }
                    }
                }

                Assert.IsTrue(iVer == 0, "The new entries should all be part of the feed now, we have " + iVer + " left");
            }

            f = cr.GetContacts();
            if (inserted.Count > 0)
            {
                int iVer = numberOfInserts;
                // let's find those guys
                foreach (Contact e in f.Entries )
                {
                    for (int i = 0; i < inserted.Count; i++)
                    {
                        Contact test = inserted[i];
                        if (e.Id == test.Id)
                        {
                            iVer--;
                            // verify we got the phonenumber back....
                            Assert.IsTrue(e.PrimaryPhonenumber != null, "They should have a primary phonenumber");
                            Assert.AreEqual(e.PrimaryPhonenumber.Value,p.Value, "They should be identical");
                            Assert.AreEqual(e.Name.FamilyName, newTitle, "The familyname should have been updated");
                        }
                    }
                }
                Assert.IsTrue(iVer == 0, "The new entries should all be part of the feed now, we have: " +  iVer + " now");
            }


            // now delete them again
            DeleteList(inserted, cr, new Uri(f.AtomFeed.Batch));

            // now make sure they are gone
            if (inserted.Count > 0)
            {
                int iVer = inserted.Count;
                f = cr.GetContacts();
                foreach (Contact e in f.Entries)
                {
                    // let's find those guys, we should not find ANY
                    for (int i = 0; i < inserted.Count; i++)
                    {
                        Contact test = inserted[i] as Contact;
                        Assert.IsTrue(e.Id != test.Id, "The new entries should all be deleted now");
                    }
                }
                Assert.IsTrue(f.TotalResults == originalCount, "The count should be correct as well");
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new contact</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void ModelUpdateIfMatchAllContactsTest()
        {
            const int numberOfInserts = 5;
            Tracing.TraceMsg("Entering ModelInsertContactsTest");

            DeleteAllContacts();

            RequestSettings rs = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            rs.AutoPaging = true;

            ContactsRequest cr = new ContactsRequest(rs);

            Feed<Contact> f = cr.GetContacts();

            int originalCount = f.TotalResults;

            PhoneNumber p = null;
            List<Contact> inserted = new List<Contact>();

            if (f != null)
            {
                Assert.IsTrue(f.Entries != null, "the contacts needs entries");

                for (int i = 0; i < numberOfInserts; i++)
                {
                    Contact entry = new Contact();
                    entry.AtomEntry = ObjectModelHelper.CreateContactEntry(i);
                    entry.PrimaryEmail.Address = "joe" + i.ToString() + "@doe.com";
                    p = entry.PrimaryPhonenumber;
                    inserted.Add(cr.Insert(f, entry));
                }
            }

            string newTitle = "This is an update to the title";
            f = cr.GetContacts();
            if (inserted.Count > 0)
            {
                int iVer = numberOfInserts;
                // let's find those guys
                foreach (Contact e in f.Entries)
                {
                    for (int i = 0; i < inserted.Count; i++)
                    {
                        Contact test = inserted[i];
                        if (e.Id == test.Id)
                        {
                            iVer--;
                            // verify we got the phonenumber back....
                            Assert.IsTrue(e.PrimaryPhonenumber != null, "They should have a primary phonenumber");
                            Assert.AreEqual(e.PrimaryPhonenumber.Value, p.Value, "They should be identical");
                            e.Name.FamilyName = newTitle;
                            e.ETag = GDataRequestFactory.IfMatchAll;
                            inserted[i] = cr.Update(e);
                        }
                    }
                }

                Assert.IsTrue(iVer == 0, "The new entries should all be part of the feed now, we have " + iVer + " left");
            }

            f = cr.GetContacts();
            if (inserted.Count > 0)
            {
                int iVer = numberOfInserts;
                // let's find those guys
                foreach (Contact e in f.Entries)
                {
                    for (int i = 0; i < inserted.Count; i++)
                    {
                        Contact test = inserted[i];
                        if (e.Id == test.Id)
                        {
                            iVer--;
                            // verify we got the phonenumber back....
                            Assert.IsTrue(e.PrimaryPhonenumber != null, "They should have a primary phonenumber");
                            Assert.AreEqual(e.PrimaryPhonenumber.Value, p.Value, "They should be identical");
                            Assert.AreEqual(e.Name.FamilyName, newTitle, "The familyname should have been updated");
                        }
                    }
                }
                Assert.IsTrue(iVer == 0, "The new entries should all be part of the feed now, we have: " + iVer + " now");
            }


            // now delete them again
            DeleteList(inserted, cr, new Uri(f.AtomFeed.Batch));



            // now make sure they are gone
            if (inserted.Count > 0)
            {
                int iVer = inserted.Count;
                f = cr.GetContacts();
                foreach (Contact e in f.Entries)
                {
                    // let's find those guys, we should not find ANY
                    for (int i = 0; i < inserted.Count; i++)
                    {
                        Contact test = inserted[i] as Contact;
                        Assert.IsTrue(e.Id != test.Id, "The new entries should all be deleted now");
                    }
                }
                Assert.IsTrue(f.TotalResults == originalCount, "The count should be correct as well");
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>tests querying contacts with an etag</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void ModelTestETagQuery()
        {
            Tracing.TraceMsg("Entering ModelTestETagQuery");

            RequestSettings rs = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            rs.AutoPaging = true;

            ContactsRequest cr = new ContactsRequest(rs);

            Feed<Contact> f = cr.GetContacts();

            ContactsQuery q = new ContactsQuery(ContactsQuery.CreateContactsUri(null));

            q.Etag = ((ISupportsEtag)f.AtomFeed).Etag;

            try
            {
                f = cr.Get<Contact>(q);
                foreach (Contact c in f.Entries)
                {
                }
            }
            catch (GDataNotModifiedException g)
            {
                Assert.IsTrue(g != null);
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new contact</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void ModelPhotoTest()
        {
            Tracing.TraceMsg("Entering ModelPhotoTest");

            RequestSettings rs = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            rs.AutoPaging = true; 

            ContactsRequest cr = new ContactsRequest(rs);

            Feed<Contact> f = cr.GetContacts();

            Contact e = null;

            if (f != null)
            {
                Contact entry = new Contact();
                entry.AtomEntry = ObjectModelHelper.CreateContactEntry(1);
                entry.PrimaryEmail.Address = "joe@doe.com";
                e = cr.Insert(f, entry);
                
            }
            Assert.IsTrue(e!=null, "we should have a contact here");

            Stream s = cr.GetPhoto(e);

            Assert.IsTrue(s == null, "There should be no photo yet"); 


            using (FileStream fs = new FileStream(this.resourcePath + "contactphoto.jpg", System.IO.FileMode.Open))
            {
                cr.SetPhoto(e, fs); 
            }

            // now delete the guy, which requires us to reload him from the server first, as the photo change operation
            // changes the etag off the entry
            e = cr.Retrieve(e);
            cr.Delete(e);
        }
        /////////////////////////////////////////////////////////////////////////////
        
        
        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test, inserts a new contact</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void ModelBatchContactsTest()
        {
            const int numberOfInserts = 5;
            Tracing.TraceMsg("Entering ModelInsertContactsTest");

            DeleteAllContacts();

            RequestSettings rs = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            rs.AutoPaging = true; 

            ContactsRequest cr = new ContactsRequest(rs);

            List<Contact> list = new List<Contact>();

            Feed<Contact> f = cr.GetContacts();

            for (int i = 0; i < numberOfInserts; i++)
            {
                Contact entry = new Contact();
                entry.AtomEntry = ObjectModelHelper.CreateContactEntry(i);
                entry.PrimaryEmail.Address = "joe" + i.ToString() + "@doe.com";
                GDataBatchEntryData g = new GDataBatchEntryData();
                g.Id = i.ToString(); 
                g.Type = GDataBatchOperationType.insert;
                entry.BatchData = g;
                list.Add(entry);
            }

            Feed<Contact> r = cr.Batch(list, new Uri(f.AtomFeed.Batch), GDataBatchOperationType.Default);
            list.Clear();

            int iVerify = 0;

            foreach (Contact c in r.Entries )
            {
                // let's count and update them
                iVerify++; 
                c.Name.FamilyName = "get a nother one"; 
                c.BatchData.Type = GDataBatchOperationType.update;
                list.Add(c);
            }
            Assert.IsTrue(iVerify == numberOfInserts, "should have gotten 5 inserts");

            Feed<Contact> u = cr.Batch(list, new Uri(f.AtomFeed.Batch), GDataBatchOperationType.Default);
            list.Clear();

            iVerify = 0; 
            foreach (Contact c in u.Entries )
            {
                // let's count and update them
                iVerify++; 
                c.BatchData.Type = GDataBatchOperationType.delete;
                list.Add(c);
            }
            Assert.IsTrue(iVerify == numberOfInserts, "should have gotten 5 updates");

            Feed<Contact> d = cr.Batch(list, new Uri(f.AtomFeed.Batch), GDataBatchOperationType.Default);

            iVerify = 0; 
            foreach (Contact c in d.Entries )
            {
                if (c.BatchData.Status.Code == 200)
                {
                    // let's count and update them
                    iVerify++; 
                }
            }
            Assert.IsTrue(iVerify == numberOfInserts, "should have gotten 5 deletes");

        }
        /////////////////////////////////////////////////////////////////////////////
 

        /////////////////////////////////////////////////////////////////////
        /// <summary>runs an basic auth test against the groups feed test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GroupsModelTest()
        {
            Tracing.TraceMsg("Entering GroupsModelTest");


            RequestSettings rs = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            rs.AutoPaging = true; 
            ContactsRequest cr = new ContactsRequest(rs);

            Feed<Group> fg = cr.GetGroups();

            Group newGroup = new Group();
            newGroup.Title = "Private Data";

            Group insertedGroup = cr.Insert(fg, newGroup);

            Group g2 = new Group();
            g2.Title = "Another private Group"; 

            Group insertedGroup2 = cr.Insert(fg, g2); 

            // now insert a new contact that belongs to that group
            Feed<Contact> fc = cr.GetContacts();
            Contact c = new Contact();
            c.AtomEntry = ObjectModelHelper.CreateContactEntry(1);

            GroupMembership member = new GroupMembership();
            member.HRef = insertedGroup.Id; 


            GroupMembership member2 = new GroupMembership();
            member2.HRef = insertedGroup2.Id; 
            
            Contact insertedEntry = cr.Insert(fc, c);

            // now change the group membership
            insertedEntry.GroupMembership.Add(member);
            insertedEntry.GroupMembership.Add(member2);
            Contact currentEntry = cr.Update(insertedEntry);

            Assert.IsTrue(currentEntry.GroupMembership.Count == 2, "The entry should be in 2 groups");

            currentEntry.GroupMembership.Clear();
            currentEntry = cr.Update(currentEntry);
            Assert.IsTrue(currentEntry.GroupMembership.Count == 0, "The entry should not be in a group");

            cr.Delete(currentEntry);
            cr.Delete(insertedGroup);
            cr.Delete(insertedGroup2);

        }
        /////////////////////////////////////////////////////////////////////////////
    }
}




