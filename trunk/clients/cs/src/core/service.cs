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

#define USE_TRACING

using System;
using System.Xml;
using System.IO;
using System.Net;

#endregion

/////////////////////////////////////////////////////////////////////
// <summary>contains Service, the base interface that 
//   allows to query a service for different feeds
//  </summary>
////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{

    /// <summary>
    /// EventArgument class for service level events during parsing
    /// </summary>
    public class ServiceEventArgs : EventArgs
    {
        private AtomFeed feedObject;
        private IService service;
        private Uri uri;

        /// <summary>
        /// constructor. Takes the URI and the service this event applies to
        /// </summary>
        /// <param name="uri">URI currently executed</param>
        /// <param name="service">service object doing the execution</param>
        public ServiceEventArgs(Uri uri, IService service) 
        {
            this.service = service;
            this.uri = uri;
            this.feedObject = null;
        }
    
        /// <summary>the feed to be created. If this is NULL, a service 
        /// will create a DEFAULT atomfeed</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AtomFeed Feed
        {
            get {return this.feedObject;}
            set {this.feedObject = value;}
        }
        ////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        /// <summary>the service to be used for the feed to be created. </summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public IService Service
        {
            get {return this.service;}
        }
        ////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        /// <summary>the Uri to be used</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public Uri Uri
        {
            get {return this.uri;}
        }
        ////////////////////////////////////////////////////////////////////////
   }



    /// <summary>Delegate declaration for the feed creation in a service</summary> 
    public delegate void ServiceEventHandler(object sender, ServiceEventArgs e);


   
    //////////////////////////////////////////////////////////////////////
    /// <summary>base Service implementation
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class Service : IService  
    {
        /// <summary>this service's user-agent</summary> 
        public const string GServiceAgent = "GService-CS/1.0.0";
        /// <summary>holds the credential information</summary> 
        private GDataCredentials credentials; 
        /// <summary>the GDatarequest to use</summary> 
        private IGDataRequestFactory GDataRequestFactory;
        /// <summary>holds the hooks for the eventing in the feedparser</summary> 
        public event FeedParserEventHandler NewAtomEntry;
        /// <summary>eventhandler, when the parser finds a new extension element-> mirrored from underlying parser</summary> 
        public event ExtensionElementEventHandler NewExtensionElement;
        /// <summary>eventhandler, when the service needs to create a new feed</summary> 
        public event ServiceEventHandler NewFeed;


        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor, sets the default GDataRequest</summary> 
        //////////////////////////////////////////////////////////////////////
        public Service()
        {
            this.GDataRequestFactory = new GDataRequestFactory(GServiceAgent);
        }
        /////////////////////////////////////////////////////////////////////////////
 

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor, sets the default GDataRequest</summary> 
        //////////////////////////////////////////////////////////////////////
        public Service(string applicationName)
        {
            this.GDataRequestFactory = new GDataRequestFactory(applicationName + " " + GServiceAgent);
        }
        /////////////////////////////////////////////////////////////////////////////
 

        //////////////////////////////////////////////////////////////////////
        /// <summary>this will trigger the creation of an authenticating service</summary> 
        //////////////////////////////////////////////////////////////////////
        public Service(string service, string applicationName)
        {
            this.GDataRequestFactory = new GDataGAuthRequestFactory(service, applicationName, GServiceAgent);
        }
        /////////////////////////////////////////////////////////////////////////////
 

        //////////////////////////////////////////////////////////////////////
        /// <summary>this will trigger the creation of an authenticating servicea</summary> 
        //////////////////////////////////////////////////////////////////////
        public Service(string service, string applicationName, string library)
        {
            this.GDataRequestFactory = new GDataGAuthRequestFactory(service, applicationName, library);
        }
        /////////////////////////////////////////////////////////////////////////////
 

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public IGDataRequest Request</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public IGDataRequestFactory RequestFactory
        {
            get {return this.GDataRequestFactory;}
            set {this.GDataRequestFactory = value; OnRequestFactoryChanged(); }
        }

        /// <summary>
        /// notifier if someone changes the requestfactory of the service
        /// </summary>
        public virtual void OnRequestFactoryChanged() 
        {
            return; 
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public ICredentials Credentials</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public GDataCredentials Credentials
        {
            get {return this.credentials;}
            set {this.credentials = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// if the service is using a Google Request Factory it will use that 
        /// assuming credentials are set to retrieve the authentication token
        /// for those credentials
        /// </summary>
        /// <returns>string</returns>
        public string QueryAuthenticationToken() 
        {
            if (this.Credentials != null)
            {
                GDataGAuthRequestFactory factory = this.GDataRequestFactory as GDataGAuthRequestFactory;
                if (factory != null)
                {
                    return factory.QueryAuthToken(this.Credentials);
                }
            }
            return null;
        }

        /// <summary>
        /// if the service is using a Google Request Factory it will set the passed 
        /// in token to the factory
        /// </summary>
        /// <returns>string</returns>
        public void SetAuthenticationToken(string token) 
        {
            GDataGAuthRequestFactory factory = this.GDataRequestFactory as GDataGAuthRequestFactory;
            if (factory != null)
            {
                factory.GAuthToken = token;
            }
        }

   
        //////////////////////////////////////////////////////////////////////
        /// <summary>the basic interface. Take a URI and just get it</summary> 
        /// <param name="queryUri">the URI to execute</param>
        /// <returns> a webresponse object</returns>
        //////////////////////////////////////////////////////////////////////
        public Stream Query(Uri queryUri)
        {
            Tracing.TraceCall("Enter");
            if (queryUri == null)
            {
                throw new System.ArgumentNullException("queryUri");
            }
            IGDataRequest request = this.RequestFactory.CreateRequest(GDataRequestType.Query, queryUri);
            request.Credentials = this.Credentials;

            request.Execute();
            // return the response
            Tracing.TraceCall("Exit");
            return request.GetResponseStream();
        }
        /////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Sets the credentials of the user to authenticate requests
        /// to the server.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void setUserCredentials(String username, String password)
        {
            this.Credentials = new GDataCredentials(username, password);
        }

        /// <summary>
        /// Returns a single Atom entry based upon its unique URI.
        /// </summary>
        /// <param name="entryUri">The URI of the Atom entry.</param>
        /// <returns>AtomEntry representing the entry.</returns>
        public AtomEntry Get(string entryUri)
        {
            FeedQuery query = new FeedQuery(entryUri);
            AtomFeed resultFeed = Query(query);
            return resultFeed.Entries[0];
        }


   
        //////////////////////////////////////////////////////////////////////
        /// <summary>executes the query and returns an AtomFeed object tree</summary> 
        /// <param name="feedQuery">the query parameters as a FeedQuery object </param>
        /// <returns>AtomFeed object tree</returns>
        //////////////////////////////////////////////////////////////////////
        public AtomFeed Query(FeedQuery feedQuery)
        {
            AtomFeed feed = null;
            Tracing.TraceCall("Enter");

            if (feedQuery == null)
            {
                throw new System.ArgumentNullException("feedQuery", "The query argument MUST not be null");
            }
             // Create a new request to the Uri in the query object...    
            Uri targetUri  = null; 

            try 
            {
                targetUri = feedQuery.Uri;

            }
            catch (System.UriFormatException)
            {
                throw new System.ArgumentException("The query argument MUST contain a valid Uri", "feedQuery");
            }

            Tracing.TraceInfo("Service:Query - about to query"); 

            Stream responseStream = Query(targetUri);

            Tracing.TraceInfo("Service:Query - query done"); 
            if (responseStream != null)
            {
                Tracing.TraceCall("Using Atom always.... ");
                feed = createFeed(feedQuery.Uri);

                feed.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry); 
                feed.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewExtensionElement);
                feed.Parse(responseStream, AlternativeFormat.Atom); 
                responseStream.Close();
            }
            Tracing.TraceCall("Exit");
            return feed;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>object QueryOpenSearchRssDescription()</summary> 
        /// <param name="serviceUri">the service to ask for an OpenSearchRss Description</param> 
        /// <returns> a webresponse object</returns>
        //////////////////////////////////////////////////////////////////////
        public Stream QueryOpenSearchRssDescription(Uri serviceUri)
        {
            if (serviceUri == null)
            {
                throw new System.ArgumentNullException("serviceUri");
            }
            IGDataRequest request = this.RequestFactory.CreateRequest(GDataRequestType.Query, serviceUri);
            request.Credentials = this.Credentials;
            request.Execute();
            // return the response
            return request.GetResponseStream();
        }
        /////////////////////////////////////////////////////////////////////////////





        //////////////////////////////////////////////////////////////////////
        /// <summary>WebResponse Update(Uri updateUri, Stream entryStream, ICredentials credentials)</summary> 
        /// <param name="entry">the old entry to update</param> 
        /// <returns> the new Entry, as returned from the server</returns>
        //////////////////////////////////////////////////////////////////////
        public AtomEntry Update(AtomEntry entry)
        {
            Tracing.Assert(entry != null, "entry should not be null");
            if (entry == null)
            {
                throw new ArgumentNullException("entry"); 
            }

            if (entry.ReadOnly == true)
            {
                throw new GDataRequestException("Can not update a read-only entry"); 
            }


            Uri target = new Uri(entry.EditUri.ToString());

            IGDataRequest request = this.RequestFactory.CreateRequest(GDataRequestType.Update,target);
            request.Credentials = this.Credentials;
            Stream outputStream = request.GetRequestStream();

            entry.SaveToXml(outputStream);
            request.Execute();
            outputStream.Close();

            AtomFeed returnFeed = createFeed(target);

            returnFeed.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry); 
            returnFeed.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewExtensionElement);

            Stream returnStream = request.GetResponseStream();

            returnFeed.Parse(returnStream, AlternativeFormat.Atom);
            // there should be ONE entry echoed back. 
            returnStream.Close(); 

            return returnFeed.Entries[0]; 

        }
        /////////////////////////////////////////////////////////////////////////////


   

        //////////////////////////////////////////////////////////////////////
        /// <summary>public WebResponse Insert(Uri insertUri, Stream entryStream, ICredentials credentials)</summary> 
        /// <param name="feed">the feed this entry should be inserted into</param> 
        /// <param name="entry">the entry to be inserted</param> 
        /// <returns> the inserted entry</returns>
        //////////////////////////////////////////////////////////////////////
        public AtomEntry Insert(AtomFeed feed, AtomEntry entry)
        {

            Tracing.Assert(feed != null, "feed should not be null");
            if (feed == null)
            {
                throw new ArgumentNullException("feed"); 
            }
            Tracing.Assert(entry != null, "entry should not be null");
            if (entry == null)
            {
                throw new ArgumentNullException("entry"); 
            }

            if (feed.ReadOnly == true)
            {
                throw new GDataRequestException("Can not update a read-only feed"); 
            }

            Tracing.TraceMsg("Post URI is: " + feed.Post); 
            Uri target = new Uri(feed.Post); 
            return Insert(target, entry);
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>public WebResponse Insert(Uri insertUri, Stream entryStream, ICredentials credentials)</summary> 
        /// <param name="feedUri">the uri for the feed this entry should be inserted into</param> 
        /// <param name="newEntry">the entry to be inserted</param> 
        /// <returns> the inserted entry</returns>
        //////////////////////////////////////////////////////////////////////
        public AtomEntry Insert(Uri feedUri, AtomEntry newEntry)
        {
            Tracing.Assert(feedUri != null, "feedUri should not be null");
            if (feedUri == null)
            {
                throw new ArgumentNullException("feedUri"); 
            }
            Tracing.Assert(newEntry != null, "newEntry should not be null");
            if (newEntry == null)
            {
                throw new ArgumentNullException("newEntry"); 
            }

            Stream returnStream = StreamInsert(feedUri, newEntry);

            AtomFeed returnFeed = createFeed(feedUri);

            returnFeed.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry); 
            returnFeed.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewExtensionElement);
            returnFeed.Parse(returnStream, AlternativeFormat.Atom);
            returnStream.Close(); 

            // there should be ONE entry echoed back. 

            AtomEntry entry = returnFeed.Entries[0];
            if (entry != null)
            {
                entry.Service  = this;
                entry.setFeed(null);
            }

            return entry; 
        }
        /////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// simple update for media resources
        /// </summary>
        /// <param name="uriTarget"></param>
        /// <param name="input"></param>
        /// <param name="contentType"></param>
        /// <param name="slugHeader">the value for the slug header, indicating filenaming</param>
        /// <returns>AtomEntry</returns>
        public AtomEntry Update(Uri uriTarget, Stream input, string contentType, string slugHeader)
        {
            Stream returnStream = StreamSend(uriTarget, input, GDataRequestType.Update, contentType, slugHeader);
            AtomFeed returnFeed = createFeed(uriTarget);
            returnFeed.Parse(returnStream, AlternativeFormat.Atom);
            // there should be ONE entry echoed back. 
            returnStream.Close(); 
            return returnFeed.Entries[0]; 
        }   

        /// <summary>
        /// Simple insert for media resources
        /// </summary>
        /// <param name="uriTarget"></param>
        /// <param name="input"></param>
        /// <param name="contentType"></param>
        /// <param name="slugHeader">the value for the slug header, indicating filenaming</param>
        /// <returns>AtomEntry</returns>
        public AtomEntry Insert(Uri uriTarget, Stream input, string contentType, string slugHeader)
        {
            Stream returnStream = StreamSend(uriTarget, input, GDataRequestType.Insert, contentType, slugHeader);
            AtomFeed returnFeed = createFeed(uriTarget);
            returnFeed.Parse(returnStream, AlternativeFormat.Atom);
            // there should be ONE entry echoed back. 
            returnStream.Close(); 
            return returnFeed.Entries[0]; 
        }
   


        //////////////////////////////////////////////////////////////////////
        /// <summary>public WebResponse Insert(Uri insertUri, Stream entryStream, ICredentials credentials)</summary> 
        /// <param name="feedUri">the uri for the feed this entry should be inserted into</param> 
        /// <param name="newEntry">the entry to be inserted</param> 
        /// <returns> the inserted entry</returns>
        //////////////////////////////////////////////////////////////////////
        public Stream StreamInsert(Uri feedUri, AtomEntry newEntry)
        {
            return StreamInsert(feedUri, newEntry, GDataRequestType.Insert); 
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>Inserts an AtomBase entry against a Uri</summary> 
        /// <param name="feedUri">the uri for the feed this object should be posted against</param> 
        /// <param name="baseEntry">the entry to be inserted</param> 
        /// <param name="type">the type of request to create</param> 
        /// <returns> the response as a stream</returns>
        //////////////////////////////////////////////////////////////////////
        public Stream StreamInsert(Uri feedUri, AtomBase baseEntry, GDataRequestType type)
        {
            Tracing.Assert(feedUri != null, "feedUri should not be null");
            if (feedUri == null)
            {
                throw new ArgumentNullException("feedUri"); 
            }
            Tracing.Assert(baseEntry != null, "baseEntry should not be null");
            if (baseEntry == null)
            {
                throw new ArgumentNullException("baseEntry"); 
            }

            IGDataRequest request = this.RequestFactory.CreateRequest(type,feedUri);
            request.Credentials = this.Credentials;
            Stream outputStream = request.GetRequestStream();

            baseEntry.SaveToXml(outputStream);
            request.Execute();

            outputStream.Close();
            return request.GetResponseStream();
        }


        /// <summary>
        /// this is a helper function for external utilities. It is not worth
        /// running the other insert/saves through here, as this would involve
        /// double buffering/copying of the bytes
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="payload"></param>
        /// <param name="type"></param>
        /// <returns>Stream</returns>
        
        public Stream StringSend(Uri targetUri, String payload, GDataRequestType type)
        {
            Tracing.Assert(targetUri != null, "targetUri should not be null");
            if (targetUri == null)
            {
                throw new ArgumentNullException("targetUri"); 
            }
            Tracing.Assert(payload != null, "payload should not be null");
            if (payload == null)
            {
                throw new ArgumentNullException("payload"); 
            }

            IGDataRequest request = this.RequestFactory.CreateRequest(type,targetUri);
            request.Credentials = this.Credentials;

            Stream outputStream = request.GetRequestStream();

            StreamWriter w = new StreamWriter(outputStream);
            w.Write(payload);
            w.Flush();
       
            request.Execute();

            w.Close();
            return request.GetResponseStream();
        }



        /// <summary>
        /// this is a helper function for to send binary data to a resource
        /// it is not worth running the other insert/saves through here, as this would involve
        /// double buffering/copying of the bytes
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="inputStream"></param>
        /// <param name="type"></param>
        /// <param name="contentType">the contenttype to use in the request, if NULL is passed, factory default is used</param>
        /// <param name="slugHeader">the slugHeader to use in the request, if NULL is passed, factory default is used</param>
        /// <returns>Stream</returns>
        
        public Stream StreamSend(Uri targetUri, 
                                 Stream inputStream, 
                                 GDataRequestType type, 
                                 string contentType,
                                 string slugHeader)
        {
            Tracing.Assert(targetUri != null, "targetUri should not be null");
            if (targetUri == null)
            {
                throw new ArgumentNullException("targetUri"); 
            }
            if (inputStream == null)
            {
                Tracing.Assert(inputStream != null, "payload should not be null");
                throw new ArgumentNullException("payload"); 
            }
            if (type != GDataRequestType.Insert && type != GDataRequestType.Update)
            {
                Tracing.Assert(type != GDataRequestType.Insert && type != GDataRequestType.Update,"type needs to be insert or update");
                throw new ArgumentNullException("type"); 
            }

            
            IGDataRequest request = this.RequestFactory.CreateRequest(type,targetUri);
            request.Credentials = this.Credentials;

            // set the contenttype of the request
            if (contentType != null)
            {
                GDataRequest r = request as GDataRequest;
                if (r != null)
                {
                    r.ContentType = contentType;
                }
            }

            if (slugHeader != null)
            {
                GDataRequest r = request as GDataRequest;
                if (r != null)
                {
                    r.Slug = slugHeader;
                }
            }
       
            Stream outputStream = request.GetRequestStream();

            BinaryWriter w = new BinaryWriter(outputStream);
            const int size = 4096;
            byte[] bytes = new byte[4096];
            int numBytes;

            while((numBytes = inputStream.Read(bytes, 0, size)) > 0)
            {
                w.Write(bytes, 0, numBytes);
            }
            w.Flush();
            request.Execute();
            w.Close();
            return request.GetResponseStream();
        }






        //////////////////////////////////////////////////////////////////////
        /// <summary>creates a new feed instance to be returned by
        /// Batch(), Query() and other operations
        ///
        /// Subclasses can supply their own feed implementation by
        /// overriding this method.
        /// </summary>
        //////////////////////////////////////////////////////////////////////
        protected virtual AtomFeed createFeed(Uri uriToUse)
        {
            ServiceEventArgs args = null;
            AtomFeed feed = null;

            if (this.NewFeed != null)
            {
                args = new ServiceEventArgs(uriToUse, this);
                this.NewFeed(this, args);
            }

            if (args != null)
            {
                feed = args.Feed;
            }

            if (feed == null)
            {
                feed = new AtomFeed(uriToUse, this);
            }

            return feed;
        }


        /// <summary>
        /// takes a given feed, and does a batch post of that feed
        /// against the batchUri parameter. If that one is NULL 
        /// it will try to use the batch link URI in the feed
        /// </summary>
        /// <param name="feed">the feed to post</param>
        /// <param name="batchUri">the URI to user</param>
        /// <returns>the returned AtomFeed</returns>
        public AtomFeed Batch(AtomFeed feed, Uri batchUri) 
        {
            Uri uriToUse = batchUri; 

            if (uriToUse == null)
            {
                uriToUse = feed.Batch == null ? null : new Uri(feed.Batch); 
            }

            if (uriToUse == null)
            {
                throw new ArgumentNullException("batchUri"); 
            }

            Tracing.Assert(feed != null, "feed should not be null");
            if (feed == null)
            {
                throw new ArgumentNullException("feed"); 
            }

            if (feed.BatchData == null) 
            {
                // setting this will make the feed output the namespace, instead of each entry
                feed.BatchData = new GDataBatchFeedData(); 
            }


            Stream returnStream = StreamInsert(uriToUse, feed, GDataRequestType.Batch);

            AtomFeed returnFeed = createFeed(uriToUse);


            returnFeed.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry); 
            returnFeed.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewExtensionElement);

            returnFeed.Parse(returnStream, AlternativeFormat.Atom);

            returnStream.Close(); 
         
            return returnFeed;  
            
        }
        //////////////////////////////////////////////////////////////////////



    
        //////////////////////////////////////////////////////////////////////
        /// <summary>deletes an Atom entry object</summary> 
        /// <param name="entry"> </param>
        //////////////////////////////////////////////////////////////////////
        public void Delete(AtomEntry entry)
        {
            Tracing.Assert(entry != null, "entry should not be null");
            if (entry == null)
            {
                throw new ArgumentNullException("entry"); 
            }

            if (entry.ReadOnly == true)
            {
                throw new GDataRequestException("Can not update a read-only entry"); 
            }

            Tracing.Assert(entry.EditUri != null, "Entry should have a valid edit URI"); 
            if (entry.EditUri != null)
                
            {
                Delete(new Uri(entry.EditUri.ToString()));
            }
            else
            {
                throw new GDataRequestException("Invalid Entry object (no edit uri) to call Delete on"); 
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        ///<summary>Deletes an Atom entry when given a Uri</summary>
        ///<param name="uri"></param>
        /////////////////////////////////////////////////////////////////////
        public void Delete(Uri uri)
        {
            Tracing.Assert(uri != null, "uri should not be null");
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            Tracing.TraceMsg("Deleting entry: " + uri.ToString());
            IGDataRequest request = RequestFactory.CreateRequest(GDataRequestType.Delete, uri);
            request.Credentials = Credentials;
            request.Execute();
            IDisposable disp = request as IDisposable;
            disp.Dispose();
        }   
        //////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>eventchaining. We catch this by the baseFeedParsers, which 
        /// would not do anything with the gathered data. We pass the event up
        /// to the user</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnParsedNewEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }
            if (this.NewAtomEntry != null)
            {
                // just forward it upstream, if hooked
                Tracing.TraceMsg("\t calling event dispatcher"); 
                this.NewAtomEntry(this, e);
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>eventchaining. We catch this by the baseFeedParsers, which 
        /// would not do anything with the gathered data. We pass the event up
        /// to the user, and if he did not dicscard it, we add the entry to our
        /// collection</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewExtensionElement(object sender, ExtensionElementEventArgs e)
        {
            // by default, if our event chain is not hooked, the underlying parser will add it
            Tracing.TraceCall("received new extension element notification");
            Tracing.Assert(e != null, "e should not be null");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }
            if (this.NewExtensionElement != null)
            {
                Tracing.TraceMsg("\t calling event dispatcher"); 
                this.NewExtensionElement(this, e);
            }
        }
        /////////////////////////////////////////////////////////////////////////////


    }
    /////////////////////////////////////////////////////////////////////////////
} 
/////////////////////////////////////////////////////////////////////////////
