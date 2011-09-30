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
using System.ComponentModel;

namespace Google.GData.Photos 
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A photoEntry is a shallow subclass for a PicasaEntry to ease
    /// access for photospecific properties
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class PhotoEntry : PicasaEntry
    {
        /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public PhotoEntry()
            : base()
        {
            Tracing.TraceMsg("Created PhotoEntry");
            Categories.Add(PHOTO_CATEGORY);
        }
    }

    /// <summary>
    /// this is a pure accessor class that can either take a photoentry or work with 
    /// a picasaentry to get you convienience accessors
    /// </summary>
    [Obsolete("Use Google.Picasa.Photo instead. This code will be removed soon")] 
    public class PhotoAccessor
    {
        private PicasaEntry entry;

        /// <summary>
        /// constructs a photo accessor for the passed in entry
        /// </summary>
        /// <param name="entry"></param>
        public PhotoAccessor(PicasaEntry entry)
        {
            this.entry = entry;
            if (!entry.IsPhoto)
            {
                throw new ArgumentException("Entry is not a photo", "entry");
            }
        }


        /// <summary>
        /// The title of the photo
        /// </summary>
        [Category("Base Photo Data"),
        Description("Specifies the name of the photo.")]
        public string PhotoTitle
        {
            get 
            {
                return this.entry.Title.Text;
            }
            set 
            {
                this.entry.Title.Text = value;
            }
        }

        
        /// <summary>
        /// The  summary of the Photo
        /// </summary>
        [Category("Base Photo Data"),
        Description("Specifies the summary of the Photo.")]
        public string PhotoSummary
        {
            get 
            {
                return this.entry.Summary.Text;
            }
            set 
            {
                this.entry.Summary.Text = value;
            }
        }


        /// <summary>
        /// The checksum on the photo. This optional field can be used by 
        /// uploaders to associate a checksum with a photo to ease duplicate detection
        /// </summary>
        [Category("Meta Photo Data"),
        Description("The checksum on the photo.")]
        public string Checksum 
        {
            get 
            {
                return this.entry.GetPhotoExtensionValue(GPhotoNameTable.Checksum);
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Checksum, value);
            }
        }

 

        /// <summary>
        /// The height of the photo in pixels
        /// </summary>
        [Category("Basic Photo Data"),
        Description("The height of the photo in pixels.")]
        public int Height 
        {
            get 
            {
                return Convert.ToInt32(this.entry.GetPhotoExtensionValue(GPhotoNameTable.Height));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Height, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The width of the photo in pixels
        /// </summary>
        [Category("Basic Photo Data"),
        Description("The width of the photo in pixels.")]
        public int Width 
        {
            get 
            {
                return Convert.ToInt32(this.entry.GetPhotoExtensionValue(GPhotoNameTable.Width));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Width, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The rotation of the photo in degrees, used to change the rotation of the photo. Will only be shown if 
        /// the rotation has not already been applied to the requested images.
        /// </summary>
        [Category("Basic Photo Data"),
        Description("The rotation of the photo in degrees.")]
        public int Rotation 
        {
            get 
            {
                return Convert.ToInt32(this.entry.GetPhotoExtensionValue(GPhotoNameTable.Rotation));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Rotation, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The size of the photo in bytes
        /// </summary>
        [Category("Basic Photo Data"),
        Description("The size of the photo in bytes.")]
        public long Size 
        {
            get 
            {
                return Convert.ToInt32(this.entry.GetPhotoExtensionValue(GPhotoNameTable.Size));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Size, Convert.ToString(value));
            }
        }


        /// <summary>
        /// The photo's timestamp, represented as the number of milliseconds since 
        /// January 1st, 1970. Contains the date of the photo either set externally
        /// or retrieved from the Exif data.
        /// </summary>
        [Category("Meta Photo Data"),
        Description("The photo's timestamp")]
        [CLSCompliant(false)]
        public ulong Timestamp 
        {
            get 
            {
                return Convert.ToUInt64(this.entry.GetPhotoExtensionValue(GPhotoNameTable.Timestamp));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Timestamp, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The albums ID
        /// </summary>
        [Category("Meta Photo Data"),
        Description("The albums ID.")]
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
        /// the number of comments on a photo
        /// </summary>
        [Category("Commenting"),
        Description("the number of comments on a photo.")]
        [CLSCompliant(false)]
        public uint CommentCount 
        {
            get 
            {
                return Convert.ToUInt32(this.entry.GetPhotoExtensionValue(GPhotoNameTable.CommentCount));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.CommentCount, Convert.ToString(value));
            }
        }

        /// <summary>
        /// is commenting enabled on a photo
        /// </summary>
        [Category("Commenting"),
        Description("is commenting enabled on a photo.")]
        public bool CommentingEnabled 
        {
            get 
            {
                return Convert.ToBoolean(this.entry.GetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled, Utilities.ConvertBooleanToXSDString(value));
            }
        }

    
        /// <summary>
        /// the id of the photo
        /// </summary>
        [Category("Meta Photo Data"),
        Description("the id of the photo.")]
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

        /// <summary>
        /// the Longitude  of the photo
        /// </summary>
        [Category("Location Photo Data"),
        Description("The longitude of the photo.")]
        public double Longitude 
        {
            get 
            {
                GeoRssWhere where = this.entry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where != null)
                {
                    return where.Longitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere where = this.entry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where == null)
                {
                    where = entry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.entry.ExtensionElements.Add(where);
                }
                where.Longitude = value; 
            }
        }
    
        /// <summary>
        /// the Longitude  of the photo
        /// </summary>
        [Category("Location Photo Data"),
        Description("The Latitude of the photo.")]
        public double Latitude 
        {
            get 
            {
                GeoRssWhere where = this.entry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where != null)
                {
                    return where.Latitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere where = this.entry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where == null)
                {
                    where = entry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.entry.ExtensionElements.Add(where);
                }
                where.Latitude = value; 
            }
        }
    }
}

