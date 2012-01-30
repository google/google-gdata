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
    ///      A contact feed is a private read/write feed that can be used to view and manage a user's
    ///      contacts. The URI for the feed is as follows:
    ///      http://www.google.com/m8/feeds/contacts/userID/base
    ///
    ///      For example, the contacts feed for user liz@gmail.com would have the following URI:
    ///      http://www.google.com/m8/feeds/contacts/liz%40gmail.com/base
    ///
    ///      Since the contact feed is private, you can access it only by using an authenticated
    ///      request. That is, the request must contain an authentication token for the user whose
    ///      contacts you want to retrieve.
    /// </summary>
    public class ContactsFeed : AbstractFeed {
        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public ContactsFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
        }

        /// <summary>
        ///  default constructor with user name
        /// </summary>
        /// <param name="username">the username for the contacts feed</param>
        /// <param name="iService">the Service to use</param>
        public ContactsFeed(String username, IService iService)
            : base(new Uri(ContactsQuery.CreateContactsUri(username)), iService) {
        }
        /// <summary>
        /// this needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry() {
            return new ContactEntry();
        }

        /// <summary>
        /// gets called after we already handled the custom entry, to handle all 
        /// other potential parsing tasks
        /// </summary>
        /// <param name="e"></param>
        /// <param name="parser">the atom feed parser used</param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser) {
            base.HandleExtensionElements(e, parser);
        }
    }

    /// <summary>
    ///      A groups feed is a private read/write feed that can be used to view and manage a user's
    ///      groups. The URI for the feed is as follows:
    ///      http://www.google.com/m8/feeds/groups/userID/base
    ///
    ///      For example, the contacts feed for user liz@gmail.com would have the following URI:
    ///      http://www.google.com/m8/feeds/groups/liz%40gmail.com/base
    ///
    ///      Since the groups feed is private, you can access it only by using an authenticated
    ///      request. That is, the request must contain an authentication token for the user whose
    ///      contacts you want to retrieve.
    /// </summary>
    public class GroupsFeed : AbstractFeed {
        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public GroupsFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
        }

        /// <summary>
        ///  default constructor with user name
        /// </summary>
        /// <param name="username">the username for the contacts feed</param>
        /// <param name="iService">the Service to use</param>
        public GroupsFeed(String username, IService iService)
            : base(new Uri(ContactsQuery.CreateGroupsUri(username)), iService) {
        }
        /// <summary>
        /// this needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry() {
            return new GroupEntry();
        }

        /// <summary>
        /// gets called after we already handled the custom entry, to handle all 
        /// other potential parsing tasks
        /// </summary>
        /// <param name="e"></param>
        /// <param name="parser">the atom feed parser used</param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser) {
            base.HandleExtensionElements(e, parser);
        }
    }
}
