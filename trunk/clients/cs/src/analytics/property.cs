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
using System;
using System.Xml;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Analytics {
    /// <summary>
    /// GData schema extension describing a property.
    /// Property is part of the feedentry (Data and Account feed).
    /// Is a dxp:property element containing a name attribute with following
    /// possible values:
    /// ga:profileId The profile ID of the source, such as 1174
    /// ga:webPropertyId The web property ID of the source, such as UA-30481-1
    /// ga:accountName The name of the account as it appears in the Analytics interface.
    /// </summary>
    public class Property : SimpleNameValueAttribute {
        /// <summary>
        /// Constructs an empty Property instance
        /// </summary>
        public Property()
            : base(AnalyticsNameTable.XmlPropertyElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
        }

        /// <summary>
        /// default constructor, takes 2 parameters
        /// </summary>
        /// <param name="name">name property value</param>
        /// <param name="value">the value property value</param>
        public Property(String name, String value)
            : base(AnalyticsNameTable.XmlPropertyElement,
            AnalyticsNameTable.gAnalyticsPrefix,
            AnalyticsNameTable.gAnalyticsNamspace) {
            this.Name = name;
            this.Value = value;
        }
    }
}
