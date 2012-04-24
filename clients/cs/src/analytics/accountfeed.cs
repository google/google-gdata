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
/* Created by Morten Christensen, elpadrinodk@gmail.com, http://blog.sitereactor.dk */
using System;
using Google.GData.Client;
using System.Collections.Generic;

namespace Google.GData.Analytics {
    /// <summary>
    /// This is the Google Analytics Account feed that lets you access
    /// the analytics account you own.
    /// </summary>
    public class AccountFeed : AbstractFeed {

        private List<Segment> segments;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public AccountFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
            AddExtension(new Segment());
        }

        /// <summary>
        /// This needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry() {
            return new AccountEntry();
        }

        /// <summary>
        /// Is called after we already handled the custom entry, to handle all 
        /// other potential parsing tasks
        /// </summary>
        /// <param name="e"></param>
        /// <param name="parser">the atom feed parser used</param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e,
            AtomFeedParser parser) {
            base.HandleExtensionElements(e, parser);
        }

        /// <summary>
        /// This field controls the segments.
        /// </summary>
        public List<Segment> Segments {
            get {
                if (segments == null) {
                    segments = FindExtensions<Segment>(AnalyticsNameTable.XmlSegmentElement,
                        AnalyticsNameTable.gAnalyticsNamspace);
                }
                return segments;
            }
        }
    }
}
