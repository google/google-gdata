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
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;

namespace Google.GData.Spreadsheets.UnitTests
{
    [TestFixture]
    [Category("GoogleSpreadsheets")]
    public class RowCountTest
    {
        private RowCountElement rowCount;
        private Random rng;

        [SetUp]
        public void Init()
        {
            rowCount = new RowCountElement();
            rng = new Random();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetCountTest()
        {
            Assert.AreEqual(0, rowCount.Count);
        }

        [Test]
        public void SetCountTest()
        {
            uint count = (uint)rng.Next();
            rowCount.Count = count;
            Assert.AreEqual(count, rowCount.Count);
        }
    }
}

