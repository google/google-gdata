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
using Google.GData.Analytics;
using NUnit.Framework;

namespace Google.GData.Client.UnitTests.Analytics
{
    [TestFixture, Category("Analytics")]
    public class AccountEntryTest
    {
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
        ///A test for AccountEntry Constructor
        ///</summary>
        [Test]
        public void DataEntryConstructorTest()
        {
            AccountEntry target = new AccountEntry();
            Assert.IsNotNull(target, "object better not be null");
        }
    }
}
