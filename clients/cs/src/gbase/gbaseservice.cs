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

#region Using directives

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Google.GData.Client;

#endregion

namespace Google.GData.GoogleBase
{

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Google Base service object
    ///
    /// Make all accesses through this service object, as it takes
    /// care of setting Google Base-specific headers (X-Google-Key).
    /// and handles authentication.</summary>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseService : Service
    {
        /// <summary>Service name identifying Google Base.</summary>
        public const string GBaseServiceName = "gbase";
        private readonly string applicationName;
        private readonly string devKey; 
        private Uri authHandler; 
        private bool authHandlerSet; 


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a new service object.</summary>
        /// <param name="applicationName">the name of your application, usually
        /// in the format:
        /// <c>CompanyOrAuthorName-ApplicationName-ApplicationVersion</c>.
        /// This name will be sent when logging in, in the UserAgent
        /// header. It will also be used to set the tag g:application
        /// of entries inserted and updated by this feed.</param>
        /// <param name="developerKey">a key you got from
        /// http://code.google.com/apis/base/signup.html</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseService(string applicationName, string developerKey)
                : base(GBaseServiceName, applicationName)
        {
            if (developerKey == null)
            {
                throw new ArgumentNullException("developerKey"); 
            }

            this.applicationName = applicationName;
            this.devKey = developerKey; 


            GBaseParse parse = new GBaseParse();
            this.NewAtomEntry += parse.FeedParserEventHandler;
            this.NewExtensionElement += parse.ExtensionElementEventHandler;

            // setup headers for the devkey
            OnRequestFactoryChanged(); 

        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Sets user name and password (Programmatic Login),
        /// connecting to a specific Uri for authentication.
        ///
        /// By default, the uri that is used is:
        /// https://www.google.com/accounts/</summary>
        /// <param name="username">user name</param>
        /// <param name="password">password</param>
        /// <param name="authUri">Uri used for authentication, instead
        /// of the default
        /// <c>https://www.google.com/accounts/ClientLogin</c></param>
        ///////////////////////////////////////////////////////////////////////
        public void setUserCredentials(String username, String password, Uri authUri)
        {
            setUserCredentials(username, password); 
            this.authHandler = authUri; 
            this.authHandlerSet = true; 
            OnRequestFactoryChanged(); 
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>Replaces the default AtomFeed with a GBaseFeed
        /// for the Query Batch operations.</summary>
        //////////////////////////////////////////////////////////////////////
        protected override AtomFeed CreateFeed(Uri uriToUse)
        {
            return new GBaseFeed(uriToUse, this);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Executes a Google Base Query</summary>
        /// <param name="feedQuery">the query to run</param>
        /// <returns>the feed returned by the server</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseFeed Query(GBaseQuery feedQuery)
        {
            return Query((FeedQuery)feedQuery) as GBaseFeed;
        }



        /// <summary>
        /// notifier if someone changes the requestfactory of the service
        /// </summary>
        public override void OnRequestFactoryChanged() 
        {

            base.OnRequestFactoryChanged();
            GDataGAuthRequestFactory factory = this.RequestFactory as GDataGAuthRequestFactory;
            if (factory != null && this.devKey != null)
            {
                RemoveWebKey(factory.CustomHeaders);
                factory.CustomHeaders.Add(GoogleAuthentication.WebKey + this.devKey); 
                if (this.authHandlerSet)
                {
                    factory.Handler = this.authHandler == null ? null : this.authHandler.ToString();
                }
                
            }
        }

        private static void RemoveWebKey(List<string> headers)
        {
            foreach (string header in headers)
            {
                if (header.StartsWith(GoogleAuthentication.WebKey))
                {
                    headers.Remove(header);
                    return;
                }
            }
            return;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Takes a given feed, and does a batch post of that feed
        /// against the batchUri parameter. If that one is NULL
        /// it will try to use the batch link URI in the feed
        /// </summary>
        /// <param name="feed">the feed to post</param>
        /// <param name="batchUri">the URI to user</param>
        /// <returns>the returned AtomFeed</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseFeed Batch(GBaseFeed feed, Uri batchUri)
        {
            // Sets the flag g:application in even entries
            foreach (AtomEntry entry in feed.Entries)
            {
                new GBaseAttributes(entry.ExtensionElements).Application
                = applicationName;
            }
            return Batch((AtomFeed)feed, batchUri) as GBaseFeed;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Updates an entry</summary>
        /// <param name="entry">An entry with a valid id and content</param>
        /// <returns>the entry as it has been updated and returned by the
        /// server.</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseEntry Update(GBaseEntry entry)
        {
            entry.GBaseAttributes.Application = applicationName;
            return base.Update((AtomEntry)entry) as GBaseEntry;
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Inserts an entry into a feed.</summary>
        /// <param name="feed">a feed object you got from a previous
        /// query</param>
        /// <param name="entry">the entry to add to the feed</param>
        /// <returns>the same entry, with a new id, as it has been added
        /// into the feed</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseEntry Insert(GBaseFeed feed, GBaseEntry entry)
        {
            entry.GBaseAttributes.Application = applicationName;
            return base.Insert((AtomFeed)feed, (AtomEntry)entry) as GBaseEntry;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Inserts an entry into a feed.</summary>
        /// <param name="feedUri">the Uri of a feed, usually created
        /// by <see cref="GBaseUriFactory"/></param>
        /// <param name="entry">the entry to add to the feed</param>
        /// <returns>the same entry, with a new id, as it has been added
        /// into the feed</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseEntry Insert(Uri feedUri, GBaseEntry entry)
        {
            entry.GBaseAttributes.Application = applicationName;
            return base.Insert(feedUri, entry) as GBaseEntry;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets a specific entry from the server.</summary>
        /// <param name="entryUri">the Uri of a entry, usually created
        /// by <see cref="GBaseUriFactory"/></param>
        /// <returns>the entry</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseEntry GetEntry(Uri entryUri)
        {
            Stream feedStream = Query(entryUri);
            try
            {
                GBaseFeed feed = new GBaseFeed(entryUri, this);
                feed.Parse(feedStream, AlternativeFormat.Atom);
                if (feed.Entries.Count == 0)
                {
                    return null;
                }
                else
                {
                    return feed.Entries[0] as GBaseEntry;
                }
            }
            finally
            {
                feedStream.Close();
            }
        }
    }

}
