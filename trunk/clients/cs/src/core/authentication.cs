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
using System.Collections.Generic;
using System.Security.Cryptography;


namespace Google.GData.Client
{   

    /// <summary>
    /// Base authentication class. Takes credentials and applicationname
    /// and is able to create a HttpWebRequest augmented with the right
    /// authentication
    /// </summary>
    /// <returns></returns>
    public abstract class Authenticator
    {
        private string applicationName;
            
        /// <summary>
        /// an unauthenticated use case
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public Authenticator(string applicationName)
        {
            this.applicationName = applicationName;
        }

        /// <summary>
        /// Creates a HttpWebRequest object that can be used against a given service. 
        /// for a RequestSetting object that is using client login, this might call 
        /// to get an authentication token from the service, if it is not already set.
        /// 
        /// if this uses client login, and you need to use a proxy, set the application wide
        /// proxy first using the GlobalProxySelection
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="httpMethod"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public HttpWebRequest CreateHttpWebRequest(string httpMethod, Uri targetUri)
        {
            HttpWebRequest request = WebRequest.Create(targetUri) as HttpWebRequest;
            request.Method = httpMethod;
            ApplyAuthenticationToRequest(request);
            return request;
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
        /// Takes an existing httpwebrequest and modifies it's headers according to 
        /// the authentication system used.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract void ApplyAuthenticationToRequest(HttpWebRequest request);
    }


    public class ClientLoginAuthenticator : Authenticator
    {
        private GDataCredentials credentials; 
        private Uri loginHandler;
        private string serviceName;
     
        /// <summary>
        ///  a constructor for client login use cases
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public ClientLoginAuthenticator(string applicationName, string serviceName, string userName, string passWord) : 
            this(applicationName, serviceName, new GDataCredentials(userName, passWord))
        {
        }


        /// <summary>
        ///  a constructor for client login use cases
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="credentials">the user credentials</param>
        /// <returns></returns>
        public ClientLoginAuthenticator(string applicationName, string serviceName, 
                                        GDataCredentials credentials) 
                : this(applicationName, serviceName, credentials, null)
        {
        }

         /// <summary>
        ///  a constructor for client login use cases
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="credentials">the user credentials</param>
        /// <returns></returns>
        public ClientLoginAuthenticator(string applicationName, 
                                        string serviceName,
                                        GDataCredentials credentials, 
                                        Uri clientLoginHandler) 
                : base(applicationName)
        {
             this.credentials = credentials;
             this.serviceName = serviceName;
             this.loginHandler = clientLoginHandler == null ?
                    new Uri(GoogleAuthentication.UriHandler) : clientLoginHandler;
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
        /// returns the service this authenticator is working against
        /// </summary>
        /// <returns></returns>
        public string Service
        {
            get
            {
                return this.serviceName;
            }
        }

        /// <summary>
        /// returns the loginhandler that is used to aquire the token from
        /// </summary>
        /// <returns></returns>
        public Uri LoginHandler
        {
            get
            {
                return this.loginHandler;
            }
        }

        /// <summary>
        /// Takes an existing httpwebrequest and modifies it's headers according to
        /// the authentication system used.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override void ApplyAuthenticationToRequest(HttpWebRequest request)
        {
            EnsureClientLoginCredentials(request);
            if (!String.IsNullOrEmpty(this.Credentials.ClientToken))
            {
                string strHeader = GoogleAuthentication.Header + this.Credentials.ClientToken;
                request.Headers.Add(strHeader);
            }
        }


        private void EnsureClientLoginCredentials(HttpWebRequest request)
        {          
            if (String.IsNullOrEmpty(this.Credentials.ClientToken))
            {
                this.Credentials.ClientToken = Utilities.QueryClientLoginToken(this.Credentials,
                                        this.Service,
                                        this.Application,
                                        false,
                                        this.LoginHandler);
            }
            
        } 
    }

    public class AuthSubAuthenticator : Authenticator
    {
        private string authSubToken; 
        private AsymmetricAlgorithm privateKey;
  
         /// <summary>
        /// a constructor for a web application authentication scenario
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="authSubToken"></param>
        /// <returns></returns>
        public AuthSubAuthenticator(string applicationName, string authSubToken) 
                : this(applicationName, authSubToken, null)
        {
        }

        /// <summary>
        /// a constructor for a web application authentication scenario
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="authSubToken"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public AuthSubAuthenticator(string applicationName, 
                               string authSubToken, 
                               AsymmetricAlgorithm privateKey) : base(applicationName)
        {
            this.privateKey = privateKey;
            this.authSubToken = authSubToken; 
        }

        /// <summary>
        /// returns the authsub token to use for a webapplication scenario
        /// </summary>
        /// <returns></returns>
        public string Token
        {
            get
            {
                return this.authSubToken;
            }
        }

        /// <summary>
        /// returns the private key used for authsub authentication
        /// </summary>
        /// <returns></returns>
        public AsymmetricAlgorithm PrivateKey
        {
            get
            {
                return this.privateKey;
            }
        }


