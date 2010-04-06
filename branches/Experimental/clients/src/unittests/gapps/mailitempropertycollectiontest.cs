/* Copyright (c) 2007 Google Inc.
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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/
using System;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Extensions.Apps;
using Google.GData.Extensions;

namespace Google.GData.Apps.UnitTests
{
    [TestFixture]
    [Category("GoogleApps")]
    public class MailItemPropertyCollectionTest
    {
        private ExtensionCollection<MailItemPropertyElement> mailItemProperties;

        [SetUp]
        public void Init()
        {
            mailItemProperties = new ExtensionCollection<MailItemPropertyElement>(new AtomEntry());
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void GetInitialSizeTest()
        {
            Assert.IsTrue(mailItemProperties.Count ==0, "Mail item property collection should initially be empty.");
        }

        [Test]
        public void InsertionTest()
        {
            mailItemProperties.Add(MailItemPropertyElement.STARRED);

            Assert.AreEqual(1, mailItemProperties.Count,
                "Mail item property collection should have size 1 after insertion");
            Assert.AreEqual(0, mailItemProperties.IndexOf(MailItemPropertyElement.STARRED),
                "Index of singleton mail item property should be zero");
            Assert.IsTrue(mailItemProperties.Contains(MailItemPropertyElement.STARRED),
                "Mail item property collection does not contain property after insertion");
        }

        [Test]
        public void DeletionTest()
        {
            mailItemProperties.Add(MailItemPropertyElement.TRASH);
            mailItemProperties.Remove(MailItemPropertyElement.TRASH);

            Assert.IsTrue(mailItemProperties.Count==0,
                "Mail item property collection should be empty after singleton element is removed");
        }

    }
}
