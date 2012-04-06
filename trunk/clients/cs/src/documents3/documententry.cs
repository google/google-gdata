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

using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.AccessControl;
using System.Globalization;

namespace Google.GData.Documents {
    /// <summary>
    /// Name Table for string constants used in the Documents list api
    /// </summary>
    public class DocumentslistNametable : GDataParserNameTable {
        /// <summary>Document list namespace</summary>
        public const string NSDocumentslist = "http://schemas.google.com/docs/2007";
        /// <summary>Document list prefix</summary>
        public const string Prefix = "docs";
        /// <summary>Writers can invite element</summary>
        public const string WritersCanInvite = "writersCanInvite";
        /// <summary>Changestamp element</summary>
        public const string Changestamp = "changestamp";
        /// <summary>QuotaBytesTotal element</summary>
        public const string QuotaBytesTotal = "quotaBytesTotal";
        /// <summary>QuotaBytesUsedInTrash element</summary>
        public const string QuotaBytesUsedInTrash = "quotaBytesUsedInTrash";
        /// <summary>LargestChangestamp element</summary>
        public const string LargestChangestamp = "largestChangestamp";
        /// <summary>RemainingChangestamps element</summary>
        public const string RemainingChangestamps = "remainingChangestamps";
        /// <summary>ImportFormat element</summary>
        public const string ImportFormat = "importFormat";
        /// <summary>ExportFormat element</summary>
        public const string ExportFormat = "exportFormat";
        /// <summary>Source element</summary>
        public const string Source = "source";
        /// <summary>Target element</summary>
        public const string Target = "target";
        /// <summary>Feature element</summary>
        public const string Feature = "feature";
        /// <summary>FeatureName element</summary>
        public const string FeatureName = "featureName";
        /// <summary>FeatureRate element</summary>
        public const string FeatureRate = "featureRate";
        /// <summary>MaxUploadSize element</summary>
        public const string MaxUploadSize = "maxUploadSize";
        /// <summary>Kind element</summary>
        public const string Kind = "kind";
        /// <summary>AdditionalRoleInfo element</summary>
        public const string AdditionalRoleInfo = "additionalRoleInfo";
        /// <summary>AdditionalRoleSet element</summary>
        public const string AdditionalRoleSet = "additionalRoleSet";
        /// <summary>AdditionalRole element</summary>
        public const string AdditionalRole = "additionalRole";
        /// <summary>PrimaryRole element</summary>
        public const string PrimaryRole = "primaryRole";
    }

    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    public class DocumentEntry : AbstractEntry {
        static string PRESENTATION_KIND = DocumentsService.DocumentsNamespace + "#presentation";
        static string DOCUMENT_KIND = DocumentsService.DocumentsNamespace + "#document";
        static string SPREADSHEET_KIND = DocumentsService.DocumentsNamespace + "#spreadsheet";
        static string DRAWING_KIND = DocumentsService.DocumentsNamespace + "#drawing";
        static string PDF_KIND = DocumentsService.DocumentsNamespace + "#pdf";
        static string FOLDER_KIND = DocumentsService.DocumentsNamespace + "#folder";
        static string FORM_KIND = DocumentsService.DocumentsNamespace + "#form";
        static string PARENT_FOLDER_REL = DocumentsService.DocumentsNamespace + "#parent";

        static string STARRED_KIND = BaseNameTable.gLabels + "#starred";
        static string TRASHED_KIND = BaseNameTable.gLabels + "#trashed";
        static string HIDDEN_KIND = BaseNameTable.gLabels + "#hidden";
        static string VIEWED_KIND = BaseNameTable.gLabels + "#viewed";
        static string MINE_KIND = BaseNameTable.gLabels + "#mine";
        static string PRIVATE_KIND = BaseNameTable.gLabels + "#private";
        static string SHARED_KIND = BaseNameTable.gLabels + "#shared-with-domain";

        /// <summary>
        /// a predefined atom category for Documents
        /// </summary>
        public static AtomCategory DOCUMENT_CATEGORY =
            new AtomCategory(DOCUMENT_KIND, new AtomUri(BaseNameTable.gKind), "document");
        
        /// <summary>
        /// a predefined atom category for Spreadsheets
        /// </summary>
        public static AtomCategory SPREADSHEET_CATEGORY =
            new AtomCategory(SPREADSHEET_KIND, new AtomUri(BaseNameTable.gKind), "spreadsheet");
        
        /// <summary>
        /// a predefined atom category for PDF
        /// </summary>
        public static AtomCategory PDF_CATEGORY =
            new AtomCategory(PDF_KIND, new AtomUri(BaseNameTable.gKind), "pdf");
        
        /// <summary>
        /// a predefined atom category for Presentations
        /// </summary>
        public static AtomCategory PRESENTATION_CATEGORY =
            new AtomCategory(PRESENTATION_KIND, new AtomUri(BaseNameTable.gKind), "presentation");

        /// <summary>
        /// a predefined atom category for Drawings
        /// </summary>
        public static AtomCategory DRAWING_CATEGORY =
            new AtomCategory(DRAWING_KIND, new AtomUri(BaseNameTable.gKind), "drawing");

        /// <summary>
        /// a predefined atom category for folders
        /// </summary>        
        public static AtomCategory FOLDER_CATEGORY =
            new AtomCategory(FOLDER_KIND, new AtomUri(BaseNameTable.gKind), "folder");

        /// <summary>
        /// a predefined atom category for forms
        /// </summary>        
        public static AtomCategory FORM_CATEGORY =
            new AtomCategory(FORM_KIND, new AtomUri(BaseNameTable.gKind), "form");

        /// <summary>
        /// a predefined atom category for starred documents
        /// </summary>        
        public static AtomCategory STARRED_CATEGORY =
            new AtomCategory(STARRED_KIND, new AtomUri(BaseNameTable.gLabels), "starred");
        
        /// <summary>
        /// a predefined atom category for trashed documents
        /// </summary>        
        public static AtomCategory TRASHED_CATEGORY =
            new AtomCategory(TRASHED_KIND, new AtomUri(BaseNameTable.gLabels), "trashed");
        
        /// <summary>
        /// a predefined atom category for hidden documents
        /// </summary>        
        public static AtomCategory HIDDEN_CATEGORY =
            new AtomCategory(HIDDEN_KIND, new AtomUri(BaseNameTable.gLabels), "hidden");
        
        /// <summary>
        /// a predefined atom category for VIEWED documents
        /// </summary>        
        public static AtomCategory VIEWED_CATEGORY =
            new AtomCategory(VIEWED_KIND, new AtomUri(BaseNameTable.gLabels), "viewed");
        
        /// <summary>
        /// a predefined atom category for owned by user documents
        /// </summary>        
        public static AtomCategory MINE_CATEGORY =
            new AtomCategory(MINE_KIND, new AtomUri(BaseNameTable.gLabels), "mine");
        
        /// <summary>
        /// a predefined atom category for private documents
        /// </summary>        
        public static AtomCategory PRIVATE_CATEGORY =
            new AtomCategory(PRIVATE_KIND, new AtomUri(BaseNameTable.gLabels), "private");
        
        /// <summary>
        /// a predefined atom category for shared documents
        /// </summary>        
        public static AtomCategory SHARED_CATEGORY =
            new AtomCategory(SHARED_KIND, new AtomUri(BaseNameTable.gLabels), "shared-with-domain");

        /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public DocumentEntry()
            : base() {
            this.ProtocolMajor = VersionDefaults.VersionThree;

            Tracing.TraceMsg("Created DocumentEntry");
            this.AddExtension(new FeedLink());
            this.AddExtension(new ResourceId());
            this.AddExtension(new WritersCanInvite());
            this.AddExtension(new LastViewed());
            this.AddExtension(new LastModifiedBy());
            this.AddExtension(new QuotaBytesUsed());
        }

        /// <summary>
        /// add the documentslist NS
        /// </summary>
        /// <param name="writer">The XmlWrite, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer) {
            base.AddOtherNamespaces(writer);
            if (writer == null) {
                throw new ArgumentNullException("writer");
            }
            string strPrefix = writer.LookupPrefix(DocumentslistNametable.NSDocumentslist);
            if (strPrefix == null) {
                writer.WriteAttributeString("xmlns", DocumentslistNametable.Prefix, null, DocumentslistNametable.NSDocumentslist);
            }
        }

