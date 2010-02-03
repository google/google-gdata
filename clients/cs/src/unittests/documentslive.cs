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
using System.Collections.Generic;
using NUnit.Framework;
using Google.GData.Client.UnitTests;
using Google.GData.Documents;
using Google.Documents;
using Google.AccessControl;


namespace Google.GData.Client.LiveTests
{
    [TestFixture]
    [Category("LiveTest")]
    public class DocumentsTestSuite : BaseLiveTestClass
    {
      

        //////////////////////////////////////////////////////////////////////
        /// <summary>default empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public DocumentsTestSuite()
        {
        }

   
        public override string ServiceName
        {
            get {
                return "writely"; 
            }
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>runs an authentication test</summary> 
        //////////////////////////////////////////////////////////////////////
        [Test] public void GoogleAuthenticationTest()
        {
            Tracing.TraceMsg("Entering Documents List Authentication Test");

            DocumentsListQuery query = new DocumentsListQuery();
            DocumentsService service = new DocumentsService(this.ApplicationName);
            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }
            service.RequestFactory = this.factory; 

            DocumentsFeed feed = service.Query(query) as DocumentsFeed;

            ObjectModelHelper.DumpAtomObject(feed,CreateDumpFileName("AuthenticationTest")); 
                service.Credentials = null; 
        }
        /////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Tests word processor document creation and deletion
        /// </summary>
        [Test] public void CreateDocumentTest()
        {
            //set up a text/plain file
            string tempFile = Directory.GetCurrentDirectory();
            tempFile = tempFile + Path.DirectorySeparatorChar + "docs_live_test.txt";

            //Console.WriteLine("Creating temporary document at: " + tempFile);

            using (StreamWriter sw = File.CreateText(tempFile))
            {
                sw.WriteLine("My name is Ozymandias, king of kings:");
                sw.WriteLine("Look on my works, ye mighty, and despair!");
                sw.Close();
            }
            


            DocumentsService service = new DocumentsService(this.ApplicationName);
            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }
            service.RequestFactory = this.factory;

            //pick a unique document name
            string documentTitle = "Ozy " + Guid.NewGuid().ToString();

            DocumentEntry entry = service.UploadDocument(tempFile, documentTitle);

            Assert.IsNotNull(entry, "Should get a valid entry back from the server.");
            Assert.AreEqual(documentTitle, entry.Title.Text, "Title on document should be what we provided.");
            Assert.IsTrue(entry.IsDocument, "What we uploaded should come back as a text document type.");
            Assert.IsTrue(entry.AccessControlList != null, "We should get an ACL list back");

            try
            {
                Uri uri = new Uri(entry.AccessControlList);
                Assert.IsTrue(uri.AbsoluteUri == entry.AccessControlList);
    
            } catch (Exception e)
            {
                throw e;
            }

            entry.Update();
            
            
            //try to delete the document we created
            entry.Delete();

            //clean up the file we created
            File.Delete(tempFile);
            
        }

        /// <summary>
        /// Tests spreadsheet creation and deletion
        /// </summary>
        
        [Ignore("Currently delete is broken")]
        [Test] public void CreateSpreadsheetTest()
        {
            //set up a text/csv file
            string tempFile = Directory.GetCurrentDirectory();
            tempFile = tempFile + Path.DirectorySeparatorChar + "docs_live_test.csv";

            //Console.WriteLine("Creating temporary document at: " + tempFile);

            using (StreamWriter sw = File.CreateText(tempFile))
            {
                sw.WriteLine("foo,bar,baz");
                sw.WriteLine("1,2,3");
                sw.Close();
            }



            DocumentsService service = new DocumentsService(this.ApplicationName);
            if (this.userName != null)
            {
                service.Credentials = new GDataCredentials(this.userName, this.passWord);
            }
            service.RequestFactory = this.factory;

            //pick a unique document name
            string documentTitle = "Simple " + Guid.NewGuid().ToString();

            DocumentEntry entry = service.UploadDocument(tempFile, documentTitle);

            Assert.IsNotNull(entry, "Should get a valid entry back from the server.");
            Assert.AreEqual(documentTitle, entry.Title.Text, "Title on document should be what we provided.");
            Assert.IsTrue(entry.IsSpreadsheet, "What we uploaded should come back as a spreadsheet document type.");

            //try to delete the document we created
            entry.Delete();

            //clean up the file we created
            File.Delete(tempFile);
        }


