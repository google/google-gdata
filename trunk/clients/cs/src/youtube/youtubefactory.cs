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
using Google.GData.YouTube;
using Google.GData.Extensions.MediaRss;
using System.Collections.Generic;


namespace Google.YouTube 
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>a generic Feed class
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class Feed<T> where T: Entry, new()
    {
        AtomFeed af;
        bool paging = false; 

        public Feed(AtomFeed af)
        {
            this.af = af; 
        }

        /// <summary>
        /// returns the used feed object
        /// </summary>
        /// <returns></returns>
        public AtomFeed AtomFeed
        {
            get
            {
                return this.af;
            }
        }

       /// <summary>
       /// if set to true will cause the feed to add more data when you iterate over it's entries
       /// </summary>
       /// <returns></returns>
       public bool AutoPaging
       {
           get 
           {
               return this.paging;
           }
           set
           {
               this.paging = value;
           }
       }

       /// <summary>
       /// returns the position in the real feed of the first entry in this feed
       /// </summary>
       /// <returns>an int indicating the start in the feed</returns>
       public int StartIndex
       {
           get
           {
               if (this.af)
               {
                   return this.af.StartIndex;
               }
               return -1; 
           }
       }

       /// <summary>
       /// returns the setup paging size of this feed. If you set AutoPaging to true
       /// this is the size that is used to get more results
       /// </summary>
       /// <returns></returns>
       public int PageSize
       {
           get
           {
               if (this.af)
               {
                   return this.af.ItemsPerPage;
               }
               return -1; 
           }
       }

        public IEnumerable<T> Entries
        {
            get
            {
                bool looping;

                do
                {
                    looping = af.NextChunk != null && this.paging == true;
                    foreach (AtomEntry e in af.Entries)
                    {
                        T t = new T();
                        if (t != null)
                        {
                            t.AtomEntry = e; 
                            yield return t; 
                        }
                    }
                    if (looping)
                    {
                        FeedQuery q = new FeedQuery(this.af.NextChunk);
                        this.af = this.af.Service.Query(q);
                    }
                } while (looping);
            }
        }
    }
    //end of public class Feed


    public class Entry
    {
        private AtomEntry e; 

        public AtomEntry AtomEntry
        {
            get
            {
                return this.e;
            }
            set 
            {
                this.e = value; 
            }
        }

        /// <summary>
        ///  sends the data back to the server. 
        /// </summary>
        /// <returns></returns>
        public void Update()
        {
            if (this.e != null)
            {
                e.Update();
            }
        }
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>a generic Feed class
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class Video : Entry
    {
        private YouTubeEntry YT
        {
            get
            {
                return this.AtomEntry as YouTubeEntry;
            }
        }

        public string Id
        {
            get
            {
                if (this.YT != null)
                {
                    return this.YT.VideoId;
                }
                return null; 
            }
            set
            {
                this.YT.VideoId = value; 
            }
        }

        public string Title
        {
            get 
            {
                if (this.YT != null)
                {
                    return this.YT.Title.Text;
                }
                return null;
            }
            set 
            {
                this.YT.Title.Text = value; 
            }
        }

        public string Description
        {
            get
            {
                if (this.YT != null && 
                    this.YT.Media != null &&
                    this.YT.Media.Description != null)
                {
                    return this.YT.Media.Description.Value;
                }
                return null; 
            }
            set
            {
                if (this.YT != null)
                {
                    if (this.YT.Media == null)
                    {
                        this.YT.Media = new Google.GData.YouTube.MediaGroup();
                    }
                    if (this.YT.Media.Description == null)
                    {
                        this.YT.Media.Description = new MediaDescription();
                    }
                    this.YT.Media.Credit.Value = value; 
                }
            }
        }


        public ExtensionCollection<MediaCategory> Tags
        {
            get
            {
                if (this.YT != null && this.YT.Media != null)
                {
                    return this.YT.Media.Categories; 
                }
                return null;
            }
        }

        public string Uploader
        {
            get
            {
                if (this.YT!= null  && 
                    this.YT.Media != null  && 
                    this.YT.Media.Credit != null )
                {
                    return this.YT.Media.Credit.Value;
                }
                return null; 
            }
            set
            {
                if (this.YT != null)
                {
                    if (this.YT.Media == null)
                    {
                        this.YT.Media = new Google.GData.YouTube.MediaGroup();
                    }
                    if (this.YT.Media.Credit == null)
                    {
                        this.YT.Media.Credit = new Google.GData.YouTube.MediaCredit();
                    }
                    this.YT.Media.Description.Value = value; 
                }
            }

        }
    }
    //end of public class Feed




    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// The YouTube Data API allows applications to perform functions normally 
    /// executed on the YouTube website. The API enables your application to search 
    /// for YouTube videos and to retrieve standard video feeds, comments and video
    /// responses. 
    /// In addition, the API lets your application upload videos to YouTube or 
    /// update existing videos. Your can also retrieve playlists, subscriptions, 
    /// user profiles and more. Finally, your application can submit 
    /// authenticated requests to enable users to create playlists, 
    /// subscriptions, contacts and other account-specific entities.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class YouTubeFactory
    {
        private YouTubeService service;

        public YouTubeFactory(string applicationName)
        {
            if (applicationName == null)
            {
                throw new ArgumentNullException("applicationName");
            }
            this.service = new YouTubeService(applicationName);
        }

        public YouTubeFactory(string applicationName, string clientID, string developerKey)
        {
            if (applicationName == null)
            {
                throw new ArgumentNullException("applicationName");
            }
            if (developerKey == null)
            {
                throw new ArgumentNullException("developerKey");
            }
            if (clientID == null)
            {
                throw new ArgumentNullException("clientID");
            }
            this.service = new YouTubeService(applicationName, clientID, developerKey);
        }

        public YouTubeFactory(string applicationName, string clientID, string developerKey, string userName, string passWord)
        {
            if (applicationName == null)
            {
                throw new ArgumentNullException("applicationName");
            }
            if (developerKey == null)
            {
                throw new ArgumentNullException("developerKey");
            }
            if (clientID == null)
            {
                throw new ArgumentNullException("clientID");
            }
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }
            if (passWord == null)
            {
                throw new ArgumentNullException("passWord");
            }
            this.service = new YouTubeService(applicationName, clientID, developerKey);
            this.service.Credentials = new GDataCredentials(userName, passWord);
        }


        /// <summary>
        /// returns a Feed of vidoes for a given username
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Videos</returns>
        public Feed<Video> GetVideoFeed(string user)
        {
            YouTubeQuery q = new YouTubeQuery(YouTubeQuery.CreateUserUri(user));
            YouTubeFeed f = this.service.Query(q);
            return new Feed<Video>(f);
        }

         /// <summary>
        ///  returns one of the youtube default feeds. 
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Videos</returns>
        public Feed<Video> GetStandardFeed(string feedspec)
        {
            YouTubeQuery q = new YouTubeQuery(feedspec);
            YouTubeFeed f = this.service.Query(q);
            return new Feed<Video>(f);
        }
    }
}