        /// <summary>
        /// Takes an existing httpwebrequest and modifies it's headers according to
        /// the authentication system used.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override void ApplyAuthenticationToRequest(HttpWebRequest request)
        {
            string header = AuthSubUtil.formAuthorizationHeader(this.Token, 
                                                               this.PrivateKey, 
                                                               request.RequestUri,
                                                               request.Method);
            request.Headers.Add(header);
        }
    }

    abstract class OAuthAuthenticator : Authenticator
    {
        private string consumerKey;
        private string consumerSecret;
        
        public OAuthAuthenticator(string applicationName,
                                  string consumerKey,
                                  string consumerSecret) : base(applicationName)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
        }

         /// <summary>
        /// returns the ConsumerKey
        /// </summary>
        /// <returns></returns>
        public string ConsumerKey
        {
            get
            {
                return this.consumerKey;
            }
        }

        /// <summary>
        /// returns the ConsumerSecret
        /// </summary>
        /// <returns></returns>
        public string ConsumerSecret
        {
            get
            {
                return this.consumerSecret;
            }
        }

        
                
    }

    class OAuth2LeggedAuthenticator : OAuthAuthenticator
    {
        private string oAuthUser;
        private string oAuthDomain;

        /// <summary>
        ///  a constructor for OpenAuthentication login use cases
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="consumerKey">the consumerKey to use</param>
        /// <param name="consumerSecret">the consumerSecret to use</param>
        /// <param name="user">the username to use</param>
        /// <param name="domain">the domain to use</param>
        /// <returns></returns>
        public OAuth2LeggedAuthenticator(string applicationName, string consumerKey, 
                               string consumerSecret, 
                               string user, 
                               string domain) : base(applicationName, consumerKey, consumerSecret)
        {
            this.oAuthUser = user;
            this.oAuthDomain = domain;
        }

        /// <summary>
        /// returns the OAuth User
        /// </summary>
        /// <returns></returns>
        public string OAuthUser
        {
            get
            {
                return this.oAuthUser;
            }
        }

        /// <summary>
        /// returns the OAuth Domain
        /// </summary>
        /// <returns></returns>
        public string OAuthDomain
        {
            get
            {
                return this.oAuthDomain;
            }
        }


        /// <summary>
        /// Takes an existing httpwebrequest and modifies it's headers according to
        /// the authentication system used.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override void ApplyAuthenticationToRequest(HttpWebRequest request)
        {
            string oauthHeader = OAuthUtil.GenerateHeader(request.RequestUri, 
                                                            this.ConsumerKey, 
                                                            this.ConsumerSecret, 
                                                            null, 
                                                            null,
                                                            request.Method);
            request.Headers.Add(oauthHeader);
        }


        /// <summary>
        /// Creates a HttpWebRequest object that can be used against a given service. 
        /// for a RequestSetting object that is using client login, this might call 
        /// to get an authentication token from the service, if it is not already set.
        /// 
        /// if this uses client login, and you need to use a proxy, set the application wide
        /// proxy first using the GlobalProxySelection
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="httpMethod"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public HttpWebRequest CreateHttpWebRequest(string httpMethod, Uri targetUri)
        {
            // TODO: need to add the user/domain to the uri, need to 
            // use a uri builder to do so.
            return base.CreateHttpWebRequest(httpMethod, targetUri);
        }
    }

    class OAuth3LeggedAuthenticator : OAuthAuthenticator
    {
        private string token;
        private string tokenSecret; 

        /// <summary>
        ///  a constructor for OpenAuthentication login use cases using 3 legged oAuth
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="consumerKey">the consumerKey to use</param>
        /// <param name="consumerSecret">the consumerSecret to use</param>
        /// <param name="token">The token to be used</param>
        /// <param name="tokenSecret">The tokenSecret to be used</param>
        /// <returns></returns>
        public OAuth3LeggedAuthenticator(string applicationName, 
                               string consumerKey, string consumerSecret, 
                               string token, string tokenSecret)
                    : base(applicationName, consumerKey, consumerSecret)
        {
            this.token = token;
            this.tokenSecret = tokenSecret;
        }

        /// <summary>
        /// returns the Token for oAuth
        /// </summary>
        /// <returns></returns>
        public string Token
        {
            get
            {
                return this.token;
            }
        }

        /// <summary>
        /// returns the TokenSecret for oAuth
        /// </summary>
        /// <returns></returns>
        public string TokenSecret
        {
            get
            {
                return this.tokenSecret;
            }
        }

        /// <summary>
        /// Takes an existing httpwebrequest and modifies it's headers according to
        /// the authentication system used.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override void ApplyAuthenticationToRequest(HttpWebRequest request)
        {
            string oauthHeader = OAuthUtil.GenerateHeader(request.RequestUri, 
                                                            this.ConsumerKey, 
                                                            this.ConsumerSecret, 
                                                            this.Token, 
                                                            this.TokenSecret,
                                                            request.Method);
            request.Headers.Add(oauthHeader);
        }
    }
}






        
        
        


    


       

        

       
        



