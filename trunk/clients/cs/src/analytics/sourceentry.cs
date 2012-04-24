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
using Google.GData.Client;
using Google.GData.Extensions;
using System.Collections.Generic;

namespace Google.GData.Analytics {
    /// <summary>
    /// dxp:dataSource: summary information about the Analytics source of the data.
    /// It contains the following extensions:
    /// dxp:tableId The unique, namespaced profile ID of the source, such as ga:1174
    /// dxp:tableName The name of the profile as it appears in the Analytics administrative UI
    /// dxp:property
    /// </summary>
    public class DataSource : SimpleContainer {

        private List<Property> properties;

        /// <summary>
        /// Constructs a new DataEntry.
        /// </summary>
        public DataSource()
            : base(AnalyticsNameTable.XmlDataSourceElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
            this.ExtensionFactories.Add(new Property());
            this.ExtensionFactories.Add(new TableId());
            this.ExtensionFactories.Add(new TableName());
        }

        /// <summary>
        /// This field controls the tableId (ProfileId).
        /// </summary>
        public string TableId {
            get {
                TableId t = FindExtension(AnalyticsNameTable.XmlTableIdElement,
                    AnalyticsNameTable.gAnalyticsNamspace) as TableId;

                return t != null ? t.Value : null;
            }
        }

        /// <summary>
        /// This field controls the tableName.
        /// </summary>
        public string TableName {
            get {
                TableName t = FindExtension(AnalyticsNameTable.XmlTableNameElement,
                    AnalyticsNameTable.gAnalyticsNamspace) as TableName;

                return t != null ? t.Value : null;
            }
        }

        /// <summary>
        /// This field controls the properties.
        /// </summary>
        public List<Property> Properties {
            get {
                if (properties == null) {
                    properties = FindExtensions<Property>(AnalyticsNameTable.XmlPropertyElement, AnalyticsNameTable.gAnalyticsNamspace);
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
        /// The numeric id of the profile
        /// </summary>
        /// <returns></returns>
        public string ProfileId {
            get {
                return this.FindPropertyValue("ga:profileId");
            }
        }

        /// <summary>
        /// The web property ID associated with the profile.
        /// </summary>
        /// <returns></returns>
        public string WebPropertyId {
            get {
                return this.FindPropertyValue("ga:webPropertyId");
            }
        }

        /// <summary>
        /// The name of the account associated with the profile
        /// </summary>
        /// <returns></returns>
        public string AccountName {
            get {
                return this.FindPropertyValue("ga:accountName");
            }
        }
    }
}
