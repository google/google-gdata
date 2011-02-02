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
using Google.GData.Extensions;

namespace Google.GData.ContentForShopping
{
    /// <summary>
    /// Feed API customization class for defining Product feed.
    /// </summary>
    public class ProductFeed : AbstractFeed
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uriBase">The uri for the product feed.</param>
        /// <param name="iService">The ContentForShopping service.</param>
        public ProductFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
        }

        /// <summary>
        /// returns a new entry for this feed
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new ProductEntry();
        }
    }
}
