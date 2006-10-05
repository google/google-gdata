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
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.CodeSearch 
{
    /// <summary>
    /// Feed API customization class for defining feeds in an CodeSearch feed.
    /// </summary>
    public class CodeSearchFeed : AtomFeed 
    {
        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public CodeSearchFeed(Uri uriBase, IService iService) :
            base(uriBase, iService) 
        {
            this.NewAtomEntry +=
                new FeedParserEventHandler(
                this.OnParsedNewCodeSearchEntry);
            this.NewExtensionElement += 
                new ExtensionElementEventHandler(
                this.OnNewCodeSearchExtensionElement);
        }
        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Eventhandling. Called when a new event entry is parsed.
        /// </summary>
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnParsedNewCodeSearchEntry(object sender
                                                    , FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                Tracing.TraceMsg(
                    "\t top level event dispatcher - new Event Entry");
                e.Entry = new CodeSearchEntry();
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>eventhandler - called for event extension element
        /// </summary>
        /// <param name="sender">the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds 
        /// the feedEntry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void 
            OnNewCodeSearchExtensionElement(object sender,
                                            ExtensionElementEventArgs e)
        {
            Tracing.TraceCall(
                "received new event extension element notification");
            Tracing.Assert(e != null, "e should not be null");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            Tracing.TraceMsg("\t top level event = new extension");
            // Ensure that the namespace is correct.
            if (String.Compare(e.ExtensionElement.NamespaceURI,
                GCodeSearchParserNameTable.CSNamespace, true) == 0)
            {
                // found namespace I
                Tracing.TraceMsg("\t top level event = new Event extension");
                // TODO(miguelg) shall I add a check in case
                // what comes is a feed instead of an entry?
                CodeSearchEntry codeSearchEntry = e.Base as CodeSearchEntry;
                AtomFeedParser parser = sender as AtomFeedParser;
                if (codeSearchEntry != null)
                {
                    codeSearchEntry.parseEvent(e.ExtensionElement, parser);
                }
            }
        }
    }
}
