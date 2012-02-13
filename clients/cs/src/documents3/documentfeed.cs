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
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Documents {
    /// <summary>
    /// Google Documents List feed URI takes the following form:
    ///     http://docs.google.com/feeds/documents/visibility/projection
    /// The visibility parameter has two possible values: private and public. 
    /// In almost all cases, your client should use private. 
    /// The projection parameter indicates what information is included in the representation. 
    /// For example, if your client specifies a projection of basic, it's requesting an Atom feed
    ///  without any GData extension elements. 
    /// Currently, in the Documents List feed, only the private/full combination 
    /// for visibility and projection is available. 
    /// </summary>
    public class DocumentsFeed : AbstractFeed {
        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public DocumentsFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
            // todo: add extension elements as appropriate
        }

        /// <summary>
        /// this needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry() {
            return new DocumentEntry();
        }

        /// <summary>
        /// is called after we already handled the custom entry, to handle all 
        /// other potential parsing tasks
        /// </summary>
        /// <param name="e"></param>
        /// <param name="parser">the atom feed parser used</param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser) {
            base.HandleExtensionElements(e, parser);
        }
    }
}
