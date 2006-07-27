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
using System.Xml;
using System.IO;
using Google.GData.Client;
#endregion

//////////////////////////////////////////////////////////////////////
// <summary>GDataParserNameTable</summary> 
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Extensions
{

    /// <summary>
    /// Subclass of the nametable, has the extensions for the GNamespace
    /// </summary>
    public class GDataParserNameTable : BaseNameTable
    {
        /// <summary>the google calendar namespace</summary>
        public const string NSGCal  = "http://schemas.google.com/gCal/2005"; 

        /// <summary>the event prefix </summary>
        public static string Event = gNamespacePrefix + "event";

#region element strings
        /// <summary> timezone indicator on the feedlevel</summary>
        public const string XmlTimeZoneElement = "timezone"; 
        /// <summary>static string for parsing</summary> 
        public const string XmlWhenElement = "when";
        /// <summary>static string for parsing</summary> 
        public const string XmlWhereElement = "where";
        /// <summary>static string for parsing</summary> 
        public const string XmlWhoElement = "who";
        /// <summary>static string for parsing</summary> 
        public const string XmlEntryLinkElement = "entryLink";
        /// <summary>static string for parsing</summary> 
        public const string XmlFeedLinkElement = "feedLink";
        /// <summary>static string for parsing</summary> 
        public const string XmlEventStatusElement = "eventStatus";
        /// <summary>static string for parsing</summary> 
        public const string XmlVisibilityElement = "visibility";
        /// <summary>static string for parsing</summary> 
        public const string XmlTransparencyElement = "transparency";
        /// <summary>static string for parsing</summary>
        public const string XmlAttendeeTypeElement = "attendeeType";
        /// <summary>static string for parsing</summary>
        public const string XmlAttendeeStatusElement = "attendeeStatus";
        /// <summary>static string for parsing</summary>
        public const string XmlRecurrenceElement = "recurrence";
        /// <summary>static string for parsing</summary>
        public const string XmlRecurrenceExceptionElement = "recurrenceException";
        /// <summary>static string for parsing</summary>
        public const string XmlOriginalEventElement = "originalEvent";
        /// <summary>static string for parsing</summary>
        public const string XmlReminderElement = "reminder";
        /// <summary>static string for parsing</summary>
        public const string XmlCommentsElement = "comments";
#endregion

#region attribute strings

        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeStartTime = "startTime";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeEndTime = "endTime";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeValueString = "valueString";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeValue = "value";
        /// <summary>static string for parsing the email in gd:who</summary>    
        public const string XmlAttributeEmail = "email";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeRel = "rel";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeLabel = "label";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeHref = "href";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeCountHint = "countHint";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeReadOnly = "readOnly";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeId = "id";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeDays = "days";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeHours = "hours";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeMinutes = "minutes";
        /// <summary>static string for parsing</summary>    
        public const string XmlAttributeAbsoluteTime = "absoluteTime";
        /// <summary>static string for parsing the specialized attribute on a RecurringException</summary>    
        public const string XmlAttributeSpecialized = "specialized";

#endregion

    }
    /////////////////////////////////////////////////////////////////////////////

}
/////////////////////////////////////////////////////////////////////////////

