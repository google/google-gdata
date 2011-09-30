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
using System.IO;
using System.Collections;
using System.Text;
using System.Net; 
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Photos;
using Google.GData.Extensions.MediaRss;
using System.Collections.Generic;
using Google.GData.Extensions.Location;
using System.ComponentModel;

namespace Google.Picasa
{
    /// <summary>
    /// as all picasa entries are "similiar" we have this abstract baseclass here as 
    /// well, although it is not clear yet how this will benefit 
    /// </summary>
    /// <returns></returns>
    public abstract class PicasaEntity : Entry
    {
            /// <summary>
        /// readonly accessor for the AlbumEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public  PicasaEntry PicasaEntry
        {
            get
            {
                EnsureInnerObject();
                return this.AtomEntry as PicasaEntry;
            }
        }

    }


    public class Album : PicasaEntity
    {
        /// <summary>
        /// needs to be subclassed to ensure the creation of the corrent AtomEntry based
        /// object
        /// </summary>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new AlbumEntry();
            }
        }

        /// <summary>
        /// The album's access level. In this document, access level is also 
        /// referred to as "visibility." Valid values are public or private.
        /// </summary>
        [Category("Meta Album Data"),
        Description("Specifies the access for the album.")]
        public string Access 
        {
            get 
             {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Access);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Access, value);
            }
        }
        
        
        /// <summary>
        /// The nickname of the author
        /// </summary>
        [Category("Base Album Data"),
        Description("Specifies the author's nickname")]
        public string AlbumAuthorNickname
        {
            get 
            {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Nickname);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Nickname, value);
            }
        }

        /// <summary>
        /// The number of bytes of storage that this album uses.
        /// </summary>
        [Category("Meta Album Data"),
        Description("Specifies the bytes used for the album.")]
        [CLSCompliant(false)]
        public uint BytesUsed 
        {
            get 
             {
                return Convert.ToUInt32(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.BytesUsed));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.BytesUsed, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The user-specified location associated with the album
        /// </summary>
        [Category("Location Data"),
        Description("Specifies the location for the album.")]
        public string Location 
        {
            get 
             {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Location);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Location, value);
            }
        }

        /// <summary>
        /// the Longitude  of the photo
        /// </summary>
        [Category("Location Data"),
        Description("The longitude of the photo.")]
        public double Longitude 
        {
            get 
            {
                GeoRssWhere w = this.PicasaEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (w != null)
                {
                    return w.Longitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere w = this.PicasaEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (w == null)
                {
                    w = this.PicasaEntry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.PicasaEntry.ExtensionElements.Add(w);
                }
                w.Longitude = value; 
            }
        }
    
        /// <summary>
        /// the Longitude  of the photo
        /// </summary>
        [Category("Location Data"),
        Description("The Latitude of the photo.")]
        public double Latitude 
        {
            get 
            {
                GeoRssWhere w = this.PicasaEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (w != null)
                {
                    return w.Latitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere w = this.PicasaEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (w == null)
                {
                    w = this.PicasaEntry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.PicasaEntry.ExtensionElements.Add(w);
                }
                w.Latitude = value; 
            }
        }
  
        /// <summary>
        /// The number of photos in the album.
        /// </summary>
        /// 
        [Category("Meta Album Data"),
        Description("Specifies the number of photos in the album.")]
        [CLSCompliant(false)]
        public uint NumPhotos 
        {
            get 
             {
                return Convert.ToUInt32(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.NumPhotos));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.NumPhotos, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The number of remaining photo uploads allowed in this album. 
        /// This is equivalent to the user's maximum number of photos per 
        /// album (gphoto:maxPhotosPerAlbum) minus the number of photos 
        /// currently in the album (gphoto:numphotos).
        /// </summary>
        [Category("Meta Album Data"),
        Description("Specifies the number of remaining photo uploads for the album.")]
        [CLSCompliant(false)]
        public uint NumPhotosRemaining
        {
            get 
             {
                return Convert.ToUInt32(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.NumPhotosRemaining));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.NumPhotosRemaining, Convert.ToString(value));
            }
        }


        /// <summary>
        /// the number of comments on an album
        /// </summary>
        [Category("Commenting"),
        Description("Specifies the number of comments for the album.")]
        [CLSCompliant(false)]
        public uint CommentCount 
        {
            get 
            {
                return Convert.ToUInt32(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.CommentCount));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.CommentCount, Convert.ToString(value));
            }
        }

        /// <summary>
        /// is commenting enabled on an album
        /// </summary>
        [Category("Commenting"),
        Description("Comments enabled?")]
        public bool CommentingEnabled 
        {
            get 
            {
                return Convert.ToBoolean(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled, Utilities.ConvertBooleanToXSDString(value));
            }
        }

        /// <summary>
        /// the id of the album
        /// </summary>
        [Category("Base Album Data"),
        Description("Specifies the id for the album.")]
        public string Id 
        {
            get 
            {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Id);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Id, value);
            }
        }
    }


    /// <summary>
    /// The Tag is just a base PicasaEntity, beside the category it is not different 
    /// from a standard atom entry
    /// </summary>
    /// <returns></returns>
    public class Tag : PicasaEntity
    {
        /// <summary>
        /// needs to be subclassed to ensure the creation of the corrent AtomEntry based
        /// object
        /// </summary>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new TagEntry();
            }
        }
    }

    /// <summary>
    /// a comment object for a picasa photo
    /// </summary>
    /// <returns></returns>
    public class Comment : PicasaEntity
    {
        /// <summary>
        /// needs to be subclassed to ensure the creation of the corrent AtomEntry based
        /// object
        /// </summary>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new CommentEntry();
            }
        }

    
        /// <summary>
        /// The ID of the photo associated with the current comment.
        /// </summary>
        [Category("Base Comment Data"),
        Description("The ID of the photo associated with the current comment.")]
        public string PhotoId 
        {
            get 
            {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Photoid);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Photoid, value);
            }
        }

        /// <summary>
        /// The albums ID
        /// </summary>
        [Category("Base Comment Data"),
        Description("The ID of the album associated with the current comment.")]
        public string AlbumId 
        {
            get 
            {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.AlbumId);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.AlbumId, value);
            }
        }

        /// <summary>
        /// the id of the comment
        /// </summary>
        [Category("Base Comment Data"),
        Description("The ID of the comment itself.")]
        public string CommentId 
        {
            get 
            {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Id);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Id, value);
            }
        }
    }

    /// <summary>
    /// represents a photo based on a PicasaEntry object
    /// </summary>
    public class Photo : PicasaEntity
    {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new PhotoEntry();
            }
        }

        /// <summary>
        /// tries to construct an URI on the Url attribute in media.content
        /// </summary>
        /// <returns>a Uri object or null</returns>
        [Category("Base Photo Data"),
        Description("Returns the URL to the photo media.")]
        public Uri PhotoUri
        {
            get
            {
                EnsureInnerObject();
                if (this.PicasaEntry.Media != null &&
                    this.PicasaEntry.Media.Content != null) 
                {
                    return new Uri(this.PicasaEntry.Media.Content.Attributes["url"] as string);
                }
                return null;
            }
            set
            {
                EnsureMediaContent();
                this.PicasaEntry.Media.Content.Attributes["url"] = value.AbsoluteUri;
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
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Checksum);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Checksum, value);
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
                return Convert.ToInt32(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Height));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Height, Convert.ToString(value));
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
                return Convert.ToInt32(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Width));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Width, Convert.ToString(value));
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
                return Convert.ToInt32(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Rotation));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Rotation, Convert.ToString(value));
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
                return Convert.ToInt32(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Size));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Size, Convert.ToString(value));
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
                return Convert.ToUInt64(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Timestamp));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Timestamp, Convert.ToString(value));
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
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.AlbumId);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.AlbumId, value);
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
                return Convert.ToUInt32(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.CommentCount));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.CommentCount, Convert.ToString(value));
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
                return Convert.ToBoolean(this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled));
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled, Utilities.ConvertBooleanToXSDString(value));
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
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Id);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.Id, value);
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
                GeoRssWhere where = this.PicasaEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where != null)
                {
                    return where.Longitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere where = this.PicasaEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where == null)
                {
                    where = this.PicasaEntry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.PicasaEntry.ExtensionElements.Add(where);
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
                GeoRssWhere where = this.PicasaEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where != null)
                {
                    return where.Latitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere where = this.PicasaEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where == null)
                {
                    where = this.PicasaEntry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.PicasaEntry.ExtensionElements.Add(where);
                }
                where.Latitude = value; 
            }
        }

        /// <summary>
        /// Description of the album this photo is in.
        /// </summary>
        [Category("Base Photo Data"),
        Description("Description of the album this photo is in.")]
        public string AlbumDescription
        {
            get 
            {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.AlbumDesc);
            }
            set 
            {
                this.PicasaEntry.SetPhotoExtensionValue(GPhotoNameTable.AlbumDesc, value);
            }
        }

        /// <summary>
        /// Snippet that matches the search text.
        /// </summary>
        [Category("Search Photo Data"),
        Description("Snippet that matches the search text.")]
        public string Snippet
        {
            get 
            {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Snippet);
            }
        }


        /// <summary>
        /// Describes where the match with the search query was found, and thus where 
        /// the snippet is from: the photo caption, the photo tags, the album title, 
        /// the album description, or the album location.   
        /// Possible values are PHOTO_DESCRIPTION, PHOTO_TAGS, ALBUM_TITLE, 
        /// ALBUM_DESCRIPTION, or ALBUM_LOCATION.
        /// </summary>
        [Category("Search Photo Data"),
        Description("Describes where the match with the search query was found.")]
        public string SnippetType
        {
            get 
            {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.SnippetType);
            }
        }


        /// <summary>
        /// Indicates whether search results are truncated or not. 
        /// Possible values are 1 (results are truncated) or 0 (results are not truncated).
        /// </summary>
        [Category("Search Photo Data"),
        Description("Indicates whether search results are truncated or not.")]
        public string Truncated
        {
            get 
            {
                return this.PicasaEntry.GetPhotoExtensionValue(GPhotoNameTable.Truncated);
            }
        }

        private void EnsureMediaContent()
        {
            EnsureInnerObject();
            if (this.PicasaEntry.Media == null)
            {
                this.PicasaEntry.Media = new MediaGroup();
            }
            if (this.PicasaEntry.Media.Content == null)
            {
                this.PicasaEntry.Media.Content = new MediaContent();
            }
        }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// The Picasa Web Albums Data API allows client applications to view and 
    /// update albums, photos, and comments in the form of Google Data API feeds.
    /// Your client application can use the Picasa Web Albums Data API to create 
    /// new albums, upload photos, add comments, edit or delete existing albums,
    ///  photos, and comments, and query for items that match particular criteria.
    /// </summary>
    ///  <example>
    ///         The following code illustrates a possible use of   
    ///          the <c>PicasaRequest</c> object:  
    ///          <code>    
    ///            RequestSettings settings = new RequestSettings("yourApp");
    ///            settings.PageSize = 50; 
    ///            settings.AutoPaging = true;
    ///            PicasaRequest pr = new PicasaRequest(settings);
    ///            Feed&lt;Photo&gt; feed = c.GetPhotos();
    ///     
    ///         foreach (Photo p in feed.Entries)
    ///         {
    ///              Console.WriteLine(d.Title);
    ///         }
    ///  </code>
    ///  </example>
    //////////////////////////////////////////////////////////////////////
    public class PicasaRequest : FeedRequest<PicasaService>
    {
        /// <summary>
        /// default constructor for a PicasaRequest
        /// </summary>
        /// <param name="settings"></param>
        public PicasaRequest(RequestSettings settings) : base(settings)
        {
            this.Service = new PicasaService(settings.Application);
            PrepareService();
        }


        /// <summary>
        /// returns the list of Albums for the default user
        /// </summary>
        public Feed<Album> GetAlbums()
        {
            AlbumQuery q = PrepareQuery<AlbumQuery>(PicasaQuery.CreatePicasaUri(Utilities.DefaultUser));
            return PrepareFeed<Album>(q); 
        }


        /// <summary>
        /// returns a Feed of all photos for the authorized user
        /// </summary>
        /// <returns>a feed of everyting</returns>
        public Feed<Photo> GetPhotos()
        {
            PhotoQuery q = PrepareQuery<PhotoQuery>(PicasaQuery.CreatePicasaUri(Utilities.DefaultUser));
            return PrepareFeed<Photo>(q); 
        }

       
        /// <summary>
        /// returns a feed of photos in that particular album for the default user
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public Feed<Photo> GetPhotosInAlbum(string albumId)
        {
            PhotoQuery q = PrepareQuery<PhotoQuery>(PicasaQuery.CreatePicasaUri(Utilities.DefaultUser, albumId));
            return PrepareFeed<Photo>(q); 
        }


        /// <summary>
        /// Returns the comments a single photo based on
        /// the default user, the albumid and the photoid
        /// </summary>
        /// <param name="albumId">The Id of the Album</param>
        /// <param name="photoId">The id of the photo</param>
        /// <returns></returns>
        public Feed<Comment> GetComments(string albumId, string photoId)
        {
            CommentsQuery q = PrepareQuery<CommentsQuery>(PicasaQuery.CreatePicasaUri(Utilities.DefaultUser, albumId, photoId));
            return PrepareFeed<Comment>(q); 
        }


        /// <summary>
        /// Get all Tags for the default user
        /// </summary>
        /// <returns></returns>
        public Feed<Tag> GetTags()
        {
            TagQuery q = PrepareQuery<TagQuery>(PicasaQuery.CreatePicasaUri(Utilities.DefaultUser));
            return PrepareFeed<Tag>(q); 
        }

 
        /// <summary>
        /// downloads a photo. 
        /// </summary>
        /// <param name="photo">The photo to download. 
        /// <returns>either a stream to the photo, or NULL if the photo has no media URL set</returns>
        public Stream Download(Photo photo)
        {
            Service s = this.Service;
            Uri target = photo.PhotoUri;
            if (target != null)
            {
                return s.Query(target);
            }
            return null;
        }
    }
}
