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
    [Ignore("This test assumes you have a writable, generic gData server")]
    public class WriteTestSuite : BaseTestClass
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public WriteTestSuite()
        {
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>deletes all entries in defhost feed</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void DefaultHostDeleteAll()
        {
            Tracing.TraceMsg("Entering DefaultHostDeleteAll");

            FeedQuery query = new FeedQuery();
            Service service = new Service();
            service.RequestFactory = this.factory; 
            query.Uri = new Uri(this.defaultHost);

            AtomFeed returnFeed = service.Query(query);

            foreach (AtomEntry entry in returnFeed.Entries )
            {
                entry.Delete();
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>[Test] creates a new entry, saves and loads it back</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void DefaultHostInsertOneAndDelete()
        {
            Tracing.TraceMsg("Entering DefaultHostInsertOneAndDelete");
            AtomEntry entry = ObjectModelHelper.CreateAtomEntry(1); 
            Service service = new Service();
            FeedQuery query = new FeedQuery();
            service.RequestFactory = this.factory; 

            int iCount=0; 

            string strTitle = "DefaultHostInsertOneAndDelete" + Guid.NewGuid().ToString(); 
            entry.Title.Text = strTitle; 

            query.Uri = new Uri(this.defaultHost);
            AtomFeed returnFeed = service.Query(query);

            iCount = returnFeed.Entries.Count; 


            for (int i = 0; i < this.iIterations; i++)
            {
                Tracing.TraceMsg("DefaultHostInsertOneAndDelete, iteration : " + i); 

                Stream s = service.EntrySend(new Uri(this.defaultHost), entry, GDataRequestType.Insert); 
                s.Close();

                returnFeed = service.Query(query);
                Assert.AreEqual(iCount+1, returnFeed.Entries.Count, "feed should have one more entry now"); 

                AtomEntry returnEntry = null;

                foreach (AtomEntry feedEntry in returnFeed.Entries )
                {
                    if (String.Compare(feedEntry.Title.Text, strTitle) == 0)
                    {
                        // got him
                        returnEntry = feedEntry; 
                        break;
                    }
                }

                Assert.IsTrue(returnEntry != null, "did not find the just inserted entry"); 

                returnEntry.Delete();

                // query again and check count

                returnFeed = service.Query(query);

                Assert.AreEqual(iCount, returnFeed.Entries.Count, "feed has different number of entries as expected"); 
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>creates a number or rows </summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void DefaultHostInsertAndStay()
        {
            Tracing.TraceMsg("Entering DefaultHostInsertAndStay");

            int iCount=0; 
            FeedQuery query = new FeedQuery();

            Service service = new Service();
            service.RequestFactory = this.factory; 

            query.Uri = new Uri(this.defaultHost);
            AtomFeed returnFeed = service.Query(query);
            AtomEntry entry; 


            iCount = returnFeed.Entries.Count; 

            // now we have all we need. 

            for (int i = 0; i < this.iIterations; i++)
            {
                Tracing.TraceMsg("DefaultHostInsertAndStay: inserting entry #: " + i); 
                entry = ObjectModelHelper.CreateAtomEntry(i); 
                entry = returnFeed.Insert(entry); 
            }

            Tracing.TraceMsg("DefaultHostInsertAndStay: inserted lot's of  entries");
            // done doing the inserts...

            // now query the guy again. 

            returnFeed = service.Query(query);
            Assert.AreEqual(iCount+this.iIterations, returnFeed.Entries.Count, "feed should have " + this.iIterations + " more entries now"); 

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>creates a number or rows and delets them again</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void DefaultHostMassiveInsertAndDelete()
        {
            Tracing.TraceMsg("Entering DefaultHostMassiveInsertAndDelete");

            int iCount=0; 
            FeedQuery query = new FeedQuery();

            Service service = new Service();
            service.RequestFactory = this.factory; 

            query.Uri = new Uri(this.defaultHost);
            AtomFeed returnFeed = service.Query(query);
            AtomEntry entry; 


            iCount = returnFeed.Entries.Count; 

            AtomEntryCollection newEntries = new AtomEntryCollection(null); 


            // now we have all we need. 

            for (int i = 0; i < this.iIterations; i++)
            {
                entry = ObjectModelHelper.CreateAtomEntry(i); 
                entry = returnFeed.Insert(entry); 
                newEntries.Add(entry);
            }

            Tracing.TraceMsg("DefaultHostMassiveInsert: inserted lot's of  entries");
            // done doing the inserts...

            // now query the guy again. 

            returnFeed = service.Query(query);
            Assert.AreEqual(iCount+this.iIterations, returnFeed.Entries.Count, "feed should have " + this.iIterations + " more entries now"); 

            // now udpate the 100 entries we have added

            for (int i = 0; i < this.iIterations; i++)
            {
                entry = newEntries[i];
                entry.Title.Text = Guid.NewGuid().ToString(); 
                entry.Update(); 
            }
            Tracing.TraceMsg("DefaultHostMassiveInsert: updated lot's of entries");

            returnFeed = service.Query(query);
            Assert.AreEqual(iCount+this.iIterations, returnFeed.Entries.Count, "feed should have " + this.iIterations + " more entries now"); 

            // let's find them and delete them...
            for (int i = 0; i < this.iIterations; i++)
            {
                entry = newEntries[i];
                foreach (AtomEntry feedEntry in returnFeed.Entries )
                {
                    if (String.Compare(feedEntry.Title.Text, entry.Title.Text) == 0)
                    {
                        // got him
                        Tracing.TraceMsg("trying to delete entry: " + feedEntry.Title.Text +" = " + entry.Title.Text);
                        feedEntry.Delete(); 
                        break;
                    }
                }
            }


            // and a last time
            returnFeed = service.Query(query);
            Assert.AreEqual(iCount, returnFeed.Entries.Count, "feed should have the same number again"); 

            Tracing.TraceMsg("DefaultHostMassiveInsertAndDelete: deleted lot's of entries");

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>creates X rows and updates it</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void DefaultHostMassiveInsertAndUpdate()
        {
            Tracing.TraceMsg("Entering DefaultHostMassiveInsertAndUpdate");

            int iCount=0; 
            FeedQuery query = new FeedQuery();

            Service service = new Service();
            service.RequestFactory = this.factory; 

            query.Uri = new Uri(this.defaultHost);
            AtomFeed returnFeed = service.Query(query);
            AtomEntry entry; 


            iCount = returnFeed.Entries.Count; 


            // now we have all we need. 
            int z = 0; 

            for (int i = 0; i < this.iIterations; i++)
            {
                z++; 
                if (z > 500)
                {
                    z = 0; 
                    // do a requery every hundreth to see mem usage
                    Tracing.TraceMsg("Query at point: " + i); 
                    returnFeed = service.Query(query); 

                }
                Tracing.TraceMsg("Inserting entry: " + i); 
                entry = ObjectModelHelper.CreateAtomEntry(i); 
                entry = returnFeed.Insert(entry); 
                entry.Content.Content = "Updated entry: " + Guid.NewGuid().ToString(); 
                entry.Update();
            }

            // now query the guy again. 
            returnFeed = service.Query(query);
            Assert.AreEqual(iCount+this.iIterations, returnFeed.Entries.Count, "feed should have " + this.iIterations + " more entries now"); 

            Tracing.TraceMsg("Exiting DefaultHostMassiveInsertAndUpdate");

        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>reads one external feed and inserts it locally</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void DefaultHostInsertExternalFeed()
        {
            Tracing.TraceMsg("Entering DefaultHostInsertExternalFeed");

            if (this.strRemoteHost != null)
            {
                // remove old data
                DefaultHostDeleteAll();

                FeedQuery query = new FeedQuery();
                Service service = new Service();

                service.RequestFactory  =  (IGDataRequestFactory) new GDataLoggingRequestFactory(this.ServiceName, this.ApplicationName); 
                query.Uri = new Uri(this.strRemoteHost);
                AtomFeed remoteFeed = service.Query(query);

                query.Uri = new Uri(this.defaultHost);
                AtomFeed localFeed = service.Query(query); 

                foreach (AtomEntry remoteEntry in remoteFeed.Entries)
                {
                    localFeed.Entries.Add(remoteEntry);
                    Tracing.TraceInfo("added: " + remoteEntry.Title.Text); 
                }
                bool f; 

                foreach (AtomEntry localEntry in localFeed.Entries)
                {
                    f = localEntry.IsDirty();
                    Assert.AreEqual(true, f, "This entry better be dirty now"); 
                }

                f = localFeed.IsDirty();
                Assert.AreEqual(true, f, "This feed better be dirty now"); 

                localFeed.Publish();

                foreach (AtomEntry localEntry in localFeed.Entries)
                {
                    f = localEntry.IsDirty();
                    Assert.AreEqual(false, f, "This entry better NOT be dirty now"); 
                }

                f = localFeed.IsDirty();
                Assert.AreEqual(false, f, "This feed better NOT be dirty now"); 

                // requery
                localFeed = service.Query(query); 

                foreach (AtomEntry localEntry in localFeed.Entries)
                {
                    AtomSource source = localEntry.Source; 
                    Assert.AreEqual(source.Id.Uri.ToString(), remoteFeed.Id.Uri.ToString(), "This entry better has the same source ID than the remote feed"); 
                }

            }
        }
        /////////////////////////////////////////////////////////////////////////////

  
    } /// end of WriteTestSuite
}




