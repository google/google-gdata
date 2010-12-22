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

namespace Google.GData.Photos {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class TagEntry : PicasaEntry
    {
           /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public TagEntry()
        : base()
        {
            Categories.Add(TAG_CATEGORY);
        }

    }

    /// <summary>
    ///  accessor for a tag Entry
    /// </summary>
    public class TagAccessor 
    {

        private PicasaEntry entry;

        /// <summary>
        /// constructs a tag accessor for the passed in entry
        /// </summary>
        /// <param name="entry"></param>
        public TagAccessor(PicasaEntry entry)
        {
            this.entry = entry;
            if (!entry.IsTag)
            {
                throw new ArgumentException("Entry is not a tag", "entry");
            }
        }

  
        /// <summary>
        /// The weight of the tag. The weight is the number of times the tag appears 
        /// in photos under the current element. The default weight is 1.
        /// </summary>
        [CLSCompliant(false)]
        public uint Weight 
        {
            get 
             {
                return Convert.ToUInt32(this.entry.GetPhotoExtensionValue(GPhotoNameTable.Weight));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Weight, Convert.ToString(value));
            }
        }
    }
}

