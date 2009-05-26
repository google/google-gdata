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
using Google.GData.GoogleBase;
using Google.Analytics;
using NUnit.Framework;

namespace Google.GData.Client.UnitTests.Analytics
{
    /// <summary>
    ///This is a test class for DataFeedTest and is intended
    ///to contain all DataFeedTest Unit Tests
    ///</summary>
    [TestFixture, Category("Analytics")]
    public class DataFeedTest
    {
        private const string DataFeedUrl = "https://www.google.com/analytics/feeds/data";

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
        ///A test for CreateDataFeed
        ///</summary>
        [Test]
        public void CreateDataFeedTest()
        {
            DataFeed target = new DataFeed(null, null);
            DataEntry entry = target.CreateFeedEntry() as DataEntry;
            Assert.IsNotNull(entry, "better have a DataEntry here");
        }

        /// <summary>
        ///A test for DataFeed Constructor
        ///</summary>
        [Test]
        public void DataFeedConstructorTest()
        {
            DataFeed target = new DataFeed(null, null);
            Assert.IsNotNull(target, "better have an object");
            Assert.IsNull(target.Service, "better have no service yet");
        }

        [Test]
        public void DataParseTest()
        {
            string xml = "<?xml version='1.0' encoding='UTF-8'?>" +
                "<feed xmlns='http://www.w3.org/2005/Atom' xmlns:openSearch='http://a9.com/-/spec/opensearch/1.1/' xmlns:dxp='http://schemas.google.com/analytics/2009' xmlns:gd='http://schemas.google.com/g/2005' gd:etag='W/&quot;A05PSHcycSp9ImA2WxJSGYQ.&quot;'>" +
                "<id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;dimensions=ga:browser&amp;metrics=ga:pageviews&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id>" +
                "<updated>2009-05-10T01:01:01.999-07:00</updated><title>Google Analytics Data for Profile 123456</title>" +
                "<link rel='self' type='application/atom+xml' href='http://www.google.com/analytics/feeds/data?max-results=200&amp;sort=ga%3Abrowser%2Cga%3Apageviews&amp;end-date=2009-05-10&amp;start-date=2009-04-28&amp;metrics=ga%3Apageviews&amp;ids=ga%3A123456&amp;dimensions=ga%3Abrowser'/>" +
                "<author><name>Google Analytics</name></author>" +
                "<generator version='1.0'>Google Analytics</generator>" +
                "<openSearch:totalResults>10</openSearch:totalResults><openSearch:startIndex>1</openSearch:startIndex><openSearch:itemsPerPage>200</openSearch:itemsPerPage>" +
                "<dxp:startDate>2009-04-28</dxp:startDate>" +
                "<dxp:endDate>2009-05-10</dxp:endDate>" +
                "<dxp:aggregates>" +
                "<dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='50599'/>" +
                "</dxp:aggregates>" +
                "<dxp:dataSource>" +
                "<dxp:tableId>ga:123456</dxp:tableId>" +
                "<dxp:tableName>www.test.com</dxp:tableName>" +
                "<dxp:property name='ga:profileId' value='123456'/>" +
                "<dxp:property name='ga:webPropertyId' value='UA-111111-1'/>" +
                "<dxp:property name='ga:accountName' value='Test Account'/>" +
                "</dxp:dataSource>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Camino&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Camino</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Camino'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='8'/></entry>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Chrome&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Chrome</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Chrome'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='894'/></entry>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Firefox&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Firefox</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Firefox'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='9969'/></entry>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Internet%20Explorer&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Internet Explorer</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Internet Explorer'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='36261'/></entry>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Konqueror&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Konqueror</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Konqueror'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='1'/></entry>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Mozilla&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Mozilla</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Mozilla'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='21'/></entry>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Mozilla%20Compatible%20Agent&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Mozilla Compatible Agent</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Mozilla Compatible Agent'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='11'/></entry>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Netscape&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Netscape</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Netscape'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='4'/></entry>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Opera&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Opera</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Opera'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='84'/></entry>" +
                "<entry gd:etag='W/&quot;CQFFQX53eSp7ImA8WxPSGUw.&quot;'><id>http://www.google.com/analytics/feeds/data?ids=ga:123456&amp;ga:browser=Safari&amp;start-date=2009-04-28&amp;end-date=2009-05-10</id><updated>2009-05-09T17:00:00.001-07:00</updated><title>ga:browser=Safari</title><link rel='alternate' type='text/html' href='http://www.google.com/analytics'/><dxp:dimension name='ga:browser' value='Safari'/><dxp:metric confidenceInterval='0.0' name='ga:pageviews' type='integer' value='3346'/></entry>" +
                "</feed>";

            Google.GData.Analytics.DataFeed feed = Parse(xml);

            Dataset f = new Dataset(feed);
            f.AutoPaging = false; 

            Assert.IsNotNull(f.Aggregates);
            Assert.IsNotNull(f.DataSource);
            Assert.IsNotNull(f.Entries);

            Assert.AreEqual(50599, f.Aggregates.Metrics[0].IntegerValue);
            Assert.AreEqual("0.0", f.Aggregates.Metrics[0].ConfidenceInterval);
            Assert.AreEqual("ga:pageviews", f.Aggregates.Metrics[0].Name);
            Assert.AreEqual("integer", f.Aggregates.Metrics[0].Type);

            Assert.AreEqual("ga:123456", f.DataSource.TableId);
            Assert.AreEqual("www.test.com", f.DataSource.TableName);
            Assert.AreEqual("123456", f.DataSource.ProfileId);
            Assert.AreEqual("UA-111111-1", f.DataSource.WebPropertyId);
            Assert.AreEqual("Test Account", f.DataSource.AccountName);



            Assert.AreEqual(50599, Int32.Parse(feed.Aggregates.Metrics[0].Value));


            DataEntry camino = feed.Entries[0] as DataEntry;
            Assert.IsNotNull(camino, "entry");
            Assert.IsNotNull(camino.Dimensions);
            Assert.IsNotNull(camino.Metrics);

            Assert.AreEqual("ga:browser", camino.Dimensions[0].Name);
            Assert.AreEqual("Camino", camino.Dimensions[0].Value);

            Assert.AreEqual("ga:pageviews", camino.Metrics[0].Name);
            Assert.AreEqual(8, int.Parse(camino.Metrics[0].Value));

            DataEntry chrome = feed.Entries[1] as DataEntry;
            Assert.IsNotNull(chrome, "entry");
            Assert.IsNotNull(chrome.Dimensions);
            Assert.IsNotNull(chrome.Metrics);

            Assert.AreEqual("ga:browser", chrome.Dimensions[0].Name);
            Assert.AreEqual("Chrome", chrome.Dimensions[0].Value);

            Assert.AreEqual("ga:pageviews", chrome.Metrics[0].Name);
            Assert.AreEqual(894, int.Parse(chrome.Metrics[0].Value));

            DataEntry fireFox = feed.Entries[2] as DataEntry;
            Assert.IsNotNull(fireFox, "entry");
            Assert.IsNotNull(fireFox.Dimensions);
            Assert.IsNotNull(fireFox.Metrics);

            Assert.AreEqual("ga:browser", fireFox.Dimensions[0].Name);
            Assert.AreEqual("Firefox", fireFox.Dimensions[0].Value);

            Assert.AreEqual("ga:pageviews", fireFox.Metrics[0].Name);
            Assert.AreEqual(9969, int.Parse(fireFox.Metrics[0].Value));

            DataEntry ie = feed.Entries[3] as DataEntry;
            Assert.IsNotNull(ie, "entry");
            Assert.IsNotNull(ie.Dimensions);
            Assert.IsNotNull(ie.Metrics);

            Assert.AreEqual("ga:browser", ie.Dimensions[0].Name);
            Assert.AreEqual("Internet Explorer", ie.Dimensions[0].Value);

            Assert.AreEqual("ga:pageviews", ie.Metrics[0].Name);
            Assert.AreEqual(36261, int.Parse(ie.Metrics[0].Value));

            foreach (DataEntry entry in feed.Entries)
            {
                Assert.IsNotNull(entry, "entry");
                Assert.IsNotNull(entry.Dimensions);
                Assert.IsNotNull(entry.Metrics);

                Assert.AreEqual("ga:browser", entry.Dimensions[0].Name);
                Assert.IsNotEmpty(entry.Dimensions[0].Value);

                Assert.AreEqual("ga:pageviews", entry.Metrics[0].Name);
                Assert.Greater(int.Parse(entry.Metrics[0].Value), 0);
            }

            foreach (Data d in f.Entries)
            {
                Assert.IsNotNull(d, "entry");
                Assert.IsNotNull(d.Dimensions);
                Assert.IsNotNull(d.Metrics);

                Assert.AreEqual("ga:browser", d.Dimensions[0].Name);
                Assert.IsNotEmpty(d.Dimensions[0].Value);

                Assert.AreEqual("ga:pageviews", d.Metrics[0].Name);
                Assert.Greater(int.Parse(d.Metrics[0].Value), 0);
            }
        }

        private static Google.GData.Analytics.DataFeed Parse(string xml)
        {
            byte[] bytes = new UTF8Encoding().GetBytes(xml);
            DataFeed feed = new DataFeed(new Uri(DataFeedUrl), null);
            feed.Parse(new MemoryStream(bytes), AlternativeFormat.Atom);
            return feed;
        }
    }
}
