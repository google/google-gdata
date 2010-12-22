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
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using Google.GData.Client;

#endregion

namespace Google.GData.GoogleBase
{

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Generates a feed parser and an extension element event
    /// handler that knows about Google Base extensions</summary>
    ///////////////////////////////////////////////////////////////////////
    class GBaseParse
    {
        private FeedParserEventHandler feedParserEventHandler;
        private ExtensionElementEventHandler extensionElementEventHandler;

        public GBaseParse()
        {
            this.feedParserEventHandler =
                new FeedParserEventHandler(this.OnParsedNewEventEntry);
            this.extensionElementEventHandler =
                new ExtensionElementEventHandler(this.OnNewEventExtensionElement);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Event handler that will create the correct entry
        /// objects.</summary>
        ///////////////////////////////////////////////////////////////////////
        public FeedParserEventHandler FeedParserEventHandler
        {
            get
            {
                return feedParserEventHandler;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Event handler that will create the correct objects
        /// for tags the g: and gm: namespaces.</summary>
        ///////////////////////////////////////////////////////////////////////
        public ExtensionElementEventHandler ExtensionElementEventHandler
        {
            get
            {
                return extensionElementEventHandler;
            }
        }

        protected void OnParsedNewEventEntry(object sender,
                                             FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry)
            {
                e.Entry = new GBaseEntry();
            }
        }

        protected void OnNewEventExtensionElement(object sender,
                ExtensionElementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.DiscardEntry)
            {
                return;
            }

            if (String.Compare(e.ExtensionElement.NamespaceURI,
                               GBaseNameTable.NSGBase, true) == 0)
            {
                e.DiscardEntry = true;

                if (e.Base is GBaseAttributeContainer)
                {
                    GBaseAttributeContainer container = e.Base as GBaseAttributeContainer;
                    container.GBaseAttributes.AddFromXml(e.ExtensionElement);
                    return;
                }
            }

            if (e.Base is GBaseEntry)
            {
                GBaseEntry entry = e.Base as GBaseEntry;

                if (entry.AddFromMetaNamespace(e.ExtensionElement))
                {
                    e.DiscardEntry = true;
                }
            }
        }

    }

}
