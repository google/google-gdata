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

#if WindowsCE || PocketPC
#else
using System.ComponentModel;
#endif


namespace Google.Picasa
{


    /// <summary>
    /// as all picasa entries are "similiar" we have this abstract baseclass here as 
    /// well, although it is not clear yet how this will benefit 
    /// </summary>
    /// <returns></returns>
    public abstract class PicasaEntity : Entry
    {
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
        /// readonly accessor for the AlbumEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public  AlbumEntry AlbumEntry
        {
            get
            {
                EnsureInnerObject();
                return this.AtomEntry as AlbumEntry;
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
                return this.AlbumEntry.GetPhotoExtensionValue(GPhotoNameTable.Access);
            }
            set 
            {
                this.AlbumEntry.SetPhotoExtensionValue(GPhotoNameTable.Access, value);
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
                return this.AlbumEntry.GetPhotoExtensionValue(GPhotoNameTable.Nickname);
            }
            set 
            {
                this.AlbumEntry.SetPhotoExtensionValue(GPhotoNameTable.Nickname, value);
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
                return Convert.ToUInt32(this.AlbumEntry.GetPhotoExtensionValue(GPhotoNameTable.BytesUsed));
            }
            set 
            {
                this.AlbumEntry.SetPhotoExtensionValue(GPhotoNameTable.BytesUsed, Convert.ToString(value));
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
                return this.AlbumEntry.GetPhotoExtensionValue(GPhotoNameTable.Location);
            }
            set 
            {
                this.AlbumEntry.SetPhotoExtensionValue(GPhotoNameTable.Location, value);
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
                GeoRssWhere where = this.AlbumEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where != null)
                {
                    return where.Longitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere where = this.AlbumEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where == null)
                {
                    where = AlbumEntry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.AlbumEntry.ExtensionElements.Add(where);
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
                GeoRssWhere where = this.AlbumEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where != null)
                {
                    return where.Latitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere where = this.AlbumEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where == null)
                {
                    where = AlbumEntry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.AlbumEntry.ExtensionElements.Add(where);
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
                return Convert.ToUInt32(this.AlbumEntry.GetPhotoExtensionValue(GPhotoNameTable.NumPhotos));
            }
            set 
            {
                this.AlbumEntry.SetPhotoExtensionValue(GPhotoNameTable.NumPhotos, Convert.ToString(value));
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
                return Convert.ToUInt32(this.AlbumEntry.GetPhotoExtensionValue(GPhotoNameTable.NumPhotosRemaining));
            }
            set 
            {
                this.AlbumEntry.SetPhotoExtensionValue(GPhotoNameTable.NumPhotosRemaining, Convert.ToString(value));
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
                return Convert.ToUInt32(this.AlbumEntry.GetPhotoExtensionValue(GPhotoNameTable.CommentCount));
            }
            set 
            {
                this.AlbumEntry.SetPhotoExtensionValue(GPhotoNameTable.CommentCount, Convert.ToString(value));
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
                return Convert.ToBoolean(this.AlbumEntry.GetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled));
            }
            set 
            {
                this.AlbumEntry.SetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled, Utilities.ConvertBooleanToXSDString(value));
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
                return this.AlbumEntry.GetPhotoExtensionValue(GPhotoNameTable.Id);
            }
            set 
            {
                this.AlbumEntry.SetPhotoExtensionValue(GPhotoNameTable.Id, value);
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
        /// readonly accessor for the CommentEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public  CommentEntry CommentEntry
        {
            get
            {
                EnsureInnerObject();
                return this.AtomEntry as CommentEntry;
            }
        }


        /// <summary>
        /// The ID of the photo associated with the current comment.
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Base Comment Data"),
        Description("The ID of the photo associated with the current comment.")]
#endif
         public string PhotoId 
        {
            get 
            {
                return this.CommentEntry.GetPhotoExtensionValue(GPhotoNameTable.Photoid);
            }
            set 
            {
                this.CommentEntry.SetPhotoExtensionValue(GPhotoNameTable.Photoid, value);
            }
        }

