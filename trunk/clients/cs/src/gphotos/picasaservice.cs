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


namespace Google.GData.Photos {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Picasa Web Albums provides a variety of representations of photo- 
    /// and album-related data. There are three independent axes for 
    /// specifying what you want when you request data: visibility, projection, and path/kind.
    /// Visibility values let you request data at various levels of sharing. 
    /// For example, a visibility value of public requests publicly visible data. 
    /// For a list of values, see Visibility values, below. The default visibility 
    /// depends on whether the request is authenticated or not.
    /// Projection values let you indicate what elements and extensions 
    /// should appear in the feed you're requesting. For example, a projection 
    /// value of base indicates that the representation is a basic Atom feed 
    /// without any extension elements, suitable for display in an Atom reader. 
    /// You must specify a projection value. Path and kind values let you indicate what 
    /// type of items you want information about. For example, a path of user/liz 
    /// and a kind value of tag requests a feed of tags associated with the 
    /// user whose username is liz. Path and kind values are separate parts of the 
    /// URI, but they're used together to indicate the item type(s) desired. 
    /// You must specify a path, but kind is optional; the default kind depends on the path.
    /// To request a particular representation, you specify a visibility value, 
    /// a projection value, and a path and kind in the URI that you send 
    /// to Picasa Web Albums.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class PicasaService : MediaService
    {
       
        /// <summary>The Calendar service's name</summary> 
        public const string GPicasaService = "lh2";

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public PicasaService(string applicationName) : base(GPicasaService, applicationName)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed); 
        }

        /// <summary>
        /// by default all services now use version 1 for the protocol.
        /// this needs to be overridden by a service to specify otherwise. 
        /// Picasa uses version 2
        /// </summary>
        /// <returns></returns>
        protected override void InitVersionInformation()
        {
             this.ProtocolMajor = VersionDefaults.VersionTwo;
        }
   
        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public PicasaFeed Query(KindQuery feedQuery) 
        {
            return base.Query(feedQuery) as PicasaFeed;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>templated type safe verion of the interface</summary> 
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="entry">the old entry to update</param> 
        /// <param name="feedUri"></param>
        /// <returns> the new Entry, as returned from the server</returns>
        public new PicasaEntry Insert<TEntry>(Uri feedUri, TEntry entry) where TEntry : AtomEntry
        {
            return this.internalInsert(feedUri, entry) as PicasaEntry;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>templated type safe verion of the interface</summary> 
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="entry">the entry to insert</param> 
        /// <param name="feed">the feed that entry is getting inserted</param>
        /// <returns> the new Entry, as returned from the server</returns>
        public new PicasaEntry Insert<TEntry>(AtomFeed feed, TEntry entry) where TEntry : AtomEntry
        {
            IService s = this as IService;
            return s.Insert(feed, entry) as PicasaEntry;
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>eventchaining. We catch this by from the base service, which 
        /// would not by default create an atomFeed</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Picasa Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }

            // picasa feeds are based on the following templates:
            // userFeeds:       http://picasaweb.google.com/data/feed/projection/user/userID/?kind=kinds
            // albumFeeds(id):  http://picasaweb.google.com/data/feed/projection/user/userID/albumid/albumID?kind=kinds
            // albumFeed(name): http://picasaweb.google.com/data/feed/projection/user/userID/album/albumName?kind=kinds
            // PicasaFeed(id):   http://picasaweb.google.com/data/feed/projection/user/userID/albumid/albumID/photoid/photoID?kind=kinds
            // PicasaFeed(name): http://picasaweb.google.com/data/feed/projection/user/userID/album/albumName/photoid/photoID?kind=kinds
            // 
            // the easiest way, on the URI, is to just count the number of slashes
            // but we don't really need to do that, as kinds can be mixed in a feed, we just use one feedtype for picasaweb

            e.Feed = new PicasaFeed(e.Uri, e.Service);
        }
        /////////////////////////////////////////////////////////////////////////////
    }
}