        /// <summary>
        /// Checks if this is a namespace declaration that we already added
        /// </summary>
        /// <param name="node">XmlNode to check</param>
        /// <returns>True if this node should be skipped</returns>
        protected override bool SkipNode(XmlNode node) {
            if (base.SkipNode(node)) {
                return true;
            }

            return (node.NodeType == XmlNodeType.Attribute
                && node.Name.StartsWith("xmlns")
                && String.Compare(node.Value, DocumentslistNametable.NSDocumentslist) == 0);
        }

        /// <summary>
        /// Reflects if this entry is a word processor document
        /// </summary>
        public bool IsDocument {
            get {
                return this.Categories.Contains(DocumentEntry.DOCUMENT_CATEGORY);
            }
            set {
                this.ToggleCategory(DocumentEntry.DOCUMENT_CATEGORY, value);
            }
        }

        /// <summary>
        /// Reflects if this entry is a spreadsheet document
        /// </summary>
        public bool IsSpreadsheet {
            get {
                return this.Categories.Contains(DocumentEntry.SPREADSHEET_CATEGORY);
            }
            set {
                this.ToggleCategory(DocumentEntry.SPREADSHEET_CATEGORY, value);
            }
        }

        /// <summary>
        /// Reflects if this entry is a presentation document
        /// </summary>
        public bool IsPresentation {
            get {
                return this.Categories.Contains(DocumentEntry.PRESENTATION_CATEGORY);
            }
            set {
                this.ToggleCategory(DocumentEntry.PRESENTATION_CATEGORY, value);
            }
        }