        /// <summary>
        /// The albums ID
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Base Comment Data"),
        Description("The ID of the album associated with the current comment.")]
#endif
        public string AlbumId 
        {
            get 
            {
                return this.CommentEntry.GetPhotoExtensionValue(GPhotoNameTable.AlbumId);
            }
            set 
            {
                this.CommentEntry.SetPhotoExtensionValue(GPhotoNameTable.AlbumId, value);
            }
        }

        /// <summary>
        /// the id of the comment
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Base Comment Data"),
        Description("The ID of the comment itself.")]
#endif
        public string CommentId 
        {
            get 
            {
                return this.CommentEntry.GetPhotoExtensionValue(GPhotoNameTable.Id);
            }
            set 
            {
                this.CommentEntry.SetPhotoExtensionValue(GPhotoNameTable.Id, value);
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
        /// readonly accessor for the PhotoEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public  PhotoEntry PhotoEntry
        {
            get
            {
                EnsureInnerObject();
                return this.AtomEntry as PhotoEntry;
            }
        }

        /// <summary>
        /// tries to construct an URI on the Url attribute in media.content
        /// </summary>
        /// <returns>a Uri object or null</returns>
#if WindowsCE || PocketPC
#else
        [Category("Base Photo Data"),
        Description("Returns the URL to the photo media.")]
#endif
        public Uri PhotoUri
        {
            get
            {
                EnsureInnerObject();
                if (this.PhotoEntry.Media != null &&
                    this.PhotoEntry.Media.Content != null) 
                {
                    return new Uri(this.PhotoEntry.Media.Content.Attributes["url"] as string);
                }
                return null;
            }
            set
            {
                EnsureMediaContent();
                this.PhotoEntry.Media.Content.Attributes["url"] = value.AbsoluteUri;
            }
        }



 
        /// <summary>
        /// The checksum on the photo. This optional field can be used by 
        /// uploaders to associate a checksum with a photo to ease duplicate detection
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("The checksum on the photo.")]
#endif
        public string Checksum 
        {
            get 
            {
                return this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.Checksum);
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.Checksum, value);
            }
        }

      

        /// <summary>
        /// The height of the photo in pixels
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Basic Photo Data"),
        Description("The height of the photo in pixels.")]
#endif
        public int Height 
        {
            get 
            {
                return Convert.ToInt32(this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.Height));
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.Height, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The width of the photo in pixels
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Basic Photo Data"),
        Description("The width of the photo in pixels.")]
#endif
        public int Width 
        {
            get 
            {
                return Convert.ToInt32(this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.Width));
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.Width, Convert.ToString(value));
            }
        }


  

        /// <summary>
        /// The rotation of the photo in degrees, used to change the rotation of the photo. Will only be shown if 
        /// the rotation has not already been applied to the requested images.
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Basic Photo Data"),
        Description("The rotation of the photo in degrees.")]
#endif
        public int Rotation 
        {
            get 
            {
                return Convert.ToInt32(this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.Rotation));
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.Rotation, Convert.ToString(value));
            }
        }

        /// <summary>
        /// The size of the photo in bytes
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Basic Photo Data"),
        Description("The size of the photo in bytes.")]
#endif
        public long Size 
        {
            get 
            {
                return Convert.ToInt32(this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.Size));
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.Size, Convert.ToString(value));
            }
        }


        /// <summary>
        /// The photo's timestamp, represented as the number of milliseconds since 
        /// January 1st, 1970. Contains the date of the photo either set externally
        /// or retrieved from the Exif data.
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("The photo's timestamp")]
#endif
        [CLSCompliant(false)]
        public ulong Timestamp 
        {
            get 
            {
                return Convert.ToUInt64(this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.Timestamp));
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.Timestamp, Convert.ToString(value));
            }
        }


 
        /// <summary>
        /// The albums ID
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("The albums ID.")]
#endif
        public string AlbumId 
        {
            get 
            {
                return this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.AlbumId);
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.AlbumId, value);
            }
        }

        /// <summary>
        /// the number of comments on a photo
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Commenting"),
        Description("the number of comments on a photo.")]
