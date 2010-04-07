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
 * 
 * Author: Andrew Smith <andy@snae.net> 22/11/08
 * 
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web;

namespace Google.GData.Client
{
    /// <summary>
    /// Provides a means to generate an OAuth signature suitable for use
    /// with Google two-legged OAuth requests.
    /// </summary>
    public class OAuthUtil : OAuthBase
    {
        public static readonly OAuthUtil Instance = new OAuthUtil();

        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns></returns>
        public override string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string timeStamp = ts.TotalSeconds.ToString(CultureInfo.InvariantCulture); 
            // remove any fractions of seconds
            int pointIndex = timeStamp.IndexOf(".");
            if (pointIndex != -1)
                timeStamp = timeStamp.Substring(0, pointIndex);
            return timeStamp;
        }

        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns>A nonce suitable for Google's two-legged OAuth implementation</returns>
        public override string GenerateNonce()
        {
            // changed from the original oauth code to use Guid
            return Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }

        /// <summary>
        /// Generates an OAuth header.
        /// </summary>
        /// <param name="uri">The URI of the request</param>
        /// <param name="consumerKey">The consumer key</param>
        /// <param name="consumerSecret">The consumer secret</param>
        /// <param name="httpMethod">The http method</param>
        /// <returns>The OAuth authorization header</returns>
        public static string GenerateHeader(Uri uri, String consumerKey, String consumerSecret, String httpMethod)
        {
            return GenerateHeader(uri, consumerKey, consumerSecret, string.Empty, string.Empty, httpMethod);
        }

        public static string GenerateHeader(Uri uri, String consumerKey, String consumerSecret, String token, String tokenSecret, String httpMethod)
        {
            string timeStamp = Instance.GenerateTimeStamp();
            string nonce = Instance.GenerateNonce();
            string normalizedUrl; string normalizedRequestParameters;

  
            string signature = Instance.GenerateSignature(uri, consumerKey, consumerSecret, token, tokenSecret,
                httpMethod.ToUpper(), timeStamp, nonce, out normalizedUrl, out normalizedRequestParameters);
           
            signature = System.Web.HttpUtility.UrlEncode(signature);
            
            StringBuilder sb = new StringBuilder();
            sb.Append("Authorization: OAuth realm=\"\",oauth_version=\"1.0\",");
            sb.AppendFormat("oauth_nonce=\"{0}\",", nonce);
            sb.AppendFormat("oauth_timestamp=\"{0}\",", timeStamp);
            sb.AppendFormat("oauth_consumer_key=\"{0}\",", consumerKey);
            if (!String.IsNullOrEmpty(token))
            {
                token = System.Web.HttpUtility.UrlEncode(token);
                sb.AppendFormat("oauth_token=\"{0}\",", token);
            }
            sb.Append("oauth_signature_method=\"HMAC-SHA1\",");
            sb.AppendFormat("oauth_signature=\"{0}\"", signature);

            return sb.ToString();
        }

        /// <summary>
        /// Calculates the signature base url as per section 9.1 of the OAuth Spec.
        /// This is a concatenation of http method, request url, and other request
        /// parameters.
        /// 
        /// See <a href="http://oauth.net/core/1.0/#anchor14">9.1 Signature Base String</a>.
        /// </summary>
        /// <param name="requestUrl">The URL of the request.</param>
        /// <param name="httpMethod">The HTTP method, for example "GET" or "PUT"</param>
        /// <param name="baseParameters">
        /// The request parameters (see section 9.1.3 of the OAuth spec).
        /// </param>
        /// <returns>The base string to be used in the OAuth signature.</returns>
        /// <exception cref="OAuthException">If the input URL is not formatted properly.</exception>
        public string GenerateSignatureBaseString(
            string requestUrl, string httpMethod, IDictionary<string, string> baseParameters)
        {
            return UrlEncode(httpMethod.ToUpper()) + '&'
                    + UrlEncode(NormalizeUrl(requestUrl)) + '&'
                    + UrlEncode(NormalizeParameters(requestUrl, baseParameters));
        }


