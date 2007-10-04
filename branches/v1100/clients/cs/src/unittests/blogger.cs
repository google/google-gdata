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
using System.Web;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Client.UnitTests;
using Google.GData.Extensions;
using Google.GData.Calendar;


namespace Google.GData.Client.LiveTests
{
    [TestFixture]
    [Category("LiveTest")]
    public class BloggerTestSuite : BaseLiveTestClass
    {
        /// <summary>
        ///  test Uri for google calendarURI
        /// </summary>
        protected string bloggerURI; 


        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public BloggerTestSuite()
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
            FeedCleanup(this.bloggerURI, this.userName, this.passWord);
        }
        /////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////
        /// <summary>the end it all method</summary> 
        //////////////////////////////////////////////////////////////////////
        [TearDown] public override void EndTest()
        {
            Tracing.ExitTracing();
            FeedCleanup(this.bloggerURI, this.userName, this.passWord);
     }
        /////////////////////////////////////////////////////////////////////////////

      

        //////////////////////////////////////////////////////////////////////
        /// <summary>private void ReadConfigFile()</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("bloggerURI") == true)
            {
                this.bloggerURI = (string) unitTestConfiguration["bloggerURI"];
                Tracing.TraceInfo("Read bloggerURI value: " + this.bloggerURI);
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        public override string ServiceName
        {
            get {
                return "blogger"; 
            }
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GoogleAuthenticationTest()
        {
            Tracing.TraceMsg("Entering Blogger AuthenticationTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service(this.ServiceName, this.ApplicationName);

            int iCount; 

            if (this.bloggerURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.bloggerURI);
                AtomFeed blogFeed = service.Query(query);

                ObjectModelHelper.DumpAtomObject(blogFeed,CreateDumpFileName("AuthenticationTest")); 
                iCount = blogFeed.Entries.Count; 

                String strTitle = "Dinner time" + Guid.NewGuid().ToString(); 

                if (blogFeed != null && blogFeed.Entries.Count > 0)
                {
                    // get the first entry
                    AtomEntry entry = blogFeed.Entries[0];

                    entry = ObjectModelHelper.CreateAtomEntry(1);
                    // blogger does not like labels yet.
                    entry.Categories.Clear(); 
                    entry.Title.Text = strTitle;
                    entry.Categories.Clear();
                    entry.IsDraft = true; 
                    entry.Updated = Utilities.EmptyDate;
                    entry.Published = Utilities.EmptyDate; 
                    AtomEntry newEntry = blogFeed.Insert(entry); 
                    iCount++; 
                    Tracing.TraceMsg("Created blogger entry");

                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 

                    AtomFeed newFeed = service.Query(singleQuery);

                    AtomEntry sameGuy = newFeed.Entries[0]; 

                    Tracing.TraceMsg("retrieved blogger entry");

                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 
                    Assert.IsTrue(sameGuy.IsDraft == true); 

                }

                blogFeed = service.Query(query);

                Assert.AreEqual(iCount, blogFeed.Entries.Count, "Feed should have one more entry, it has: " + blogFeed.Entries.Count); 

                if (blogFeed != null && blogFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (AtomEntry entry in blogFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            entry.Content.Content = "Maybe stay until breakfast";
                            entry.Content.Type = "text";
                            entry.Update();
                            Tracing.TraceMsg("Updated entry");
                        }
                    }
                }

                blogFeed = service.Query(query);

                Assert.AreEqual(iCount, blogFeed.Entries.Count, "Feed should have one more entry, it has: " + blogFeed.Entries.Count); 

                if (blogFeed != null && blogFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (AtomEntry entry in blogFeed.Entries)
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


                blogFeed = service.Query(query);
                Assert.AreEqual(iCount, blogFeed.Entries.Count, "Feed should have the same count again, it has: " + blogFeed.Entries.Count); 

                service.Credentials = null; 

            }

        }
        /////////////////////////////////////////////////////////////////////////////
        

        //////////////////////////////////////////////////////////////////////
        /// <summary>checks for xhtml persistence</summary> 
        //////////////////////////////////////////////////////////////////////
        [Ignore ("Currently broken on the server")]
        [Test] public void BloggerXHTMLTest()
        {
            Tracing.TraceMsg("Entering BloggerXHTMLTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service(this.ServiceName, this.ApplicationName);

            if (this.bloggerURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.bloggerURI);
                AtomFeed calFeed = service.Query(query);

                String strTitle = "Dinner time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    String xhtmlContent = "<div><b>this is an xhtml test text</b></div>"; 
                    AtomEntry entry = ObjectModelHelper.CreateAtomEntry(1); 
                    entry.Categories.Clear();
                    entry.Title.Text = strTitle;
                    entry.Content.Type = "xhtml";
                    entry.Content.Content = xhtmlContent;

                    AtomEntry newEntry = calFeed.Insert(entry); 
                    Tracing.TraceMsg("Created blogger entry");

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
        /// <summary>checks for xhtml persistence</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void BloggerHTMLTest()
        {
            Tracing.TraceMsg("Entering BloggerHTMLTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service(this.ServiceName, this.ApplicationName);

            if (this.bloggerURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.bloggerURI);
                AtomFeed calFeed = service.Query(query);

                String strTitle = "Dinner time" + Guid.NewGuid().ToString(); 

                if (calFeed != null)
                {
                    // get the first entry
                    String htmlContent = "<div>&lt;b&gt;this is an html test text&lt;/b&gt;</div>"; 
                    AtomEntry entry = ObjectModelHelper.CreateAtomEntry(1); 
                    entry.Categories.Clear();
                    entry.Title.Text = strTitle;
                    entry.Content.Type = "html";
                    entry.Content.Content = htmlContent;

                    AtomEntry newEntry = calFeed.Insert(entry); 
                    Tracing.TraceMsg("Created blogger entry");

                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 
                    AtomFeed newFeed = service.Query(singleQuery);
                    AtomEntry sameGuy = newFeed.Entries[0]; 

                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 
                    Assert.IsTrue(sameGuy.Content.Type.Equals("html"));
                    String input = HttpUtility.HtmlDecode(htmlContent); 
                    String output = HttpUtility.HtmlDecode(sameGuy.Content.Content); 
                    Assert.IsTrue(input.Equals(output), "The input string should be equal the output string"); 

                }

                service.Credentials = null; 

                factory.MethodOverride = false;

            }

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>tests if we can access a public feed</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void BloggerPublicFeedTest()
        {
            Tracing.TraceMsg("Entering BloggerPublicFeedTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service(this.ServiceName, this.ApplicationName);

            String publicURI = (String) this.externalHosts[0];

            if (publicURI != null)
            {
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(publicURI);
                AtomFeed feed = service.Query(query);

                 if (feed != null)
                 {
                       // look for the one with dinner time...
                    foreach (AtomEntry entry in feed.Entries)
                    {
                        Assert.IsTrue(entry.ReadOnly, "The entry should be readonly");
                    }
                 }
            }

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void BloggerStressTest()
        {
            Tracing.TraceMsg("Entering Blogger GoogleStressTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service(this.ServiceName, this.ApplicationName);

            if (this.bloggerURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.bloggerURI);
                AtomFeed blogFeed = service.Query(query);

                ObjectModelHelper.DumpAtomObject(blogFeed,CreateDumpFileName("AuthenticationTest")); 

                if (blogFeed != null)
                {
                    for (int i=0; i< 100; i++)
                    {
                        AtomEntry entry = ObjectModelHelper.CreateAtomEntry(i); 
                        entry.Categories.Clear();
                        entry.Title.Text = "Title " + i; 
                        entry.Content.Content = "Some text...";
                        entry.Content.Type = "html"; 

                        blogFeed.Insert(entry); 
                    }
                }
            }

        }
        /////////////////////////////////////////////////////////////////////////////


    } /////////////////////////////////////////////////////////////////////////////
}




