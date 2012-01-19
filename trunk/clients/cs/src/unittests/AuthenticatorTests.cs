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
using Google.GData.Documents;

namespace Google.GData.Client.LiveTests {
    [TestFixture]
    [Category("AuthenticatorTests")]
    public class AuthenticatorTestSuite : OAuthTestSuite {
        /// <summary>
        /// default empty constructor
        /// </summary> 
        public AuthenticatorTestSuite() {
        }

        /// <summary>
        /// the setup method
        /// </summary> 
        [SetUp]
        public override void InitTest() {
            Tracing.TraceCall();
            base.InitTest();
        }

        /// <summary>
        /// the end it all method
        /// </summary>
        [TearDown]
        public override void EndTest() {
            Tracing.ExitTracing();
        }

        /// <summary>
        /// runs an authentication test with client auth
        /// </summary>
        [Test]
        public void ClientLoginAuthenticatorTest() {
            Tracing.TraceMsg("Entering ClientLoginAuthenticatorTest");

            ClientLoginAuthenticator auth = new ClientLoginAuthenticator(
                this.ApplicationName,
                ServiceNames.Documents,
                this.userName,
                this.passWord);

            HttpWebRequest request = auth.CreateHttpWebRequest("GET", new Uri(DocumentsListQuery.documentsBaseUri));
            request.Headers.Set(GDataGAuthRequestFactory.GDataVersion, "3.0");
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            if (response.StatusCode == HttpStatusCode.Redirect) {
                request = WebRequest.Create(response.Headers["Location"]) as HttpWebRequest;
                auth.ApplyAuthenticationToRequest(request);

                response = request.GetResponse() as HttpWebResponse;
            }

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        /// <summary>
        /// runs an authentication test with 2-legged OAuth
        /// </summary>
        [Test]
        public void OAuth2LeggedAuthenticatorTest() {
            Tracing.TraceMsg("Entering OAuth2LeggedAuthenticationTest");

            OAuth2LeggedAuthenticator auth = new OAuth2LeggedAuthenticator(
                this.ApplicationName,
                this.oAuthConsumerKey,
                this.oAuthConsumerSecret,
                this.oAuthUser,
                this.oAuthDomain,
                this.oAuthSignatureMethod);

            HttpWebRequest request = auth.CreateHttpWebRequest("GET", new Uri("https://www.google.com/calendar/feeds/default/owncalendars/full"));
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            if (response.StatusCode == HttpStatusCode.Redirect) {
                request = WebRequest.Create(response.Headers["Location"]) as HttpWebRequest;
                auth.ApplyAuthenticationToRequest(request);

                response = request.GetResponse() as HttpWebResponse;
            }

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        /// <summary>
        /// runs an authentication test with 3-legged OAuth
        /// </summary>
        [Test]
        public void OAuth3LeggedAuthenticatorTest() {
            Tracing.TraceMsg("Entering OAuth3LeggedAuthenticationTest");

            OAuth3LeggedAuthenticator auth = new OAuth3LeggedAuthenticator(
                this.ApplicationName,
                this.oAuthConsumerKey,
                this.oAuthConsumerSecret,
                this.oAuthToken,
                this.oAuthTokenSecret,
                this.oAuthScope,
                this.oAuthSignatureMethod);

            HttpWebRequest request = auth.CreateHttpWebRequest("GET", new Uri("https://www.google.com/calendar/feeds/default/owncalendars/full"));
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            if (response.StatusCode == HttpStatusCode.Redirect) {
                request = WebRequest.Create(response.Headers["Location"]) as HttpWebRequest;
                auth.ApplyAuthenticationToRequest(request);

                response = request.GetResponse() as HttpWebResponse;
            }

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}