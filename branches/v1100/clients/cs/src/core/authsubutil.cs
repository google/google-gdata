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
using System.Security.Cryptography;
using System.Net;
using System.Text;




#endregion

//////////////////////////////////////////////////////////////////////
// Contains AuthSubUtil, a helper class for authsub communications
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>helper class for communications between a 3rd party site and Google using the AuthSub protocol
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class AuthSubUtil
    {
        private static string DEFAULT_PROTOCOL = "https"; 
        private static string DEFAULT_DOMAIN = "www.google.com";
        private static string DEFAULT_HANDLER = "/accounts/AuthSubRequest";

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the request URL to be used to retrieve an AuthSub 
        /// token. On success, the user will be redirected to the continue URL 
        /// with the AuthSub token appended to the URL.
        /// Use getTokenFromReply(String) to retrieve the token from the reply.
        /// </summary> 
        /// <param name="continueUrl">the URL to redirect to on successful 
        /// token retrieval</param>
        /// <param name="scope">the scope of the requested AuthSub token</param>
        /// <param name="secure">if the token will be used securely</param>
        /// <param name="session"> if the token will be exchanged for a
        ///  session cookie</param>
        /// <returns>the URL to be used to retrieve the AuthSub token</returns>
        //////////////////////////////////////////////////////////////////////
        public static string getRequestUrl(string continueUrl,
                                           string scope,
                                           bool secure,
                                           bool session)
        {

            return getRequestUrl(DEFAULT_PROTOCOL, DEFAULT_DOMAIN, continueUrl, scope,
                                 secure, session);
        }




        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the request URL to be used to retrieve an AuthSub 
        /// token. On success, the user will be redirected to the continue URL 
        /// with the AuthSub token appended to the URL.
        /// Use getTokenFromReply(String) to retrieve the token from the reply.
        /// </summary> 
        /// <param name="protocol">the protocol to use to communicate with the 
        /// server</param>
        /// <param name="domain">the domain at which the authentication server 
        /// exists</param>
        /// <param name="continueUrl">the URL to redirect to on successful 
        /// token retrieval</param>
        /// <param name="scope">the scope of the requested AuthSub token</param>
        /// <param name="secure">if the token will be used securely</param>
        /// <param name="session"> if the token will be exchanged for a
        ///  session cookie</param>
        /// <returns>the URL to be used to retrieve the AuthSub token</returns>
        //////////////////////////////////////////////////////////////////////
        public static string getRequestUrl(string protocol,
                                           string domain,
                                           string continueUrl,
                                           string scope,
                                           bool secure,
                                           bool session)
        {
            return getRequestUrl(protocol, domain, DEFAULT_HANDLER, continueUrl, 
                scope, secure, session);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the request URL to be used to retrieve an AuthSub 
        /// token. On success, the user will be redirected to the continue URL 
        /// with the AuthSub token appended to the URL.
        /// Use getTokenFromReply(String) to retrieve the token from the reply.
        /// </summary> 
        /// <param name="protocol">the protocol to use to communicate with the 
        /// server</param>
        /// <param name="domain">the domain at which the authentication server 
        /// exists</param>
        /// <param name="handler">the location of the authentication handler
        ///  (defaults to "/accounts/AuthSubRequest".</param>
        /// <param name="continueUrl">the URL to redirect to on successful 
        /// token retrieval</param>
        /// <param name="scope">the scope of the requested AuthSub token</param>
        /// <param name="secure">if the token will be used securely</param>
        /// <param name="session"> if the token will be exchanged for a
        ///  session cookie</param>
        /// <returns>the URL to be used to retrieve the AuthSub token</returns>
        //////////////////////////////////////////////////////////////////////
        public static string getRequestUrl(string protocol,
                                           string domain,
                                           string handler,
                                           string continueUrl,
                                           string scope,
                                           bool secure,
                                           bool session)
        {

            StringBuilder url = new StringBuilder(protocol);
            url.Append("://");
            url.Append(domain);
            url.Append(handler);
            url.Append("?");
            addParameter(url, "next", continueUrl);
            url.Append("&");
            addParameter(url, "scope", scope);
            url.Append("&");
            addParameter(url, "secure", secure ? "1" : "0");
            url.Append("&");
            addParameter(url, "session", session ? "1" : "0");
            return url.ToString();
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary> 
        /// Adds the query parameter with the given name and value to the URL.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        private static void addParameter(StringBuilder url,
                                         string name,
                                         string value)
        {
            // encode them
            name = Utilities.UriEncodeReserved(name);
            value = Utilities.UriEncodeReserved(value);
            // Append the name/value pair
            url.Append(name);
            url.Append('='); 
            url.Append(value);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the URL to use to exchange the one-time-use token for
        ///  a session token.
        /// </summary> 
        /// <returns>the URL to exchange for the session token</returns>
        //////////////////////////////////////////////////////////////////////
        public static string getSessionTokenUrl()
        {
            return getSessionTokenUrl(DEFAULT_PROTOCOL, DEFAULT_DOMAIN);       
        }
        //end of public static string getSessionTokenUrl()

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the URL to use to exchange the one-time-use token for
        ///  a session token.
        /// </summary> 
        /// <param name="protocol">the protocol to use to communicate with
        /// the server</param>
        /// <param name="domain">the domain at which the authentication server 
        /// exists</param>
        /// <returns>the URL to exchange for the session token</returns>
        //////////////////////////////////////////////////////////////////////
        public static string getSessionTokenUrl(string protocol, string domain)
        {
            return protocol + "://" + domain + "/accounts/AuthSubSessionToken";  
        }
        //end of public static string getSessionTokenUrl()




        //////////////////////////////////////////////////////////////////////
        /// <summary>
        ///  Parses and returns the AuthSub token returned by Google on a successful
        ///  AuthSub login request.  The token will be appended as a query parameter
        /// to the continue URL specified while making the AuthSub request.
        /// </summary> 
        /// <param name="uri">The reply URI to parse </param>
        /// <returns>the token value of the URI, or null if none </returns>
        //////////////////////////////////////////////////////////////////////
        public static string getTokenFromReply(Uri uri)
        {
            char [] deli = {'?','&'}; 
            TokenCollection tokens = new TokenCollection(uri.Query, deli); 
            foreach (String token in tokens )
            {
                if (token.Length > 0)
                {
                    char [] otherDeli = {'='};
                    String [] parameters = token.Split(otherDeli,2); 
                    if (parameters[0] == "token")
                    {
                        return parameters[1]; 
                    }
                }
            }
            return null; 
        }
        //end of public static string getTokenFromReply(URL url)

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Exchanges the one time use token returned in the URL for a session
        /// token. If the key is non-null, the token will be used securely, 
        /// and the request will be signed
        /// </summary> 
        /// <param name="onetimeUseToken">the token send by google in the URL</param>
        /// <param name="key">the private key used to sign</param>
        /// <returns>the session token</returns>
        //////////////////////////////////////////////////////////////////////
        public static String exchangeForSessionToken(String onetimeUseToken, 
                                                     AsymmetricAlgorithm key)
        {
            return exchangeForSessionToken(DEFAULT_PROTOCOL, DEFAULT_DOMAIN,
                                           onetimeUseToken, key);    
        }
        //end of public static String exchangeForSessionToken(String onetimeUseToken, PrivateKey key)




        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Exchanges the one time use token returned in the URL for a session
        /// token. If the key is non-null, the token will be used securely, 
        /// and the request will be signed
        /// </summary> 
        /// <param name="protocol">the protocol to use to communicate with the 
        /// server</param>
        /// <param name="domain">the domain at which the authentication server 
        /// exists</param>
        /// <param name="onetimeUseToken">the token send by google in the URL</param>
        /// <param name="key">the private key used to sign</param>
        /// <returns>the session token</returns>
        //////////////////////////////////////////////////////////////////////
        public static string exchangeForSessionToken(string protocol,
                                                     string domain,
                                                     string onetimeUseToken, 
                                                     AsymmetricAlgorithm key)
        {
            HttpWebResponse response = null;
            string authSubToken = null; 

            try
            {
                string sessionUrl = getSessionTokenUrl(protocol, domain);
                Uri uri = new Uri(sessionUrl);

                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;


                string header = formAuthorizationHeader(onetimeUseToken, key, uri, "GET");
                request.Headers.Add(header); 

                response = request.GetResponse() as HttpWebResponse; 

            }
            catch (WebException e)
            {
                Tracing.TraceMsg("exchangeForSessionToken failed " + e.Status); 
                throw new GDataRequestException("Execution of exchangeForSessionToken", e);
            }
            if (response != null)
            {
                int code= (int)response.StatusCode;
                if (code != 200)
                {
                    throw new GDataRequestException("Execution of exchangeForSessionToken request returned unexpected result: " +code,  response); 
                }
                // verify the content length. This should not be big, hence a big result might indicate a phoney
                if (response.ContentLength > 1024)
                {
                    throw new GDataRequestException("Execution of exchangeForSessionToken request returned unexpected large content length: " + response.ContentLength,  response); 
                }
                // get the body and parse it

                authSubToken = Utilities.ParseValueFormStream(response.GetResponseStream(), GoogleAuthentication.AuthSubToken); 
            }

            Tracing.Assert(authSubToken != null, "did not find an auth token in exchangeForSessionToken"); 

            return authSubToken; 

        }
        //end of public static String exchangeForSessionToken(String onetimeUseToken, PrivateKey key)


        //////////////////////////////////////////////////////////////////////
        /// <summary>Forms the AuthSub authorization header.
        /// if key is null, the token will be in insecure mode, otherwise 
        /// the token will be used securely and the header contains
        /// a signature
        /// </summary> 
        /// <param name="token">the AuthSub token to use </param>
        /// <param name="key">the private key to used </param>
        /// <param name="requestUri">the request uri to use </param>
        /// <param name="requestMethod">the HTTP method to use </param>
        /// <returns>the authorization header </returns>
        //////////////////////////////////////////////////////////////////////
        public static string formAuthorizationHeader(string token, 
                                                     AsymmetricAlgorithm key, 
                                                     Uri requestUri, 
                                                     string requestMethod)
        {
            if (key == null)
            {
                return String.Format("Authorization: AuthSub token=\"{0}\"", token);
            }
            else
            {
                // Form signature for secure mode
                TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                int timestamp = (int)t.TotalSeconds;

                string nounce = generateULONGRnd(); 

                string dataToSign = String.Format("{0} {1} {2} {3}", 
                                                  requestMethod, 
                                                  requestUri.AbsoluteUri,
                                                  timestamp.ToString(), 
                                                  nounce);


                byte[] signature = sign(dataToSign, key);

                string encodedSignature = Convert.ToBase64String(signature);

                string algorithmName = key is DSACryptoServiceProvider ? "dsa-sha1" : "rsa-sha1";

                return String.Format("Authorization: AuthSub token=\"{0}\" data=\"{1}\" sig=\"{2}\" sigalg=\"{3}\"",
                                     token, dataToSign, encodedSignature,  algorithmName);
            }

        }
        //end of public static string formAuthorizationHeader(string token, p key, Uri requestUri, string requestMethod)


        //////////////////////////////////////////////////////////////////////
        /// <summary>creates a max 20 character long string of random numbers</summary>
        /// <returns> the string containing random numbers</returns>
        //////////////////////////////////////////////////////////////////////
        private static string generateULONGRnd()
        {
            byte[] randomNumber = new byte[20];

            // Create a new instance of the RNGCryptoServiceProvider. 
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();

            // Fill the array with a random value.
            Gen.GetBytes(randomNumber);

            StringBuilder x = new StringBuilder(20);
            for (int i = 0; i < 20; i++)
            {
                if (randomNumber[i] == 0 && x.Length == 0)
                {
                    continue; 
                }
                x.Insert(i, Convert.ToInt16(randomNumber[i]).ToString()[0]);
            }
            return x.ToString(); 
        }
        //end of private static string generateULONGRnd()



        //////////////////////////////////////////////////////////////////////
        /// <summary>signs the data with the given key</summary>
        /// <param name="dataToSign">the data to sign </param>
        /// <param name="key">the private key to used </param>
        /// <returns> the signed data</returns>
        //////////////////////////////////////////////////////////////////////
        private static byte[]  sign(string dataToSign, AsymmetricAlgorithm key) 
        {
            byte[] data = new ASCIIEncoding().GetBytes(dataToSign);

            try
            {
                RSACryptoServiceProvider providerRSA = key as RSACryptoServiceProvider; 
                if (providerRSA != null)
                {
                    return providerRSA.SignData(data, new SHA1CryptoServiceProvider());
                }
                DSACryptoServiceProvider providerDSA = key as DSACryptoServiceProvider;
                if (providerDSA != null)
                {
                    return providerDSA.SignData(data); 
                }
            }
            catch (CryptographicException e)
            {
                Tracing.TraceMsg(e.Message);
            }
            return null; 
        }
    }
    //end of public class AuthSubUtil
} 
/////////////////////////////////////////////////////////////////////////////
