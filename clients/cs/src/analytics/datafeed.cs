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
using Google.GData.Extensions;
using System.Collections.Generic;

namespace Google.GData.Analytics {
    /// <summary>DataFeed, entry
    /// Returns DataEntry containing:
    /// id, updated, title, dxp:dimension, dxp:metric.
    /// Added Extension dxp:aggregates, which contains a list of dxp:metric.
    /// </summary>
    public class DataFeed : AbstractFeed {

        private List<Segment> segments;

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public DataFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
            AddExtension(new Aggregates());
            AddExtension(new DataSource());
            AddExtension(new Segment());

        }

        /// <summary>
        /// This needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry() {
            return new DataEntry();
        }

        /// <summary>
        /// returns the Aggregates object for the DataFeed. 
        /// </summary>
        /// <returns></returns>
        public Aggregates Aggregates {
            get { return FindExtension(AnalyticsNameTable.XmlAggregatesElement, AnalyticsNameTable.gAnalyticsNamspace) as Aggregates; }
        }

        /// <summary>
        /// returns the DataSource object for the DataFeed. 
        /// </summary>
        /// <returns></returns>
        public DataSource DataSource {
            get { return FindExtension(AnalyticsNameTable.XmlDataSourceElement, AnalyticsNameTable.gAnalyticsNamspace) as DataSource; }
        }

        /// <summary>
        /// This field controls the segments.
        /// </summary>
        public List<Segment> Segments {
            get {
                if (segments == null) {
                    segments = FindExtensions<Segment>(AnalyticsNameTable.XmlSegmentElement, AnalyticsNameTable.gAnalyticsNamspace);
                }
                return segments;
            }
        }
    }
}
