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
using System.Xml;
using System.IO; 
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Blogger
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// CalendarEntry API customization class for defining entries in a calendar feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class BloggerEntry : AbstractEntry
    {
        

        /// <summary>
        /// Constructs a new CalenderEntry instance
        /// </summary>
        public BloggerEntry() : base()
        {
        }


         //////////////////////////////////////////////////////////////////////
        /// <summary>Allows you to retrieve the Post Uri value for a blogger entry. 
        /// This is relevant in the "feeds of blogs"</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AtomUri PostUri
        {
            get 
            {
                AtomLink link = this.Links.FindService(BaseNameTable.ServicePost, AtomLink.ATOM_TYPE);
                // scan the link collection
                return link == null ? null : link.HRef;
            }
            set
            {
                AtomLink link = this.Links.FindService(BaseNameTable.ServicePost, AtomLink.ATOM_TYPE);
                if (link == null)
                {
                    link = new AtomLink(AtomLink.ATOM_TYPE, BaseNameTable.ServicePost);
                    this.Links.Add(link);
                }
                link.HRef = value;
            }
        }
        /////////////////////////////////////////////////////////////////////////////

    }
}
