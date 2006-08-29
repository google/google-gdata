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

#region Using directives
using System;
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
#endregion

namespace Google.GData.GoogleBase {

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Identifiers for standard Google Base attribute types.
    /// All non-standard types share the type otherType</summary>
    ///////////////////////////////////////////////////////////////////////
    public enum StandardGBaseAttributeTypeIds
    {
        /// <summary>Id of the standard type <c>text</c></summary>
        textType,
        /// <summary>Id of the standard type <c>boolean</c></summary>
        booleanType,

        /// <summary>Id of the standard type <c>location</c></summary>
        locationType,

        /// <summary>Id of the standard type <c>url</c></summary>
        urlType,

        /// <summary>Id of the standard type <c>int</c></summary>
        intType,

        /// <summary>Id of the standard type <c>float</c></summary>
        floatType,

        /// <summary>Id of the standard type <c>number</c></summary>
        numberType,

        /// <summary>Id of the standard type <c>intUnit</c></summary>
        intUnitType,

        /// <summary>Id of the standard type <c>floatUnit</c></summary>
        floatUnitType,

        /// <summary>Id of the standard type <c>numberUnit</c></summary>
        numberUnitType,

        /// <summary>Id of the standard type <c>date</c></summary>
        dateType,

        /// <summary>Id of the standard type <c>dateTime</c></summary>
        dateTimeType,

        /// <summary>Id of the standard type <c>dateTimeRange</c></summary>
        dateTimeRangeType,

