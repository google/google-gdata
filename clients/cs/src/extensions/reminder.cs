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
using System.Collections;
using System.Text;
using Google.GData.Client;

namespace Google.GData.Extensions {

    /// <summary>
    /// GData schema extension describing a reminder.
    /// </summary>
    public class Reminder : IExtensionElement
    {

        /// <summary>
        /// Number of days before the event.
        /// </summary>
        protected int days;

        /// <summary>
        /// Number of hours.
        /// </summary>
        protected int hours;

        /// <summary>
        /// Number of minutes.
        /// </summary>
        protected int minutes;

        /// <summary>
        /// Absolute time of the reminder.
        /// </summary>
        protected DateTime absoluteTime;

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public Days</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int Days
        {
            get { return days;}
            set { days = value;}
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public Hours</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int Hours
        {
            get { return hours;}
            set { hours = value;}
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public Minutes</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int Minutes
        {
            get { return minutes;}
            set { minutes = value;}
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public absoluteTime</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public DateTime AbsoluteTime
        {
            get { return absoluteTime;}
            set { absoluteTime = value;}
        }

#region Reminder Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an XML node to create an Reminder object.</summary> 
        /// <param name="node">reminder node</param>
        /// <returns> the created Reminder object</returns>
        //////////////////////////////////////////////////////////////////////
        public static Reminder ParseReminder(XmlNode node)
        {
            Tracing.TraceCall();
            Reminder reminder = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            bool absoluteFlag = false;
            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.XmlReminderElement))
            {
                reminder = new Reminder();
                if (node.Attributes != null)
                {
                    if (node.Attributes[GDataParserNameTable.XmlAttributeAbsoluteTime] != null)
                    {
                        try
                        {
                            absoluteFlag = true;
                            reminder.AbsoluteTime =
                            DateTime.Parse(node.Attributes[GDataParserNameTable.XmlAttributeAbsoluteTime].Value);
                        }
                        catch (FormatException fe)
                        {
                            throw new ArgumentException("Invalid g:reminder/@absoluteTime.", fe);
                        }
                    }

                    if (node.Attributes[GDataParserNameTable.XmlAttributeDays] != null)
                    {
                        try
                        {
                            reminder.Days = Int32.Parse(node.Attributes[GDataParserNameTable.XmlAttributeDays].Value);
                        }
                        catch (FormatException fe)
                        {
                            throw new ArgumentException("Invalid g:reminder/@days.", fe);
                        }
                    }

                    if (node.Attributes[GDataParserNameTable.XmlAttributeHours] != null)
                    {
                        try
                        {
                            reminder.Hours = Int32.Parse(node.Attributes[GDataParserNameTable.XmlAttributeHours].Value);
                        }
                        catch (FormatException fe)
                        {
                            throw new ArgumentException("Invalid g:reminder/@hours.", fe);
                        }
                    }

                    if (node.Attributes[GDataParserNameTable.XmlAttributeMinutes] != null)
                    {
                        try
                        {
                            reminder.Minutes = Int32.Parse(node.Attributes[GDataParserNameTable.XmlAttributeMinutes].Value);
                        }
                        catch (FormatException fe)
                        {
                            throw new ArgumentException("Invalid g:reminder/@minutes.", fe);
                        }
                    }
                }
            }

            if ((reminder.Days == 0 ? 0 : 1) +
                (reminder.Hours == 0 ? 0 : 1) +
                (reminder.Minutes == 0 ? 0 : 1) +
                (!absoluteFlag ? 0 : 1) != 1)
            {

                throw new ArgumentException("g:reminder must have exactly one attribute.");
            }

            return reminder;
        }
#endregion

#region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.XmlReminderElement;}
        }

        /// <summary>
        /// Persistence method for the Reminder  object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (this.Days > 0 ||
                this.Hours > 0 ||
                this.Minutes > 0 ||
                Utilities.IsPersistable(this.AbsoluteTime))
                
            {
                writer.WriteStartElement(BaseNameTable.gDataPrefix, XmlName, BaseNameTable.gNamespace);

                if (Days > 0)
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeDays, this.Days.ToString());
                }

                if (Hours > 0)
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeHours, this.Hours.ToString());
                }

                if (Minutes > 0)
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeMinutes, this.Minutes.ToString());
                }

                if (AbsoluteTime != new DateTime(1, 1, 1))
                {
                    string date = Utilities.LocalDateTimeInUTC(AbsoluteTime);
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeAbsoluteTime, date);
                }

                writer.WriteEndElement();
            }
        }
#endregion
    }
}

