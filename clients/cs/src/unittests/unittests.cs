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
using Google.GData.Client.LiveTests;
using Google.GData.Extensions;
using Google.GData.Calendar;

namespace Google.GData.Client.UnitTests
{
    [TestFixture]
    public class BaseTestClass
    {
        /// <summary>default extension for temp files. They do get removed after a successful run</summary> 
        protected static string defExt = ".log"; 
        /// <summary>holds the current working directory</summary> 
        protected string currentDir; 
        /// <summary>holds the default localhost address</summary> 
        protected string defaultHost; 
        /// <summary>holds the default external host</summary> 
        protected string strRemoteHost; 
        /// <summary>holds the default remote host address</summary> 
        protected IDictionary externalHosts; 
        /// <summary>holds the number of iterations for the tests</summary> 
        protected int iIterations; 
        /// <summary>holds the logging factory</summary> 
        protected IGDataRequestFactory factory; 
        /// <summary>holds the configuration of the test found in the dll.config file</summary>
        protected IDictionary unitTestConfiguration;
        /// <summary>holds path to resources (xml files, jpgs) that are used during the unittests</summary>
        protected string resourcePath = "";


        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public BaseTestClass()
        {
        }

        public virtual string ServiceName
        {
            get {
                return "cl"; 
            }
        }

