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
using Google.GData.Extensions;
using Google.GData.Calendar;




namespace Google.GData.Client.UnitTests
{
    [TestFixture] 
    public class CalendarTestSuite : BaseTestClass
    {

        /// <summary>holds the username to use</summary>
        protected string userName;
        /// <summary>holds the password to use</summary>
        protected string passWord;

        /// <summary>holds the default authhandler</summary> 
        protected string strAuthHandler; 

        /// <summary>
        ///  test Uri for google calendarURI
        /// </summary>
        protected string defaultCalendarUri; 


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
            base.InitTest(); 
            GDataGAuthRequestFactory authFactory = this.factory as GDataGAuthRequestFactory; 
            if (authFactory != null)
            {
                authFactory.Handler = this.strAuthHandler; 
            }

            CalendarCleanup(); 
        }
        /////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////
        /// <summary>the end it all method</summary> 
        //////////////////////////////////////////////////////////////////////
        [TearDown] public override void EndTest()
        {
            CalendarCleanup(); 
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

            IDictionary unitTestDictionary = (IDictionary) ConfigurationSettings.GetConfig("unitTestSection");

            if (unitTestDictionary != null)
            {
                if (unitTestDictionary["authHandler"] != null)
                {
                    this.strAuthHandler = (string) unitTestDictionary["authHandler"];
                    Tracing.TraceInfo("Read authHandler value: " + this.strAuthHandler); 
                }
                if (unitTestDictionary["calendarURI"] != null)
                {
                    this.defaultCalendarUri = (string) unitTestDictionary["calendarURI"];
                    Tracing.TraceInfo("Read calendarURI value: " + this.defaultCalendarUri); 
                }
                if (unitTestDictionary["userName"] != null)
                {
                    this.userName = (string) unitTestDictionary["userName"];
                    Tracing.TraceInfo("Read userName value: " + this.userName); 
                }
                if (unitTestDictionary["passWord"] != null)
                {
                    this.passWord = (string) unitTestDictionary["passWord"];
                    Tracing.TraceInfo("Read passWord value: " + this.passWord); 
                }
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
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
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

                factory.MethodOverride = false;

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
            CalendarService service = new CalendarService(this.strApplicationName);

            int iCount; 

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query);

                if (calFeed.TimeZone != null)
                {
                    Tracing.TraceMsg(calFeed.TimeZone.Value); 
                }

                ObjectModelHelper.DumpAtomObject(calFeed,CreateDumpFileName("AuthenticationTest")); 
                iCount = calFeed.Entries.Count; 

                String strTitle = "Dinner & time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    EventEntry entry  = (EventEntry) ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = strTitle;

                    EventEntry newEntry = (EventEntry) calFeed.Insert(entry); 

                    Assert.AreEqual(entry.Reminder.Minutes, entry.Reminder.Minutes, "Reminder time should be identical"); 
                    iCount++; 
                    Tracing.TraceMsg("Created calendar entry");

                    newEntry.Content.Content = "Updated..";
                    newEntry.Update();


                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 

                    EventFeed newFeed = service.Query(singleQuery);

                    EventEntry sameGuy = newFeed.Entries[0] as EventEntry; 

                    sameGuy.Content.Content = "Updated again..."; 
                    When x = sameGuy.Times[0]; 
                    sameGuy.Times.Clear();
                    x.StartTime = DateTime.Now; 
                    sameGuy.Times.Add(x); 
                    sameGuy.Update();

                    
                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 

                }

                calFeed = service.Query(query);

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

