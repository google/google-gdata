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
#define USE_TRACING

using System;
using System.Xml;
using System.IO; 
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Globalization;

namespace Google.GData.YouTube {


    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Playlist feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class PlaylistEntry : YouTubeEntry
    {
         /// <summary>
        /// Category used to label entries as Playlistentries
        /// </summary>
        public static AtomCategory PLAYLIST_CATEGORY =
        new AtomCategory(YouTubeNameTable.KIND_PLAYLIST, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new PlayListEntry instance
        /// </summary>
        public PlaylistEntry()
        : base()
        {
            Tracing.TraceMsg("Created PlaylistEntry");

            if (this.ProtocolMajor == 1)
            {
                Description d = new Description();
                this.AddExtension(d);
            }
            this.AddExtension(new Private());
            Categories.Add(PLAYLIST_CATEGORY);
        }


        /// <summary>
        /// getter/setter for Description subelement
        /// </summary>
        [Obsolete("replaced with Summary.Text")] 
        public string Description 
        {
            get
            {
                return getDescription();
            }
            set
            {
                setDescription(value);
            }
        }

        /// <summary>
        /// getter/setter for the private subelement. Indicates if the playlist is private
        /// </summary>
        public bool Private
        {
            get
            {
                return Convert.ToBoolean(getYouTubeExtensionValue(YouTubeNameTable.Private), CultureInfo.InvariantCulture);
            }
            set
            {
                setYouTubeExtension(YouTubeNameTable.Private, value == false ? null : "");
            }
        }

      

    }
}