        /// <summary>
        /// tests etag refresh on a feed level
        /// </summary>
        [Ignore("Doclist updates timestamps on feeds based on access, so etags are useless here")]
        [Test] public void ModelTestFeedETagRefresh()
        {
            RequestSettings settings = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            // settings.PageSize = 15;
            DocumentsRequest r = new DocumentsRequest(settings);

            // this returns the server default answer
            Feed<Document> feed = r.GetDocuments();

            foreach (Document d in feed.Entries )
            {
                Assert.IsTrue(d != null, "We should have something");
            }

            Feed<Document> reload = r.Get(feed, FeedRequestType.Refresh);

            // now this should result in a notmodified
            try
            {
                foreach (Document d in reload.Entries )
                {
                    Assert.IsTrue(d == null, "We should not get here");
                }
            }
            catch (GDataNotModifiedException g)
            {
                Assert.IsTrue(g!=null);
            }
        }

        /// <summary>
        /// tests document download
        /// </summary>
        [Test]
        public void ModelTestSSL()
        {
            RequestSettings settings = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            settings.AutoPaging = true;
            settings.UseSSL = true;
            DocumentsRequest r = new DocumentsRequest(settings);

            // this returns the server default answer
            Feed<Document> feed = r.GetDocuments();

            foreach (Document x in feed.Entries)
            {
                Assert.IsTrue(x != null, "We should have something");

                Stream ret = r.Download(x, Document.DownloadType.pdf);
                ret.Close();
            }
        }


        /// <summary>
        /// tests document download
        /// </summary>
        [Test] public void ModelTestDocumentDownload()
        {
            RequestSettings settings = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            settings.AutoPaging = true;
            DocumentsRequest r = new DocumentsRequest(settings);

            // this returns the server default answer
            Feed<Document> feed = r.GetDocuments();

            foreach (Document x in feed.Entries )
            {
                Assert.IsTrue(x != null, "We should have something");

                Stream ret = r.Download(x, Document.DownloadType.pdf);
                ret.Close();
            }
        }


        /// <summary>
        /// tests etag refresh on an entry level
        /// </summary>
        [Test] public void ModelTestEntryETagRefresh()
        {
            RequestSettings settings = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            // settings.PageSize = 15;
            DocumentsRequest r = new DocumentsRequest(settings);

            // this returns the server default answer
            Feed<Document> feed = r.GetDocuments();

            Document d = null; 

            foreach (Document x in feed.Entries )
            {
                Assert.IsTrue(x != null, "We should have something");
                d = x;
            }

            Assert.IsTrue(d != null, "We should have something");
            
            // now this should result in a notmodified
            try
            {
                Document refresh = r.Retrieve(d);
                Assert.IsTrue(refresh == null, "we should not be here");
            }
            catch (GDataNotModifiedException g)
            {
                Assert.IsTrue(g!=null);
            }
        }

        /// <summary>
        /// tests to verify that an acl was detected
        /// </summary>
        [Test]
        public void ModelTestACLs()
        {
            RequestSettings settings = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            // settings.PageSize = 15;
            DocumentsRequest r = new DocumentsRequest(settings);

            // this returns the server default answer
            Feed<Document> feed = r.GetDocuments();

            foreach (Document x in feed.Entries)
            {
                Assert.IsTrue(x != null, "We should have something");
                Assert.IsNotNull(x.AccessControlList);

                Feed<Acl> f = r.Get<Acl>(x.AccessControlList);
                foreach (Acl a in f.Entries)
                {
                    Assert.IsNotNull(a.Role);
                    Assert.IsNotNull(a.Scope);
                    Assert.IsNotNull(a.Scope.Type);
                    Assert.IsNotNull(a.Scope.Value);
                }
            }
        }

