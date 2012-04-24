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
    public class Goal : SimpleContainer {

        private Destination destination;
        private Engagement engagement;
        public Goal()
            : base(AnalyticsNameTable.XmlGoalElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeName, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeNumber, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeValue, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeActive, null);

            this.ExtensionFactories.Add(new Destination());
            this.ExtensionFactories.Add(new Engagement());
        }

        public Goal(String name, String number, String value, String active)
            : base(AnalyticsNameTable.XmlSegmentElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeName, name);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeNumber, number);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeValue, value);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeActive, active);

            this.ExtensionFactories.Add(new Destination());
            this.ExtensionFactories.Add(new Engagement());
        }

        /// <summary>Accessor for "name" attribute.</summary> 
        /// <returns> </returns>
        public string Name {
            get {
                return this.Attributes[BaseNameTable.XmlName] as string;
            }
            set {
                this.Attributes[BaseNameTable.XmlName] = value;
            }
        }

        /// <summary>Accessor for "Number" attribute.</summary> 
        /// <returns> </returns>
        public string Number {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeNumber] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeNumber] = value;
            }
        }

        /// <summary>Accessor for "Active" attribute.</summary> 
        /// <returns> </returns>
        public string Active {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeActive] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeActive] = value;
            }
        }

        /// <summary>Accessor for "value" attribute.</summary> 
        /// <returns> </returns>
        public string Value {
            get {
                return this.Attributes[BaseNameTable.XmlValue] as string;
            }
            set {
                this.Attributes[BaseNameTable.XmlValue] = value;
            }
        }

        /// <summary>Accessor for "Destination" sub-element.</summary> 
        /// <returns> </returns>
        public Destination Destination {
            get {
                if (destination == null) {
                    destination = FindExtension(AnalyticsNameTable.XmlDestinationElement,
                        AnalyticsNameTable.gaNamespace) as Destination;
                }

                return destination;
            }
        }

        /// <summary>Accessor for "Engagement" sub-element.</summary> 
        /// <returns> </returns>
        public Engagement Engagement {
            get {
                if (engagement == null) {
                    engagement = FindExtension(AnalyticsNameTable.XmlEngagementElement,
                        AnalyticsNameTable.gaNamespace) as Engagement;
                }
                return engagement;
            }
        }
    }
}
