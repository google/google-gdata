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
using Google.GData.Calendar;
using Google.GData.AccessControl;




namespace Google.GData.Client.LiveTests
{
    [TestFixture] 
    [Category("LiveTest")]
    public class CalendarTestSuite : BaseLiveTestClass
    {
        /// <summary>
        ///  test Uri for google calendarURI
        /// </summary>
        protected string defaultCalendarUri;

        /// <summary>
        /// test URI for Google Calendar owncalendars feed
        /// </summary>
        protected string defaultOwnCalendarsUri;

        /// <summary>
        ///  test Uri for google acl feed
        /// </summary>
        protected string aclFeedUri; 


        /// <summary>
        ///  test Uri for google composite calendarURI
        /// </summary>
        protected string defaultCompositeUri; 

        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public CalendarTestSuite()
        {
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>the setup method</summary> 
        //////////////////////////////////////////////////////////////////////
        [SetUp] public override void InitTest()
        {
            Tracing.TraceCall();
            base.InitTest(); 
            GDataGAuthRequestFactory authFactory = this.factory as GDataGAuthRequestFactory; 
            if (authFactory != null)
            {
                authFactory.Handler = this.strAuthHandler; 
            }

            FeedCleanup(this.defaultCalendarUri, this.userName, this.passWord, VersionDefaults.Major);

        }
        /////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////
        /// <summary>the end it all method</summary> 
        //////////////////////////////////////////////////////////////////////
        [TearDown] public override void EndTest()
        {
            Tracing.TraceCall();
            FeedCleanup(this.defaultCalendarUri, this.userName, this.passWord, VersionDefaults.Major);
            Tracing.ExitTracing();
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>private void ReadConfigFile()</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("calendarURI"))
            {
                this.defaultCalendarUri = (string) unitTestConfiguration["calendarURI"];
                Tracing.TraceInfo("Read calendarURI value: " + this.defaultCalendarUri);
            }
            if (unitTestConfiguration.Contains("aclFeedURI"))
            {
                this.aclFeedUri = (string) unitTestConfiguration["aclFeedURI"];
                Tracing.TraceInfo("Read aclFeed value: " + this.aclFeedUri);
            }
            if (unitTestConfiguration.Contains("compositeURI"))
            {
                this.defaultCompositeUri = (string) unitTestConfiguration["compositeURI"];
                Tracing.TraceInfo("Read compositeURI value: " + this.defaultCompositeUri);
            }
            if (unitTestConfiguration.Contains("ownCalendarsURI"))
            {
                this.defaultOwnCalendarsUri = (string) unitTestConfiguration["ownCalendarsURI"];
                Tracing.TraceInfo("Read ownCalendarsURI value: " + this.defaultOwnCalendarsUri);
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GoogleAuthenticationTest()
        {
            Tracing.TraceMsg("Entering GoogleAuthenticationTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service();

            int iCount; 

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                AtomFeed calFeed = service.Query(query);

                ObjectModelHelper.DumpAtomObject(calFeed,CreateDumpFileName("AuthenticationTest")); 
                iCount = calFeed.Entries.Count; 

                String strTitle = "Dinner time" + Guid.NewGuid().ToString(); 

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // get the first entry
                    AtomEntry entry = calFeed.Entries[0];

                    entry = ObjectModelHelper.CreateAtomEntry(1); 
                    entry.Title.Text = strTitle;
                    AtomEntry newEntry = calFeed.Insert(entry); 
                    iCount++; 
                    Tracing.TraceMsg("Created calendar entry");

                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 

                    AtomFeed newFeed = service.Query(singleQuery);

                    AtomEntry sameGuy = newFeed.Entries[0]; 

                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 
                }
                calFeed = service.Query(query);
                Assert.AreEqual(iCount, calFeed.Entries.Count, "Feed should have one more entry, it has: " + calFeed.Entries.Count); 

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (AtomEntry entry in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            entry.Content.Content = "Maybe stay until breakfast";
                            entry.Update();
                            Tracing.TraceMsg("Updated entry");
                        }
                    }
                }

                calFeed = service.Query(query);

                Assert.AreEqual(iCount, calFeed.Entries.Count, "Feed should have one more entry, it has: " + calFeed.Entries.Count); 

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (AtomEntry entry in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            entry.Delete();
                            iCount--; 
                            Tracing.TraceMsg("deleted entry");
                        }
                    }
                }
                calFeed = service.Query(query);

