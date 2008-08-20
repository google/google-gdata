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
using System.Text;
using System.Xml;
using NUnit.Framework;

using Google.Spreadsheets;

namespace Google.GData.Spreadsheets.UnitTests
{
    [TestFixture]
    [Category("GoogleSpreadsheets")]
    public class SpreadsheetApplicationTest
    {
        private Application app; 

        [SetUp]
        public void Init()
        {
            app = new Application("test", "testpwd");
        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        public void ParseRangeTest()
        {
            int [] result  = app.ParseRangeString("A1");

            Assert.IsTrue(result[0] == 1, "A1, A== 1");
            Assert.IsTrue(result[1] == 1, "A1, 1 == 1");
            
            result = app.ParseRangeString("A2:D2");
            Assert.IsTrue(result[0] == 1, "A2 , A == 1");
            Assert.IsTrue(result[1] == 2, "A2 , 2 == 2");
            Assert.IsTrue(result[2] == 4, "D2 , D == 4");
            Assert.IsTrue(result[3] == 2, "D2 , 2 == 2");


            result = app.ParseRangeString("Z17:AB2");
            Assert.IsTrue(result[0] == 26, "Z17 , Z == 26");
            Assert.IsTrue(result[1] == 17, "Z17 , 17 == 17");
            Assert.IsTrue(result[2] == 28, "AB2 , AB == 28");
            Assert.IsTrue(result[3] == 2, "AB2 , 2 == 2");

        }
    }
}
