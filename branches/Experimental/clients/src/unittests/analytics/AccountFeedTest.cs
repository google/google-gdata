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
/* Created by Alex Maitland, maitlandalex@gmail.com */
using System;
using System.IO;
using System.Text;
using Google.GData.Analytics;
using Google.GData.Client;
using Google.GData.Client.UnitTests;
using Google.Analytics;
using NUnit.Framework;

namespace Google.GData.Client.UnitTests.Analytics
{
    /// <summary>
    ///This is a test class for AccountFeedTest and is intended
    ///to contain all AccountFeedTest Unit Tests
    ///</summary>
    [TestFixture, Category("Analytics")]
    public class AccountFeedTest
    {
        private const string AccountFeedUrl = "https://www.google.com/analytics/feeds/accounts/default";

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        ///A test for CreateAccountFeed
        ///</summary>
        [Test]
        public void CreateAccountFeedTest()
        {
            AccountFeed target = new AccountFeed(null, null);
            AccountEntry entry = target.CreateFeedEntry() as AccountEntry;
            Assert.IsNotNull(entry, "better have a AccountEntry here");
        }

        /// <summary>
        ///A test for AccountFeed Constructor
        ///</summary>
        [Test]
        public void AccountFeedConstructorTest()
        {
            AccountFeed target = new AccountFeed(null, null);
            Assert.IsNotNull(target, "better have an object");
            Assert.IsNull(target.Service, "better have no service yet");
        }

