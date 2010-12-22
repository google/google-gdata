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
        protected string oAuthConsumerSecret;
        protected string oAuthDomain;
        protected string oAuthUser;
        protected string oAuthToken;
        protected string oAuthTokenSecret;


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

            if (unitTestConfiguration.Contains("OAUTHCONSUMERKEY"))
            {
                this.oAuthConsumerKey = (string)unitTestConfiguration["OAUTHCONSUMERKEY"];
             }
            if (unitTestConfiguration.Contains("OAUTHCONSUMERSECRET"))
            {
                this.oAuthConsumerSecret = (string)unitTestConfiguration["OAUTHCONSUMERSECRET"];
            }
            if (unitTestConfiguration.Contains("OAUTHDOMAIN"))
            {
                this.oAuthDomain = (string)unitTestConfiguration["OAUTHDOMAIN"];
            }
            if (unitTestConfiguration.Contains("OAUTHUSER"))
            {
                this.oAuthUser = (string)unitTestConfiguration["OAUTHUSER"];
            }
            if (unitTestConfiguration.Contains("OAUTHTOKEN"))
            {
                this.oAuthToken = (string)unitTestConfiguration["OAUTHTOKEN"];
            }
            if (unitTestConfiguration.Contains("OAUTHTOKENSECRET"))
            {
                this.oAuthTokenSecret = (string)unitTestConfiguration["OAUTHTOKENSECRET"];
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Verifies the signature generation</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void OAuthBaseSignatureTest()
        {
            Tracing.TraceMsg("Entering OAuthBaseSignatureTest");

            Uri uri = new Uri("http://photos.example.net/photos?file=vacation.jpg&size=original");

            string sig = OAuthBase.GenerateSignatureBase(uri,
                                            "dpf43f3p2l4k3l03",
                                            "nnch734d00sl2jdk",
                                            null,
                                            "GET",
                                            "1191242096",
                                            "kllo9940pd9333jh",
                                            "HMAC-SHA1");
            Assert.AreEqual("GET&http%3A%2F%2Fphotos.example.net%2Fphotos&file%3Dvacation.jpg%26oauth_consumer_key%3Ddpf43f3p2l4k3l03%26oauth_nonce%3Dkllo9940pd9333jh%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D1191242096%26oauth_token%3Dnnch734d00sl2jdk%26oauth_version%3D1.0%26size%3Doriginal",
                             sig);


            uri = new Uri("https://www.google.com/calendar/feeds/default/owncalendars/full");

            sig = OAuthBase.GenerateSignatureBase(uri,
                                                    this.oAuthConsumerKey,
                                                    "1/ZTmkWjywYI5qJhcuwjHi8Xx9he7Gu7FXuX9OXXpM_Ac",
                                                    "pHqmVqazj2mdG9EVyW1OzVix",
                                                    "GET",
                                                    "1274286639",
                                                    "bdcc500c4dbdb2fd8d6bf3e3346007c7",
                                                    "HMAC-SHA1");

            Assert.AreEqual("GET&https%3A%2F%2Fwww.google.com%2Fcalendar%2Ffeeds%2Fdefault%2Fowncalendars%2Ffull&oauth_consumer_key%3Dmantek.org%26oauth_nonce%3Dbdcc500c4dbdb2fd8d6bf3e3346007c7%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D1274286639%26oauth_token%3D1%252FZTmkWjywYI5qJhcuwjHi8Xx9he7Gu7FXuX9OXXpM_Ac%26oauth_version%3D1.0",
                                sig);


            uri = new Uri("http://example.com/request?b5=%3D%253D&a3=a&c%40=&a2=r%20b&c2&a3=2+q");
            sig = OAuthBase.GenerateSignatureBase(uri,
                                                    "9djdj82h48djs9d2",
                                                    "kkk9d7dh3k39sjv7",
                                                    null,
                                                    "GET",
                                                    "137131201",
                                                    "7d8f3e4a",
                                                    "HMAC-SHA1");

            Assert.AreEqual("GET&http%3A%2F%2Fexample.com%2Frequest&a2%3Dr%2520b%26a3%3D2%2520q%26a3%3Da%26b5%3D%253D%25253D%26c%2540%3D%26c2%3D%26oauth_consumer_key%3D9djdj82h48djs9d2%26oauth_nonce%3D7d8f3e4a%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D137131201%26oauth_token%3Dkkk9d7dh3k39sjv7%26oauth_version%3D1.0", sig);



        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Verifies the signature generation</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void OAuthBaseGenerateOAuthSignatureTest()
        {
            Tracing.TraceMsg("Entering OAuthBaseGenerateOAuthSignatureTest");

            string sig;

            sig = OAuthBase.GenerateOAuthSignatureEncoded("djr9rjt0jd78jf88", "");
            Assert.AreEqual("djr9rjt0jd78jf88%26", sig);

            sig = OAuthBase.GenerateOAuthSignatureEncoded("djr9rjt0jd78jf88", "jjd99$tj88uiths3");
            Assert.AreEqual("djr9rjt0jd78jf88%26jjd99%2524tj88uiths3", sig);


            sig = OAuthBase.GenerateOAuthSignatureEncoded("djr9rjt0jd78jf88", "jjd999tj88uiths3");
            Assert.AreEqual("djr9rjt0jd78jf88%26jjd999tj88uiths3", sig);
        }
        /////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        /// <summary>Verifies the signed signature generation</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void OAuthBaseSigningTest()
        {
            Tracing.TraceMsg("Entering OAuthBaseSignatureTest");


            Uri uri = new Uri("http://photos.example.net/photos?file=vacation.jpg&size=original");

            string sig = OAuthBase.GenerateSignature(uri,
                                            "dpf43f3p2l4k3l03",
                                            "kd94hf93k423kf44",
                                            "nnch734d00sl2jdk",
                                            "pfkkdhi9sl3r4s00",
                                            "GET",
                                            "1191242096",
                                            "kllo9940pd9333jh",
                                            OAuthBase.SignatureTypes.HMACSHA1);

            Assert.AreEqual("tR3+Ty81lMeYAr/Fid0kMTYa/WM=",
                             sig);


            uri = new Uri("https://www.google.com/calendar/feeds/default/owncalendars/full");

            sig = OAuthBase.GenerateSignatureBase(uri,
                                            this.oAuthConsumerKey,
                                             "1/NOQv9YTpvvzo8aFC9WpDuRxDl58cSF7JJaDQV1LnXgs",
                                            "MS3p04xWG7MkEyUwk91D1xEU",
                                            "GET",
                                            "1274791118",
                                            "f425726b32231fb9a363957f44e00227",
                                            "HMAC-SHA1");


            Assert.AreEqual("GET&https%3A%2F%2Fwww.google.com%2Fcalendar%2Ffeeds%2Fdefault%2Fowncalendars%2Ffull&oauth_consumer_key%3Dmantek.org%26oauth_nonce%3Df425726b32231fb9a363957f44e00227%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D1274791118%26oauth_token%3D1%252FNOQv9YTpvvzo8aFC9WpDuRxDl58cSF7JJaDQV1LnXgs%26oauth_version%3D1.0",
                sig);

            sig = OAuthBase.GenerateSignature(uri,
                                this.oAuthConsumerKey,
                                this.oAuthConsumerSecret,
                                "1/NOQv9YTpvvzo8aFC9WpDuRxDl58cSF7JJaDQV1LnXgs",
                                "MS3p04xWG7MkEyUwk91D1xEU",
                                "GET",
                                "1274791118",
                                "f425726b32231fb9a363957f44e00227",
                                OAuthBase.SignatureTypes.HMACSHA1);


            Assert.AreEqual("NrqbCmBZ4GePYLeAg6m4WHjvD1w=", sig);

            uri = new Uri("http://www.google.com/calendar/feeds/default/owncalendars/full?xoauth_requestor_id=admin%40mantek.org");

            sig = OAuthBase.GenerateSignature(uri,
                           this.oAuthConsumerKey,
                           this.oAuthConsumerSecret,
                           null,
                           null,
                           "GET",
                           "1274791118",
                           "f425726b32231fb9a363957f44e00227",
                           OAuthBase.SignatureTypes.HMACSHA1);

            Assert.AreEqual("f4mWCiT6IR6Ybq7fq9zc7860xE4=", sig);

            sig = OAuthBase.GenerateSignature(uri,
                           this.oAuthConsumerKey,
                           this.oAuthConsumerSecret,
                           null,
                           null,
                           "GET",
                           "1274809835",
                           "c198e3abc8bfb1b11ea9a79987e80252",
                           OAuthBase.SignatureTypes.HMACSHA1);

            Assert.AreEqual("omsn9/am4uIQZdDxmuWzeEap3hE=", sig);


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
            requestFactory.ConsumerSecret = this.oAuthConsumerSecret;
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
  
        
            RequestSettings rs = new RequestSettings(this.ApplicationName, this.oAuthConsumerKey, this.oAuthConsumerSecret,
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


            RequestSettings rs = new RequestSettings(this.ApplicationName, this.oAuthConsumerKey, this.oAuthConsumerSecret,
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


            RequestSettings rs = new RequestSettings(this.ApplicationName, this.oAuthConsumerKey, this.oAuthConsumerSecret,
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



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test with 2 legged oauth</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void OAuth3LeggedAuthenticationTest()
        {
            Tracing.TraceMsg("Entering OAuth3LeggedAuthenticationTest");

            CalendarService service = new CalendarService("OAuthTestcode");

            GOAuthRequestFactory requestFactory = new GOAuthRequestFactory("cl", "OAuthTestcode");
            requestFactory.ConsumerKey = this.oAuthConsumerKey;
            requestFactory.ConsumerSecret = this.oAuthConsumerSecret;
            requestFactory.Token = this.oAuthToken;
            requestFactory.TokenSecret = this.oAuthTokenSecret;
            service.RequestFactory = requestFactory;

            CalendarEntry calendar = new CalendarEntry();
            calendar.Title.Text = "Test OAuth";

            Uri postUri = new Uri("https://www.google.com/calendar/feeds/default/owncalendars/full");
            CalendarEntry createdCalendar = (CalendarEntry)service.Insert(postUri, calendar);

            // delete the guy again

            createdCalendar.Delete();


        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test with 2 legged oauth</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        public void OAuth3LeggedModelAuthenticationTest()
        {

            Tracing.TraceMsg("Entering OAuth3LeggedModelAuthenticationTest");


            RequestSettings rs = new RequestSettings(this.ApplicationName, this.oAuthConsumerKey, this.oAuthConsumerSecret,
                                                     this.oAuthToken, this.oAuthTokenSecret, null, null);

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


      
    }
}





