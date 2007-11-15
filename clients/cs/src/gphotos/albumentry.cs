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
            if (entry.IsAlbum == false)
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
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.Access);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Access, value);
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
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.Nickname);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Nickname, value);
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
                if (authors != null && authors.Count >0) 
                {
                    AtomPerson person = authors[0];
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
        public uint BytesUsed 
        {
            get 
             {
                return Convert.ToUInt32(this.entry.getPhotoExtensionValue(GPhotoNameTable.BytesUsed));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.BytesUsed, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The user-specified location associated with the album
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Base Album Data"),
        Description("Specifies the location for the album.")]
#endif
        public string Location 
        {
            get 
             {
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.Location);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Location, value);
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
        public uint NumPhotos 
        {
            get 
             {
                return Convert.ToUInt32(this.entry.getPhotoExtensionValue(GPhotoNameTable.NumPhotos));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.NumPhotos, Convert.ToString(value));
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
        public uint NumPhotosRemaining
        {
            get 
             {
                return Convert.ToUInt32(this.entry.getPhotoExtensionValue(GPhotoNameTable.NumPhotosRemaining));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.NumPhotosRemaining, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The name of the album, which is the URL-usable name 
        /// derived from the title. This is the name that should be used in all 
        /// URLs involving the album.
        /// </summary>
        
#if WindowsCE || PocketPC
#else
        [Category("Base Album Data"),
        Description("Specifies the name for the album.")]
#endif
        public string Name 
        {
            get 
             {
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.Name);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Name, value);
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
                return Convert.ToBoolean(this.entry.getPhotoExtensionValue(GPhotoNameTable.CommentingEnabled));
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.CommentCount, Utilities.ConvertBooleanToXSDString(value));
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
                return this.entry.getPhotoExtensionValue(GPhotoNameTable.Id);
            }
            set 
            {
                this.entry.setPhotoExtension(GPhotoNameTable.Id, value);
            }
        }
    }
}


