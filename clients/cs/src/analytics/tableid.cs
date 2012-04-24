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
using Google.GData.Extensions;

namespace Google.GData.Analytics {
    /// <summary>
    /// GData schema extension describing a tableId.
    /// Part of a feedentry (account feed entry or DataEntry SourceEntry/dxp:dataSource).
    /// dxp:tableId The unique, namespaced ID to be used when requesting data from a profile.
    /// </summary>
    public class TableId : SimpleElement {
        /// <summary>
        /// Constructs an empty TableId instance
        /// </summary>
        public TableId()
            : base(AnalyticsNameTable.XmlTableIdElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
        }

        /// <summary>
        /// default constructor, takes 1 parameters
        /// </summary>
        /// <param name="value">the value property value</param>
        public TableId(String value)
            : base(AnalyticsNameTable.XmlTableIdElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace,
            value) {
        }
    }
}
