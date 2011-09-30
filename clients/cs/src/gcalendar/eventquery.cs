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
    /// <summary>Enum to describe the different sort orders
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public enum CalendarSortOrder
    {
        /// <summary> do not create the parameter, do whatever the server does</summary>
        serverDefault,
        /// <summary>sort in ascending order</summary>
        ascending,                       
        /// <summary>sort in descending order</summary>
        descending
    }
    /////////////////////////////////////////////////////////////////////////////


    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of FeedQuery, to create a Calendar query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class EventQuery : FeedQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public EventQuery()
        : base()
        {
            this.sortOrder = CalendarSortOrder.serverDefault; 
            this.singleEvents = false;
            this.futureEvents = false;
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public EventQuery(string queryUri)
        : base(queryUri)
        {
            this.sortOrder = CalendarSortOrder.serverDefault;
            this.singleEvents = false;
            this.futureEvents = false;
        }

        private DateTime startTime;
        private DateTime endTime;
        private DateTime recurrenceStart;
        private DateTime recurrenceEnd;
        private CalendarSortOrder sortOrder;
        private bool singleEvents;
        private bool futureEvents;
        private string timeZone;

        //////////////////////////////////////////////////////////////////////
        /// <summary>this indicates the ctz parameter in the query. It
        /// allows you specify the timezone that is used to calculate the 
        /// start/end times for events</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string TimeZone
        {
            get {return this.timeZone;}
            set {this.timeZone = value;}
        }
        // end of accessor public string TimeZone

        //////////////////////////////////////////////////////////////////////
        /// <summary>indicates the sortorder of the returned feed</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public CalendarSortOrder SortOrder
        {
            get {return this.sortOrder;}
            set {this.sortOrder = value;}
        }
        // end of accessor public CalendarSortOrder SortOrder


        //////////////////////////////////////////////////////////////////////
        /// <summary>Decides wether recurring events should be expanded or not</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public bool SingleEvents
        {
            get {return this.singleEvents;}
            set {this.singleEvents = value;}
        }
        // end of accessor public bool SingleEvents


        //////////////////////////////////////////////////////////////////////
        /// <summary>Decides wether events in the past should be returned. Defa</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public bool FutureEvents
        {
            get {return this.futureEvents;}
            set {this.futureEvents = value;}
        }
        // end of accessor public bool FutureEvents



        /// <summary>
        ///  StartTime, this effects the start-min parameter of the uri
        ///  Together with start-max creates a timespan such that only 
        ///  events that are within the timespan are returned. 
        ///  If not specified, default start-min is 1970-01-01.
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime;}
            set { startTime = value;}
        }


        /// <summary>
        ///  EndTime, this effects the start-max parameter of the uri
        ///  Together with start-min creates a timespan such that 
        ///  only events that are within the timespan are returned. 
        ///  If not specified, default start-max is 2031-01-01.
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime;}
            set { endTime = value;}
        }


        /// <summary>
        ///  RecurrenceStart, effects the recurrance-expansion-start parameter
        ///  Specifies beginning of time period for which to expand recurring events.
        /// </summary>
        public DateTime RecurrenceStart
        {
            get { return recurrenceStart;}
            set { recurrenceStart = value;}
        }


        /// <summary>
        ///  RecurrenceEnd, effects the recurrance-expansion-end parameter
        ///  Specifies ending of time period for which to expand recurring events.
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

                string source = HttpUtility.UrlDecode(targetUri.Query);
                TokenCollection tokens = new TokenCollection(source, deli);
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
                            case "singleevents":
                                this.singleEvents = bool.Parse(parameters[1]); 
                                break;
                            case "futureevents":
                                this.futureEvents = bool.Parse(parameters[1]); 
                                break;
                            case "sortorder":
                                this.sortOrder = (CalendarSortOrder) Enum.Parse(typeof(CalendarSortOrder), parameters[1]); 
                                break;
                            case "ctz":
                                this.timeZone = parameters[1];
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
            startTime = endTime = Utilities.EmptyDate;
            recurrenceStart = recurrenceEnd = Utilities.EmptyDate;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> string => the query part of the URI </returns>
        //////////////////////////////////////////////////////////////////////
        protected override string CalculateQuery(string basePath)
        {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path); 

            if (Utilities.IsPersistable(this.StartTime))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "start-min={0}", Utilities.UriEncodeReserved(Utilities.LocalDateTimeInUTC(this.StartTime)));
                paramInsertion = '&';
            }
            if (Utilities.IsPersistable(this.EndTime))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "start-max={0}", Utilities.UriEncodeReserved(Utilities.LocalDateTimeInUTC(this.EndTime)));
                paramInsertion = '&';
            }
            if (Utilities.IsPersistable(this.RecurrenceStart))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "recurrence-expansion-start={0}", Utilities.UriEncodeReserved(Utilities.LocalDateTimeInUTC(this.RecurrenceStart))); 
                paramInsertion = '&';
            }
            if (Utilities.IsPersistable(this.TimeZone))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "ctz={0}", Utilities.UriEncodeReserved(this.TimeZone)); 
                paramInsertion = '&';
            }
            if (Utilities.IsPersistable(this.RecurrenceEnd))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "recurrence-expansion-end={0}", Utilities.UriEncodeReserved(Utilities.LocalDateTimeInUTC(this.RecurrenceEnd))); 
                paramInsertion = '&';
            }
            if (this.sortOrder != CalendarSortOrder.serverDefault)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "sortorder={0}", this.sortOrder.ToString()); 
                paramInsertion = '&';
            }
            if (this.futureEvents)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "futureevents=true"); 
                paramInsertion = '&';
            }
            if (this.singleEvents)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "singleevents=true"); 
                paramInsertion = '&';
            }

            return newPath.ToString();
        }
    }
}
