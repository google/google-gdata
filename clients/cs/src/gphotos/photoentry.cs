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
#if WindowsCE || PocketPC
#else
using System.ComponentModel;
#endif

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
            if (entry.IsPhoto == false)
            {
                throw new ArgumentException("Entry is not a photo", "entry");
            }
        }



#if WindowsCE || PocketPC
#else
        [Category("Base Photo Data"),
        Description("Specifies the name of the photo.")]
#endif
        /// <summary>
        /// The title of the photo
        /// </summary>
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
#if WindowsCE || PocketPC
#else
        [Category("Base Photo Data"),
        Description("Specifies the summary of the Photo.")]
#endif
        /// <summary>
        /// The  summary of the Photo
        /// </summary>
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


#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("The checksum on the photo.")]
#endif
        /// <summary>
        /// The checksum on the photo. This optional field can be used by 
        /// uploaders to associate a checksum with a photo to ease duplicate detection
        /// </summary>
        public string Checksum 
        {
            get 
            {
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.Checksum);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Checksum, value);
            }
        }

#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("The client application that created the photo.")]
#endif
        /// <summary>
        /// The client application that created the photo. (Optional element.)
        /// </summary>
        public string Client 
        {
            get 
            {
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.Client);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Client, value);
            }
        }

#if WindowsCE || PocketPC
#else
        [Category("Basic Photo Data"),
        Description("The height of the photo in pixels.")]
#endif
        /// <summary>
        /// The height of the photo in pixels
        /// </summary>
        public int Height 
        {
            get 
            {
                return Convert.ToInt32(this.entry.getPhotoExtensionValue(GPhotoNameTable.Height));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Height, Convert.ToString(value));
            }
        }

#if WindowsCE || PocketPC
#else
        [Category("Basic Photo Data"),
        Description("The width of the photo in pixels.")]
#endif
        /// <summary>
        /// The width of the photo in pixels
        /// </summary>
        public int Width 
        {
            get 
            {
                return Convert.ToInt32(this.entry.getPhotoExtensionValue(GPhotoNameTable.Width));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Width, Convert.ToString(value));
            }
        }


#if WindowsCE || PocketPC
#else
        [Category("Basic Photo Data"),
        Description("The ordinal position of the photo in the parent album.")]
#endif
        /// <summary>
        /// The ordinal position of the photo in the parent album
        /// </summary>
        public double Position 
        {
            get 
            {
                return Convert.ToDouble(this.entry.getPhotoExtensionValue(GPhotoNameTable.Position));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Position, Convert.ToString(value));
            }
        }

#if WindowsCE || PocketPC
#else
        [Category("Basic Photo Data"),
        Description("The rotation of the photo in degrees.")]
#endif
        /// <summary>
        /// The rotation of the photo in degrees, used to change the rotation of the photo. Will only be shown if 
        /// the rotation has not already been applied to the requested images.
        /// </summary>
        public int Rotation 
        {
            get 
            {
                return Convert.ToInt32(this.entry.getPhotoExtensionValue(GPhotoNameTable.Rotation));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Rotation, Convert.ToString(value));
            }
        }

#if WindowsCE || PocketPC
#else
        [Category("Basic Photo Data"),
        Description("The size of the photo in bytes.")]
#endif
        /// <summary>
        /// The size of the photo in bytes
        /// </summary>
        public long Size 
        {
            get 
            {
                return Convert.ToInt32(this.entry.getPhotoExtensionValue(GPhotoNameTable.Size));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Size, Convert.ToString(value));
            }
        }


#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("The photo's timestamp")]
#endif
        /// <summary>
        /// The photo's timestamp, represented as the number of milliseconds since 
        /// January 1st, 1970. Contains the date of the photo either set externally
        /// or retrieved from the Exif data.
        /// </summary>
        public ulong Timestamp 
        {
            get 
            {
                return Convert.ToUInt64(this.entry.getPhotoExtensionValue(GPhotoNameTable.Timestamp));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Timestamp, Convert.ToString(value));
            }
        }


#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("The version number of the photo.")]
#endif
        /// <summary>
        /// The version number of the photo. Version numbers are based on modification time, 
        /// so they don't increment linearly. Note that if you try to update a photo using a 
        /// version number other than the latest one, you may receive an error; 
        /// for more information, see Optimistic concurrency (versioning) in the GData reference document
        /// </summary>
        public string Version 
        {
            get 
            {
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.Version);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Version, value);
            }
        }

#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("The albums ID.")]
#endif
        /// <summary>
        /// The albums ID
        /// </summary>
        public string AlbumId 
        {
            get 
            {
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.AlbumId);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.AlbumId, value);
            }
        }

#if WindowsCE || PocketPC
#else
        [Category("Commenting"),
        Description("the number of comments on a photo.")]
#endif
        /// <summary>
        /// the number of comments on a photo
        /// </summary>
        public uint CommentCount 
        {
            get 
            {
                return Convert.ToUInt32(this.entry.getPhotoExtensionValue(GPhotoNameTable.CommentCount));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.CommentCount, Convert.ToString(value));
            }
        }

#if WindowsCE || PocketPC
#else
        [Category("Commenting"),
        Description("is commenting enabled on a photo.")]
#endif
        /// <summary>
        /// is commenting enabled on a photo
        /// </summary>
        public bool CommentingEnabled 
        {
            get 
            {
                return Convert.ToBoolean(this.entry.getPhotoExtensionValue(GPhotoNameTable.CommentingEnabled));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.CommentCount, Utilities.ConvertBooleanToXSDString(value));
            }
        }

    
#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("the id of the photo.")]
#endif
        /// <summary>
        /// the id of the photo
        /// </summary>
        public string Id 
        {
            get 
            {
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.Id);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Id, value);
            }
        }


    }

   
}