                Assert.AreEqual(iCount, calFeed.Entries.Count, "Feed should have the same count again, it has: " + calFeed.Entries.Count); 
                service.Credentials = null; 
            }

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Ignore ("Currently broken on the server")]
        [Test] public void CalendarXHTMLTest()
        {
            Tracing.TraceMsg("Entering CalendarXHTMLTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service();

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                AtomFeed calFeed = service.Query(query);

                String strTitle = "Dinner time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    Tracing.TraceMsg("Created calendar entry");
                    String xhtmlContent = "<div><b>this is an xhtml test text</b></div>"; 
                    AtomEntry entry = ObjectModelHelper.CreateAtomEntry(1); 
                    Tracing.TraceMsg("Created calendar entry");
                    entry.Title.Text = strTitle;
                    entry.Content.Type = "xhtml";
                    Tracing.TraceMsg("Created calendar entry");
                    entry.Content.Content = xhtmlContent;

                    AtomEntry newEntry = calFeed.Insert(entry); 
                    Tracing.TraceMsg("Created calendar entry");

                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 
                    AtomFeed newFeed = service.Query(singleQuery);
                    AtomEntry sameGuy = newFeed.Entries[0]; 

                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 
                    Assert.IsTrue(sameGuy.Content.Type.Equals("xhtml"));
                    Assert.IsTrue(sameGuy.Content.Content.Equals(xhtmlContent)); 

                }

                service.Credentials = null; 
            }

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarExtensionTest()
        {
            Tracing.TraceMsg("Entering CalendarExtensionTest");

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

            int iCount; 

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query) as EventFeed;

                if (calFeed.TimeZone != null)
                {
                    Tracing.TraceMsg(calFeed.TimeZone.Value); 
                }

                iCount = calFeed.Entries.Count; 

               String strTitle = "Dinner & time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    EventEntry entry  = ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = strTitle;

                    EventEntry newEntry = (EventEntry) calFeed.Insert(entry); 

                    iCount++; 
                    Tracing.TraceMsg("Created calendar entry");

                    Reminder rNew = null;
                    Reminder rOld = null;
                    if (newEntry.Reminders.Count > 0)
                    {
                        rNew = newEntry.Reminders[0] as Reminder;
                    }
                    if (entry.Reminders.Count > 0)
                    {
                        rOld = entry.Reminders[0] as Reminder;
                    }

                    Assert.IsTrue(rNew != null, "Reminder should not be NULL);");
                    Assert.IsTrue(rOld != null, "Original Reminder should not be NULL);");
                    Assert.AreEqual(rNew.Minutes, rOld.Minutes, "Reminder time should be identical"); 

                    Where wOldOne, wOldTwo;
                    Where wNewOne;

                    Assert.IsTrue(entry.Locations.Count == 2, "entry should have 2 locations");
                    // calendar ignores sending more than one location
                    Assert.IsTrue(newEntry.Locations.Count == 1, "new entry should have 1 location");


                    if (entry.Locations.Count > 1)
                    {
                        wOldOne = entry.Locations[0];
                        wOldTwo = entry.Locations[1];
                    
                        if (newEntry.Locations.Count == 1)
                        {
                            wNewOne = newEntry.Locations[0];
                            Assert.IsTrue(wOldOne != null, "Where oldOne should not be NULL);");
                            Assert.IsTrue(wOldTwo != null, "Where oldTwo should not be NULL);");
                            Assert.IsTrue(wNewOne != null, "Where newOne should not be NULL);");
                            Assert.IsTrue(wOldOne.ValueString == wNewOne.ValueString, "location one should be identical");
                        }
                    }

                    newEntry.Content.Content = "Updated..";
                    newEntry.Update();

                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 

                    EventFeed newFeed = service.Query(query) as EventFeed;
                    EventEntry sameGuy = newFeed.Entries[0] as EventEntry; 

                    sameGuy.Content.Content = "Updated again..."; 
                    When x = sameGuy.Times[0]; 
                    sameGuy.Times.Clear();
                    x.StartTime = DateTime.Now; 
                    sameGuy.Times.Add(x); 
                    sameGuy.Update();

                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 

                }

                calFeed = service.Query(query) as EventFeed;

                Assert.AreEqual(iCount, calFeed.Entries.Count, "Feed should have one more entry, it has: " + calFeed.Entries.Count); 

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (EventEntry entry in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            Assert.AreEqual(ObjectModelHelper.DEFAULT_REMINDER_TIME, entry.Reminder.Minutes, "Reminder time should be identical"); 
                            entry.Content.Content = "Maybe stay until breakfast";
                            entry.Update();
                            Tracing.TraceMsg("Updated entry");
                        }
                    }
                }

                calFeed = service.Query(query) as EventFeed;

                Assert.AreEqual(iCount, calFeed.Entries.Count, "Feed should have one more entry, it has: " + calFeed.Entries.Count); 

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (AtomEntry entry in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            entry.Delete();
                            iCount--; 
                            Tracing.TraceMsg("deleted entry");
                        }
                    }
                }

                calFeed = service.Query(query) as EventFeed;
                Assert.AreEqual(iCount, calFeed.Entries.Count, "Feed should have the same count again, it has: " + calFeed.Entries.Count); 

                service.Credentials = null; 
            }

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarQuickAddTest()
        {
            Tracing.TraceMsg("Entering CalendarQuickAddTest");

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query) as EventFeed;

                if (calFeed != null)
                {
                    // get the first entry
                    EventEntry entry  = new EventEntry();
                    entry.Content.Content = "Dinner with Sabine, Oct 1st, 10pm";
                    entry.Content.Type = "html";
                    entry.QuickAdd = true;


                    EventEntry newEntry = (EventEntry) calFeed.Insert(entry); 

                    Assert.IsTrue(newEntry.Title.Text.StartsWith("Dinner with Sabine"), "both titles should be identical" + newEntry.Title.Text); 

                }
                service.Credentials = null; 
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a test of batch support on the events feed</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarBatchTest()
        {
            Tracing.TraceMsg("Entering CalendarBatchTest");

            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory;

                EventQuery query = new EventQuery(this.defaultCalendarUri);

                EventFeed feed = service.Query(query);
                AtomFeed batchFeed = new AtomFeed(feed);

                string newEntry1Title = "new event" + Guid.NewGuid().ToString();
                EventEntry newEntry1 = new EventEntry(newEntry1Title);
                newEntry1.BatchData = new GDataBatchEntryData("1", GDataBatchOperationType.insert);
                batchFeed.Entries.Add(newEntry1);

                string newEntry2Title = "new event" + Guid.NewGuid().ToString();
                EventEntry newEntry2 = new EventEntry(newEntry2Title);
                newEntry2.BatchData = new GDataBatchEntryData("2", GDataBatchOperationType.insert);
                batchFeed.Entries.Add(newEntry2);

                string newEntry3Title = "new event" + Guid.NewGuid().ToString();
                EventEntry newEntry3 = new EventEntry(newEntry3Title);
                newEntry3.BatchData = new GDataBatchEntryData("3", GDataBatchOperationType.insert);
                batchFeed.Entries.Add(newEntry3);

                Tracing.TraceMsg("Creating batch items");

                EventFeed batchResultFeed = (EventFeed)service.Batch(batchFeed, new Uri(feed.Batch));

                foreach (EventEntry evt in batchResultFeed.Entries)
                {
                    Assert.IsNotNull(evt.BatchData, "Result should contain batch information.");
                    Assert.IsNotNull(evt.BatchData.Id, "Result should have a Batch ID.");
                    Assert.AreEqual(201, evt.BatchData.Status.Code, "Created entries should return 201");
 
                    switch (evt.BatchData.Id)
                    {
                        case "1":
                            Assert.AreEqual(newEntry1Title, evt.Title.Text, "titles should be equal.");
                            break;
                        case "2":
                            Assert.AreEqual(newEntry2Title, evt.Title.Text, "titles should be equal.");
                            break;
                        case "3":
                            Assert.AreEqual(newEntry3Title, evt.Title.Text, "titles should be equal.");
                            break;
                        default:
                            Assert.Fail("Unrecognized entry in result of batch insert feed");
                            break;
                    }

                }

                Tracing.TraceMsg("Updating created entries.");

                batchFeed = new AtomFeed(feed);
                foreach (EventEntry evt in batchResultFeed.Entries)
                {
                    evt.BatchData = new GDataBatchEntryData(evt.BatchData.Id, GDataBatchOperationType.update);
                    evt.Title.Text = evt.Title.Text + "update";
                    batchFeed.Entries.Add(evt);
                }

                batchResultFeed = (EventFeed) service.Batch(batchFeed, new Uri(feed.Batch));

                foreach (EventEntry evt in batchResultFeed.Entries)
                {
                    Assert.IsNotNull(evt.BatchData, "Result should contain batch information.");
                    Assert.IsNotNull(evt.BatchData.Id, "Result should have a Batch ID.");
                    Assert.AreEqual(200, evt.BatchData.Status.Code, "Updated entries should return 200");

                    switch (evt.BatchData.Id)
                    {
                        case "1":
                            Assert.AreEqual(newEntry1Title + "update", evt.Title.Text, "titles should be equal.");
                            break;
                        case "2":
                            Assert.AreEqual(newEntry2Title + "update", evt.Title.Text, "titles should be equal.");
                            break;
                        case "3":
                            Assert.AreEqual(newEntry3Title + "update", evt.Title.Text, "titles should be equal.");
                            break;
                        default:
                            Assert.Fail("Unrecognized entry in result of batch update feed");
                            break;
                    }

                }



                Tracing.TraceMsg("Deleting created entries.");

                batchFeed = new AtomFeed(feed);
                foreach (EventEntry evt in batchResultFeed.Entries)
                {
                    evt.BatchData = new GDataBatchEntryData(GDataBatchOperationType.delete);
                    evt.Id = new AtomId(evt.EditUri.ToString());
                    batchFeed.Entries.Add(evt);
                }

                batchResultFeed = (EventFeed)service.Batch(batchFeed, new Uri(feed.Batch));

                foreach (EventEntry evt in batchResultFeed.Entries)
                {
                    Assert.AreEqual(200, evt.BatchData.Status.Code, "Deleted entries should return 200");
                }

                service.Credentials = null;
            }
        }
        /////////////////////////////////////////////////////////////////////////////

   
       //////////////////////////////////////////////////////////////////////
        /// <summary>Tests the reminder method property</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarReminderMethodTest()
        {
            Tracing.TraceMsg("Entering CalendarReminderMethodTest");

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

        
            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query) as EventFeed;
                
                String strTitle = "Dinner & time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    EventEntry entry  = ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = strTitle;

                    entry.Reminders.Clear();
                    
                    Reminder r1 = new Reminder();
                    r1.Method = Reminder.ReminderMethod.email;
                    r1.Minutes = 30;
                    Reminder r2 = new Reminder();
                    r2.Method = Reminder.ReminderMethod.alert;
                    r2.Minutes = 60;

                    entry.Reminders.Add(r1);
                    entry.Reminders.Add(r2);

                    EventEntry newEntry = (EventEntry) calFeed.Insert(entry); 

                    Assert.AreEqual(2, newEntry.Reminders.Count,  "There should be two reminders");

                    Reminder r3 = newEntry.Reminders[0] as Reminder;
                    Reminder r4 = newEntry.Reminders[1] as Reminder;

                    Reminder r1a;
                    Reminder r2a;
                    if (r3.Method == Reminder.ReminderMethod.email)
                    {
                        r1a = r3;
                        r2a = r4;
                    }
                    else 
                    {
                        r1a = r4;
                        r2a = r3;
                    }
                
                    Assert.AreEqual(r1.Minutes, r1a.Minutes, "Reminder time should be identical"); 
                    Assert.AreEqual(r1.Method,  r1a.Method, "Reminder method should be identical"); 
                    Assert.AreEqual(r2.Minutes, r2a.Minutes, "Reminder time should be identical"); 
                    Assert.AreEqual(r2.Method,  r2a.Method, "Reminder method should be identical"); 
                    Tracing.TraceMsg("Created calendar entry");
                }

                service.Credentials = null; 

            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Tests the ACL extensions</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarACLTest()
        {
            Tracing.TraceMsg("Entering CalendarACLTest");

            AclQuery query = new AclQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

            int iCount; 

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.aclFeedUri);
                AclFeed aclFeed = service.Query(query);
                AclEntry newEntry = null;

                foreach (AclEntry e in aclFeed.Entries ) 
                {
                    if (!e.Scope.Value.StartsWith(this.userName))
                    {
                        e.Delete();
                    }
                }
                aclFeed = service.Query(query);

                iCount = aclFeed.Entries.Count;

                if (aclFeed != null)
                {
                    // create an entry
                    AclEntry entry = new AclEntry();
                    entry.Role = AclRole.ACL_CALENDAR_FREEBUSY;
                    AclScope scope = new AclScope();
                    scope.Type = AclScope.SCOPE_USER;
                    scope.Value = "meoh2my@test.com";
                    entry.Scope = scope;

                    newEntry = (AclEntry) aclFeed.Insert(entry);

                    Assert.AreEqual(newEntry.Role.Value, entry.Role.Value);
                    Assert.AreEqual(newEntry.Scope.Type, entry.Scope.Type);
                    Assert.AreEqual(newEntry.Scope.Value, entry.Scope.Value);
                }

                Tracing.TraceMsg("CalendarACLTest: done insering Acl:entry");


                iCount++;
                aclFeed = (AclFeed) service.Query(query);

                Tracing.TraceMsg("CalendarACLTest: done query after: Acl:entry");

                // update that entry

                if (newEntry != null)
                {
                    newEntry.Role = AclRole.ACL_CALENDAR_READ;
                    newEntry = (AclEntry) newEntry.Update();
                    Assert.AreEqual(AclRole.ACL_CALENDAR_READ.Value, newEntry.Role.Value);
                }

                Tracing.TraceMsg("CalendarACLTest: done updating Acl:entry");

                newEntry.Delete();
                iCount--;

                Tracing.TraceMsg("CalendarACLTest: done deleting Acl:entry");


                aclFeed = (AclFeed) service.Query(query);
                Assert.AreEqual(iCount, aclFeed.Entries.Count, "Feed should have one more entry, it has: " + aclFeed.Entries.Count); 


                service.Credentials = null; 
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Tests the ACL extensions, this time getting the feed from the entry</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarACL2Test()
        {
            Tracing.TraceMsg("Entering CalendarACL2Test");

            CalendarQuery query = new CalendarQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }


                service.RequestFactory = this.factory;

                query.Uri = new Uri(this.defaultOwnCalendarsUri);
                CalendarFeed calFeed = service.Query(query);

                if (calFeed != null && calFeed.Entries != null && calFeed.Entries[0] != null)
                {
                   AtomLink link = calFeed.Entries[0].Links.FindService(AclNameTable.LINK_REL_ACCESS_CONTROL_LIST, null);
                   AclEntry aclEntry = new AclEntry();

                   aclEntry.Scope = new AclScope();
                   aclEntry.Scope.Type = AclScope.SCOPE_USER;
                   aclEntry.Scope.Value = "meoh2my@test.com";
                   aclEntry.Role = AclRole.ACL_CALENDAR_READ;

                   Uri aclUri = null;
                   if (link != null)
                   {
                       aclUri = new Uri(link.HRef.ToString());
                   }
                   else
                   {
                       throw new Exception("ACL link was null.");
                   }

                   AclEntry insertedEntry = service.Insert(aclUri, aclEntry); 
                   insertedEntry.Delete();
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an CalendarWebContentTest test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarWebContentTest()
        {
            Tracing.TraceMsg("Entering CalendarWebContentTest");

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query) as EventFeed;

                if (calFeed.TimeZone != null)
                {
                    Tracing.TraceMsg(calFeed.TimeZone.Value); 
                }

                String strTitle = "Dinner & time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    EventEntry entry  = ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = strTitle;

                    WebContentLink wc = new WebContentLink();
                    wc.Type = "image/gif";
                    wc.Url = "http://www.google.com/logos/july4th06.gif";
                    wc.Icon = "http://www.google.com/calendar/images/google-holiday.gif";
                    wc.Title = "Test content"; 
                    wc.Width = 270;
                    wc.Height = 130; 
                    wc.GadgetPreferences.Add("color", "blue");
                    wc.GadgetPreferences.Add("taste", "sweet");
                    wc.GadgetPreferences.Add("smell", "fresh");

                    entry.WebContentLink = wc;

                    EventEntry newEntry = (EventEntry) calFeed.Insert(entry); 

                    // check if the web content link came back
                    Assert.IsTrue(newEntry.WebContentLink != null, "the WebContentLink did not come back for the webContent"); 
                    Tracing.TraceMsg("Created calendar entry");

                    Assert.IsTrue(newEntry.WebContentLink.WebContent != null, "The returned WebContent element was not found");

                    Assert.AreEqual(3, newEntry.WebContentLink.GadgetPreferences.Count, "The gadget preferences should be there");
                    Assert.AreEqual("blue", newEntry.WebContentLink.GadgetPreferences["color"], "Color should be blue");
                    Assert.AreEqual("sweet", newEntry.WebContentLink.GadgetPreferences["taste"], "Taste should be sweet");
                    Assert.AreEqual("fresh", newEntry.WebContentLink.GadgetPreferences["smell"], "smell should be fresh");


                    newEntry.Content.Content = "Updated..";
                    newEntry.Update();


                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 

                    EventFeed newFeed = service.Query(query) as EventFeed;

                    EventEntry sameGuy = newFeed.Entries[0] as EventEntry; 

                    sameGuy.Content.Content = "Updated again..."; 
                    When x = sameGuy.Times[0]; 
                    sameGuy.Times.Clear();
                    x.StartTime = DateTime.Now; 
                    sameGuy.Times.Add(x); 
                    sameGuy.Update();


                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 

                }

                calFeed = service.Query(query) as EventFeed;


                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (EventEntry entry in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Entry title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            Assert.AreEqual(ObjectModelHelper.DEFAULT_REMINDER_TIME, entry.Reminder.Minutes, "Reminder time should be identical"); 
                            // check if the link came back
                            // check if the web content link came back
                            Assert.IsTrue(entry.WebContentLink != null, "the WebContentLink did not come back for the webContent"); 

                        }
                    }
                }

                calFeed = service.Query(query) as EventFeed;

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (EventEntry entry in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Entry title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            entry.Delete();
                            Tracing.TraceMsg("deleted entry");
                        }
                    }
                }

                service.Credentials = null; 
            }

        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarRecurranceTest()
        {
            Tracing.TraceMsg("Entering CalendarRecurranceTest");

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query) as EventFeed;

                string recur  = 
                    "DTSTART;TZID=America/Los_Angeles:20060314T060000\n" +
                    "DURATION:PT3600S\n" + 
                    "RRULE:FREQ=DAILY;UNTIL=20060321T220000Z\n" +
                    "BEGIN:VTIMEZONE\n" + 
                    "TZID:America/Los_Angeles\n" +
                    "X-LIC-LOCATION:America/Los_Angeles\n" +
                    "BEGIN:STANDARD\n" +
                    "TZOFFSETFROM:-0700\n" +
                    "TZOFFSETTO:-0800\n" +
                    "TZNAME:PST\n" +
                    "DTSTART:19671029T020000\n" +
                    "RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=-1SU\n" +
                    "END:STANDARD\n" +
                    "BEGIN:DAYLIGHT\n" +
                    "TZOFFSETFROM:-0800\n" +
                    "TZOFFSETTO:-0700\n" +
                    "TZNAME:PDT\n" +
                    "DTSTART:19870405T020000\n" +
                    "RRULE:FREQ=YEARLY;BYMONTH=4;BYDAY=1SU\n" +
                    "END:DAYLIGHT\n" +
                    "END:VTIMEZONE\n"; 

                EventEntry entry  =  ObjectModelHelper.CreateEventEntry(1); 
                entry.Title.Text = "New recurring event" + Guid.NewGuid().ToString();  

                // get rid of the when entry
                entry.Times.Clear(); 

                entry.Recurrence = new Recurrence();
                entry.Recurrence.Value = recur; 

                calFeed.Insert(entry); 

                if (calFeed.TimeZone != null)
                {
                    Tracing.TraceMsg(calFeed.TimeZone.Value); 
                }

                // requery
                calFeed = service.Query(query) as EventFeed;


                ObjectModelHelper.DumpAtomObject(calFeed,CreateDumpFileName("CalendarRecurrance")); 

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for all events that have an original Event pointer, and if so, try to find that one
                    foreach (EventEntry e in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Looping Feed entries, title: " + e.Title.Text); 
                        if (e.OriginalEvent != null)
                        {
                            Tracing.TraceMsg("Searching for original Event"); 
                            EventEntry original = calFeed.FindEvent(e.OriginalEvent); 
                            Tracing.TraceMsg("Found original Event: " + original.Title.Text); 
                            Assert.IsTrue(original != null, "can not find original event"); 
                        }
                    }
                }
                service.Credentials = null; 
            }

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an enter all day event test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarAllDayEvent()
        {
            Tracing.TraceMsg("Entering CalendarAllDayEvent");

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

            int iCount; 

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query) as EventFeed;

                iCount = calFeed.Entries.Count; 

                String strTitle = "Dinner time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    EventEntry entry  = ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = strTitle;

                    entry.Times[0].AllDay = true; 

                    EventEntry newEntry = (EventEntry) calFeed.Insert(entry); 
                    iCount++; 
                    Tracing.TraceMsg("Created calendar entry");


                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 

                    EventFeed newFeed = service.Query(singleQuery) as EventFeed;

                    EventEntry sameGuy = newFeed.Entries[0] as EventEntry; 

                    sameGuy.Content.Content = "Updated again..."; 
                    sameGuy.Times[0].StartTime = DateTime.Now; 
                    sameGuy.Update();

                    
                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 

                }

                calFeed = service.Query(query) as EventFeed;

                Assert.AreEqual(iCount, calFeed.Entries.Count, "Feed should have one more entry, it has: " + calFeed.Entries.Count); 

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (EventEntry entry in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            Assert.IsTrue(entry.Times[0].AllDay, "Entry should be an all day event"); 
                        }
                    }
                }
                service.Credentials = null; 
            }

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>test for composite mode</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarCompositeTest()
        {
            Tracing.TraceMsg("Entering CalendarCompositeTest");

            // first run the RecurranceTest to create a recurring event
            this.CalendarRecurranceTest(); 

            // now get the feedService

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCompositeUri); 
                EventFeed calFeed = service.Query(query) as EventFeed;
                Assert.IsTrue(calFeed!=null, "that's wrong, there should be a feed object" + calFeed); 
                service.Credentials = null; 
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Test to check creating/updating/deleting a secondary calendar.
        /// </summary>
        [Test] public void CalendarOwnCalendarsTest()
        {
            Tracing.TraceMsg("Enterting CalendarOwnCalendarsTest");

            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultOwnCalendarsUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory;

                CalendarEntry newCalendar = new CalendarEntry();
                newCalendar.Title.Text = "new calendar" + Guid.NewGuid().ToString();
                newCalendar.Summary.Text = "some unique summary" + Guid.NewGuid().ToString();
                newCalendar.TimeZone = "America/Los_Angeles";
                newCalendar.Hidden = false;
                newCalendar.Selected = true;
                newCalendar.Color = "#2952A3";
                newCalendar.Location = new Where("", "", "Test City");

                Uri postUri = new Uri(this.defaultOwnCalendarsUri);
                CalendarEntry createdCalendar = (CalendarEntry) service.Insert(postUri, newCalendar);

                Assert.IsNotNull(createdCalendar, "created calendar should be returned.");

                Assert.AreEqual(newCalendar.Title.Text, createdCalendar.Title.Text, "Titles should be equal");
                Assert.AreEqual(newCalendar.Summary.Text, createdCalendar.Summary.Text, "Summaries should be equal");
                Assert.AreEqual(newCalendar.TimeZone, createdCalendar.TimeZone, "Timezone should be equal");
                Assert.AreEqual(newCalendar.Hidden, createdCalendar.Hidden, "Hidden property should be equal");
                Assert.AreEqual(newCalendar.Color, createdCalendar.Color, "Color property should be equal");
                Assert.AreEqual(newCalendar.Location.ValueString, createdCalendar.Location.ValueString, "Where should be equal");

                createdCalendar.Title.Text = "renamed calendar" + Guid.NewGuid().ToString();
                createdCalendar.Hidden = true;
                CalendarEntry updatedCalendar = (CalendarEntry) createdCalendar.Update();

                Assert.AreEqual(createdCalendar.Title.Text, updatedCalendar.Title.Text, "entry should have been updated");

                updatedCalendar.Delete();

                CalendarQuery query = new CalendarQuery();
                query.Uri = postUri;

                CalendarFeed calendarList = service.Query(query);

                foreach (CalendarEntry entry in calendarList.Entries)
                {
                    Assert.IsTrue(entry.Title.Text != updatedCalendar.Title.Text, "Calendar should have been removed");
                }


                service.Credentials = null;
            }

        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an enter all day event test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Ignore ("Currently broken on the server")]
        [Test] public void CalendarCommentTest()
        {
            Tracing.TraceMsg("Entering CalendarCommentTest");

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(this.ApplicationName);

            int iCount; 

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query) as EventFeed;

                iCount = calFeed.Entries.Count; 

                String strTitle = "Comment Test" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // insert a new entry
                    EventEntry entry  = ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = strTitle;

                    entry.Times[0].AllDay = true; 

                    calFeed.Insert(entry); 
                    iCount++; 
                    Tracing.TraceMsg("Created calendar entry");
                }

                calFeed = service.Query(query) as EventFeed;

                Assert.AreEqual(iCount, calFeed.Entries.Count, "Feed should have one more entry, it has: " + calFeed.Entries.Count); 

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (EventEntry entry in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            // get the comment feed
                            Uri commentFeedUri = new Uri(entry.Comments.FeedLink.Href); 

                            // now we use an AtomFeed to post there
                            Service feedService = new Service("cl", "UnitTests"); 
                            feedService.Credentials = new GDataCredentials(this.userName, this.passWord);
                            query.Uri = commentFeedUri; 

                            AtomFeed commentFeed = feedService.Query(query); 
                            AtomEntry newEntry = ObjectModelHelper.CreateAtomEntry(1); 
                            Tracing.TraceMsg("trying to insert a comment"); 
                            try
                            {
                                commentFeed.Insert(newEntry);
                            } catch (GDataRequestException e )
                            {
                                Console.WriteLine(e.ResponseString); 
                                Tracing.TraceMsg(e.ResponseString); 
                            }
                            
                        }
                    }
                }
                service.Credentials = null; 
            }

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a stress test against the calendar</summary> 
        //////////////////////////////////////////////////////////////////////
        [Ignore ("Normally not required to run, and takes pretty long")]
        [Test] public void CalendarStressTest()
        {
            Tracing.TraceMsg("Entering CalendarStressTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service();

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                AtomFeed calFeed = service.Query(query);
                if (calFeed != null)
                {

                    for (int i = 0; i<127; i++)
                    {
                        AtomEntry entry = ObjectModelHelper.CreateAtomEntry(1); 
                        entry.Title.Text = "Entry number: " + i; 
                        calFeed.Insert(entry); 
                    }
                    
                }

                calFeed = service.Query(query);

                int iCount = 0; 

                while (calFeed != null && calFeed.Entries.Count > 0)
                {
                    iCount += calFeed.Entries.Count; 
                    // just query the same query again.
                    if (calFeed.NextChunk != null)
                    {
                        query.Uri = new Uri(calFeed.NextChunk); 
                        calFeed = service.Query(query);
                    }
                    else 
                    {
                        calFeed = null; 
                    }
                    
                }


                Assert.AreEqual(127, iCount,  "Feed should have 127 entries, it has: " + iCount); 

                service.Credentials = null; 

            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>tests the sendNotification property against the calendar</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarNotificationTest()
        {
            Tracing.TraceMsg("Entering CalendarNotificationTest");

            FeedQuery query = new FeedQuery();

            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);

                EventFeed calFeed = service.Query(query) as EventFeed;

                string guid = Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    EventEntry entry = ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = guid; 
                    entry.Notifications = true; 
                    calFeed.Insert(entry); 
                }

                calFeed = service.Query(query) as EventFeed;

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    EventEntry entry = calFeed.Entries[0] as EventEntry;

                    Assert.AreEqual(entry.Title.Text, guid, "Expected the same entry");
                    // Assert.IsTrue(entry.Notifications, "Expected the sendNotify to be true" + entry.Notifications.ToString()); 
                }

                service.Credentials = null; 

                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        
        //////////////////////////////////////////////////////////////////////
        /// <summary>tests the extended property against the calendar</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarExtendedPropertyTest()
        {
            Tracing.TraceMsg("Entering CalendarExtendedPropertyTest");

            FeedQuery query = new FeedQuery();

            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);

                EventFeed calFeed = service.Query(query) as EventFeed;

                string guid = Guid.NewGuid().ToString(); 

                ExtendedProperty prop; 
                EventEntry entry;

                if (calFeed != null)
                {
                    entry = ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = guid; 

                    prop = new ExtendedProperty(); 
                    prop.Name = "http://frank.schemas/2005#prop"; 
                    prop.Value = "Mantek"; 

                    entry.ExtensionElements.Add(prop); 

                    calFeed.Insert(entry); 
                }

                calFeed = service.Query(query) as EventFeed;
                prop = null;
                entry = null;

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    entry = calFeed.Entries[0] as EventEntry;

                    Assert.AreEqual(entry.Title.Text, guid, "Expected the same entry");

                    foreach (Object o in entry.ExtensionElements )
                    {
                        ExtendedProperty p = o as ExtendedProperty; 
                        if (p != null)
                        {
                            Tracing.TraceMsg("Found one extended property"); 
                            Assert.AreEqual(p.Name, "http://frank.schemas/2005#prop", "Expected the same entry");
                            Assert.AreEqual(p.Value, "Mantek", "Expected the same entry");
                            prop = p; 
                        }
                    }
                }

                Assert.IsTrue(prop != null, "prop should not be null"); 

                // now delete the prop again
                // BUGBUG: currently you can not delete extended properties in the calendar
                /*
                if (entry != null)
                {
                    entry.ExtensionElements.Remove(prop);
                    prop = null;
                    EventEntry newEntry = entry.Update() as EventEntry; 
                    foreach (Object o in newEntry.ExtensionElements )
                    {
                        ExtendedProperty p = o as ExtendedProperty; 
                        if (p != null)
                        {
                            Tracing.TraceMsg("Found one extended property"); 
                            prop = p; 
                            break;
                        }
                    }
                    Assert.IsTrue(prop == null, "prop should be gone now");
                }
                */
                // get rid of the entry
                if (entry != null)
                    entry.Delete();
                
                service.Credentials = null; 

            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>tests that a default reminder get's created if none is set</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarDefaultReminderTest()
        {
            Tracing.TraceMsg("Entering CalendarDefaultReminderTest");

            FeedQuery query = new FeedQuery();

            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);

                EventFeed calFeed = service.Query(query) as EventFeed;

                EventEntry entry  =  ObjectModelHelper.CreateEventEntry(1); 
                entry.Title.Text = "New event with default reminder" + Guid.NewGuid().ToString();  

                entry.Reminder = new Reminder();
                entry.Reminder.Method = Reminder.ReminderMethod.unspecified;

                EventEntry newEntry = calFeed.Insert(entry) as EventEntry;
                Reminder reminder = newEntry.Reminder;
                Assert.IsTrue(reminder != null, "reminder should not be null - this only works if the calendar HAS default remidners set"); 
                Assert.IsTrue(reminder.Method != Reminder.ReminderMethod.unspecified, "reminder should not be unspecified - this only works if the calendar HAS default remidners set"); 

                service.Credentials = null; 
                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////
       
        /// 
        /// tests the reminder object
        ///
        [Test] public void ReminderTest()
        {
            String xml = "<reminder xmlns=\"http://schemas.google.com/g/2005\" minutes=\"0\" method=\"email\"/>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode reminderNode = doc.FirstChild;
            Reminder f = new Reminder();
            Reminder r; 
            r = f.CreateInstance(reminderNode, new AtomFeedParser()) as Reminder;

            Assert.IsTrue(r.Method == Reminder.ReminderMethod.email);
            Assert.IsTrue(r.Minutes == 0);


            xml = "<reminder xmlns=\"http://schemas.google.com/g/2005\" minutes=\"5\" method=\"sms\"/>";
            doc = new XmlDocument();
            doc.LoadXml(xml);
            reminderNode = doc.FirstChild;
            r = new Reminder();
            r = f.CreateInstance(reminderNode, new AtomFeedParser()) as Reminder;

            Assert.IsTrue(r.Method == Reminder.ReminderMethod.sms);
            Assert.IsTrue(r.Minutes == 5);
        }
               

        //////////////////////////////////////////////////////////////////////
        /// <summary>tests the original event </summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarOriginalEventTest()
        {
            Tracing.TraceMsg("Entering CalendarOriginalEventTest");

            FeedQuery query = new FeedQuery();

            CalendarService service = new CalendarService(this.ApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);

                EventFeed calFeed = service.Query(query) as EventFeed;

                string recur  = 
                    "DTSTART;TZID=America/Los_Angeles:20060314T060000\n" +
                    "DURATION:PT3600S\n" + 
                    "RRULE:FREQ=DAILY;UNTIL=20060321T220000Z\n" +
                    "BEGIN:VTIMEZONE\n" + 
                    "TZID:America/Los_Angeles\n" +
                    "X-LIC-LOCATION:America/Los_Angeles\n" +
                    "BEGIN:STANDARD\n" +
                    "TZOFFSETFROM:-0700\n" +
                    "TZOFFSETTO:-0800\n" +
                    "TZNAME:PST\n" +
                    "DTSTART:19671029T020000\n" +
                    "RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=-1SU\n" +
                    "END:STANDARD\n" +
                    "BEGIN:DAYLIGHT\n" +
                    "TZOFFSETFROM:-0800\n" +
                    "TZOFFSETTO:-0700\n" +
                    "TZNAME:PDT\n" +
                    "DTSTART:19870405T020000\n" +
                    "RRULE:FREQ=YEARLY;BYMONTH=4;BYDAY=1SU\n" +
                    "END:DAYLIGHT\n" +
                    "END:VTIMEZONE\n"; 

                EventEntry entry  =  ObjectModelHelper.CreateEventEntry(1); 
                entry.Title.Text = "New recurring event" + Guid.NewGuid().ToString();  

                // get rid of the when entry
                entry.Times.Clear(); 

                entry.Recurrence = new Recurrence();
                entry.Recurrence.Value = recur; 

                EventEntry recEntry = calFeed.Insert(entry) as EventEntry;

                entry = ObjectModelHelper.CreateEventEntry(1); 
                entry.Title.Text = "whateverfancy"; 

                OriginalEvent originalEvent = new OriginalEvent();
                When start = new When();
                start.StartTime = new DateTime(2006, 03, 14, 15, 0,0); 
                originalEvent.OriginalStartTime = start;
                originalEvent.Href = recEntry.SelfUri.ToString();
                originalEvent.IdOriginal = recEntry.EventId;
                entry.OriginalEvent = originalEvent;
                entry.Times.Add(new When(new DateTime(2006, 03, 14, 9,0,0),
                                         new DateTime(2006, 03, 14, 10,0,0)));
                calFeed.Insert(entry);

                service.Credentials = null; 
                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////
    } /////////////////////////////////////////////////////////////////////////////


}







