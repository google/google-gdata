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
using System.Collections.Specialized;

#endregion

/////////////////////////////////////////////////////////////////////
// <summary>contains GDataRequest our thin wrapper class for request/response
//  </summary>
////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{

    /// <summary>
    /// the class holds username and password to replace networkcredentials
    /// </summary>
    public class GDataCredentials 
    {
        private string passWord;
        private string userName;
  
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="username">the username to use</param>
        /// <param name="password">the password to use</param>
        public GDataCredentials(string username, string password)
        {
            this.userName = username;
            this.passWord = password;
        }
        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Username</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Username
        {
            get {return this.userName;}
            set {this.userName = value;}
        }
        // end of accessor public string Username

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method Password</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Password
        {
            set {this.passWord = value;}
        }


        internal string getPassword()
        {
            return this.passWord;
        }
        // end of accessor Password

        /// <summary>
        /// returns a windows conforming NetworkCredential 
        /// </summary>
        public ICredentials NetworkCredential 
        {
            get 
            {
                return new NetworkCredential(this.userName, this.passWord);
            }
        }
     }

    
    //////////////////////////////////////////////////////////////////////
    /// <summary>base GDataRequestFactory implementation</summary> 
    //////////////////////////////////////////////////////////////////////
    public class GDataRequestFactory : IGDataRequestFactory  
    {
        /// <summary>this factory's agent</summary> 
        public const string GDataAgent = "GData-CS/1.0.0";
        /// <summary>holds the user-agent</summary> 
        private string userAgent;
        private StringCollection customHeaders;     // holds any custom headers to set
        private String shardingCookie;              // holds the sharding cookie if returned
        private WebProxy webProxy;                  // holds a webproxy to use
        private bool keepAlive;                     // indicates wether or not to keep the connection alive
        private bool useGZip;
        private string contentType = "application/atom+xml; charset=UTF-8";
        private string slugHeader;

        /// <summary>Cookie setting header, returned from server</summary>
        public const string SetCookieHeader = "Set-Cookie"; 
        /// <summary>Cookie client header</summary>
        public const string CookieHeader = "Cookie"; 
        /// <summary>Slug client header</summary>
        public const string SlugHeader = "Slug";


        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public GDataRequestFactory(string userAgent)
        {
            if (userAgent != null) 
            {
              this.userAgent = userAgent + " " + GDataAgent;
            } 
            else 
            {
              this.userAgent = GDataAgent;
            }
            this.keepAlive = true;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public virtual IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget)
        {
            return new GDataRequest(type, uriTarget, this);
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>set wether or not new request should use GZip</summary>
        //////////////////////////////////////////////////////////////////////
        public bool UseGZip
        {
            get { return (this.useGZip); }
            set { this.useGZip = value; }
        }
        //////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>set's and get's the sharding cookie</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Cookie
        {
            get {return this.shardingCookie;}
            set {this.shardingCookie = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>set's and get's the content Type, used for binary transfers</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string ContentType
        {
            get {return this.contentType;}
            set {this.contentType = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>set's and get's the slug header, used for binary transfers
        /// note that the data will be URLencoded before send</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Slug
        {
            get {return this.slugHeader;}
            set {this.slugHeader = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string UserAgent</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public virtual string UserAgent
        {
            get { return this.userAgent; }
            set {this.userAgent = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method to the webproxy object to use</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public WebProxy Proxy
        {
            get {return this.webProxy;}
            set {this.webProxy = value;}
        }
        /////////////////////////////////////////////////////////////////////////////
    
        /////////////////////////////////////////////////////////////////////
        /// <summary>indicates if the connection should be kept alive, default
        /// is true</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public bool KeepAlive
        {
          get {return this.keepAlive;}
          set {this.keepAlive = value;}
        }
        /////////////////////////////////////////////////////////////////////////////
         
        internal bool hasCustomHeaders
        {
            get {
                return this.customHeaders != null; 
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public StringArray CustomHeaders</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public StringCollection CustomHeaders
        {
            get {
                if (this.customHeaders == null) 
                {
                    this.customHeaders = new StringCollection(); 
                }
                return this.customHeaders;
                }
            set {this.customHeaders = value;}
        }
        // end of accessor public StringArray CustomHeaders


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
        private GDataCredentials credentials; 
        /// <summary>holds the request if a stream is open</summary> 
        private Stream requestStream;
        private GDataRequestFactory factory; // holds the factory to use
        /// <summary>holds if we are disposed</summary> 
        protected bool disposed; 
        /// <summary>set wheter or not this request should use GZip</summary>
        private bool useGZip;
        /// <summary>stream from the response</summary>
        private Stream  responseStream;
        /// <summary>holds the contenttype to use if overridden</summary>
        private string contentType;
        /// <summary>holds the slugheader to use if overridden</summary>
        private string slugHeader;

   
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        internal GDataRequest(GDataRequestType type, Uri uriTarget, GDataRequestFactory factory)
        {
            this.type = type;
            this.targetUri = uriTarget;
            this.factory = factory; 
            this.useGZip = this.factory.UseGZip; // use gzip setting from factory
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


        /// <summary>
        /// exposing the private targetUri so that subclasses can override
        /// the value for redirect handling
        /// </summary>
        protected Uri TargetUri 
        {
            get 
            {
                return this.targetUri;
            }
            set 
            {
                this.targetUri = value;
            }
        }
        //////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>set wether or not this request should use GZip</summary>
        //////////////////////////////////////////////////////////////////////
        public bool UseGZip
        {
            get { return (this.useGZip); }
            set { this.useGZip = value; }
        }
        //////////////////////////////////////////////////////////////////////
        
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
        /// <summary>accessor method for the GDataCredentials used</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public GDataCredentials Credentials
        {
            get {return this.credentials;}
            set {this.credentials = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

       //////////////////////////////////////////////////////////////////////
        /// <summary>set's and get's the content Type, used for binary transfers</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string ContentType
        {
            get {return this.contentType == null ? this.factory.ContentType : this.contentType;}
            set {this.contentType = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>set's and get's the slugHeader, used for binary transfers</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Slug
        {
            get {return this.slugHeader == null ? this.factory.Slug : this.slugHeader;}
            set {this.slugHeader = value;}
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
            this.requestStream = this.webRequest.GetRequestStream();
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
                        case GDataRequestType.Batch:
                        case GDataRequestType.Insert:
                            web.Method = HttpMethods.Post;
                            break;
                        case GDataRequestType.Query:
                            web.Method = HttpMethods.Get;
                            break;
    
                    }
                    if (this.useGZip == true)
                        web.Headers.Add("Accept-Encoding", "gzip");

                    web.ContentType = this.ContentType;
                    web.UserAgent = this.factory.UserAgent;
                    web.KeepAlive = this.factory.KeepAlive; 
             
                    // add all custom headers
                    if (this.factory.hasCustomHeaders == true)
                    {
                        foreach (string s in this.factory.CustomHeaders)
                        {
                            this.Request.Headers.Add(s); 
                        }
                    }
                    if (this.Slug != null)
                    {
                        this.Request.Headers.Add(GDataRequestFactory.SlugHeader + ": " + Utilities.UriEncodeReserved(this.Slug));
                    }
                    if (this.factory.Cookie != null)
                    {
                        this.Request.Headers.Add(GDataRequestFactory.CookieHeader + ": " + this.factory.Cookie); 
                    }
                    if (this.factory.Proxy != null)
                    {
                        this.Request.Proxy = this.factory.Proxy; 
                    }
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
                this.webRequest.Credentials = this.Credentials.NetworkCredential; 
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
                GDataRequestException gde = new GDataRequestException("Execution of request failed: " + this.targetUri.ToString(), e);
                throw gde;
            }
            if (this.webResponse != null)
                this.responseStream = this.webResponse.GetResponseStream();
            if (this.webResponse is HttpWebResponse)
            {
                HttpWebResponse response = this.webResponse as HttpWebResponse;
                HttpWebRequest request = this.webRequest as HttpWebRequest;

                this.useGZip = (string.Compare(response.ContentEncoding, "gzip", true) == 0);
                if (this.useGZip == true)
                    this.responseStream = new GZipStream(this.responseStream, CompressionMode.Decompress);

                Tracing.Assert(response != null, "The response should not be NULL"); 
                Tracing.Assert(request != null, "The request should not be NULL"); 

                int code = (int)response.StatusCode;

                Tracing.TraceMsg("Returned contenttype is: " + (response.ContentType == null ? "None" : response.ContentType) + " from URI : " + request.RequestUri.ToString()); ; 
                Tracing.TraceMsg("Returned statuscode is: " + response.StatusCode + code); 

                // check for a returned set-cookie header and store it
                if (response != null && 
                    response.Headers != null)
                {
                    String cookie = response.Headers[GDataRequestFactory.SetCookieHeader];
                    if (cookie != null)
                    {
                        this.factory.Cookie = cookie; 
                    }
                }

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    // that could imply that we need to reauthenticate
                    Tracing.TraceMsg("need to reauthenticate");
                    throw new GDataForbiddenException("Execution of request returned HttpStatusCode.Forbidden: " + 
                                                    this.targetUri.ToString() + response.StatusCode.ToString(), this.webResponse); 
                }

            
                if (response.StatusCode == HttpStatusCode.Redirect ||
                    response.StatusCode == HttpStatusCode.Found ||
                    response.StatusCode == HttpStatusCode.RedirectKeepVerb)
                {
                    Tracing.TraceMsg("throwing for redirect");
                    throw new GDataRedirectException("Execution resulted in a redirect from " + this.targetUri.ToString(), this.webResponse);
                }

                if (request.Method == HttpMethods.Delete && response.StatusCode != HttpStatusCode.OK)
                {
                    Tracing.TraceCall("GDataRequest::Deletion returned unxepected result: " + response.StatusCode.ToString()); 
                    throw new GDataRequestException("Execution of DELETE returned unexpected result: " + this.targetUri.ToString(), this.webResponse); 
                }


                if (code > 299)
                {
                    // treat everything else over 300 as errors
                    throw new GDataRequestException("Execution of request returned unexpected result: " + this.targetUri.ToString() + 
                                                    response.StatusCode.ToString(), this.webResponse); 
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
            return (this.responseStream);
        }
        /////////////////////////////////////////////////////////////////////////////

    }
    /////////////////////////////////////////////////////////////////////////////
} 
/////////////////////////////////////////////////////////////////////////////