#endif
        [CLSCompliant(false)]
        public uint CommentCount 
        {
            get 
            {
                return Convert.ToUInt32(this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.CommentCount));
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.CommentCount, Convert.ToString(value));
            }
        }

        /// <summary>
        /// is commenting enabled on a photo
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Commenting"),
        Description("is commenting enabled on a photo.")]
#endif
        public bool CommentingEnabled 
        {
            get 
            {
                return Convert.ToBoolean(this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled));
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.CommentingEnabled, Utilities.ConvertBooleanToXSDString(value));
            }
        }

    
        /// <summary>
        /// the id of the photo
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Meta Photo Data"),
        Description("the id of the photo.")]
#endif
        public string Id 
        {
            get 
            {
                return this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.Id);
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.Id, value);
            }
        }

        /// <summary>
        /// the Longitude  of the photo
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Location Photo Data"),
        Description("The longitude of the photo.")]
#endif
        public double Longitude 
        {
            get 
            {
                GeoRssWhere where = this.PhotoEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where != null)
                {
                    return where.Longitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere where = this.PhotoEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where == null)
                {
                    where = this.PhotoEntry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.PhotoEntry.ExtensionElements.Add(where);
                }
                where.Longitude = value; 
            }
        }
    
        /// <summary>
        /// the Longitude  of the photo
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Location Photo Data"),
        Description("The Latitude of the photo.")]
#endif
        public double Latitude 
        {
            get 
            {
                GeoRssWhere where = this.PhotoEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where != null)
                {
                    return where.Latitude;
                }
                return -1; 
            }
            set 
            {
                GeoRssWhere where = this.PhotoEntry.FindExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                if (where == null)
                {
                    where = this.PhotoEntry.CreateExtension(GeoNametable.GeoRssWhereElement, GeoNametable.NSGeoRss) as GeoRssWhere;
                    this.PhotoEntry.ExtensionElements.Add(where);
                }
                where.Latitude = value; 
            }
        }

        /// <summary>
        /// Description of the album this photo is in.
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Base Photo Data"),
        Description("Description of the album this photo is in.")]
#endif
        public string AlbumDescription
        {
            get 
            {
                return this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.AlbumDesc);
            }
            set 
            {
                this.PhotoEntry.SetPhotoExtensionValue(GPhotoNameTable.AlbumDesc, value);
            }
        }

        /// <summary>
        /// Snippet that matches the search text.
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Search Photo Data"),
        Description("Snippet that matches the search text.")]
#endif
        public string Snippet
        {
            get 
            {
                return this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.Snippet);
            }
        }


        /// <summary>
        /// Describes where the match with the search query was found, and thus where 
        /// the snippet is from: the photo caption, the photo tags, the album title, 
        /// the album description, or the album location.   
        /// Possible values are PHOTO_DESCRIPTION, PHOTO_TAGS, ALBUM_TITLE, 
        /// ALBUM_DESCRIPTION, or ALBUM_LOCATION.
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Search Photo Data"),
        Description("Describes where the match with the search query was found.")]
#endif
        public string SnippetType
        {
            get 
            {
                return this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.SnippetType);
            }
        }


        /// <summary>
        /// Indicates whether search results are truncated or not. 
        /// Possible values are 1 (results are truncated) or 0 (results are not truncated).
        /// </summary>
#if WindowsCE || PocketPC
#else
        [Category("Search Photo Data"),
        Description("Indicates whether search results are truncated or not.")]
#endif
        public string Truncated
        {
            get 
            {
                return this.PhotoEntry.GetPhotoExtensionValue(GPhotoNameTable.Truncated);
            }
        }



        private void EnsureMediaContent()
        {
            EnsureInnerObject();
            if (this.PhotoEntry.Media == null)
            {
                this.PhotoEntry.Media = new MediaGroup();
            }
            if (this.PhotoEntry.Media.Content == null)
            {
                this.PhotoEntry.Media.Content = new MediaContent();
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
