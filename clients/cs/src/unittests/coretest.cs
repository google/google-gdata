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
using Google.GData.Calendar;




namespace Google.GData.Client.UnitTests
{
    [TestFixture] 
    public class CoreTestSuite : BaseTestClass
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public CoreTestSuite()
        {
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>[Test] public QueryObjectTest()</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void QueryObjectTest()
        {
            Tracing.TraceInfo("Entering QueryObject Test"); 

            FeedQuery query = new FeedQuery();
            query.Uri = new Uri(this.defaultHost);

            AtomCategory aCat = new AtomCategory("Test", new AtomUri("urn:test.com")); 

            QueryCategory qCat = new QueryCategory(aCat);

            query.Categories.Add(qCat);


            aCat = new AtomCategory("TestNotAndOr", new AtomUri("urn:test.com")); 
            qCat = new QueryCategory(aCat);
            qCat.Operator = QueryCategoryOperator.OR; 
            qCat.Excluded = true; 

            query.Categories.Add(qCat);


            aCat = new AtomCategory("ANDTHISONE", new AtomUri("")); 
            qCat = new QueryCategory(aCat);
            query.Categories.Add(qCat);

            aCat = new AtomCategory("AnotherOrWithoutCategory"); 
            qCat = new QueryCategory(aCat);
            qCat.Operator = QueryCategoryOperator.OR; 
            qCat.Excluded = true; 
            query.Categories.Add(qCat);

            query.Query = "Hospital";
            query.NumberToRetrieve = 20; 
            Tracing.TraceInfo("query: "  + query.Uri);  

            Uri uri =  query.Uri; 

            Tracing.TraceInfo("Uri: query= " + uri.Query); 
            query.Uri = uri; 

            Tracing.TraceInfo("Parsed Query URI: " + query.Uri); 

            Assert.IsTrue(uri.AbsolutePath.Equals(query.Uri.AbsolutePath), "both query URIs should be identical"); 
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>[Test] creates a feed object from scratch</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test]public void CreateFeed() 
        {
            Tracing.TraceInfo("Entering Create Feed Test"); 
            AtomFeed feed = new AtomFeed(new Uri("http://dummy"), null);

            AtomEntry entry; 

            for (int i = 1; i <= this.iIterations; i++)
            {
                entry = ObjectModelHelper.CreateAtomEntry(i);
                feed.Entries.Add(entry);
            }

            Tracing.TraceInfo("now persisting feed"); 

            ObjectModelHelper.DumpAtomObject(feed, CreateDumpFileName("CreateFeed"));

            Tracing.TraceInfo("now loadiing feed from disk");

            Service service = new Service();
            service.RequestFactory = this.factory; 

            FeedQuery query = new FeedQuery();
            query.Uri = new Uri(CreateUriFileName("CreateFeed"));

            feed = service.Query(query);

            Assert.IsTrue(feed.Entries != null, "Feed.Entries should not be null");
            Assert.AreEqual(this.iIterations, feed.Entries.Count, "Feed.Entries should have 50 elements");
            if (feed.Entries != null)
            {
                for (int i = 1; i <= this.iIterations; i++)
                {
                    entry = ObjectModelHelper.CreateAtomEntry(i);
                    AtomEntry theOtherEntry = feed.Entries[i-1];
                    Assert.IsTrue(ObjectModelHelper.IsEntryIdentical(entry, theOtherEntry));
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>[Test] creates a new entry, saves and loads it back</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CreateEntrySaveAndLoad()
        {

            Tracing.TraceMsg("Entering Create/Save/Load test");

            AtomEntry entry = ObjectModelHelper.CreateAtomEntry(1);

            ObjectModelHelper.DumpAtomObject(entry, CreateDumpFileName("CreateEntrySaveAndLoad"));


            // let's try loading this... 
            Service service = new Service();
            service.RequestFactory = this.factory; 

            FeedQuery query = new FeedQuery();
            query.Uri = new Uri(CreateUriFileName("CreateEntrySaveAndLoad"));
            AtomFeed feed = service.Query(query);
            Assert.IsTrue(feed.Entries != null, "Feed.Entries should not be null");
            Assert.AreEqual(1, feed.Entries.Count, "Feed.Entries should have ONE element");
            // that feed should have ONE entry
            if (feed.Entries != null)
            {
                AtomEntry theOtherEntry = feed.Entries[0];
                Assert.IsTrue(ObjectModelHelper.IsEntryIdentical(entry, theOtherEntry));
            }

        }
        /////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////
        /// <summary>[Test] creates a new entry, saves and loads it back</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void CreateEmptyEntrySaveAndLoad()
        {

            Tracing.TraceMsg("Entering Create/Save/Load test");

            AtomEntry entry = ObjectModelHelper.CreateAtomEntry(1);

            entry.Content.Type = "text";
            entry.Content.Content = ""; 

            ObjectModelHelper.DumpAtomObject(entry, CreateDumpFileName("CreateEntrySaveAndLoad"));


            // let's try loading this... 
            Service service = new Service();
            service.RequestFactory = this.factory; 

            FeedQuery query = new FeedQuery();
            query.Uri = new Uri(CreateUriFileName("CreateEntrySaveAndLoad"));
            AtomFeed feed = service.Query(query);
            Assert.IsTrue(feed.Entries != null, "Feed.Entries should not be null");
            Assert.AreEqual(1, feed.Entries.Count, "Feed.Entries should have ONE element");
            // that feed should have ONE entry
            if (feed.Entries != null)
            {
                AtomEntry theOtherEntry = feed.Entries[0];
                Assert.IsTrue(ObjectModelHelper.IsEntryIdentical(entry, theOtherEntry), "Entries should be identical");
            }

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>[Test] queries the remote feed, saves it, loads it and compares it</summary> 
        /// <param name="uriToQuery">the host to access, including query parameters</param>
        //////////////////////////////////////////////////////////////////////
        internal void RemoteHostQueryAndCompare(Uri uriToQuery)
        {

            Tracing.TraceMsg("Entering RemoteHostQueryAndCompare");

            int iCount = 0; 
            FeedQuery query = new FeedQuery();
            query.Uri = uriToQuery; 

            Service service = new Service();
            service.RequestFactory = this.factory; 

            AtomFeed f = service.Query(query);

            ObjectModelHelper.DumpAtomObject(f, CreateDumpFileName("QueryRemoteHost"));

            iCount = f.Entries.Count;

            // let's try loading this... 
            Service service2 = new Service();
            FeedQuery query2 = new FeedQuery();
            query2.Uri = new Uri(CreateUriFileName("QueryRemoteHost"));

            AtomFeed feed = service2.Query(query);
            Assert.AreEqual(iCount, feed.Entries.Count, "loaded feed has different number of entries");


            Tracing.TraceInfo("Comparing feed objects as source"); 
            Assert.IsTrue(ObjectModelHelper.IsSourceIdentical(f, feed)); 
            if (feed.Entries != null)
            {
                AtomEntry theOtherEntry;

                Tracing.TraceInfo("Comparing Entries"); 
                for (int i = 0; i < feed.Entries.Count; i++)
                {
                    theOtherEntry = feed.Entries[i];
                    Assert.IsTrue(ObjectModelHelper.IsEntryIdentical(f.Entries[i], theOtherEntry));
                }

            }

            Tracing.TraceInfo("Leaving RemoteHostQueryAndCompare for : " + uriToQuery.AbsoluteUri); 

        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>[Test] walks over the list of remotehosts out of the 
        /// unitTestExternalHosts
        /// add key="Host1" value="http://www.franklinmint.fm/2005/09/26/test_entry2.xml" 
        /// section in the config file and queries and compares the object model
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void RemoteHostQueryTest()
        {
            Tracing.TraceInfo("Entering RemoteHostQueryTest()"); 
            if (this.externalHosts != null)
            {
                for (int i=0; i< this.iIterations; i++) 
                {
                    Tracing.TraceInfo("Having a dictionary RemoteHostQueryTest()"); 
                    foreach (DictionaryEntry de in this.externalHosts )
                    {
                        Tracing.TraceInfo("Using DictionaryEntry for external Query: " + de.Value); 
                        Uri uriToQuery = new Uri((string) de.Value); 
                        RemoteHostQueryAndCompare(uriToQuery); 
    
                    }
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>creates a number or rows and delets them again</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void DefaultHostExtensionTest()
        {
            Tracing.TraceMsg("Entering DefaultHostExtensionTest");

            FeedQuery query = new FeedQuery();
            Service service = new Service();

            service.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry); 
            service.NewExtensionElement += new ExtensionElementEventHandler(this.OnNewExtensionElement);


            service.RequestFactory  =  (IGDataRequestFactory) new GDataLoggingRequestFactory(this.strServiceName, this.strApplicationName); 

            query.Uri = new Uri(this.strRemoteHost);

            AtomFeed returnFeed = service.Query(query);

            ObjectModelHelper.DumpAtomObject(returnFeed,CreateDumpFileName("ExtensionFeed")); 
        }
        /////////////////////////////////////////////////////////////////////////////

  
    } /// end of CoreTestSuite
}