                factory.MethodOverride = false;

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
            CalendarService service = new CalendarService(this.strApplicationName);

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query);

                string recur = 
                    "DTSTART;TZID=America/Los_Angeles:20070416T093000\n" +
                    "DURATION:PT3600S\n" +
                    "RRULE:FREQ=DAILY;UNTIL=20070828T163000Z\n" +
                    "BEGIN:VTIMEZONE\n" +
                    "TZID:America/Los_Angeles\n" +
                    "X-LIC-LOCATION:America/Los_Angeles\n" +
                    "BEGIN:STANDARD\n" +
                    "TZOFFSETFROM:-0700\n" +
                    "TZOFFSETTO:-0800\n" +
                    "TZNAME:PST\n" +
                    "DTSTART:19701025T020000\n" +
                    "RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=-1SU\n" +
                    "END:STANDARD\n" +
                    "BEGIN:DAYLIGHT\n" +
                    "TZOFFSETFROM:-0800\n" +
                    "TZOFFSETTO:-0700\n" +
                    "TZNAME:PDT\n" +
                    "DTSTART:19700405T020000\n" +
                    "RRULE:FREQ=YEARLY;BYMONTH=4;BYDAY=1SU\n" +
                    "END:DAYLIGHT\n" +
                    "END:VTIMEZONE\n";


                EventEntry entry  = (EventEntry) ObjectModelHelper.CreateEventEntry(1); 
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

                factory.MethodOverride = false;

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
            CalendarService service = new CalendarService(this.strApplicationName);

            int iCount; 

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                // factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query);

                ObjectModelHelper.DumpAtomObject(calFeed,CreateDumpFileName("AuthenticationTest")); 
                iCount = calFeed.Entries.Count; 

                String strTitle = "Dinner time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    EventEntry entry  = (EventEntry) ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = strTitle;

                    entry.Times[0].AllDay = true; 

                    EventEntry newEntry = (EventEntry) calFeed.Insert(entry); 
                    iCount++; 
                    Tracing.TraceMsg("Created calendar entry");


                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 

                    EventFeed newFeed = service.Query(singleQuery);

                    EventEntry sameGuy = newFeed.Entries[0] as EventEntry; 

                    sameGuy.Content.Content = "Updated again..."; 
                    sameGuy.Times[0].StartTime = DateTime.Now; 
                    sameGuy.Update();

                    
                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 

                }

                calFeed = service.Query(query);

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
                factory.MethodOverride = false;
            }

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an enter all day event test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarCommentTest()
        {
            Tracing.TraceMsg("Entering CalendarCommentTest");

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(this.strApplicationName);

            int iCount; 

            if (this.defaultCalendarUri != null)
            {
                NetworkCredential nc = null; 

                if (this.userName != null)
                {
                    nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                // factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                EventFeed calFeed = service.Query(query);

                iCount = calFeed.Entries.Count; 

                String strTitle = "Comment Test" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // insert a new entry
                    EventEntry entry  = (EventEntry) ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = strTitle;

                    entry.Times[0].AllDay = true; 

                    calFeed.Insert(entry); 
                    iCount++; 
                    Tracing.TraceMsg("Created calendar entry");
                }

                calFeed = service.Query(query);

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
                            feedService.Credentials = nc;

                            query.Uri = commentFeedUri; 

                            AtomFeed commentFeed = feedService.Query(query); 
                            AtomEntry newEntry = ObjectModelHelper.CreateAtomEntry(1); 
                            Tracing.TraceMsg("trying to insert a comment"); 
                            commentFeed.Insert(newEntry);
                        }
                    }
                }
                service.Credentials = null; 
                factory.MethodOverride = false;
            }

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a stress test against the calendar</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CalendarStressTest()
        {
            Tracing.TraceMsg("Entering CalendarStressTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service();

            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                AtomFeed calFeed = service.Query(query);

                ObjectModelHelper.DumpAtomObject(calFeed,CreateDumpFileName("AuthenticationTest")); 

                if (calFeed != null)
                {
                    // get the first entry
                    AtomEntry entry = ObjectModelHelper.CreateAtomEntry(1); 

                    for (int i = 0; i<127; i++)
                    {
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
                    query.Uri = new Uri(calFeed.NextChunk); 
                    calFeed = service.Query(query);
                }


                Assert.AreEqual(127, iCount,  "Feed should have 500 entries, it has: " + calFeed.Entries.Count); 

                service.Credentials = null; 

                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>empty the calendar</summary> 
        //////////////////////////////////////////////////////////////////////
        public void CalendarCleanup()
        {
            Tracing.TraceMsg("Entering CalendarCleanup");

            FeedQuery query = new FeedQuery();
            Service service = new Service();


            if (this.defaultCalendarUri != null)
            {
                if (this.userName != null)
                {
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);
                AtomFeed calFeed = service.Query(query);

                int iCount=0; 

                while (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
					foreach (AtomEntry entry in calFeed.Entries)
					{
						entry.Delete(); 
                        iCount++; 
                        Tracing.TraceMsg("CalendarCleanup = deleting entry" + iCount);
					}
                    // just query the same query again.
                    calFeed = service.Query(query);
                }

                Tracing.Assert(calFeed.Entries.Count ==0, "Feed should be empty" ); 

                query.Uri = new Uri(this.defaultCalendarUri);
                calFeed = service.Query(query);

                Assert.AreEqual(0, calFeed.Entries.Count, "Feed should have no more entries, it has: " + calFeed.Entries.Count); 
                service.Credentials = null; 
                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////

    } /////////////////////////////////////////////////////////////////////////////
}




