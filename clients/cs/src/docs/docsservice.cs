/* Copyright (c) 2011 Google Inc.
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

using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.AccessControl;
using Google.GData.Client.ResumableUpload;
using System.Collections.Specialized;
using System.Text;

namespace Google.GData.Docs {

    public class DocsAuthenticatorRequestFactory : ICreateHttpRequest {
        private GDataGAuthRequestFactory realFactory;
        private DocsService service;

        public DocsAuthenticatorRequestFactory(DocsService service) {
            this.realFactory = (GDataGAuthRequestFactory)service.RequestFactory;
            this.service = service;
        }

        public HttpWebRequest Create(Uri target) {
            GDataRequest req = (GDataRequest)this.realFactory.CreateRequest(
                GDataRequestType.Query, target);
            HttpWebRequest outReq = req.GetFinalizedRequest();
            return outReq;
        }
    }

    public class DocsAuthenticator : Authenticator {
        public DocsAuthenticator(DocsService service)
            : base("service name") {
            this.RequestFactory = new DocsAuthenticatorRequestFactory(service);
        }

        public override void ApplyAuthenticationToRequest(HttpWebRequest request) {
            // Ignore the base implementation that is YouTube-specific.
        }
    }

    /// <summary>
    /// Documents URI.
    /// </summary>
    public class DocsUri : UriBuilder {
        /// <summary>
        /// The API URI.
        /// </summary>
        static String API_URI = "https://docs.google.com/feeds/";

        /// <summary>
        /// The user identifier.
        /// </summary>
        public String userId = "default";

        /// <summary>
        /// The query values.
        /// </summary>
        private NameValueCollection queryValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.DocsUri"/> class.
        /// </summary>
        public DocsUri()
            : this(API_URI) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.DocsUri"/> class.
        /// </summary>
        /// <param name='uri'>
        /// URI.
        /// </param>
        public DocsUri(String uri)
            : base(uri) {
            this.queryValues = System.Web.HttpUtility.ParseQueryString(this.Query.ToString());
        }

        /// <summary>
        /// Sets the parameter.
        /// </summary>
        /// <param name='key'>
        /// Key.
        /// </param>
        /// <param name='val'>
        /// Value.
        /// </param>
        public void SetParameter(String key, String val) {
            this.queryValues.Set(key, val);
            this.UpdateQuery();
        }

        /// <summary>
        /// Adds the path.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public void AddPath(String path) {
            if (!this.Path.EndsWith("/")) {
                this.Path += "/";
            }

            char[] toTrim = { '/' };
            path = path.TrimStart(toTrim);
            this.Path = this.Path + path;
        }

        /// <summary>
        /// Adds the user identifier path.
        /// </summary>
        /// <param name='service'>
        /// Service.
        /// </param>
        public void AddUserIdPath(DocsService service) {
            if (service.xOAuthRequestorId != null) {
                AddPath(service.xOAuthRequestorId);
            } else {
                AddPath("default");
            }
        }

        /// <summary>
        /// Updates the query.
        /// </summary>
        private void UpdateQuery() {
            List<String> parts = new List<String>();
            foreach (string key in this.queryValues.AllKeys) {
                parts.Add(String.Format("{0}={1}",
                    HttpUtility.UrlEncode(key),
                    HttpUtility.UrlEncode(this.queryValues[key])));
            }
            if (parts.Count > 0) {
                this.Query = string.Join("&", parts.ToArray());
            } else {
                this.Query = "";
            }
        }

        /// <summary>
        /// Builds the Metadata URI.
        /// </summary>
        /// <returns>
        /// The URI.
        /// </returns>
        /// <param name='service'>
        /// Service.
        /// </param>
        public static DocsUri MetadataUri(DocsService service) {
            DocsUri metadataUri = new DocsUri();
            metadataUri.AddPath("metadata");
            metadataUri.AddUserIdPath(service);
            return metadataUri;
        }

        /// <summary>
        /// Builds the Changes URI.
        /// </summary>
        /// <returns>
        /// The URI.
        /// </returns>
        /// <param name='service'>
        /// Service.
        /// </param>
        public static DocsUri ChangesUri(DocsService service) {
            DocsUri docsUri = new DocsUri();
            docsUri.AddUserIdPath(service);
            docsUri.AddPath("private/changes");
            return docsUri;
        }

        /// <summary>
        /// Builds the Resources URI.
        /// </summary>
        /// <returns>
        /// The URI.
        /// </returns>
        /// <param name='service'>
        /// Service.
        /// </param>
        public static DocsUri ResourcesUri(DocsService service) {
            DocsUri docsUri = new DocsUri();
            docsUri.AddUserIdPath(service);
            docsUri.AddPath("private/full");
            return docsUri;
        }

        /// <summary>
        /// Builds the Resources URI.
        /// </summary>
        /// <returns>
        /// The URI.
        /// </returns>
        /// <param name='service'>
        /// Service.
        /// </param>
        /// <param name='showfolders'>
        /// Showfolders.
        /// </param>
        /// <param name='q'>
        /// Q.
        /// </param>
        public static DocsUri ResourcesUri(DocsService service,
            bool showfolders, string q) {
            DocsUri docsUri = ResourcesUri(service);
            if (showfolders) {
                docsUri.SetParameter("showfolders", "true");
            }
            if (q != null) {
                docsUri.SetParameter("q", q);
            }
            return docsUri;
        }

        /// <summary>
        /// Builds the Resource URI.
        /// </summary>
        /// <returns>
        /// The URI.
        /// </returns>
        /// <param name='service'>
        /// Service.
        /// </param>
        /// <param name='resourceId'>
        /// Resource identifier.
        /// </param>
        public static DocsUri ResourceUri(DocsService service, String resourceId) {
            DocsUri docsUri = ResourcesUri(service);
            docsUri.AddPath(resourceId);
            return docsUri;
        }

        /// <summary>
        /// Builds the Resource URI.
        /// </summary>
        /// <returns>
        /// The type URI.
        /// </returns>
        /// <param name='resourceType'>
        /// Resource type.
        /// </param>
        public static DocsUri ResourceTypeUri(String resourceType) {
            DocsUri docsUri = new DocsUri();
            return docsUri;
        }

        /// <summary>
        /// Creates the resource URI.
        /// </summary>
        /// <returns>
        /// The resource URI.
        /// </returns>
        /// <param name='service'>
        /// Service.
        /// </param>
        public static DocsUri CreateResourceUri(DocsService service) {
            DocsUri docsUri = new DocsUri();
            docsUri.AddPath("upload/create-session");
            docsUri.AddUserIdPath(service);
            docsUri.AddPath("private/full");
            return docsUri;
        }

        /// <summary>
        /// Creates the resource URI.
        /// </summary>
        /// <returns>
        /// The resource URI.
        /// </returns>
        /// <param name='service'>
        /// Service.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public static DocsUri CreateResourceUri(DocsService service, DocsUploadConfiguration config) {
            DocsUri baseUri = CreateResourceUri(service);
            if (!config.Convert) {
                baseUri.SetParameter("convert", "false");
            }
            return baseUri;
        }

        /// <summary>
        /// Builds the Resource type URI.
        /// </summary>
        /// <returns>
        /// The type URI.
        /// </returns>
        /// <param name='resourceType'>
        /// Resource type.
        /// </param>
        /// <param name='resourceLabel'>
        /// Resource label.
        /// </param>
        public static DocsUri ResourceTypeUri(String resourceType, String resourceLabel) {
            DocsUri docsUri = new DocsUri();
            return docsUri;
        }

        /// <summary>
        /// Builds the Upload URI.
        /// </summary>
        /// <returns>
        /// The URI.
        /// </returns>
        public static DocsUri UploadUri() {
            DocsUri docsUri = new DocsUri();
            return docsUri;
        }

        /// <summary>
        /// Builds the Upload URI.
        /// </summary>
        /// <returns>
        /// The URI.
        /// </returns>
        /// <param name='convert'>
        /// Convert.
        /// </param>
        public static DocsUri UploadUri(Boolean convert) {
            DocsUri docsUri = new DocsUri();
            docsUri.AddPath("");
            return docsUri;
        }

        /// <summary>
        /// Builds the Archive URI.
        /// </summary>
        /// <returns>
        /// The URI.
        /// </returns>
        /// <param name='service'>
        /// Service.
        /// </param>
        public static DocsUri ArchiveUri(DocsService service) {
            DocsUri docsUri = new DocsUri();
            docsUri.AddUserIdPath(service);
            docsUri.AddPath("private/archive");
            return docsUri;
        }
    }

    /// <summary>
    /// Documents upload configuration.
    /// </summary>
    public class DocsUploadConfiguration {
        public bool Convert = true;
        public bool NewRevision = false;
        public bool includeMeta = true;
        public bool includeMedia = true;
    }

    /// <summary>
    /// Documents download configuration.
    /// </summary>
    public class DocsDownloadConfiguration {
        public string exportFormat;
    }

    /// <summary>
    /// Documents service.
    /// </summary>
    /// <exception cref='ArgumentNullException'>
    /// Is thrown when an argument passed to a method is invalid because it is <see langword="null" /> .
    /// </exception>
    /// <exception cref='ArgumentException'>
    /// Is thrown when an argument passed to a method is invalid.
    /// </exception>
    public class DocsService : Service {
        /// <summary>
        /// The xOAuth requestor identifier.
        /// </summary>
        public string xOAuthRequestorId;

        private string applicationName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.DocsService"/> class.
        /// </summary>
        /// <param name='applicationName'>
        /// Application name.
        /// </param>
        public DocsService(string applicationName)
            : base(ServiceNames.Documents, applicationName) {
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewEntry);
            this.NewFeed += new ServiceEventHandler(this.OnParsedNewFeed);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.DocsService"/> class.
        /// </summary>
        /// <param name='applicationName'>
        /// Application name.
        /// </param>
        /// <param name='xOauthRequestorId'>
        /// xOAuth requestor identifier.
        /// </param>
        public DocsService(string applicationName, string xOauthRequestorId)
            : this(applicationName) {
            this.xOAuthRequestorId = xOauthRequestorId;
            this.applicationName = applicationName;
        }

        /// <summary>
        /// Event handler. Called when a new list entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected new void OnParsedNewEntry(object sender, FeedParserEventArgs e) {
            if (e == null) {
                throw new ArgumentNullException("e");
            }

            if (e.CreatingEntry) {
                if (e.Entry == null) {
                    e.Entry = new Resource();
                }
            }
        }

        /// <summary>
        /// Feed handler. Instantiates a new <code>ResourceFeed</code>.
        /// </summary>
        /// <param name="sender">the object that's sending the event</param>
        /// <param name="e"><code>ServiceEventArgs</code>, holds the feed</param>
        protected void OnParsedNewFeed(object sender, ServiceEventArgs e) {
            if (e == null) {
                throw new ArgumentNullException("e");
            }

            string feedUri = e.Uri.AbsoluteUri;
            if (feedUri.IndexOf("/revisions") > -1) {
                e.Feed = new RevisionFeed(e.Uri, e.Service);
            } else if (feedUri.IndexOf("/changes") > -1) {
                e.Feed = new ChangeFeed(e.Uri, e.Service);
            } else if (feedUri.IndexOf("/acl") > -1) {
                e.Feed = new AclFeed(e.Uri, e.Service);
            } else if (feedUri.IndexOf("/metadata") > -1) {
                e.Feed = new MetadataFeed(e.Uri, e.Service);
            } else {
                e.Feed = new ResourceFeed(e.Uri, e.Service);
            }
        }

        /// <summary>
        /// Inits the version information.
        /// </summary>
        protected override void InitVersionInformation() {
            this.ProtocolMajor = VersionDefaults.VersionThree;
        }

        /// <summary>
        /// Queries the feed.
        /// </summary>
        /// <returns>
        /// The feed.
        /// </returns>
        /// <param name='feed'>
        /// Feed.
        /// </param>
        /// <param name='feedUri'>
        /// Feed URI.
        /// </param>
        private AtomFeed QueryFeed(AtomFeed feed, Uri feedUri) {
            feed.Parse(this.Query(feedUri), AlternativeFormat.Atom);
            return feed;
        }

        /// <summary>
        /// Queries the entry.
        /// </summary>
        /// <returns>
        /// The entry.
        /// </returns>
        /// <param name='feed'>
        /// Feed.
        /// </param>
        /// <param name='feedUri'>
        /// Feed URI.
        /// </param>
        private AtomEntry QueryEntry(AtomFeed feed, Uri feedUri) {
            this.QueryFeed(feed, feedUri);
            if (feed.Entries.Count > 0) {
                return feed.Entries[0];
            }
            return null;
        }

        private Uri UploadUri(Uri originalUri, DocsUploadConfiguration config) {
            DocsUri uri = new DocsUri(originalUri.ToString());
            if (config.NewRevision) {
                uri.SetParameter("new-revision", "true");
            }
            if (!config.Convert) {
                uri.SetParameter("convert", "false");
            }
            return uri.Uri;
        }

        /// <summary>
        /// Uploads the file with meta.
        /// </summary>
        /// <returns>
        /// The file with meta.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='createUri'>
        /// Create URI.
        /// </param>
        /// <param name='httpMethod'>
        /// Http method.
        /// </param>
        public Resource UploadWithMeta(Resource resource,
            Uri createUri, string httpMethod, DocsUploadConfiguration config) {
            WebResponse r = null;
            createUri = this.UploadUri(createUri, config);
            ResumableUploader uploader = new ResumableUploader();
            DocsAuthenticator authenticator = new DocsAuthenticator(this);
            Uri resumeUri = uploader.InitiateUpload(createUri, authenticator, resource, httpMethod);
            using (Stream s = resource.MediaSource.GetDataStream()) {
                r = uploader.UploadStream(HttpMethods.Put, resumeUri, authenticator, s, resource.MediaSource.ContentType, null);
            }

            if (r == null) {
                return null;
            }
            
            ResourceFeed feed = new ResourceFeed(createUri, this);
            feed.Parse(r.GetResponseStream(), AlternativeFormat.Atom);
            if (feed.Entries.Count > 0) {
                return feed.Entries[0] as Resource;
            }
            return null;
        }

        /// <summary>
        /// Uploads the file without meta.
        /// </summary>
        /// <returns>
        /// The file without meta.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='createUri'>
        /// Create URI.
        /// </param>
        /// <param name='httpMethod'>
        /// Http method.
        /// </param>
        public Resource UploadWithoutMeta(Resource resource,
            Uri createUri, string httpMethod, DocsUploadConfiguration config) {
            WebResponse r = null;
            createUri = this.UploadUri(createUri, config);
            ResumableUploader uploader = new ResumableUploader();
            DocsAuthenticator authenticator = new DocsAuthenticator(this);
            Uri resumeUri = uploader.InitiateUpload(createUri, authenticator,
                resource.MediaSource.ContentType,
                resource.MediaSource.Name,
                resource.MediaSource.ContentLength);
            using (Stream s = resource.MediaSource.GetDataStream()) {
                r = uploader.UploadStream(HttpMethods.Put, resumeUri, authenticator, s, resource.MediaSource.ContentType, null);
            }

            if (r == null) {
                return null;
            }

            ResourceFeed feed = new ResourceFeed(createUri, this);
            feed.Parse(r.GetResponseStream(), AlternativeFormat.Atom);
            if (feed.Entries.Count > 0) {
                return feed.Entries[0] as Resource;
            }
            return null;
        }

        /// <summary>
        /// Gets the download URI.
        /// </summary>
        /// <returns>
        /// The download URI.
        /// </returns>
        /// <param name='baseUri'>
        /// Base URI.
        /// </param>
        /// <param name='extraParams'>
        /// Extra parameters.
        /// </param>
        protected Uri GetDownloadUri(Uri baseUri, Dictionary<string, string> extraParams) {
            DocsUri docsUri = new DocsUri(baseUri.AbsoluteUri);
            return docsUri.Uri;
        }

        /// <summary>
        /// Gets the download URI.
        /// </summary>
        /// <returns>
        /// The download URI.
        /// </returns>
        /// <param name='baseUri'>
        /// Base URI.
        /// </param>
        /// <param name='exportFormat'>
        /// Export format.
        /// </param>
        protected Uri GetDownloadUri(Uri baseUri, string exportFormat) {
            DocsUri docsUri = new DocsUri(baseUri.AbsoluteUri);
            docsUri.SetParameter("export-format", exportFormat);
            return docsUri.Uri;
        }

        /// <summary>
        /// Gets the download stream.
        /// </summary>
        /// <returns>
        /// The download stream.
        /// </returns>
        /// <param name='uri'>
        /// URI.
        /// </param>
        protected Stream GetDownloadStream(Uri uri) {
            return this.Query(uri);
        }

        /// <summary>
        /// Gets the content of the download.
        /// </summary>
        /// <returns>
        /// The download content.
        /// </returns>
        /// <param name='uri'>
        /// URI.
        /// </param>
        protected byte[] GetDownloadContent(Uri uri) {
            Stream stream = GetDownloadStream(uri);
            using (BinaryReader reader = new BinaryReader(stream)) {
                return reader.ReadBytes((int)stream.Length);
            }
        }

        /// <summary>
        /// Downloads to stream.
        /// </summary>
        /// <returns>
        /// The to stream.
        /// </returns>
        /// <param name='uri'>
        /// URI.
        /// </param>
        /// <param name='output'>
        /// Output.
        /// </param>
        public byte[] DownloadToStream(Uri uri, Stream output) {
            byte[] content = this.GetDownloadContent(uri);
            output.Write(content, 0, content.Length);
            output.Close();
            return content;
        }

        /// <summary>
        /// Downloads to file.
        /// </summary>
        /// <param name='uri'>
        /// URI.
        /// </param>
        /// <param name='filePath'>
        /// File path.
        /// </param>
        protected void DownloadToFile(Uri uri, string filePath) {
            Stream output = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            this.DownloadToStream(uri, output);
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <returns>
        /// The metadata.
        /// </returns>
        public Metadata GetMetadata() {
            Uri queryUri = DocsUri.MetadataUri(this).Uri;
            Metadata meta = (Metadata)this.Get(queryUri.AbsoluteUri);
            return meta;
        }

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <returns>
        /// The resources.
        /// </returns>
        public ResourceFeed GetResources() {
            return this.GetResources(DocsUri.ResourcesUri(this).Uri);
        }

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <returns>
        /// The resources.
        /// </returns>
        /// <param name='uri'>
        /// URI.
        /// </param>
        public ResourceFeed GetResources(Uri uri) {
            Stream stream = Query(uri);
            ResourceFeed feed = new ResourceFeed(uri, this);
            feed.Parse(stream, AlternativeFormat.Atom);
            return feed;
        }

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <returns>
        /// The resources.
        /// </returns>
        /// <param name='maxResults'>
        /// Limit.
        /// </param>
        public ResourceFeed GetResources(int maxResults) {
            DocsUri uri = DocsUri.ResourcesUri(this);
            uri.SetParameter("max-results", maxResults.ToString());
            Console.WriteLine(uri.Uri);
            return this.GetResources(uri.Uri);
        }

        /// <summary>
        /// Gets all resources.
        /// </summary>
        /// <returns>
        /// All resources.
        /// </returns>
        public Resource[] GetAllResources() {
            return this.GetAllResources(DocsUri.ResourcesUri(this).Uri).ToArray();
        }

        /// <summary>
        /// Gets all resources.
        /// </summary>
        /// <returns>
        /// All resources.
        /// </returns>
        /// <param name='uri'>
        /// URI.
        /// </param>
        public List<Resource> GetAllResources(Uri uri) {
            List<Resource> resources = new List<Resource>();
            ResourceFeed feed = this.GetResources(uri);
            resources.AddRange(feed.Entries.GetEnumerator() as IEnumerable<Resource>);
            string next = feed.NextChunk;
            while (next != null) {
                feed = this.GetResources(new Uri(next));
                resources.AddRange(feed.Entries.GetEnumerator() as IEnumerable<Resource>);
                next = feed.NextChunk;
            }
            return resources;
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        public Resource GetResource(Resource resource) {
            return this.Get(resource.SelfUri.ToString()) as Resource;
        }

        /// <summary>
        /// Creates the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        public Resource CreateResource(Resource resource) {
            return this.CreateResource(resource, new DocsUploadConfiguration());
        }

        /// <summary>
        /// Creates the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='createUri'>
        /// Create URI.
        /// </param>
        public Resource CreateResource(Resource resource, Uri createUri) {
            return this.CreateResource(resource, createUri, new DocsUploadConfiguration());
        }

        /// <summary>
        /// Creates the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='createUri'>
        /// Create URI.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public Resource CreateResource(Resource resource, Uri createUri, DocsUploadConfiguration config) {
            if (resource.MediaSource == null) {
                return CreateResourceMeta(resource, createUri, config);
            } else {
                return CreateResourceMetaMedia(resource, createUri, config);
            }
        }

        /// <summary>
        /// Creates the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public Resource CreateResource(Resource resource, DocsUploadConfiguration config) {
            if (resource.MediaSource == null) {
                return CreateResourceMeta(resource, config);
            } else {
                return CreateResourceMetaMedia(resource, config);
            }
        }

        /// <summary>
        /// Creates the resource meta.
        /// </summary>
        /// <returns>
        /// The resource meta.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public Resource CreateResourceMeta(Resource resource, DocsUploadConfiguration config) {
            Uri createUri = DocsUri.ResourcesUri(this).Uri;
            return this.CreateResourceMeta(resource, createUri, config);
        }

        /// <summary>
        /// Creates the resource meta.
        /// </summary>
        /// <returns>
        /// The resource meta.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='createUri'>
        /// Create URI.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public Resource CreateResourceMeta(Resource resource, Uri createUri, DocsUploadConfiguration config) {
            return this.Insert(createUri, resource);
        }

        /// <summary>
        /// Creates the resource meta media.
        /// </summary>
        /// <returns>
        /// The resource meta media.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public Resource CreateResourceMetaMedia(Resource resource, DocsUploadConfiguration config) {
            Uri createUri = DocsUri.CreateResourceUri(this).Uri;
            return this.CreateResourceMetaMedia(resource, createUri, config);
        }

        /// <summary>
        /// Creates the resource meta media.
        /// </summary>
        /// <returns>
        /// The resource meta media.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='createUri'>
        /// Create URI.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public Resource CreateResourceMetaMedia(Resource resource, Uri createUri, DocsUploadConfiguration config) {
            return this.UploadWithMeta(resource, createUri, HttpMethods.Post, config);
        }

        /// <summary>
        /// Updates the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        /// <exception cref='ArgumentException'>
        /// Is thrown when an argument passed to a method is invalid.
        /// </exception>
        public Resource UpdateResource(
            Resource resource,
            DocsUploadConfiguration config) {
            if (config.includeMedia && resource.MediaSource != null) {
                if (config.includeMeta) {
                    return this.UpdateResourceMetaMedia(resource, config);
                } else {
                    return this.UpdateResourceMeta(resource, config);
                }
            } else if (config.includeMeta) {
                return this.UpdateResourceMeta(resource, config);
            } else {
                throw new ArgumentException("Nothing to update!");
            }
        }

        /// <summary>
        /// Updates the resource meta media.
        /// </summary>
        /// <returns>
        /// The resource meta media.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public Resource UpdateResourceMetaMedia(Resource resource, DocsUploadConfiguration config) {
            return this.UploadWithMeta(resource, resource.ResumableEditUri, HttpMethods.Put, config);
        }

        /// <summary>
        /// Updates the resource media.
        /// </summary>
        /// <returns>
        /// The resource media.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public Resource UpdateResourceMedia(Resource resource, DocsUploadConfiguration config) {
            return this.UploadWithoutMeta(resource, resource.ResumableEditUri, HttpMethods.Put, config);
        }

        /// <summary>
        /// Updates the resource meta.
        /// </summary>
        /// <returns>
        /// The resource meta.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public Resource UpdateResourceMeta(Resource resource, DocsUploadConfiguration config) {
            return this.Update(resource) as Resource;
        }

        /// <summary>
        /// Updates the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        public Resource UpdateResource(Resource resource) {
            return this.UpdateResource(resource, new DocsUploadConfiguration());
        }

        /// <summary>
        /// Downloads the resource.
        /// </summary>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='filePath'>
        /// File path.
        /// </param>
        public void DownloadResource(Resource resource, string filePath) {
            this.DownloadResource(resource, filePath,
                new DocsDownloadConfiguration());
        }

        /// <summary>
        /// Downloads the resource.
        /// </summary>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='filePath'>
        /// File path.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public void DownloadResource(Resource resource, string filePath,
            DocsDownloadConfiguration config) {
            this.DownloadToFile(new Uri(resource.Content.Src.ToString()), filePath);
        }

        /// <summary>
        /// Copies the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='title'>
        /// Title.
        /// </param>
        public Resource CopyResource(Resource resource, string title) {
            return this.CopyResource(resource.SelfUri.ToString(), title);
        }

        /// <summary>
        /// Copies the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='ResourceId'>
        /// Resource identifier.
        /// </param>
        /// <param name='title'>
        /// Title.
        /// </param>
        public Resource CopyResource(string ResourceId, string title) {
            Resource copyResource = new Resource();
            copyResource.Id = new AtomId(ResourceId);
            copyResource.Title.Text = title;
            return this.Insert(DocsUri.ResourcesUri(this).Uri, copyResource);
        }

        /// <summary>
        /// Moves the resource.
        /// </summary>
        /// <returns>
        /// The resource.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='collection'>
        /// Collection.
        /// </param>
        public Resource MoveResource(Resource resource, Resource collection) {
            Resource moveResource = new Resource();
            moveResource.Id = new AtomId(resource.SelfUri.ToString());
            Uri collectionUri = new Uri(collection.Content.Src.ToString());
            return this.Insert(collectionUri, moveResource);
        }

        /// <summary>
        /// Deletes the resource.
        /// </summary>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        public void DeleteResource(Resource resource) {
            Delete(resource);
        }

        /// <summary>
        /// Deletes the resource.
        /// </summary>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='permanent'>
        /// Permanent.
        /// </param>
        public void DeleteResource(Resource resource, bool permanent) {
            Delete(resource, permanent);
        }

        /// <summary>
        /// Gets the acl.
        /// </summary>
        /// <returns>
        /// The acl.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        public AclFeed GetAcl(Resource resource) {
            Uri feedUri = new Uri(resource.AclFeedLink);
            AclFeed feed = new AclFeed(feedUri, this);
            return this.QueryFeed(feed, feedUri) as AclFeed;
        }

        /// <summary>
        /// Builds the acl.
        /// </summary>
        /// <returns>
        /// The acl.
        /// </returns>
        /// <param name='role'>
        /// Role.
        /// </param>
        /// <param name='scope'>
        /// Scope.
        /// </param>
        /// <param name='scopeType'>
        /// Scope type.
        /// </param>
        public AclEntry BuildAcl(String role, String scope, String scopeType) {
            AclEntry entry = new AclEntry();
            entry.Role = new AclRole(role);
            entry.Scope = new AclScope(scope, scopeType);
            return entry;
        }

        /// <summary>
        /// Adds the acl.
        /// </summary>
        /// <returns>
        /// The acl.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='role'>
        /// Role.
        /// </param>
        /// <param name='scope'>
        /// Scope.
        /// </param>
        /// <param name='scopeType'>
        /// Scope type.
        /// </param>
        public AclEntry AddAcl(Resource resource, String role, String scope, String scopeType) {
            AclEntry acl = this.BuildAcl(role, scope, scopeType);
            return this.AddAcl(resource, acl);
        }

        /// <summary>
        /// Adds the acl.
        /// </summary>
        /// <returns>
        /// The acl.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='acl'>
        /// Acl.
        /// </param>
        public AclEntry AddAcl(Resource resource, AclEntry acl) {
            Uri aclFeedUri = new Uri(resource.AclFeedLink);
            return this.Insert(aclFeedUri, acl);
        }

        /// <summary>
        /// Gets the acl entry.
        /// </summary>
        /// <returns>
        /// The acl entry.
        /// </returns>
        /// <param name='entry'>
        /// Entry.
        /// </param>
        public AclEntry GetAclEntry(AclEntry entry) {
            return this.GetAclEntry(new Uri(entry.SelfUri.ToString()));
        }

        /// <summary>
        /// Gets the acl entry.
        /// </summary>
        /// <returns>
        /// The acl entry.
        /// </returns>
        /// <param name='selfLink'>
        /// Self link.
        /// </param>
        public AclEntry GetAclEntry(Uri selfLink) {
            AclFeed feed = new AclFeed(selfLink, this);
            return this.QueryEntry(feed, selfLink) as AclEntry;
        }

        /// <summary>
        /// Adds the acl entry.
        /// </summary>
        /// <returns>
        /// The acl entry.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='aclEntry'>
        /// Acl entry.
        /// </param>
        /// <param name='sendNotifications'>
        /// Send notifications.
        /// </param>
        public AclEntry AddAclEntry(Resource resource, AclEntry aclEntry,
          bool sendNotifications) {
            DocsUri aclUri = new DocsUri(resource.AclFeedLink);
            if (!sendNotifications) {
                aclUri.SetParameter("send-notification-emails", "false");
            }
            return Insert(aclUri.Uri, aclEntry);
        }

        /// <summary>
        /// Updates the acl entry.
        /// </summary>
        /// <returns>
        /// The acl entry.
        /// </returns>
        /// <param name='entry'>
        /// Entry.
        /// </param>
        /// <param name='sendNotifications'>
        /// Send notifications.
        /// </param>
        public AclEntry UpdateAclEntry(AclEntry entry, bool sendNotifications) {
            return Update(entry);
        }

        /// <summary>
        /// Deletes the acl entry.
        /// </summary>
        /// <param name='entry'>
        /// Entry.
        /// </param>
        public void DeleteAclEntry(AclEntry entry) {
            this.Delete(entry);
        }

        /// <summary>
        /// Batchs the process acl entries.
        /// </summary>
        /// <returns>
        /// The process acl entries.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        /// <param name='entries'>
        /// Entries.
        /// </param>
        public AclFeed BatchProcessAclEntries(Resource resource, List<AclEntry> entries) {
            AclFeed feed = new AclFeed(new Uri(resource.AclFeedLink), this);
            Uri batchUri = new Uri(resource.AclFeedLink + "/batch");
            foreach (AclEntry acl in entries) {
                feed.Entries.Add(acl);
            }
            return this.Batch(feed, batchUri) as AclFeed;
        }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <returns>
        /// The changes.
        /// </returns>
        /// <param name='feedUri'>
        /// Feed URI.
        /// </param>
        public ChangeFeed GetChanges(Uri feedUri) {
            ChangeFeed feed = new ChangeFeed(feedUri, this);
            return (ChangeFeed)this.QueryFeed(feed, feedUri);
        }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <returns>
        /// The changes.
        /// </returns>
        public ChangeFeed GetChanges() {
            DocsUri feedUri = DocsUri.ChangesUri(this);
            return this.GetChanges(feedUri.Uri);
        }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <returns>
        /// The changes.
        /// </returns>
        /// <param name='maxResults'>
        /// Max results.
        /// </param>
        public ChangeFeed GetChanges(int maxResults) {
            DocsUri feedUri = DocsUri.ChangesUri(this);
            feedUri.SetParameter("max-results", maxResults.ToString());
            return this.GetChanges(feedUri.Uri);
        }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <returns>
        /// The changes.
        /// </returns>
        /// <param name='maxResults'>
        /// Max results.
        /// </param>
        /// <param name='startIndex'>
        /// Start index.
        /// </param>
        public ChangeFeed GetChanges(int maxResults, int startIndex) {
            DocsUri feedUri = DocsUri.ChangesUri(this);
            feedUri.SetParameter("max-results", maxResults.ToString());
            feedUri.SetParameter("start-index", startIndex.ToString());
            return this.GetChanges(feedUri.Uri);
        }

        /// <summary>
        /// Gets the revisions.
        /// </summary>
        /// <returns>
        /// The revisions.
        /// </returns>
        /// <param name='resource'>
        /// Resource.
        /// </param>
        public RevisionFeed GetRevisions(Resource resource) {
            Uri feedUri = new Uri(resource.RevisionFeedLink);
            RevisionFeed feed = new RevisionFeed(feedUri, this);
            return (RevisionFeed)this.QueryFeed(feed, feedUri);
        }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        /// <returns>
        /// The revision.
        /// </returns>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        public Resource GetRevision(Revision revision) {
            return this.GetRevision(new Uri(revision.SelfUri.ToString()));
        }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        /// <returns>
        /// The revision.
        /// </returns>
        /// <param name='selfLink'>
        /// Self link.
        /// </param>
        public Resource GetRevision(Uri selfLink) {
            return this.Get(selfLink.ToString()) as Resource;
        }

        /// <summary>
        /// Downloads the revision.
        /// </summary>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        /// <param name='filePath'>
        /// File path.
        /// </param>
        public void DownloadRevision(Revision revision, string filePath) {
            this.DownloadRevision(revision, filePath,
              new DocsDownloadConfiguration());
        }

        /// <summary>
        /// Downloads the revision.
        /// </summary>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        /// <param name='filePath'>
        /// File path.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public void DownloadRevision(Revision revision, string filePath,
            DocsDownloadConfiguration config) {
            this.DownloadToFile(new Uri(revision.Content.Src.ToString()), filePath);
        }

        /// <summary>
        /// Downloads the revision stream.
        /// </summary>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        /// <param name='output'>
        /// Output.
        /// </param>
        /// <param name='config'>
        /// Config.
        /// </param>
        public void DownloadRevisionStream(Revision revision, Stream output,
            DocsDownloadConfiguration config) {
            this.DownloadToStream(new Uri(revision.Content.Src.ToString()), output);
        }

        /// <summary>
        /// Publishs the revision.
        /// </summary>
        /// <returns>
        /// The revision.
        /// </returns>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        public Revision PublishRevision(Revision revision) {
            revision.Publish = true;
            return this.UpdateRevision(revision);
        }

        /// <summary>
        /// Publishs the revision.
        /// </summary>
        /// <returns>
        /// The revision.
        /// </returns>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        /// <param name='publishAuto'>
        /// Publish auto.
        /// </param>
        public Revision PublishRevision(Revision revision, bool publishAuto) {
            revision.PublishAuto = true;
            return this.PublishRevision(revision);
        }

        /// <summary>
        /// Publishes the revision.
        /// </summary>
        /// <returns>
        /// The revision.
        /// </returns>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        /// <param name='publishAuto'>
        /// Publish auto.
        /// </param>
        /// <param name='publishOutsideDomain'>
        /// Publish outside domain.
        /// </param>
        public Revision PublishRevision(Revision revision, bool publishAuto,
            bool publishOutsideDomain) {
            revision.PublishOutsideDomain = publishOutsideDomain;
            return this.PublishRevision(revision, publishAuto);
        }

        /// <summary>
        /// Unpublishes the revision.
        /// </summary>
        /// <returns>
        /// The revision.
        /// </returns>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        public Revision UnpublishRevision(Revision revision) {
            revision.Publish = false;
            return this.UpdateRevision(revision);
        }

        /// <summary>
        /// Updates the revision.
        /// </summary>
        /// <returns>
        /// The revision.
        /// </returns>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        public Revision UpdateRevision(Revision revision) {
            return (Revision)this.Update(revision);
        }

        /// <summary>
        /// Deletes the revision.
        /// </summary>
        /// <param name='revision'>
        /// Revision.
        /// </param>
        public void DeleteRevision(Revision revision) {
            this.Delete(revision);
        }

        /// <summary>
        /// Creates the archive.
        /// </summary>
        /// <returns>
        /// The archive.
        /// </returns>
        /// <param name='archive'>
        /// Archive.
        /// </param>
        public Archive CreateArchive(Archive archive) {
            Uri createUri = DocsUri.ArchiveUri(this).Uri;
            return this.CreateArchive(archive, createUri);
        }

        /// <summary>
        /// Creates the archive.
        /// </summary>
        /// <returns>
        /// The archive.
        /// </returns>
        /// <param name='archive'>
        /// Archive.
        /// </param>
        /// <param name='createUri'>
        /// Create URI.
        /// </param>
        public Archive CreateArchive(Archive archive, Uri createUri) {
            return this.Insert(createUri, archive);
        }
    }
}
