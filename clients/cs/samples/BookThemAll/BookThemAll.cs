//
// Copyright 2011 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Text;
using System.Collections;
using Google.GData.Apps;
using Google.GData.Client;
using Google.GData.Calendar;
using Google.GData.Extensions;
using Google.GData.Extensions.Apps;

namespace BookThemAll
{
    public class NoAvailableSlotWithinLimitException : Exception
    {
    }

    public class BookThemAllSample
    {
        protected AppsService apps;
        protected CalendarService calendar;
        protected static string feedUrl = "https://www.google.com/calendar/feeds/{0}/private/full";
        private string admin;

        public BookThemAllSample(string domain, string admin, string password)
        {
            apps = new AppsService(domain, admin, password);
            calendar = new CalendarService("BookThemAll");
            calendar.setUserCredentials(admin, password);
            this.admin = admin;
        }

        public AtomEntryCollection RetrieveAllGroupMembers(string group)
        {
            return apps.Groups.RetrieveAllMembers(group).Entries;
        }

        public bool IsUserAvailable(string user, DateTime datetime)
        {
            EventQuery query = new EventQuery(string.Format(feedUrl, user));
            query.StartTime = datetime;      
            query.EndTime = datetime + new System.TimeSpan(0, 0, 30, 0);
            EventFeed feed = calendar.Query(query);
            return feed.Entries.Count == 0;
        }

        public bool AreUsersAvailable(AtomEntryCollection users, DateTime datetime)
        {
            foreach (AppsExtendedEntry entry in users)
            {
                string user = entry.getPropertyValueByName("memberId");
                bool isAvailable = IsUserAvailable(user, datetime);
                if (!isAvailable)
                {
                    return false;
                }
            }
            return true;
        }

        public DateTime FindNextAvailableTimeSlot(AtomEntryCollection users, DateTime limit)
        {
            DateTime nextHour = NextHour();
            while (nextHour < limit)
            {
                if (AreUsersAvailable(users, nextHour))
                {
                    return nextHour;
                }
                nextHour += new System.TimeSpan(0, 0, 30, 0);
            }
            throw new NoAvailableSlotWithinLimitException();
        }

        public DateTime NextHour()
        {
            DateTime now = DateTime.Now;
            return now + new System.TimeSpan(0, 0, 59-now.Minute, 59-now.Second, 1000-now.Millisecond);
        }

        public EventEntry CreateEvent(string title, DateTime date, AtomEntryCollection users)
        {
            EventEntry entry = new EventEntry();
            entry.Title.Text = title;
            entry.Content.Content = title + "Content";
            When eventTime = new When(date, date + new System.TimeSpan(0, 0, 30, 0));
            entry.Times.Add(eventTime);
            foreach (AppsExtendedEntry user in users)
            {
                string member = user.getPropertyValueByName("memberId");
                Who who = new Who();
                who.Email = member;
                who.Rel = Who.RelType.EVENT_ATTENDEE;
                entry.Participants.Add(who);
            }
            Uri postUri = new Uri(string.Format(feedUrl, admin));
            return calendar.Insert(postUri, entry);
        }

        public DateTime Run(string group)
        {
            DateTime now = DateTime.Now;
            AtomEntryCollection users =  RetrieveAllGroupMembers(group);
            DateTime nextSlot = FindNextAvailableTimeSlot(users, now + new System.TimeSpan(30, 0, 0, 0));
            CreateEvent("Meeting", nextSlot, users);
            return nextSlot;
        }

        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: BookThemAll <domain> <admin> <password> <group>");
            }
            else
            {
                // Workaround Certificate validation errors
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                  delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                           System.Security.Cryptography.X509Certificates.X509Chain chain,
                           System.Net.Security.SslPolicyErrors sslPolicyErrors)
                  {
                    return true;
                  };
                string domain = args[0];
                string admin = args[1];
                string password = args[2];
                string group = args[3];
                BookThemAllSample bookthemall = new BookThemAllSample(domain, admin, password);
                Console.WriteLine("Event created at: {0}", bookthemall.Run(group));
            }
        }
    }
}
