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
using System.Security.Cryptography;
using System.Text;

namespace Google.GData.Client
{
    public class OAuthHmacSha1Signer : IOAuthSigner
    {
        public string Sign(string baseString, OAuthParameters oauthParameters)
        {
            if (oauthParameters == null)
            {
                throw new OAuthException("OAuth parameters cannot be null");
            }
            return Sign(baseString, GetKey(oauthParameters));
        }

        /// <summary>
        /// Signs the string using HMACSHA1.
        /// </summary>
        /// <param name="baseString">The string to sign.</param>
        /// <param name="keyString">The key to use for signing.</param>
        /// <returns>The signature encoded in Base64.</returns>
        public string Sign(string baseString, string keyString)
        {
            try
            {
                HMAC hmac = HMAC.Create("HMACSHA1");
                hmac.Key = Encoding.UTF8.GetBytes(keyString);
                return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(baseString)));
            }
            catch (ArgumentNullException e)
            {
                throw new OAuthException(e);
            }
            catch (EncoderFallbackException e)
            {
                throw new OAuthException(e);
            }
        }

        public string SignatureMethod
        {
            get { return "HMAC-SHA1"; }
        }

        private static String GetKey(OAuthParameters oauthParameters) {
            return new StringBuilder()
                .Append(OAuthUtil.Instance.UrlEncode(oauthParameters.OAuthConsumerSecret))
                .Append("&")
                .Append(OAuthUtil.Instance.UrlEncode(oauthParameters.OAuthTokenSecret))
                .ToString();
        }
    }
}
