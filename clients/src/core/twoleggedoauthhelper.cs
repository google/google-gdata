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
    public class TwoLeggedOAuthHelper
    {
        private readonly IOAuthSigner signer;
        private readonly OAuthParameters parameters;

        public TwoLeggedOAuthHelper(IOAuthSigner signer, OAuthParameters parameters)
        {
            this.signer = signer;
            this.parameters = parameters;
        }

        internal void ValidateInputParameters() 
        {
            parameters.AssertOAuthConsumerKeyExists();
            if (signer is OAuthHmacSha1Signer)
            {
                parameters.AssertOAuthConsumerSecretExists();
            }
        }


        /// <summary>
        /// Generate and add request-specific parameters that are common to all OAuth
        /// requests (if the user did not already specify them in the oauthParameters
        /// object). The following parameters are added to the oauthParameter set:
        /// <ul>
        /// <li>oauth_signature
        /// <li>oauth_signature_method
        /// <li>oauth_timestamp
        /// <li>oauth_nonce
        /// </ul>
        /// </summary>
        /// <param name="baseUrl">The url to make the request to</param>
        /// <param name="httpMethod">
        /// the http method of this request (for example, "GET").
        /// </param>
        /// <exception cref="OAuthException">
        /// If there is an error with the OAuth request.
        /// </exception>
        internal void AddCommonRequestParameters(string baseUrl, string httpMethod)
        {
            // add the signature method if it doesn't already exist.
            if (parameters.OAuthSignatureMethod.Length == 0) 
            {
                parameters.OAuthSignatureMethod = signer.SignatureMethod;
            }
            
            // add the nonce if it doesn't already exist.
            if (parameters.OAuthTimestamp.Length == 0) 
            {
                parameters.OAuthTimestamp = OAuthUtil.Instance.GenerateTimeStamp();
            }

            // add the timestamp if it doesn't already exist.
            if (parameters.OAuthNonce.Length == 0) 
            {
                parameters.OAuthNonce = OAuthUtil.Instance.GenerateNonce();
            }

            // add the signature if it doesn't already exist.
            // The signature is calculated by the IOAuthSigner.
            if (parameters.OAuthSignature.Length == 0) 
            {
                string baseString = OAuthUtil.Instance.GenerateSignatureBaseString(baseUrl, httpMethod, parameters.BaseParameters);
                parameters.OAuthSignature = signer.Sign(baseString, parameters);
            }
        }
    }
}
