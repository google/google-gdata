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

namespace Google.GData.Client
{
    public class GoogleOAuthHelper : OAuthHelper
    {
        // Google's OAuth endpoints
        private static readonly string requestTokenUrl =
            "https://www.google.com/accounts/OAuthGetRequestToken";
        private static readonly string userAuthorizationUrl =
            "https://www.google.com/accounts/OAuthAuthorizeToken";
        private static readonly string accessTokenUrl =
          "https://www.google.com/accounts/OAuthGetAccessToken";
        private static readonly string revokeTokenUrl =
          "https://www.google.com/accounts/AuthSubRevokeToken";

        /// <summary>
        /// Creates a new GoogleOAuthHelper, which is an {@link OAuthHelper} with
        /// specific urls.
        /// </summary>
        /// <param name="signer">
        /// The <see cref="IOAuthSigner"/> to use when signing the request
        /// </param>
        public GoogleOAuthHelper(IOAuthSigner signer) 
            : base(requestTokenUrl, userAuthorizationUrl, accessTokenUrl, revokeTokenUrl, signer)
        {
        }
    }
}
