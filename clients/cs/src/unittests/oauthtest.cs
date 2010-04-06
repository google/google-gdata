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
using Google.Contacts;
using Google.Documents;
using System.Collections.Generic;




namespace Google.GData.Client.LiveTests
{
    [TestFixture]
    [Category("LiveTest")]
    public class OAuthTestSuite : BaseLiveTestClass
    {
        protected string oAuthConsumerKey;
        protected string oAuthConsumerSecrect;
        protected string oAuthDomain;
        protected string oAuthUser;


        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public OAuthTestSuite()
        {
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>the setup method</summary> 
        //////////////////////////////////////////////////////////////////////
        [SetUp]
        public override void InitTest()
        {
            Tracing.TraceCall();
            base.InitTest();
        }
        /////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////
        /// <summary>the end it all method</summary> 
        //////////////////////////////////////////////////////////////////////
        [TearDown]
        public override void EndTest()
        {
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

            if (unitTestConfiguration.Contains("OAUTHCONSUMERKEY") == true)
            {
                this.oAuthConsumerKey = (string)unitTestConfiguration["OAUTHCONSUMERKEY"];
             }
            if (unitTestConfiguration.Contains("OAUTHCONSUMERSECRET") == true)
            {
                this.oAuthConsumerSecrect = (string)unitTestConfiguration["OAUTHCONSUMERSECRET"];
            }
            if (unitTestConfiguration.Contains("OAUTHDOMAIN") == true)
            {
                this.oAuthDomain = (string)unitTestConfiguration["OAUTHDOMAIN"];
            }
            if (unitTestConfiguration.Contains("OAUTHUSER") == true)
            {
                this.oAuthUser = (string)unitTestConfiguration["OAUTHUSER"];
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test with 2 legged oauth</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void OAuth2LeggedAuthenticationTest()
        {
            Tracing.TraceMsg("Entering OAuth2LeggedAuthenticationTest");

            CalendarService service = new CalendarService("OAuthTestcode");

            GOAuthRequestFactory requestFactory = new GOAuthRequestFactory("cl", "OAuthTestcode");
            requestFactory.ConsumerKey = this.oAuthConsumerKey;
            requestFactory.ConsumerSecret = this.oAuthConsumerSecrect;
            service.RequestFactory = requestFactory;

            CalendarEntry calendar = new CalendarEntry();
            calendar.Title.Text = "Test OAuth";

            OAuthUri postUri = new OAuthUri("http://www.google.com/calendar/feeds/default/owncalendars/full", this.oAuthUser,
                this.oAuthDomain);
            CalendarEntry createdCalendar = (CalendarEntry)service.Insert(postUri, calendar);


        }
        /////////////////////////////////////////////////////////////////////////////

        [Test]
        public void OAuth2LeggedContactsTest()
        {
            Tracing.TraceMsg("Entering OAuth2LeggedContactsTest");
  
        
            RequestSettings rs = new RequestSettings(this.ApplicationName, this.oAuthConsumerKey, this.oAuthConsumerSecrect,
                                                     this.oAuthUser, this.oAuthDomain);
     
            ContactsRequest cr = new ContactsRequest(rs);

            Feed<Contact> f = cr.GetContacts();

            // modify one
            foreach (Contact c in f.Entries)
            {
                c.Title = "new title";
                cr.Update(c);
                break;
            }


            Contact entry = new Contact();
            entry.AtomEntry = ObjectModelHelper.CreateContactEntry(1);
            entry.PrimaryEmail.Address = "joe@doe.com";
            Contact e = cr.Insert(f, entry);


            cr.Delete(e);
            
        }
        /////////////////////////////////////////////////////////////////////////////

        [Test]
        public void OAuth2LeggedDocumentsTest()
        {
            Tracing.TraceMsg("Entering OAuth2LeggedDocumentsTest");


            RequestSettings rs = new RequestSettings(this.ApplicationName, this.oAuthConsumerKey, this.oAuthConsumerSecrect,
                                                     this.oAuthUser, this.oAuthDomain);

            DocumentsRequest dr = new DocumentsRequest(rs);

            Feed<Document> f = dr.GetDocuments();

            // modify one
            foreach (Document d in f.Entries)
            {
                string s = d.AtomEntry.EditUri.ToString();              
                d.AtomEntry.EditUri = new AtomUri(s.Replace("@", "%40"));
               
                dr.Update(d);
                AclQuery q = new AclQuery();
                q.Uri = d.AccessControlList;
                Feed<Google.AccessControl.Acl> facl = dr.Get<Google.AccessControl.Acl>(q);

                foreach (Google.AccessControl.Acl a in facl.Entries)
                {
                    s = a.AclEntry.EditUri.ToString();
                    a.AclEntry.EditUri = new AtomUri(s.Replace("@", "%40"));
                    dr.Update(a);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test using OAUTH, inserts lot's of new contacts
        /// and deletes them again</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void OAuth2LeggedModelContactsBatchInsertTest()
        {
            const int numberOfInserts = 37;
            Tracing.TraceMsg("Entering OAuth2LeggedModelContactsBatchInsertTest");


            RequestSettings rs = new RequestSettings(this.ApplicationName, this.oAuthConsumerKey, this.oAuthConsumerSecrect,
                                               this.oAuthUser, this.oAuthDomain);

            ContactsTestSuite.DeleteAllContacts(rs);
     
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
                            Assert.AreEqual(e.PrimaryPhonenumber.Value, p.Value, "They should be identical");
                        }
                    }
                }

                Assert.IsTrue(iVer == 0, "The new entries should all be part of the feed now, " + iVer + " left over");
            }

            // now delete them again
            ContactsTestSuite.DeleteList(inserted, cr, new Uri(f.AtomFeed.Batch));

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


      
    }
}