        /// <summary>An attribute type that could not be recognized by
        /// the library. See the attribute name.</summary>
        otherType
    }

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Object representation of Google Base attribute types.
    ///
    /// To get GBaseAttributeType instance, use one of the predefined
    /// constants or call GBaseAttributeType.ForName(string)
    /// </summary>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseAttributeType
    {
        private readonly string name;
        private readonly StandardGBaseAttributeTypeIds id;
        private readonly GBaseAttributeType supertype;


        /// <summary>Text attributes.</summary>
        public static readonly GBaseAttributeType Text =
            new GBaseAttributeType(StandardGBaseAttributeTypeIds.textType,
                                   "text");

        /// <summary>Boolean attributes.</summary>
        public static readonly GBaseAttributeType Boolean =
            new GBaseAttributeType(StandardGBaseAttributeTypeIds.booleanType,
                                   "boolean");

        /// <summary>Location attributes, as a free string.</summary>
        public static readonly GBaseAttributeType Location =
            new GBaseAttributeType(StandardGBaseAttributeTypeIds.locationType,
                                   "location");

        /// <summary>Url attributes.</summary>
        public static readonly GBaseAttributeType Url =
            new GBaseAttributeType(StandardGBaseAttributeTypeIds.urlType,
                                   "url");

        /// <summary>Number attribute: a float or an int.</summary>
        public static readonly GBaseAttributeType Number =
            new GBaseAttributeType(StandardGBaseAttributeTypeIds.numberType,
                                   "number");

        /// <summary>Integer attribute, a subtype of Number.</summary>
        public static readonly GBaseAttributeType Int =
            new GBaseAttributeType(Number,
                                   StandardGBaseAttributeTypeIds.intType,
                                   "int");

        /// <summary>Float attribute, a subtype of Number.</summary>
        public static readonly GBaseAttributeType Float =
            new GBaseAttributeType(Number,
                                   StandardGBaseAttributeTypeIds.floatType,
                                   "float");

        /// <summary>Number attribute with a unit ("12 km"). A supertype
        /// of FloatUnit and IntUnit.</summary>
        public static readonly GBaseAttributeType NumberUnit =
            new GBaseAttributeType(StandardGBaseAttributeTypeIds.numberUnitType,
                                   "numberUnit");

        /// <summary>Int attribute with a unit, a subtype of NumberUnit.</summary>
        public static readonly GBaseAttributeType IntUnit =
            new GBaseAttributeType(NumberUnit,
                                   StandardGBaseAttributeTypeIds.intUnitType,
                                   "intUnit");

        /// <summary>Float attribute with a unit, a subtype of NumberUnit.</summary>
        public static readonly GBaseAttributeType FloatUnit =
            new GBaseAttributeType(NumberUnit,
                                   StandardGBaseAttributeTypeIds.floatUnitType,
                                   "floatUnit");

        /// <summary>A time range, with a starting and an end date/time, a
        /// supertype of DateTime and Date. For example:
        /// "2006-01-01T12:00:00Z 2006-01-02T14:00:00Z"
        ///
        /// Empty time ranges are considered equivalent to DateTime.</summary>
        public static readonly GBaseAttributeType DateTimeRange =
            new GBaseAttributeType(StandardGBaseAttributeTypeIds.dateTimeRangeType,
                                   "dateTimeRange");

        /// <summary>A date and a time, a subtype of DateTimeRange.</summary>
        public static readonly GBaseAttributeType DateTime =
            new GBaseAttributeType(DateTimeRange,
                                   StandardGBaseAttributeTypeIds.dateTimeType,
                                   "dateTime");
        /// <summary>A date, a subtype of DateTime and DateTimeRange.</summary>
        public static readonly GBaseAttributeType Date =
            new GBaseAttributeType(DateTime,
                                   StandardGBaseAttributeTypeIds.dateType,
                                   "date");

        /// <summary>All standard attribute types.</summary>
        public static readonly GBaseAttributeType[] AllStandardTypes = {
                    Text, Boolean, Location, Url,
                    Int, Float, Number,
                    IntUnit, FloatUnit, NumberUnit,
                    Date, DateTime, DateTimeRange
                };

        private static readonly IDictionary StandardTypesDict = new Hashtable();
        static GBaseAttributeType()
        {
            foreach(GBaseAttributeType type in AllStandardTypes)
            {
                StandardTypesDict.Add(type.Name, type);
            }
        }

        private GBaseAttributeType(GBaseAttributeType supertype,
                                   StandardGBaseAttributeTypeIds id,
                                   string name)
        {
            this.supertype = supertype;
            this.name = name;
            this.id = id;
        }

        private GBaseAttributeType(StandardGBaseAttributeTypeIds id, string name)
                : this(null, id, name)
        {

        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Get or create an attribute with the given name.
        /// If the name corresponds to a standard attribute, the global
        /// instance will be returned. Otherwise, a new GBaseAttributeType
        /// with Id = otherType will be created.</summary>
        ///////////////////////////////////////////////////////////////////////
        public static GBaseAttributeType ForName(String name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            GBaseAttributeType standard =
                StandardTypesDict[name] as GBaseAttributeType;
            if (standard != null)
            {
                return standard;
            }

            return new GBaseAttributeType(StandardGBaseAttributeTypeIds.otherType,
                                          name);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the attribute name.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return name;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Compares two types by comparing their names.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override bool Equals(object o)
        {
            if (Object.ReferenceEquals(o, this))
            {
                return true;
            }

            if (!(o is GBaseAttributeType))
            {
                return false;
            }

            GBaseAttributeType other = o as GBaseAttributeType;

            if (other.id == id)
            {
                if (other.id == StandardGBaseAttributeTypeIds.otherType)
                {
                    return name.Equals(other.name);
                }
                return true;
            }
            return false;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates a hash code for this element that is
        /// consistent with its Equals() method.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override int GetHashCode()
        {
            if (id == StandardGBaseAttributeTypeIds.otherType)
            {
                return 11 + name.GetHashCode();
            }
            else
            {
                return (int)id;
            }
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Checks whether this object is a supertype or the
        /// same as another type.</summary>
        /// <param name="subtype">other attribute type.</param>
        ///////////////////////////////////////////////////////////////////////
        public bool IsSupertypeOf(GBaseAttributeType subtype)
        {
            if (this == subtype)
            {
                return true;
            }
            GBaseAttributeType otherSupertype = subtype.Supertype;
            if (otherSupertype == null)
            {
                return false;
            }
            return IsSupertypeOf(otherSupertype);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Type name</summary>
        ///////////////////////////////////////////////////////////////////////
        public string Name
        {
            get
            {
                return name;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Type identifier, otherType for nonstandard types.</summary>
        ///////////////////////////////////////////////////////////////////////
        public StandardGBaseAttributeTypeIds Id
        {
            get
            {
                return id;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>This type's supertype or null.</summary>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttributeType Supertype
        {
            get
            {
                return supertype;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Comparison based on the Equals() method.</summary>
        ///////////////////////////////////////////////////////////////////////
        public static bool operator ==(GBaseAttributeType a, GBaseAttributeType b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Comparison based on the Equals() method.</summary>
        ///////////////////////////////////////////////////////////////////////
        public static bool operator !=(GBaseAttributeType a, GBaseAttributeType b)
        {
            return !(a == b);
        }
    }


    ///////////////////////////////////////////////////////////////////////
    /// <summary>Extension element corresponding to a g: tag.
    /// This element contains the logic to convert a g: tag to and from
    /// XML.
    ///
    /// It is usually not accessed through
    /// <see href="GBaseAttributes">GBaseAttributes</see>.
    /// </summary>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseAttribute : IExtensionElement
    {
        private string name;
        private GBaseAttributeType type;
        private bool isPrivate;
        private string content;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an empty GBaseAttribute.</summary>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute()
        {
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a GBaseAttribute with a name and a type.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">attribute type or null if unknown</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute(String name, GBaseAttributeType type)
                : this(name, type, null)
        {
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a GBaseAttribute</summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">attribute type or null if unknown</param>
        /// <param name="content">value</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute(String name, GBaseAttributeType type, String content)
        {
            this.name = name;
            this.type = type;
            this.content = content;
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
            set
            {
                name = value;
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
            set
            {
                type = value;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Attribute value, as a string</summary>
        ///////////////////////////////////////////////////////////////////////
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Private access (XML attribute access="private")</summary>
        ///////////////////////////////////////////////////////////////////////
        public bool IsPrivate
        {
            get
            {
                return isPrivate;
            }
            set
            {
                isPrivate = value;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Parses an XML node and create the corresponding
        /// GBaseAttribute.</summary>
        ///////////////////////////////////////////////////////////////////////
        public static GBaseAttribute ParseGBaseAttribute(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            GBaseAttribute attribute = new GBaseAttribute();
            attribute.Name = FromXmlTagName(node.LocalName);
            if (node.Attributes["type"] != null)
            {
                attribute.Type = GBaseAttributeType.ForName(node.Attributes["type"].Value);
            }
            attribute.IsPrivate = node.Attributes["access"] != null &&
                                  "private".Equals(node.Attributes["access"].Value);
            attribute.Content = node.InnerXml;

            return attribute;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Converts the current GBaseAttribute to XML.</summary>
        ///////////////////////////////////////////////////////////////////////
        public void Save(XmlWriter writer)
        {
            writer.WriteStartElement(GBaseNameTable.GBasePrefix,
                                     ToXmlTagName(name),
                                     GBaseNameTable.NSGBase);
            if (type != null)
            {
                writer.WriteAttributeString("type", type.Name);
            }
            if (isPrivate)
            {
                writer.WriteAttributeString("access", "private");
            }
            if (content != null)
            {
                writer.WriteString(content);
            }
            writer.WriteEndElement();
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>A partial text representation, to help
        /// debugging.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return name + "(" + type + "):\"" + content + "\"";
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Compares two attribute names, types, content and
        /// private flag.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override bool Equals(object o)
        {
            if (!(o is GBaseAttribute))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, o))
            {
                return true;
            }

            GBaseAttribute other = o as GBaseAttribute;
            return name == other.name && type == other.type &&
                   content == other.content && isPrivate == other.isPrivate;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates a hash code for this element that is
        /// consistent with its Equals() method.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override int GetHashCode()
        {
            int retval = 49 * (27 + name.GetHashCode()) + type.GetHashCode();
            if (content != null)
            {
                retval = 49 * retval + content.GetHashCode();
            }
            return retval;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Given an attribute name, with spaces, generates
        /// the corresponding XML tag name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the name with all spaces replaced with underscores
        /// </returns>
        ///////////////////////////////////////////////////////////////////////
        static string ToXmlTagName(string name)
        {
            return name.Replace(' ', '_');
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Given an XML tag name, with underscores, generates
        /// the corresponding attribute name.</summary>
        /// <param name="name">XML tag name, without namespace prefix</param>
        /// <returns>the name with all underscores replaced with spaces
        /// </returns>
        ///////////////////////////////////////////////////////////////////////
        static string FromXmlTagName(string name)
        {
            return name.Replace('_', ' ');
        }
    }

}
