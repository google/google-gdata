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
using Google.GData.Client;

namespace Google.GData.Blogger
{


    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// This is the Google Calendar feed that lets you access and manage
    /// the calendars you own and also lets you subscribe or 
    /// unsubscribe from calendars owned by others.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class BloggerFeed : AbstractFeed
    {

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public BloggerFeed(Uri uriBase, IService iService) : base(uriBase, iService)
        {
        }

        /// <summary>
        /// This needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new BloggerEntry();
        }
    }
}