        /// <summary>
        /// Calculates the normalized request url, as per section 9.1.2 of the OAuth
        /// Spec.  This removes the querystring from the url and the port (if it is
        /// the standard http or https port).
        /// 
        /// See <a href="http://oauth.net/core/1.0/#rfc.section.9.1.2">9.1.2
        /// Construct Request URL</a>.
        /// </summary>
        /// <param name="requestUrl">
        /// The request URL to normalize (not <code>null</code>).
        /// </param>
        /// <returns>
        /// The normalized requset URL, as per the rules in the link above.
        /// </returns>
        /// <exception cref="OAuthException">
        /// If the input URL is not formatted properly.
        /// </exception>
        public static string NormalizeUrl(string requestUrl)
        {
            // validate the request url
            if (string.IsNullOrEmpty(requestUrl))
            {
                throw new OAuthException("Request URL cannot be empty");
            }

            // parse the url into its constituent parts.
            Uri uri;
            try
            {
                uri = new Uri(requestUrl);
            }
            catch (UriFormatException e)
            {
                throw new OAuthException(e);
            }

            string authority = uri.Authority;
            string scheme = uri.Scheme;
            if (authority == null || scheme == null)
            {
                throw new OAuthException("Invalid Request URL");
            }
            authority = authority.ToLower();
            scheme = scheme.ToLower();

            // if this url contains the standard port, remove it
            if ((scheme == "http" && uri.Port == 80)
                || (scheme == "https" && uri.Port == 443))
            {
                int index = authority.LastIndexOf(":");
                if (index >= 0)
                {
                    authority = authority.Substring(0, index);
                }
            }

            // piece together the url without the querystring
            return scheme + "://" + authority + uri.LocalPath;
        }

        /// <summary>
        /// Calculates the normalized request parameters string to use in the base
        /// string, as per section 9.1.1 of the OAuth Spec.
        /// 
        /// See <a href="http://oauth.net/core/1.0/#rfc.section.9.1.1">9.1.1
        /// Normalize Request Parameters</a>.
        /// </summary>
        /// <param name="requestUrl">
        /// The request url to normalize (not <code>null</code>)
        /// </param>
        /// <param name="requestParameters">
        /// Key/value pairs of parameters in the request
        /// </param>
        /// <returns>The parameters normalized to a string.</returns>
        public string NormalizeParameters(string requestUrl, IDictionary<string, string> requestParameters)
        {
            // Use a SortedDictionary to alphabetize the parameters by their key
            SortedDictionary<string, string> sortedDictionary
                = new SortedDictionary<string, string>(requestParameters);

            // Add the query string to the base string (if one exists)
            int c = requestUrl.IndexOf('?');
            if (c > 0)
            {
                foreach (KeyValuePair<string, string> kvp
                    in ParseQuerystring(requestUrl.Substring(c + 1)))
                {
                    sortedDictionary.Add(kvp.Key, kvp.Value);
                }
            }

            // piece together the base string, encoding each key and value
            StringBuilder paramString = new StringBuilder();
            foreach (KeyValuePair<string, string> e in sortedDictionary)
            {
                if (e.Value.Length == 0)
                {
                    continue;
                }
                if (paramString.Length > 0)
                {
                    paramString.Append("&");
                }
                paramString.Append(UrlEncode(e.Key)).Append("=").Append(UrlEncode(e.Value));
            }
            return paramString.ToString();
        }

        /// <summary>
        /// Parse a querystring into a map of key/value pairs.
        /// </summary>
        /// <param name="queryString">the string to parse (without the '?')</param>
        /// <returns>key/value pairs mapping to the items in the querystring</returns>
        public IDictionary<string, string> ParseQuerystring(string queryString)
        {
            IDictionary<string, string> map = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(queryString))
            {
                return map;
            }

            string[] parameters = queryString.Split('&');
            foreach (string param in parameters)
            {
                string[] keyValuePair = param.Split(new[] { '=' }, 2);
                string name = HttpUtility.UrlDecode(keyValuePair[0], Encoding.UTF8);
                if (name == "")
                {
                    continue;
                }
                string value = keyValuePair.Length > 1
                    ? HttpUtility.UrlDecode(keyValuePair[1], Encoding.UTF8) : "";
                map[name] = value;
            }
            return map;
        }
    }
}
