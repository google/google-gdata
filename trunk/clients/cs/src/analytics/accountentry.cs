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
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Analytics
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// AccountEntry API customization class for defining entries in an account feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class AccountEntry : AbstractEntry
    {
        /// <summary>
        /// Lazy loading for the properties and tableId.
        /// </summary>
        private List<Property> properties;
        private TableId tableId;

        /// <summary>
        /// Constructs a new AccountEntry instance
        /// </summary>
        public AccountEntry() : base()
        {
            this.AddExtension(new Property());
            this.AddExtension(new TableId());
        }

        /// <summary>
        /// Basic method for retrieving Account extension elements.
        /// </summary>
        /// <param name="extension">The name of the extension element to look for</param>
        /// <returns>SimpleAttribute, or NULL if the extension was not found</returns>
        public SimpleAttribute getAccountExtension(string extension)
        {
            return FindExtension(extension, AnalyticsNameTable.gAnalyticsNamspace) as SimpleAttribute;
        }

        /// <summary>
        /// Base method for retrieving Account extension element values.
        /// </summary>
        /// <param name="extension">The name of the Account extension element to look for</param>
        /// <returns>value as string, or NULL if the extension was not found</returns>
        public string getAccountExtensionValue(string extension)
        {
            SimpleAttribute e = getAccountExtension(extension);
            if (e != null)
            {
                return (string)e.Value;
            }
            return null;
        }

        /// <summary>
        /// Base method for setting Account extension element values.
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <param name="newValue">the new value for this extension element</param>
        /// <returns>SimpleAttribute, either a brand new one, or the one
        /// returned by the service</returns>
        public SimpleElement setAccountExtension(string extension, string newValue)
        {
            if (extension == null)
            {
                throw new System.ArgumentNullException("extension");
            }

            SimpleAttribute ele = getAccountExtension(extension);
            if (ele == null)
            {
                ele = CreateExtension(extension, AnalyticsNameTable.gAnalyticsNamspace) as SimpleAttribute;
                this.ExtensionElements.Add(ele);
            }

            ele.Value = newValue;

            return ele;
        }

        /// <summary>
        /// This field controls the properties.
        /// </summary>
        public List<Property> Properties
        {
            get
            {
                if (properties == null)
                {
                    properties = FindExtensions<Property>(AnalyticsNameTable.XmlPropertyElement, AnalyticsNameTable.gAnalyticsNamspace);
                }
                return properties;
            }
        }

        /// <summary>
        /// This field controls the tableId (ProfileId).
        /// </summary>
        public TableId ProfileId
        {
            get
            {
                if (tableId == null)
                {
                    tableId = FindExtension(AnalyticsNameTable.XmlTableIdElement, AnalyticsNameTable.gAnalyticsNamspace) as TableId;
                }
                return tableId;
            }
            set
            {
                ReplaceExtension(AnalyticsNameTable.XmlTableIdElement,
                                 AnalyticsNameTable.gAnalyticsNamspace, value);
            }
        }
    }
}
