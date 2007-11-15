/* Copyright (c) 2007 Google Inc.
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
using System.Text;
using System.Xml;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps
{
    /// <summary>
    /// Feed API customization class for defining nickname feed.
    /// </summary>
    public class NicknameFeed : AbstractFeed
    {
        /// <summary>
        /// Constructs a new NicknameFeed.
        /// </summary>
        /// <param name="uriBase">the URI of the feed</param>
        /// <param name="iService">the service with which this
        /// feed will be associated</param>
        public NicknameFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
            GAppsExtensions.AddProvisioningExtensions(this);
        }

        /// <summary>
        /// Overridden.  Returns a new <code>NicknameEntry</code>.
        /// </summary>
        /// <returns>the new <code>NicknameEntry</code></returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new NicknameEntry();
        }
    }
}
