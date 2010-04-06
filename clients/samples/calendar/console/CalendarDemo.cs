using System;
using System.Text;

using Google.GData.AccessControl;
using Google.GData.Calendar;
using Google.GData.Client;
using Google.GData.Extensions;

namespace CalendarDemoConsoleApplication
{
    class CalendarDemo
    {
        private static String userName, userPassword, feedUri;

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
        static void PrintAllEvents(CalendarService service)
        {
            EventQuery myQuery = new EventQuery(feedUri);
            EventFeed myResultsFeed = service.Query(myQuery) as EventFeed;

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
        /// <param name="queryString">The text for which to query.</param>
        static void FullTextQuery(CalendarService service, String queryString)
        {
            EventQuery myQuery = new EventQuery(feedUri);
            myQuery.Query = queryString;

            EventFeed myResultsFeed = service.Query(myQuery) as EventFeed;

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
        /// <param name="startTime">Start time (inclusive) of events to print.</param>
        /// <param name="endTime">End time (exclusive) of events to print.</param>
        static void DateRangeQuery(CalendarService service, DateTime startTime, DateTime endTime)
        {
            EventQuery myQuery = new EventQuery(feedUri);
            myQuery.StartTime = startTime;
            myQuery.EndTime = endTime;

            EventFeed myResultsFeed = service.Query(myQuery) as EventFeed;

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

        /// <summary>
        /// Retrieves and prints the access control lists of all
        /// of the authenticated user's calendars.
        /// </summary>
        /// <param name="service">The authenticated CalendarService object.</param>
        static void RetrieveAcls(CalendarService service)
        {
            FeedQuery query = new FeedQuery();
            query.Uri = new Uri("http://www.google.com/calendar/feeds/default");
            AtomFeed calFeed = service.Query(query);

            Console.WriteLine();
            Console.WriteLine("Sharing permissions for your calendars:");

            // Retrieve the meta-feed of all calendars.
            foreach (AtomEntry calendarEntry in calFeed.Entries)
            {
                Console.WriteLine("Calendar: {0}", calendarEntry.Title.Text);
                AtomLink link = calendarEntry.Links.FindService(
                    AclNameTable.LINK_REL_ACCESS_CONTROL_LIST, null);

                // For each calendar, retrieve its ACL feed.
                if (link != null)
                {
                    AclFeed feed = service.Query(new AclQuery(link.HRef.ToString()));
                    foreach (AclEntry aclEntry in feed.Entries)
                    {
                        Console.WriteLine("\tScope: Type={0} ({1})", aclEntry.Scope.Type,
                            aclEntry.Scope.Value);
                        Console.WriteLine("\tRole: {0}", aclEntry.Role.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Shares a calendar with the specified user.  Note that this method
        /// will not run by default.
        /// </summary>
        /// <param name="service">The authenticated CalendarService object.</param>
        /// <param name="aclFeedUri">the ACL feed URI of the calendar being shared.</param>
        /// <param name="userEmail">The email address of the user with whom to share.</param>
        /// <param name="role">The role of the user with whom to share.</param>
        /// <returns>The AclEntry returned by the server.</returns>
        static AclEntry AddAccessControl(CalendarService service, string aclFeedUri,
            string userEmail, AclRole role)
        {
            AclEntry entry = new AclEntry();

            entry.Scope = new AclScope();
            entry.Scope.Type = AclScope.SCOPE_USER;
            entry.Scope.Value = userEmail;

            entry.Role = role;

            Uri aclUri =
                new Uri("http://www.google.com/calendar/feeds/gdata.ops.test@gmail.com/acl/full");

            AclEntry insertedEntry = service.Insert(aclUri, entry); 
            Console.WriteLine("Added user {0}", insertedEntry.Scope.Value);

            return insertedEntry;
        }

        /// <summary>
        /// Updates a user to have new access permissions over a calendar.
        /// Note that this method will not run by default.
        /// </summary>
        /// <param name="entry">An existing AclEntry representing sharing permissions.</param>
        /// <param name="newRole">The new role (access permissions) for the user.</param>
        /// <returns>The updated AclEntry.</returns>
        static AclEntry UpdateEntry(AclEntry entry, AclRole newRole)
        {
            entry.Role = newRole;
            AclEntry updatedEntry = entry.Update() as AclEntry;

            Console.WriteLine("Updated {0} to have role {1}", updatedEntry.Scope.Value,
                entry.Role.Value);
            return updatedEntry;
        }

        /// <summary>
        /// Deletes a user from a calendar's access control list, preventing
        /// that user from accessing the calendar.  Note that this method will
        /// not run by default.
        /// </summary>
        /// <param name="entry">An existing AclEntry representing sharing permissions.</param>
        static void DeleteEntry(AclEntry entry)
        {
            entry.Delete();
        }

        /// <summary>
        /// Runs the methods above to demonstrate usage of the .NET
        /// client library.  The methods that add, update, or remove
        /// users on access control lists will not run by default.
        /// </summary>
        static void RunSample()
        {
            CalendarService service = new CalendarService("exampleCo-exampleApp-1");
            service.setUserCredentials(userName, userPassword);

            // Demonstrate retrieving a list of the user's calendars.
            PrintUserCalendars(service);

            // Demonstrate various feed queries.
            PrintAllEvents(service);
            FullTextQuery(service, "Tennis");
            DateRangeQuery(service, new DateTime(2007, 1, 5), new DateTime(2007, 1, 7));

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

            // Demonstrate retrieving access control lists for all calendars.
            RetrieveAcls(service);
        }

        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: gcal_demo <username> <password> <feedUri>");
            }
            else
            {
                userName = args[0];
                userPassword = args[1];
                feedUri = args[2];

                RunSample();
            }
        }
    }
}
