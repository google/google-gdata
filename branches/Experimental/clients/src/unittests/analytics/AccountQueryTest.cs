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
    ///This is a test class for AccountQueryTest and is intended
    ///to contain all AccountQueryTest Unit Tests
    ///</summary>
    [TestFixture, Category("Analytics")]
    public class AccountQueryTest
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
        ///Empty Query Test
        ///</summary>
        [Test]
        public void EmptyQueryTest()
        {
            const string result = "http://www.test.com/";
            AccountQuery target = new AccountQuery();
            target.BaseAddress = result;
            Assert.AreEqual(new Uri(result), target.Uri);
        }


        /// <summary>
        ///A test for Uri
        ///</summary>
        [Test]
        public void UriTest()
        {
            AccountQuery target = new AccountQuery();
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
            AccountQuery target = new AccountQuery();
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
            AccountQuery target = new AccountQuery();
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
            AccountQuery target = new AccountQuery();
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
            AccountQuery target = new AccountQuery();
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
            AccountQuery target = new AccountQuery();
            DateTime expected = DateTime.Now;
            DateTime actual;
            target.EndDate = expected;
            actual = target.EndDate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AccountQuery Constructor
        ///</summary>
        [Test]
        public void AccountQueryConstructorTest1()
        {
            AccountQuery target = new AccountQuery();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for AccountQuery Constructor
        ///</summary>
        [Test]
        public void AccountQueryConstructorTest()
        {
            const string baseUri = "http://www.test.com/";
            AccountQuery target = new AccountQuery(baseUri);
            Assert.AreEqual(target.Uri, new Uri(baseUri));
        }
    }
}
