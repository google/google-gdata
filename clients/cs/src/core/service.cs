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

    //////////////////////////////////////////////////////////////////////
    /// <summary>base Service implementation
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class Service : IService  
    {
        /// <summary>this service's user-agent</summary> 
        public const string GServiceAgent = "GService-CS/1.0.0";
        /// <summary>holds the credential information</summary> 
        private ICredentials credentials; 
        /// <summary>the GDatarequest to use</summary> 
        private IGDataRequestFactory GDataRequestFactory;
        /// <summary>holds the hooks for the eventing in the feedparser</summary> 
        public event FeedParserEventHandler NewAtomEntry;
        /// <summary>eventhandler, when the parser finds a new extension element-> mirrored from underlying parser</summary> 
        public event ExtensionElementEventHandler NewExtensionElement;


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
        /// <summary>default constructor, sets the default GDataRequest</summary> 
        //////////////////////////////////////////////////////////////////////
        public Service(string service, string applicationName)
        {
            this.GDataRequestFactory = new GDataGAuthRequestFactory(service, applicationName, GServiceAgent);
        }
        /////////////////////////////////////////////////////////////////////////////
 

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor, sets the default GDataRequest</summary> 
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
            set {this.GDataRequestFactory = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public ICredentials Credentials</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public ICredentials Credentials
        {
            get {return this.credentials;}
            set {this.credentials = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

   
        //////////////////////////////////////////////////////////////////////
        /// <summary>the basic interface. Take a URI and just get it</summary> 
        /// <param name="queryUri">the URI to execute</param>
        /// <returns> a webresponse object</returns>
        //////////////////////////////////////////////////////////////////////
        public Stream Query(Uri queryUri)
        {
            Tracing.Timestamp("Service.Query - Stream - Enter"); 
            if (queryUri == null)
            {
                throw new System.ArgumentNullException("queryUri");
            }
            IGDataRequest request = this.RequestFactory.CreateRequest(GDataRequestType.Query, queryUri);
            request.Credentials = this.Credentials;

            request.Execute();
            // return the response
            Tracing.Timestamp("Service.Query - Stream - Exit"); 
            return request.GetResponseStream();
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>executes the query and returns an AtomFeed object tree</summary> 
        /// <param name="feedQuery">the query parameters as a FeedQuery object </param>
        /// <returns>AtomFeed object tree</returns>
        //////////////////////////////////////////////////////////////////////
        public AtomFeed Query(FeedQuery feedQuery)
        {
            AtomFeed feed = null;
            Tracing.Timestamp("Service.Query - Feed - Enter"); 

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

                feed = new AtomFeed(feedQuery.Uri, this);

                feed.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry); 
                feed.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewExtensionElement);

                // TODO: for now, to be more relaxed, just always parse the thing...
                feed.Parse(responseStream, AlternativeFormat.Atom); 
                responseStream.Close();
            }
            Tracing.Timestamp("Service.Query - Feed - Exit"); 
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

            AtomFeed returnFeed = new AtomFeed(target, this);

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

            AtomFeed returnFeed = new AtomFeed(feedUri, this);

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

            AtomFeed returnFeed = new AtomFeed(uriToUse, this);


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
                Tracing.TraceMsg("Deleting entry: " + entry.EditUri.ToString()); 
                IGDataRequest request = this.RequestFactory.CreateRequest(GDataRequestType.Delete,new Uri(entry.EditUri.ToString()));
                request.Credentials = this.Credentials;
                request.Execute();
                IDisposable disp = request as IDisposable;
                disp.Dispose(); 
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
