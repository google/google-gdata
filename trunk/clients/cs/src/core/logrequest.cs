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
using System.Net;

#endregion

/////////////////////////////////////////////////////////////////////
// <summary>contains GDataLogingRequest
//  </summary>
////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>base GDataRequestFactory implmentation</summary> 
    //////////////////////////////////////////////////////////////////////
    public class GDataLoggingRequestFactory : GDataGAuthRequestFactory
    {
        /// <summary>holds the filename for the input request</summary> 
        private string strInput;
        /// <summary>holds the filename for the output response</summary> 
        private string strOutput; 
        /// <summary>holds the filename for the combined logger</summary> 
        private string strCombined; 

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string RequestFileName</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string RequestFileName
        {
            get {return this.strInput;}
            set {this.strInput = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string ResponseFileName</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string ResponseFileName
        {
            get {return this.strOutput;}
            set {this.strOutput = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string CombinedLogFileName</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string CombinedLogFileName
        {
            get {return this.strCombined;}
            set {this.strCombined = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public GDataLoggingRequestFactory(string service, string applicationName) : base(service, applicationName)
        {
            this.strInput = "GDatarequest.xml";
            this.strOutput = "GDataresponse.xml"; 
            this.strCombined = "GDatatraffic.log"; 

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public GDataLoggingRequestFactory(string service, string applicationName, string library) : base(service, applicationName, library)
        {
            this.strInput = "GDatarequest.xml";
            this.strOutput = "GDataresponse.xml"; 
            this.strCombined = "GDatatraffic.log"; 

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public override IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget)
        {
            return new GDataLoggingRequest(type, uriTarget, this, this.strInput, this.strOutput, this.strCombined);
        }
        /////////////////////////////////////////////////////////////////////////////



    }
    /////////////////////////////////////////////////////////////////////////////


    //////////////////////////////////////////////////////////////////////
    /// <summary>base GDataRequest implementation</summary> 
    //////////////////////////////////////////////////////////////////////
    public class GDataLoggingRequest : GDataGAuthRequest, IDisposable
    {
        /// <summary>holds the filename for the input request</summary> 
        private string strInput;
        /// <summary>holds the filename for the output response</summary> 
        private string strOutput; 
        /// <summary>holds the filename for the combined logger</summary> 
        private string strCombined; 
        /// <summary>holds carriage return/linefeed </summary>
        private static byte [] arrCRLF = { 13,10,13,10 }; 


        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        internal GDataLoggingRequest(GDataRequestType type, Uri uriTarget, GDataGAuthRequestFactory factory, string strInputFileName, string strOutputFileName, string strCombinedLogFileName) : base(type, uriTarget, factory)
        {
            this.strInput = strInputFileName;
            this.strOutput = strOutputFileName;
            this.strCombined = strCombinedLogFileName;
        }
        /////////////////////////////////////////////////////////////////////////////

        
        //////////////////////////////////////////////////////////////////////
        /// <summary>does the real disposition</summary> 
        /// <param name="disposing">indicates if dispose called it or finalize</param>
        //////////////////////////////////////////////////////////////////////
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing); 
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>Executes the request and prepares the response stream. Also 
        /// does error checking</summary> 
        //////////////////////////////////////////////////////////////////////
        public override void Execute()
        {
            if (this.RequestCopy != null)
            {
                this.RequestCopy.Seek(0, SeekOrigin.Begin); 
                SaveStream(this.RequestCopy, this.strInput); 
                this.RequestCopy.Seek(0, SeekOrigin.Begin); 

            }
            try
            {
                base.Execute(); 

            } catch (GDataRequestException re)
            {
                Tracing.TraceMsg("Got into exception handling for base.execute"); 
                HttpWebResponse response = re.Response as HttpWebResponse;
            
                if (response != null)
                {
                    Stream   req = response.GetResponseStream(); 
                    SaveStream(req, this.strOutput); 
                }
                throw; 
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>private void SaveStream()</summary> 
        /// <param name="stream">the stream to save </param>
        /// <param name="targetFile">the targetFilename  to save into </param>
        //////////////////////////////////////////////////////////////////////
        private void SaveStream(Stream stream, String targetFile)
        {
            if (stream != null)
            {
                // save the response to the log
                FileStream w = new FileStream(targetFile, FileMode.Create);
                FileStream x = new FileStream(this.strCombined, FileMode.Append); 

                const int size = 4096;
                byte[] bytes = new byte[4096];
                int numBytes;

                x.Write(GDataLoggingRequest.arrCRLF, 0, 4); 


                while((numBytes = stream.Read(bytes, 0, size)) > 0)
                {
                    w.Write(bytes, 0, numBytes);
                    x.Write(bytes, 0, numBytes);
                }
                w.Close();
                x.Write(GDataLoggingRequest.arrCRLF, 0, 4); 
                x.Close(); 
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>gets the readable response stream</summary> 
        /// <returns> the response stream</returns>
        //////////////////////////////////////////////////////////////////////
        public override Stream GetResponseStream()
        {
            // get the real response stream, stream it to disk and return a filestream

            FileStream w = new FileStream(this.strOutput, FileMode.Create);
            FileStream x = new FileStream(this.strCombined, FileMode.Append); 

            MemoryStream mem = new MemoryStream(); 
            Stream   req = base.GetResponseStream(); 


            const int size = 4096;
            byte[] bytes = new byte[4096];
            int numBytes;

            x.Write(GDataLoggingRequest.arrCRLF, 0, 4); 

            while((numBytes = req.Read(bytes, 0, size)) > 0)
            {
                w.Write(bytes, 0, numBytes);
                x.Write(bytes, 0, numBytes);
                mem.Write(bytes, 0, numBytes); 
            }
            w.Close();
            mem.Seek(0, SeekOrigin.Begin); 
            x.Write(GDataLoggingRequest.arrCRLF, 0, 4); 
            x.Close(); 
            return mem; 
        }
        /////////////////////////////////////////////////////////////////////////////

    }
    /////////////////////////////////////////////////////////////////////////////
} 
/////////////////////////////////////////////////////////////////////////////
