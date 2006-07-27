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


namespace Google.GData.Calendar {

    /// <summary>
    /// The CalendarService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
    /// Google Calendar data API.
    /// </summary>
    public class CalendarService : Service
    {
        /// <summary>This service's User-Agent string</summary> 
        public const string GCalendarAgent = "GCalendar-CS/1.0.0";
        /// <summary>The Calendar service's name</summary> 
        public const string GCalendarService = "cl";

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public CalendarService(string applicationName) : base(GCalendarService, applicationName, GCalendarAgent)
        {
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEventEntry);
            this.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewEventExtensionElement);

            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
            GDataGAuthRequestFactory factory = (GDataGAuthRequestFactory)this.RequestFactory;
            factory.MethodOverride = true;
        }

        /// <summary>
        /// Sets the credentials of the user to authenticate requests
        /// to the server.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void setUserCredentials(String username, String password)
        {
            this.Credentials = new NetworkCredential(username, password);
        }


        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery touse</param>
        /// <returns>the retrieved EventFeed</returns>
        public new EventFeed Query(FeedQuery feedQuery)
        {
            Stream feedStream = Query(feedQuery.Uri);
            EventFeed feed = new EventFeed(feedQuery.Uri, this);
            feed.Parse(feedStream, AlternativeFormat.Atom);

            return feed;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called when a new event entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnParsedNewEventEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                Tracing.TraceMsg("\t top level event dispatcher - new Event Entry");
                e.Entry = new EventEntry();
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Event handler. Called for an event extension element.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewEventExtensionElement(object sender, ExtensionElementEventArgs e)
        {

            Tracing.TraceCall("received new event extension element notification");
            Tracing.Assert(e != null, "e should not be null");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            Tracing.TraceMsg("\t top level event = new extension");

            if (String.Compare(e.ExtensionElement.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
            {
                // found G namespace
                Tracing.TraceMsg("\t top level event = new Event extension");
                e.DiscardEntry = true;
                AtomFeedParser parser = sender as AtomFeedParser; 


                if (e.Base.XmlName == AtomParserNameTable.XmlFeedElement)
                {
                    EventFeed eventFeed = e.Base as EventFeed;
                    if (eventFeed != null)
                    {
                        eventFeed.parseEvent(e.ExtensionElement, parser);
                    }
                }
                else if (e.Base.XmlName == AtomParserNameTable.XmlAtomEntryElement)
                {
                    EventEntry eventEntry = e.Base as EventEntry;
                    if (eventEntry != null)
                    {
                        eventEntry.parseEvent(e.ExtensionElement, parser);
                    }
                }
            }
        }
    }
}
