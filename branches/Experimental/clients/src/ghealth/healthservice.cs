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


using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net; 
using Google.GData.Client;
using Google.GData.Extensions;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ASTM.Org.CCR;

namespace Google.GData.Health 
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// The Google Health Data API allows applications to view and send health data in the form of Google Data feeds. 
    /// The HealthService class encapsulates authentication to the Google HealthService.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class HealthService : BaseHealthService
    {
        /// <summary>The Calendar service's name</summary> 
        public const string ServiceName = "health";
		/// <summary>The base Google Health Feeds address.</summary>
		public const string BaseAddress = "https://www.google.com/health/feeds/";
		/// <summary>The Google Health Profile list feed.</summary>
		public const string ProfileListFeed = BaseAddress + "profile/list/";
		/// <summary>The Google Health Profile UI feed</summary>
		public const string ProfileFeed = BaseAddress + "profile/ui/";
		/// <summary>The Google Health Register UI feed</summary>
		public const string RegisterFeed = BaseAddress + "register/ui/";
		/// <summary>The Google Health Default Profile UI feed</summary>
		public const string DefaultProfileFeed = BaseAddress + "profile/default";
		/// <summary>The Google Health Default Register UI feed</summary>
		public const string DefaultRegisterFeed = BaseAddress + "register/default";

        /// <summary>
        /// Constructs the Google Health Service using the 
		/// primary Health Feeds address.
        /// </summary>
        /// <param name="applicationName">The name of the application leveraging the service.</param>
		public HealthService(string applicationName) : base(applicationName, 
			BaseAddress, ProfileListFeed, ProfileFeed, RegisterFeed, 
			DefaultProfileFeed, DefaultRegisterFeed) { }
    }
}
