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
               if (this.af != null)
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
               if (this.af != null)
               {
                   return this.af.ItemsPerPage;
               }
               return -1; 
           }
       }


       /// <summary>
       ///  returns the initial list of entries.This page is the data
       ///  you got from the Requestobject and will remain constant.
       ///  Unless you set AutoPaging to true, in that case:
       ///  This will go back to the server and fetch data again if
        /// needed. Example. If you pagesize is 30, you get an initial set of 
        /// 30 entries. While enumerating, when reaching 30, the code will go 
        /// to the server and get the next 30 rows. It will continue to do so
        /// until the server reports no more rows available. 
       /// </summary>
       /// <returns></returns>
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


    /// <summary>
    /// the Entry class is the base class for all Feed<t> type feeds
    /// it encapsulates the AtomEntry
    /// </summary>
    /// <returns></returns>
    public class Entry
    {
        private AtomEntry e; 

        public Entry()
        {

        }

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

    /// <summary>
    /// the Playlist entry for a Playlist Feed, a feed<Playlist> for YouTube
    /// </summary>
    public class Playlist : Entry
    {
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>the Video Entry in feed<Videos> for YouTube
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
                if (this.YT != null)
                {
                    if (this.YT.Media == null)
                    {
                        this.YT.Media = new Google.GData.YouTube.MediaGroup();
                    }
                    return this.YT.Media.Categories; 
                }
                return null;
            }
        }

        public ExtensionCollection<MediaThumbnail> Thumbnails
        {
            get
            {
                if (this.YT != null)
                {
                    if (this.YT.Media == null)
                    {
                        this.YT.Media = new Google.GData.YouTube.MediaGroup();
                    }
                    return this.YT.Media.Thumbnails; 
                }
                return null;
            }
        }

        public Uri WatchPage
        {
            get
            {
                if (this.YT!= null  && 
                    this.YT.Media != null  && 
                    this.YT.Media.Contents != null )
                {
                    foreach (Google.GData.YouTube.MediaContent m in this.YT.Media.Contents )
                    {
                        if (m.Format=="5")
                        {
                            return new Uri(m.Url); 
                        }
                    }
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


    /// <summary>
    /// base requestsettings class. Takes credentials, applicaitonsnames
    /// and supports pagesizes and autopaging
    /// </summary>
    /// <returns></returns>
    public class RequestSettings
    {
        private string applicationName;
        private GDataCredentials credentials; 
        private int pageSize = -1;
        private bool autoPage;

        public RequestSettings(string applicationName)
        {
            this.applicationName = applicationName;
        }

        public RequestSettings(string applicationName, string userName, string passWord)
        {
            this.applicationName = applicationName;
            this.credentials = new GDataCredentials(userName, passWord);
        }

        public GDataCredentials Credentials
        {
            get
            {
                return this.credentials;
            }
        }

        public string Application
        {
            get
            {
                return this.applicationName;
            }
        }

        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        public bool AutoPaging
        {
            get
            {
                return this.autoPage;
            }
            set
            {
                this.autoPage = value; 
            }
        }

    }

    /// <summary>
    /// YouTube specific class for request settings,
    /// adds support for developer key and clientid
    /// </summary>
    /// <returns></returns>
    public class YouTubeRequestSettings : RequestSettings
    {
        private string clientID;
        private string developerKey;

        public YouTubeRequestSettings(string applicationName, string client, string developerKey) : base(applicationName)
        {
            this.clientID = client;
            this.developerKey = developerKey;
        }

        public YouTubeRequestSettings(string applicationName, string client, string developerKey, string userName, string passWord)  
                    : base(applicationName, userName, passWord)
        {
            this.clientID = client;
            this.developerKey = developerKey;
        }

        public string Client
        {
            get
            {
                return this.clientID;
            }
        }

        public string DeveloperKey
        {
            get
            {
                return this.developerKey;
            }
        }
    }

    /// <summary>
    /// base class for Request objects.
    /// </summary>
    /// <returns></returns>
    public abstract class FeedRequest<T> where T : Service
    {
        private RequestSettings settings;
        protected T service; 
        public FeedRequest(RequestSettings settings)
        {
            this.settings = settings; 

        }


        protected void PrepareService()
        {
            if (settings.Credentials != null)
            {
                this.service.Credentials = settings.Credentials;
            }
        }

        protected T PrepareQuery<T>(string uri) where T: FeedQuery, new()
        {
            T query = new T(); 
            query.BaseAddress = uri; 

            if (this.settings.PageSize != -1)
            {
                query.NumberToRetrieve = this.settings.PageSize; 
            }
            return query; 
        }

        protected Feed<T> PrepareFeed<T>(FeedQuery q) where T : Entry, new()
        {
             AtomFeed feed = this.service.Query(q);
             Feed<T> f = new Feed<T>(feed);
             f.AutoPaging = this.settings.AutoPaging;
             return f;
        }


    }



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
    public class YouTubeRequest : FeedRequest<YouTubeService>
    {
        public YouTubeRequest(YouTubeRequestSettings settings) : base(settings)
        {
            if (settings.Client != null && settings.DeveloperKey != null)
            {
                this.service = new YouTubeService(settings.Application, settings.Client, settings.DeveloperKey);
            }
            else
            {
                this.service = new YouTubeService(settings.Application);
            }

            PrepareService();
        }

        /// <summary>
        /// returns a Feed of vidoes for a given username
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Videos</returns>
        public Feed<Video> GetVideoFeed(string user)
        {
            YouTubeQuery q = PrepareQuery<YouTubeQuery>(YouTubeQuery.CreateUserUri(user));
            return PrepareFeed<Video>(q); 
        }

         /// <summary>
        ///  returns one of the youtube default feeds. 
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Videos</returns>
        public Feed<Video> GetStandardFeed(string feedspec)
        {
            YouTubeQuery q = new YouTubeQuery(feedspec);
            return PrepareFeed<Video>(q); 
        }

        /// <summary>
        /// returns a Feed of favorite videos for a given username
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Videos</returns>
        public Feed<Video> GetFavoriteFeed(string user)
        {
            YouTubeQuery q = new YouTubeQuery(YouTubeQuery.CreateFavoritesUri(user));
            return PrepareFeed<Video>(q); 
        }

        /// <summary>
        /// returns a Feed of playlists  for a given username
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Videos</returns>
        public Feed<Playlist> GetPlaylistsFeed(string user)
        {
            YouTubeQuery q = new YouTubeQuery(YouTubeQuery.CreatePlaylistsUri(user));
            return PrepareFeed<Playlist>(q);             
        }

        /// <summary>
        /// uses a preset YouTubeQuery object to retrieve videos. 
        /// </summary>
        /// <param name="q">the query object to use</param>
        /// <returns></returns>
        public Feed<Video> GetFeed(YouTubeQuery q)
        {
            YouTubeFeed f = this.service.Query(q);
            return PrepareFeed<Video>(q); 
        }

    }
}
