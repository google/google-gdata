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

namespace Google.Picasa
{

    /// <summary>
    /// represents a photo based on a PicasaEntry object
    /// </summary>
    public class Photo : Entry
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
        /// returns a Feed of all photos for the authorized user
        /// </summary>
        /// <returns>a feed of everyting</returns>
        public Feed<Photo> GetPhotos()
        {
            return GetPhotos("default");
        }

        /// <summary>
        /// returns a Feed of all documents and folders for the authorized user
        /// </summary>
        /// <returns>a feed of everyting</returns>
        public Feed<Photo> GetPhotos(string user)
        {
            PhotoQuery q = PrepareQuery<PhotoQuery>(PicasaQuery.CreatePicasaUri(user));
            return PrepareFeed<Photo>(q); 
        }

        /// <summary>
        /// returns a feed of photos in that particular album for the default user
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public Feed<Photo> GetPhotosByAlbum(string album)
        {
            return GetPhotosByAlbum("default", album);
        }

        /// <summary>
        /// returns a feed of photos in that particular album for the given user
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public Feed<Photo> GetPhotosByAlbum(string user, string album)
        {
            PhotoQuery q = PrepareQuery<PhotoQuery>(PicasaQuery.CreatePicasaUri(user, album));
            return PrepareFeed<Photo>(q); 
        }


        /// <summary>
        /// Returns a single photo based on the default user, the albumid and the photoid
        /// </summary>
        /// <param name="album"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public Photo GetPhoto(string album, string photo)
        {
            return GetPhoto("default", album, photo);
        }


        /// <summary>
        /// Returns a single photo based on the given user, the albumid and the photoid
        /// </summary>
        /// <param name="album"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public Photo GetPhoto(string user, string album, string photo)
        {
            Uri uri = new Uri(PicasaQuery.CreatePicasaUri(user, album, photo));
            return Retrieve<Photo>(uri);
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
