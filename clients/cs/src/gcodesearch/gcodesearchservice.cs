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


namespace Google.GData.CodeSearch 
{

    /// <summary>
    /// The CodeSearchService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
    /// Google Code Search data API.
    /// </summary>
    public class CodeSearchService : Service
    {
        /// <summary>The service's name</summary> 
        public const string GCodeSearchService = "codesearch";

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public CodeSearchService(string applicationName) :
            base(GCodeSearchService,
            applicationName) 
        {
        }

        /// <summary>
        ///  overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The CodeSearchQuery touse</param>
        /// <returns>the retrieved CodeSearchFeed</returns>
        public new CodeSearchFeed Query(FeedQuery feedQuery) 
        {
            Stream feedStream = Query(feedQuery.Uri);
            CodeSearchFeed feed = new CodeSearchFeed(feedQuery.Uri, this);
            feed.Parse(feedStream, AlternativeFormat.Atom);
            feedStream.Close(); 
            return feed;
        }
    }
}

