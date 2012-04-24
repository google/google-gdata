/* Copyright (c) 2010 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Analytics;
using Google.GData.Extensions;
using Google.GData.Client;

namespace Google.GData.Analytics {
    public class Engagement : SimpleElement {
        public Engagement()
            : base(AnalyticsNameTable.XmlEngagementElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeComparison, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeThresholdValue, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeType, null);
        }

        public Engagement(String comparison, String thresholdValue, String type)
            : base(AnalyticsNameTable.XmlEngagementElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeComparison, comparison);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeThresholdValue, thresholdValue);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeType, type);
        }

        /// <summary>Accessor for "Comparison" attribute.</summary> 
        /// <returns> </returns>
        public string Comparison {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeComparison] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeComparison] = value;
            }
        }

        /// <summary>Accessor for "thresholdValue" attribute.</summary> 
        /// <returns> </returns>
        public string Threshold {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeThresholdValue] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeThresholdValue] = value;
            }
        }

        /// <summary>Accessor for "type" attribute.</summary> 
        /// <returns> </returns>
        public string Type {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeType] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeType] = value;
            }
        }

    }
}
