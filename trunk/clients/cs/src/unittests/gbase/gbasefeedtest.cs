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
#define USE_TRACING

using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Net;
using System.Text;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.GoogleBase;
using System.Collections.Generic;


namespace Google.GData.GoogleBase.UnitTests
{

    [TestFixture]
    [Category("GoogleBase")]
    public class GBaseFeedTest
    {
        private static readonly Uri FeedUri =
            new Uri("http://www.google.com/base/feeds/snippets");
        private const string SAMPLE_FEED =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<feed xmlns=\"http://www.w3.org/2005/Atom\"" +
            "  xmlns:g=\"http://base.google.com/ns/1.0\"" +
            "  xmlns:gm=\"http://base.google.com/ns-metadata/1.0\"" +
            ">" +
            "  <entry>" +
            "    <author>" +
            "      <name>Jane Doe</name>" +
            "      <email>JaneDoe@gmail.com</email>" +
            "    </author>" +
            "    <category scheme='http://www.google.com/categories/itemtypes' term='Recipes'/>" +
            "    <title type='text'>He Jingxian's chicken</title>" +
            "    <content type='xhtml'>" +
            "      <div xmlns='http://www.w3.org/1999/xhtml'>Delectable Sichuan speciality</div>" +
            "    </content>" +
            "    <link rel='alternate' type='text/html' href='http://www.host.com/123456jsh9'/>" +
            "    <g:label type='text'>kung pao chicken</g:label>" +
            "    <g:label type='text'>chinese cuisine</g:label>" +
            "    <g:label type='text'>recipes</g:label>" +
            "    <g:label type='text'>asian</g:label>" +
            "    <g:label type='text'>sichuan</g:label>" +
            "    <g:location type='location'>Mountain View, CA 94043</g:location>" +
            "    <g:item_type type='text'>Recipes</g:item_type>" +
            "    <g:cooking_time type='intUnit'>30 minutes</g:cooking_time>" +
            "    <g:main_ingredient type='text'>chicken</g:main_ingredient>" +
            "    <g:main_ingredient type='text'>chili peppers</g:main_ingredient>" +
            "    <g:main_ingredient type='text'>peanuts</g:main_ingredient>" +
            "    <g:serving_count type='number'>5</g:serving_count>" +
            "    <gm:stats><gm:impressions total='100'/></gm:stats>" +
            "  </entry>" +
            "</feed>";

        [Test]
        public void ReadFeedFromXmlRegenerateAndReparse()
        {
            GBaseFeed feed = Parse(SAMPLE_FEED);
            CheckParsedFeedEntries(feed);
        }

        private void CheckParsedFeedEntries(GBaseFeed feed)
        {
            GBaseEntry entry = feed.Entries[0] as GBaseEntry;
            Assert.IsNotNull(entry, "entry");
            GBaseAttributes attrs = entry.GBaseAttributes;
            Assert.AreEqual("Recipes", attrs.ItemType, "item type");
            Assert.AreEqual("Mountain View, CA 94043", attrs.Location, "location");
            String[] labels = attrs.Labels;
            Assert.AreEqual("kung pao chicken", labels[0], "label");
            Assert.AreEqual("chinese cuisine", labels[1], "label");
            Assert.AreEqual("recipes", labels[2], "label");
            Assert.AreEqual("asian", labels[3], "label");
            Assert.AreEqual("sichuan", labels[4], "label");
            Assert.AreEqual(5f, attrs.GetNumberAttribute("serving count", 0f));
            Assert.IsNotNull(entry.Stats, "gm:stats");
            Assert.AreEqual(100, entry.Stats.Impressions.Total, "gm:impressions");
        }

        [Test]
        public void ParseGenerateAndReparse()
        {
            GBaseFeed feed = Parse(SAMPLE_FEED);

            StringWriter sw = new StringWriter();
            XmlWriter xmlWriter = new XmlTextWriter(sw);
            feed.SaveToXml(xmlWriter);
            xmlWriter.Close();

            Tracing.TraceMsg(sw.ToString());

            GBaseFeed reparsed = Parse(sw.ToString());
            CheckParsedFeedEntries(reparsed);
        }

