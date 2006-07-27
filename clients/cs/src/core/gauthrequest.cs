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
using System.Text;
using System.Collections;

#endregion

/////////////////////////////////////////////////////////////////////
// <summary>contains GDataRequest, our thin wrapper class for request/response
// </summary>
////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>constants for the authentication handler
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class GoogleAuthentication
    {
        /// <summary>Google client authentication handler</summary>
        public const string UriHandler = "https://www.google.com/accounts/ClientLogin"; 
        /// <summary>Google client authentication email</summary>
        public const string Email = "Email";
        /// <summary>Google client authentication password</summary>
        public const string Password = "Passwd";
        /// <summary>Google client authentication source constant</summary>
        public const string Source = "source";
        /// <summary>Google client authentication default service constant</summary>
        public const string Service = "service";
        /// <summary>Google client authentication LSID</summary>
        public const string Lsid = "LSID";
        /// <summary>Google client authentication SSID</summary>
        public const string Ssid = "SSID";
        /// <summary>Google client authentication Token</summary>
        public const string AuthToken = "Auth"; 
        /// <summary>Google authSub authentication Token</summary>
        public const string AuthSubToken = "token"; 
        /// <summary>Google client header</summary>
        public const string Header = "Authorization: GoogleLogin auth="; 
        /// <summary>Google method override header</summary>
        public const string Override = "X-HTTP-Method-Override"; 
    }
    /////////////////////////////////////////////////////////////////////////////


    //////////////////////////////////////////////////////////////////////
    /// <summary>base GDataRequestFactory implementation</summary> 
    //////////////////////////////////////////////////////////////////////
    public class GDataGAuthRequestFactory : IGDataRequestFactory
    {
        /// <summary>this factory's agent</summary> 
        public const string GDataGAuthAgent = "GDataGAuth-CS/1.0.0";

        private string userAgent;    // holds the user-agent
        private string gAuthToken;   // we want to remember the token here
        private string handler;      // so the handler is useroverridable, good for testing
        private CookieContainer cookies; //  all the cookies we use
        private string gService;         // the service we pass to Gaia for token creation
        private string applicationName;  // the application name we pass to Gaia and append to the user-agent
        private bool fMethodOverride;    // to override using post, or to use PUT/DELETE
                                         

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public GDataGAuthRequestFactory(string service, string applicationName)
        {
    	    this.Service = service;
    	    this.ApplicationName = applicationName;
    	    if (applicationName != null) {
    	        this.userAgent = applicationName + " " + GDataGAuthAgent;
    	    } else {
    	        this.userAgent = GDataGAuthAgent;
    	    }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public GDataGAuthRequestFactory(string service, string applicationName, string library) : this(service, library)
        {
    	    if (applicationName != null) {
    	        this.userAgent = applicationName + " " + this.userAgent;
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public virtual IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget)
        {
            return new GDataGAuthRequest(type, uriTarget, this.UserAgent, this.ApplicationName, this); 
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Get/Set accessor for gAuthToken</summary> 
        //////////////////////////////////////////////////////////////////////
        internal string GAuthToken
        {
            get {return this.gAuthToken;}
            set {
                Tracing.TraceMsg("set token called with: " + value); 
                this.gAuthToken = value;
                }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Get/Set accessor for the cookie box</summary> 
        //////////////////////////////////////////////////////////////////////
        internal CookieContainer GCookies
        {
            get {
                if (this.cookies == null) {
                    this.cookies = new CookieContainer(); 
                }
                return this.cookies;
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Get/Set accessor for the user-agent</summary> 
        //////////////////////////////////////////////////////////////////////
        public string UserAgent
        {
            get {return this.userAgent;}
            set {this.userAgent = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Get/Set accessor for the application name</summary> 
        //////////////////////////////////////////////////////////////////////
        public string ApplicationName
        {
            get {return this.applicationName == null ? "" : this.applicationName;}
            set {this.applicationName = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>returns the service string</summary> 
        //////////////////////////////////////////////////////////////////////
        public string Service
        {
            get {return this.gService;}
            set {this.gService = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public bool MethodOverride</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public bool MethodOverride
        {
            get {return this.fMethodOverride;}
            set {this.fMethodOverride = value;}
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Handler</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Handler
        {
            get {

                return this.handler!=null ? this.handler : GoogleAuthentication.UriHandler; 
            }
            set {this.handler = value;}
        }
        /////////////////////////////////////////////////////////////////////////////
        
    }
    /////////////////////////////////////////////////////////////////////////////


    //////////////////////////////////////////////////////////////////////
    /// <summary>base GDataRequest implementation</summary> 
    //////////////////////////////////////////////////////////////////////
    public class GDataGAuthRequest : GDataRequest
    {
        /// <summary>holds the input in memory stream</summary> 
        private MemoryStream requestCopy;
        /// <summary>holds the factory instance</summary> 
        private GDataGAuthRequestFactory factory; 
        /// <summary>holds the application name</summary> 
        private string applicationName; 
        

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        internal GDataGAuthRequest(GDataRequestType type, Uri uriTarget, string userAgent, string applicationName, GDataGAuthRequestFactory factory)  : base(type, uriTarget, userAgent)
        {
            // need to remember the factory, so that we can pass the new authtoken back there if need be
            this.factory = factory; 
            this.applicationName = applicationName;
        }
        /////////////////////////////////////////////////////////////////////////////




        //////////////////////////////////////////////////////////////////////
        /// <summary>returns the writable request stream</summary> 
        /// <returns> the stream to write into</returns>
        //////////////////////////////////////////////////////////////////////
        public override Stream GetRequestStream()
        {
            this.requestCopy = new MemoryStream(); 
            return this.requestCopy; 
        }
        /////////////////////////////////////////////////////////////////////////////

       //////////////////////////////////////////////////////////////////////
       /// <summary>Read only accessor for requestCopy</summary> 
       //////////////////////////////////////////////////////////////////////
       internal Stream RequestCopy
       {
           get {return this.requestCopy;}
       }
       /////////////////////////////////////////////////////////////////////////////
       

        
        //////////////////////////////////////////////////////////////////////
        /// <summary>does the real disposition</summary> 
        /// <param name="disposing">indicates if dispose called it or finalize</param>
        //////////////////////////////////////////////////////////////////////
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing); 
            if (this.disposed == true)
            {
                return;
            }
            if (disposing == true)
            {
                if (this.requestCopy != null)
                {
                    this.requestCopy.Close();
                    this.requestCopy = null;
                }
                this.disposed = true;
            }

        }




        //////////////////////////////////////////////////////////////////////
        /// <summary>sets up the correct credentials for this call, pending 
        /// security scheme</summary> 
        //////////////////////////////////////////////////////////////////////
        protected override void EnsureCredentials()
        {
            Tracing.Assert(this.Request!= null, "We should have a webrequest now"); 
            if (this.Request == null)
            {
                return; 
            }
            // if the token is NULL, we need to get a token. 
            if (this.factory.GAuthToken == null)
            {
                // we will take the standard credentials for that
                NetworkCredential nc = this.Credentials as NetworkCredential;
                Tracing.TraceMsg(nc == null ? "No Network credentials set" : "Network credentials found"); 
                if (nc != null)
                {
                    // only now we have something to do... 
                    this.factory.GAuthToken = QueryAuthToken(nc); 
                }
            }
            // now add the auth token to the header
            // Tracing.Assert(this.factory.GAuthToken != null, "We should have a token now"); 
            Tracing.TraceMsg("Using auth token: " + this.factory.GAuthToken); 
            string strHeader = GoogleAuthentication.Header + this.factory.GAuthToken; 
            this.Request.Headers.Add(strHeader); 
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>sets the redirect to false after everything else
        /// is done </summary> 
        //////////////////////////////////////////////////////////////////////
        protected override void EnsureWebRequest()
        {
            base.EnsureWebRequest(); 
            HttpWebRequest http = this.Request as HttpWebRequest; 
            if (http != null)
            {
                // we do not want this to autoredirect, our security header will be 
                // lost in that case
                http.AllowAutoRedirect = false;
                // and we want to set a cookie container
                http.CookieContainer = this.factory.GCookies;
                if (this.factory.MethodOverride == true && 
                    http.Method != HttpMethods.Get &&
                    http.Method != HttpMethods.Post)
                {
                    // not put and delete, all is post
                    if (http.Method == HttpMethods.Delete)
                    {
                        http.ContentLength = 0;
                    }
                    http.Headers.Add(GoogleAuthentication.Override, http.Method);
                    http.Method = HttpMethods.Post; 
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>goes to the Google auth service, and gets a new auth token</summary> 
        /// <returns>the auth token, or NULL if none received</returns>
        //////////////////////////////////////////////////////////////////////
        protected string QueryAuthToken(NetworkCredential nc)
        {
            Tracing.Assert(nc != null, "Do not call QueryAuthToken with no network credentials"); 
            if (nc == null)
            {
                throw new System.ArgumentNullException("nc", "No credentials supplied");
            }
            // Create a new request to the authentication URL.    
            Uri authHandler = null; 
            try 
            {
                authHandler = new Uri(this.factory.Handler); 
            }
            catch
            {
                throw new GDataRequestException("Invalid authentication handler URI given"); 
            }

            WebRequest authRequest = WebRequest.Create(authHandler); 
            WebResponse authResponse = null; 

            string authToken = null; 
            try
            {
                authRequest.ContentType = HttpFormPost.Encoding; 
                authRequest.Method = HttpMethods.Post;
                ASCIIEncoding encoder = new ASCIIEncoding();

                // now enter the data in the stream
                string postData = GoogleAuthentication.Email + "=" + nc.UserName + "&"; 
                postData += GoogleAuthentication.Password + "=" + nc.Password + "&";  
                postData += GoogleAuthentication.Source + "=" + this.applicationName + "&"; 
                postData += GoogleAuthentication.Service + "=" + this.factory.Service; 

                byte[] encodedData = encoder.GetBytes(postData);
                authRequest.ContentLength = encodedData.Length; 

                Stream requestStream = authRequest.GetRequestStream() ;
                requestStream.Write(encodedData, 0, encodedData.Length); 
                requestStream.Close();        
                authResponse = authRequest.GetResponse(); 

            } 
            catch (WebException e)
            {
                Tracing.TraceMsg("QueryAuthtoken failed " + e.Status); 
                throw new GDataRequestException("Execution of authentication request failed", e);
            }
            HttpWebResponse response = authResponse as HttpWebResponse;
            if (response != null)
            {
                int code= (int)response.StatusCode;
                if (code != 200)
                {
                    throw new GDataRequestException("Execution of authentication request returned unexpected result: " +code,  this.Response); 
                }
                // check the content type, it must be text
                if (!response.ContentType.Equals(HttpFormPost.ContentType))
                {
                    throw new GDataRequestException("Execution of authentication request returned unexpected content type: " + response.ContentType,  this.Response); 
                }
                // verify the content length. This should not be big, hence a big result might indicate a phoney
                if (response.ContentLength > 1024)
                {
                    throw new GDataRequestException("Execution of authentication request returned unexpected large content length: " + response.ContentLength,  this.Response); 
                }

                authToken = Utilities.ParseValueFormStream(response.GetResponseStream(), GoogleAuthentication.AuthToken); 
            }
            Tracing.Assert(authToken != null, "did not find an auth token in QueryAuthToken"); 
            return authToken;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Executes the request and prepares the response stream. Also 
        /// does error checking</summary> 
        //////////////////////////////////////////////////////////////////////
        public override void Execute()
        {

            Tracing.TraceCall("GoogleAuth: Execution called");
            try
            {
                CopyRequestData(); 
                base.Execute(); 
            }
            catch (GDataRequestException re)
            {
                Tracing.TraceMsg("Got into exception handling for base.execute"); 
                HttpWebResponse response = re.Response as HttpWebResponse;
            
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        Tracing.TraceMsg("need to reauthenticate, got a forbidden back");
                        // do it again, once, reset AuthToken first and streams first
                        this.Reset(); 
                        CopyRequestData(); 
                        base.Execute();
                    }
                    else if (response.StatusCode == HttpStatusCode.Found 
                          || response.StatusCode == HttpStatusCode.Redirect) 
                    {
                        // we got a redirect.
                        Tracing.TraceMsg("Got a redirect. " + response.ResponseUri); 
                        // only reset the base, the auth cookie is still valid
                        // and cookies are stored in the factory
                        base.Reset(); 
                        CopyRequestData(); 
                        base.Execute();
                    }
                    else 
                        throw re; 
                }
                else 
                {
                    Tracing.TraceMsg("Got no response object");
                    throw re; 
                }
            }
            catch 
            {
                Tracing.TraceMsg("otherstuff");
            }
            if (this.requestCopy != null)
            {
                this.requestCopy.Close(); 
                this.requestCopy = null; 
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Resets the request object</summary> 
        //////////////////////////////////////////////////////////////////////
        protected override void Reset()
        {
            base.Reset();
            this.factory.GAuthToken = null; 
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>takes our copy of the stream, and puts it into the request stream</summary> 
        //////////////////////////////////////////////////////////////////////
        protected void CopyRequestData()
        {
            if (this.requestCopy != null)
            {
                // stream it into the real request stream
                Stream   req = base.GetRequestStream(); 

                const int size = 4096;
                byte[] bytes = new byte[4096];
                int numBytes;

                this.requestCopy.Seek(0, SeekOrigin.Begin); 

                while((numBytes = this.requestCopy.Read(bytes, 0, size)) > 0)
                {
                    req.Write(bytes, 0, numBytes);
                }
                req.Close();
            }
        }
        /////////////////////////////////////////////////////////////////////////////

    }
    /////////////////////////////////////////////////////////////////////////////
} 
/////////////////////////////////////////////////////////////////////////////
