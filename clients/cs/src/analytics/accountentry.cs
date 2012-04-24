/* Copyright (c) 2006 Google Inc.
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
/* Created by Morten Christensen, elpadrinodk@gmail.com, http://blog.sitereactor.dk */
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Analytics {
    /// <summary>
    /// AccountEntry API customization class for defining entries in an account feed.
    /// </summary>
    public class AccountEntry : AbstractEntry {
        /// <summary>
        /// Lazy loading for the properties and tableId.
        /// </summary>
        private List<Property> properties;
        private List<CustomVariable> customVariables;
        private List<Goal> goals;
        private TableId tableId;

        /// <summary>
        /// Constructs a new AccountEntry instance
        /// </summary>
        public AccountEntry()
            : base() {
            this.AddExtension(new Property());
            this.AddExtension(new TableId());
            this.AddExtension(new CustomVariable());
            this.AddExtension(new Goal());
        }

        /// <summary>
        /// This field controls the properties.
        /// </summary>
        public List<Property> Properties {
            get {
                if (properties == null) {
                    properties = FindExtensions<Property>(AnalyticsNameTable.XmlPropertyElement,
                        AnalyticsNameTable.gAnalyticsNamspace);
                }
                return properties;
            }
        }

        /// <summary>
        /// searches through the property list to find a specific one
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string FindPropertyValue(string name) {
            foreach (Property p in this.Properties) {
                if (p.Name == name) {
                    return p.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// This field controls the tableId (ProfileId).
        /// </summary>
        public TableId ProfileId {
            get {
                if (tableId == null) {
                    tableId = FindExtension(AnalyticsNameTable.XmlTableIdElement,
                        AnalyticsNameTable.gAnalyticsNamspace) as TableId;
                }
                return tableId;
            }
        }

        /// <summary>
        /// This field controls the Custom Variables.
        /// </summary>
        public List<CustomVariable> CustomVariables {
            get {
                if (customVariables == null) {
                    customVariables = FindExtensions<CustomVariable>(AnalyticsNameTable.XmlCustomVariableElement,
                        AnalyticsNameTable.gaNamespace);
                }
                return customVariables;
            }
        }

        /// <summary>
        /// This field controls the goals.
        /// </summary>
        public List<Goal> Goals {
            get {
                if (goals == null) {
                    goals = FindExtensions<Goal>(AnalyticsNameTable.XmlGoalElement, AnalyticsNameTable.gaNamespace);
                }
                return goals;
            }
        }
    }
}
