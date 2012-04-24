/* Copyright (c) 2010 Google Inc.
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
using System.Collections.Generic;
using System.Text;
using Google.GData.Analytics;
using Google.GData.Extensions;
using Google.GData.Client;

namespace Google.GData.Analytics {
    public class Destination : SimpleContainer {

        private List<Step> steps;

        public Destination()
            : base(AnalyticsNameTable.XmlDestinationElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeCaseSensitive, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeExpression, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeMatchType, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeStep1Required, null);
            this.ExtensionFactories.Add(new Step());
        }

        public Destination(String caseSensitive, String expression,
            String matchType, String step1Required)
            : base(AnalyticsNameTable.XmlDestinationElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeCaseSensitive, caseSensitive);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeExpression, expression);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeMatchType, matchType);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeStep1Required, step1Required);
            this.ExtensionFactories.Add(new Step());
        }

        /// <summary>Accessor for "Step" sub-element.</summary> 
        /// <returns> </returns>
        public List<Step> Steps {
            get {
                if (steps == null) {
                    steps = FindExtensions<Step>(AnalyticsNameTable.XmlStepElement, AnalyticsNameTable.gaNamespace);
                }

                return steps;
            }
        }

        /// <summary>Accessor for "caseSensitive" attribute.</summary> 
        /// <returns> </returns>
        public string CaseSensitive {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeCaseSensitive] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeCaseSensitive] = value;
            }
        }

        /// <summary>Accessor for "expression" attribute.</summary> 
        /// <returns> </returns>
        public string Expression {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeExpression] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeExpression] = value;
            }
        }

        /// <summary>Accessor for "matchType" attribute.</summary> 
        /// <returns> </returns>
        public string MatchType {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeMatchType] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeMatchType] = value;
            }
        }

        /// <summary>Accessor for "step1Required" attribute.</summary> 
        /// <returns> </returns>
        public string Step1Required {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeStep1Required] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeStep1Required] = value;
            }
        }
    }
}
