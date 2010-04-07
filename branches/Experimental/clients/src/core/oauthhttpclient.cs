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
using System.IO;
using System.Net;

namespace Google.GData.Client
{
    public class OAuthHttpClient
    {
        /// <summary>
        /// Makes an HTTP request to the input URL, and returns the response body as 
        /// a string.
        /// </summary>
        /// <param name="uri">The url to make the request to</param>
        /// <returns>The response body of the request decoded to a string.</returns>
        /// <exception cref="OAuthException">
        /// If there was an error making the request.
        /// </exception>
        public string GetResponse(Uri uri)
        {
            return GetResponse(uri, null);
        }

        /// <summary>
        /// Makes an HTTP request to the input URL, and returns the response body as 
        /// a string.
        /// </summary>
        /// <param name="uri">The url to make the request to</param>
        /// <param name="headers">Any headers to add to the request.  May be null.</param>
        /// <returns>The response body of the request decoded to a string.</returns>
        /// <exception cref="OAuthException">
        /// If there was an error making the request.
        /// </exception>
        public string GetResponse(Uri uri, IDictionary<string, string> headers)
        {
            try 
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);   
                if (headers != null)
                {
                    foreach (var entry in headers)
                    {
                        req.Headers.Add(entry.Key, entry.Value);
                    }
                }

                WebResponse res = req.GetResponse();

                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            } 
            catch (IOException e) 
            {
                throw new OAuthException("Error getting HTTP response", e);
            }
        }
    }
}
