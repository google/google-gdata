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

    public class GPhotoExtensions 
    {
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
            baseObject.AddExtension(new GPhotoClient());
            baseObject.AddExtension(new GPhotoHeight());
            baseObject.AddExtension(new GPhotoPosition());
            baseObject.AddExtension(new GPhotoRotation());
            baseObject.AddExtension(new GPhotoSize());
            baseObject.AddExtension(new GPhotoTimestamp());
            baseObject.AddExtension(new GPhotoVersion());
            baseObject.AddExtension(new GPhotoWidth());
            baseObject.AddExtension(new GPhotoPhotoId());
            baseObject.AddExtension(new GPhotoWeight());
            baseObject.AddExtension(new GPhotoName());

        }
    }

    public class GPhotoNameTable 
    {
        /// <summary>static string to specify the GeoRSS namespace supported</summary>
        public const string NSGPhotos = "http://schemas.google.com/photos/2007"; 
        /// <summary>static string to specify the Google Picasa prefix used</summary>
        public const string gPhotoPrefix = "gphoto"; 
    }


    /// <summary>
    /// id schema extension describing an ID.
    /// </summary>
    public class GPhotoId : SimpleElement
    {
        public GPhotoId()
        : base("id", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
        {}

        public GPhotoId(string initValue)
        : base("id", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoAlbumId schema extension describing an albumid
    /// </summary>
    public class GPhotoAlbumId : SimpleElement
    {
        public GPhotoAlbumId()
        : base("albumid", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoAlbumId(string initValue)
        : base("albumid", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoCommentCount schema extension describing an commentCount
    /// </summary>
    public class GPhotoCommentCount : SimpleElement
    {
        public GPhotoCommentCount()
        : base("commentCount", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoCommentCount(string initValue)
        : base("commentCount", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoCommentingEnabled schema extension describing an commentingEnabled
    /// </summary>
    public class GPhotoCommentingEnabled : SimpleElement
    {
        public GPhotoCommentingEnabled()
        : base("commentingEnabled", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoCommentingEnabled(string initValue)
        : base("commentingEnabled", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoMaxPhotosPerAlbum schema extension describing an maxPhotosPerAlbum
    /// </summary>
    public class GPhotoMaxPhotosPerAlbum : SimpleElement
    {
        public GPhotoMaxPhotosPerAlbum()
        : base("maxPhotosPerAlbum", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoMaxPhotosPerAlbum(string initValue)
        : base("maxPhotosPerAlbum", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoNickName schema extension describing an nickname
    /// </summary>
    public class GPhotoNickName : SimpleElement
    {
        public GPhotoNickName()
        : base("nickname", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoNickName(string initValue)
        : base("nickname", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoQuotaCurrent schema extension describing an quotacurrent
    /// </summary>
    public class GPhotoQuotaCurrent : SimpleElement
    {
        public GPhotoQuotaCurrent()
        : base("quotacurrent", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoQuotaCurrent(string initValue)
        : base("quotacurrent", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoQuotaLimit schema extension describing an quotalimit
    /// </summary>
    public class GPhotoQuotaLimit : SimpleElement
    {
        public GPhotoQuotaLimit()
        : base("quotalimit", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoQuotaLimit(string initValue)
        : base("quotalimit", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoThumbnail schema extension describing an thumbnail
    /// </summary>
    public class GPhotoThumbnail : SimpleElement
    {
        public GPhotoThumbnail()
        : base("thumbnail", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoThumbnail(string initValue)
        : base("thumbnail", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoUser schema extension describing an user
    /// </summary>
    public class GPhotoUser : SimpleElement
    {
        public GPhotoUser()
        : base("user", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoUser(string initValue)
        : base("user", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoAccess schema extension describing an access
    /// </summary>
    public class GPhotoAccess : SimpleElement
    {
        public GPhotoAccess()
        : base("access", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoAccess(string initValue)
        : base("access", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoBytesUsed schema extension describing an bytesUsed
    /// </summary>
    public class GPhotoBytesUsed : SimpleElement
    {
        public GPhotoBytesUsed()
        : base("bytesUsed", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoBytesUsed(string initValue)
        : base("bytesUsed", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoLocation schema extension describing an location
    /// </summary>
    public class GPhotoLocation : SimpleElement
    {
        public GPhotoLocation()
        : base("location", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoLocation(string initValue)
        : base("location", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoName schema extension describing an name
    /// </summary>
    public class GPhotoName : SimpleElement
    {
        public GPhotoName()
        : base("name", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoName(string initValue)
        : base("name", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoNumPhotos schema extension describing an numphotos
    /// </summary>
    public class GPhotoNumPhotos : SimpleElement
    {
        public GPhotoNumPhotos()
        : base("numphotos", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoNumPhotos(string initValue)
        : base("numphotos", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoNumPhotosRemaining schema extension describing an numphotosremaining
    /// </summary>
    public class GPhotoNumPhotosRemaining : SimpleElement
    {
        public GPhotoNumPhotosRemaining()
        : base("numphotosremaining", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoNumPhotosRemaining(string initValue)
        : base("numphotosremaining", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoChecksum schema extension describing an checksum
    /// </summary>
    public class GPhotoChecksum : SimpleElement
    {
        public GPhotoChecksum()
        : base("checksum", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoChecksum(string initValue)
        : base("checksum", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoClient schema extension describing an client
    /// </summary>
    public class GPhotoClient : SimpleElement
    {
        public GPhotoClient()
        : base("client", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoClient(string initValue)
        : base("client", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoHeight schema extension describing an height
    /// </summary>
    public class GPhotoHeight : SimpleElement
    {
        public GPhotoHeight()
        : base("height", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoHeight(string initValue)
        : base("height", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoPosition schema extension describing an position
    /// </summary>
    public class GPhotoPosition : SimpleElement
    {
        public GPhotoPosition()
        : base("position", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoPosition(string initValue)
        : base("position", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoRotation schema extension describing an rotation
    /// </summary>
    public class GPhotoRotation : SimpleElement
    {
        public GPhotoRotation()
        : base("rotation", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoRotation(string initValue)
        : base("rotation", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoSize schema extension describing an size
    /// </summary>
    public class GPhotoSize : SimpleElement
    {
        public GPhotoSize()
        : base("size", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoSize(string initValue)
        : base("size", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoTimestamp schema extension describing an timestamp
    /// </summary>
    public class GPhotoTimestamp : SimpleElement
    {
        public GPhotoTimestamp()
        : base("timestamp", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoTimestamp(string initValue)
        : base("timestamp", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }


    /// <summary>
    /// GPhotoVersion schema extension describing an version
    /// </summary>
    public class GPhotoVersion : SimpleElement
    {
        public GPhotoVersion()
        : base("version", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoVersion(string initValue)
        : base("version", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoPhotoId schema extension describing an photoid
    /// </summary>
    public class GPhotoPhotoId : SimpleElement
    {
        public GPhotoPhotoId()
        : base("photoid", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoPhotoId(string initValue)
        : base("photoid", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoWidth schema extension describing an width
    /// </summary>
    public class GPhotoWidth : SimpleElement
    {
        public GPhotoWidth()
        : base("width", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoWidth(string initValue)
        : base("width", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }

    /// <summary>
    /// GPhotoWeight schema extension describing an weight
    /// </summary>
    public class GPhotoWeight : SimpleElement
    {
        public GPhotoWeight()
        : base("weight", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos)
         {}
        public GPhotoWeight(string initValue)
        : base("weight", GPhotoNameTable.gPhotoPrefix, GPhotoNameTable.NSGPhotos, initValue)
        {}
    }
}
