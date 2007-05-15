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
        public const string ApplicationName = "UpdateSample/1.0.0";  
            
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
            EventFeed calFeed = service.Query(query) as EventFeed;

 			foreach (EventEntry entry in calFeed.Entries)
            {
            	if (entry.Title.Text == "Conference www2006") 
            	{
                	entry.Content.Content = "The conference was fun... ";
                	entry.Update();
                	Console.WriteLine("Updated the Conference entry"); 
                }
            }
        }

    }
}
