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
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.AccessControl;

namespace Google.GData.Calendar {
    /// <summary>
    /// The CalendarService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
    /// Google Calendar data API.
    /// </summary>
    public class CalendarService : Service {
        /// <summary>The Calendar service's name</summary>
        public const string GCalendarService = "cl";

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public CalendarService(string applicationName)
            : base(GCalendarService, applicationName) {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public EventFeed Query(EventQuery feedQuery) {
            return base.Query(feedQuery) as EventFeed;
        }

        /// <summary>
        /// overloaded to create typed version of Query for Calendar feeds
        /// </summary>
        /// <param name="calQuery">The query object for searching a calendar feed.</param>
        /// <returns>CalendarFeed of the returned calendar entries.</returns>
        public CalendarFeed Query(CalendarQuery calQuery) {
            return base.Query(calQuery) as CalendarFeed;
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public AclFeed Query(AclQuery feedQuery) {
            return base.Query(feedQuery) as AclFeed;
        }

        /// <summary>
        /// by default all services now use version 1 for the protocol.
        /// this needs to be overridden by a service to specify otherwise.
        /// Calendar uses version 2
        /// </summary>
        /// <returns></returns>
        protected override void InitVersionInformation() {
            this.ProtocolMajor = VersionDefaults.VersionTwo;
        }

        /// <summary>eventchaining. We catch this by from the base service, which
        /// would not by default create an atomFeed</summary>
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        /// <returns> </returns>
        protected void OnNewFeed(object sender, ServiceEventArgs e) {
            Tracing.TraceMsg("Created new Calendar Feed");
            if (e == null) {
                throw new ArgumentNullException("e");
            }

            if (e.Uri.AbsoluteUri.IndexOf("/acl/") != -1) {
                e.Feed = new AclFeed(e.Uri, e.Service);
            } else if ((e.Uri.AbsoluteUri.IndexOf("/allcalendars/") != -1) ||
                       (e.Uri.AbsoluteUri.IndexOf("/owncalendars/") != -1)) {
                e.Feed = new CalendarFeed(e.Uri, e.Service);
            } else {
                e.Feed = new EventFeed(e.Uri, e.Service);
            }
        }
    }
}
