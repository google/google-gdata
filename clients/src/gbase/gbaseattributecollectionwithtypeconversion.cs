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
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
using System.Collections.Generic;
#endregion

namespace Google.GData.GoogleBase {

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Typesafe convenience methods for getting and setting
    /// google base attributes (Level 2)
    ///
    /// This class adds convenience methods for accessing attributes
    /// according to their type.
    /// </summary>
    /// <seealso cref="GBaseAttributes"/>
    /// <seealso cref="GBaseAttributeCollection"/>
    /// <seealso cref="GBaseEntry"/>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseAttributeCollectionWithTypeConversion
                : GBaseAttributeCollection
    {
        private static readonly DateTime NoDateTime = new DateTime(0);

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a GBaseAttributeCollectionWithTypeConversion
        /// object that will access and modify the given extension list.
        /// </summary>
        /// <param name="baseList">a list that contains GBaseAttribute object,
        /// among others</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttributeCollectionWithTypeConversion(ExtensionList baseList)
                : base(baseList)
        {
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets the content of the first attribute found with
        /// a specific name, as a string, whatever its type.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the value of the first attribute, if found, or null
        /// </returns>
        ///////////////////////////////////////////////////////////////////////
        public String GetAttributeAsString(string name)
        {
            return ExtractContent(GetAttribute(name));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets the content of the first attribute found with
        /// a specific name and type, as a string.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">type of the attribute</param>
        /// <returns>the value of the first attribute, if found, or null
        /// </returns>
        ///////////////////////////////////////////////////////////////////////
        public String GetAttributeAsString(string name, GBaseAttributeType type)
        {
            return ExtractContent(GetAttribute(name, type));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets all the content of all the attributes found
        /// with a specific name, whatever their type might be.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the values of all the attributes found with this name
        /// as an array of strings, never null</returns>
        ///////////////////////////////////////////////////////////////////////
        public String[] GetAttributesAsString(string name)
        {
            return ExtractContent(GetAttributes(name));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets all the content of all the attributes found
        /// with a specific name and type..</summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">attribute type</param>
        /// <returns>the values of all the attributes found with this name
        /// and type as an array of strings, never null</returns>
        ///////////////////////////////////////////////////////////////////////
        public String[] GetAttributesAsString(string name, GBaseAttributeType type)
        {
            return ExtractContent(GetAttributes(name, type));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// text with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the value of the first attribute, or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public String GetTextAttribute(string name)
        {
            return GetAttributeAsString(name, GBaseAttributeType.Text);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// text with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public String[] GetTextAttributes(string name)
        {
            return GetAttributesAsString(name, GBaseAttributeType.Text);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type text.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddTextAttribute(string name, string value)
        {
            return Add(new GBaseAttribute(name, GBaseAttributeType.Text, value));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// url with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the value of the first attribute, or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public String GetUrlAttribute(string name)
        {
            return GetAttributeAsString(name, GBaseAttributeType.Url);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// url with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public String[] GetUrlAttributes(string name)
        {
            return GetAttributesAsString(name, GBaseAttributeType.Url);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type url.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddUrlAttribute(string name, string value)
        {
            return Add(new GBaseAttribute(name, GBaseAttributeType.Url, value));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the address of the first attribute of type
        /// location with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the value of the first attribute, or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public String GetLocationAttribute(string name)
        {
            return GetAttributeAsString(name, GBaseAttributeType.Location);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the address of all the attribute of type
        /// location with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public String[] GetLocationAttributes(string name)
        {
            return GetAttributesAsString(name, GBaseAttributeType.Location);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the object representation of an attribute
        /// of type location with this name.</summary>
        //////////////////////////////////////////////////////////////////////
        public Location GetLocationAttributeAsObject(string name)
        {
            GBaseAttribute attribute = GetAttribute(name, GBaseAttributeType.Location);
            if (attribute == null)
            {
                return null;
            }
            return new Location(attribute);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns all the object representations for attributes
        /// of type location with this name.</summary>
        //////////////////////////////////////////////////////////////////////
        public List<Location> GetLocationAttributesAsObjects(string name)
        {
            List<GBaseAttribute> attributes = GetAttributes(name, GBaseAttributeType.Location);
            List<Location> retval = new List<Location>(attributes.Count);
            for (int i = 0; i < retval.Count; i++)
            {
                retval[i] = new Location(attributes[i]);
            }
            return retval;
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the object representation for an attribute
        /// of type shipping.</summary>
        //////////////////////////////////////////////////////////////////////
        public Shipping GetShippingAttribute(string name)
        {
            GBaseAttribute attribute = GetAttribute(name, GBaseAttributeType.Shipping);
            if (attribute == null)
            {
                return null;
            }
            return new Shipping(attribute);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type shipping.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">attribute value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        //////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddShippingAttribute(string name, Shipping value)
        {
            return Add(value.CreateGBaseAttribute(name));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type location.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddLocationAttribute(string name, string value)
        {
            return Add(new GBaseAttribute(name, GBaseAttributeType.Location, value));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type location.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddLocationAttribute(string name, Location value)
        {
            return Add(value.CreateGBaseAttribute(name));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type boolean.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddBooleanAttribute(string name, bool value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.Boolean,
                                          value ? Utilities.XSDTrue : Utilities.XSDFalse));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type int.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddIntAttribute(string name, int value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.Int,
                                          NumberFormat.ToString(value)));
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type float.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddFloatAttribute(string name, float value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.Float,
                                          NumberFormat.ToString(value)));
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type number.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddNumberAttribute(string name, float value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.Number,
                                          NumberFormat.ToString(value)));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type number.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddNumberAttribute(string name, int value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.Number,
                                          NumberFormat.ToString(value)));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type intUnit.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">integer value</param>
        /// <param name="unit">unit</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddIntUnitAttribute(string name, int value, string unit)
        {
            return AddIntUnitAttribute(name, new IntUnit(value, unit));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type intUnit.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">attribute value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddIntUnitAttribute(string name, IntUnit value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.IntUnit,
                                          value.ToString()));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type floatUnit.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">float value</param>
        /// <param name="unit">unit</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddFloatUnitAttribute(string name,
                float value,
                string unit)
        {
            return AddFloatUnitAttribute(name, new FloatUnit(value, unit));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type floatUnit.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">attribute value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddFloatUnitAttribute(string name, FloatUnit value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.FloatUnit,
                                          value.ToString()));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type numberUnit.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">integer value</param>
        /// <param name="unit">unit</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddNumberUnitAttribute(string name,
                int value,
                string unit)
        {
            return AddNumberUnitAttribute(name, new IntUnit(value, unit));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type numberUnit.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">float value</param>
        /// <param name="unit">unit</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddNumberUnitAttribute(string name,
                float value,
                string unit)
        {
            return AddNumberUnitAttribute(name, new FloatUnit(value, unit));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type numberUnit.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">attribute value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddNumberUnitAttribute(string name, NumberUnit value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.NumberUnit,
                                          value.ToString()));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type date.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">attribute value. Only the date will be
        /// used.</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddDateAttribute(string name, DateTime value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.Date,
                                          Utilities.LocalDateInUTC(value)));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type date/time.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">attribute value</param>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddDateTimeAttribute(string name, DateTime value)
        {
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.DateTime,
                                          Utilities.LocalDateTimeInUTC(value)));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds a new attribute of type dateTimeRange.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value</param>
        /// <exception cref="ArgumentException">Thrown when the range is
        /// empty, in which case you should add a DateTime attribute instead.
        /// </exception>
        /// <returns>the newly-created GBaseAttribute object</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute AddDateTimeRangeAttribute(string name,
                DateTimeRange value)
        {
            if (value.IsDateTimeOnly())
            {
                // The server rejects empty ranges.
                throw new ArgumentException("value should not be an empty range. " +
                                            "You probably want to convert it into a " +
                                            "DateTime and call AddDateTimeAttribute().");
            }
            return Add(new GBaseAttribute(name,
                                          GBaseAttributeType.DateTimeRange,
                                          value.ToString()));
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// boolean with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="defaultValue">value to return if no attribute
        /// was found</param>
        /// <returns>the value of the first attribute, or the default</returns>
        ///////////////////////////////////////////////////////////////////////
        public bool GetBooleanAttribute(string name, bool defaultValue)
        {
            bool value;
            if (ExtractBooleanAttribute(name, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Looks for an attribute of type boolean with this name.
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value to set if the attribute is found</param>
        /// <returns>true if an attribute was found, in which case
        /// the value will have been set.</returns>
        ///////////////////////////////////////////////////////////////////////
        public bool ExtractBooleanAttribute(string name, out bool value)
        {
            String stringValue = GetAttributeAsString(name, GBaseAttributeType.Boolean);
            if (stringValue == null)
            {
                value = false;
                return false;
            }
            value = "true" == stringValue;
            return true;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// int with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="defaultValue">value to return if no attribute
        /// was found</param>
        /// <returns>the value of the first attribute, or the default</returns>
        ///////////////////////////////////////////////////////////////////////
        public int GetIntAttribute(string name, int defaultValue)
        {
            int value;
            if (ExtractIntAttribute(name, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Looks for an attribute of type int with this name.
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value to set if the attribute is found</param>
        /// <returns>true if an attribute was found, in which case
        /// the value will have been set.</returns>
        ///////////////////////////////////////////////////////////////////////
        public bool ExtractIntAttribute(string name, out int value)
        {
            String stringValue = GetAttributeAsString(name, GBaseAttributeType.Int);
            if (stringValue == null)
            {
                value = 0;
                return false;
            }
            value = NumberFormat.ToInt(stringValue);
            return true;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// int with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<int> GetIntAttributes(string name)
        {
            List<int> retval = new List<int>();
            foreach (GBaseAttribute attribute
                     in GetAttributes(name, GBaseAttributeType.Int))
            {
                String content = attribute.Content;
                if (content != null)
                {
                    retval.Add(NumberFormat.ToInt(content));
                }
            }
            return retval;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// float with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="defaultValue">value to return if no attribute
        /// was found</param>
        /// <returns>the value of the first attribute, or the default</returns>
        ///////////////////////////////////////////////////////////////////////
        public float GetFloatAttribute(string name, float defaultValue)
        {
            float value;
            if (ExtractFloatAttribute(name, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// number with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="defaultValue">value to return if no attribute
        /// was found</param>
        /// <returns>the value of the first attribute, or the default</returns>
        ///////////////////////////////////////////////////////////////////////
        public float GetNumberAttribute(string name, float defaultValue)
        {
            float value;
            if (ExtractNumberAttribute(name, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Looks for an attribute of type float with this name.
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value to set if the attribute is found</param>
        /// <returns>true if an attribute was found, in which case
        /// the value will have been set.</returns>
        ///////////////////////////////////////////////////////////////////////
        public bool ExtractFloatAttribute(string name, out float value)
        {
            return ExtractAttributeAsFloat(name,
                                           out value,
                                           GBaseAttributeType.Float);
        }

        private bool ExtractAttributeAsFloat(string name,
                                             out float value,
                                             GBaseAttributeType type)
        {
            String stringValue = GetAttributeAsString(name, type);
            if (stringValue == null)
            {
                value = 0;
                return false;
            }
            value = NumberFormat.ToFloat(stringValue);
            return true;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// float with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<float> GetFloatAttributes(string name)
        {
            return GetAttributesAsFloat(name, GBaseAttributeType.Float);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// float with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<float> GetNumberAttributes(string name)
        {
            return GetAttributesAsFloat(name, GBaseAttributeType.Number);
        }

        private List<float> GetAttributesAsFloat(string name, GBaseAttributeType type)
        {
            List<float> retval = new List<float>();
            foreach (GBaseAttribute attribute in GetAttributes(name, type))
            {
                String content = attribute.Content;
                if (content != null)
                {
                    retval.Add(NumberFormat.ToFloat(content));
                }
            }
            return retval;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Looks for an attribute of type number with this name.
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value to set if the attribute is found</param>
        /// <returns>true if an attribute was found, in which case
        /// the value will have been set.</returns>
        ///////////////////////////////////////////////////////////////////////
        public bool ExtractNumberAttribute(string name, out float value)
        {
            return ExtractAttributeAsFloat(name, out value, GBaseAttributeType.Number);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// intUnit, floatUnit or numberUnit with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the value of the first attribute, or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public NumberUnit GetNumberUnitAttribute(string name)
        {
            GBaseAttribute attribute = GetAttribute(name,
                                                    GBaseAttributeType.NumberUnit);
            return toNumberUnit(attribute);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// intUnit, floatUnit or numberUnit with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<NumberUnit> GetNumberUnitAttributes(string name)
        {
            List<NumberUnit> retval = new List<NumberUnit>();
            foreach (GBaseAttribute attribute
                     in GetAttributes(name, GBaseAttributeType.NumberUnit))
            {
                NumberUnit value = toNumberUnit(attribute);
                if (value != null)
                {
                    retval.Add(value);
                }
            }
            return retval;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// floatUnit with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the value of the first attribute, or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public FloatUnit GetFloatUnitAttribute(string name)
        {
            String stringValue = GetAttributeAsString(name,
                                 GBaseAttributeType.FloatUnit);
            if (stringValue == null)
            {
                return null;
            }
            return new FloatUnit(stringValue);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// floatUnit with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<FloatUnit> GetFloatUnitAttributes(string name)
        {
            String[] stringValues = GetAttributesAsString(name,
                                    GBaseAttributeType.FloatUnit);
            List<FloatUnit> retval = new List<FloatUnit>();
            foreach (String stringValue in stringValues)
            {
                if (stringValue != null)
                {
                    retval.Add(new FloatUnit(stringValue));
                }
            }
            return retval;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// intUnit with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the value of the first attribute, or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public IntUnit GetIntUnitAttribute(string name)
        {
            String stringValue = GetAttributeAsString(name, GBaseAttributeType.IntUnit);
            if (stringValue == null)
            {
                return null;
            }
            return new IntUnit(stringValue);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// intUnit with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<IntUnit> GetIntUnitAttributes(string name)
        {
            String[] stringValues = GetAttributesAsString(name,
                                    GBaseAttributeType.IntUnit);
            List<IntUnit> retval = new List<IntUnit>();
            foreach (String stringValue in stringValues)
            {
                if (stringValue != null)
                {
                    retval.Add(new IntUnit(stringValue));
                }
            }
            return retval;
        }

        private NumberUnit toNumberUnit(GBaseAttribute attribute)
        {
            if (attribute == null)
            {
                return null;
            }
            if (attribute.Type == GBaseAttributeType.IntUnit)
            {
                return new IntUnit(attribute.Content);
            }
            return new FloatUnit(attribute.Content);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// date with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="defaultValue">value returned if no attribute
        /// could be found.</param>
        /// <returns>the value of the first attribute, or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public DateTime GetDateAttribute(string name, DateTime defaultValue)
        {
            DateTime value;
            if (ExtractDateAttribute(name, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Looks for an attribute of type date with this name.
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value to set if the attribute is found</param>
        /// <returns>true if an attribute was found, in which case
        /// the value will have been set.</returns>
        ///////////////////////////////////////////////////////////////////////
        public bool ExtractDateAttribute(string name, out DateTime value)
        {
            return ExtractAttributeAsDateTime(name, GBaseAttributeType.Date, out value);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// date or dateTime with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="defaultValue">value returned if no attribute
        /// could be found.</param>
        /// <returns>the value of the first attribute, or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public DateTime GetDateTimeAttribute(string name, DateTime defaultValue)
        {
            DateTime value;
            if (ExtractDateTimeAttribute(name, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Looks for an attribute of type date or dateTime with this
        /// name</summary>
        /// <param name="name">attribute name</param>
        /// <param name="value">value to set if the attribute is found</param>
        /// <returns>true if an attribute was found, in which case
        /// the value will have been set.</returns>
        ///////////////////////////////////////////////////////////////////////
        public bool ExtractDateTimeAttribute(string name, out DateTime value)
        {
            return ExtractAttributeAsDateTime(name,
                                              GBaseAttributeType.DateTime,
                                              out value);
        }

        private bool ExtractAttributeAsDateTime(string name,
                                                GBaseAttributeType type,
                                                out DateTime value)
        {
            String stringValue = GetAttributeAsString(name, type);
            if (stringValue == null)
            {
                value = NoDateTime;
                return false;
            }
            try
            {
                value = DateTime.Parse(stringValue);
                return true;
            }
            catch(FormatException e)
            {
                throw new FormatException(e.Message + " (" + stringValue + ")", e);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value of the first attribute of type
        /// date, dateTime or dateTimeRange with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>the value of the first attribute, or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public DateTimeRange GetDateTimeRangeAttribute(string name)
        {
            String stringValue = GetAttributeAsString(name,
                                 GBaseAttributeType.DateTimeRange);
            if (stringValue == null)
            {
                return null;
            }
            return new DateTimeRange(stringValue);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// date with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<DateTime> GetDateAttributes(string name)
        {
            return GetAttributesAsDateTime(name, GBaseAttributeType.Date);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// date or dateTime with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<DateTime> GetDateTimeAttributes(string name)
        {
            return GetAttributesAsDateTime(name, GBaseAttributeType.DateTime);
        }

        private List<DateTime> GetAttributesAsDateTime(string name,
                GBaseAttributeType type)
        {
            List<DateTime> retval = new List<DateTime>();
            foreach (GBaseAttribute attribute in GetAttributes(name, type))
            {
                if (attribute.Content != null)
                {
                    retval.Add(DateTime.Parse(attribute.Content));
                }
            }
            return retval;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the values of all the attribute of type
        /// date, dateTime or dateTimeRange with this name.</summary>
        /// <param name="name">attribute name</param>
        /// <returns>all the values found, never nul</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<DateTimeRange> GetDateTimeRangeAttributes(string name)
        {
            List<DateTimeRange> retval = new List<DateTimeRange>();
            foreach (GBaseAttribute attribute
                     in GetAttributes(name, GBaseAttributeType.DateTimeRange))
            {
                if (attribute.Content != null)
                {
                    retval.Add(new DateTimeRange(attribute.Content));
                }
            }
            return retval;
        }

        private String ExtractContent(GBaseAttribute attribute)
        {
            if (attribute == null)
            {
                return null;
            }
            return attribute.Content;
        }

        private String[] ExtractContent(List<GBaseAttribute> attributes)
        {
            String[] retval = new String[attributes.Count];
            for (int i = 0; i < retval.Length; i++)
            {
                retval[i] = ExtractContent(attributes[i]);
            }
            return retval;
        }
    }

}
