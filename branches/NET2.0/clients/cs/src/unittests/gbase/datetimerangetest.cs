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
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.GoogleBase;


namespace Google.GData.GoogleBase.UnitTests
{

    [TestFixture]
    [Category("GoogleBase")]
    public class DateTimeRangeTest
    {
        private static readonly string ATimeString = "2005-09-2T17:32:10Z";
        private static readonly string BTimeString = "2006-10-20T13:24:08Z";

        private static readonly DateTime ATime = DateTime.Parse(ATimeString);
        private static readonly DateTime BTime = DateTime.Parse(BTimeString);

        [Test]
        public void IsDateTimeOnlyTest()
        {
            Assert.IsFalse(new DateTimeRange(ATime, BTime).IsDateTimeOnly(),
                           "atime, btime");
            Assert.IsTrue(new DateTimeRange(ATime).IsDateTimeOnly(),
                          "atime");
        }

        [Test]
        public void ToStringTest()
        {
            Assert.AreEqual(new DateTimeRange(ATime, BTime),
                            new DateTimeRange(
                                new DateTimeRange(ATime, BTime).ToString()));
        }

        [Test]
        public void ParseRangeTest()
        {
            Assert.AreEqual(new DateTimeRange(ATime, BTime),
                            new DateTimeRange(ATimeString + " " + BTimeString));
        }

        [Test]
        public void ParseDateTimeOnlyTest()
        {
            Assert.AreEqual(new DateTimeRange(ATime),
                            new DateTimeRange(ATimeString));
        }
    }

}
