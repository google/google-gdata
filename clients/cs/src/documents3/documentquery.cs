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

using System;
using System.Xml;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using Google.GData.Client;

namespace Google.GData.Documents {
    /// <summary>
    /// A subclass of FeedQuery, to create an Documents query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// 
    /// Documents List supports the following standard GData query parameters: 
    ///     alt, author, q, start-index, max-results, updated-min, updated-max, /category
    /// For more information about the standard parameters, see the GData protocol reference document.
    /// In addition to the standard GData query parameters, the Documents List data API uses the following parameters.
    /// Parameter	Meaning	                                                
    /// title	    Specifies the search terms for the title of a document.	
    ///             This parameter used without title-exact will only submit partial queries, not exact queries.
    /// 
    /// title-exact	Specifies whether the title query should be taken as an exact string.	
    ///             Meaningless without title. Possible values are true and false.
    /// 
    /// The Documents List data API supports the following categories.
    ///     Category: Document Type	
    ///             Scheme: http://schemas.google.com/g/2005#kind	
    ///             Term: http://schemas.google.com/docs/2007#type	
    ///             Label: type	    
    ///             All documents of the corresponding type in the requesting users document list. 
    ///             Type is currently one of (document|spreadsheet|presentation)
    ///     Category: Starred Status
    ///         	Scheme: http://schemas.google.com/g/2005/labels	
    ///             Term: starred	                                    
    ///             Label: starred	    
    ///             All documents that have been starred by the requesting user
    ///     Category: Containing Folders	
    ///             Scheme: http://schemas.google.com/docs/2007/folders/user-email	
    ///             Term: folder-id	
    ///             Label: folder-name	
    ///             All documents inside the given folder for the requesting user
    ///  </summary>
    public class DocumentsListQuery : FeedQuery {
        /// <summary>
        /// document feed base URI 
        /// </summary>
        public static string documentsBaseUri = "https://docs.google.com/feeds/default/private/full";

        /// <summary>
        /// document feed base URI with ACLs
        /// </summary>
        public static string documentsAclUri = "https://docs.google.com/feeds/default/private/expandAcl";

        /// <summary>
        /// template to construct a folder URI for a folder ID
        /// </summary>
        public static string foldersUriTemplate = "https://docs.google.com/feeds/default/private/full/{0}/contents";

        /// <summary>
        /// template to get the ACLs for a resourceID
        /// </summary>
        public static string aclsUriTemplate = "https://docs.google.com/feeds/default/private/full/{0}/acl";

        /// <summary>
        /// template to get the media for a resourceID
        /// </summary>
        public static string mediaUriTemplate = "https://docs.google.com/feeds/default/media/{0}";

        /// <summary>
        /// uri to get you all folders
        /// </summary>
        public static string allFoldersUri = "https://docs.google.com/feeds/default/private/full/-/folder";

        /// <summary>
        /// template to access the changes feed
        /// </summary>
        public static string allChangesTemplate = "https://docs.google.com/feeds/{0}/private/changes";

        /// <summary>
        /// template to access the metadata feed
        /// </summary>
        public static string metadataTemplate = "https://docs.google.com/feeds/metadata/{0}";

        /// <summary>
        /// template to get a revision for a given resourceID and revisionID
        /// </summary>
        public static string revisionsUriTemplate = "https://docs.google.com/feeds/default/private/full/{0}/revisions/{1}";

        /// <summary>
        /// URI to access the archive feed
        /// </summary>
        public static string archiveUri = "https://docs.google.com/feeds/default/private/archive";

        /// <summary>
        /// predefined query category for documents
        /// </summary>
        public static QueryCategory DOCUMENTS = new QueryCategory(new AtomCategory("document"));

        /// <summary>
        /// predefined query category for spreadsheets
        /// </summary>
        public static QueryCategory SPREADSHEETS = new QueryCategory(new AtomCategory("spreadsheet"));

        /// <summary>
        /// predefined query category for presentations
        /// </summary>
        public static QueryCategory PRESENTATIONS = new QueryCategory(new AtomCategory("presentation"));

