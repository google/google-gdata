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
     /// GData schema extension describing a reminder on an event.  You can
     /// represent a set of reminders where each has a (1) reminder period
     /// and (2) notification method.  The method can be either "sms",
     /// "email", "alert", "none", "all".
     ///
     /// The meaning of this set of reminders differs based on whether you
     /// are reading or writing feeds.  When reading, the set of reminders
     /// returned on an event takes into account both defaults on a
     /// parent recurring event (when applicable) as well as the user's
     /// defaults on calendar.  If there are no gd:reminders returned that
     /// means the event has absolutely no reminders.  "none" or "all" will
     /// not apply in this case.
     ///
     /// Writing is different because we have to be backwards-compatible
     /// (see *) with the old way of setting reminders.  For easier analysis
     /// we describe all the behaviors defined in the table below.  (Notice
     /// we only include cases for minutes, as the other cases specified in
     /// terms of days/hours/absoluteTime can be converted to this case.)
     ///
     /// Notice method is case-sensitive: must be in lowercase!
     ///
     ///                   no method      method         method=
     ///                   or method=all  =none          email|sms|alert
     ///  ____________________________________________________________________________
     ///  no gd:rem        *no reminder    N/A            N/A
     ///
     ///  1 gd:rem         *use user's    no reminder    InvalidEntryException
     ///                   def. settings
     ///
     ///  1 gd:rem min=0   *use user's    no reminder    InvalidEntryException
     ///                   def. settings
     ///
     ///  1 gd:rem min=-1  *no reminder   no reminder    InvalidEntryException
     ///
     ///  1 gd:rem min=+n  *override with no reminder    set exactly one reminder
     ///                   +n for user's                 on event at +n with given
     ///                   selected                      method
     ///                   methods
     ///
     ///  multiple gd:rem  InvalidEntry-  InvalidEntry-  copy this set exactly
     ///                   Exception      Exception
     ///
     /// Hence, to override an event with a set of reminder <time, method>
     /// pairs, just specify them exactly.  To clear an event of all
     /// overrides (and go back to inheriting the user's defaults), one can
     /// simply specify a single gd:reminder with no extra attributes.  To
     /// have NO event reminders on an event, either set a single
     /// gd:reminder with negative reminder time, or simply update the event
     /// with a single <gd:reminder method=none/>.
     ///
    /// </summary>
    public class Reminder : IExtensionElement
    {
 
         public enum ReminderMethod {
                alert,
                all,
                email,
                none,
                sms,
                unspecified
         };

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

        /// <summary>
        /// holds the method type
        /// </summary>
        protected ReminderMethod method;

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public Method Method</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public ReminderMethod Method
        {
            get {return this.method;}
            set {this.method = value;}
        }
        // end of accessor public Method Method

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

                    if (node.Attributes[GDataParserNameTable.XmlAttributeMethod] != null)
                    {
                        try
                        {
                            reminder.Method = (ReminderMethod)Enum.Parse(typeof(ReminderMethod), 
                                                                         node.Attributes[GDataParserNameTable.XmlAttributeMethod].Value,
                                                                         true);
                        }
                        catch (Exception e)
                        {
                            throw new ArgumentException("Invalid g:reminder/@method.", e);
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

                if (this.Method != ReminderMethod.unspecified)
                {
                    writer.WriteAttributeString(GDataParserNameTable.XmlAttributeMethod, this.Method.ToString());
                }
                writer.WriteEndElement();
            }
        }
#endregion
    }
}

