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

/* 
 * Created by Morten Christensen, http://blog.sitereactor.dk | http://twitter.com/sitereactor
 */

using System;
using Google.GData.Client;
using Google.GData.Client.LiveTests;
using Google.GData.Client.UnitTests;
using Google.GData.WebmasterTools;
using Google.WebmasterTools;
using NUnit.Framework;

namespace unittests.webmastertools
{
    public class WebmasterToolsUpdateTests : BaseLiveTestClass
    {
        private Google.GData.Client.UnitTests.TestContext TestContext1;

        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("userName") == true)
            {
                this.userName = (string)unitTestConfiguration["userName"];
                Tracing.TraceInfo("Read userName value: " + this.userName);
            }
            if (unitTestConfiguration.Contains("passWord") == true)
            {
                this.passWord = (string)unitTestConfiguration["passWord"];
                Tracing.TraceInfo("Read passWord value: " + this.passWord);
            }
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public Google.GData.Client.UnitTests.TestContext TestContext
        {
            get { return TestContext1; }
            set { TestContext1 = value; }
        }

        [Test]
        public void UpdateGeolocation()
        {
            WebmasterToolsService service = new WebmasterToolsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            try
            {

                SitesQuery q = new SitesQuery();
                SitesFeed sf = service.Query(q);

                SitesEntry entry = new SitesEntry();
                string siteUrl = "http://www.example.dk/";
                entry.Content.Src = siteUrl;
                entry.Content.Type = "text/plain";
                SitesEntry newEntry = service.Insert(new Uri(SitesQuery.HttpsFeedUrl), entry);

                newEntry.GeoLocation = "US";
                SitesEntry updatedEntry = newEntry.Update();

                Assert.IsTrue(updatedEntry != null);
                Assert.IsTrue(updatedEntry.GeoLocation == "US");
            }
            finally
            {
                Uri deleteUrl = SitesQuery.CreateCustomUri("http://www.example.dk");
                service.Delete(deleteUrl, null);
            }

        }

        [Test]
        public void RequestUpdateGeolocation()
        {
            RequestSettings settings = new RequestSettings("NETUnittests", this.userName, this.passWord);
            WebmasterToolsRequest f = new WebmasterToolsRequest(settings);

            try
            {

                Sites site = new Sites("http://www.example.com", "text/plain");
                Sites newSite = f.AddSite(site);

                newSite.GeoLocation = "US";
                //Sites updatedSite = f.Update(newSite);
                Sites updatedSite = f.UpdateSiteEntry(newSite, "http://www.example.com");

                Assert.IsNotNull(updatedSite);
            }
            finally
            {
                Uri deleteUrl = SitesQuery.CreateCustomUri("http://www.example.com");
                f.Delete(deleteUrl, null);
            }

        }

        [Test]
        public void MarkReadMessage()
        { }
    }
}
