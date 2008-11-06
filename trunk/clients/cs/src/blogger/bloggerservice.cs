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
using Google.GData.Client;


namespace Google.GData.Blogger {

    /// <summary>
    /// The CalendarService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
    /// Google Calendar data API.
    /// </summary>
    public class BloggerService : Service
    {
        /// <summary>This service's User-Agent string</summary> 
        /// <summary>The Calendar service's name</summary> 
        public const string Service = "blogger";

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public BloggerService(string applicationName) : base(BloggerService.Service, applicationName)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed); 
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public BloggerFeed Query(BloggerQuery feedQuery) 
        {
            return base.Query(feedQuery) as BloggerFeed;
        }


         /// <summary>
        /// by default all services now use version 1 for the protocol.
        /// this needs to be overridden by a service to specify otherwise. 
        /// YouTube uses version 2
        /// </summary>
        /// <returns></returns>
        protected override void InitVersionInformation()
        {
             this.ProtocolMajor = VersionDefaults.VersionTwo;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>eventchaining. We catch this by from the base service, which 
        /// would not by default create an atomFeed</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Calendar Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }
            e.Feed = new BloggerFeed(e.Uri, e.Service);
        }
        /////////////////////////////////////////////////////////////////////////////
   }
}
