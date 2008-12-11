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
using System.Collections.Generic;
using Google.GData.Client;

namespace Google.GData.YouTube {
    
    /// <summary>
    /// enum to define different activities. 
    /// </summary>
    public enum ActivityType
    {
        /// <summary>
        /// a user rated an entry
        /// </summary>
        Rated,
        /// <summary>
        /// a user shared a video
        /// </summary>
        Shared,
        /// <summary>
        /// a user uploaded a video
        /// </summary>
        Uploaded,
        /// <summary>
        /// a user added something to his favorites
        /// </summary>
        Favorited,
        /// <summary>
        /// undfined -> there was no Type for this entry
        /// </summary>
        Undefined
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for retrieving activies 
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class ActivityEntry : AbstractEntry
    {


         /// <summary>
        /// Category used to label entries that indicate a user marking a video as a favorite
        /// </summary>
        public static AtomCategory VIDEORATED_CATEGORY =
        new AtomCategory(YouTubeNameTable.VideoRatedCategory, new AtomUri(YouTubeNameTable.EventsCategorySchema));

                 /// <summary>
        /// Category used to label entries that indicate a user marking a video as a favorite
        /// </summary>
        public static AtomCategory VIDEOSHARED_CATEGORY =
        new AtomCategory(YouTubeNameTable.VideoSharedCategory, new AtomUri(YouTubeNameTable.EventsCategorySchema));

                 /// <summary>
        /// Category used to label entries that indicate a user marking a video as a favorite
        /// </summary>
        public static AtomCategory VIDEOUPLOADED_CATEGORY =
        new AtomCategory(YouTubeNameTable.VideoUploadedCategory, new AtomUri(YouTubeNameTable.EventsCategorySchema));

                 /// <summary>
        /// Category used to label entries that indicate a user marking a video as a favorite
        /// </summary>
        public static AtomCategory VIDEOFAVORITED_CATEGORY =
        new AtomCategory(YouTubeNameTable.VideoFavoritedCategory, new AtomUri(YouTubeNameTable.EventsCategorySchema));


        /// <summary>
        /// Constructs a new EventEmtry instance
        /// </summary>
        public ActivityEntry()
        : base()
        {
            this.AddExtension(new VideoId());
        }

        /// <summary>
        ///  The type of Event, the user action that caused this.
        /// </summary>
        /// <returns></returns>
        public ActivityType Type
        {
            get 
            {
                if (this.Categories.Contains(VIDEORATED_CATEGORY))
                {
                    return ActivityType.Rated;
                } 
                else if (this.Categories.Contains(VIDEOSHARED_CATEGORY))
                {
                    return ActivityType.Shared;
                }
                else if (this.Categories.Contains(VIDEOUPLOADED_CATEGORY))
                {
                    return ActivityType.Uploaded;
                }
                else if (this.Categories.Contains(VIDEOFAVORITED_CATEGORY))
                {
                    return ActivityType.Favorited;
                }
                return ActivityType.Undefined;
            }
        } 

        /// <summary>
        ///  property accessor for the VideoID, if applicable
        /// </summary>
        public VideoId VideoId
        {
            get
            {
                return FindExtension(YouTubeNameTable.VideoID,
                                     YouTubeNameTable.NSYouTube) as VideoId;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>returns the video relation link uri, which can be used to
        /// retrieve the video entry</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AtomUri VideoLink
        {
            get
            {
                AtomLink link = this.Links.FindService(YouTubeNameTable.KIND_VIDEO, AtomLink.ATOM_TYPE);
                // scan the link collection
                return link == null ? null : link.HRef;
            }
        }
    }
}