        /// <summary>
        /// Reflects if this entry is a drawing document
        /// </summary>
        public bool IsDrawing {
            get {
                return this.Categories.Contains(DocumentEntry.DRAWING_CATEGORY);
            }
            set {
                this.ToggleCategory(DocumentEntry.DRAWING_CATEGORY, value);
            }
        }

        /// <summary>
        /// Reflects if this entry is a form
        /// </summary>
        public bool IsForm {
            get {
                return this.Categories.Contains(DocumentEntry.FORM_CATEGORY);
            }
            set {
                this.ToggleCategory(DocumentEntry.FORM_CATEGORY, value);
            }
        }

        /// <summary>
        /// Reflects if this entry is a PDF document
        /// </summary>
        public bool IsPDF {
            get {
                return this.Categories.Contains(DocumentEntry.PDF_CATEGORY);
            }
            set {
                this.ToggleCategory(DocumentEntry.PDF_CATEGORY, value);
            }
        }

        /// <summary>
        /// Reflects if this entry is starred
        /// </summary>
        public bool IsStarred {
            get {
                return this.Categories.Contains(DocumentEntry.STARRED_CATEGORY);
            }
            set {
                this.ToggleCategory(DocumentEntry.STARRED_CATEGORY, value);
            }
        }

        /// <summary>
        /// returns true if this is a folder
        /// </summary>
        public bool IsFolder {
            get {
                return this.Categories.Contains(DocumentEntry.FOLDER_CATEGORY);
            }
            set {
                this.ToggleCategory(DocumentEntry.FOLDER_CATEGORY, value);
            }
        }