        /// <summary>
        /// predefined query category for drawings
        /// </summary>
        public static QueryCategory DRAWINGS = new QueryCategory(new AtomCategory("drawing"));

        /// <summary>
        /// predefined query category for PDFS
        /// </summary>
        public static QueryCategory PDFS = new QueryCategory(new AtomCategory("pdf"));

        /// <summary>
        /// predefined query category for Forms
        /// </summary>
        public static QueryCategory FORMS = new QueryCategory(new AtomCategory("form"));

        /// <summary>
        /// predefined query category for starred documents
        /// </summary>
        public static QueryCategory STARRED = new QueryCategory(new AtomCategory("starred"));

        /// <summary>
        /// predefined query category for starred documents
        /// </summary>
        public static QueryCategory VIEWED = new QueryCategory(new AtomCategory("viewed"));

        /// <summary>
        /// predefined query category for hidden documents
        /// </summary>
        public static QueryCategory HIDDEN = new QueryCategory(new AtomCategory("hidden"));

        /// <summary>
        /// predefined query category for trashed documents
        /// </summary>
        public static QueryCategory TRASHED = new QueryCategory(new AtomCategory("trashed"));

        /// <summary>
        /// predefined query category for user owned documents
        /// </summary>
        public static QueryCategory MINE = new QueryCategory(new AtomCategory("mine"));

        /// <summary>
        /// predefined query category for private documents
        /// </summary>
        public static QueryCategory PRIVATE = new QueryCategory(new AtomCategory("private"));

        /// <summary>
        /// predefined query category for shared documents
        /// </summary>
        public static QueryCategory SHARED = new QueryCategory(new AtomCategory("shared-with-domain"));

        //Local variable to hold the contents of a title query
        private string title;
        //Local variable to hold if the title query we are doing should be exact.
        private bool titleExact;

        private string owner;
        private string writer;
        private string reader;
        private string targetLanguage;
        private string sourceLanguage;
        private bool showFolders = false;
        private bool showDeleted = false;
        private bool includeProfileInfo = false;
        private bool ocr = false;
        private DateTime editedMin;
        private DateTime editedMax;

