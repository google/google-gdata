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


    /// <summary>
    /// holds the timezone element on the feed level
    /// </summary>
    public class TimeZone : SimpleAttribute
    {
        /// <summary>
        ///  default constructor
        /// </summary>
        public TimeZone()
            : base(GDataParserNameTable.XmlTimeZoneElement, GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal)
        {
        }

        /// <summary>
        ///  default constructor with an initial value for the attribute
        /// </summary>
        /// <param name="initValue"></param>
        public TimeZone(string initValue)
            : base(GDataParserNameTable.XmlTimeZoneElement, GDataParserNameTable.gCalPrefix, GDataParserNameTable.NSGCal, initValue)
        {
        }
    }

    
    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Feed API customization class for defining feeds in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class EventFeed : AbstractFeed
    {

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public EventFeed(Uri uriBase, IService iService) : base(uriBase, iService)
        {
            AddExtension(new WebContent());
            AddExtension(new Where());
            AddExtension(new TimeZone());
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor for the Location collection</summary>
        //////////////////////////////////////////////////////////////////////
        public Where Location
        {
            get 
            { 

                return FindExtension(GDataParserNameTable.XmlWhereElement, 
                                     BaseNameTable.gNamespace) as Where;
            }
            set
            {
                ReplaceExtension(GDataParserNameTable.XmlWhereElement, 
                                 BaseNameTable.gNamespace,
                                 value);
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public TimeZone TimeZone</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public TimeZone TimeZone
        {
            get 
            { 

                return FindExtension(GDataParserNameTable.XmlTimeZoneElement, 
                                     GDataParserNameTable.NSGCal) as TimeZone;
            }
            set
            {
                ReplaceExtension(GDataParserNameTable.XmlTimeZoneElement, 
                                 GDataParserNameTable.NSGCal,
                                 value);
            }
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
                EventQuery query = new EventQuery(original.Href);
                EventFeed newFeed =  (EventFeed) calService.Query(query); 

                if (newFeed != null && newFeed.Entries != null)
                {
                    Tracing.Assert(newFeed.Entries.Count == 1, "There should be just one entry returned"); 
                    return newFeed.Entries[0] as EventEntry;  
                }
            }
            return null; 
        }


      
        /// <summary>
        /// this needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new EventEntry();
        }
    }
}
