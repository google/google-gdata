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
    /// <summary>
    /// GData schema extension describing a Segment.
    /// Segment is part of Data Feed 
    /// </summary>    
    /// 
    public class Segment : SimpleContainer {

        private Definition definition;

        public Segment()
            : base(AnalyticsNameTable.XmlSegmentElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeId, null);
            this.ExtensionFactories.Add(new Definition());
        }

        public Segment(String name, String id)
            : base(AnalyticsNameTable.XmlSegmentElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
            this.Attributes.Add(BaseNameTable.XmlName, name);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeId, id);
            this.ExtensionFactories.Add(new Definition());
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

        /// <summary>Accessor for "name" attribute.</summary> 
        /// <returns> </returns>
        public string Id {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributeId] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributeId] = value;
            }
        }

        /// <summary>Accessor for "definition" attribute.</summary> 
        /// <returns> </returns>
        public Definition Definition {
            get {
                if (definition == null) {
                    definition = FindExtensions<Definition>(AnalyticsNameTable.XmlDefinitionElement,
                        AnalyticsNameTable.gAnalyticsNamspace)[0];
                }

                return definition;
            }
        }
    }
}
