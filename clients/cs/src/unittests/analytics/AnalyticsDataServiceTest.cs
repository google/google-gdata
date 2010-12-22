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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Google.GData.Analytics;
using Google.GData.Client;
using Google.GData.Client.LiveTests;
using Google.GData.Client.UnitTests;
using Google.Analytics;
using NUnit.Framework;

namespace Google.GData.Client.UnitTests.Analytics
{
    [TestFixture, Category("Analytics")]
    public class AnalyticsDataServiceTest : BaseLiveTestClass
    {
        private const string DataFeedUrl = "http://www.google.com/analytics/feeds/data";
        private const string AccountFeedUrl = "http://www.google.com/analytics/feeds/accounts/default";

        private string accountId;


        private TestContext TestContext1;


        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("analyticsUserName"))
            {
                this.userName = (string)unitTestConfiguration["analyticsUserName"];
                Tracing.TraceInfo("Read userName value: " + this.userName);
            }
            if (unitTestConfiguration.Contains("analyticsPassWord"))
            {
                this.passWord = (string)unitTestConfiguration["analyticsPassWord"];
                Tracing.TraceInfo("Read passWord value: " + this.passWord);
            }

            if (unitTestConfiguration.Contains("accountId"))
            {
                this.accountId = (string)unitTestConfiguration["accountId"];
                Tracing.TraceInfo("Read accountId value: " + this.accountId);
            }

        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return TestContext1; }
            set { TestContext1 = value; }
        }

        [Test]
        public void QueryAccountIds()
        {
            AnalyticsService service = new AnalyticsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            AccountQuery feedQuery = new AccountQuery(AccountFeedUrl);
            AccountFeed actual = service.Query(feedQuery);

            foreach (AccountEntry entry in actual.Entries)
            {
                Assert.IsNotNull(entry.Id);
                Assert.IsNotNull(entry.ProfileId.Value);
                if (this.accountId == null)
                    this.accountId = entry.ProfileId.Value;
            }
        }

        [Test]
        public void QueryBrowserMetrics()
		{
			AnalyticsService service = new AnalyticsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            DataQuery query = new DataQuery(DataFeedUrl);
			query.Ids = this.accountId;
			query.Metrics = "ga:pageviews";
			query.Dimensions = "ga:browser";
			query.Sort = "ga:browser,ga:pageviews";
			query.GAStartDate = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
			query.GAEndDate = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
			query.NumberToRetrieve = 200;

			DataFeed actual = service.Query(query);

			XmlTextWriter writer = new XmlTextWriter("QueryBrowserMetricsOutput.xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;

            actual.SaveToXml(writer);

			foreach (DataEntry entry in actual.Entries)
			{
				Assert.IsNotNull(entry.Id);
			}
		}

        [Test]
        public void QueryProductCategoryResultForPeriod()
		{
            AnalyticsService service = new AnalyticsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            DataQuery query = new DataQuery(DataFeedUrl);
            query.Ids = this.accountId;
			query.Dimensions = "ga:productCategory,ga:productName";
			query.Metrics = "ga:itemRevenue,ga:itemQuantity";
			query.Sort = "ga:productCategory";
			query.GAStartDate = new DateTime(2009, 04, 19).ToString("yyyy-MM-dd");
			query.GAEndDate = new DateTime(2009, 04, 25).ToString("yyyy-MM-dd");

			DataFeed actual = service.Query(query);

			Assert.IsNotNull(actual);
			Assert.IsNotNull(actual.Entries);

			foreach(DataEntry entry in actual.Entries)
			{
				Assert.AreEqual(2, entry.Dimensions.Count);
				Assert.IsNotNull(entry.Dimensions[0]);
				Assert.IsNotNull(entry.Dimensions[0].Name);
				Assert.IsNotNull(entry.Dimensions[0].Value);
				Assert.IsNotNull(entry.Dimensions[1]);
				Assert.IsNotNull(entry.Dimensions[1].Name);
				Assert.IsNotNull(entry.Dimensions[1].Value);
				Assert.AreEqual(2, entry.Metrics.Count);
				Assert.IsNotNull(entry.Metrics[0]);
				Assert.IsNotNull(entry.Metrics[0].Name);
				Assert.IsNotNull(entry.Metrics[0].Value);
				Assert.IsNotNull(entry.Metrics[1]);
				Assert.IsNotNull(entry.Metrics[1].Name);
				Assert.IsNotNull(entry.Metrics[1].Value);
			}
		}

        [Test]
        public void QueryVisitorsResultForPeriod()
		{
            AnalyticsService service = new AnalyticsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            DataQuery query = new DataQuery(DataFeedUrl);
            query.Ids = this.accountId;
			query.Metrics = "ga:visitors";
			query.GAStartDate = new DateTime(2009, 04, 19).ToString("yyyy-MM-dd");
			query.GAEndDate = new DateTime(2009, 04, 25).ToString("yyyy-MM-dd");
			query.StartIndex = 1;

			DataFeed actual = service.Query(query);

			Assert.IsNotNull(actual.Aggregates);
			Assert.AreEqual(1, actual.Aggregates.Metrics.Count);
		}

        [Test]
        public void QuerySiteStatsResultForPeriod()
		{
            AnalyticsService service = new AnalyticsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            DataQuery query = new DataQuery(DataFeedUrl);
            query.Ids = this.accountId;
			query.Metrics = "ga:pageviews,ga:visits,ga:newVisits,ga:transactions,ga:uniquePageviews";
			query.GAStartDate = new DateTime(2009, 04, 19).ToString("yyyy-MM-dd");
			query.GAEndDate = new DateTime(2009, 04, 25).ToString("yyyy-MM-dd");
			query.StartIndex = 1;

			DataFeed actual = service.Query(query);

			Assert.IsNotNull(actual.Aggregates);
			Assert.AreEqual(5, actual.Aggregates.Metrics.Count);
		}

        [Test]
        public void QueryTransactionIdReturnAllResults()
		{
            AnalyticsService service = new AnalyticsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            int currentIndex = 1;
			const int resultsPerPage = 1000;
			List<DataFeed> querys = new List<DataFeed>();

			DataQuery query = new DataQuery(DataFeedUrl);
            query.Ids = this.accountId;
			query.Dimensions = "ga:transactionId,ga:date";
			query.Metrics = "ga:transactionRevenue";
			query.Sort = "ga:date";
			query.GAStartDate = new DateTime(2009, 04, 01).ToString("yyyy-MM-dd");
			query.GAEndDate = new DateTime(2009, 04, 30).ToString("yyyy-MM-dd");
			query.NumberToRetrieve = resultsPerPage;

			query.StartIndex = currentIndex;
			DataFeed actual = service.Query(query);

			querys.Add(actual);

			double totalPages = Math.Round(((double)actual.TotalResults / (double)resultsPerPage) + 0.5);

			for (int i = 1; i < totalPages; i++)
			{
				currentIndex += resultsPerPage;
				query.StartIndex = currentIndex;
				actual = service.Query(query);
				querys.Add(actual);
			}

			for (int i = 0; i < querys.Count; i++)
			{
				foreach (DataEntry entry in querys[i].Entries)
				{
					Assert.IsNotNull(entry.Id);
				}
			}
		}

        [Test]
        public void QueryTransactionIdReturn200Results()
		{
            AnalyticsService service = new AnalyticsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            DataQuery query = new DataQuery(DataFeedUrl);
            query.Ids = this.accountId;
			query.Dimensions = "ga:transactionId,ga:date";
			query.Metrics = "ga:transactionRevenue";
			query.Sort = "ga:date";
			query.GAStartDate = new DateTime(2009, 04, 01).ToString("yyyy-MM-dd");
			query.GAEndDate = new DateTime(2009, 04, 30).ToString("yyyy-MM-dd");
			query.NumberToRetrieve = 200;
			DataFeed actual = service.Query(query);

			Assert.AreEqual(200, actual.ItemsPerPage);

			Assert.IsNotNull(actual.Aggregates);
			Assert.AreEqual(1, actual.Aggregates.Metrics.Count);

			foreach (DataEntry entry in actual.Entries)
			{
				Assert.IsNotNull(entry.Id);
			}
		}

        [Test]
        public void QueryPageViews()
		{
            AnalyticsService service = new AnalyticsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            DataQuery query = new DataQuery(DataFeedUrl);
            query.Ids = this.accountId;
			query.Metrics = "ga:pageviews";
			query.Dimensions = "ga:pageTitle";
			query.Sort = "-ga:pageviews";
			query.GAStartDate = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
			query.GAEndDate = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
			query.NumberToRetrieve = 200;
			DataFeed actual = service.Query(query);

			Assert.IsNotNull(actual.Aggregates);
			Assert.AreEqual(1, actual.Aggregates.Metrics.Count);

			foreach (DataEntry entry in actual.Entries)
			{
				Assert.IsNotNull(entry.Id);
			}
		}

        [Test]
        public void TestAnalyticsModel()
        {
            RequestSettings settings = new RequestSettings("Unittests", this.userName, this.passWord);

            AnalyticsRequest request = new AnalyticsRequest(settings);

            Feed<Account> accounts = request.GetAccounts();

            foreach (Account a in accounts.Entries)
            {
                Assert.IsNotNull(a.AccountId);
                Assert.IsNotNull(a.ProfileId);
                Assert.IsNotNull(a.WebPropertyId);
                if (this.accountId == null)
                    this.accountId = a.TableId;
            }
            

            DataQuery q = new DataQuery(this.accountId, DateTime.Now.AddDays(-14), DateTime.Now.AddDays(-2), "ga:pageviews", "ga:pageTitle", "ga:pageviews");


            Dataset set = request.Get(q);

            foreach (Data d in set.Entries)
            {
                Assert.IsNotNull(d.Id);
                Assert.IsNotNull(d.Metrics);
                Assert.IsNotNull(d.Dimensions);
            }
        }
    }
}
