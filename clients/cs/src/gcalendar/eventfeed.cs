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

namespace Google.GData.Calendar {

    //////////////////////////////////////////////////////////////////////
    /// <summary>holds the timezone element on the feed level
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class TimeZone  : IExtensionElement
    {
        private string value; 

        //////////////////////////////////////////////////////////////////////
        /// <summary>read only accessor</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Value
        {
            get {return this.value;}
        }
        // end of accessor public string Value

        //////////////////////////////////////////////////////////////////////
        /// <summary>internal setter for parsing</summary> 
        //////////////////////////////////////////////////////////////////////
        internal void setTimeZone(string value) 
        {
            this.value = value; 
        }

        #region timezone Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an XML node to create a feed level timezone</summary> 
        /// <param name="node">timezone node</param>
        /// <returns> the created timezone object</returns>
        //////////////////////////////////////////////////////////////////////
        public static TimeZone ParseTimeZone(XmlNode node)
        {
            Tracing.TraceMsg("Entering ParseTimeZone");
            TimeZone timezone = null;
            Tracing.Assert(node != null, "node should not be null");

            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.XmlTimeZoneElement))
            {
                timezone = new TimeZone();
                if (node.Attributes != null)
                {
                    if (node.Attributes[GDataParserNameTable.XmlAttributeValue] != null)
                    {
                        timezone.setTimeZone(node.Attributes[GDataParserNameTable.XmlAttributeValue].Value); 
                    }
                }
            }
            
            return timezone;
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>saves the current object into the stream. TimeZone,
        /// though does not get saved</summary> 
        /// <param name="writer">xmlWriter to write into</param>
        //////////////////////////////////////////////////////////////////////
        public void Save(XmlWriter writer)
        {
            Tracing.TraceMsg("Save called on TimeZone... skipping it"); 
            return; 
        }

