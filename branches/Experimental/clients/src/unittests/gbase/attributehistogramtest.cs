/* Copyright (c) 2006-2008 Google Inc.
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
using System.IO;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Net;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.GoogleBase;
using System.Collections.Generic;


namespace Google.GData.GoogleBase.UnitTests
{

    [TestFixture]
    [Category("GoogleBase")]
    public class AttributeHistogramTest
    {

        [Test]
        public void TestParse()
        {
            AttributeHistogram histogram =
                Parse("<attribute " +
                      " name='my attr'" +
                      " type='text'" +
                      " count='200'>" +
                      "  <value count='12'>hello</value>" +
                      "  <value count='100'>world</value>" +
                      "</attribute>");
            Assert.AreEqual("my attr", histogram.Name);
            Assert.AreEqual(GBaseAttributeType.Text, histogram.Type);
            Assert.AreEqual(200, histogram.Count);

            List<HistogramValue> values = histogram.Values;
            Assert.AreEqual(2, values.Count, "values.Length");
            Assert.AreEqual("hello", values[0].Content, "values[0]");
            Assert.AreEqual(12, values[0].Count, "values[0].Count");
            Assert.AreEqual("world", values[1].Content, "values[1]");
            Assert.AreEqual(100, values[1].Count, "values[1].Count");
        }

        [Test]
        public void TestGenerate()
        {
            HistogramValue[] values = { new HistogramValue("a", 10),
                                        new HistogramValue("b", 12) };
            AttributeHistogram original =
                new AttributeHistogram("xxx", GBaseAttributeType.Text, 1000, new List<HistogramValue>(values));

            StringWriter sWriter = new StringWriter();
            XmlWriter xmlWriter = new XmlTextWriter(sWriter);
            original.Save(xmlWriter);
            xmlWriter.Close();

            AttributeHistogram parsed = Parse(sWriter.ToString());

            Assert.AreEqual("xxx", parsed.Name, "name");
            Assert.AreEqual(1000, parsed.Count, "count");
            Assert.AreEqual(GBaseAttributeType.Text, parsed.Type, "type");

            List<HistogramValue> parsedValues = parsed.Values;
            Assert.AreEqual("a", parsedValues[0].Content, "values[0].Name");
            Assert.AreEqual(10, parsedValues[0].Count, "values[0].Count");
            Assert.AreEqual("b", parsedValues[1].Content, "values[1].Name");
            Assert.AreEqual(12, parsedValues[1].Count, "values[1].Count");
        }

        private AttributeHistogram Parse(String xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(xml));

            return AttributeHistogram.Parse(doc.DocumentElement);
        }
    }

}
