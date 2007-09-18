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
using System.Xml;
using System.IO; 
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Calendar
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// CalendarEntry API customization class for defining entries in a calendar feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class CalendarEntry : AbstractEntry
    {
        

        /// <summary>
        /// Constructs a new CalenderEntry instance
        /// </summary>
        public CalendarEntry() : base()
        {
            this.AddExtension(new GCalHidden());
            this.AddExtension(new GCalColor());
            this.AddExtension(new GCalSelected());
            this.AddExtension(new GCalAccessLevel());
            this.AddExtension(new Where());
            this.AddExtension(new TimeZone());
        }

        /// <summary>
        /// Basic method for retrieving Calendar extension elements.
        /// </summary>
        /// <param name="extension">The name of the extension element to look for</param>
        /// <returns>SimpleElement, or NULL if the extension was not found</returns>
        public SimpleElement getCalendarExtension(string extension)
        {
            return FindExtension(extension, GDataParserNameTable.NSGCal) as SimpleElement;
        }

        /// <summary>
        /// Base method for retrieving Calendar extension element values.
        /// </summary>
        /// <param name="extension">The name of the Calendar extension element to look for</param>
        /// <returns>value as string, or NULL if the extension was not found</returns>
        public string getCalendarExtensionValue(string extension)
        {
            SimpleElement e = getCalendarExtension(extension);
            if (e != null)
            {
                return (string) e.Attributes["value"];
            }
            return null;
        }


        /// <summary>
        /// Base method for setting Calendar extension element values.
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <param name="newValue">the new value for this extension element</param>
        /// <returns>SimpleElement, either a brand new one, or the one
        /// returned by the service</returns>
        public SimpleElement setCalendarExtension(string extension, string newValue)
        {
            if (extension == null)
            {
                throw new System.ArgumentNullException("extension");
            }

            SimpleElement ele = getCalendarExtension(extension);
            if (ele == null)
            {
                ele = CreateExtension(extension, GDataParserNameTable.NSGCal) as SimpleElement;
                this.ExtensionElements.Add(ele);
            }
            if (ele.Attributes.ContainsKey("value"))
            {
                ele.Attributes["value"] = newValue;
            }
            else
            {
                ele.Attributes.Add("value", newValue);
            }

            return ele;
        }


        /// <summary>
        /// This field tells if the calendar is currently hidden in the UI list
        /// </summary>
        public bool Hidden
        {
            get
            {
                return bool.Parse(getCalendarExtensionValue(GDataParserNameTable.XmlHiddenElement));
            }
            set
            {
                setCalendarExtension(GDataParserNameTable.XmlHiddenElement, value.ToString());
            }
        }

        /// <summary>
        /// This field tells if the calendar is currently selected in the UI
        /// </summary>
        public bool Selected
        {
            get
            {
                return bool.Parse(getCalendarExtensionValue(GDataParserNameTable.XmlSelectedElement));
            }
            set
            {
                setCalendarExtension(GDataParserNameTable.XmlSelectedElement, value.ToString());
            }
        }

        /// <summary>
        /// This field manages the color of the calendar.
        /// </summary>
        public string Color
        {
            get
            {
                return getCalendarExtensionValue(GDataParserNameTable.XmlColorElement);
            }
            set
            {
                setCalendarExtension(GDataParserNameTable.XmlColorElement, value);
            }
        }

        /// <summary>
        /// This field deals with the access level of the current user on the calendar.
        /// </summary>
        public string AccessLevel
        {
            get
            {
                return getCalendarExtensionValue(GDataParserNameTable.XmlAccessLevelElement);
            }
        }
 
    }

    /// <summary>
    /// Color schema describing a gCal:color
    /// </summary>
    public class GCalColor : SimpleElement
    {
        public GCalColor()
            : base("color", GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal)
        {
            this.Attributes.Add("value", null);
        }

        public GCalColor(string initValue)
            : base("color", GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal)
        {
            this.Attributes.Add("value", initValue);
        }
    }

    /// <summary>
    /// Color schema describing a gCal:hidden
    /// </summary>
    public class GCalHidden : SimpleElement
    {
        public GCalHidden()
            : base("hidden", GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal)
        {
            this.Attributes.Add("value", null);
        }

        public GCalHidden(string initValue)
            : base("hidden", GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal)
        {
            this.Attributes.Add("value", initValue);
        }
    }

    /// <summary>
    /// Color schema describing a gCal:selected
    /// </summary>
    public class GCalSelected : SimpleElement
    {
        public GCalSelected()
            : base("selected", GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal)
        {
            this.Attributes.Add("value", null);
        }

        public GCalSelected(string initValue)
            : base("selected", GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal)
        {
            this.Attributes.Add("value", initValue);
        }
    }

    /// <summary>
    /// Color schema describing a gCal:accesslevel
    /// </summary>
    public class GCalAccessLevel : SimpleElement
    {
        public GCalAccessLevel()
            : base("accesslevel", GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal)
        {
            this.Attributes.Add("value", null);
        }

        public GCalAccessLevel(string initValue)
            : base("accesslevel", GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal)
        {
            this.Attributes.Add("value", initValue);
        }
    }
}
