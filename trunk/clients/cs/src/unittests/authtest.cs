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
using Google.GData.Client.UnitTests;




namespace Google.GData.Client.AuthUnitTests
{
    [TestFixture] 
    [Category("GoogleAuthenticationTests")]
    public class AuthenticationTestSuite : BaseTestClass
    {

        /// <summary>holds the username to use</summary>
        protected string userName;
        /// <summary>holds the password to use</summary>
        protected string passWord;

        /// <summary>holds the default authhandler</summary> 
        protected string strAuthHandler; 
        /// <summary>holds the default UIR to test against</summary> 
        protected string  defaultUri;
    
        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public AuthenticationTestSuite()
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
        }
        /////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////
        /// <summary>the end it all method</summary> 
        //////////////////////////////////////////////////////////////////////
        [TearDown] public override void EndTest()
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

            IDictionary unitTestDictionary = (IDictionary) ConfigurationSettings.GetConfig("unitTestSection");

            if (unitTestDictionary != null)
            {
                if (unitTestDictionary["authHandler"] != null)
                {
                    this.strAuthHandler = (string) unitTestDictionary["authHandler"];
                    Tracing.TraceInfo("Read authHandler value: " + this.strAuthHandler); 
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

                if (unitTestDictionary["calendarURI"] != null)
                {
                    this.defaultUri = (string) unitTestDictionary["calendarURI"];
                    Tracing.TraceInfo("Read default URI value: " + this.defaultUri); 
                }

            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void PositiveTest()
        {
            Tracing.TraceMsg("Entering PositiveTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service();

             if (this.defaultUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultUri);
                service.Query(query);
                service.Credentials = null;
                factory.MethodOverride = false;
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void TestFactoryCredentialRetrieval()
        {
            Tracing.TraceMsg("Entering TestFactoryCredentialRetrieval");

            Service service = new Service("lh2", "test");

            if (this.defaultUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }
                string token = service.QueryAuthenticationToken(); 
            }
        }
        ////////////////////////////////////////////////////////////////////////////

     
         //////////////////////////////////////////////////////////////////////
        /// <summary>correct account, invalid password</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void InvalidCredentialsTest()
        {
            Tracing.TraceMsg("Entering InvalidCredentialsTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service();

            if (this.defaultUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultUri);
                try
                {
                    service.Query(query);
                }  
                catch (InvalidCredentialsException e)
                {
                    Tracing.TraceMsg("Got the correct Exception");
                }
                 service.Credentials = null;
                factory.MethodOverride = false;
            }
        }
        ////////////////////////////////////////////////////////////////////////////

         //////////////////////////////////////////////////////////////////////
        /// <summary>incorrect account</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void InvalidAccountTest()
        {
            Tracing.TraceMsg("Entering InvalidAccountTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service();

            if (this.defaultUri != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName + "xyz", "wrong");
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultUri);
                try
                {
                    service.Query(query);
                }  
                catch (InvalidCredentialsException e)
                {
                    Tracing.TraceMsg("Got the correct Exception");
                }
                catch (CaptchaRequiredException e)
                {
                    Console.WriteLine("Token: {0}", e.Token);
                    Console.WriteLine("URL: {0}", e.Url);
                    Tracing.TraceMsg("Got the correct Exception");
                }
                service.Credentials = null;
                factory.MethodOverride = false;
            }
        }
        ////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>catpcha test
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]
        [Ignore("don't want to block the account all the time")]
        public void CaptchaTest()
        {
        
            Uri uri = new Uri("http://www.google.com/calendar/feeds/default/private/full");

            CalendarService service = new CalendarService("foo");
            service.setUserCredentials(this.userName, "foobar");

            for (int i = 0; i < 50; i++)
            {
                try
                {
                    service.Query(uri);
                }
                catch (InvalidCredentialsException)
                {
                    // keep going
                    Console.WriteLine("Invalid");
                }
                catch (CaptchaRequiredException e)
                {
                    Console.WriteLine("Token: {0}", e.Token);
                    Console.WriteLine("URL: {0}", e.Url);
                    string response = Console.ReadLine();

                    ((GDataGAuthRequestFactory)service.RequestFactory).CaptchaToken = e.Token;
                    ((GDataGAuthRequestFactory)service.RequestFactory).CaptchaAnswer = response;
                    service.setUserCredentials(this.userName, this.passWord);
                    service.Query(uri);
                    break;
                }
            }
        }

    } /////////////////////////////////////////////////////////////////////////////
}




