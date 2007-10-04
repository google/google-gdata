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
#define USE_TRACING
#define DEBUG

using System;
using System.IO;
using System.Xml; 
using System.Net; 
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Calendar;


namespace Google.GData.Client.Samples
{
    /// <summary>
    /// simple pull app for a calendar
    /// </summary>
    class CalendarSample
    {
        /// <summary>name of this application</summary>
        public const string ApplicationName = "InsertSample/1.0.0";  
            
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //
            // TODO: Add code to start application here
            //
            if (args.Length < 3)
            {
                Console.WriteLine("Not enough parameters. Usage is Sample <uri> <username> <password>");
                return;
            }

            string calendarURI = args[0];
            string userName = args[1];
            string passWord = args[2];

            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService(ApplicationName);

            if (userName != null)
            {
                service.setUserCredentials(userName, passWord);
            }

            query.Uri = new Uri(calendarURI);
            EventFeed calFeed = service.Query(query);

            EventEntry insertedEntry = InsertEvent(calFeed, "Conference www2006",
                "Frank Mantek", DateTime.Now, 
                                DateTime.Now.AddDays(1), 
                                true,
                                "Edinburgh");

            if (insertedEntry != null) 
            {
                DumpEventEntry(insertedEntry);
            }
        }


        private static EventEntry InsertEvent(EventFeed feed, String title, 
            String author, DateTime startTime, DateTime endTime, bool fAllDay,
            String place)
        {
            EventEntry entry = new EventEntry();

            entry.Title = new AtomTextConstruct(
                                AtomTextConstructElementType.Title, 
                                title);
            entry.Authors.Add(new AtomPerson(AtomPersonType.Author, author));
            entry.Published = DateTime.Now;
            entry.Updated = DateTime.Now;


            Where newPlace = new Where();
            newPlace.ValueString = place;
            entry.Locations.Add(newPlace);

            When newTime = new When();
            newTime.StartTime = startTime;
            newTime.EndTime = endTime;
            newTime.AllDay = fAllDay; 
            entry.Times.Add(newTime);

            return feed.Insert(entry) as EventEntry;
        }
        
        private static void DumpEventEntry(EventEntry feedEntry)
        {
            Console.WriteLine("");
            Console.WriteLine("Appointment: " + feedEntry.Title.Text);
            foreach (When when in feedEntry.Times)
            {
                Console.WriteLine("When: " + when.StartTime + " " + 
                    when.EndTime + " " + when.ValueString);
            }
            foreach (Where where in feedEntry.Locations)
            {
                Console.WriteLine("Where: " + where.Rel + " " + 
                        where.Label + " " + where.ValueString);
            }
            foreach (Who who in feedEntry.Participants)
            {
                Console.WriteLine("Who: " + who.Rel + " " + who.ValueString);
                if (who.Attendee_Status != null)
                    Console.WriteLine("Who Status: " + who.Attendee_Status.Value);

                if (who.Attendee_Type != null)
                    Console.WriteLine("Who Type: " + who.Attendee_Type.Value);
            }

            if (feedEntry.EventVisibility != null)
                Console.WriteLine("Visibility: " + feedEntry.EventVisibility.Value);

            if (feedEntry.EventTransparency != null)
                Console.WriteLine("Transparency: " + feedEntry.EventTransparency.Value);

            if (feedEntry.Status != null)
                Console.WriteLine("Status: " + feedEntry.Status.Value);

            if (feedEntry.Recurrence != null)
                Console.WriteLine("Recurrence: " + feedEntry.Recurrence.Value);

            if (feedEntry.Comments != null)
                Console.WriteLine("Comments: " + feedEntry.Comments.FeedLink.Href);

            if (feedEntry.OriginalEvent != null)
            {
                Console.WriteLine("OriginalEvent - ID: " + 
                                    feedEntry.OriginalEvent.IdOriginal);
                Console.WriteLine("OriginalEvent - Href: " + 
                                    feedEntry.OriginalEvent.Href);
                Console.WriteLine("OriginalEvent - startTime: " + 
                                    feedEntry.OriginalEvent.OriginalStartTime.StartTime);
            }

            if (feedEntry.Reminder != null)
            {
                if (feedEntry.Reminder.Days > 0)
                    Console.WriteLine("Reminder - Days: " + 
                                    feedEntry.Reminder.Days.ToString());
                if (feedEntry.Reminder.Hours > 0)
                    Console.WriteLine("Reminder - Hours: " + 
                                    feedEntry.Reminder.Hours.ToString());
                if (feedEntry.Reminder.Minutes > 0)
                    Console.WriteLine("Reminder - Minutes: " + 
                                    feedEntry.Reminder.Minutes.ToString());
                if (feedEntry.Reminder.AbsoluteTime != new DateTime(1, 1, 1))
                    Console.WriteLine("Reminder - AbsoluteTime: " + 
                                    feedEntry.Reminder.AbsoluteTime);
            }
        }

    }
}
