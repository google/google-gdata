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
        /// <returns>SimpleAttribute, or NULL if the extension was not found</returns>
        public SimpleAttribute getCalendarExtension(string extension)
        {
            return FindExtension(extension, GDataParserNameTable.NSGCal) as SimpleAttribute;
        }

        /// <summary>
        /// Base method for retrieving Calendar extension element values.
        /// </summary>
        /// <param name="extension">The name of the Calendar extension element to look for</param>
        /// <returns>value as string, or NULL if the extension was not found</returns>
        public string getCalendarExtensionValue(string extension)
        {
            SimpleAttribute e = getCalendarExtension(extension);
            if (e != null)
            {
                return (string) e.Value;
            }
            return null;
        }


        /// <summary>
        /// Base method for setting Calendar extension element values.
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <param name="newValue">the new value for this extension element</param>
        /// <returns>SimpleAttribute, either a brand new one, or the one
        /// returned by the service</returns>
        public SimpleElement setCalendarExtension(string extension, string newValue)
        {
            if (extension == null)
            {
                throw new System.ArgumentNullException("extension");
            }

            SimpleAttribute ele = getCalendarExtension(extension);
            if (ele == null)
            {
                ele = CreateExtension(extension, GDataParserNameTable.NSGCal) as SimpleAttribute;
                this.ExtensionElements.Add(ele);
            }

            ele.Value = newValue;

            return ele;
        }


        /// <summary>
        /// This field tells if the calendar is currently hidden in the UI list
        /// </summary>
        public bool Hidden
        {
            get
            {
				bool value;
				if (!bool.TryParse(getCalendarExtensionValue(GDataParserNameTable.XmlHiddenElement), out value)) {
					value = false;
				}
				return value;
            }
            set
            {
                setCalendarExtension(GDataParserNameTable.XmlHiddenElement, Utilities.ConvertBooleanToXSDString(value));
            }
        }

        /// <summary>
        /// This field tells if the calendar is currently selected in the UI
        /// </summary>
        public bool Selected
        {
            get
            {
                bool value;
				if (!bool.TryParse(getCalendarExtensionValue(GDataParserNameTable.XmlSelectedElement), out value)) {
					value = false;
				}
				return value;
            }
            set
            {
                setCalendarExtension(GDataParserNameTable.XmlSelectedElement, Utilities.ConvertBooleanToXSDString(value));
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

        /// <summary>
        /// This field controls the time zone of the calendar.
        /// </summary>
        public string TimeZone
        {
            get
            {
                return getCalendarExtensionValue(GDataParserNameTable.XmlTimeZoneElement);
            }
            set
            {
                setCalendarExtension(GDataParserNameTable.XmlTimeZoneElement, value);
            }
        }

        /// <summary>
        /// This field controls the location of the calendar.
        /// </summary>
        public Where Location
        {
            get
            {
                return FindExtension(GDataParserNameTable.XmlWhereElement,
                                   BaseNameTable.gNamespace) as Where;
            }
            set
            {
                ReplaceExtension(GDataParserNameTable.XmlWhereElement,
                                 BaseNameTable.gNamespace, value);
            }
        }
 
    }

    /// <summary>
    /// Color schema describing a gCal:color
    /// </summary>
    public class GCalColor : SimpleAttribute
    {
        /// <summary>
        /// default calendar color constructor
        /// </summary>
        public GCalColor()
            : base(GDataParserNameTable.XmlColorElement, GDataParserNameTable.gCalPrefix, 
              GDataParserNameTable.NSGCal)
        {
        }

        /// <summary>
        /// default calendar color constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalColor(string initValue)
            : base(GDataParserNameTable.XmlColorElement, GDataParserNameTable.gCalPrefix, 
              GDataParserNameTable.NSGCal, initValue)
        {
        }
    }

    /// <summary>
    /// Color schema describing a gCal:hidden
    /// </summary>
    public class GCalHidden : SimpleAttribute
    {

        /// <summary>
        /// default calendar hidden constructor
        /// </summary>
        public GCalHidden()
            : base(GDataParserNameTable.XmlHiddenElement, GDataParserNameTable.gCalPrefix, 
              GDataParserNameTable.NSGCal)
        {
        }

        /// <summary>
        /// default calendar hidden constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalHidden(string initValue)
            : base(GDataParserNameTable.XmlHiddenElement, GDataParserNameTable.gCalPrefix, 
              GDataParserNameTable.NSGCal, initValue)
        {
        }
    }

    /// <summary>
    /// Color schema describing a gCal:selected
    /// </summary>
    public class GCalSelected : SimpleAttribute
    {
        /// <summary>
        /// default calendar selected constructor
        /// </summary>
        public GCalSelected()
            : base(GDataParserNameTable.XmlSelectedElement, GDataParserNameTable.gCalPrefix, 
              GDataParserNameTable.NSGCal)
        {
        }

        /// <summary>
        /// default calendar selected constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalSelected(string initValue)
            : base(GDataParserNameTable.XmlSelectedElement, GDataParserNameTable.gCalPrefix, 
              GDataParserNameTable.NSGCal, initValue)
        {
        }
    }

    /// <summary>
    /// Color schema describing a gCal:accesslevel
    /// </summary>
    public class GCalAccessLevel : SimpleAttribute
    {
        /// <summary>
        /// default calendar access level constructor
        /// </summary>        
        public GCalAccessLevel()
            : base(GDataParserNameTable.XmlAccessLevelElement, GDataParserNameTable.gCalPrefix, 
              GDataParserNameTable.NSGCal)
        {
        }

        /// <summary>
        /// default calendar acccess level
        ///  constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalAccessLevel(string initValue)
            : base(GDataParserNameTable.XmlAccessLevelElement, GDataParserNameTable.gCalPrefix, 
            GDataParserNameTable.NSGCal, initValue)
        {
        }
    }
}
