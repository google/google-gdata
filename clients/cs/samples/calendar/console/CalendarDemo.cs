using System;
using System.Text;

using Google.GData.Calendar;
using Google.GData.Client;
using Google.GData.Extensions;

namespace CalendarDemoConsoleApplication
{
    class CalendarDemo
    {
        private static String userName = "REPLACE WITH YOUR USER NAME";
        private static String userPassword = "REPLCE WITH YOUR PASSWORD";
        private static String feedUri = "REPLACE WITH YOUR FULL PRIVATE FEED";

        /// <summary>
        /// Prints a list of the user's calendars.
        /// </summary>
        /// <param name="service">The authenticated CalendarService object.</param>
        static void PrintUserCalendars(CalendarService service)
        {
            FeedQuery query = new FeedQuery();
            query.Uri = new Uri("http://www.google.com/calendar/feeds/default");

            // Tell the service to query:
            AtomFeed calFeed = service.Query(query);

            Console.WriteLine("Your calendars:");
            Console.WriteLine();
            for (int i = 0; i < calFeed.Entries.Count; i++)
            {
                Console.WriteLine(calFeed.Entries[i].Title.Text);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Prints the titles of all events on the specified calendar.
        /// </summary>
        /// <param name="service">The authenticated CalendarService object.</param>
        /// <param name="feedUri">The feed URI of the calendar to access.</param>
        static void PrintAllEvents(CalendarService service, String feedUri)
        {
            EventQuery myQuery = new EventQuery(feedUri);
            EventFeed myResultsFeed = service.Query(myQuery);

            Console.WriteLine("All events on your calendar:");
            Console.WriteLine();
            for (int i = 0; i < myResultsFeed.Entries.Count; i++)
            {
                Console.WriteLine(myResultsFeed.Entries[i].Title.Text);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Prints the titles of all events matching a full-text query.
        /// </summary>
        /// <param name="service">The authenticated CalendarService object.</param>
        /// <param name="feedUri">The feed URI of the calendar to access.</param>
        /// <param name="queryString">The text for which to query.</param>
        static void FullTextQuery(CalendarService service, String feedUri, String queryString)
        {
            EventQuery myQuery = new EventQuery(feedUri);
            myQuery.Query = queryString;

            EventFeed myResultsFeed = service.Query(myQuery);

            Console.WriteLine("Events matching \"{0}\":", queryString);
            Console.WriteLine();
            for (int i = 0; i < myResultsFeed.Entries.Count; i++)
            {
                Console.WriteLine(myResultsFeed.Entries[i].Title.Text);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Prints the titles of all events in a specified date/time range.
        /// </summary>
        /// <param name="service">The authenticated CalendarService object.</param>
        /// <param name="feedUri">The feed URI of the calendar to access.</param>
        /// <param name="startTime">Start time (inclusive) of events to print.</param>
        /// <param name="endTime">End time (exclusive) of events to print.</param>
        static void DateRangeQuery(CalendarService service, String feedUri,
                                   DateTime startTime, DateTime endTime)
        {
            EventQuery myQuery = new EventQuery(feedUri);
            myQuery.StartTime = startTime;
            myQuery.EndTime = endTime;

            EventFeed myResultsFeed = service.Query(myQuery);

            Console.WriteLine("Matching events from {0} to {1}:", 
                              startTime.ToShortDateString(),
                              endTime.ToShortDateString());
            Console.WriteLine();
            for (int i = 0; i < myResultsFeed.Entries.Count; i++)
            {
                Console.WriteLine(myResultsFeed.Entries[i].Title.Text);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Helper method to create either single-instance or recurring events.
        /// For simplicity, some values that might normally be passed as parameters
        /// (such as author name, email, etc.) are hard-coded.
        /// </summary>
        /// <param name="service">The authenticated CalendarService object.</param>
        /// <param name="entryTitle">Title of the event to create.</param>
        /// <param name="recurData">Recurrence value for the event, or null for
        ///                         single-instance events.</param>
        /// <returns>The newly-created EventEntry on the calendar.</returns>
        static EventEntry CreateEvent(CalendarService service, String entryTitle,
                                     String recurData)
        {
            EventEntry entry = new EventEntry();

            // Set the author of this entry.
            AtomPerson author = new AtomPerson(AtomPersonType.Author);
            author.Name = "Jo March";
            author.Email = "jo@gmail.com";
            entry.Authors.Add(author);

            // Set the title and content of the entry.
            entry.Title.Text = entryTitle;
            entry.Content.Content = "Meet for a quick lesson.";

            // Set a location for the event.
            Where eventLocation = new Where();
            eventLocation.ValueString = "South Tennis Courts";
            entry.Locations.Add(eventLocation);

            // If a recurrence was requested, add it.  Otherwise, set the
            // time (the current date and time) and duration (30 minutes)
            // of the event.
            if (recurData == null) {
                When eventTime = new When();
                eventTime.StartTime = DateTime.Now;
                eventTime.EndTime = eventTime.StartTime.AddMinutes(30);
                entry.Times.Add(eventTime);
            } else {
                Recurrence recurrence = new Recurrence();
                recurrence.Value = recurData;
                entry.Recurrence = recurrence;
            }

            // Send the request and receive the response:
            Uri postUri = new Uri(feedUri);
            AtomEntry insertedEntry = service.Insert(postUri, entry);

            return (EventEntry)insertedEntry;
        }

        /// <summary>
        /// Creates a single-instance event on a calendar.
        /// </summary>
        /// <param name="service">The authenticated CalendarService object.</param>
        /// <param name="entryTitle">Title of the event to create.</param>
        /// <returns>The newly-created EventEntry on the calendar.</returns>
        static EventEntry CreateSingleEvent(CalendarService service, String entryTitle)
        {
            return CreateEvent(service, entryTitle, null);
        }

        /// <summary>
        /// Creates a recurring event on a calendar. In this example, the event
        /// occurs every Tuesday from May 1, 2007 through September 4, 2007. Note
        /// that we are using iCal (RFC 2445) syntax; see http://www.ietf.org/rfc/rfc2445.txt
        /// for more information.
        /// </summary>
        /// <param name="service">The authenticated CalendarService object.</param>
        /// <param name="entryTitle">Title of the event to create.</param>
        /// <returns>The newly-created EventEntry on the calendar.</returns>
        static EventEntry CreateRecurringEvent(CalendarService service, String entryTitle)
        {
            String recurData =
              "DTSTART;VALUE=DATE:20070501\r\n" +
              "DTEND;VALUE=DATE:20070502\r\n" +
              "RRULE:FREQ=WEEKLY;BYDAY=Tu;UNTIL=20070904\r\n";

            return CreateEvent(service, entryTitle, recurData);
        }

        /// <summary>
        /// Updates the title of an existing calendar event.
        /// </summary>
        /// <param name="entry">The event to update.</param>
        /// <param name="newTitle">The new title for this event.</param>
        /// <returns>The updated EventEntry object.</returns>
        static EventEntry UpdateTitle(EventEntry entry, String newTitle)
        {
            entry.Title.Text = newTitle;
            return (EventEntry)entry.Update();
        }

        /// <summary>
        /// Adds a reminder to a calendar event.
        /// </summary>
        /// <param name="entry">The event to update.</param>
        /// <param name="numMinutes">Reminder time, in minutes.</param>
        /// <returns>The updated EventEntry object.</returns>
        static EventEntry AddReminder(EventEntry entry, int numMinutes)
        {
            Reminder reminder = new Reminder();
            reminder.Minutes = numMinutes;
            entry.Reminder = reminder;

            return (EventEntry)entry.Update();
        }

        /// <summary>
        /// Adds an extended property to a calendar event.
        /// </summary>
        /// <param name="entry">The event to update.</param>
        /// <returns>The updated EventEntry object.</returns>
        static EventEntry AddExtendedProperty(EventEntry entry)
        {
            ExtendedProperty property = new ExtendedProperty();
            property.Name = "http://www.example.com/schemas/2005#mycal.id";
            property.Value = "1234";

            entry.ExtensionElements.Add(property);
            
            return (EventEntry)entry.Update();
        }

        static void Main(string[] args)
        {
            CalendarService service = new CalendarService("exampleCo-exampleApp-1");
            service.setUserCredentials(userName, userPassword);

            // Demonstrate retrieving a list of the user's calendars.
            PrintUserCalendars(service);

            // Demonstrate various feed queries.
            PrintAllEvents(service, feedUri);
            FullTextQuery(service, feedUri, "Tennis");
            DateRangeQuery(service, feedUri, new DateTime(2007, 1, 5), new DateTime(2007, 1, 7));

            // Demonstrate creating a single-occurrence event.
            EventEntry singleEvent = CreateSingleEvent(service, "Tennis with Mike");
            Console.WriteLine("Successfully created event {0}", singleEvent.Title.Text);

            // Demonstrate creating a recurring event.
            AtomEntry recurringEvent = CreateRecurringEvent(service, "Tennis with Dan");
            Console.WriteLine("Successfully created recurring event {0}", recurringEvent.Title.Text);

            // Demonstrate updating the event's text.
            singleEvent = UpdateTitle(singleEvent, "Important meeting");
            Console.WriteLine("Event's new title is {0}", singleEvent.Title.Text);

            // Demonstrate adding a reminder.  Note that this will only work on a primary
            // calendar.
            singleEvent = AddReminder(singleEvent, 15);
            Console.WriteLine("Set a {0}-minute reminder for the event.", singleEvent.Reminder.Minutes);

            // Demonstrate adding an extended property.
            AddExtendedProperty(singleEvent);

            // Demonstrate deleting the item.
            singleEvent.Delete();
        }
    }
}