        /// <summary>
        /// base constructor
        /// </summary>
        public DocumentsListQuery()
            : base(documentsBaseUri) {
            this.CategoryQueriesAsParameter = true;
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public DocumentsListQuery(string queryUri)
            : base(queryUri) {
            this.CategoryQueriesAsParameter = true;
        }

        /// <summary>Doclist does not support index based paging</returns>
        [Obsolete("Index based paging is not supported on DocumentsList")]
        public override int StartIndex {
            get { return 0; }
            set { }
        }

        /// <summary>
        /// Restricts the results to only starred documents
        /// </summary>
        [CLSCompliant(false)]
        public bool Starred {
            get {
                return this.Categories.Contains(DocumentsListQuery.STARRED);
            }
            set {
                if (value) {
                    this.Categories.Add(DocumentsListQuery.STARRED);
                } else {
                    this.Categories.Remove(DocumentsListQuery.STARRED);
                }
            }
        }

        /// <summary>
        /// Restricts the results to only viewed documents
        /// </summary>
        [CLSCompliant(false)]
        public bool Viewed {
            get {
                return this.Categories.Contains(DocumentsListQuery.VIEWED);
            }
            set {
                if (value) {
                    this.Categories.Add(DocumentsListQuery.VIEWED);
                } else {
                    this.Categories.Remove(DocumentsListQuery.VIEWED);
                }
            }
        }

        /// <summary>
        /// Restricts the results to only trashed documents
        /// </summary>
        [CLSCompliant(false)]
        public bool Trashed {
            get {
                return this.Categories.Contains(DocumentsListQuery.TRASHED);
            }
            set {
                if (value) {
                    this.Categories.Add(DocumentsListQuery.TRASHED);
                } else {
                    this.Categories.Remove(DocumentsListQuery.TRASHED);
                }
            }
        }

        /// <summary>
        /// Restricts the results to only documents owned by the user
        /// </summary>
        [CLSCompliant(false)]
        public bool Mine {
            get {
                return this.Categories.Contains(DocumentsListQuery.MINE);
            }
            set {
                if (value) {
                    this.Categories.Add(DocumentsListQuery.MINE);
                } else {
                    this.Categories.Remove(DocumentsListQuery.MINE);
                }
            }
        }

        /// <summary>
        /// if true, shows folders in the result
        /// </summary>
        [CLSCompliant(false)]
        public bool ShowFolders {
            get {
                return this.showFolders;
            }
            set {
                this.showFolders = value;
            }
        }

        /// <summary>
        /// Restricts the results to only documents with titles matching a string.
        /// </summary>
        public string Title {
            get {
                return this.title;
            }
            set {
                this.title = value;
            }
        }

        /// <summary>
        /// Restricts the results to only documents matching a string provided
        /// by the Title property exactly. (No partial matches.)
        /// </summary>
        public bool TitleExact {
            get {
                return this.titleExact;
            }
            set {
                this.titleExact = value;
            }
        }

        /// <summary>
        /// Searches for documents with a specific owner. Use the email address of the owner
        /// </summary>
        public string Owner {
            get {
                return this.owner;
            }
            set {
                this.owner = value;
            }
        }
        /// <summary>
        /// Searches for documents which can be written to by specific users.
        /// Use a single email address or a comma separated list of email addresses.
        /// </summary>
        public string Writer {
            get {
                return this.writer;
            }
            set {
                this.writer = value;
            }
        }

        /// <summary>
        /// Searches for documents which can be read by specific users.
        /// Use a single email address or a comma separated list of email addresses.
        /// </summary>
        public string Reader {
            get {
                return this.reader;
            }
            set {
                this.reader = value;
            }
        }

        /// <summary>
        /// Specifies whether to attempt OCR on a .jpg, .png, of .gif upload.
        /// </summary>
        public bool Ocr {
            get {
                return this.ocr;
            }
            set {
                this.ocr = value;
            }
        }

        /// <summary>
        /// Specifies whether the query should return documents which are in the trash as well as other documents.
        /// </summary>
        public bool ShowDeleted {
            get {
                return this.showDeleted;
            }
            set {
                this.showDeleted = value;
            }
        }

        /// <summary>
        /// Specifies the language to translate a document into.
        /// </summary>
        public string TargetLanguage {
            get {
                return this.targetLanguage;
            }
            set {
                this.targetLanguage = value;
            }
        }

        /// <summary>
        /// Specifies the source langugate to translate a document from.
        /// </summary>
        public string SourceLanguage {
            get {
                return this.sourceLanguage;
            }
            set {
                this.sourceLanguage = value;
            }
        }

        /// <summary>
        /// Lower bound on the last time a document was edited by the current user.
        /// </summary> 
        public DateTime EditedMin {
            get { return this.editedMin; }
            set { this.editedMin = value; }
        }

        /// <summary>Upper bound on the last time a document was edited by the current user.</summary> 
        public DateTime EditedMax {
            get { return this.editedMax; }
            set { this.editedMax = value; }
        }

        /// <summary>
        /// Specifies whether the query should return additional profile information for the users.
        /// </summary>
        public bool IncludeProfileInfo {
            get {
                return this.includeProfileInfo;
            }
            set {
                this.includeProfileInfo = value;
            }
        }

        /// <summary>Parses custom properties out of the incoming URI</summary> 
        /// <param name="targetUri">A URI representing a query on a feed</param>
        /// <returns>returns the base uri</returns>
        protected override Uri ParseUri(Uri targetUri) {
            base.ParseUri(targetUri);
            if (targetUri != null) {
                char[] deli = { '?', '&' };

                string source = HttpUtility.UrlDecode(targetUri.Query);
                TokenCollection tokens = new TokenCollection(source, deli);
                foreach (String token in tokens) {
                    if (token.Length > 0) {
                        char[] otherDeli = { '=' };
                        String[] parameters = token.Split(otherDeli, 2);
                        switch (parameters[0]) {
                            case "title-exact":
                                this.TitleExact = bool.Parse(parameters[1]);
                                break;
                            case "title":
                                this.Title = parameters[1];
                                break;
                            case "owner":
                                this.Owner = parameters[1];
                                break;
                            case "reader":
                                this.Reader = parameters[1];
                                break;
                            case "writer":
                                this.Writer = parameters[1];
                                break;
                            case "targetLanguage":
                                this.TargetLanguage = parameters[1];
                                break;
                            case "sourceLanguage":
                                this.SourceLanguage = parameters[1];
                                break;
                            case "showfolders":
                                this.ShowFolders = bool.Parse(parameters[1]);
                                break;
                            case "ocr":
                                this.Ocr = bool.Parse(parameters[1]);
                                break;
                            case "showDeleted":
                                this.ShowDeleted = bool.Parse(parameters[1]);
                                break;
                            case "edited-min":
                                this.EditedMin = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "edited-max":
                                this.EditedMax = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "include-profile-info":
                                this.IncludeProfileInfo = bool.Parse(parameters[1]);
                                break;
                        }
                    }
                }
            }
            return this.Uri;
        }

        /// <summary>Creates the partial URI query string based on all
        /// set properties.</summary> 
        /// <returns> A string representing the query part of the URI.</returns>
        protected override string CalculateQuery(string basePath) {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path);

            paramInsertion = AppendQueryPart(this.Title, "title", paramInsertion, newPath);

            if (this.TitleExact) {
                paramInsertion = AppendQueryPart("true", "title-exact", paramInsertion, newPath);
            }

            if (this.ShowFolders) {
                paramInsertion = AppendQueryPart("true", "showfolders", paramInsertion, newPath);
            }

            if (this.Ocr) {
                paramInsertion = AppendQueryPart("true", "ocr", paramInsertion, newPath);
            }

            if (this.ShowDeleted) {
                paramInsertion = AppendQueryPart("true", "showDeleted", paramInsertion, newPath);
            }

            if (this.IncludeProfileInfo) {
                paramInsertion = AppendQueryPart("true", "include-profile-info", paramInsertion, newPath);
            }

            paramInsertion = AppendQueryPart(this.Owner, "owner", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Writer, "writer", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Reader, "reader", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.EditedMin, "edited-min", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.EditedMax, "edited-max", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.TargetLanguage, "targetLanguage", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.SourceLanguage, "sourceLanguage", paramInsertion, newPath);

            return newPath.ToString();
        }
    }

