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
using System.IO;
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Docs {

    public class DOCS : GDataParserNameTable {
        // Namespaces

        /// <summary>
        /// Documents List API namespace.
        /// </summary>
        public const string Ns = "http://schemas.google.com/docs/2007";

        /// <summary>
        /// Documents list XML namespace prefix.
        /// </summary>
        public const string Prefix = "docs";

        /// <summary>
        /// Revisions rel link.
        /// </summary>
        public const string Revisions = Ns + "/revisions";

        /// <summary>
        /// Parent rel link.
        /// </summary>
        public const string Parent = Ns + "#parent";

        // Elements

        /// <summary>
        /// docs:writersCanInvite element name.
        /// </summary>
        public const string WritersCanInviteElement = "writersCanInvite";

        /// <summary>
        /// docs:importFormat element name.
        /// </summary>
        public const string ImportFormatElement = "importFormat";

        /// <summary>
        /// docs:exportFormat element name.
        /// </summary>
        public const string ExportFormatElement = "exportFormat";
        public const string FeatureElement = "feature";
        public const string FeatureNameElement = "featureName";
        public const string FeatureRateElement = "featureRate";
        public const string MaxUploadSizeElement = "maxUploadSize";
        public const string AdditionalRoleInfoElement = "additionalRoleInfo";
        public const string AdditionalRoleSetElement = "additionalRoleSet";
        public const string AdditionalRoleElement = "additionalRole";
        public const string ContentElement = "content";
        public const string PublishAutoElement = "publishAuto";
        public const string PublishElement = "publish";
        public const string PublishOutsideDomainElement = "publishOutsideDomain";
        public const string ArchiveNotifyElement = "archiveNotify";
        public const string ArchiveStatusElement = "archiveStatus";
        public const string ArchiveResourceIdElement = "archiveresourceId";
        public const string ArchiveConversionElement = "archiveconversion";
        public const string Changestamp = "changestamp";
        public const string LargestChangestamp = "largestChangestamp";
        public const string Removed = "removed";
        public const string ArchiveComplete = "archiveComplete";
        public const string ArchiveTotal = "archiveTotal";
        public const string ArchiveFailure = "archiveFailure";
        public const string ArchiveTotalComplete = "archiveTotalComplete";
        public const string ArchiveTotalFailure = "archiveTotalFailure";
        public const string ArchiveNotifyStatus = "archiveNotifyStatus";

        // Resource Type Categories

        public static AtomUri KindTerm = new AtomUri(BaseNameTable.gKind);
        public static AtomUri LabelTerm = new AtomUri(BaseNameTable.gLabels);

        /// <summary>
        /// The file label.
        /// </summary>
        public const string File = "file";

        /// <summary>
        /// The folder label.
        /// </summary>
        public const string Folder = "folder";

        /// <summary>
        /// Constant document label.
        /// </summary>
        public const string Document = "document";

        /// <summary>
        /// Constant presentation.
        /// </summary>
        public const string Presentation = "presentation";

        /// <summary>
        /// Constant spreadsheet.
        /// </summary>
        public const string Spreadsheet = "spreadsheet";

        /// <summary>
        /// Constant pdf.
        /// </summary>
        public const string Pdf = "pdf";

        /// <summary>
        /// Constant drawing.
        /// </summary>
        public const string Drawing = "drawing";

        // Resource Label Categories
        public const string Starred = "starred";
        public const string Trashed = "trashed";
        public const string Hidden = "hidden";
        public const string Viewed = "viewed";
        public const string Mine = "mine";
        public const string Private = "private";
        public const string Shared = "shared-with-domain";

        /// <summary>
        /// The types.
        /// </summary>
        private static string[] types = {
                                            File, Folder, Document, Presentation, Spreadsheet, Pdf, Drawing
                                        };

        private static string[] labels = {
                                             Starred, Trashed, Hidden, Viewed, Mine, Private, Shared
                                         };

        private static Dictionary<string, ResourceType> items;

        /// <summary>
        /// Initializes the <see cref="Google.GData.Docs.DOCS"/> class.
        /// </summary>
        static DOCS() {
            items = new Dictionary<String, ResourceType> { };
            foreach (string label in types) {
                items[label] = new ResourceType(label, DOCS.Ns, KindTerm);
            }
            
            foreach (string label in labels) {
                items[label] = new ResourceType(label, BaseNameTable.gLabels, LabelTerm);
            }
        }

        public static ResourceType ResourceType(string label) {
            return items[label];
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <returns>
        /// The category.
        /// </returns>
        /// <param name='label'>
        /// Label.
        /// </param>
        public static AtomCategory GetCategory(string label) {
            return items[label].category;
        }
    }

    /// <summary>
    /// Resource type.
    /// </summary>
    public class ResourceType {
        /// <summary>
        /// The label.
        /// </summary>
        public string label;

        /// <summary>
        /// The kind.
        /// </summary>
        public string kind;

        /// <summary>
        /// The category.
        /// </summary>
        public AtomCategory category;

        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.ResourceType"/> class.
        /// </summary>
        /// <param name='label'>
        /// Label.
        /// </param>
        /// <param name='ns'>
        /// Ns.
        /// </param>
        /// <param name='term'>
        /// Term.
        /// </param>
        public ResourceType(string label, string ns, AtomUri term) {
            this.label = label;
            this.kind = ns + "#" + label;
            this.category = new AtomCategory(this.kind, term);
        }
    }
}
