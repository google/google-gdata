/* Copyright (c) 2010 Google Inc.
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
using System.Collections.Generic;
using System.Text;
using Google.GData.Extensions.Apps;
using Google.GData.Apps;

namespace CalendarResource
{
    /// <summary>
    /// A sample client for Calendar Resource API
    /// </summary>
    class CalendarResourceDemo
    {
        private static string domain;
        private static string adminEmail;
        private static string adminPassword;

        /// <summary>
        /// This console application demonstrates all the Google Apps
        /// Calendar Resource API calls. 
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the domain, args[1] is the admin email address and args[2]
        /// is the admin password. 
        /// 
        /// Example: CalendarResourceDemo example.com admin@example.com admin_password </param>
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Syntax: CalendarResourceDemo <domain> <admin_email> <admin_password>");
            }
            else
            {
                domain = args[0];
                adminEmail = args[1];
                adminPassword = args[2];

                CalendarResourceService service = new CalendarResourceService(domain, "calendarresource-apps-demo");
                service.setUserCredentials(adminEmail, adminPassword);

                RunSample(service);
            }
        }

        private static void RunSample(CalendarResourceService service)
        {
            try
            {
                const String TEST_RESOURCE_ID = "NYV-BUILDING-5-Batman";
                // Create a new CalendarResource
                AppsExtendedEntry entry = service.CreateCalendarResource(
                    TEST_RESOURCE_ID, "Batman", "6 Person VC", "CR");

                Console.WriteLine("Created: "
                    + entry.getPropertyValueByName(AppsCalendarResourceNameTable.resourceId));

                // Retrieve a CalendarResource
                entry = service.RetrieveCalendarResource(TEST_RESOURCE_ID);

                Console.WriteLine("Retrieved: "
                    + entry.getPropertyValueByName(AppsCalendarResourceNameTable.resourceEmail));

                Console.WriteLine("Dscription: "
                    + entry.getPropertyValueByName(AppsCalendarResourceNameTable.resourceDescription));

                Console.WriteLine("Resource Id: "
                    + entry.getPropertyValueByName(AppsCalendarResourceNameTable.resourceId));

                Console.WriteLine("Common name: "
                    + entry.getPropertyValueByName(AppsCalendarResourceNameTable.resourceCommonName));

                //Retrieve all resources
                Console.WriteLine("Retrieving all calendar resources (this may take some time) ..... ");
                AppsExtendedFeed feed = service.RetrieveAllCalendarResources();
                Console.WriteLine("Retrieved Entries Count: " + feed.Entries.Count);

                foreach (AppsExtendedEntry resourceEntry in feed.Entries)
                {
                    Console.WriteLine("Resource Emails: "
                        + entry.getPropertyValueByName(AppsCalendarResourceNameTable.resourceEmail));
                }

                //Delete a resource
                service.DeleteCalendarResource(TEST_RESOURCE_ID);
                Console.WriteLine("Deleted: " + TEST_RESOURCE_ID);
                Console.Read();

            }
            catch (AppsException a)
            {
                Console.WriteLine("A Google Apps error occurred.");
                Console.WriteLine();
                Console.WriteLine("Error code: {0}", a.ErrorCode);
                Console.WriteLine("Invalid input: {0}", a.InvalidInput);
                Console.WriteLine("Reason: {0}", a.Reason);
            }
        }
    }
}
