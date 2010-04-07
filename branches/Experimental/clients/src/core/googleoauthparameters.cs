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
    public class GoogleOAuthParameters : OAuthParameters
    {
        public static readonly string SCOPE_KEY = "scope";
        public static readonly string XOAUTH_DISPLAYNAME_KEY = "xoauth_displayname";

        /// <summary>
        /// The request scope.  The scope is a URI defined by each Google
        /// service that indicates which resources the user has permissions to access.
        /// It is used when retrieving the unauthorized request token.  Multiple scopes
        /// are separated with a space.  This parameter is required for OAuth requests
        /// against Google.
        /// </summary>
        public string Scope
        {
            get { return Get(SCOPE_KEY, baseParameters); }
            set { Put(SCOPE_KEY, value, baseParameters); }
        }


        public string DisplayName
        {
            get { return Get(XOAUTH_DISPLAYNAME_KEY, baseParameters); }
            set { Put(XOAUTH_DISPLAYNAME_KEY, value, baseParameters); }
        }

        /// <summary>
        /// Since the scope parameter may be different for each OAuth request, it is
        /// cleared between requests, and must be manually set before each request.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            Remove(SCOPE_KEY, baseParameters);
            Remove(XOAUTH_DISPLAYNAME_KEY, baseParameters);
        }
    }
}
