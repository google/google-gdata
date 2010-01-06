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
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;

#endregion


/////////////////////////////////////////////////////////////////////
// <summary>contains support classes to work with 
// the resumable upload protocol. 
//  </summary>
////////////////////////////////////////////////////////////////////
namespace Google.GData.Client.ResumableUpload
{

    class ResumableUploader
    {
        // chunksize in Megabytes
        private int chunkSize;
        private static int MB = 1048576;
        private Authenticator authenticator;

        /// <summary>
        /// The relationship value to be used to find the resumable 
        /// </summary>
        public static string CreateMediaRelation = "http://schemas.google.com/g/2005#resumable-create-media";
        public static string EditMediaRelation = "http://schemas.google.com/g/2005#resumable-edit-media";

        /// <summary>
        /// Default constructor. Uses the default chunksize of one megabyte
        /// </summary>
        /// <returns></returns>
        public ResumableUploader() : this(1)
        {
        }

        /// <summary>
        /// ResumableUploader constructor. 
        /// </summary>
        /// <param name="chunkSize">the upload chunksize in Megabytes, needs to be greater 0</param>
        /// <returns></returns>
        public ResumableUploader(int chunkSize)
        {
            if (chunkSize < 1)
                throw new ArgumentException("chunkSize needs to be > 0");
            this.chunkSize = chunkSize;
        }

        /// <summary>
        /// returns the resumable edit media Uri for a given entry
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static Uri GetResumableEditUri(AbstractEntry entry)
        {
             // scan the link collection
            AtomLink link = entry.Links.FindService(EditMediaRelation, null);
            return link == null ? null : new Uri(link.AbsoluteUri);
        }

        /// <summary>
        /// returns the resumabled create media Uri for a given entry
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static Uri GetResumableCreateUri(AbstractEntry entry)
        {
            // scan the link collection
            AtomLink link = entry.Links.FindService(CreateMediaRelation, null);
            return link == null ? null : new Uri(link.AbsoluteUri);
        }
        

        /// <summary>
        /// Uploads an entry, including it's media to the uri given inside the entry. 
        /// </summary>
        /// <param name="authentication">The authentication information to be used</param>
        /// <param name="payload">The entry to be uploaded. This is a complete entry, including the metadata. 
        /// This will create a new entry on the service</param>
        /// <returns></returns>
        public WebResponse Insert(Authenticator authentication, AbstractEntry payload)
        {
            Uri initialUri = ResumableUploader.GetResumableCreateUri(payload);
            if (initialUri == null)
                throw new ArgumentException("payload did not contain a resumabled create media Uri");

            Uri resumeUri = InitiateUpload(initialUri, authentication, payload.MediaSource.ContentType, payload);
            return UploadStream(HttpMethods.Post, resumeUri,  authentication, payload.MediaSource.Data, payload.MediaSource.ContentType);
        }

        /// <summary>
        /// Uploads just the media media to the uri given. 
        /// </summary>
        /// <param name="resumableUploadUri"></param>
        /// <param name="authentication">The authentication information to be used</param>
        /// <param name="payload">The media to uploaded.</param>
        /// <param name="contentType">The type of the content, e.g. text/html</param>
        /// <returns></returns>
        public WebResponse Insert(Uri resumableUploadUri, Authenticator authentication, Stream payload, string contentType)
        {
            Uri resumeUri = InitiateUpload(resumableUploadUri, authentication, contentType, null);
            return UploadStream(HttpMethods.Post, resumeUri, authentication, payload, contentType);
        }
        
        /// <summary>
        /// Uploads an entry, including it's media to the uri given inside the entry
        /// </summary>
        /// <param name="resumableUploadUri"></param>
        /// <param name="authentication">The authentication information to be used</param>
        /// <param name="payload">The entry to be uploaded. This is a complete entry, including the metadata. 
        /// This will create a new entry on the service</param>
        /// <returns></returns>
        public WebResponse Update(Authenticator authentication, AbstractEntry payload)
        {
            Uri initialUri = ResumableUploader.GetResumableEditUri(payload);
            if (initialUri == null)
                throw new ArgumentException("payload did not contain a resumabled edit media Uri");

            Uri resumeUri = InitiateUpload(initialUri, authentication, payload.MediaSource.ContentType, payload);
            return UploadStream(HttpMethods.Put, resumeUri, authentication, payload.MediaSource.Data, payload.MediaSource.ContentType);
        }

