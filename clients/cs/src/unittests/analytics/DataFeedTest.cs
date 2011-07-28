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
            string xml = @"<?xml version='1.0' encoding='UTF-8'?>
                <feed xmlns='http://www.w3.org/2005/Atom' xmlns:dxp='http://schemas.google.com/analytics/2009' xmlns:openSearch='http://a9.com/-/spec/opensearch/1.1/' xmlns:gd='http://schemas.google.com/g/2005' gd:etag='W/&quot;DUINSHcycSp7I2A9WxRWFEQ.&quot;' gd:kind='analytics#data'>
                        <id>http://www.google.com/analytics/feeds/data?ids=ga:1174&amp;dimensions=ga:medium,ga:source&amp;metrics=ga:bounces,ga:visits&amp;filters=ga:medium%3D%3Dreferral&amp;start-date=2008-10-01&amp;end-date=2008-10-31</id>
                        <updated>2008-10-31T16:59:59.999-07:00</updated>
                        <title>Google Analytics Data for Profile 1174</title>
                        <link rel='self' type='application/atom+xml' href='http://www.google.com/analytics/feeds/data?max-results=5&amp;sort=-ga%3Avisits&amp;end-date=2008-10-31&amp;start-date=2008-10-01&amp;metrics=ga%3Avisits%2Cga%3Abounces&amp;ids=ga%3A1174&amp;dimensions=ga%3Asource%2Cga%3Amedium&amp;filters=ga%3Amedium%3D%3Dreferral'/>
                        <link rel='next' type='application/atom+xml' href='http://www.google.com/analytics/feeds/data?start-index=6&amp;max-results=5&amp;sort=-ga%3Avisits&amp;end-date=2008-10-31&amp;start-date=2008-10-01&amp;metrics=ga%3Avisits%2Cga%3Abounces&amp;ids=ga%3A1174&amp;dimensions=ga%3Asource%2Cga%3Amedium&amp;filters=ga%3Amedium%3D%3Dreferral'/>
                        <author>
                                <name>Google Analytics</name>
                        </author>
                        <generator version='1.0'>Google Analytics</generator>
                        <openSearch:totalResults>6451</openSearch:totalResults>
                        <openSearch:startIndex>1</openSearch:startIndex>
                        <openSearch:itemsPerPage>5</openSearch:itemsPerPage>
                        <dxp:aggregates>
                                <dxp:metric confidenceInterval='0.0' name='ga:visits' type='integer' value='136540'/>
                                <dxp:metric confidenceInterval='0.0' name='ga:bounces' type='integer' value='101535'/>
                        </dxp:aggregates>
                        <dxp:dataSource>
                                <dxp:property name='ga:profileId' value='1174'/>
                                <dxp:property name='ga:webPropertyId' value='UA-30481-1'/>
                                <dxp:property name='ga:accountName' value='Google Store'/>
                                <dxp:tableId>ga:1174</dxp:tableId>
                                <dxp:tableName>www.googlestore.com</dxp:tableName>
                        </dxp:dataSource>
                        <dxp:endDate>2008-10-31</dxp:endDate>
                        <dxp:startDate>2008-10-01</dxp:startDate>
                        <dxp:segment id='gaid::-11' name='Mobile Traffic'>
                          <dxp:definition>ga:operatingSystem==iPhone</dxp:definition>
                        </dxp:segment>
                        <entry gd:etag='W/&quot;C0UEQX47eSp7I2A9WxRWFEw.&quot;' gd:kind='analytics#datarow'>
                                <id>http://www.google.com/analytics/feeds/data?ids=ga:1174&amp;ga:medium=referral&amp;ga:source=blogger.com&amp;filters=ga:medium%3D%3Dreferral&amp;start-date=2008-10-01&amp;end-date=2008-10-31</id>
                                <updated>2008-10-30T17:00:00.001-07:00</updated>
                                <title>ga:source=blogger.com | ga:medium=referral</title>
                                <link rel='alternate' type='text/html' href='http://www.google.com/analytics'/>
                                <dxp:dimension name='ga:source' value='blogger.com'/>
                                <dxp:dimension name='ga:medium' value='referral'/>
                                <dxp:metric confidenceInterval='0.0' name='ga:visits' type='integer' value='68140'/>
                                <dxp:metric confidenceInterval='0.0' name='ga:bounces' type='integer' value='61095'/>
                        </entry>
                        <entry gd:etag='W/&quot;C0UEQX47eSp7I2A9WxRWFEw.&quot;' gd:kind='analytics#datarow'>
                                <id>http://www.google.com/analytics/feeds/data?ids=ga:1174&amp;ga:medium=referral&amp;ga:source=google.com&amp;filters=ga:medium%3D%3Dreferral&amp;start-date=2008-10-01&amp;end-date=2008-10-31</id>
                                <updated>2008-10-30T17:00:00.001-07:00</updated>
                                <title>ga:source=google.com | ga:medium=referral</title>
                                <link rel='alternate' type='text/html' href='http://www.google.com/analytics'/>
                                <dxp:dimension name='ga:source' value='google.com'/>
                                <dxp:dimension name='ga:medium' value='referral'/>
                                <dxp:metric confidenceInterval='0.0' name='ga:visits' type='integer' value='29666'/>
                                <dxp:metric confidenceInterval='0.0' name='ga:bounces' type='integer' value='14979'/>
                        </entry>
                </feed>
            ";

            Google.GData.Analytics.DataFeed feed = Parse(xml);

            Dataset f = new Dataset(feed);
            f.AutoPaging = false; 

            Assert.IsNotNull(f.Aggregates);
            Assert.IsNotNull(f.DataSource);
            Assert.IsNotNull(f.Entries);

            Assert.AreEqual(136540, f.Aggregates.Metrics[0].IntegerValue);
            Assert.AreEqual("0.0", f.Aggregates.Metrics[0].ConfidenceInterval);
            Assert.AreEqual("ga:visits", f.Aggregates.Metrics[0].Name);
            Assert.AreEqual("integer", f.Aggregates.Metrics[0].Type);

            Assert.AreEqual("ga:1174", f.DataSource.TableId);
            Assert.AreEqual("www.googlestore.com", f.DataSource.TableName);
            Assert.AreEqual("1174", f.DataSource.ProfileId);
            Assert.AreEqual("UA-30481-1", f.DataSource.WebPropertyId);
            Assert.AreEqual("Google Store", f.DataSource.AccountName);


            Assert.AreEqual(136540, Int32.Parse(feed.Aggregates.Metrics[0].Value));


            DataEntry blogger = feed.Entries[0] as DataEntry;
            Assert.IsNotNull(blogger, "entry");
            Assert.IsNotNull(blogger.Dimensions);
            Assert.IsNotNull(blogger.Metrics);

            Assert.AreEqual("ga:source", blogger.Dimensions[0].Name);
            Assert.AreEqual("blogger.com", blogger.Dimensions[0].Value);

            Assert.AreEqual("ga:visits", blogger.Metrics[0].Name);
            Assert.AreEqual(68140, int.Parse(blogger.Metrics[0].Value));

            DataEntry google = feed.Entries[1] as DataEntry;
            Assert.IsNotNull(google, "entry");
            Assert.IsNotNull(google.Dimensions);
            Assert.IsNotNull(google.Metrics);

            Assert.AreEqual("ga:source", google.Dimensions[0].Name);
            Assert.AreEqual("google.com", google.Dimensions[0].Value);

            Assert.AreEqual("ga:visits", google.Metrics[0].Name);
            Assert.AreEqual(29666, int.Parse(google.Metrics[0].Value));

            foreach (DataEntry entry in feed.Entries)
            {
                Assert.IsNotNull(entry, "entry");
                Assert.IsNotNull(entry.Dimensions);
                Assert.IsNotNull(entry.Metrics);

                Assert.AreEqual("ga:source", entry.Dimensions[0].Name);
                Assert.IsNotEmpty(entry.Dimensions[0].Value);

                Assert.AreEqual("ga:visits", entry.Metrics[0].Name);
                Assert.Greater(int.Parse(entry.Metrics[0].Value), 0);
            }

            foreach (Data d in f.Entries)
            {
                Assert.IsNotNull(d, "entry");
                Assert.IsNotNull(d.Dimensions);
                Assert.IsNotNull(d.Metrics);

                Assert.AreEqual("ga:source", d.Dimensions[0].Name);
                Assert.IsNotEmpty(d.Dimensions[0].Value);

                Assert.AreEqual("ga:visits", d.Metrics[0].Name);
                Assert.Greater(int.Parse(d.Metrics[0].Value), 0);
            }

            Assert.IsNotEmpty(feed.Segments);

            Segment s = feed.Segments[0];
            Assert.IsNotNull(s.Name);
            Assert.AreEqual(s.Id, "gaid::-11");
            Assert.IsNotNull(s.Id);
            Assert.AreEqual(s.Name, "Mobile Traffic");
            Assert.IsNotNull(s.Definition);
            Assert.IsNotEmpty(s.Definition.Value);
            Assert.AreEqual(s.Definition.Value, "ga:operatingSystem==iPhone");

            
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
