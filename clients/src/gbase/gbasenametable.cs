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
using Google.GData.Client;


namespace Google.GData.GoogleBase
{
    ///////////////////////////////////////////////////////////////////////
    /// <summary>Namespace constants specific to Google Base.</summary>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseNameTable : BaseNameTable
    {
        /// <summary>Google Base namespace (g:) URI.</summary>
        public const string NSGBase = "http://base.google.com/ns/1.0";

        /// <summary>Google Base namespace prefix.</summary>
        public const string GBasePrefix = "g";

        /// <summary>Google Base meta namespace (gm:) URI.</summary>
        public const string NSGBaseMeta = "http://base.google.com/ns-metadata/1.0";

        /// <summary>Google Base meta namespace prefix.</summary>
        public const string GBaseMetaPrefix = "gm";

        /// <summary>Google Base publishing priority element.</summary>
        public const string PublishingPriority = "publishing_priority";

    }
}
