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

namespace Google.GData.Analytics
{
    /// <summary>
    /// dxp:dataSource: summary information about the Analytics source of the data.
    /// It contains the following extensions:
    /// dxp:tableId  The unique, namespaced profile ID of the source, such as ga:1174
    /// dxp:tableName  The name of the profile as it appears in the Analytics administrative UI
    /// dxp:property
    /// </summary>
    public class SourceEntry : AbstractEntry
    {
        /// <summary>
        /// Constructs a new DataEntry.
        /// </summary>
        public SourceEntry()
            : base()
        {
            this.AddExtension(new Property());
            this.AddExtension(new TableId());
            this.AddExtension(new TableName());
        }

        /// <summary>
        /// Basic method for retrieving Data extension elements.
        /// </summary>
        /// <param name="extension">The name of the extension element to look for</param>
        /// <returns>SimpleAttribute, or NULL if the extension was not found</returns>
        public SimpleAttribute getDataExtension(string extension)
        {
            return FindExtension(extension, AnalyticsNameTable.gAnalyticsNamspace) as SimpleAttribute;
        }

        /// <summary>
        /// Base method for retrieving Data extension element values.
        /// </summary>
        /// <param name="extension">The name of the Data extension element to look for</param>
        /// <returns>value as string, or NULL if the extension was not found</returns>
        public string getAccountExtensionValue(string extension)
        {
            SimpleAttribute e = getDataExtension(extension);
            if (e != null)
            {
                return (string)e.Value;
            }
            return null;
        }

        /// <summary>
        /// Base method for setting Data extension element values.
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <param name="newValue">the new value for this extension element</param>
        /// <returns>SimpleAttribute, either a brand new one, or the one
        /// returned by the service</returns>
        public SimpleElement setDataExtension(string extension, string newValue)
        {
            if (extension == null)
            {
                throw new System.ArgumentNullException("extension");
            }

            SimpleAttribute ele = getDataExtension(extension);
            if (ele == null)
            {
                ele = CreateExtension(extension, AnalyticsNameTable.gAnalyticsNamspace) as SimpleAttribute;
                this.ExtensionElements.Add(ele);
            }

            ele.Value = newValue;

            return ele;
        }

        /// <summary>
        /// This field controls the property.
        /// </summary>
        public Property Property
        {
            get
            {
                return FindExtension(AnalyticsNameTable.XmlPropertyElement,
                                   AnalyticsNameTable.gAnalyticsNamspace) as Property;
            }
            set
            {
                ReplaceExtension(AnalyticsNameTable.XmlPropertyElement,
                                 AnalyticsNameTable.gAnalyticsNamspace, value);
            }
        }

        /// <summary>
        /// This field controls the tableId (ProfileId).
        /// </summary>
        public TableId ProfileId
        {
            get
            {
                return FindExtension(AnalyticsNameTable.XmlTableIdElement,
                                   AnalyticsNameTable.gAnalyticsNamspace) as TableId;
            }
            set
            {
                ReplaceExtension(AnalyticsNameTable.XmlTableIdElement,
                                 AnalyticsNameTable.gAnalyticsNamspace, value);
            }
        }

        /// <summary>
        /// This field controls the tableName.
        /// </summary>
        public TableName TableName
        {
            get
            {
                return FindExtension(AnalyticsNameTable.XmlTableNameElement,
                                   AnalyticsNameTable.gAnalyticsNamspace) as TableName;
            }
            set
            {
                ReplaceExtension(AnalyticsNameTable.XmlTableNameElement,
                                 AnalyticsNameTable.gAnalyticsNamspace, value);
            }
        }
    }
}
