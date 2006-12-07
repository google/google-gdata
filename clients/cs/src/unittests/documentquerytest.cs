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

namespace Google.GData.Client.UnitTests
{
    [TestFixture]
    [Category("Client")]
    public class DocumentQueryTest
    {
        private DocumentQuery query;

        [SetUp]
        public void Init()
        {
            query = new DocumentQuery("");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetTitleQueryTest()
        {
            Assert.IsNull(query.Title, "Clean get");
        }

        [Test]
        public void IsExactTitleTest()
        {
            Assert.IsFalse(query.Exact, "Clean get");
        }

        [Test]
        public void SetTitleExactTrueThenFalseTest()
        {
            query.Exact = true;
            Assert.IsTrue(query.Exact);
            query.Exact = false;
            Assert.IsFalse(query.Exact);
        }

        [Test]
        public void SetTitleExactFalseTest()
        {
            query.Exact = false;
            Assert.IsFalse(query.Exact);
        }

        [Test]
        public void SetTitleQuerySingleWordTest()
        {
            query.Title = "singleWord";
            Assert.AreEqual("singleWord", query.Title);
        }

        [Test]
        public void SetTitleQueryMultipleWordsTest()
        {
            query.Title = "multiple words";
            Assert.AreEqual("multiple words", query.Title);
        }
    }
}