        public virtual string ApplicationName
        {
            get {
                return "UnitTests"; 
            }
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>the setup method</summary> 
        //////////////////////////////////////////////////////////////////////
        [SetUp] public virtual void InitTest()
        {
            this.currentDir = Directory.GetCurrentDirectory();
            Tracing.InitTracing();

            this.defaultHost = "http://localhost";
            this.strRemoteHost = null; 
            this.externalHosts = null; 
            this.iIterations = 10; 

            if (this.factory == null)
            {
                GDataLoggingRequestFactory factory = new GDataLoggingRequestFactory(this.ServiceName, this.ApplicationName); 
                this.factory = (IGDataRequestFactory) factory; 
            }
            ReadConfigFile(); 
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>private void ReadConfigFile()</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected virtual void ReadConfigFile()
        {
            this.unitTestConfiguration = (IDictionary) ConfigurationManager.GetSection("unitTestSection");

            // no need to go further if the configuration file is needed.
            if (unitTestConfiguration == null)
                throw new FileNotFoundException("The DLL configuration file wasn't found, aborting.");

            if (unitTestConfiguration.Contains("defHost"))
            {
                this.defaultHost = (string) unitTestConfiguration["defHost"];
                Tracing.TraceInfo("Read defaultHost value: " + this.defaultHost);
            }
            if (unitTestConfiguration.Contains("defRemoteHost"))
            {
                this.strRemoteHost = (string) unitTestConfiguration["defRemoteHost"];
                Tracing.TraceInfo("Read default remote host value: " + this.strRemoteHost);
            }
            if (unitTestConfiguration.Contains("iteration"))
            {
                this.iIterations = int.Parse((string)unitTestConfiguration["iteration"]);
            }
            if (unitTestConfiguration.Contains("resourcePath"))
            {
                this.resourcePath =(string) unitTestConfiguration["resourcePath"];
            }

            if (unitTestConfiguration.Contains("requestlogging"))
            {
                bool flag = bool.Parse((string) unitTestConfiguration["requestlogging"]);
                if (!flag)
                {
                    // we are creating the logging factory by default. If 
                    // tester set's it off, create the standard factory. 
                    this.factory = new GDataGAuthRequestFactory(this.ServiceName, this.ApplicationName);
                }
            }

            this.externalHosts = (IDictionary)ConfigurationManager.GetSection("unitTestExternalHosts");
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>the end it all method</summary> 
        //////////////////////////////////////////////////////////////////////
        [TearDown] public virtual void EndTest()
        {
            Tracing.ExitTracing();
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>private string CreateDumpFileName(string baseName)</summary> 
        /// <param name="baseName">the basename</param>
        /// <returns>the complete filename for file creation</returns>
        //////////////////////////////////////////////////////////////////////
        protected string CreateDumpFileName(string baseName)
        {
            return this.currentDir + Path.DirectorySeparatorChar + baseName + BaseTestClass.defExt;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>private string CreateUriFileName(string baseName)</summary> 
        /// <param name="baseName">the basename</param>
        /// <returns>the complete Uri name for file access</returns>
        //////////////////////////////////////////////////////////////////////
        protected string CreateUriLogFileName(string baseName)
        {
            return CreateUriFileName(baseName + BaseTestClass.defExt);
        }
        /////////////////////////////////////////////////////////////////////////////
         
        //////////////////////////////////////////////////////////////////////
        /// <summary>private string CreateUriFileName(string baseName)</summary> 
        /// <param name="baseName">the basename</param>
        /// <returns>the complete Uri name for file access</returns>
        //////////////////////////////////////////////////////////////////////
        protected string CreateUriFileName(string fileName)
        {
            string fileAndPath = this.currentDir + "/"  + fileName;
            return CreateUri(fileAndPath);
        }
        /////////////////////////////////////////////////////////////////////////////
         

        //////////////////////////////////////////////////////////////////////
        /// <summary>private string CreateUriFileName(string baseName)</summary> 
        /// <param name="baseName">the basename</param>
        /// <returns>the complete Uri name for file access</returns>
        //////////////////////////////////////////////////////////////////////
        protected string CreateUri(string fileAndPathName)
        {
            string strUri = null;

            try
            {
                UriBuilder temp = new UriBuilder("file", "localhost", 0, fileAndPathName);
                strUri = temp.Uri.AbsoluteUri; 
            }
            catch (System.UriFormatException)
            {
                UriBuilder temp = new UriBuilder("file", "", 0, fileAndPathName);
                strUri = temp.Uri.AbsoluteUri; 
            }
            return(strUri);
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// eventhandling. called when a new entry is parsed
        /// </summary>
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnParsedNewEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }
            if (e.CreatingEntry)
            {
                Tracing.TraceMsg("\t top level event dispatcher - new Entry"); 
                e.Entry = new MyEntry();
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>eventhandler - called for new extension element
        /// </summary>
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewExtensionElement(object sender, ExtensionElementEventArgs e)
        {
            // by default, if our event chain is not hooked, the underlying parser will add it
            Tracing.TraceCall("received new extension element notification");
            Tracing.Assert(e != null, "e should not be null");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }
            Tracing.TraceMsg("\t top level event = new extension"); 

            if (String.Compare(e.ExtensionElement.NamespaceURI, "http://purl.org/dc/elements/1.1/", true) == 0)
            {
                // found DC namespace
                Tracing.TraceMsg("\t top level event = new DC extension"); 
                if (e.ExtensionElement.LocalName == "date")
                {
                    MyEntry entry = e.Base as MyEntry;

                    if (entry != null)
                    {
                        entry.DCDate = DateTime.Parse(e.ExtensionElement.InnerText);
                        e.DiscardEntry = true;
                    }

                }
            }
        }
        
    } // end of BaseTestClass




    /// <summary>this is just a dummy class to test new tests. Use the Ignore on the fixutre to disable or enable one class</summary> 
    [TestFixture] 
    public class InDevTests : IBaseWalkerAction
    {
        /// <summary>basic public constructor for Nunit</summary> 
        public InDevTests()
        {
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>public bool Go(AtomBase baseObject)</summary> 
        /// <param name="baseObject">object to do something with </param>
        /// <returns>true if we are done walking the tree</returns>
        //////////////////////////////////////////////////////////////////////
        public bool Go(AtomBase baseObject)
        {
            Tracing.TraceInfo("inside go: " + baseObject.ToString() + " is dirty set: " + baseObject.IsDirty().ToString());
            return false; 
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>the setup method</summary> 
        //////////////////////////////////////////////////////////////////////
        [SetUp] public virtual void InitTest()
        {
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>creates a number or rows and delets them again</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void TestIt()
        {
            Tracing.TraceMsg("Entering TestIt");
        }
        /////////////////////////////////////////////////////////////////////////////
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>a subclass that is used to represent the tree in the extension testcase</summary>
    //////////////////////////////////////////////////////////////////////
    public class MyEntry : AtomEntry
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>saves the inner state of the element</summary>
        /// <param name="writer">the xmlWriter to save into </param>
        //////////////////////////////////////////////////////////////////////
        protected override void SaveInnerXml(XmlWriter writer)
        {
            base.SaveInnerXml(writer);
            writer.WriteElementString("date", "http://purl.org/dc/elements/1.1/", this.dcDate.ToString()); 
        }

        private DateTime dcDate;

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public DateTime DCDate</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public DateTime DCDate
        {
            get {return this.dcDate;}
            set {this.dcDate = value;}
        }
        /////////////////////////////////////////////////////////////////////////////
    }
}