        [Test]
        public void AccountParseTest()
        {
            string xml = @"<?xml version='1.0' encoding='UTF-8'?>
                           <feed xmlns='http://www.w3.org/2005/Atom' xmlns:gd='http://schemas.google.com/g/2005' gd:etag='W/&quot;D0UFR347eCp8ImA4WxVQE04.&quot;' xmlns:openSearch='http://a9.com/-/spec/opensearch/1.1/' xmlns:dxp='http://schemas.google.com/analytics/2009' xmlns:ga='http://schemas.google.com/ga/2009'>
                             <author>
                               <name>Google Analytics</name>
                             </author>
                             <generator>Google Analytics</generator>
                             <id>http://www.google.com/analytics/feeds/accounts/test@test.com</id>
                             <link href='http://www.google.com/analytics/feeds/accounts/default' rel='self' type='application/atom+xml' />
                             <title type='text'>Profile list for test@test.com</title>
                             <updated>2009-01-31T01:01:01+10:00</updated>
                             <dxp:segment id='gaid::-1' name='All Visits'>
                               <dxp:definition> </dxp:definition>
                             </dxp:segment>
                             <dxp:segment id='gaid::-2' name='New Visitors'>
                               <dxp:definition>ga:visitorType==New Visitor</dxp:definition>
                             </dxp:segment>


                             <entry gd:etag='W/&quot;D0UAR248eCp4ImA9WxVQE04.&quot;'>
                               <ga:customVariable index='1' name='My Custom Variable' scope='3'/>
                               <ga:customVariable index='2' name='My Seconds Variable' scope='1'/>
                               <dxp:tableId>ga:1234567</dxp:tableId>
                               <dxp:property name='ga:accountId' value='123456' />
                               <dxp:property name='ga:accountName' value='Test Account' />
                               <dxp:property name='ga:profileId' value='1234567' />
                               <dxp:property name='ga:webPropertyId' value='UA-111111-1' />
                               <title type='text'>www.test.com</title>
                               <id>http://www.google.com/analytics/feeds/accounts/ga:1234567</id>
                               <link href='http://www.google.com/analytics' rel='alternate' type='text/html' />
                               <content type='text'/>
                               <updated>2009-01-31T01:01:01+10:00</updated>
                               <ga:goal active='true' name='Completing Order' number='1' value='10.0'>
                                 <ga:destination caseSensitive='false' expression='/purchaseComplete.html' matchType='regex' step1Required='false'>
                                   <ga:step name='View Product Categories' number='1' path='/Apps|Accessories'/>
                                   <ga:step name='View Product' number='2' path='/Apps|Accessories/(.*)\.axd'/>
                                   <ga:step name='View Shopping Cart' number='3' path='/shoppingcart.aspx'/>
                                   <ga:step name='Login' number='4' path='/login.html'/>
                                   <ga:step name='Place Order' number='5' path='/placeOrder.html'/>
                                 </ga:destination>
                               </ga:goal>
                               <ga:goal active='true' name='Browsed my site over 5 minutes' number='2' value='0.0'>
                                 <ga:engagement comparison='&gt;' thresholdValue='300' type='timeOnSite'/>
                               </ga:goal>
                               <ga:goal active='true' name='Visited &gt; 4 pages' number='3' value='0.25'>
                                 <ga:engagement comparison='&gt;' thresholdValue='4' type='pagesVisited'/>
                               </ga:goal>
                             </entry>
                           </feed>";

            AccountFeed feed = Parse(xml);
            AccountEntry entry = feed.Entries[0] as AccountEntry;
            Assert.IsNotNull(entry, "entry");
            Assert.IsNotNull(entry.Properties);
            Assert.IsNotNull(entry.ProfileId);

            Assert.AreEqual("ga:accountId", entry.Properties[0].Name);
            Assert.AreEqual("123456", entry.Properties[0].Value);

            Assert.AreEqual("ga:accountName", entry.Properties[1].Name);
            Assert.AreEqual("Test Account", entry.Properties[1].Value);

            Assert.AreEqual("ga:profileId", entry.Properties[2].Name);
            Assert.AreEqual("1234567", entry.Properties[2].Value);

            Assert.AreEqual("ga:webPropertyId", entry.Properties[3].Name);
            Assert.AreEqual("UA-111111-1", entry.Properties[3].Value);

            Assert.AreEqual("www.test.com", entry.Title.Text);

            Account a = new Account();
            a.AtomEntry = entry; 

            Assert.AreEqual("123456", a.AccountId);
            Assert.AreEqual("Test Account", a.AccountName);
            Assert.AreEqual("1234567", a.ProfileId);
            Assert.AreEqual("UA-111111-1", a.WebPropertyId);
            Assert.AreEqual("www.test.com", a.Title);
            Assert.AreEqual("ga:1234567", a.TableId);

            Assert.IsNotEmpty(feed.Segments);
            foreach (Segment s in feed.Segments)
            {
                Assert.IsNotNull(s.Name);
                Assert.IsNotNull(s.Id);
            }

            Assert.AreEqual(feed.Segments[0].Name, "All Visits");
            Assert.AreEqual(feed.Segments[0].Id, "gaid::-1");

            Assert.AreEqual(feed.Segments[1].Name, "New Visitors");
            Assert.AreEqual(feed.Segments[1].Id, "gaid::-2");

            System.Collections.Generic.List<Goal> goals = entry.Goals;
            Assert.IsNotEmpty(goals);
            Assert.AreEqual(goals.Count, 3);

            //First goal: Test all the way down
            Goal firstGoal = goals[0];
            Assert.AreEqual(firstGoal.Active, "true" );
            Assert.AreEqual(firstGoal.Name , "Completing Order");
            Assert.AreEqual(firstGoal.Number, "1");
            Assert.AreEqual(firstGoal.Value, "10.0");

            //Destination
            Assert.IsNotNull(firstGoal.Destination);
            Destination destination = firstGoal.Destination;
            Assert.AreEqual(destination.CaseSensitive , "false" );
            Assert.AreEqual(destination.Expression , "/purchaseComplete.html" );
            Assert.AreEqual(destination.MatchType , "regex" );
            Assert.AreEqual(destination.Step1Required , "false" );

            //Test first step of destination
            //<ga:step name='View Product Categories' number='1' path='/Apps|Accessories'/>

            Assert.IsNotNull(destination.Steps);
            Assert.IsNotEmpty(destination.Steps);
            Step step = destination.Steps[0];

            Assert.AreEqual(step.Name, "View Product Categories");
            Assert.AreEqual(step.Number, "1");
            Assert.AreEqual(step.Path, "/Apps|Accessories");
        
            //Engagement
            //testing against node: <ga:engagement comparison='&gt;' thresholdValue='300' type='timeOnSite'/>

            Assert.IsNotNull(goals[1].Engagement);
            Engagement engagement = goals[1].Engagement;

            Assert.AreEqual(engagement.Comparison, ">");
            Assert.AreEqual(engagement.Threshold, "300");
            Assert.AreEqual(engagement.Type, "timeOnSite");


            //Custom Variables


            Assert.IsNotNull(entry.CustomVariables);
            Assert.IsNotEmpty(entry.CustomVariables);
            CustomVariable cv = entry.CustomVariables[0];

            Assert.AreEqual(cv.Name, "My Custom Variable");
            Assert.AreEqual(cv.Index, "1");
            Assert.AreEqual(cv.Scope, "3");
            

        }

        private static AccountFeed Parse(string xml)
        {
            byte[] bytes = new UTF8Encoding().GetBytes(xml);
            AccountFeed feed = new AccountFeed(new Uri(AccountFeedUrl), new AnalyticsService("Test"));
            feed.Parse(new MemoryStream(bytes), AlternativeFormat.Atom);
            return feed;
        }
    }
}
