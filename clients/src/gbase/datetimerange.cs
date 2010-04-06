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
    /// <summary>A range with a start time and an end time.
    ///
    /// Empty ranges are considered as one single DateTime object
    /// by the Google Base framework.</summary>
    ///////////////////////////////////////////////////////////////////////
    public class DateTimeRange
    {
        private readonly DateTime start;
        private readonly DateTime end;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a DateTimeRange given a string representation.
        ///
        /// The string representation should be
        /// <c>&lt;date/datetime&gt; [" " &lt;date/datetime&gt;]</c>
        ///
        /// String representations for dates and date/time are accepted. In
        /// this case, the start and end dates will be the same and
        /// <see cref="IsDateTimeOnly">IsDateTimeOnly()</see> will be
        /// true.
        /// </summary>
        ///////////////////////////////////////////////////////////////////////
        public DateTimeRange(string stringrep)
                : this(ParseDateTime(ExtractStart(stringrep)),
                       ParseDateTime(ExtractEnd(stringrep)))
        {
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a DateTimeRange with a start and end date/time</summary>
        /// <param name="start">start date</param>
        /// <param name="end">end date</param>
        ///////////////////////////////////////////////////////////////////////
        public DateTimeRange(DateTime start, DateTime end)
        {
            AssertArgumentNotNull(start, "start");
            AssertArgumentNotNull(end, "end");
            this.start = start;
            this.end = end;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an empty range, equivalent to a single
        /// DateTime.
        ///
        /// Use <see cref="ToDateTime">ToDateTime()</see> to convert it
        /// back to a DateTime, if needed.</summary>
        /// <param name="dateTime">a single date/time</param>
        ///////////////////////////////////////////////////////////////////////
        public DateTimeRange(DateTime dateTime)
                : this(dateTime, dateTime)
        {

        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Checks whether the range is empty and should be considered
        /// as a single DateTime.</summary>
        /// <seealso cref="ToDateTime"/>
        ///////////////////////////////////////////////////////////////////////
        public bool IsDateTimeOnly()
        {
            return start.Equals(end);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Converts an empty range to a single DateTime object.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the range
        /// is not empty.</exception>
        /// <seealso cref="IsDateTimeOnly"/>
        ///////////////////////////////////////////////////////////////////////
        public DateTime ToDateTime()
        {
            if (!IsDateTimeOnly())
            {
                throw new InvalidOperationException("This is a real range, with start < end");
            }
            return start;
        }

        private void AssertArgumentNotNull(object arg, string name)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Start time.</summary>
        ///////////////////////////////////////////////////////////////////////
        public DateTime Start
        {
            get
            {
                return start;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>End time</summary>
        ///////////////////////////////////////////////////////////////////////
        public DateTime End
        {
            get
            {
                return end;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates a valid string representation for the DateTimeRange
        /// that can later be parsed by its constructor.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return Utilities.LocalDateTimeInUTC(start) + " " +
                   Utilities.LocalDateTimeInUTC(end);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates a hash code for this element that is
        /// consistent with its Equals() method.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override int GetHashCode()
        {
            return 49 * ( 17 + start.GetHashCode() ) + end.GetHashCode();
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Two ranges are equal if their start and end DateTime
        /// are equal.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override bool Equals(object o)
        {
            if (Object.ReferenceEquals(this, o))
            {
                return true;
            }
            if (!(o is DateTimeRange))
            {
                return false;
            }
            DateTimeRange other = o as DateTimeRange;
            return other.start == start && other.end == end;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Comparison based on Equals()</summary>
        ///////////////////////////////////////////////////////////////////////
        public static bool operator ==(DateTimeRange a, DateTimeRange b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null || (object)b == null)
            {
                return false;
            }
            return a.Equals(b);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Comparison based on Equals()</summary>
        ///////////////////////////////////////////////////////////////////////
        public static bool operator !=(DateTimeRange a, DateTimeRange b)
        {
            return !(a == b);
        }

        private static string ExtractStart(string startEnd)
        {
            int space = startEnd.IndexOf(' ');
            if (space == -1)
            {
                // There's only one date (start == end)
                return startEnd;
            }
            return startEnd.Substring(0, space);
        }

        private static string ExtractEnd(string startEnd)
        {
            int space = startEnd.IndexOf(' ');
            if (space == -1)
            {
                // There's only one date (start == end)
                return startEnd;
            }
            return startEnd.Substring(space + 1);
        }

        private static DateTime ParseDateTime(string stringValue)
        {
            // Make the error message more explicit
            try
            {
                return DateTime.Parse(stringValue);
            }
            catch(FormatException e)
            {
                throw new FormatException(e.Message + " (" + stringValue + ")", e);
            }
        }
    }


}
