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

using System.Collections.Generic;

namespace Google.GData.Client
{
    public class OAuthParameters
    {
        /// <summary>
        /// Type of OAuth for this parameter set (i.e., two-legged or three-legged
        /// OAuth (see {@link "https://sites.google.com/a/google.com/oauth/"}). 
        /// </summary>
        public enum OAuthType 
        {
            TwoLeggedOAuth,
            ThreeLeggedOAuth
        }

        public static readonly string OAUTH_CALLBACK_KEY = "oauth_callback";
        public static readonly string OAUTH_CONSUMER_KEY = "oauth_consumer_key";
        public static readonly string OAUTH_CONSUMER_SECRET = "oauth_consumer_secret";
        public static readonly string OAUTH_NONCE_KEY = "oauth_nonce";
        public static readonly string OAUTH_KEY = "OAuth";
        public static readonly string OAUTH_SIGNATURE_KEY = "oauth_signature";
        public static readonly string OAUTH_SIGNATURE_METHOD_KEY = "oauth_signature_method";
        public static readonly string OAUTH_TIMESTAMP_KEY = "oauth_timestamp";
        public static readonly string OAUTH_TOKEN_KEY = "oauth_token";
        public static readonly string OAUTH_TOKEN_SECRET_KEY = "oauth_token_secret";
        public static readonly string OAUTH_VERIFIER_KEY = "oauth_verifier";
        public static readonly string REALM_KEY = "realm";
        public static readonly string XOAUTH_REQUESTOR_ID_KEY = "xoauth_requestor_id";
        
        protected IDictionary<string, string> baseParameters;
        protected IDictionary<string, string> extraParameters;

