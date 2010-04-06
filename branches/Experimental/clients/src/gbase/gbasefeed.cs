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
#region Using directives

using System;
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;

#endregion

namespace Google.GData.GoogleBase {

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Google Base feeds.</summary>
    /// <seealso cref="GBaseEntry"/>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseFeed : AtomFeed, GBaseAttributeContainer
    {
        private readonly GBaseAttributes attributes;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a new feed</summary>
        /// <param name="uriBase">a uri, usually coming from
        /// <see cref="GBaseUriFactory"/></param>
        /// <param name="service">the GBaseService that this feed will be
        /// used with.</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseFeed(Uri uriBase, GBaseService service)
                : base(uriBase, service)
        {
            attributes = new GBaseAttributes(ExtensionElements);

            GBaseParse parse = new GBaseParse();
            this.NewAtomEntry += parse.FeedParserEventHandler;
            this.NewExtensionElement += parse.ExtensionElementEventHandler;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>g: tags that have been put directly into the feed.</summary>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttributes GBaseAttributes
        {
            get
            {
                return attributes;
            }
        }
    }

}
