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
        List<T> list; 

        public Feed(AtomFeed af)
        {
            this.af = af; 
        }

        public AtomFeed AtomFeed
        {
            get
            {
                return this.af;
            }
        }


        /// <summary>
        /// loads data from the underlying server. This will append the new data to the end of the list
        /// </summary>
        /// <param name="limit">the maximum number of entries to load. if -1, loads all</param>
        /// <param name="offset">the offset to start with</param>
        /// <returns>the number or entries added</returns>
        public int Load(int limit, int offset)
        {
            bool fDone = false; 
            int  added = 0; 

            YouTubeQuery q = new YouTubeQuery(this.af.Self);
            q.StartIndex = offset;
            if (limit > 0)
            {
                q.NumberToRetrieve = limit; 
            }
            while (fDone == false)
            {
                fDone = true;
                AtomFeed f = this.af.Service.Query(q);
                foreach (AtomEntry e in af.Entries)
                {
                    T t = new T();
                    if (t != null)
                    {
                        t.AtomEntry = e; 
                        this.list.Add(t);
                    }
                    added++; 
                }
                if (limit <= 0 || added < limit)
                {
                    if (f.NextChunk != null)
                    {
                        q = new YouTubeQuery(f.NextChunk); 
                        fDone= false; 
                    }
                }
            }
            return added; 
        }

        public List<T> Entries
        {
            get
            {
                if (this.list == null)
                {
                    this.list = new List<T>(); 
    
                    foreach (AtomEntry e in af.Entries)
                    {
                        T t = new T();
                        if (t != null)
                        {
                            t.AtomEntry = e; 
                            this.list.Add(t);
                        }
                    }
    
                }
                return this.list;
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
    }
}
