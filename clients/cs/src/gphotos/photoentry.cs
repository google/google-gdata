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
using Google.GData.Extensions.MediaRss;
using Google.GData.Extensions.Exif;
using Google.GData.Extensions.Location;


namespace Google.GData.Photos {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class PhotoEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain Event extension data.
        /// </summary>
        public static AtomCategory PHOTO_CATEGORY =
        new AtomCategory(GDataParserNameTable.Event, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public PhotoEntry()
        : base()
        {
            Tracing.TraceMsg("Created PhotoEntry");
            Categories.Add(PHOTO_CATEGORY);
            GPhotoExtensions.AddExtension(this);
            MediaRssExtensions.AddExtension(this);
            ExifExtensions.AddExtension(this);
            GeoRssExtensions.AddExtension(this);
        }

        /// <summary>
        /// getter/setter for the GeoRssWhere extension element
        /// </summary>
        public GeoRssWhere Location 
        {
            get
            {
                return FindExtension(GeoNametable.GeoRssWhereElement,
                                     GeoNametable.NSGeoRss) as GeoRssWhere;
            }
            set
            {
                ReplaceExtension(GeoNametable.GeoRssWhereElement,
                                GeoNametable.NSGeoRss,
                                value);
            }
        }

        /// <summary>
        /// getter/setter for the ExifTags extension element
        /// </summary>
        public ExifTags Exif 
        {
            get
            {
                return FindExtension(ExifNameTable.ExifTags,
                                     ExifNameTable.NSExif) as ExifTags;
            }
            set
            {
                ReplaceExtension(ExifNameTable.ExifTags,
                                ExifNameTable.NSExif,
                                value);
            }
        }

        /// <summary>
        /// returns the media:rss group container element
        /// </summary>
        public MediaGroup Media
        {
            get
            {
                return FindExtension(MediaRssNameTable.MediaRssGroup,
                                     MediaRssNameTable.NSMediaRss) as MediaGroup;
            }
            set
            {
                ReplaceExtension(MediaRssNameTable.MediaRssGroup,
                                MediaRssNameTable.NSMediaRss,
                                value);
            }
        }





    }
}

