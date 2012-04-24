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
using System.Xml;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Analytics {
    /// <summary>
    /// GData schema extension describing a dimension.
    /// A dimension is part of a DataEntry (entry).
    /// dxp:dimensionone element for each dimension in the query, 
    /// which includes the name and value of the dimension
    /// </summary>
    public class Dimension : SimpleNameValueAttribute {
        /// <summary>
        /// default constructor for an extended property
        /// </summary>
        public Dimension()
            : base(AnalyticsNameTable.XmlDimensionElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
        }

        /// <summary>
        /// default constructor with a value and a key name
        /// </summary>
        /// <param name="initValue">initial value</param>
        /// <param name="initName">name for the key</param>
        public Dimension(string initName, string initValue)
            : base(AnalyticsNameTable.XmlDimensionElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
            this.Value = initValue;
            this.Name = initName;
        }
    }
}
