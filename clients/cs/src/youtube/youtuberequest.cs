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
        bool paging; 


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

        /// <summary>
        ///  default public constructor, needed for generics.
        /// </summary>
        /// <returns></returns>
        public Entry()
        {

        }

        /// <summary>
        ///  the original AtomEntry object that this object is standing in for
        /// </summary>
        /// <returns></returns>
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
        /// the title of the Entry. 
        /// </summary>
        /// <returns></returns>
        public string Title
        {
            get 
            {
                if (this.e != null)
                {
                    return this.e.Title.Text;
                }
                return null;
            }
            set 
            {
                this.e.Title.Text = value; 
            }
        }

        /// <summary>
        ///  returns the first author name in the atom.entry.authors collection
        /// </summary>
        /// <returns></returns>
        public string Author
        {
            get
            {
                if (this.e != null && this.e.Authors.Count > 0 && this.e.Authors[0] != null)
                {
                    return this.e.Authors[0].Name;
                }
                return null;
            }
            set
            {
                if (this.e != null)
                {
                    AtomPerson p = null; 
                    if (this.e.Authors.Count == 0)
                    {
                        p = new AtomPerson(AtomPersonType.Author);
                        this.e.Authors.Add(p);
                    }
                    else
                    {
                        p = this.e.Authors[0];
                    }
                    p.Name = value; 
                }
            }
        }

        /// <summary>
        /// returns the string representation of the atom.content element
        /// </summary>
        /// <returns></returns>
        public string Content
        {
            get
            {
                if (this.e != null)
                {
                    return this.e.Content.Content;
                }
                return null;
            }
            set
            {
                if (this.e != null)
                {
                    this.e.Content.Content = value;
                }
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
                this.e = this.e.Update();
            }
        }

        /// <summary>
        ///  deletes the underlying entry and makes this object invalid
        /// </summary>
        /// <returns></returns>
        public void Delete()
        {
            if (this.e != null)
            {
                this.e.Delete();
                this.e = null; 
            }
        }
    }

    /// <summary>
    /// the Playlist entry for a Playlist Feed, a feed<Playlist> for YouTube
    /// </summary>
    public class Playlist : Entry
    {
    }

  /// <summary>
    /// the Comment entry for a Comments Feed, a feed<Comment> for YouTube
    /// </summary>
    public class Comment : Entry
    {
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>the Video Entry in feed<Videos> for YouTube
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class Video : Entry
    {
        public  YouTubeEntry YouTubeEntry
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
                if (this.YouTubeEntry != null)
                {
                    return this.YouTubeEntry.VideoId;
                }
                return null; 
            }
            set
            {
                this.YouTubeEntry.VideoId = value; 
            }
        }
     /// <summary>
        /// the Media Description element
        /// </summary>
        /// <returns></returns>
        public string Description
        {
            get
            {
                if (this.YouTubeEntry != null && 
                    this.YouTubeEntry.Media != null &&
                    this.YouTubeEntry.Media.Description != null)
                {
                    return this.YouTubeEntry.Media.Description.Value;
                }
                return null; 
            }
            set
            {
                if (this.YouTubeEntry != null)
                {
                    if (this.YouTubeEntry.Media == null)
                    {
                        this.YouTubeEntry.Media = new Google.GData.YouTube.MediaGroup();
                    }
                    if (this.YouTubeEntry.Media.Description == null)
                    {
                        this.YouTubeEntry.Media.Description = new MediaDescription();
                    }
                    this.YouTubeEntry.Media.Description.Value = value; 
                }
            }
        }


        /// <summary>
        /// returns the categories for the video
        /// </summary>
        /// <returns></returns>
        public ExtensionCollection<MediaCategory> Tags
        {
            get
            {
                if (this.YouTubeEntry != null)
                {
                    if (this.YouTubeEntry.Media == null)
                    {
                        this.YouTubeEntry.Media = new Google.GData.YouTube.MediaGroup();
                    }
                    return this.YouTubeEntry.Media.Categories; 
                }
                return null;
            }
        }

        /// <summary>
        /// returns the collection of thumbnails for the vido
        /// </summary>
        /// <returns></returns>
        public ExtensionCollection<MediaThumbnail> Thumbnails
        {
            get
            {
                if (this.YouTubeEntry != null)
                {
                    if (this.YouTubeEntry.Media == null)
                    {
                        this.YouTubeEntry.Media = new Google.GData.YouTube.MediaGroup();
                    }
                    return this.YouTubeEntry.Media.Thumbnails; 
                }
                return null;
            }
        }

        /// <summary>
        /// returns the url attribute of media:player as a Uri
        /// </summary>
        /// <returns></returns>
        public Uri WatchPage
        {
            get
            {
                if (this.YouTubeEntry!= null  && 
                    this.YouTubeEntry.Media != null  && 
                    this.YouTubeEntry.Media.Player != null )
                {
                    return new Uri(this.YouTubeEntry.Media.Player.Url);
                }
                return null; 
            }
        }

        /// <summary>
        /// get's sets the uploader of the video
        /// </summary>
        /// <returns></returns>
        public string Uploader
        {
            get
            {
                if (this.YouTubeEntry!= null  && 
                    this.YouTubeEntry.Media != null  && 
                    this.YouTubeEntry.Media.Credit != null )
                {
                    return this.YouTubeEntry.Media.Credit.Value;
                }
                return null; 
            }
            set
            {
                if (this.YouTubeEntry != null)
                {
                    if (this.YouTubeEntry.Media == null)
                    {
                        this.YouTubeEntry.Media = new Google.GData.YouTube.MediaGroup();
                    }
                    if (this.YouTubeEntry.Media.Credit == null)
                    {
                        this.YouTubeEntry.Media.Credit = new Google.GData.YouTube.MediaCredit();
                    }
                    this.YouTubeEntry.Media.Credit.Value = value; 
                }
            }
        }
    }
    

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

        protected virtual Feed<T> PrepareFeed<T>(FeedQuery q) where T : Entry, new()
        {
             AtomFeed feed = this.service.Query(q);
             Feed<T> f = new Feed<T>(feed);
             f.AutoPaging = this.settings.AutoPaging;
             return f;
        }

        public Feed<T> GetFeed<T>(FeedQuery q) where T: Entry, new()
        {
            return PrepareFeed<T>(q);  
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
        /// returns the related videos for a given video
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Feed<Video> GetRelatedVideos(Video v)
        {
            if (v.YouTubeEntry != null)
            {
                if (v.YouTubeEntry.RelatedVideosUri != null)
                {
                    YouTubeQuery q = new YouTubeQuery(v.YouTubeEntry.RelatedVideosUri.ToString());
                    return PrepareFeed<Video>(q); 
                }
            }
            return null;
        }

        /// <summary>
        ///  gets the response videos for a given video
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Feed<Video> GetResponseVideos(Video v)
        {
            if (v.YouTubeEntry != null)
            {
                if (v.YouTubeEntry.VideoResponsesUri != null)
                {
                    YouTubeQuery q = new YouTubeQuery(v.YouTubeEntry.VideoResponsesUri.ToString());
                    return PrepareFeed<Video>(q); 
                }
            }
            return null;
        }

        /// <summary>
        /// get's the comments for a given video
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Feed<Comment> GetComments(Video v)
        {
             if (v.YouTubeEntry != null && 
                    v.YouTubeEntry.Comments != null && 
                    v.YouTubeEntry.Comments.FeedLink != null && 
                    v.YouTubeEntry.Comments.FeedLink.Href != null
                    )
             {
                    YouTubeQuery q = new YouTubeQuery(v.YouTubeEntry.Comments.FeedLink.Href);
                    return PrepareFeed<Comment>(q); 
             }
            return null;
        }

        /// <summary>
        /// retuns the feed of videos for a given playlist
        /// </summary>
        /// <param name="p">the playlist to get the videos for</param>
        /// <returns></returns>
        public Feed<Video> GetPlaylist(Playlist p)
        {
            if (p.AtomEntry != null && 
                p.AtomEntry.Content != null && 
                p.AtomEntry.Content.AbsoluteUri != null)
            {
                   YouTubeQuery q = new YouTubeQuery(p.AtomEntry.Content.AbsoluteUri);
                   return PrepareFeed<Video>(q); 
            }
           return null;
        }
    }
}
