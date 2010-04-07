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
    public interface IOAuthSigner
    {
        /// <summary>
        /// Signs the input string using the appropriate signature method.
        /// </summary>
        /// <param name="baseString">The string to sign.</param>
        /// <param name="oauthParameters">
        /// The parameters related to the OAuth request, or null.
        /// </param>
        /// <returns>The signed string.</returns>
        /// <exception cref="OAuthException">
        /// If signing fails for any reason.
        /// </exception>
        string Sign(string baseString, OAuthParameters oauthParameters);

        /// <summary>
        /// The signature method used to sign the base string for this
        /// specific implementation.
        /// </summary>
        string SignatureMethod { get; }
    }
}
