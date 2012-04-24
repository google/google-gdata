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
    public class Step : SimpleElement {
        public Step()
            : base(AnalyticsNameTable.XmlStepElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(BaseNameTable.XmlName, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeNumber, null);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributePath, null);
        }

        public Step(String name, String number, String path)
            : base(AnalyticsNameTable.XmlStepElement,
            AnalyticsNameTable.gaPrefix,
            AnalyticsNameTable.gaNamespace) {
            this.Attributes.Add(BaseNameTable.XmlName, name);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributeNumber, number);
            this.Attributes.Add(AnalyticsNameTable.XmlAttributePath, path);
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

        /// <summary>Accessor for "Path" attribute.</summary> 
        /// <returns> </returns>
        public string Path {
            get {
                return this.Attributes[AnalyticsNameTable.XmlAttributePath] as string;
            }
            set {
                this.Attributes[AnalyticsNameTable.XmlAttributePath] = value;
            }
        }
    }
}

