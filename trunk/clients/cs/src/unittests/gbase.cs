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
using Google.GData.Extensions;
using Google.GData.GoogleBase;
using Google.GData.Client.UnitTests;

namespace Google.GData.Client.LiveTests
{
    [TestFixture] 
    [Category("LiveTest")]
    public class GBatchTestSuite : BaseLiveTestClass
    {
        private string gBaseURI; // holds the base uri

        private string gBaseKey; // holds the key 


        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public GBatchTestSuite()
        {
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>the setup method</summary> 
        //////////////////////////////////////////////////////////////////////
        [SetUp] public override void InitTest()
        {
            base.InitTest(); 
            GDataGAuthRequestFactory authFactory = this.factory as GDataGAuthRequestFactory; 
            if (authFactory != null)
            {
                authFactory.Handler = this.strAuthHandler; 
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////
        /// <summary>the end it all method</summary> 
        //////////////////////////////////////////////////////////////////////
        [TearDown] public override void EndTest()
        {
            Tracing.ExitTracing();
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>private void ReadConfigFile()</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("gBaseURI"))
            {
                this.gBaseURI = (string) unitTestConfiguration["gBaseURI"];
                Tracing.TraceInfo("Read gBase URI value: " + this.gBaseURI);
            }
            if (unitTestConfiguration.Contains("gBaseKey"))
            {
                this.gBaseKey = (string) unitTestConfiguration["gBaseKey"];
                Tracing.TraceInfo("Read gBaseKey value: " + this.gBaseKey);
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        public override string ServiceName
        {
            get 
            {
                return "gbase"; 
            }
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GoogleBaseAuthenticationTest()
        {
            Tracing.TraceMsg("Entering Base AuthenticationTest");

            FeedQuery query = new FeedQuery();
            Service service = new GBaseService(this.ApplicationName, this.gBaseKey);

            int iCount; 

            if (this.gBaseURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.gBaseURI);
                AtomFeed baseFeed = service.Query(query);

                
                // this should have a batch URI

                Assert.IsTrue(baseFeed.Batch != null, "This is a base Feed, it should have batch URI"); 

                ObjectModelHelper.DumpAtomObject(baseFeed,CreateDumpFileName("AuthenticationTest")); 
                iCount = baseFeed.Entries.Count; 

                String strTitle = "Dinner time" + Guid.NewGuid().ToString(); 

                if (baseFeed != null && baseFeed.Entries.Count > 0)
                {
                    // get the first entry
                    AtomEntry entry = baseFeed.Entries[0];

                    entry = ObjectModelHelper.CreateGoogleBaseEntry(1); 
                    entry.Title.Text = strTitle;

                    GBaseEntry newEntry = baseFeed.Insert(entry) as GBaseEntry; 

                    newEntry.PublishingPriority = new PublishingPriority("high");

                    GBaseEntry updatedEntry = newEntry.Update() as GBaseEntry;

                    //  publishing priority does not seem to be echoed back
                    // Assert.IsTrue(updatedEntry.PublishingPriority.Value == "high");
                    iCount++; 
                    Tracing.TraceMsg("Created google base  entry");

                    // try to get just that guy.....
                    FeedQuery singleQuery = new FeedQuery();
                    singleQuery.Uri = new Uri(newEntry.SelfUri.ToString()); 

                    AtomFeed newFeed = service.Query(singleQuery);

                    AtomEntry sameGuy = newFeed.Entries[0]; 

                    Assert.IsTrue(sameGuy.Title.Text.Equals(newEntry.Title.Text), "both titles should be identical"); 

                }

                baseFeed = service.Query(query);


                if (baseFeed != null && baseFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (AtomEntry entry in baseFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            entry.Content.Content = "Maybe stay until breakfast";
                            entry.Content.Type = "text";
                            entry.Update();
                            Tracing.TraceMsg("Updated entry");
                        }
                    }
                }

                baseFeed = service.Query(query);

                if (baseFeed != null && baseFeed.Entries.Count > 0)
                {
                    // look for the one with dinner time...
                    foreach (AtomEntry entry in baseFeed.Entries)
                    {
                        Tracing.TraceMsg("Entrie title: " + entry.Title.Text); 
                        if (String.Compare(entry.Title.Text, strTitle)==0)
                        {
                            entry.Delete();
                            iCount--; 
                            Tracing.TraceMsg("deleted entry");
                        }
                    }
                }
                baseFeed = service.Query(query);
                service.Credentials = null; 
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a batch upload test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] 
        public void GoogleBaseBatchInsert()
        {
            Tracing.TraceMsg("Entering GoogleBaseBatchUpload");

            FeedQuery query = new FeedQuery();
            Service service = new GBaseService(this.ApplicationName, this.gBaseKey);


            if (this.gBaseURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 



                query.Uri = new Uri(this.gBaseURI);
                AtomFeed baseFeed = service.Query(query);

                // this should have a batch URI

                Assert.IsTrue(baseFeed.Batch != null, "This is a base Feed, it should have batch URI"); 

                AtomFeed batchFeed = new AtomFeed(new Uri(this.gBaseURI), service);

                // set the default operation. Unneeded, as the default is insert, 
                // but want to make sure the code is complete
                batchFeed.BatchData = new GDataBatchFeedData();
                batchFeed.BatchData.Type = GDataBatchOperationType.delete; 

      

                for (int i=0; i<20; i++)
                {
                    AtomEntry entry = ObjectModelHelper.CreateGoogleBaseEntry(i);
                    entry.BatchData = new GDataBatchEntryData();

                    entry.BatchData.Type = GDataBatchOperationType.insert; 
                    entry.BatchData.Id = i.ToString();
                 
                    batchFeed.Entries.Add(entry); 
                }

                AtomFeed resultFeed = service.Batch(batchFeed, new Uri(baseFeed.Batch));

                foreach (AtomEntry resultEntry in resultFeed.Entries )
                {
                    GDataBatchEntryData data = resultEntry.BatchData;
                    Assert.IsTrue(data.Status.Code == 201, "Status code should be 201, is:" + data.Status.Code);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a batch upload test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GoogleBaseBatchDelete()
        {
            Tracing.TraceMsg("Entering GoogleBaseBatchDelete");

            FeedQuery query = new FeedQuery();
            Service service = new GBaseService(this.ApplicationName, this.gBaseKey);


            if (this.gBaseURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.gBaseURI);
                AtomFeed baseFeed = service.Query(query);

                Tracing.TraceMsg("Queried");

                // this should have a batch URI
                Assert.IsTrue(baseFeed.Batch != null, "This is a base Feed, it should have batch URI"); 


                AtomFeed batchFeed = new AtomFeed(new Uri(this.gBaseURI), service);

                // set the default operation. 
                batchFeed.BatchData = new GDataBatchFeedData();
                batchFeed.BatchData.Type = GDataBatchOperationType.delete; 

                Tracing.TraceMsg("Pouet ?");

                int i = 1; 
                foreach (AtomEntry entry in baseFeed.Entries)
                {
                    AtomEntry batchEntry = new AtomEntry(); 
                    batchEntry.Id = entry.Id; 

                    batchEntry.BatchData = new GDataBatchEntryData();
                    batchEntry.BatchData.Id = i.ToString(); 
                    batchEntry.BatchData.Type = GDataBatchOperationType.delete; 

                    batchFeed.Entries.Add(batchEntry); 
                    i++; 
                }
                AtomFeed resultFeed = service.Batch(batchFeed, new Uri(baseFeed.Batch));
                foreach (AtomEntry resultEntry in resultFeed.Entries )
                {
                    GDataBatchEntryData data = resultEntry.BatchData;
                    Assert.IsTrue(data.Status.Code == 200, "Status code should be 200, is:" + data.Status.Code);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a batch upload test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GoogleBaseBatchUpdates()
        {
            Tracing.TraceMsg("Entering GoogleBaseBatchUpdates");

            FeedQuery query = new FeedQuery();
            Service service = new GBaseService(this.ApplicationName, this.gBaseKey);


            if (this.gBaseURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.gBaseURI);
                AtomFeed baseFeed = service.Query(query);
                // this should have a batch URI
                Assert.IsTrue(baseFeed.Batch != null, "This is a base Feed, it should have batch URI"); 


                AtomFeed batchFeed = new AtomFeed(baseFeed);

                // set the default operation. 
                batchFeed.BatchData = new GDataBatchFeedData();
                batchFeed.BatchData.Type = GDataBatchOperationType.delete; 

                int i = 1; 
                foreach (AtomEntry entry in baseFeed.Entries)
                {
                    AtomEntry batchEntry = batchFeed.Entries.CopyOrMove(entry); 

                    batchEntry.BatchData = new GDataBatchEntryData();
                    batchEntry.BatchData.Id = i.ToString(); 
                    batchEntry.BatchData.Type = GDataBatchOperationType.update; 
                    batchEntry.Title.Text = "Updated"; 

                }





                AtomFeed resultFeed = service.Batch(batchFeed, new Uri(baseFeed.Batch));

                foreach (AtomEntry resultEntry in resultFeed.Entries )
                {
                    GDataBatchEntryData data = resultEntry.BatchData;
                    Assert.IsTrue(data.Status.Code == 200, "Status code should be 200, is:" + data.Status.Code);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a batch upload test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GoogleBaseBatchUpdateSameFeed()
        {
            Tracing.TraceMsg("Entering GoogleBaseBatchUpdateSameFeed");

            FeedQuery query = new FeedQuery();
            Service service = new GBaseService(this.ApplicationName, this.gBaseKey);


            if (this.gBaseURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.gBaseURI);
                AtomFeed baseFeed = service.Query(query);
                // this should have a batch URI
                Assert.IsTrue(baseFeed.Batch != null, "This is a base Feed, it should have batch URI"); 

                int i = 0; 

                foreach (AtomEntry entry in baseFeed.Entries)
                {
                    entry.BatchData = new GDataBatchEntryData();
                    entry.BatchData.Id = i.ToString(); 
                    entry.BatchData.Type = GDataBatchOperationType.update; 
                    entry.Title.Text = "Updated"; 
                    i++; 
                }





                AtomFeed resultFeed = service.Batch(baseFeed, new Uri(baseFeed.Batch));

                foreach (AtomEntry resultEntry in resultFeed.Entries )
                {
                    GDataBatchEntryData data = resultEntry.BatchData;
                    Assert.IsTrue(data.Status.Code == 200, "Status code should be 200, is:" + data.Status.Code);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs a batch upload test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GoogleBaseBatchMix()
        {
            Tracing.TraceMsg("Entering GoogleBaseBatchMix");

            FeedQuery query = new FeedQuery();
            Service service = new GBaseService(this.ApplicationName, this.gBaseKey);


            if (this.gBaseURI != null)
            {
                if (this.userName != null)
                {
                    service.Credentials = new GDataCredentials(this.userName, this.passWord);
                }

                service.RequestFactory = this.factory; 

                query.Uri = new Uri(this.gBaseURI);
                AtomFeed baseFeed = service.Query(query);
                // this should have a batch URI
                Assert.IsTrue(baseFeed.Batch != null, "This is a base Feed, it should have batch URI"); 

                AtomFeed batchFeed = new AtomFeed(baseFeed);

                // set the default operation. 
                batchFeed.BatchData = new GDataBatchFeedData();
                batchFeed.BatchData.Type = GDataBatchOperationType.insert; 

                int id = 1; 
                bool fUpdate = true; 
                foreach (AtomEntry entry in baseFeed.Entries)
                {
                    AtomEntry batchEntry = batchFeed.Entries.CopyOrMove(entry); 

                    if (fUpdate==true)
                    {
                        batchEntry.BatchData = new GDataBatchEntryData();
                        batchEntry.BatchData.Id = id.ToString(); 
                        batchEntry.BatchData.Type = GDataBatchOperationType.update; 
                        batchEntry.Title.Text = "Updated"; 
                        fUpdate = false;

                    }
                    else 
                    {

                        batchEntry.BatchData = new GDataBatchEntryData();
                        batchEntry.BatchData.Id = id.ToString(); 
                        batchEntry.BatchData.Type = GDataBatchOperationType.delete; 
                        fUpdate = true;
                    }


                    // insert one
                    id++; 
                    batchEntry = ObjectModelHelper.CreateGoogleBaseEntry(1);
                    batchEntry.BatchData = new GDataBatchEntryData();
                    batchEntry.BatchData.Type = GDataBatchOperationType.insert; 
                    batchEntry.BatchData.Id = id.ToString();
                    batchFeed.Entries.Add(batchEntry); 
                    id++; 
                }





                AtomFeed resultFeed = service.Batch(batchFeed, new Uri(baseFeed.Batch));

                foreach (AtomEntry resultEntry in resultFeed.Entries )
                {
                    GDataBatchEntryData data = resultEntry.BatchData;
                    int testcode = 200; 
                    if (data.Type == GDataBatchOperationType.insert)
                    {
                        testcode = 201; 
                    }
                    Assert.IsTrue(data.Status.Code == testcode, "Status code should be: " + testcode + ", is:" + data.Status.Code);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Loads a test file with an error return</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GoogleBaseErrorTest()
        {
            Tracing.TraceMsg("Entering GoogleBaseErrorTest");

            FeedQuery query = new FeedQuery();

            Uri uri = new Uri(CreateUri(this.resourcePath + "batcherror.xml"));
            query.Uri = uri; 
            Service service = new GBaseService(this.ApplicationName, this.gBaseKey);


            AtomFeed errorFeed = service.Query(query);
            foreach (AtomEntry errorEntry in errorFeed.Entries )
            {
                GDataBatchEntryData data = errorEntry.BatchData;
                if (data.Status.Code == 400)
                {
                    Assert.IsTrue(data.Status.Errors.Count == 2);
                    GDataBatchError error = data.Status.Errors[0];

                    Assert.IsTrue(error.Type == "data");
                    Assert.IsTrue(error.Field == "expiration_date");
                    Assert.IsTrue(error.Reason != null);
                    Assert.IsTrue(error.Reason.StartsWith("Invalid type specified"));

                    error = data.Status.Errors[1];

                    Assert.IsTrue(error.Type == "data");
                    Assert.IsTrue(error.Field == "price_type");
                    Assert.IsTrue(error.Reason == "Invalid price type");
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////
    } /////////////////////////////////////////////////////////////////////////////
}