    /// <summary>
    /// a subclass setup to just retrieve all word processor documents
    /// </summary>
    public class TextDocumentQuery : DocumentsListQuery {
        /// <summary>
        /// base constructor
        /// </summary>
        public TextDocumentQuery()
            : base() {
            this.Categories.Add(DocumentsListQuery.DOCUMENTS);
        }
    }

    /// <summary>
    /// a subclass setup to just retrieve all spreadsheets
    /// </summary>
    public class SpreadsheetQuery : DocumentsListQuery {
        /// <summary>
        /// base constructor
        /// </summary>
        public SpreadsheetQuery()
            : base() {
            this.Categories.Add(DocumentsListQuery.SPREADSHEETS);
        }
    }

    /// <summary>
    /// a subclass setup to just retrieve all presentations
    /// </summary>
    public class PresentationsQuery : DocumentsListQuery {
        /// <summary>
        /// base constructor
        /// </summary>
        public PresentationsQuery()
            : base() {
            this.Categories.Add(DocumentsListQuery.PRESENTATIONS);
        }
    }

    /// <summary>
    /// a subclass setup to just retrieve all drawings
    /// </summary>
    public class DrawingsQuery : DocumentsListQuery {
        /// <summary>
        /// base constructor
        /// </summary>
        public DrawingsQuery()
            : base() {
            this.Categories.Add(DocumentsListQuery.DRAWINGS);
        }
    }

