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


        /// <summary>
        /// default constructor that takes the underlying atomfeed
        /// </summary>
        /// <param name="af"></param>
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
       /// returns the initial list of entries.This page is the data
       /// you got from the Requestobject and will remain constant.
       /// Unless you set AutoPaging to true, in that case:
       /// This will go back to the server and fetch data again if
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
                if (this.af == null)
                    yield break;

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
    /// the Entry class is the base class for all Feed of T type feeds
    /// it encapsulates the AtomEntry
    /// </summary>
    /// <returns></returns>
    public abstract class Entry
    {
        private AtomEntry e; 

        /// <summary>
        ///  default public constructor, needed for generics. You should not use that one, but use the
        /// CreateInstance method for the entry you want to create
        /// </summary>
        /// <returns></returns>
        public  Entry()
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
        public virtual string Title
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
        /// returns the string representation of the atom.Summary element
        /// </summary>
        /// <returns></returns>
        public string Summary
        {
            get
            {
                if (this.e != null)
                {
                    return this.e.Summary.Text;
                }
                return null;
            }
            set
            {
                if (this.e != null)
                {
                    this.e.Summary.Text = value;
                }
            }
        }
    }

    /// <summary>
    /// the Playlist entry for a Playlist Feed, a feed of Playlist for YouTube
    /// </summary>
    public class Playlist : Entry
    {
        /// <summary>
        /// returns the internal atomentry as a PlayListsEntry
        /// </summary>
        /// <returns></returns>
        public PlaylistsEntry PlaylistsEntry
        {
            get
            {
                return this.AtomEntry as PlaylistsEntry;
            }
        }

        /// <summary>
        /// specifies the number of entries in a playlist feed. This tag appears in the entries in a 
        /// playlists feed, where each entry contains information about a single playlist.
        /// </summary>
        /// <returns></returns>
        public int CountHint
        {
            get 
            {
                if (this.PlaylistsEntry != null)
                {
                    return this.PlaylistsEntry.CountHint;
                }
                return 0; 
            }
        }
    }

    /// <summary>
    /// the Comment entry for a Comments Feed, a feed of Comment for YouTube
    /// </summary>
    public class Comment : Entry
    {
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>the Video Entry in feed&lt;Videos&gt; for YouTube
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class Video : Entry
    {
        /// <summary>
        /// creates a new video and set's up the internal Atom object for representation
        /// </summary>
        /// <returns></returns>
        public static Video CreateInstance()
        {
            Video v = new Video();
            v.AtomEntry = new YouTubeEntry();
            return v;
        }


        /// <summary>
        /// readonly accessor for the YouTubeEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public  YouTubeEntry YouTubeEntry
        {
            get
            {
                return this.AtomEntry as YouTubeEntry;
            }
        }

        /// <summary>
        /// specifies a unique ID that YouTube uses to identify a video.
        /// </summary>
        /// <returns></returns>
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
        }

        /// <summary>
        /// contains a summary or description of a video. This field is required in requests to 
        /// upload or update a video's metadata. The description should be sentence-based, 
        /// rather than a list of keywords, and may be displayed in search results. The description has a 
        /// maximum length of 5000 characters and may contain all valid UTF-8 characters except &lt; and &gt; 
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
        /// the title of the Video. Overloaded to keep entry.title and the media.title 
        ///  in sync. 
        /// </summary>
        /// <returns></returns>
        public override string Title
        {
            get
            {
                return base.Title;
            }
            set
            {
                base.Title = value;
                // now set the media title element as well
                if (this.YouTubeEntry != null)
                {
                    if (this.YouTubeEntry.Media == null)
                    {
                        this.YouTubeEntry.Media = new Google.GData.YouTube.MediaGroup();
                    }
                }
                if (this.YouTubeEntry.Media.Title == null)
                {
                    this.YouTubeEntry.Media.Title = new MediaTitle();
                }
                this.YouTubeEntry.Media.Title.Value = value; 
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
        /// returns the keywords for the video, see MediaKeywords for more
        /// </summary>
        /// <returns></returns>
        public string Keywords
        {
            get
            {
                if (this.YouTubeEntry != null)
                {
                    if (this.YouTubeEntry.Media != null)
                    {
                        if (this.YouTubeEntry.Media.Keywords != null)
                        {
                            return this.YouTubeEntry.Media.Keywords.Value;
                        }
                    }
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
                    if (this.YouTubeEntry.Media.Keywords == null)
                    {
                        this.YouTubeEntry.Media.Keywords = new MediaKeywords();
                    }
                    this.YouTubeEntry.Media.Keywords.Value = value; 
                }
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
        /// specifies a URL where the full-length video is available through a media player that runs 
        /// inside a web browser. In a YouTube Data API response, this specifies the URL for the page 
        /// on YouTube's website that plays the video
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
        /// identifies the owner of a video.
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


        /// <summary>
        /// returns the viewcount for the video
        /// </summary>
        /// <returns></returns>
        public int ViewCount
        {
            get
            {
                if (this.YouTubeEntry != null && this.YouTubeEntry.Statistics != null)
                    return Int32.Parse(this.YouTubeEntry.Statistics.ViewCount);
                return 0;
            }
        }

        /// <summary>
        /// returns the number of comments for the video
        /// </summary>
        /// <returns></returns>
        public int CommmentCount
        {
            get
            {
                if (this.YouTubeEntry != null && 
                    this.YouTubeEntry.Comments != null &&
                    this.YouTubeEntry.Comments.FeedLink != null)
                {
                        return this.YouTubeEntry.Comments.FeedLink.CountHint;
                }
                return 0;
            }
        }

        /// <summary>
        /// returns the rating average for a video
        /// </summary>
        /// <returns></returns>
        public double Rating
        {
            get
            {
                if (this.YouTubeEntry != null &&
                    this.YouTubeEntry.Rating != null)
                {
                    return this.YouTubeEntry.Rating.Average;
                }
                return 0;
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
        private string authSubToken; 
        private int pageSize = -1;
        private bool autoPage;

        /// <summary>
        /// an unauthenticated use case
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public RequestSettings(string applicationName)
        {
            this.applicationName = applicationName;
        }

        /// <summary>
        ///  a constructor for client login use cases
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="userName">the user name</param>
        /// <param name="passWord">the password</param>
        /// <returns></returns>
        public RequestSettings(string applicationName, string userName, string passWord)
        {
            this.applicationName = applicationName;
            this.credentials = new GDataCredentials(userName, passWord);
        }

        /// <summary>
        /// a constructor for a web application authentication scenario
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="authSubToken"></param>
        /// <returns></returns>
        public RequestSettings(string applicationName, string authSubToken)
        {
            this.applicationName = applicationName;
            this.authSubToken = authSubToken; 
        }


        /// <summary>
        /// returns the Credentials in case of a client login scenario
        /// </summary>
        /// <returns></returns>
        public GDataCredentials Credentials
        {
            get
            {
                return this.credentials;
            }
        }

        /// <summary>
        /// returns the authsub token to use for a webapplication scenario
        /// </summary>
        /// <returns></returns>
        public string AuthSubToken
        {
            get
            {
                return this.authSubToken;
            }
        }

        /// <summary>
        /// returns the application name
        /// </summary>
        /// <returns></returns>
        public string Application
        {
            get
            {
                return this.applicationName;
            }
        }

        /// <summary>
        /// the pagesize specifies how many entries should be retrieved per call. If not set, 
        /// the server default will be used
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// AutoPaging specifies if a feed iterator should return to the server to fetch more data 
        /// automatically. If set to false, a loop over feed.Entries will stop when the currently 
        /// fetched set of data reaches it's end. 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// A constructor for a readonly scenario.
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="client">the client ID to use</param>
        /// <param name="developerKey">the developer key to use</param>
        /// <returns></returns>
        public YouTubeRequestSettings(string applicationName, string client, string developerKey) : base(applicationName)
        {
            this.clientID = client;
            this.developerKey = developerKey;
        }

        /// <summary>
        /// A constructor for a client login scenario
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="client">the client ID to use</param>
        /// <param name="developerKey">the developer key to use</param>
        /// <param name="userName">the username</param>
        /// <param name="passWord">the password</param>
        /// <returns></returns>
        public YouTubeRequestSettings(string applicationName, string client, string developerKey, string userName, string passWord)  
                    : base(applicationName, userName, passWord)
        {
            this.clientID = client;
            this.developerKey = developerKey;
        }

        /// <summary>
        /// a constructor for a web application authentication scenario        
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="client">the client ID to use</param>
        /// <param name="developerKey">the developer key to use</param>
        /// <param name="authSubToken">the authentication token</param>
        /// <returns></returns>
        public YouTubeRequestSettings(string applicationName, string client, string developerKey, string authSubToken)  
                    : base(applicationName, authSubToken)
        {
            this.clientID = client;
            this.developerKey = developerKey;
        }

        /// <summary>
        /// returns the client ID
        /// </summary>
        /// <returns></returns>
        public string Client
        {
            get
            {
                return this.clientID;
            }
        }

        /// <summary>
        /// returns the developer key
        /// </summary>
        /// <returns></returns>
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
        private T atomService; 

        /// <summary>
        /// default constructor based on a RequestSettings object
        /// </summary>
        /// <param name="settings"></param>
        public FeedRequest(RequestSettings settings)
        {
            this.settings = settings; 

        }

        /// <summary>
        /// prepares the created service based on the settings 
        /// </summary>
        protected void PrepareService()
        {
            if (settings.Credentials != null)
            {
                this.atomService.Credentials = settings.Credentials;
            }
#if WindowsCE || PocketPC
#else
            if (settings.AuthSubToken != null)
            {
                GAuthSubRequestFactory authFactory = new GAuthSubRequestFactory(atomService.ServiceIdentifier, settings.Application);
                authFactory.Token = settings.AuthSubToken; 
                atomService.RequestFactory = authFactory;
            }
#endif
        }

        /// <summary>
        /// creates a query object and set's it up based on the settings object.
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        protected Y PrepareQuery<Y>(string uri) where Y: FeedQuery, new()
        {
            Y query = new Y(); 
            query.BaseAddress = uri; 

            if (this.settings.PageSize != -1)
            {
                query.NumberToRetrieve = this.settings.PageSize; 
            }
            return query; 
        }

        /// <summary>
        /// creates a feed of Y object based on the query and the settings
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        protected virtual Feed<Y> PrepareFeed<Y>(FeedQuery q) where Y : Entry, new()
        {
             AtomFeed feed = this.atomService.Query(q);
             Feed<Y> f = new Feed<Y>(feed);
             f.AutoPaging = this.settings.AutoPaging;
             return f;
        }

        /// <summary>
        /// gets a feed object of type T
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        public Feed<Y> GetFeed<Y>(FeedQuery q) where Y: Entry, new()
        {
            return PrepareFeed<Y>(q);  
        }

        /// <summary>
        /// returns the service instance that is used
        /// </summary>
        public T Service
        {
            get
            {
                return this.atomService;
            }
            set
            {
                this.atomService = value;
            }
        }

        /// <summary>
        ///  sends the data back to the server. 
        /// </summary>
        /// <returns>the reflected entry from the server if any given</returns>
        public Y Update<Y>(Y entry) where Y: Entry, new()
        {
            if (entry == null)
                throw new ArgumentNullException("Entry was null");

            if (entry.AtomEntry == null)
                throw new ArgumentNullException("Entry.AtomEntry was null");

            Y r = null;
            AtomEntry ae = this.Service.Update(entry.AtomEntry);
           
            if (ae != null)
            {
                r = new Y();
                r.AtomEntry = ae;
            }
            return r; 
        }

        /// <summary>
        ///  deletes the Entry from the Server
        /// </summary>
        public void Delete<Y>(Y entry) where Y : Entry, new()
        {
            if (entry == null)
                throw new ArgumentNullException("Entry was null");

            if (entry.AtomEntry == null)
                throw new ArgumentNullException("Entry.AtomEntry was null");

            entry.AtomEntry.Delete();
        }

        /// <summary>
        /// takes the given Entry and inserts its into the server
        /// </summary>
        /// <returns>the reflected entry from the server if any given</returns>
        public Y Insert<Y>(Uri address, Y entry) where Y : Entry, new()
        {
            if (entry == null)
                throw new ArgumentNullException("Entry was null");

            if (entry.AtomEntry == null)
                throw new ArgumentNullException("Entry.AtomEntry was null");

            if (address == null)
                throw new ArgumentNullException("Entry was null");
          
            Y r = null;
            AtomEntry ae = this.Service.Insert(address, entry.AtomEntry);
            if (ae != null)
            {
                r = new Y();
                r.AtomEntry = ae;
            }
            return r;
        }

        /// <summary>
        /// takes the given Entry and inserts its into the server
        /// </summary>
        /// <returns>the reflected entry from the server if any given</returns>
        public Y Insert<Y>(Feed<Y> feed, Y entry) where Y : Entry, new()
        {
            if (entry == null)
                throw new ArgumentNullException("Entry was null");

            if (entry.AtomEntry == null)
                throw new ArgumentNullException("Entry.AtomEntry was null");

            if (feed == null)
                throw new ArgumentNullException("Feed was null");

            Y r = null;
            AtomEntry ae = this.Service.Insert(feed.AtomFeed, entry.AtomEntry);
            if (ae != null)
            {
                r = new Y();
                r.AtomEntry = ae;
            }
            return r;
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
        /// <summary>
        /// default constructor for a YouTubeRequest
        /// </summary>
        /// <param name="settings"></param>
        public YouTubeRequest(YouTubeRequestSettings settings) : base(settings)
        {
            if (settings.Client != null && settings.DeveloperKey != null)
            {
                this.Service = new YouTubeService(settings.Application, settings.Client, settings.DeveloperKey);
            }
            else
            {
                this.Service = new YouTubeService(settings.Application);
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
        /// <param name="feedspec">the string representation of the URI to use</param>
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
             return new Feed<Comment>(null);
        }

        /** 
           <summary>
            returns the feed of videos for a given playlist
           </summary>
            <example>
                The following code illustrates a possible use of   
                the <c>GetPlaylist</c> method:  
                <code>    
                  YouTubeRequestSettings settings = new YouTubeRequestSettings("yourApp", "yourClient", "yourKey", "username", "pwd");
                  YouTubeRequest f = new YouTubeRequest(settings);
                  Feed&lt;Playlist&gt; feed = f.GetPlaylistsFeed(null);
                </code>
            </example>
            <param name="p">the playlist to get the videos for</param>
            <returns></returns>
        */
        public Feed<Video> GetPlaylist(Playlist p)
        {
            if (p.AtomEntry != null && 
                p.AtomEntry.Content != null && 
                p.AtomEntry.Content.AbsoluteUri != null)
            {
                   YouTubeQuery q = new YouTubeQuery(p.AtomEntry.Content.AbsoluteUri);
                   return PrepareFeed<Video>(q); 
            }
            return new Feed<Video>(null);
        }

        /// <summary>
        /// uploads or inserts a new video for a given user.
        /// </summary>
        /// <param name="userName">if this is null the default authenticated user will be used</param>
        /// <param name="v">the created video to be used</param>
        /// <returns></returns>
        public Video Upload(string userName, Video v)
        {
            Video rv = null;
            YouTubeEntry e = this.Service.Upload(v.YouTubeEntry);
            if (e != null)
            {
                rv= new Video();
                rv.AtomEntry = e; 
            }
            return rv; 
        }

    }
}
