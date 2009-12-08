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

    }
}





