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
    public class CommentEntry : PicasaEntry
    {
        /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public CommentEntry()
        : base()
        {
            Categories.Add(COMMENT_CATEGORY);
        }


    }

    /// <summary>
    ///  accessor for a Comment Entry
    /// </summary>
    [Obsolete("Use Google.Picasa.Comment instead. This code will be removed soon")] 
    public class CommentAccessor 
    {

        private PicasaEntry entry;

        /// <summary>
        /// constructs a photo accessor for the passed in entry
        /// </summary>
        /// <param name="entry"></param>
        public CommentAccessor(PicasaEntry entry)
        {
            this.entry = entry;
            if (!entry.IsComment)
            {
                throw new ArgumentException("Entry is not a comment", "entry");
            }
        }

        /// <summary>
        /// The ID of the photo associated with the current comment.
        /// </summary>
        public string PhotoId 
        {
            get 
            {
                return this.entry.GetPhotoExtensionValue(GPhotoNameTable.Photoid);
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Photoid, value);
            }
        }

        /// <summary>
        /// The albums ID
        /// </summary>
        public string AlbumId 
        {
            get 
            {
                return this.entry.GetPhotoExtensionValue(GPhotoNameTable.AlbumId);
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.AlbumId, value);
            }
        }

        /// <summary>
        /// the id of the comment
        /// </summary>
        public string Id 
        {
            get 
            {
                return this.entry.GetPhotoExtensionValue(GPhotoNameTable.Id);
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Id, value);
            }
        }
      
    }
}

