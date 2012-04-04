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
#define TRACE

using System;
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.AccessControl {
    /// <summary>
    /// A subclass of FeedQuery, to create an ACL query URI.
    /// currently only exists to allow a Service. Query overload that 
    /// creates an ACL feed
    /// </summary> 
    public class AclQuery : FeedQuery {
        /// <summary>default with no params</summary> 
        public AclQuery()
            : base() {
        }

        /// <summary>constructor taking a base URI constructor.</summary> 
        public AclQuery(string baseUri)
            : base(baseUri) {
        }
    }

    /// <summary>
    /// AccessControlFeed customization class
    /// the AccessControl feed does not expose anything in addition 
    /// to the base feed, so the only customization is the creation of
    /// AccessControlEntries
    /// </summary>
    public class AclFeed : AbstractFeed {

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feed</param>
        /// <param name="service">the Service to use</param>
        public AclFeed(Uri uriBase, IService service)
            : base(uriBase, service) {
        }

        /// <summary>
        /// returns a new entry for this feed
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry() {
            Tracing.TraceMsg("Construcing new AclEntry");
            return new AclEntry();
        }

        /// <summary>
        /// gets called after we already handled the custom entry, to handle all 
        /// other potential parsing tasks
        /// </summary>
        /// <param name="e">the Event arguments</param>
        /// <param name="parser">the atom feed parser used</param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser) {
            Tracing.TraceMsg("\t HandleExtensionElements for Access Control feed called");
        }
    }
}