        #endregion



    }
    //end of public class TimeZone

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Feed API customization class for defining feeds in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class EventFeed : AtomFeed
    {

        private Where location;
        private TimeZone timezone; 

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public EventFeed(Uri uriBase, IService iService) : base(uriBase, iService)
        {
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEventEntry);
            this.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewEventExtensionElement);
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor for the Location collection</summary>
        //////////////////////////////////////////////////////////////////////
        public Where Location
        {
            get { return location; }
            set
            {
                if (location != null)
                {
                    ExtensionElements.Remove(location);
                }
                location = value;
                ExtensionElements.Add(location);
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public TimeZone TimeZone</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public TimeZone TimeZone
        {
            get {return this.timezone;}
            set {this.timezone = value;}
        }
        // end of accessor public TimeZone TimeZone


        //////////////////////////////////////////////////////////////////////
        /// <summary>searches through the evententries to 
        /// find the original event</summary> 
        /// <param name="original">The original event to find</param>
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public EventEntry FindEvent(OriginalEvent original) 
        {
            // first try the internal cache

            foreach (EventEntry entry  in this.Entries )
            {
                if (entry.SelfUri.ToString() == original.Href)
                {
                    return entry; 
                }
            }

            // did not find it in the cache. Need to call the server to get it. 
            CalendarService calService = this.Service as CalendarService; 

            if (calService != null)
            {
                EventQuery query = new EventQuery();
                query.Uri = new Uri(original.Href); 
                EventFeed newFeed = calService.Query(query); 

                if (newFeed != null && newFeed.Entries != null)
                {
                    Tracing.Assert(newFeed.Entries.Count == 1, "There should be just one entry returned"); 
                    return newFeed.Entries[0] as EventEntry;  
                }
            }
            return null; 
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses the inner state of the element.</summary>
        /// <param name="eventNode">a g-scheme, xml node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        //////////////////////////////////////////////////////////////////////
        public void parseEvent(XmlNode eventNode, AtomFeedParser parser)
        {
            if (String.Compare(eventNode.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
            {
                // Parse a Where Element
                if (eventNode.LocalName == GDataParserNameTable.XmlWhereElement)
                {
                    if (this.Location == null)
                    {
                        this.Location = Where.ParseWhere(eventNode, parser);
                    } 
                    else
                    {
                        throw new ArgumentException("Only one g:where element is valid in the Event Feeds");
                    }
                }
            }
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>empty base implementation</summary> 
        /// <param name="writer">the xmlwriter, where we want to add default namespaces to</param>
        //////////////////////////////////////////////////////////////////////
        protected override void AddOtherNamespaces(XmlWriter writer) 
        {
            base.AddOtherNamespaces(writer); 
            Utilities.EnsureGDataNamespace(writer); 
        }
        /////////////////////////////////////////////////////////////////////////////
         
        //////////////////////////////////////////////////////////////////////
        /// <summary>checks if this is a namespace 
        /// decl that we already added</summary> 
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


        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Eventhandling. Called when a new event entry is parsed.
        /// </summary>
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnParsedNewEventEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                Tracing.TraceMsg("\t top level event dispatcher - new Event Entry");
                e.Entry = new EventEntry();
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>eventhandler - called for event extension element
        /// </summary>
        /// <param name="sender">the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedEntry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewEventExtensionElement(object sender, ExtensionElementEventArgs e)
        {

            Tracing.TraceCall("received new event extension element notification");
            Tracing.Assert(e != null, "e should not be null");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            Tracing.TraceMsg("\t top level event = new extension");

            if (String.Compare(e.ExtensionElement.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
            {
                // found GD namespace
                Tracing.TraceMsg("\t top level event = new Event extension");
                e.DiscardEntry = true;
                AtomFeedParser parser = sender as AtomFeedParser; 

                if (e.Base.XmlName == AtomParserNameTable.XmlFeedElement)
                {
                    EventFeed eventFeed = e.Base as EventFeed;
                    if (eventFeed != null)
                    {
                        eventFeed.parseEvent(e.ExtensionElement, parser);
                    }
                }
                else if (e.ExtensionElement.LocalName == GDataParserNameTable.XmlExtendedPropertyElement)
                {
                    ExtendedProperty prop = ExtendedProperty.Parse(e.ExtensionElement); 
                    e.Base.ExtensionElements.Add(prop); 
                }
                else if (e.Base.XmlName == AtomParserNameTable.XmlAtomEntryElement)
                {
                    EventEntry eventEntry = e.Base as EventEntry;
                    
                    if (eventEntry != null)
                    {
                        eventEntry.parseEvent(e.ExtensionElement, parser);
                    }
                }
            }
            else if (String.Compare(e.ExtensionElement.NamespaceURI, GDataParserNameTable.NSGCal, true) == 0)
            {
                // found calendar  namespace
                Tracing.TraceMsg("\t entering the handler for calendar specific extensions for: " + e.Base.XmlName);
                e.DiscardEntry = true;

				if (e.ExtensionElement.LocalName == GDataParserNameTable.XmlTimeZoneElement)
				{
					EventFeed eventFeed = e.Base as EventFeed;
					if (eventFeed != null)
					{
						eventFeed.TimeZone = TimeZone.ParseTimeZone(e.ExtensionElement);
					}
				}
				else if (e.ExtensionElement.LocalName == GDataParserNameTable.XmlWebContentElement)
				{
					WebContent content = WebContent.ParseWebContent(e.ExtensionElement); 
					e.Base.ExtensionElements.Add(content); 
				}
				else if (e.Base.XmlName == AtomParserNameTable.XmlAtomEntryElement)
				{
					EventEntry eventEntry = e.Base as EventEntry;
					AtomFeedParser parser = sender as AtomFeedParser; 
                    
					if (eventEntry != null)
					{
						eventEntry.parseEvent(e.ExtensionElement, parser);
					}
				}
            }
        }
    }
}
