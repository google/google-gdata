/* Copyright (c) 2006 Google Inc.7
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

#define USE_TRACING

using System;
using System.Xml;
using System.Collections;
using System.Net;

#endregion

//////////////////////////////////////////////////////////////////////
// <summary>Contains AtomFeed, an object to represent the atom:feed
// element.</summary> 
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>Base class to read gData feeds in Atom, with the extension of
    /// setting up extension element parsing
    /// </summary> 
    /////////////////////////////////////////////////////////////////////
    public abstract class AbstractFeed : AtomFeed
    {

        /// <summary>
        /// Constructor, set's up extension handlers
        /// </summary>
        /// <param name="uriBase">The uri for this cells feed.</param>
        /// <param name="iService">The Spreadsheets service.</param>
        public AbstractFeed(Uri uriBase, IService iService) : base(uriBase, iService)
        {
            NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewAbstractEntry);
            NewExtensionElement += new ExtensionElementEventHandler(this.OnNewExtensionsElement);
        }


        /// <summary>extension feeds most likely add the GData namespace, so let's 
        /// have a default implementation that does this</summary> 
        /// <param name="writer">the xmlwriter, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer)
        {
            base.AddOtherNamespaces(writer);
            Utilities.EnsureGDataNamespace(writer);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>checks if this is a namespace 
        /// decl that we already added. As the abstract feed adds
        /// the GData namespace, check that one</summary> 
        /// <param name="node">XmlNode to check</param>
        /// <returns>true if this node should be skipped </returns>
        //////////////////////////////////////////////////////////////////////
        protected override bool SkipNode(XmlNode node)
        {
            if (base.SkipNode(node)==true)
            {
                return true; 
            }

            if (node.NodeType == XmlNodeType.Attribute && 
                (node.Name.StartsWith("xmlns") == true) && 
                (String.Compare(node.Value,BaseNameTable.gNamespace)==0))
                return true;
            return false; 
        }


        /// <summary>
        /// Eventhandling. Called when a new entry is parsed.
        /// </summary>
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        protected void OnParsedNewAbstractEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                e.Entry = CreateFeedEntry();
            }
        }

        /// <summary>
        /// this needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public abstract AtomEntry CreateFeedEntry(); 


        /// <summary>eventhandler - called for event extension element
        /// </summary>
        /// <param name="sender">the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedEntry</param> 
        /// <returns> </returns>
        protected void OnNewExtensionsElement(object sender, ExtensionElementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            AtomFeedParser parser = sender as AtomFeedParser;

            if (e.Base.XmlName == AtomParserNameTable.XmlAtomEntryElement)
            {
                 // the base is the Entry of the feed, let's call our parsing on the Entry
                AbstractEntry entry = e.Base as AbstractEntry;
                if (entry != null)
                {
                    entry.Parse(e, parser);
                }
            }
            else 
            {    
                HandleExtensionElements(e, parser);
            }
        }


        /// <summary>
        /// event on the Feed to handle extension elements during parsing
        /// </summary>
        /// <param name="e">the event arguments</param>        /// <param name="parser">the parser that caused this</param>
        protected virtual void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser) 
        {
            Tracing.TraceMsg("Entering HandleExtensionElements on AbstractFeed");
            XmlNode node = e.ExtensionElement;
            if (this.ExtensionFactories != null && this.ExtensionFactories.Count > 0)
            {
                Tracing.TraceMsg("Entring default Parsing for AbstractFeed");
                foreach (IExtensionElementFactory f in this.ExtensionFactories)
                {
                    Tracing.TraceMsg("Found extension Factories");
                    if (String.Compare(node.NamespaceURI, f.XmlNameSpace, true) == 0)
                    {
                        if (String.Compare(node.LocalName, f.XmlName) == 0)
                        {
                            e.Base.ExtensionElements.Add(f.CreateInstance(node, parser));
                            e.DiscardEntry = true;
                            break;
                        }
                    }
                }
            }
            return;

        }
    }
    /////////////////////////////////////////////////////////////////////////////
}
/////////////////////////////////////////////////////////////////////////////
 
