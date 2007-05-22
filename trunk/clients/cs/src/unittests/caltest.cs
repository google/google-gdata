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
using Google.GData.AccessControl;




namespace Google.GData.Client.UnitTests
{
    [TestFixture] 
    [Category("GoogleCalendar")]
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
            base.InitTest(); 
            GDataGAuthRequestFactory authFactory = this.factory as GDataGAuthRequestFactory; 
            if (authFactory != null)
            {
                authFactory.Handler = this.strAuthHandler; 
            }

            FeedCleanup(this.defaultCalendarUri, this.userName, this.passWord);

        }
        /////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////
        /// <summary>the end it all method</summary> 
        //////////////////////////////////////////////////////////////////////
        [TearDown] public override void EndTest()
        {
            FeedCleanup(this.defaultCalendarUri, this.userName, this.passWord);
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

            if (unitTestConfiguration.Contains("authHandler") == true)
            {
                this.strAuthHandler = (string) unitTestConfiguration["authHandler"];
                Tracing.TraceInfo("Read authHandler value: " + this.strAuthHandler);
            }
            if (unitTestConfiguration.Contains("calendarURI") == true)
            {
                this.defaultCalendarUri = (string) unitTestConfiguration["calendarURI"];
                Tracing.TraceInfo("Read calendarURI value: " + this.defaultCalendarUri);
            }
            if (unitTestConfiguration.Contains("aclFeedURI") == true)
            {
                this.aclFeedUri = (string) unitTestConfiguration["aclFeedURI"];
                Tracing.TraceInfo("Read aclFeed value: " + this.aclFeedUri);
            }
            if (unitTestConfiguration.Contains("compositeURI") == true)
            {
                this.defaultCompositeUri = (string) unitTestConfiguration["compositeURI"];
                Tracing.TraceInfo("Read compositeURI value: " + this.defaultCompositeUri);
            }
            if (unitTestConfiguration.Contains("userName") == true)
            {
                this.userName = (string) unitTestConfiguration["userName"];
                Tracing.TraceInfo("Read userName value: " + this.userName);
            }
            if (unitTestConfiguration.Contains("passWord") == true)
            {
                this.passWord = (string) unitTestConfiguration["passWord"];
                Tracing.TraceInfo("Read passWord value: " + this.passWord);
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
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
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
            CalendarService service = new CalendarService(this.ApplicationName);

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

                    Assert.AreEqual(entry.Reminder.Minutes, entry.Reminder.Minutes, "Reminder time should be identical"); 
                    iCount++; 
                    Tracing.TraceMsg("Created calendar entry");

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

                factory.MethodOverride = false;

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
                EventFeed calFeed = service.Query(query) as EventFeed;
                iCount = calFeed.Entries.Count; 

                String strTitle = "Dinner & time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    EventEntry entry  = ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = strTitle;
                    entry.Reminder.Method = Reminder.ReminderMethod.email;
                    EventEntry newEntry = (EventEntry) calFeed.Insert(entry); 

                    Assert.AreEqual(entry.Reminder.Minutes, newEntry.Reminder.Minutes, "Reminder time should be identical"); 
                    Assert.AreEqual(entry.Reminder.Method, Reminder.ReminderMethod.email, "Reminder method should be identical"); 
                    iCount++; 
                    Tracing.TraceMsg("Created calendar entry");

                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 
                    EventFeed newFeed  = service.Query(query) as EventFeed;
                    EventEntry sameGuy = newFeed.Entries[0] as EventEntry; 

                    Assert.AreEqual(sameGuy.Reminder.Minutes, newEntry.Reminder.Minutes, "Reminder time should be identical"); 
                    Assert.AreEqual(sameGuy.Reminder.Method, Reminder.ReminderMethod.email, "Reminder method should be identical"); 
                }

                service.Credentials = null; 

                factory.MethodOverride = false;

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
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

               // service.NewFeed += new ServiceEventHandler(this.OnNewAclFeed); 

                query.Uri = new Uri(this.aclFeedUri);
                AclFeed aclFeed = service.Query(query);
                AclEntry newEntry = null;

                iCount = aclFeed.Entries.Count; 

                if (aclFeed != null)
                {
                    // create an entry
                    AclEntry entry = new AclEntry();
                    entry.Role = AclRole.ACL_CALENDAR_FREEBUSY;
                    AclScope scope = new AclScope();
                    scope.Value = "meohmy@test.com"; 
                    scope.Type = AclScope.SCOPE_DEFAULT;
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
                factory.MethodOverride = false;

            }

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>eventchaining. We catch this by the baseFeedParsers, which 
        /// would not do anything with the gathered data. We pass the event up
        /// to the user</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewAclFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Acl Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }
            e.Feed = new AclFeed(e.Uri, e.Service);
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
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
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

                    AtomLink link = new AtomLink("image/gif", "http://schemas.google.com/gCal/2005/webContent"); 

                    link.Title = "Test content"; 
                    link.HRef = "http://www.google.com/calendar/images/google-holiday.gif";

                    WebContent content = new WebContent();

                    content.Url = "http://www.google.com/logos/july4th06.gif";
                    content.Width = 270;
                    content.Height = 130; 

                    link.ExtensionElements.Add(content);
                    entry.Links.Add(link); 

                    EventEntry newEntry = (EventEntry) calFeed.Insert(entry); 

                    // check if the link came back
                    link = newEntry.Links.FindService("http://schemas.google.com/gCal/2005/webContent", "image/gif"); 
                    Assert.IsTrue(link != null, "the link did not come back for the webContent"); 
                    Tracing.TraceMsg("Created calendar entry");

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
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            Assert.AreEqual(ObjectModelHelper.DEFAULT_REMINDER_TIME, entry.Reminder.Minutes, "Reminder time should be identical"); 
                            // check if the link came back
                            AtomLink link = entry.Links.FindService("http://schemas.google.com/gCal/2005/webContent", "image/gif"); 
                            Assert.IsTrue(link != null, "the link did not come back for the webContent"); 

                        }
                    }
                }

                calFeed = service.Query(query) as EventFeed;

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (EventEntry entry in calFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            entry.Delete();
                            Tracing.TraceMsg("deleted entry");
                        }
                    }
                }

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
            CalendarService service = new CalendarService(this.ApplicationName);

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
                EventFeed calFeed = service.Query(query) as EventFeed;

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


                recur  = 
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
            CalendarService service = new CalendarService(this.ApplicationName);

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
                factory.MethodOverride = false;
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
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCompositeUri); 
                EventFeed calFeed = service.Query(query) as EventFeed;
                Assert.IsTrue(calFeed!=null, "that's wrong, there should be a feed object" + calFeed); 
                service.Credentials = null; 
                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////



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
                            feedService.Credentials = nc;

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

                factory.MethodOverride = false;
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
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
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
                    Assert.IsTrue(entry.Notifications, "Expected the sendNotify to be true" + entry.Notifications.ToString()); 
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
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultCalendarUri);

                EventFeed calFeed = service.Query(query) as EventFeed;

                string guid = Guid.NewGuid().ToString(); 

                ExtendedProperty prop; 


                if (calFeed != null)
                {
                    EventEntry entry = ObjectModelHelper.CreateEventEntry(1); 
                    entry.Title.Text = guid; 

                    prop = new ExtendedProperty(); 
                    prop.Name = "http://frank.schemas/2005#prop"; 
                    prop.Value = "Mantek"; 

                    entry.ExtensionElements.Add(prop); 

                    calFeed.Insert(entry); 
                }

                calFeed = service.Query(query) as EventFeed;

                prop = null;

                if (calFeed != null && calFeed.Entries.Count > 0)
                {
                    EventEntry entry = calFeed.Entries[0] as EventEntry;

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

                service.Credentials = null; 

                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////
       

    } /////////////////////////////////////////////////////////////////////////////
}




