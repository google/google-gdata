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
#define USE_TRACING
#define DEBUG

using System;
using System.IO;
using System.Xml; 
using System.Collections;
using System.Configuration;
using System.Net; 
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Client.UnitTests;
using Google.GData.Extensions;
using Google.GData.Photos;




namespace Google.GData.Client.LiveTests
{
    [TestFixture] 
    [Category("LiveTest")]
    public class PhotosTestSuite : BaseLiveTestClass
    {
        /// <summary>
        ///  test Uri for google calendarURI
        /// </summary>
        protected string defaultPhotosUri; 

      //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public PhotosTestSuite()
        {
        }

        public override string ServiceName
        {
            get {
                return PicasaService.GPicasaService; 
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>private void ReadConfigFile()</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("photosUri") == true)
            {
                this.defaultPhotosUri = (string) unitTestConfiguration["photosUri"];
                Tracing.TraceInfo("Read photosUri value: " + this.defaultPhotosUri);
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void PhotosAuthenticationTest()
        {
            Tracing.TraceMsg("Entering PhotosAuthenticationTest");

            PicasaQuery query = new PicasaQuery();
            PicasaService service = new PicasaService("unittests");
            query.KindParameter = new PicasaQuery.Kinds[2] {PicasaQuery.Kinds.album, PicasaQuery.Kinds.tag};
           
            int iCount; 

            if (this.defaultPhotosUri != null)
            {
                if (this.userName != null)
                {
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultPhotosUri);
                AtomFeed feed = service.Query(query);

                ObjectModelHelper.DumpAtomObject(feed,CreateDumpFileName("PhotoAuthTest")); 
                iCount = feed.Entries.Count; 

                if (feed != null && feed.Entries.Count > 0)
                {
                    Tracing.TraceMsg("Found a Feed " + feed.ToString());
                    DisplayExtensions(feed);

                    foreach (AtomEntry entry in feed.Entries)
                    {
                        Tracing.TraceMsg("Found an entry " + entry.ToString());
                        DisplayExtensions(entry);
                    }
                }

                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void QueryPhotosTest()
        {
            Tracing.TraceMsg("Entering PhotosQueryPhotosTest");

            PhotoQuery query = new PhotoQuery();
            PicasaService service = new PicasaService("unittests");
           
            int iCount; 

            if (this.defaultPhotosUri != null)
            {
                if (this.userName != null)
                {
                    NetworkCredential nc = new NetworkCredential(this.userName, this.passWord); 
                    service.Credentials = nc;
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory) this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.defaultPhotosUri);
                AtomFeed feed = service.Query(query);

                ObjectModelHelper.DumpAtomObject(feed,CreateDumpFileName("PhotoAuthTest")); 
                iCount = feed.Entries.Count; 

                if (feed != null && feed.Entries.Count > 0)
                {
                    Tracing.TraceMsg("Found a Feed " + feed.ToString());
                    DisplayExtensions(feed);

                    foreach (AtomEntry entry in feed.Entries)
                    {
                        Tracing.TraceMsg("Found an entry " + entry.ToString());
                        DisplayExtensions(entry);
                    }
                }

                factory.MethodOverride = false;
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        protected void DisplayExtensions(AtomBase obj) 
        {
            foreach (Object o in obj.ExtensionElements)
            {
                
                IExtensionElement e = o as IExtensionElement;
                SimpleElement s = o as SimpleElement;
                XmlElement x = o as XmlElement;
                if (s != null)
                {
                    Tracing.TraceMsg("Found a simple Element " + s.ToString() + " " + s.Value);
                } 
                else if (e != null)
                {
                    Tracing.TraceMsg("Found an extension Element " + e.ToString());
                } 
                else if (x != null)
                {
                    Tracing.TraceMsg("Found an XmlElement " + x.ToString() + " " + x.LocalName + " " + x.NamespaceURI);
                }
                else 
                {
                    Tracing.TraceMsg("Found an object " + o.ToString());
                }
            }
        }

    } /////////////////////////////////////////////////////////////////////////////
}




