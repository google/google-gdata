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
using System.IO;
using System.Net;

#endregion

/////////////////////////////////////////////////////////////////////
// <summary>contains GDataRequest our thin wrapper class for request/response
//  </summary>
////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{


    
    //////////////////////////////////////////////////////////////////////
    /// <summary>base GDataRequestFactory implementation</summary> 
    //////////////////////////////////////////////////////////////////////
    public class GDataRequestFactory : IGDataRequestFactory  
    {
        /// <summary>this factory's agent</summary> 
        public const string GDataAgent = "GData-CS/1.0.0";
        /// <summary>holds the user-agent</summary> 
        private string userAgent;


   
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public GDataRequestFactory(string userAgent)
        {
            if (userAgent != null) {
	        this.userAgent = userAgent + " " + GDataAgent;
            } else {
	        this.userAgent = GDataAgent;
	    }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget)
        {
            return new GDataRequest(type, uriTarget, this.UserAgent);
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string UserAgent</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string UserAgent
        {
            get {return this.userAgent;}
            set {this.userAgent = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


    }
    /////////////////////////////////////////////////////////////////////////////


    //////////////////////////////////////////////////////////////////////
    /// <summary>base GDataRequest implmentation</summary> 
    //////////////////////////////////////////////////////////////////////
    public class GDataRequest : IGDataRequest, IDisposable
    {
        /// <summary>holds the webRequest object</summary> 
        private WebRequest webRequest; 
        /// <summary>holds the webresponse object</summary> 
        private WebResponse webResponse;
        /// <summary>holds the target Uri</summary> 
        private Uri targetUri;
        /// <summary>holds request type</summary> 
        private GDataRequestType type;
        /// <summary>holds the credential information</summary> 
        private ICredentials credentials; 
        /// <summary>holds the user-agent</summary> 
        private string userAgent;
        /// <summary>holds the request if a stream is open</summary> 
        private Stream requestStream;
        /// <summary>holds if we are disposed</summary> 
        protected bool disposed; 


   
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        internal GDataRequest(GDataRequestType type, Uri uriTarget, string userAgent)
        {
            this.type = type;
            this.targetUri = uriTarget;
	        this.userAgent = userAgent;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>implements the disposable interface</summary> 
        //////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /////////////////////////////////////////////////////////////////////////////


        
        //////////////////////////////////////////////////////////////////////
        /// <summary>does the real disposition</summary> 
        /// <param name="disposing">indicates if dispose called it or finalize</param>
        //////////////////////////////////////////////////////////////////////
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed == true)
            {
                return;
            }
            if (disposing == true)
            {
                Tracing.TraceMsg("disposing of request"); 
                this.Reset(); 
                this.disposed = true;
            }
        }



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
        /// <summary>accessor method protected WebRequest Request</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected WebRequest Request
        {
            get {return this.webRequest;}
            set {this.webRequest = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method protected WebResponse Response</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected WebResponse Response
        {
            get {return this.webResponse;}
            set {this.webResponse = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>resets the object's state</summary> 
        //////////////////////////////////////////////////////////////////////
        protected virtual void Reset()
        {
            this.requestStream = null;
            this.webRequest = null;
            if (this.webResponse != null)
            {
                this.webResponse.Close();
            }
            this.webResponse = null; 
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>returns the writable request stream</summary> 
        /// <returns> the stream to write into</returns>
        //////////////////////////////////////////////////////////////////////
        public virtual Stream GetRequestStream()
        {

            EnsureWebRequest();
            this.requestStream = this.webRequest.GetRequestStream() ;
            return this.requestStream; 
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>ensures that the correct HTTP verb is set on the stream</summary> 
        //////////////////////////////////////////////////////////////////////
        protected virtual void EnsureWebRequest()
        {

            if (this.webRequest == null && this.targetUri != null)
            {
                this.webRequest = WebRequest.Create(this.targetUri);
                this.webResponse = null; 
                if (this.webRequest == null)
                {
                    throw new OutOfMemoryException("Could not create a new Webrequest"); 
                }
                HttpWebRequest web = this.webRequest as HttpWebRequest;

                Tracing.Assert(web != null, "Ensure WebRequest got a NON HttpWebRequest, unclear codepath"); 

                if (web != null)
                {
                    switch (this.type) 
                    {
                        case GDataRequestType.Delete:
                            web.Method = HttpMethods.Delete;
                            break;
                        case GDataRequestType.Update:
                            web.Method = HttpMethods.Put;
                            break;
                        case GDataRequestType.Insert:
                            web.Method = HttpMethods.Post;
                            break;
                        case GDataRequestType.Query:
                            web.Method = HttpMethods.Get;
                            break;
    
                    }
                    web.ContentType = "application/atom+xml; charset=UTF-8";
                    web.UserAgent = this.userAgent;
                }
                
                EnsureCredentials(); 
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>sets up the correct credentials for this call, pending 
        /// security scheme</summary> 
        //////////////////////////////////////////////////////////////////////
        protected virtual void EnsureCredentials()
        {
            if (this.Credentials != null)
            {
                this.webRequest.Credentials = this.Credentials; 
            }
        }
        /////////////////////////////////////////////////////////////////////////////

    

        //////////////////////////////////////////////////////////////////////
        /// <summary>Executes the request and prepares the response stream. Also 
        /// does error checking</summary> 
        //////////////////////////////////////////////////////////////////////
        public virtual void Execute()
        {
            try 
            {
                EnsureWebRequest();
                // if we ever handed out a stream, we want to close it before doing the real excecution
                if (this.requestStream != null)
                {
                    this.requestStream.Close(); 
                }
                Tracing.TraceCall("calling the real execution over the webresponse");
                this.webResponse = this.webRequest.GetResponse(); 
            }
            catch (WebException e)
            {
                Tracing.TraceCall("GDataRequest::Execute failed: " + this.targetUri.ToString()); 
                throw new GDataRequestException("Execution of request failed: " + this.targetUri.ToString(), e);
            }
            if (this.webResponse is HttpWebResponse)
            {
                HttpWebResponse response = this.webResponse as HttpWebResponse;
                HttpWebRequest request = this.webRequest as HttpWebRequest;

                Tracing.Assert(response != null, "The response should not be NULL"); 
                Tracing.Assert(request != null, "The request should not be NULL"); 

                int code = (int)response.StatusCode;

                Tracing.TraceMsg("Returned contenttype is: " + (response.ContentType == null ? "None" : response.ContentType) + " from URI : " + request.RequestUri.ToString()); ; 
                Tracing.TraceMsg("Returned statuscode is: " + response.StatusCode + code); 

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    // that could imply that we need to reauthenticate
                    Tracing.TraceMsg("need to reauthenticate");
                    throw new GDataRequestException("Execution of request returned unexpected result: " + this.targetUri.ToString() + response.StatusCode.ToString(), this.webResponse); 
                }
  
                if (code > 299)
                {
                    // treat everything 300 and up as errors
                    Tracing.TraceMsg("throwing for redirect");
                    throw new GDataRequestException("Execution of request returned unexpected result: " + this.targetUri.ToString() + response.StatusCode.ToString(), this.webResponse); 
                }

                if (request.Method == HttpMethods.Delete && response.StatusCode != HttpStatusCode.OK)
                {
                    Tracing.TraceCall("GDataRequest::Deletion returned unxepected result: " + response.StatusCode.ToString()); 
                    throw new GDataRequestException("Execution of DELETE returned unexpected result: " + this.targetUri.ToString(), this.webResponse); 
                }
                response = null;
                request = null; 
            }
            // now we need to check all kinds of error returns... 

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>gets the readable response stream</summary> 
        /// <returns> the response stream</returns>
        //////////////////////////////////////////////////////////////////////
        public virtual Stream GetResponseStream()
        {
            return this.webResponse != null ? this.webResponse.GetResponseStream() : null;
        }
        /////////////////////////////////////////////////////////////////////////////

    }
    /////////////////////////////////////////////////////////////////////////////
} 
/////////////////////////////////////////////////////////////////////////////
