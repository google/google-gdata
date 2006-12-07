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

namespace Google.GData.Spreadsheets.UnitTests
{
    [TestFixture]
    [Category("GoogleSpreadsheets")]
    public class CustomTest
    {
        private ListEntry.Custom element;

        [SetUp]
        public void Init()
        {
            element = new ListEntry.Custom();
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetLocalNameTest()
        {
            Assert.IsNull(element.LocalName);
        }

        [Test]
        public void SetLocalNameTest()
        {
            element.LocalName = "local name";
            Assert.AreEqual("local name", element.LocalName);
        }

        [Test]
        public void GetValueTest()
        {
            Assert.IsNull(element.Value);
        }

        [Test]
        public void SetValueTest()
        {
            element.Value = "value";
            Assert.AreEqual("value", element.Value);
        }
    }
}
