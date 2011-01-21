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
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Spreadsheets
{
    /// <summary>
    /// A subclass of DocumentQuery, to create a Spreadsheets worksheet query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public class WorksheetQuery : DocumentQuery
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public WorksheetQuery(string key, string visibility, string projection) 
        : base("https://spreadsheets.google.com/feeds/worksheets/"
               + key + "/" + visibility + "/" + projection)
        {
        }

        /// <summary>
        /// Constructor - Sets the base URI
        /// </summary>
        public WorksheetQuery(string baseUri, string key, string visibility, string projection)
        : base(baseUri + "/" + key + "/" + visibility + "/" + projection)
        {
        }

        /// <summary>
        /// Constructor - Sets the base URI including key, visibility, and projection
        /// </summary>
        public WorksheetQuery(string baseUri) : base(baseUri)
        {
        }
    }
}