        /// <summary>
        /// Uploads just the media media to the uri given.
        /// </summary>
        /// <param name="resumableUploadUri"></param>
        /// <param name="authentication">The authentication information to be used</param>
        /// <param name="payload">The media to uploaded.</param>
        /// <param name="contentType">The type of the content, e.g. text/html</param>
        /// <returns></returns>
        public WebResponse Update(Uri resumableUploadUri, Authenticator authentication, Stream payload, string contentType)
        {
            Uri resumeUri = InitiateUpload(resumableUploadUri, authentication, contentType, null);
            return UploadStream(HttpMethods.Put, resumeUri, authentication, payload, contentType);
        }

        /// <summary>
        /// Note the URI passed in here, is the session URI obtained by InitiateUpload
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="authentication"></param>
        /// <param name="payload"></param>
        /// <param name="mediaType"></param>
        private WebResponse UploadStream(string httpMethod, Uri sessionUri, Authenticator authentication, Stream payload, string mediaType)
        {
            HttpWebResponse returnResponse = null;
            // upload one part at a time
            int index = 0;
            bool isDone = false;
  
            do
            {

                using (HttpWebResponse response = UploadStreamPart(index, httpMethod, sessionUri, authentication, payload, mediaType))
                {
                    int status = (int)response.StatusCode;
                    switch (status)
                    {

                        case 308:
                            isDone = false;
                            break;
                        case 200:
                        case 201:
                            isDone = true;
                            returnResponse = response;
                            break;
                        default:
                            throw new ApplicationException("Unexpected return code during resumable upload");

                    }

                }
                index++;
            } while (isDone == false);
            return returnResponse;
        }

        private HttpWebResponse UploadStreamPart(int partIndex, string httpMethod, Uri sessionUri, Authenticator authentication, Stream payload, string mediaType)
        {
            HttpWebRequest request = authentication.CreateHttpWebRequest(httpMethod, sessionUri);

            // write the data
            CopyData(payload, request, partIndex);
            request.ContentType = mediaType;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            return response;
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>takes our copy of the stream, and puts it into the request stream
        /// returns FALSE when we are done by reaching the end of the input stream</summary> 
        //////////////////////////////////////////////////////////////////////
        protected bool CopyData(Stream input, HttpWebRequest request, int partIndex)
        {
            request.ContentLength = this.chunkSize * ResumableUploader.MB;

            long chunkCounter = 0;
            long chunkStart = partIndex * this.chunkSize * ResumableUploader.MB;
            long dataLength;
            
            try
            {
                dataLength = input.Length;
            }
            catch (NotSupportedException e)
            {
                dataLength = -1;
            }

            // stream it into the real request stream
            using (Stream req = request.GetRequestStream())
            {
                 // move the source stream to the correct position
                input.Seek(chunkStart, SeekOrigin.Begin);

                const int size = 16384;
                byte[] bytes = new byte[size];
                int numBytes;

                // to reduce memory consumption, we read in 16K chunks
            
                while ((numBytes = input.Read(bytes, 0, size)) > 0)
                {
                    req.Write(bytes, 0, numBytes);
                    chunkCounter += numBytes;
                }
            }

            // modify the content-range header

            string contentRange = String.Format("bytes {0}-{1}/{2}", chunkStart, chunkStart + chunkCounter, dataLength > 0 ? dataLength.ToString() : "*");
            request.Headers.Add(HttpRequestHeader.ContentRange, contentRange);

            return chunkCounter < this.chunkSize;
    
        }
        /////////////////////////////////////////////////////////////////////////////
 


        /// <summary>
        /// retrieves the resumable URI for the rest of the operation. This will initiate the 
        /// communication with resumable upload server by posting against the starting URI
        /// </summary>
        /// <param name="resumableUploadUri"></param>
        /// <param name="authentication"></param>
        /// <param name="entry"></param>
        /// <returns>The uri to be used for the rest of the operation</returns>
        private Uri InitiateUpload(Uri resumableUploadUri, Authenticator authentication, string contentType, AbstractEntry entry)
        {
            this.authenticator = authentication;

            HttpWebRequest request = this.authenticator.CreateHttpWebRequest(HttpMethods.Post, resumableUploadUri);

            if (entry != null)
            {
                IVersionAware v = entry as IVersionAware;
                if (v != null)
                {
                    // need to add the version header to the request
                    request.Headers.Add(GDataGAuthRequestFactory.GDataVersion, v.ProtocolMajor.ToString() + "." + v.ProtocolMinor.ToString());
                }
                Stream outputStream = request.GetRequestStream();
                entry.SaveToXml(request.GetRequestStream());
            }

            request.ContentType = contentType;

            WebResponse response = request.GetResponse();
            return new Uri(response.Headers["Location"]);
        }
        
    }
}
