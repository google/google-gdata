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
using NUnit.Framework;
using Google.GData.Analytics;
using System;

namespace Google.GData.Client.UnitTests.Analytics
{

    /// <summary>
    ///This is a test class for DataQueryTest and is intended
    ///to contain all DataQueryTest Unit Tests
    ///</summary>
    [TestFixture, Category("Analytics")]
    public class DataQueryTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set
            {
                testContextInstance = value;
            }
        }
        
        /// <summary>
        ///A test for Dimensions property
        ///</summary>
        [Test]
        public void DimensionPropertyTest()
        {
            DataQuery target = new DataQuery();
            const string expected = "ga:productCategory,ga:productName";
            target.Dimensions = expected;
            string actual = target.Dimensions;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Dimensions parsing
        ///</summary>
        [Test]
        public void DimensionParseTest()
        {
            const string expected = "ga:productCategory,ga:productName";
            DataQuery target = new DataQuery();
            //Force expected to be parsed
            target.Uri = new Uri("http://test.com?dimensions=" + expected);
            string actual = target.Dimensions;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Metric parsing
        ///</summary>
        [Test]
        public void MetricParseTest()
        {
            const string expected = "ga:itemRevenue,ga:itemQuantity";
            DataQuery target = new DataQuery();
            //Force expected to be parsed
            target.Uri = new Uri("http://test.com?metrics=" + expected);
            string actual = target.Metrics;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Ids parsing
        ///</summary>
        [Test]
        public void IdsParseTest()
        {
            const string expected = "ga:1234";
            DataQuery target = new DataQuery();
            //Force expected to be parsed
            target.Uri = new Uri("http://test.com?ids=" + expected);
            string actual = target.Ids;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Filters parsing
        ///</summary>
        [Test]
        public void FiltersParseTest()
        {
            const string expected = "ga:country%3D%3DCanada";
            DataQuery target = new DataQuery();
            //Force expected to be parsed
            target.Uri = new Uri("http://test.com?filters=" + expected);
            string actual = target.Filters;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Sort parsing
        ///</summary>
        [Test]
        public void SortParseTest()
        {
            const string expected = "ga:productCategory";
            DataQuery target = new DataQuery();
            //Force expected to be parsed
            target.Uri = new Uri("http://test.com?sort=" + expected);
            string actual = target.Sort;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GAStartDate parsing
        ///</summary>
        [Test]
        public void GAStartDateParseTest()
        {
            const string expected = "2009-04-1";
            DataQuery target = new DataQuery();
            //Force expected to be parsed
            target.Uri = new Uri("http://test.com?start-date=" + expected);
            string actual = target.GAStartDate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GAEndDate parsing
        ///</summary>
        [Test]
        public void GAEndDateParseTest()
        {
            const string expected = "2009-04-25";
            DataQuery target = new DataQuery();
            //Force expected to be parsed
            target.Uri = new Uri("http://test.com?end-date=" + expected);
            string actual = target.GAEndDate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for parsing of a complete query
        /// Parse complete query URL and check the results
        ///</summary>
        [Test]
        public void ParseTest()
        {
            const string dimensionExpected = "ga:productCategory,ga:productName";
            const string metricExpected = "ga:itemRevenue,ga:itemQuantity";
            const string idsExpected = "ga:1234";
            const string filtersExpected = "ga:country%3D%3DCanada";
            const string sortExpected = "ga:productCategory";
            const string startDateExpected = "2009-04-1";
            const string endDateExpected = "2009-04-25";
            DataQuery target = new DataQuery();

            target.Uri = new Uri(string.Format("http://test.com?ids={0}&dimensions={1}&metrics={2}&filters={3}&sort={4}&start-date={5}&end-date={6}", idsExpected, dimensionExpected, metricExpected, filtersExpected, sortExpected, startDateExpected, endDateExpected));
            Assert.AreEqual("http://test.com/", target.BaseAddress);
            Assert.AreEqual(dimensionExpected, target.Dimensions);
            Assert.AreEqual(metricExpected, target.Metrics);
            Assert.AreEqual(idsExpected, target.Ids);
            Assert.AreEqual(filtersExpected, target.Filters);
            Assert.AreEqual(sortExpected, target.Sort);
            Assert.AreEqual(startDateExpected, target.GAStartDate);
            Assert.AreEqual(endDateExpected, target.GAEndDate);
        }

        /// <summary>
        /// A test for calculating the query by setting the paramaters
        /// Parse complete query URL and check the results
        ///</summary>
        [Test]
        public void CalculateQueryTest()
        {
            const string baseUrlExpected = "http://test.com/";
            const string dimensionExpected = "ga:productCategory,ga:productName";
            const string metricExpected = "ga:itemRevenue,ga:itemQuantity";
            const string idsExpected = "ga:1234";
            const string filtersExpected = "ga:country%3D%3DCanada";
            const string sortExpected = "ga:productCategory";
            const string startDateExpected = "2009-04-1";
            const string endDateExpected = "2009-04-25";
            DataQuery target = new DataQuery();

            target.BaseAddress = baseUrlExpected;
            target.Dimensions = dimensionExpected;
            target.Metrics = metricExpected;
            target.Ids = idsExpected;
            target.Filters = filtersExpected;
            target.Sort = sortExpected;
            target.GAStartDate = startDateExpected;
            target.GAEndDate = endDateExpected;

            Uri expectedResult = new Uri(string.Format("http://test.com?dimensions={0}&end-date={1}&filters={2}&ids={3}&metrics={4}&sort={5}&start-date={6}", Utilities.UriEncodeReserved(dimensionExpected), Utilities.UriEncodeReserved(endDateExpected), Utilities.UriEncodeReserved(filtersExpected), Utilities.UriEncodeReserved(idsExpected), Utilities.UriEncodeReserved(metricExpected), Utilities.UriEncodeReserved(sortExpected), Utilities.UriEncodeReserved(startDateExpected)));

            Assert.AreEqual(expectedResult.AbsoluteUri, target.Uri.AbsoluteUri);
        }

        /// <summary>
        ///A test for Uri
        ///</summary>
        [Test]
        public void UriTest()
        {
            DataQuery target = new DataQuery();
            Uri expected = new Uri("http://www.test.com/");
            Uri actual;
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StartDate
        ///</summary>
        [Test]
        public void StartDateTest()
        {
            DataQuery target = new DataQuery();
            DateTime expected = DateTime.Now;
            DateTime actual;
            target.StartDate = expected;
            actual = target.StartDate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Query
        ///</summary>
        [Test]
        public void QueryTest()
        {
            DataQuery target = new DataQuery();
            string expected = "TestValue";
            string actual;
            target.Query = expected;
            actual = target.Query;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NumberToRetrieve
        ///</summary>
        [Test]
        public void NumberToRetrieveTest()
        {
            DataQuery target = new DataQuery();
            int expected = 12;
            int actual;
            target.NumberToRetrieve = expected;
            actual = target.NumberToRetrieve;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ExtraParameters
        ///</summary>
        [Test]
        public void ExtraParametersTest()
        {
            DataQuery target = new DataQuery();
            string expected = "TestValue";
            string actual;
            target.ExtraParameters = expected;
            actual = target.ExtraParameters;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EndDate
        ///</summary>
        [Test]
        public void EndDateTest()
        {
            DataQuery target = new DataQuery();
            DateTime expected = DateTime.Now;
            DateTime actual;
            target.EndDate = expected;
            actual = target.EndDate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DataQuery Constructor
        ///</summary>
        [Test]
        public void DataQueryConstructorTest1()
        {
            DataQuery target = new DataQuery();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for DataQuery Constructor
        ///</summary>
        [Test]
        public void FeedQueryConstructorTest()
        {
            const string baseUri = "http://www.test.com/";
            DataQuery target = new DataQuery(baseUri);
            Assert.AreEqual(target.Uri, new Uri(baseUri));
        }
    }
}
