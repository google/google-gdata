/* Copyright (c) 2010 Google Inc.
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
 * 
 * Ported from Java client: http://code.google.com/p/gdata-java-client/
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.GData.Client
{
    public class OAuthHelper
    {
        private String requestTokenUrl;
        private String userAuthorizationUrl;
        private String accessTokenUrl;
        private String revokeTokenUrl;

        private OAuthHttpClient httpClient;
        private IOAuthSigner signer;

        /// <param name="requestTokenUrl">
        /// the url used to obtain an unauthorized request token
        /// </param>
        /// <param name="userAuthorizationUrl">
        /// the url used to obtain user authorization for consumer access
        /// </param>
        /// <param name="accessTokenUrl">
        /// the url used to exchange the user-authorized request token for an access token
        /// </param>
        /// <param name="revokeTokenUrl">
        /// the url used to revoke the OAuth token
        /// </param>
        /// <param name="signer">
        /// the <see cref="IOAuthSigner"/> to use when signing the request
        /// </param>
        public OAuthHelper(string requestTokenUrl, string userAuthorizationUrl,
                string accessTokenUrl, string revokeTokenUrl, IOAuthSigner signer) 
            : this(requestTokenUrl, userAuthorizationUrl, accessTokenUrl, revokeTokenUrl,
                signer, new OAuthHttpClient())
        {
        }

        /// <summary>
        /// This version of the constructor
        /// is primarily for testing purposes, where a mocked <see cref="OAuthHttpClient"/>
        /// and <see cref="IOAuthSigner"/> can be specified.
        /// </summary>
        /// <param name="requestTokenUrl">the url used to obtain an unauthorized request token</param>
        /// <param name="userAuthorizationUrl">
        /// the url used to obtain user authorization for consumer access
        /// </param>
        /// <param name="accessTokenUrl">
        /// the url used to exchange the user-authorized request token for an access token
        /// </param>
        /// <param name="revokeTokenUrl">the url used to revoke the OAuth token</param>
        /// <param name="signer">
        /// the <see cref="IOAuthSigner"/> to use when signing the request
        /// </param>
        /// <param name="httpClient">
        /// the <see cref="OAuthHttpClient"/> to use when making http requests
        /// </param>
        public OAuthHelper(string requestTokenUrl, string userAuthorizationUrl, string accessTokenUrl, 
            string revokeTokenUrl, IOAuthSigner signer, OAuthHttpClient httpClient)
        {
            this.requestTokenUrl = requestTokenUrl;
            this.userAuthorizationUrl = userAuthorizationUrl;
            this.accessTokenUrl = accessTokenUrl;
            this.revokeTokenUrl = revokeTokenUrl;
            this.httpClient = httpClient;
            this.signer = signer;
        }

        /// <summary>
        /// An abstract helper class for generating a string of key/value pairs that
        /// are separated by string delimiters.  For example, suppose there is a set of
        /// key value pairs: key1/value1, key2/value2, etc.  Using the
        /// <see cref="QueryKeyValuePair"/> class, the resulting string would look like:
        /// key1=value1&key2=value2.  There is no trailing ampersand at the end and
        /// each key and value will be encoded according to the OAuth spec
        /// (<a href="http://oauth.net/core/1.0/#encoding_parameters">Section 5.1</a>).
        /// </summary>
        abstract class KeyValuePair 
        {
            private IList<string> keys;
            private IList<string> values;
            private string keyValueStartDelimiter;
            private string keyValueEndDelimiter;
            private string pairDelimiter;

            /// <summary>
            /// Create a new instance.  The string delimiters specified as inputs are
            /// used by the {@link #toString} method to generating the string.
            /// </summary>
            /// <param name="keyValueStartDelimiter">
            /// the delimiter placed between the key and the value
            /// </param>
            /// <param name="keyValueEndDelimiter">
            /// the delimiter placed after the value
            /// </param>
            /// <param name="pairDelimiter">
            /// the delimiter placed in between each key/value pair
            /// </param>
            protected KeyValuePair(string keyValueStartDelimiter,
                string keyValueEndDelimiter, string pairDelimiter) 
            {
                this.keyValueStartDelimiter = keyValueStartDelimiter;
                this.keyValueEndDelimiter = keyValueEndDelimiter;
                this.pairDelimiter = pairDelimiter;
                keys = new List<string>();
                values = new List<string>();
            }


            /// <summary>Add a key/value pair</summary>
            /// <param name="key">the key of the pair</param>
            /// <param name="value">the value of the pair</param>
            public void Add(string key, string value) 
            {
                keys.Add(key);
                values.Add(value);
            }

            /// <summary>
            /// Get the key at the input position.
            /// </summary>
            /// <param name="i">the position to retrieve the key</param>
            /// <returns>the key at the input position</returns>
            public string GetKey(int i) 
            {
                return keys[i];
            }

            /// <summary>
            /// Get the value at the input position.
            /// </summary>
            /// <param name="i">the position to retrieve the value</param>
            /// <returns>the value at the input position</returns>
            public string GetValue(int i) 
            {
                return values[i];
            }

            /// <summary>
            /// The number of key/value pairs
            /// </summary>
            public int Count
            {
                get
                {
                    return keys.Count;
                }
            }

            /// <summary>
            /// Concatenates the key/value pairs into a string.  For example, suppose
            /// there is a set of key value pairs: key1/value1, key2/value2, etc.  Using
            /// the <see cref="QueryKeyValuePair"/> class, the resulting string would look
            /// like: key1=value1&key2=value2.  There is no trailing ampersand at the end
            /// and each key and value will be encoded according to the OAuth spec
            /// (<a href="http://oauth.net/core/1.0/#encoding_parameters">Section
            /// 5.1</a>).
            /// </summary>
            public override string ToString() 
            {
                StringBuilder keyValueString = new StringBuilder();
                for (int i = 0, length = Count; i < length; i++) 
                {
                    if (i > 0) 
                    {
                        keyValueString.Append(pairDelimiter);
                    }
                    keyValueString.Append(OAuthUtil.Instance.UrlEncode(GetKey(i)))
                        .Append(keyValueStartDelimiter)
                        .Append(OAuthUtil.Instance.UrlEncode(GetValue(i)))
                        .Append(keyValueEndDelimiter);
                }
                return keyValueString.ToString();
            }
        }

        /// <summary>
        /// Generates a key/value string appropriate for a url's query string.  For
        /// example: key1=value1&key2=value2&key3=value3.
        /// </summary>
        private class QueryKeyValuePair : KeyValuePair
        {
            public QueryKeyValuePair() : base("=", "", "&")
            {
            }
        }

        public void GetUnauthorizedRequestToken(OAuthParameters oauthParameters)
        {
            TwoLeggedOAuthHelper helper
                = new TwoLeggedOAuthHelper(signer, oauthParameters);
            helper.ValidateInputParameters();

            // If the callback is present in this step, assume the user is using
            // OAuth v1.0a, and include the url in the base parameters.
            bool oauthCallbackExists = false;
            if (oauthParameters.CheckOAuthCallbackExists()) 
            {
                string callback = oauthParameters.OAuthCallback;
                oauthParameters.AddCustomBaseParameter(OAuthParameters.OAUTH_CALLBACK_KEY, 
                    callback);
                oauthCallbackExists = true;
            }

            // Generate a signed URL that allows the consumer to retrieve the
            // unauthorized request token.
            Uri uri = GetOAuthUrl(requestTokenUrl, "GET", oauthParameters);

            // Retrieve the unauthorized request token and store it in the
            // oauthParameters
            string response = httpClient.GetResponse(uri);
            IDictionary<string, string> queryString = OAuthUtil.Instance.ParseQuerystring(response);
            if (queryString.ContainsKey(OAuthParameters.OAUTH_TOKEN_KEY))
            {
                oauthParameters.OAuthToken = queryString[OAuthParameters.OAUTH_TOKEN_KEY];
            }
            if (queryString.ContainsKey(OAuthParameters.OAUTH_TOKEN_SECRET_KEY))
            {
                oauthParameters.OAuthTokenSecret = queryString[OAuthParameters.OAUTH_TOKEN_SECRET_KEY];
            }

            if (oauthCallbackExists) 
            {
                // OAuth callback can be completely removed from parameters here,
                // but leave it in for now in order to be compatible with both the
                // old and new OAuth protocol.
                oauthParameters.RemoveCustomBaseParameter(OAuthParameters.OAUTH_CALLBACK_KEY);
            }

            // clear the request-specific parameters set in getOAuthUrl(), such as
            // nonce, timestamp and signature, which are only needed for a single
            // request.
            oauthParameters.Reset();
        }

        /// <summary>
        /// Generates the url which the user should visit in order to authenticate and
        /// authorize with the Service Provider. This method does not modify the
        /// <see cref="OAuthParameters"/> object.  The url will look something like this:
        /// https://www.google.com/accounts/OAuthAuthorizeToken?oauth_token=[OAUTHTOKENSTRING]&oauth_callback=http%3A%2F%2Fwww.google.com%2F
        /// <p>
        /// The following parameter is required in <see cref="OAuthParameters"/>
        /// <ul>
        /// <li>oauth_token
        /// </ul>
        /// <p>
        /// The following parameter is optional:
        /// <ul>
        /// <li>oauth_callback
        /// </ul>
        /// </summary>
        /// <param name="oauthParameters">the OAuth parameters necessary for this request</param>
        /// <returns>
        /// The full authorization url the user should visit.  The method also modifies the 
        /// oauthParameters object by adding the request token and token secret.
        /// </returns>
        public string CreateUserAuthorizationUrl(OAuthParameters oauthParameters)
        {
            // Format and return the user authorization url.
            KeyValuePair queryParams = new QueryKeyValuePair();
            queryParams.Add(OAuthParameters.OAUTH_TOKEN_KEY, oauthParameters.OAuthToken);
            if (oauthParameters.OAuthCallback.Length > 0)
            {
                queryParams.Add(OAuthParameters.OAUTH_CALLBACK_KEY, oauthParameters.OAuthCallback);
            }

            return userAuthorizationUrl + "?" + queryParams;
        }

        /// <summary>
        /// Exchanges the user-authorized request token for an access token.
        /// Typically, this method is called immediately after you extract the
        /// user-authorized request token from the authorization response, but it can
        /// also be triggered by a user action indicating they've successfully
        /// completed authorization with the service provider.
        /// <p>
        /// The following parameters are required in <see cref="OAuthParameters"/>:
        /// <ul>
        /// <li>oauth_consumer_key
        /// <li>oauth_token
        /// <li>oauth_token_secret (if signing with HMAC)
        /// </ul>
        /// <p>
        /// If the request is successful, the following parameters will be set in
        /// <see cref="OAuthParameters"/>:
        /// <ul>
        /// <li>oauth_token
        /// <li>oauth_token_secret (if signing with HMAC)
        /// </ul>
        /// <p>
        /// See <a href="http://oauth.net/core/1.0/#auth_step3">OAuth Step 3</a>.
        /// </summary>
        /// <param name="oauthParameters">
        /// OAuth parameters for this request
        /// </param>
        /// <returns>
        /// The access token.  This method also replaces the request token
        /// with the access token in the oauthParameters object.
        /// </returns>
        /// <exception cref="OAuthException">
        /// if there is an error with the OAuth request.
        /// </exception>
        public String GetAccessToken(OAuthParameters oauthParameters)
        {
            // STEP 1: Validate the input parameters
            TwoLeggedOAuthHelper helper
                = new TwoLeggedOAuthHelper(signer, oauthParameters);
            helper.ValidateInputParameters();
            oauthParameters.AssertOAuthTokenExists();
            if (signer is OAuthHmacSha1Signer) {
                oauthParameters.AssertOAuthTokenSecretExists();
            }

            // STEP 2: Generate the OAuth request url based on the input parameters.
            Uri url = GetOAuthUrl(accessTokenUrl, "GET", oauthParameters);

            // STEP 3: Make a request for the access token, and store it in
            // oauthParameters
            string response = httpClient.GetResponse(url);
            IDictionary<string, string> queryString = OAuthUtil.Instance.ParseQuerystring(response);
            if (queryString.ContainsKey(OAuthParameters.OAUTH_TOKEN_KEY))
            {
                oauthParameters.OAuthToken = queryString[OAuthParameters.OAUTH_TOKEN_KEY];
            }
            if (queryString.ContainsKey(OAuthParameters.OAUTH_TOKEN_SECRET_KEY))
            {
                oauthParameters.OAuthTokenSecret = queryString[OAuthParameters.OAUTH_TOKEN_SECRET_KEY];
            }

            // clear the request-specific parameters set in getOAuthUrl(), such as
            // nonce, timestamp and signature, which are only needed for a single
            // request.
            oauthParameters.Reset();

            return oauthParameters.OAuthToken;
        }

        /// <summary>
        /// Returns a properly formatted and signed OAuth request url, with the
        /// appropriate parameters.
        /// </summary>
        /// <param name="baseUrl">the url to make the request to</param>
        /// <param name="httpMethod">the http method of this request (for example, "GET")</param>
        /// <param name="oauthParameters">OAuth parameters for this request</param>
        /// <returns>the OAuth request url</returns>
        /// <exception cref="OAuthException"> if there is an error with the OAuth request</exception>
        public Uri GetOAuthUrl(string baseUrl, string httpMethod, OAuthParameters oauthParameters) 
        {
            TwoLeggedOAuthHelper helper
                = new TwoLeggedOAuthHelper(signer, oauthParameters);

            // add request-specific parameters
            helper.AddCommonRequestParameters(baseUrl, httpMethod);

            // add all query string information
            KeyValuePair queryParams = new QueryKeyValuePair();
            foreach (var e in oauthParameters.BaseParameters)
            {
                if (e.Value.Length > 0)
                {
                    queryParams.Add(e.Key, e.Value);
                }
            }
            queryParams.Add(OAuthParameters.OAUTH_SIGNATURE_KEY,
                            oauthParameters.OAuthSignature);

            // build the url string
            StringBuilder fullUrl = new StringBuilder(baseUrl);
            fullUrl.Append(baseUrl.IndexOf("?") > 0 ? "&" : "?");
            fullUrl.Append(queryParams.ToString());

            try 
            {
                return new Uri(fullUrl.ToString());
            } 
            catch (UriFormatException ufe) 
            {
                throw new OAuthException(ufe);
            }
        }
    }
}
