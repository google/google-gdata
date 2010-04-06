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
    public class StatsTest
    {
        private const string XmlHeader =
            "<?xml version='1.0'?><root xmlns='" + GBaseNameTable.NSGBaseMeta + "'>";
        private const string XmlFooter = "</root>";

        [Test]
        public void TestParse()
        {
            Stats stats =
                Parse("<stats> " +
                      "  <impressions total='10'><source name ='somesource' count='2'/></impressions>" +
                      "  <clicks total='2'/>" +
                      "  <page_views total='1'/>" +
                      "</stats>");
            Assert.AreEqual(10, stats.Impressions.Total);
            Assert.AreEqual(2, stats.Clicks.Total);
            Assert.AreEqual(1, stats.PageViews.Total);
            Assert.AreEqual(2, stats.Impressions["somesource"]);
        }

        [Test]
        public void TestGenerate()
        {
            Stats stats = new Stats();
            stats.Impressions.Total = 500;
            stats.Impressions["hello"] = 3;
            stats.PageViews.Total = 9;

            Stats stats2 = GenerateAndParse(stats);
            Assert.AreEqual(500, stats2.Impressions.Total);
            Assert.AreEqual(0, stats2.Clicks.Total);
            Assert.AreEqual(9, stats2.PageViews.Total);
            Assert.AreEqual(3, stats2.Impressions["hello"]);
        }

        private Stats GenerateAndParse(Stats original)
        {
            StringWriter sWriter = new StringWriter();
            XmlWriter xmlWriter = new XmlTextWriter(sWriter);
            original.Save(xmlWriter);
            xmlWriter.Close();

            return Parse(sWriter.ToString());
        }

        private Stats Parse(String partialXml)
        {
            return ParseRaw(XmlHeader + partialXml + XmlFooter);
        }

        private Stats ParseRaw(String xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(xml));
            XmlNodeList nodeList = doc.GetElementsByTagName("stats", GBaseNameTable.NSGBaseMeta);
            Assert.AreEqual(1, nodeList.Count, "expected exactly one stats tag");
            return Stats.Parse(nodeList.Item(0));
        }
    }

}
