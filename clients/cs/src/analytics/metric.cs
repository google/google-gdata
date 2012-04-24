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
    /// GData schema extension describing a metric.
    /// A metric is part of a DataEntry (entry).
    /// One element for each metric in the query.
    /// A metric contains the following attributes:
    /// # name the name of the metric
    /// # type either integer or string
    /// # value the aggregate value for the query for that metric (e.g. 24 for 24 pageviews)
    /// # ci the confidence interval, or range of values likely to include the correct value.
    /// </summary>
    public class Metric : SimpleNameValueAttribute {
        /// <summary>
        /// Constructs an empty Metric instance
        /// </summary>
        public Metric()
            : base(AnalyticsNameTable.XmlMetricElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeConfidenceInterval, null);
            this.Attributes.Add(BaseNameTable.XmlAttributeType, null);
        }

        /// <summary>
        /// default constructor, takes 4 parameters
        /// </summary>
        /// <param name="confidenceInterval">the confidenceInterval property value</param>
        /// <param name="name">the name property value</param>
        /// <param name="type">the type property value</param>
        /// <param name="value">the value property value</param>
        public Metric(String confidenceInterval, String name, String type, String value) :
            base(AnalyticsNameTable.XmlMetricElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeConfidenceInterval, confidenceInterval);
            this.Attributes.Add(BaseNameTable.XmlAttributeType, type);
            this.Value = value;
        }

        /// <summary>Accessor for "confidenceInterval" attribute.</summary> 
        /// <returns> </returns>
        public string ConfidenceInterval {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeConfidenceInterval] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeConfidenceInterval] = value;
            }
        }

        /// <summary>Accessor for "type" attribute.</summary> 
        /// <returns> </returns>
        public string Type {
            get {
                return this.Attributes[BaseNameTable.XmlAttributeType] as string;
            }
            set {
                this.Attributes[BaseNameTable.XmlAttributeType] = value;
            }
        }
    }
}
