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
    public class ItemTypeDefinitionsTest
    {
        [Test]
        public void ParseMetadataItemTypeTest()
        {
            MetadataItemType type =
                MetadataItemType.Parse(Parse("<item_type>hello world</item_type>"));
            Assert.AreEqual("hello world", type.Name);
        }

        [Test]
        public void GenerateMetadataXmlTest()
        {
            MetadataItemType type = new MetadataItemType("xyz");
            string xml = GenerateXml(type);

            MetadataItemType parsed =
                MetadataItemType.Parse(Parse(xml));

            Assert.AreEqual("xyz", parsed.Name);
        }

        [Test]
        public void ParseAttributesTest()
        {
            ItemTypeAttributes attrs = ItemTypeAttributes.Parse(Parse("<attributes>" +
                                       "<attribute name='a' type='text'/>" +
                                       "<attribute name='b' type='boolean'/>" +
                                       "</attributes>"));
            List<AttributeId> attributeIds = attrs.Attributes;
            Assert.AreEqual(2, attributeIds.Count);
            Assert.AreEqual("a", attributeIds[0].Name);
            Assert.AreEqual(GBaseAttributeType.Text, attributeIds[0].Type);
            Assert.AreEqual("b", attributeIds[1].Name);
            Assert.AreEqual(GBaseAttributeType.Boolean, attributeIds[1].Type);
        }

        [Test]
        public void ParseEmptyAttributesTest()
        {
            ItemTypeAttributes attrs = ItemTypeAttributes.Parse(Parse("<attributes/>"));

            List<AttributeId> attributeIds = attrs.Attributes;
            Assert.IsNotNull(attributeIds);
            Assert.AreEqual(0, attributeIds.Count);
        }

        [Test]
        public void GenerateAttributesTest()
        {
            AttributeId[] ids = { new AttributeId("x", GBaseAttributeType.Int),
                                  new AttributeId("y", GBaseAttributeType.Float) };
            string xml = GenerateXml(new ItemTypeAttributes(new List<AttributeId>(ids)));

            List<AttributeId> parsedIds =
                ItemTypeAttributes.Parse(Parse(xml)).Attributes;
            Assert.AreEqual(2, parsedIds.Count);
            Assert.AreEqual("x", parsedIds[0].Name);
            Assert.AreEqual(GBaseAttributeType.Int, parsedIds[0].Type);
            Assert.AreEqual("y", parsedIds[1].Name);
            Assert.AreEqual(GBaseAttributeType.Float, parsedIds[1].Type);

        }

        [Test]
        public void ItemTypeDefinitionTest()
        {
            AttributeId[] ids = { new AttributeId("x", GBaseAttributeType.Int) };

            ExtensionList extList = ExtensionList.NotVersionAware();
            //extList.Add("garbage");
            //extList.Add(12);

            ItemTypeDefinition defs = new ItemTypeDefinition(extList);
            Assert.IsNull(defs.ItemType);
            Assert.IsNotNull(defs.Attributes);
            Assert.AreEqual(0, defs.Attributes.Count);

            extList.Add(new MetadataItemType("hello"));
            Assert.AreEqual("hello", defs.ItemType);

            extList.Add(new ItemTypeAttributes(new List<AttributeId>(ids)));
            Assert.AreEqual(1, defs.Attributes.Count);
            Assert.AreEqual("x", defs.Attributes[0].Name);
        }

        private XmlNode Parse(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(xml));
            return doc.DocumentElement;
        }

        private String GenerateXml(IExtensionElementFactory ext)
        {
            StringWriter sw = new StringWriter();
            XmlWriter xmlw = new XmlTextWriter(sw);
            ext.Save(xmlw);
            xmlw.Close();

            return sw.ToString();
        }
    }

}