        /// <summary>
        /// returns the string that should represent the Uri to the access control list
        /// </summary>
        /// <returns>the value of the hret attribute for the acl feedlink, or null if not found</returns>
        public string AccessControlList {
            get {
                List<FeedLink> list = FindExtensions<FeedLink>(GDataParserNameTable.XmlFeedLinkElement,
                    BaseNameTable.gNamespace);

                foreach (FeedLink fl in list) {
                    if (fl.Rel == AclNameTable.LINK_REL_ACCESS_CONTROL_LIST) {
                        return fl.Href;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// returns the string that should represent the Uri to the revision document
        /// </summary>
        /// <returns>the value of the hret attribute for the revisons feedlink, or null if not found</returns>
        public string RevisionDocument {
            get {
                List<FeedLink> list = FindExtensions<FeedLink>(GDataParserNameTable.XmlFeedLinkElement,
                    BaseNameTable.gNamespace);

                foreach (FeedLink fl in list) {
                    if (fl.Rel == DocumentsService.Revisions) {
                        return fl.Href;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// returns the href value of the parent link releationship
        /// </summary>
        /// <returns></returns>
        public List<AtomLink> ParentFolders {
            get {
                List<AtomLink> links = this.Links.FindServiceList(PARENT_FOLDER_REL, AtomLink.ATOM_TYPE);
                return links;
            }
        }

        /// <summary>
        /// Documents resource Identifier.
        /// </summary>
        /// <returns></returns>
        public string ResourceId {
            get {
                return GetStringValue<ResourceId>(GDataParserNameTable.XmlResourceIdElement,
                    GDataParserNameTable.gNamespace);
            }
        }

        /// <summary>
        /// Identifies if a collaborator can modify the ACLs of the document
        /// </summary>
        /// <returns></returns>
        public bool WritersCanInvite {
            get {
                String v = GetStringValue<WritersCanInvite>(DocumentslistNametable.WritersCanInvite,
                    DocumentslistNametable.NSDocumentslist);
                // v can either be 1/0 or true/false
                bool ret = Utilities.XSDTrue == v;

                if (!ret) {
                    ret = "0" == v;
                }
                return ret;
            }
            set {
                String v = value ? Utilities.XSDTrue : Utilities.XSDFalse;
                SetStringValue<WritersCanInvite>(v,
                    DocumentslistNametable.WritersCanInvite,
                    DocumentslistNametable.NSDocumentslist);
            }
        }

        /// <summary>
        /// Returns the last viewed timestamp
        /// </summary>
        /// <returns></returns>
        public DateTime LastViewed {
            get {
                LastViewed e = FindExtension(GDataParserNameTable.XmlLastViewedElement, GDataParserNameTable.gNamespace) as LastViewed;
                if (e != null && e.Value != null) {
                    return DateTime.Parse(e.Value, CultureInfo.InvariantCulture);
                }
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// returns the last modifiedBy Element
        /// </summary>
        /// <returns></returns>
        public LastModifiedBy LastModified {
            get {
                return FindExtension(GDataParserNameTable.XmlLastModifiedByElement, GDataParserNameTable.gNamespace) as LastModifiedBy;
            }
        }

        /// <summary>
        /// returns the last quotabytesused Element
        /// </summary>
        /// <returns></returns>
        public QuotaBytesUsed QuotaUsed {
            get {
                return FindExtension(GDataParserNameTable.XmlQuotaBytesUsedElement, GDataParserNameTable.gNamespace) as QuotaBytesUsed;
            }
        }
    }

    /// <summary>
    /// Determines if a collaborator can modify a documents ACL
    /// </summary>
    public class WritersCanInvite : SimpleAttribute {
        /// <summary>
        /// default constructor for gd:resourceid 
        /// </summary>
        public WritersCanInvite()
            : base(DocumentslistNametable.WritersCanInvite,
                   DocumentslistNametable.Prefix,
                   DocumentslistNametable.NSDocumentslist) {
        }
    }
}

