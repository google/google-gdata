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
    public class PicasaEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain photo extension data.
        /// </summary>
        public static AtomCategory PHOTO_CATEGORY =
        new AtomCategory(GPhotoNameTable.PhotoKind, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Category used to label entries that contain photo extension data.
        /// </summary>
        public static AtomCategory ALBUM_CATEGORY =
        new AtomCategory(GPhotoNameTable.AlbumKind, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Category used to label entries that contain comment extension data.
        /// </summary>
        public static AtomCategory COMMENT_CATEGORY =
        new AtomCategory(GPhotoNameTable.CommentKind, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Category used to label entries that contain photo extension data.
        /// </summary>
        public static AtomCategory TAG_CATEGORY =
        new AtomCategory(GPhotoNameTable.TagKind, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new PicasaEntry instance
        /// </summary>
        public PicasaEntry()
        : base()
        {
            Tracing.TraceMsg("Created PicasaEntry");
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

      
        /// <summary>
        /// instead of having 20 extension elements
        /// we have one string based getter
        /// usage is: entry.getPhotoExtension("albumid") to get the element
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <returns>SimpleElement, or NULL if the extension was not found</returns>
        public SimpleElement GetPhotoExtension(string extension) 
        {
            return FindExtension(extension, GPhotoNameTable.NSGPhotos) as SimpleElement;
        }

        /// <summary>
        /// instead of having 20 extension elements
        /// we have one string based getter
        /// usage is: entry.GetPhotoExtensionValue("albumid") to get the elements value
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <returns>value as string, or NULL if the extension was not found</returns>
        public string GetPhotoExtensionValue(string extension) 
        {
            return GetExtensionValue(extension, GPhotoNameTable.NSGPhotos);
        }




        /// <summary>
        /// instead of having 20 extension elements
        /// we have one string based setter
        /// usage is: entry.SetPhotoExtensionValue("albumid") to set the element
        /// this will create the extension if it's not there
        /// note, you can ofcourse, just get an existing one and work with that 
        /// object: 
        ///     SimpleElement e = entry.getPhotoExtension("albumid");
        ///     e.Value = "new value";  
        /// 
        /// or 
        ///    entry.SetPhotoExtensionValue("albumid", "new Value");
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <param name="newValue">the new value for this extension element</param>
        /// <returns>SimpleElement, either a brand new one, or the one
        /// returned by the service</returns>
        public SimpleElement SetPhotoExtensionValue(string extension, string newValue) 
        {
            return SetExtensionValue(extension, GPhotoNameTable.NSGPhotos, newValue);
        }

        /// <summary>
        /// returns true if the entry is a photo entry
        /// </summary>
        public bool IsPhoto
        {
            get 
            {
                return (this.Categories.Contains(PHOTO_CATEGORY));
            }
            set 
            {
                ToggleCategory(PHOTO_CATEGORY, value);
            }
        } 

        /// <summary>
        /// returns true if the entry is a comment entry
        /// </summary>
        public bool IsComment
        {
            get 
            {
                return (this.Categories.Contains(COMMENT_CATEGORY));
            }
            set 
            {
                ToggleCategory(COMMENT_CATEGORY, value);
            }
        } 

        /// <summary>
        /// returns true if the entry is an album entry
        /// </summary>
        public bool IsAlbum
        {
            get 
            {
                return (this.Categories.Contains(ALBUM_CATEGORY));
            }
            set 
            {
                ToggleCategory(ALBUM_CATEGORY, value);
            }
        } 

        /// <summary>
        /// returns true if the entry is a tag entry
        /// </summary>
        public bool IsTag
        {
            get 
            {
                return (this.Categories.Contains(TAG_CATEGORY));
            }
            set 
            {
                ToggleCategory(TAG_CATEGORY, value);
            }
        } 
    }
}