        public OAuthParameters()
        {
            baseParameters = new Dictionary<string, string>();
            extraParameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// A map of key/value pairs to use in the signature base string.
        /// </summary>
        public IDictionary<string, string> BaseParameters 
        {
            get 
            { 
                // Copy the dictionary so modifications by the caller don't affect this
                return new Dictionary<string, string>(baseParameters);
            }
        }

        /// <summary>
        /// Adds a parameter to be used when generating the OAuth signature.
        /// </summary>
        /// <param name="key">
        /// The key used to reference this parameter.  This key will also be
        /// used to reference the value in the request url and in the http
        /// authorization header.
        /// </param>
        /// <param name="value">The value of the parameter.</param>
        public void AddCustomBaseParameter(string key, string value)
        {
            Put(key, value, baseParameters);
        }

        /// <summary>
        /// Removes a parameter from the OAuth signature.
        /// </summary>
        /// <param name="key">The key used to reference this parameter.</param>
        public void RemoveCustomBaseParameter(string key)
        {
            Remove(key, baseParameters);
        }

        /// <summary>
        /// Resets all transient parameters related to a single request, so that these
        /// parameters do not interfere with multiple requests.
        /// </summary>
        public virtual void Reset()
        {
            Remove(OAUTH_NONCE_KEY, baseParameters);
            Remove(OAUTH_TIMESTAMP_KEY, baseParameters);
            Remove(OAUTH_VERIFIER_KEY, baseParameters);
            Remove(OAUTH_SIGNATURE_KEY, extraParameters);
        }

        /// <summary>
        /// The OAuth Consumer Key.  The OAuth Consumer Key is a value used by
        /// the Consumer to identify itself to the Service Provider.  This parameter is
        /// required for all OAuth requests.  This parameter is included in the OAuth
        /// signature base string.
        /// </summary>
        public string OAuthConsumerKey
        {
            get { return Get(OAUTH_CONSUMER_KEY, baseParameters); }
            set { Put(OAUTH_CONSUMER_KEY, value, baseParameters); }
        }

        public string OAuthConsumerSecret
        {
            get { return Get(OAUTH_CONSUMER_SECRET, extraParameters); }
            set { Put(OAUTH_CONSUMER_SECRET, value, extraParameters); }
        }

        /// <summary>
        /// Returns the OAuth callback url.  The OAuth callback url is a url the
        /// Consumer provides to the Service Provider in the user authorization url.
        /// Once the user has authorized, the Service Provider will redirect the user
        /// back to the callback url with the user-authorized request token in the
        /// response.  This parameter is optional.
        /// </summary>
        public string OAuthCallback
        {
            get { return Get(OAUTH_CALLBACK_KEY, extraParameters); }
            set { Put(OAUTH_CALLBACK_KEY, value, extraParameters); }
        }

        /// <summary>
        /// The OAuth nonce.  OAuth defines the nonce as "a random string,
        /// uniquely generated for each request. The nonce allows the Service Provider
        /// to verify that a request has never been made before and helps prevent
        /// replay attacks when requests are made over a non-secure channel (such as
        /// HTTP)."  This parameter is optional, and it will be supplied by
        /// <see cref="OAuthUtil.GenerateNonce"/> if it is not provided.  This parameter is
        /// included in the OAuth signature base string.
        /// </summary>
        public string OAuthNonce
        {
            get { return Get(OAUTH_NONCE_KEY, baseParameters); }
            set { Put(OAUTH_NONCE_KEY, value, baseParameters); }
        }


        /// <summary>
        /// The OAuth signature used to sign the current request.  This 
        /// parameter is optional, and it will be set by <see cref="OAuthHelper"/> 
        /// if it is not provided.
        /// </summary>
        public string OAuthSignature
        {
            get { return Get(OAUTH_SIGNATURE_KEY, extraParameters); }
            set { Put(OAUTH_SIGNATURE_KEY, value, extraParameters); }
        }

        /// <summary>
        /// Returns the OAuth Signature Method.  Valid values are "RSA-SHA1",
        /// "HMAC-SHA1" and "PLAINTEXT".  This parameter is optional, and will be
        /// supplied by <see cref="IOAuthSigner"/> if it is not provided by the user.  
        /// This parameter is included in the OAuth signature base string.
        /// </summary>
        public string OAuthSignatureMethod
        {
            get { return Get(OAUTH_SIGNATURE_METHOD_KEY, baseParameters); }
            set { Put(OAUTH_SIGNATURE_METHOD_KEY, value, baseParameters); }
        }

        /// <summary>
        /// Returns the OAuth timestamp.  OAuth defines the timestamp as "the number of
        /// seconds since January 1, 1970 00:00:00 GMT. The timestamp value MUST be a
        /// positive integer and MUST be equal or greater than the
        /// timestamp used in previous requests."  This parameter is optional, and will
        /// be supplied by <see cref="OAuthUtil.GenerateTimeStamp"/>  if it is not provided by
        /// the user.  This parameter is included in the OAuth signature base string.
        /// </summary>
        public string OAuthTimestamp
        {
            get { return Get(OAUTH_TIMESTAMP_KEY, baseParameters); }
            set { Put(OAUTH_TIMESTAMP_KEY, value, baseParameters); }
        }

        /// <summary>
        /// The OAuth token.  This token may either be the unauthorized
        /// request token, the user-authorized request token, or the access token.
        /// This parameter is optional, and will be modified by the methods in
        /// <see cref="OAuthHelper"/>.  This parameter is included in the OAuth 
        /// signature base string.
        /// </summary>
        public string OAuthToken
        {
            get { return Get(OAUTH_TOKEN_KEY, baseParameters); }
            set { Put(OAUTH_TOKEN_KEY, value, baseParameters); }
        }

        /// <summary>
        /// Checks to see if the OAuth token exists.  Throws an exception if
        /// it does not.  See <see cref="OAuthToken"/> to learn more about this
        /// parameter.
        /// </summary>
        public void AssertOAuthTokenExists()
        {
            AssertExists(OAUTH_TOKEN_KEY, baseParameters);
        }

        /// <summary>
        /// The OAuth Token Secret.  The OAuth Token Secret is a secret used
        /// by the Consumer to establish ownership of a given Token.  This parameter
        /// is optional.
        /// </summary>
        public string OAuthTokenSecret
        {
            get { return Get(OAUTH_TOKEN_SECRET_KEY, extraParameters); }
            set { Put(OAUTH_TOKEN_SECRET_KEY, value, extraParameters); }
        }

        /// <summary>
        /// Checks to see if the OAuth token secret exists.  Throws an exception if
        /// it does not.  See <see cref="OAuthTokenSecret"/> to learn more about this
        /// parameter.
        /// </summary>
        public void AssertOAuthTokenSecretExists()
        {
            AssertExists(OAUTH_TOKEN_SECRET_KEY, extraParameters);
        }
        
        /// <summary>
        /// Checks to see if the OAuth Consumer Key exists.  Throws an exception if
        /// it does not.  See <see cref="OAuthConsumerKey"/>  to learn more about this
        /// parameter.
        /// </summary>
        /// <exception cref="OAuthException">
        /// if the OAuth Consumer Key does not exist.
        /// </exception>
        public void AssertOAuthConsumerKeyExists() 
        {
            AssertExists(OAUTH_CONSUMER_KEY, baseParameters);
        }


        public void AssertOAuthConsumerSecretExists() 
        {
            AssertExists(OAUTH_CONSUMER_SECRET, extraParameters);
        }

        /// <summary>
        /// Checks to see if the OAuth callback exists.  See 
        /// <see cref="OAuthCallback"/> to learn more about this parameter.
        /// </summary>
        /// <returns>True if the OAuth callback exists, false otherwise.</returns>
        public bool CheckOAuthCallbackExists()
        {
            return CheckExists(OAUTH_CALLBACK_KEY, extraParameters);
        }

        /// <param name="key">The key whose value to retrieve.</param>
        /// <param name="parameters">The value associated with the given key.</param>
        /// <returns>
        /// The value with the given key from parameters, if the key is present,
        /// otherwise an empty string.
        /// </returns>
        protected static string Get(string key, IDictionary<string, string> parameters)
        {
            return parameters.ContainsKey(key) ? parameters[key] : string.Empty;
        }

        /// <summary>
        /// Adds the key/value pair to the input map.
        /// </summary>
        /// <param name="key">The key to add to the map.</param>
        /// <param name="value">the value to add to the map</param>
        /// <param name="parameters">The map to add the values to</param>
        protected static void Put(string key, string value, IDictionary<string, string> parameters)
        {
            parameters[key] = value;
        }

        /// <summary>
        /// Removes a key/value pair from the input map.
        /// </summary>
        /// <param name="key">the key to remove</param>
        /// <param name="parameters">the map to remove the key from</param>
        protected static void Remove(string key, IDictionary<string, string> parameters) 
        {
            parameters.Remove(key);
        }

        /// <summary>
        /// Checks the given key to see if it exists.  In order to "exist", the value
        /// can't be null, and it can't be an empty string.
        /// </summary>
        /// <param name="key">The key to check for existence.</param>
        /// <param name="parameters">The parameter map to check for the key.</param>
        /// <returns>True if the value is a string that is not empty, false otherwise.</returns>
        protected static bool CheckExists(string key, IDictionary<string, string> parameters) 
        {
            return Get(key, parameters).Length > 0;
        }

        /// <summary>
        /// Checks the given key to see if it exists, and throws an exception if it 
        /// does not.  See <see cref="CheckExists"/> for more information.
        /// </summary>
        /// <param name="key">The key to check for existence.</param>
        /// <param name="parameters">The map to check for hte key.</param>
        /// <exception cref="OAuthException">
        /// If the value for the given key doesn't exist.
        /// </exception>
        protected static void AssertExists(string key, IDictionary<string, string> parameters)
        {
            if (!CheckExists(key, parameters)) 
            {
                throw new OAuthException(key + " does not exist.");
            }
        }
    }
}
