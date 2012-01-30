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
using Google.GData.Extensions;

namespace Google.GData.Contacts {
    /// <summary>
    /// The Contacts Data API allows client applications to view and update a user's contacts. 
    /// Contacts are stored in the user's Google Account; most Google services have access 
    /// to the contact list. Your client application can use the Contacts Data API to create 
    /// new contacts, edit or delete existing contacts, and query for contacts that 
    /// match particular criteria.
    /// </summary>
    public class ContactsService : Service {
        /// <summary>The Calendar service name</summary> 
        public const string GContactService = "cp";

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="applicationName">the application name</param>
        public ContactsService(string applicationName)
            : base(GContactService, applicationName) {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public ContactsFeed Query(ContactsQuery feedQuery) {
            return base.Query(feedQuery) as ContactsFeed;
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public GroupsFeed Query(GroupsQuery feedQuery) {
            return base.Query(feedQuery) as GroupsFeed;
        }

        /// <summary>
        /// by default all services now use version 1 for the protocol.
        /// this needs to be overridden by a service to specify otherwise. 
        /// Contacts uses version 3
        /// </summary>
        /// <returns></returns>
        protected override void InitVersionInformation() {
            this.ProtocolMajor = VersionDefaults.VersionThree;
        }

        /// <summary>eventchaining. We catch this by from the base service, which 
        /// would not by default create an atomFeed</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        protected void OnNewFeed(object sender, ServiceEventArgs e) {
            Tracing.TraceMsg("Created new Contacts Feed");
            if (e == null) {
                throw new ArgumentNullException("e");
            }

            if (e.Uri.AbsolutePath.IndexOf("/m8/feeds/groups/") != -1) {
                e.Feed = new GroupsFeed(e.Uri, e.Service);
            } else {
                e.Feed = new ContactsFeed(e.Uri, e.Service);
            }
        }
    }
}
