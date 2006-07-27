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
using System.Text;
using System.Globalization;
using System.Diagnostics;
using Google.GData.Client;

namespace Google.GData.Calendar {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of FeedQuery, to create a Calendar query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class EventQuery : FeedQuery
    {

        public EventQuery()
        : base()
        {
        }

        private DateTime startTime;
        private DateTime endTime;
        private DateTime recurrenceStart;
        private DateTime recurrenceEnd;

        /// <summary>
        ///  Accessor method for StartTime
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime;}
            set { startTime = value;}
        }


        /// <summary>
        ///  Accessor method for EndTime
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime;}
            set { endTime = value;}
        }


        /// <summary>
        ///  Accessor method for RecurrenceStart
        /// </summary>
        public DateTime RecurrenceStart
        {
            get { return recurrenceStart;}
            set { recurrenceStart = value;}
        }


        /// <summary>
        ///  Accessor method for RecurrenceEnd
        /// </summary>
        public DateTime RecurrenceEnd
        {
            get { return recurrenceEnd;}
            set { recurrenceEnd = value;}
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>protected void ParseUri</summary> 
        /// <param name="targetUri">takes an incoming Uri string and parses all the properties out of it</param>
        /// <returns>throws a query exception when it finds something wrong with the input, otherwise returns a baseuri</returns>
        //////////////////////////////////////////////////////////////////////
        protected override Uri ParseUri(Uri targetUri)
        {
            base.ParseUri(targetUri);
            if (targetUri != null)
            {
                char[] deli = { '?', '&'};

                TokenCollection tokens = new TokenCollection(targetUri.Query, deli);
                foreach (String token in tokens)
                {
                    if (token.Length > 0)
                    {
                        char[] otherDeli = { '='};
                        String[] parameters = token.Split(otherDeli, 2);
                        switch (parameters[0])
                        {
                            case "start-min":
                                this.startTime = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "start-max":
                                this.endTime = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "recurrence-expansion-start":
                                this.recurrenceStart = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "recurrence-expansion-end":
                                this.recurrenceEnd = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                        }
                    }
                }
            }
            return this.Uri;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Resets object state to default, as if newly created.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        protected override void Reset()
        {
            base.Reset();
            startTime = endTime = FeedQuery.EmptyDate;
            recurrenceStart = recurrenceEnd = FeedQuery.EmptyDate;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> string => the query part of the URI </returns>
        //////////////////////////////////////////////////////////////////////
        protected override string CalculateQuery()
        {
            string path = base.CalculateQuery();
            StringBuilder newPath = new StringBuilder(path, 2048);

            char paramInsertion;

            if (path.IndexOf('?') == -1)
            {
                paramInsertion = '?';
            }
            else
            {
                paramInsertion = '&';
            }
            if (this.StartTime != FeedQuery.EmptyDate)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat("start-min={0}", Utilities.LocalDateTimeInUTC(this.StartTime));
                paramInsertion = '&';
            }
            if (this.EndTime != FeedQuery.EmptyDate)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat("start-max={0}", Utilities.LocalDateTimeInUTC(this.EndTime));
                paramInsertion = '&';
            }
            if (this.RecurrenceStart != FeedQuery.EmptyDate)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat("recurrence-expansion-start={0}", Utilities.LocalDateTimeInUTC(this.RecurrenceStart)); 
                paramInsertion = '&';
            }
            if (this.RecurrenceEnd != FeedQuery.EmptyDate)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat("recurrence-expansion-end={0}", Utilities.LocalDateTimeInUTC(this.RecurrenceEnd)); 
                paramInsertion = '&';
            }

            return newPath.ToString();
        }
    }
}
