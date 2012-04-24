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
    public class CustomVariable : SimpleElement {
        public CustomVariable()
            : base(AnalyticsNameTable.XmlCustomVariableElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeIndex, null);
            this.Attributes.Add(BaseNameTable.XmlName, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeScope, null);
        }

        public CustomVariable(String comparison, String thresholdValue, String type)
            : base(AnalyticsNameTable.XmlCustomVariableElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeIndex, null);
            this.Attributes.Add(BaseNameTable.XmlName, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeScope, null);
        }

        /// <summary>Accessor for "Index" attribute.</summary> 
        /// <returns> </returns>
        public string Index {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeIndex] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeIndex] = value;
            }
        }

        /// <summary>Accessor for "Name" attribute.</summary> 
        /// <returns> </returns>
        public string Name {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeName] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeName] = value;
            }
        }

        /// <summary>Accessor for "Scope" attribute.</summary> 
        /// <returns> </returns>
        public string Scope {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeScope] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeScope] = value;
            }
        }
    }
}
