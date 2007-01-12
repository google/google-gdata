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
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Net;
using System.Text;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.GoogleBase;


namespace Google.GData.GoogleBase.UnitTests
{

    [TestFixture]
    [Category("GoogleBase")]
    public class GBaseQueryTest
    {
        private const string BaseUrl = "http://www.example.com/base/feeds/snippets";

        [Test]
        public void ParseBqTest()
        {
            GBaseQuery query = Parse("?bq=hello%20world&max-results=12");
            Assert.AreEqual("hello world", query.GoogleBaseQuery);
            Assert.AreEqual(12, query.NumberToRetrieve);
        }

        [Test]
        public void ParseMaxValuesTest()
        {
            GBaseQuery query = Parse("?q=hello&max-values=3");
            Assert.AreEqual(3, query.MaxValues);
        }

        [Test]
        public void ParseMissingMaxValuesTest()
        {
            GBaseQuery query = Parse("?q=hello");
            Assert.AreEqual(-1, query.MaxValues);
        }

        [Test]
        public void GenerateBqTest()
        {
            GBaseQuery query = new GBaseQuery(BaseUrl);
            query.GoogleBaseQuery = "hello";
            Assert.AreEqual(BaseUrl + "?bq=hello",
                            query.Uri.ToString());
        }

        [Test]
        public void GenerateMaxValuesTest()
        {
            GBaseQuery query = new GBaseQuery(BaseUrl);
            query.MaxValues = 12;
            Assert.AreEqual(BaseUrl + "?max-values=12",
                            query.Uri.ToString());
        }

        [Test]
        public void GenerateNoMaxValuesTest()
        {
            GBaseQuery query = new GBaseQuery(BaseUrl);
            query.MaxValues = 0;
            Assert.AreEqual(BaseUrl + "?max-values=0",
                            query.Uri.ToString());
        }

        [Test]
        public void ParseOrderByTest()
        {
            Assert.AreEqual("modification_time",
                            Parse(BaseUrl + "?orderby=modification_time").OrderBy);
        }

        [Test]
        public void GenerateOrderByTest()
        {
            GBaseQuery query = new GBaseQuery(BaseUrl);
            query.OrderBy = "test";
            Assert.AreEqual(BaseUrl + "?orderby=test",
                            query.Uri.ToString());
        }

        [Test]
        public void ParseSortOrderTest()
        {
            Assert.AreEqual(true,
                            Parse(BaseUrl + "?sortorder=ascending").AscendingOrder);
            Assert.AreEqual(false,
                            Parse(BaseUrl + "?sortorder=descending").AscendingOrder);
        }

        [Test]
        public void GenerateSortOrderTest()
        {
            GBaseQuery query = new GBaseQuery(BaseUrl);
            query.AscendingOrder = true;
            Assert.AreEqual(BaseUrl + "?sortorder=ascending",
                            query.Uri.ToString());
            query.AscendingOrder = false;
            Assert.AreEqual(BaseUrl,
                            query.Uri.ToString());
        }

        [Test]
        public void ParseRefine()
        {
            Assert.AreEqual(true,
                            Parse(BaseUrl + "?refine=true").Refine);
            Assert.AreEqual(false,
                            Parse(BaseUrl + "?refine=false").Refine);
        }

        [Test]
        public void GenerateRefineTest()
        {
            GBaseQuery query = new GBaseQuery(BaseUrl);
            query.Refine = true;
            Assert.AreEqual(BaseUrl + "?refine=true",
                            query.Uri.ToString());
            query.Refine = false;
            Assert.AreEqual(BaseUrl,
                            query.Uri.ToString());
        }

        [Test]
        public void ParseContent()
        {
            Assert.AreEqual("meta",
                            Parse(BaseUrl + "?content=meta").Content);
        }

        [Test]
        public void GenerateContentTest()
        {
            GBaseQuery query = new GBaseQuery(BaseUrl);
            query.Content = "test";
            Assert.AreEqual(BaseUrl + "?content=test",
                            query.Uri.ToString());

        }

        private GBaseQuery Parse(string parameters)
        {
            GBaseQuery query = new GBaseQuery();;
            query.Uri = new Uri(BaseUrl + parameters);
            return query;
        }
    }

}
