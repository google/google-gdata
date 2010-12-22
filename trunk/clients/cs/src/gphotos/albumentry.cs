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
using Google.GData.Extensions.Location;
#if WindowsCE || PocketPC
#else
using System.ComponentModel;
#endif


namespace Google.GData.Photos 
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class AlbumEntry : PicasaEntry
    {
        /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public AlbumEntry()
        : base()
        {
            Categories.Add(ALBUM_CATEGORY);
        }
    }

    /// <summary>
    ///  accessor for an AlbumEntry
    /// </summary>
    [Obsolete("Use Google.Picasa.Album instead. This code will be removed soon")] 
    public class AlbumAccessor
    {

        private PicasaEntry entry;

        /// <summary>
        /// constructs a photo accessor for the passed in entry
        /// </summary>
        /// <param name="entry"></param>
        public AlbumAccessor(PicasaEntry entry)
        {
            this.entry = entry;
            if (!entry.IsAlbum)
            {
                throw new ArgumentException("Entry is not a album", "entry");
            }
        }

        /// <summary>
        /// The album's access level. In this document, access level is also 
        /// referred to as "visibility." Valid values are public or private.
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Meta Album Data"),
        Description("Specifies the access for the album.")]
#endif
        public string Access 
        {
            get 
             {
                return this.entry.GetPhotoExtensionValue(GPhotoNameTable.Access);
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Access, value);
            }
        }
        
        
        /// <summary>
        /// The nickname of the author
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Base Album Data"),
        Description("Specifies the author's nickname")]
#endif
        public string AlbumAuthorNickname
        {
            get 
            {
                return this.entry.GetPhotoExtensionValue(GPhotoNameTable.Nickname);
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Nickname, value);
            }
        }

        /// <summary>
        /// The  author's name
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Base Album Data"),
        Description("Specifies the author's name")]
#endif
        public string AlbumAuthor
        {
            get 
            {
                AtomPersonCollection authors = this.entry.Authors;
                if (authors != null && authors.Count >0) 
                {
                    AtomPerson person = authors[0];
                    return person.Name;
                }
                return "No Author given";

            }
            set 
            {
                AtomPersonCollection authors = this.entry.Authors;
                if (authors != null) 
                {
                    AtomPerson person = null;
                    if (authors.Count > 0)
                    {
                        person = authors[0]; 
                    }
                    else 
                    {
                        person = new AtomPerson(AtomPersonType.Author);
                        this.entry.Authors.Add(person);
                    }
                    person.Name = value;
                }
            }
        }

        /// <summary>
        /// The title of the album
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Base Album Data"),
        Description("Specifies the name of the album.")]
#endif
        public string AlbumTitle
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
        /// The  summary of the album
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Base Album Data"),
        Description("Specifies the summary of the album.")]
#endif
        public string AlbumSummary
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
        /// The number of bytes of storage that this album uses.
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Meta Album Data"),
        Description("Specifies the bytes used for the album.")]
#endif
        [CLSCompliant(false)]
        public uint BytesUsed 
        {
            get 
             {
                return Convert.ToUInt32(this.entry.GetPhotoExtensionValue(GPhotoNameTable.BytesUsed));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.BytesUsed, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The user-specified location associated with the album
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Location Data"),
        Description("Specifies the location for the album.")]
#endif
        public string Location 
        {
            get 
             {
                return this.entry.GetPhotoExtensionValue(GPhotoNameTable.Location);
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.Location, value);
            }
        }

        /// <summary>
        /// the Longitude  of the photo
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Location Data"),
        Description("The longitude of the photo.")]
#endif
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
#if WindowsCE || PocketPC
#else
        [Category("Location Data"),
        Description("The Latitude of the photo.")]
#endif
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
  
        /// <summary>
        /// The number of photos in the album.
        /// </summary>
        /// 
#if WindowsCE || PocketPC
#else
        [Category("Meta Album Data"),
        Description("Specifies the number of photos in the album.")]
#endif
        [CLSCompliant(false)]
        public uint NumPhotos 
        {
            get 
             {
                return Convert.ToUInt32(this.entry.GetPhotoExtensionValue(GPhotoNameTable.NumPhotos));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.NumPhotos, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The number of remaining photo uploads allowed in this album. 
        /// This is equivalent to the user's maximum number of photos per 
        /// album (gphoto:maxPhotosPerAlbum) minus the number of photos 
        /// currently in the album (gphoto:numphotos).
        /// </summary>
        
#if WindowsCE || PocketPC
#else
        [Category("Meta Album Data"),
        Description("Specifies the number of remaining photo uploads for the album.")]
#endif
        [CLSCompliant(false)]
        public uint NumPhotosRemaining
        {
            get 
             {
                return Convert.ToUInt32(this.entry.GetPhotoExtensionValue(GPhotoNameTable.NumPhotosRemaining));
            }
            set 
            {
                this.entry.SetPhotoExtensionValue(GPhotoNameTable.NumPhotosRemaining, Convert.ToString(value));
            }
        }


        /// <summary>
        /// the number of comments on an album
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Commenting"),
        Description("Specifies the number of comments for the album.")]
#endif
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
        /// is commenting enabled on an album
        /// </summary>
        
#if WindowsCE || PocketPC
#else
        [Category("Commenting"),
        Description("Comments enabled?")]
#endif
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
        /// the id of the album
        /// </summary>
        
#if WindowsCE || PocketPC
#else
        [Category("Base Album Data"),
        Description("Specifies the id for the album.")]
#endif
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


