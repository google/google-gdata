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
using System.Collections.Specialized;


namespace Google.GData.Health {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// The Google Health Data API allows applications to view and send health data in the form of Google Data feeds. 
    /// The HealthService class encapsulates authentication to the Google HealthService.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class HealthService : Service
    {
       
        /// <summary>The Calendar service's name</summary> 
        public const string ServiceName = "health";

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public HealthService(string applicationName) : base(ServiceName, applicationName)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed); 
        }
   
        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public HealthFeed Query(HealthQuery feedQuery) 
        {
            return base.Query(feedQuery) as HealthFeed;
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
            Tracing.TraceMsg("Created new YouTube Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }

            e.Feed = new HealthFeed(e.Uri, e.Service);
        }
        /////////////////////////////////////////////////////////////////////////////
    }
}