    /// <summary>
    /// a subclass setup to just retrieve all PDFs
    /// </summary>
    public class PDFsQuery : DocumentsListQuery {
        /// <summary>
        /// base constructor
        /// </summary>
        public PDFsQuery()
            : base() {
            this.Categories.Add(DocumentsListQuery.PDFS);
        }
    }

    /// <summary>
    /// a subclass setup to just retrieve all Folders
    /// </summary>
    public class FolderQuery : DocumentsListQuery {
        /// <summary>
        /// base constructor
        /// </summary>
        public FolderQuery()
            : base() {
            this.baseUri = DocumentsListQuery.allFoldersUri;
        }

        /// <summary>
        /// base constructor
        /// </summary>
        public FolderQuery(string folderId)
            : base() {
            this.baseUri = String.Format(DocumentsListQuery.foldersUriTemplate, folderId);
            this.ShowFolders = true;
        }
    }

    /// <summary>
    /// a subclass setup to retrieve all changes
    /// </summary>
    public class ChangesQuery : DocumentsListQuery {
        private int startIndex;
        private bool expandAcl;

        /// <summary>
        /// base constructor
        /// </summary>
        public ChangesQuery()
            : this("default") {
        }

        /// <summary>
        /// base constructor
        /// </summary>
        public ChangesQuery(string userId)
            : base() {
                this.baseUri = String.Format(DocumentsListQuery.allChangesTemplate, userId);
        }

        public override int StartIndex {
            get {
                return this.startIndex;
            }
            set {
                this.startIndex = value;
            }
        }

        public bool ExpandAcl {
            get {
                return this.expandAcl;
            }
            set {
                this.expandAcl = value;
            }
        }

        protected override string CalculateQuery(string basePath) {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path);

            if (this.ExpandAcl) {
                paramInsertion = AppendQueryPart("true", "expand-acl", paramInsertion, newPath);
            }

            return newPath.ToString();
        }
    }

    /// <summary>
    /// a subclass setup to retrieve information about a user account
    /// </summary>
    public class MetadataQuery : DocumentsListQuery {
        private int remainingChangestampsFirst;
        private int remainingChangestampsLimit;

        /// <summary>
        /// base constructor
        /// </summary>
        public MetadataQuery()
            : this("default") {
        }

        /// <summary>
        /// base constructor
        /// </summary>
        public MetadataQuery(string userId)
            : base() {
                this.baseUri = String.Format(DocumentsListQuery.metadataTemplate, userId);
        }

        public int RemainingChangestampsFirst {
            get {
                return this.remainingChangestampsFirst;
            }
            set {
                this.remainingChangestampsFirst = value;
            }
        }

        public int RemainingChangestampsLimit {
            get {
                return this.remainingChangestampsLimit;
            }
            set {
                this.remainingChangestampsLimit = value;
            }
        }

        protected override string CalculateQuery(string basePath) {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path);

            if (this.RemainingChangestampsFirst > 0) {
                paramInsertion = AppendQueryPart(
                    this.RemainingChangestampsFirst.ToString(),
                    "remaining-changestamps-first",
                    paramInsertion,
                    newPath);
            }

            if (this.RemainingChangestampsLimit > 0) {
                paramInsertion = AppendQueryPart(
                    this.RemainingChangestampsLimit.ToString(),
                    "remaining-changestamps-limit",
                    paramInsertion,
                    newPath);
            }

            return newPath.ToString();
        }
    }

    /// <summary>
    /// a query object used to interact with the Archive feed
    /// </summary>
    public class ArchiveQuery : DocumentsListQuery {
        /// <summary>
        /// base constructor
        /// </summary>
        public ArchiveQuery(string archiveId)
            : base() {
            this.baseUri = DocumentsListQuery.archiveUri + "/" + archiveId;
        }
    }

    /// <summary>
    /// a query object used to interact with the Revision feed
    /// </summary>
    public class RevisionQuery : DocumentsListQuery {
        /// <summary>
        /// base constructor
        /// </summary>
        public RevisionQuery(string revisionUri)
            : base() {
            this.baseUri = revisionUri;
        }
    }
}