        [Test]
        public void ReadAttributesFeedFromXml()
        {
            string xml = "<?xml version='1.0' encoding='UTF-8'?>" +
                         "<feed xmlns='http://www.w3.org/2005/Atom' xmlns:openSearch='http://a9.com/-/spec/opensearchrss/1.0/' xmlns:gm='http://base.google.com/ns-metadata/1.0'>" +
                         "  <id>http://www.google.com/base/feeds/attributes</id>" +
                         "  <updated>2006-08-24T14:26:45.558Z</updated>" +
                         "  <title type='text'>Attribute histogram for query: </title>" +
                         "  <link rel='alternate' type='text/html' href='http://www.google.com'/>" +
                         "  <link rel='http://schemas.google.com/g/2005#feed' type='application/atom+xml' href='http://www.google.com/base/feeds/attributes'/>" +
                         "  <link rel='self' type='application/atom+xml' href='http://www.google.com/base/feeds/attributes?max-results=1&amp;key=ABQIAAAA7VerLsOcLuBYXR7vZI2NjhTRERdeAiwZ9EeJWta3L_JZVS0bOBRIFbhTrQjhHE52fqjZvfabYYyn6A'/>" +
                         "  <generator version='1.0' uri='http://www.google.com'>GoogleBase</generator>" +
                         "  <openSearch:totalResults>1</openSearch:totalResults>" +
                         "  <openSearch:itemsPerPage>1</openSearch:itemsPerPage>" +
                         "  <entry>" +
                         "    <id>http://www.google.com/base/feeds/attributes/label%28text%29N</id>" +
                         "    <updated>2006-08-24T14:26:45.651Z</updated>" +
                         "    <title type='text'>label(text)</title>" +
                         "    <content type='text'>Attribute label of type text.</content>" +
                         "    <link rel='self' type='application/atom+xml' href='http://www.google.com/base/feeds/attributes/label%28text%29N'/>" +
                         "    <gm:attribute name='label' type='text' count='158778'>" +
                         "      <gm:value count='47544'>housing</gm:value>" +
                         "      <gm:value count='36954'>reviews</gm:value>" +
                         "    </gm:attribute>" +
                         "  </entry>" +
                         "</feed>";
            GBaseFeed feed = Parse(xml);
            GBaseEntry entry = feed.Entries[0] as GBaseEntry;
            Assert.IsNotNull(entry, "entry");

            AttributeHistogram hist = entry.AttributeHistogram;
            Assert.AreEqual("label", hist.Name, "name");
            Assert.AreEqual(GBaseAttributeType.Text, hist.Type, "type");
            Assert.AreEqual(158778, hist.Count, "count");

            List<HistogramValue> values = hist.Values;
            Assert.AreEqual(2, values.Count, "values.Length");
            Assert.AreEqual("housing", values[0].Content);
            Assert.AreEqual(47544, values[0].Count);
            Assert.AreEqual("reviews", values[1].Content);
            Assert.AreEqual(36954, values[1].Count);
        }

        [Test]
        public void ReadItemTypesFeedFromXml()
        {
            string xml = "<?xml version='1.0' encoding='UTF-8'?>\n" +
                         "<feed xmlns='http://www.w3.org/2005/Atom' xmlns:openSearch='http://a9.com/-/spec/opensearchrss/1.0/' xmlns:gm='http://base.google.com/ns-metadata/1.0'>\n" +
                         "  <id>http://www.google.com/base/feeds/itemtypes/en_US</id>\n" +
                         "  <updated>2006-08-24T14:32:27.228Z</updated>\n" +
                         "  <title type='text'>Item types for locale en_US</title>\n" +
                         "  <link rel='alternate' type='text/html' href='http://www.google.com'/>\n" +
                         "  <link rel='http://schemas.google.com/g/2005#feed' type='application/atom+xml' href='http://www.google.com/base/feeds/itemtypes/en_US'/>\n" +
                         "  <link rel='self' type='application/atom+xml' href='http://www.google.com/base/feeds/itemtypes/en_US?max-results=1&amp;key=ABQIAAAAfW6XRFfnNGUiegdqIq0KExT2yXp_ZAY8_ufC3CFXhHIE1NvwkxS8MfEf7Ag6UI0Ony8Yq3gZHm6c9w'/>\n" +
                         "  <author>\n" +
                         "    <name>Google Inc.</name>\n" +
                         "    <email>base@google.com</email>\n" +
                         "  </author>\n" +
                         "  <generator version='1.0' uri='http://www.google.com'>GoogleBase</generator>\n" +
                         "  <openSearch:totalResults>1</openSearch:totalResults>\n" +
                         "  <openSearch:itemsPerPage>1</openSearch:itemsPerPage>\n" +
                         "  <entry>\n" +
                         "    <id>http://www.google.com/base/feeds/itemtypes/en_US/products</id>\n" +
                         "    <updated>2006-08-24T14:32:27.233Z</updated>\n" +
                         "    <category scheme='http://www.google.com/categories/itemtypes' term='products'/>\n" +
                         "    <title type='text'>products</title>\n" +
                         "    <content type='text'>products</content>\n" +
                         "    <gm:item_type>products</gm:item_type>\n" +
                         "    <gm:attributes>\n" +
                         "      <gm:attribute name='product type' type='text'/>\n" +
                         "      <gm:attribute name='condition' type='text'/>\n" +
                         "      <gm:attribute name='count' type='int'/>\n" +
                         "    </gm:attributes>\n" +
                         "  </entry>\n" +
                         "</feed>";
            GBaseFeed feed = Parse(xml);
            GBaseEntry entry = feed.Entries[0] as GBaseEntry;
            Assert.IsNotNull(entry, "entry");

            ItemTypeDefinition def = entry.ItemTypeDefinition;
            Assert.IsNotNull(def, "ItemTypeDefinition");
            Assert.AreEqual("products", def.ItemType);
            Assert.AreEqual(3, def.Attributes.Count);
            Assert.AreEqual("product type", def.Attributes[0].Name);
            Assert.AreEqual(GBaseAttributeType.Text, def.Attributes[0].Type);
            Assert.AreEqual("condition", def.Attributes[1].Name);
            Assert.AreEqual(GBaseAttributeType.Text, def.Attributes[1].Type);
            Assert.AreEqual("count", def.Attributes[2].Name);
            Assert.AreEqual(GBaseAttributeType.Int, def.Attributes[2].Type);
        }

        private GBaseFeed Parse(String xml)
        {
            byte[] bytes = new UTF8Encoding().GetBytes(xml);
            GBaseFeed feed = new GBaseFeed(FeedUri, new GBaseService("Test", "boguskey"));
            feed.Parse(new MemoryStream(bytes), AlternativeFormat.Atom);
            return feed;
        }
    }

}
