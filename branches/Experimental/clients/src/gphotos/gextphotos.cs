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
//using System.Collections;
//using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Photos {

    /// <summary>
    /// helper to instantiate all factories defined in here and attach 
    /// them to a base object
    /// </summary> 
    public static class GPhotoExtensions 
    {
        /// <summary>
        /// helper to add all picasa photo extensions to the base object
        /// </summary>
        /// <param name="baseObject"></param>
        public static void AddExtension(AtomBase baseObject) 
        {
            baseObject.AddExtension(new GPhotoAlbumId());
            baseObject.AddExtension(new GPhotoCommentCount());
            baseObject.AddExtension(new GPhotoCommentingEnabled());
            baseObject.AddExtension(new GPhotoId());
            baseObject.AddExtension(new GPhotoMaxPhotosPerAlbum());
            baseObject.AddExtension(new GPhotoNickName());
            baseObject.AddExtension(new GPhotoQuotaCurrent());
            baseObject.AddExtension(new GPhotoQuotaLimit());
            baseObject.AddExtension(new GPhotoThumbnail());
            baseObject.AddExtension(new GPhotoUser());
            baseObject.AddExtension(new GPhotoAccess());
            baseObject.AddExtension(new GPhotoBytesUsed());
            baseObject.AddExtension(new GPhotoLocation());
            baseObject.AddExtension(new GPhotoNumPhotos());
            baseObject.AddExtension(new GPhotoNumPhotosRemaining());
            baseObject.AddExtension(new GPhotoChecksum());
            baseObject.AddExtension(new GPhotoHeight());
            baseObject.AddExtension(new GPhotoRotation());
            baseObject.AddExtension(new GPhotoSize());
            baseObject.AddExtension(new GPhotoTimestamp());
            baseObject.AddExtension(new GPhotoWidth());
            baseObject.AddExtension(new GPhotoPhotoId());
            baseObject.AddExtension(new GPhotoWeight());
            baseObject.AddExtension(new GPhotoAlbumDesc());
            baseObject.AddExtension(new GPhotoAlbumTitle());
            baseObject.AddExtension(new GPhotoSnippet());
            baseObject.AddExtension(new GPhotoSnippetType());
            baseObject.AddExtension(new GPhotoTruncated());
        }
    }

    /// <summary>
    /// short table to hold the namespace and the prefix
    /// </summary>
    public static class GPhotoNameTable 
    {
        /// <summary>static string to specify the GeoRSS namespace supported</summary>
        public const string NSGPhotos = "http://schemas.google.com/photos/2007"; 
        /// <summary>static string to specify the Google Picasa prefix used</summary>
        public const string gPhotoPrefix = "gphoto"; 

        /// <summary>
        /// Comment Kind definition
        /// this is the term value for a category
        /// </summary>
        public const string CommentKind = NSGPhotos + "#comment";
        /// <summary>
        /// Photo Kind definition
        /// this is the term value for a category
        /// </summary>
        public const string PhotoKind = NSGPhotos + "#photo";
        /// <summary>
        /// Album Kind definition
        /// this is the term value for a category
        /// </summary>
        public const string AlbumKind = NSGPhotos + "#album";
        /// <summary>
        /// Tag Kind definition
        /// this is the term value for a category
        /// </summary>
        public const string TagKind = NSGPhotos + "#tag";

        /// <summary>
        /// id element string
        /// </summary>
        public const string Id = "id";

        /// <summary>
        /// album id element string
        /// </summary>
        public const string AlbumId = "albumid";

        /// <summary>
        /// comment count element string
        /// </summary>
        public const string CommentCount = "commentCount";

        /// <summary>
        /// commenting enabled element string
        /// </summary>
        public const string CommentingEnabled = "commentingEnabled";

        /// <summary>
        /// maximal photos per album element string
        /// </summary>
        public const string MaxPhotosPerAlbum = "maxPhotosPerAlbum";

        /// <summary>
        /// nickname element string
        /// </summary>
        public const string Nickname = "nickname";

        /// <summary>
        /// current Quota element string
        /// </summary>
        public const string QuotaCurrent = "quotacurrent";

        /// <summary>
        ///  Quota Limit element string
        /// </summary>
        public const string QuotaLimit = "quotalimit";

        /// <summary>
        /// Thumbnail element string
        /// </summary>
        public const string Thumbnail = "thumbnail";

        /// <summary>
        /// User element string
        /// </summary>
        public const string User = "user";

        /// <summary>
        /// access element string
        /// </summary>
        public const string Access = "access";

        /// <summary>
        /// bytesUsed element string
        /// </summary>
        public const string BytesUsed = "bytesUsed";

        /// <summary>
        /// location element string
        /// </summary>
        public const string Location = "location";


        /// <summary>
        /// numphotos element string
        /// </summary>
        public const string NumPhotos = "numphotos";

        /// <summary>
        /// numphotosremaining element string
        /// </summary>
        public const string NumPhotosRemaining = "numphotosremaining";

        /// <summary>
        /// numphotos element string
        /// </summary>
        public const string Checksum = "checksum";

        /// <summary>
        /// client element string
        /// </summary>
        public const string Client = "client";

        /// <summary>
        /// height element string
        /// </summary>
        public const string Height = "height";

        /// <summary>
        /// position element string
        /// </summary>
        public const string Position = "position";

        /// <summary>
        /// rotation element string
        /// </summary>
        public const string Rotation = "rotation";

        /// <summary>
        /// size element string
        /// </summary>
        public const string Size = "size";

        /// <summary>
        /// timestamp element string
        /// </summary>
        public const string Timestamp = "timestamp";

        /// <summary>
        /// version element string
        /// </summary>
        public const string Version = "version";

        /// <summary>
        /// photoid element string
        /// </summary>
        public const string Photoid = "photoid";

        /// <summary>
        /// width element string
        /// </summary>
        public const string Width = "width";

        /// <summary>
        /// weight element string
        /// </summary>
        public const string Weight = "weight";

        /// <summary>
        /// Description of the album this photo is in.
        /// </summary>
        public const string AlbumDesc = "albumdesc";
        /// <summary>
        /// Title of the album this photo is in.
        /// </summary>
        public const string AlbumTitle = "albumtitle";
        /// <summary>
        /// Snippet that matches the search text.
        /// </summary>
        public const string Snippet = "snippet";
        /// <summary>
        /// Describes where the match with the search query was found, and thus where the snippet 
        /// is from: the photo caption, the photo tags, the album title, 
        /// the album description, or the album location.
        /// </summary>
        public const string SnippetType = "snippettype";
        /// <summary>
        /// Indicates whether search results are truncated or not
        /// </summary>
        public const string Truncated = "truncated";

    }


    /// <summary>
    /// id schema extension describing an ID.
    /// </summary>
    public class GPhotoId : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoId()
        : base(GPhotoNameTable.Id, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
        {}

        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoId(string initValue)
        : base(GPhotoNameTable.Id, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoAlbumId schema extension describing an albumid
    /// </summary>
    public class GPhotoAlbumId : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoAlbumId()
        : base(GPhotoNameTable.AlbumId, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoAlbumId(string initValue)
        : base(GPhotoNameTable.AlbumId, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoCommentCount schema extension describing an commentCount
    /// </summary>
    public class GPhotoCommentCount : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoCommentCount()
        : base(GPhotoNameTable.CommentCount, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoCommentCount(string initValue)
        : base(GPhotoNameTable.CommentCount, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoCommentingEnabled schema extension describing an commentingEnabled
    /// </summary>
    public class GPhotoCommentingEnabled : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoCommentingEnabled()
        : base(GPhotoNameTable.CommentingEnabled, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoCommentingEnabled(string initValue)
        : base(GPhotoNameTable.CommentingEnabled, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoMaxPhotosPerAlbum schema extension describing an maxPhotosPerAlbum
    /// </summary>
    public class GPhotoMaxPhotosPerAlbum : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoMaxPhotosPerAlbum()
        : base(GPhotoNameTable.MaxPhotosPerAlbum, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoMaxPhotosPerAlbum(string initValue)
        : base(GPhotoNameTable.MaxPhotosPerAlbum, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoNickName schema extension describing an nickname
    /// </summary>
    public class GPhotoNickName : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoNickName()
        : base(GPhotoNameTable.Nickname, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoNickName(string initValue)
        : base(GPhotoNameTable.Nickname, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoQuotaCurrent schema extension describing an quotacurrent
    /// </summary>
    public class GPhotoQuotaCurrent : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoQuotaCurrent()
        : base(GPhotoNameTable.QuotaCurrent, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoQuotaCurrent(string initValue)
        : base(GPhotoNameTable.QuotaCurrent, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoQuotaLimit schema extension describing an quotalimit
    /// </summary>
    public class GPhotoQuotaLimit : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoQuotaLimit()
        : base(GPhotoNameTable.QuotaLimit, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoQuotaLimit(string initValue)
        : base(GPhotoNameTable.QuotaLimit, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoThumbnail schema extension describing an thumbnail
    /// </summary>
    public class GPhotoThumbnail : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoThumbnail()
        : base(GPhotoNameTable.Thumbnail, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoThumbnail(string initValue)
        : base(GPhotoNameTable.Thumbnail, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoUser schema extension describing an user
    /// </summary>
    public class GPhotoUser : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoUser()
        : base(GPhotoNameTable.User, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoUser(string initValue)
        : base(GPhotoNameTable.User, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoAccess schema extension describing an access
    /// </summary>
    public class GPhotoAccess : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoAccess()
        : base(GPhotoNameTable.Access, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoAccess(string initValue)
        : base(GPhotoNameTable.Access, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoBytesUsed schema extension describing an bytesUsed
    /// </summary>
    public class GPhotoBytesUsed : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoBytesUsed()
        : base(GPhotoNameTable.BytesUsed, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoBytesUsed(string initValue)
        : base(GPhotoNameTable.BytesUsed, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoLocation schema extension describing an location
    /// </summary>
    public class GPhotoLocation : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoLocation()
        : base(GPhotoNameTable.Location, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoLocation(string initValue)
        : base(GPhotoNameTable.Location, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }


    /// <summary>
    /// GPhotoNumPhotos schema extension describing an numphotos
    /// </summary>
    public class GPhotoNumPhotos : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoNumPhotos()
        : base(GPhotoNameTable.NumPhotos, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoNumPhotos(string initValue)
        : base(GPhotoNameTable.NumPhotos, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoNumPhotosRemaining schema extension describing an numphotosremaining
    /// </summary>
    public class GPhotoNumPhotosRemaining : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoNumPhotosRemaining()
        : base(GPhotoNameTable.NumPhotosRemaining, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoNumPhotosRemaining(string initValue)
        : base(GPhotoNameTable.NumPhotosRemaining, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoChecksum schema extension describing an checksum
    /// </summary>
    public class GPhotoChecksum : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoChecksum()
        : base(GPhotoNameTable.Checksum, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoChecksum(string initValue)
        : base(GPhotoNameTable.Checksum, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoHeight schema extension describing an height
    /// </summary>
    public class GPhotoHeight : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoHeight()
        : base(GPhotoNameTable.Height, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoHeight(string initValue)
        : base(GPhotoNameTable.Height, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }


    /// <summary>
    /// GPhotoRotation schema extension describing an rotation
    /// </summary>
    public class GPhotoRotation : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoRotation()
        : base(GPhotoNameTable.Rotation, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoRotation(string initValue)
        : base(GPhotoNameTable.Rotation, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoSize schema extension describing an size
    /// </summary>
    public class GPhotoSize : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoSize()
        : base(GPhotoNameTable.Size, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoSize(string initValue)
        : base(GPhotoNameTable.Size, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoTimestamp schema extension describing an timestamp
    /// </summary>
    public class GPhotoTimestamp : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoTimestamp()
        : base(GPhotoNameTable.Timestamp, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoTimestamp(string initValue)
        : base(GPhotoNameTable.Timestamp, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }



    /// <summary>
    /// GPhotoPhotoId schema extension describing an photoid
    /// </summary>
    public class GPhotoPhotoId : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoPhotoId()
        : base(GPhotoNameTable.Photoid, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoPhotoId(string initValue)
        : base(GPhotoNameTable.Photoid, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoWidth schema extension describing an width
    /// </summary>
    public class GPhotoWidth : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoWidth()
        : base(GPhotoNameTable.Width, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoWidth(string initValue)
        : base(GPhotoNameTable.Width, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoWeight schema extension describing an weight
    /// </summary>
    public class GPhotoWeight : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoWeight()
        : base(GPhotoNameTable.Weight, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoWeight(string initValue)
        : base(GPhotoNameTable.Weight, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// Description of the album this photo is in.
    /// </summary>
    public class GPhotoAlbumDesc : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoAlbumDesc()
        : base(GPhotoNameTable.AlbumDesc, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoAlbumDesc(string initValue)
        : base(GPhotoNameTable.AlbumDesc, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// Title of the album this photo is in.
    /// </summary>
    public class GPhotoAlbumTitle : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoAlbumTitle()
        : base(GPhotoNameTable.AlbumTitle, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoAlbumTitle(string initValue)
        : base(GPhotoNameTable.AlbumTitle, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// Snippet that matches the search text.
    /// </summary>
    public class GPhotoSnippet : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoSnippet()
        : base(GPhotoNameTable.Snippet, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoSnippet(string initValue)
        : base(GPhotoNameTable.Snippet, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// Describes where the match with the search query was found, and thus where the snippet 
    /// is from: the photo caption, the photo tags, the album title, 
    /// the album description, or the album location.
    /// Possible values are PHOTO_DESCRIPTION, PHOTO_TAGS, ALBUM_TITLE, 
    /// ALBUM_DESCRIPTION, or ALBUM_LOCATION.
    /// </summary>
    public class GPhotoSnippetType : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoSnippetType()
        : base(GPhotoNameTable.SnippetType, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoSnippetType(string initValue)
        : base(GPhotoNameTable.SnippetType, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// Indicates whether search results are truncated or not
    /// Possible values are 1 (results are truncated) or 0 (results are not truncated).
    /// </summary>
    public class GPhotoTruncated : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public GPhotoTruncated()
        : base(GPhotoNameTable.Truncated, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public GPhotoTruncated(string initValue)
        : base(GPhotoNameTable.Truncated, GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }
}