        /// <summary>
        /// tests including acls during feed download
        /// </summary>
        [Test] public void ModelTestIncludeACLs()
        {
            RequestSettings settings = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            // settings.PageSize = 15;
            DocumentsRequest r = new DocumentsRequest(settings);
            
            r.BaseUri = DocumentsListQuery.documentsAclUri;
            
            // this returns the server default answer
            Feed<Document> feed = r.GetDocuments();
            
            foreach (Document x in feed.Entries )
            {
                Assert.IsTrue(x != null, "We should have something");
                Assert.IsNotNull(x.AccessControlList);
            }
        }


        /// <summary>
        /// tests etag refresh on an entry level
        /// </summary>
        [Test] public void ModelTestFolders()
        {
            const string testTitle = "That is a new & weird subfolder";
            const string parentTitle = "Granddaddy folder";

            string parentID;
            string folderID;

            RequestSettings settings = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            // settings.PageSize = 15;
            DocumentsRequest r = new DocumentsRequest(settings);

            Document folder = new Document();
            folder.Type = Document.DocumentType.Folder;
            folder.Title = testTitle;

            /// first create the folder
            folder = r.CreateDocument(folder);

            Assert.IsTrue(folder.Title == testTitle);

            r.Delete(folder);

            // let's create a hierarchy

            Document parent = new Document();
            parent.Type = Document.DocumentType.Folder;
            parent.Title = parentTitle;

            parent = r.CreateDocument(parent);
            parentID = parent.Id;

            // create the child

            folder = new Document();
            folder.Type = Document.DocumentType.Folder;
            folder.Title = testTitle;

            /// first create the folder
            folder = r.CreateDocument(folder);
            folderID = folder.Id;

            // now move the folder into the parent
            r.MoveDocumentTo(parent, folder);


            // now get the folder list
            Feed<Document> folders = r.GetFolders();

            int iVerify = 2; 

            List<Document> list = new List<Document>();
            foreach (Document f in folders.Entries )
            {
                list.Add(f);
            }

            
            
            bool found = false; 

            foreach (Document f in list )
            {
                Assert.IsTrue(f.Type == Document.DocumentType.Folder, "this should be a folder");
                if (f.Id == parentID)
                {
                    iVerify--;
                }
                if (f.Id == folderID)
                {
                    iVerify--;
                    
                    // let's find the guy again.
                    foreach (Document d in list)
                    {
                        if (f.ParentFolders.Contains(d.Self))
                        {
                            found = true;
                            break;
                        }
                    }
                }
            }
            Assert.IsTrue(found, "we did not find the parent folder");

            Assert.IsTrue(iVerify==0, "We should have found both folders"); 
        }



        /// <summary>
        /// tests moving a document in and out of folders
        /// </summary>
        [Test] public void ModelTestMoveDocuments()
        {
            const string folderTitle = "That is a new & weird folder";
            const string docTitle = "that's the doc";

            RequestSettings settings = new RequestSettings(this.ApplicationName, this.userName, this.passWord);
            // settings.PageSize = 15;
            DocumentsRequest r = new DocumentsRequest(settings);

            Document folder = new Document();
            folder.Type = Document.DocumentType.Folder;
            folder.Title = folderTitle;

            /// first create the folder
            folder = r.CreateDocument(folder);

            Assert.IsTrue(folder.Title == folderTitle);

            // let's create a document

            Document doc = new Document();
            doc.Type = Document.DocumentType.Document;
            doc.Title = docTitle;

            doc = r.CreateDocument(doc);

            // create the child
            r.MoveDocumentTo(folder, doc);

            // get the folder content list

            Feed<Document> children = r.GetFolderContent(folder);

            bool fFound = false;
            foreach (Document child in children.Entries )
            {
                if (child.ResourceId == doc.ResourceId)
                {
                    fFound = true;
                    break;
                }
            }
            Assert.IsTrue(fFound, "should have found the document in the folder");


        }

    
    } /////////////////////////////////////////////////////////////////////////////
}




