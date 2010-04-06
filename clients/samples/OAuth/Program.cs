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
 * 
 * Author: Andrew Smith <andy@snae.net> 22/11/08
 * 
*/

using System;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Calendar;


namespace OAuthTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // create an OAuth factory to use
                GOAuthRequestFactory requestFactory = new GOAuthRequestFactory("cl", "MyApp");
                requestFactory.ConsumerKey = "CONSUMER_KEY";
                requestFactory.ConsumerSecret = "CONSUMER_SECRET";

                // example of performing a query (use OAuthUri or query.OAuthRequestorId)
                Uri calendarUri = new OAuthUri("http://www.google.com/calendar/feeds/default/owncalendars/full", "USER", "DOMAIN");
                // can use plain Uri if setting OAuthRequestorId in the query
                // Uri calendarUri = new Uri("http://www.google.com/calendar/feeds/default/owncalendars/full");

                CalendarQuery query = new CalendarQuery();
                query.Uri = calendarUri;
                query.OAuthRequestorId = "USER@DOMAIN"; // can do this instead of using OAuthUri for queries

                CalendarService service = new CalendarService("MyApp");
                service.RequestFactory = requestFactory;
                service.Query(query);
                Console.WriteLine("Query Success!");

                // example with insert (must use OAuthUri)
                Uri contactsUri = new OAuthUri("http://www.google.com/m8/feeds/contacts/default/full", "USER", "DOMAIN");

                ContactEntry entry = new ContactEntry();
                EMail primaryEmail = new EMail("someuser@example.com");
                primaryEmail.Primary = true;
                primaryEmail.Rel = ContactsRelationships.IsHome;
                entry.Emails.Add(primaryEmail);

                ContactsService contactsService = new ContactsService("MyApp");
                contactsService.RequestFactory = requestFactory;
                contactsService.Insert(contactsUri, entry); // this could throw if contact exists

                Console.WriteLine("Insert Success!");

                // to perform a batch use
                // service.Batch(batchFeed, new OAuthUri(atomFeed.Batch, userName, domain));

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
