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
#region Using directives
using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using System.Xml;
using Google.GData.Client;
using System.Collections.Generic;
#endregion

namespace Google.GData.GoogleBase
{

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Object representation for the gm:attributes and
    /// gm:item_type tags in an item types feed entry.
    ///
    /// This object is a restricted view of the extension list of
    /// an entry that will look for two specific extensions:
    /// ItemTypeAttributes and MetadataItemType
    /// </summary>
    ///////////////////////////////////////////////////////////////////////
    public class ItemTypeDefinition
    {
        private static readonly List<AttributeId> NoAttributes = new List<AttributeId>();

        private readonly ExtensionList extensions;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an ItemTypeDefinition based
        /// on a list of extensions.</summary>
        /// <param name="extensions">list of extensions to query and modify
        /// </param>
        ///////////////////////////////////////////////////////////////////////
        public ItemTypeDefinition(ExtensionList extensions)
        {
            this.extensions = extensions;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Item type name</summary>
        ///////////////////////////////////////////////////////////////////////
        public String ItemType
        {
            get
            {
                MetadataItemType extension =
                    GBaseUtilities.GetExtension(extensions,
                                                typeof(MetadataItemType))
                    as MetadataItemType;
                return extension == null ? null : extension.Name;
            }

            set
            {
                GBaseUtilities.SetExtension(extensions,
                                            typeof(MetadataItemType),
                                            value == null
                                            ? null  : new MetadataItemType(value));
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Attributes defined for the item type</summary>
        ///////////////////////////////////////////////////////////////////////
        public List<AttributeId> Attributes
        {
            get
            {
                ItemTypeAttributes extension =
                    GBaseUtilities.GetExtension(extensions,
                                                typeof(ItemTypeAttributes))
                    as ItemTypeAttributes;
                return extension == null ? NoAttributes : extension.Attributes;
            }
            set
            {
                GBaseUtilities.SetExtension(extensions,
                                            typeof(ItemTypeAttributes),
                                            value == null || value.Count == 0
                                            ? null: new ItemTypeAttributes(value));
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Object representation for a gm:attributes tag
    /// in an item types feed.
    ///
    /// This object is usually used only through
    /// <see cref="ItemTypeDefinition"/>.</summary>
    ///////////////////////////////////////////////////////////////////////
    public class ItemTypeAttributes : IExtensionElementFactory
    {
        private readonly List<AttributeId> attributes;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an new gm:attributes tag with gm:attributes</summary>
        /// <param name="attributes">attributes defined for the item type</param>
        ///////////////////////////////////////////////////////////////////////
        public ItemTypeAttributes(List<AttributeId> attributes)
        {
            if (attributes == null)
            {
                throw new ArgumentNullException("attributes");
            }
            this.attributes = attributes;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Attribute name and types.</summary>
        ///////////////////////////////////////////////////////////////////////
        public List<AttributeId> Attributes
        {
            get
            {
                return attributes;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Parses XML code and generates an ItemTypeAttributes
        /// object.</summary>
        ///////////////////////////////////////////////////////////////////////
        public static ItemTypeAttributes Parse(XmlNode xml)
        {
            List<AttributeId> attributeIds = new List<AttributeId>();
            for (XmlNode child = xml.FirstChild; child != null; child = child.NextSibling)
            {
                if ("attribute" == child.LocalName 
                    && child.Attributes != null 
                    && child.Attributes["type"] != null)
                {
                    GBaseAttributeType type = GBaseAttributeType.ForName(child.Attributes["type"].Value);
                    attributeIds.Add(new AttributeId(child.Attributes["name"].Value, type));
                }
            }
            
            return new ItemTypeAttributes(attributeIds);
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates an XML representation for this object.</summary>
        ///////////////////////////////////////////////////////////////////////
        public void Save(XmlWriter writer)
        {
            writer.WriteStartElement(XmlPrefix,
                                     "attributes",
                                     XmlNameSpace);

            foreach (AttributeId attributeId in attributes)
            {
                writer.WriteStartElement(GBaseNameTable.GBaseMetaPrefix,
                                         "attribute",
                                         GBaseNameTable.NSGBaseMeta);
                writer.WriteAttributeString("name", attributeId.Name);
                if (attributeId.Type != null)
                {
                    writer.WriteAttributeString("type", attributeId.Type.Name);
                }
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        #region IExtensionElementFactory Members

        public string XmlName
        {
            get
            {
                return "attributes";
            }
        }

        public string XmlNameSpace
        {
            get
            {
                return GBaseNameTable.NSGBaseMeta;
            }
        }

        public string XmlPrefix
        {
            get
            {
                return GBaseNameTable.GBaseMetaPrefix;
            }
        }

        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            return Parse(node);
        }

        #endregion
    }


    ///////////////////////////////////////////////////////////////////////
    /// <summary>Attribute name and type</summary>
    ///////////////////////////////////////////////////////////////////////
    public struct AttributeId
    {
        private readonly string name;
        private readonly GBaseAttributeType type;

        /// <summary>Creates an AttributeId</summary>x
        public AttributeId(string name, GBaseAttributeType type)
        {
            this.name = name;
            this.type = type;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Attribute name</summary>
        ///////////////////////////////////////////////////////////////////////
        public string Name
        {
            get
            {
                return name;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Attribute type</summary>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttributeType Type
        {
            get
            {
                return type;
            }
        }

    }

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Object representation of a gm:item_type tag.
    ///
    /// This tag is usually accessed through
    /// <see cref="ItemTypeDefinition"/></summary>
    ///////////////////////////////////////////////////////////////////////
    public class MetadataItemType : IExtensionElementFactory
    {
        private readonly string name;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a new extension with the given item type
        /// name</summary>
        ///////////////////////////////////////////////////////////////////////
        public MetadataItemType(string name)
        {
            this.name = name;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Item type name</summary>
        ///////////////////////////////////////////////////////////////////////
        public string Name
        {
            get
            {
                return name;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Parses an XML representation and creates an object</summary>
        ///////////////////////////////////////////////////////////////////////
        public static MetadataItemType Parse(XmlNode xml)
        {
            return new MetadataItemType(xml.InnerText);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates an XML representation for this object.</summary>
        ///////////////////////////////////////////////////////////////////////
        public void Save(XmlWriter writer)
        {
            writer.WriteStartElement(XmlPrefix,
                                     "item_type",
                                     XmlNameSpace);
            writer.WriteString(name);
            writer.WriteEndElement();
        }

        #region IExtensionElementFactory Members

        public string XmlName
        {
            get
            {
                return "item_type";
            }
        }

        public string XmlNameSpace
        {
            get
            {
                return GBaseNameTable.NSGBaseMeta;
            }
        }

        public string XmlPrefix
        {
            get
            {
                return GBaseNameTable.GBaseMetaPrefix;
            }
        }

        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            return Parse(node);
        }

        #endregion
    }

}
