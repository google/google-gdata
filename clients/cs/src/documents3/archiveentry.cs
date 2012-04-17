/* Copyright (c) 2012 Google Inc.
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
    public class ArchiveEntry : DocumentEntry {
        /// <summary>
        /// ArchiveResourceId collection
        /// </summary>
        private ExtensionCollection<ArchiveResourceId> archiveResourceIds;

        /// <summary>
        /// ArchiveConversion collection
        /// </summary>
        private ExtensionCollection<ArchiveConversion> archiveConversions;

        /// <summary>
        /// ArchiveFailure collection
        /// </summary>
        private ExtensionCollection<ArchiveFailure> archiveFailures;

        /// <summary>
        /// Constructs a new ArchiveEntry instance.
        /// </summary>
        public ArchiveEntry()
            : base() {
            this.AddExtension(new ArchiveResourceId());
            this.AddExtension(new ArchiveConversion());
            this.AddExtension(new ArchiveNotify());
            this.AddExtension(new ArchiveStatus());
            this.AddExtension(new ArchiveComplete());
            this.AddExtension(new ArchiveTotal());
            this.AddExtension(new ArchiveTotalComplete());
            this.AddExtension(new ArchiveTotalFailure());
            this.AddExtension(new ArchiveFailure());
            this.AddExtension(new ArchiveNotifyStatus());
        }

        public string ArchiveId {
            get {
                if (this.Id == null || this.Id.AbsoluteUri == null) {
                    return null;
                }
                string uri = this.Id.AbsoluteUri;
                return uri.Substring(uri.LastIndexOf('/') + 1);
            }
        }

        /// <summary>
        /// ArchiveResourceId collection.
        /// </summary>
        public ExtensionCollection<ArchiveResourceId> ArchiveResourceIds {
            get {
                if (this.archiveResourceIds == null) {
                    this.archiveResourceIds = new ExtensionCollection<ArchiveResourceId>(this);
                }
                return this.archiveResourceIds;
            }
        }

        /// <summary>
        /// ArchiveConversion collection.
        /// </summary>
        public ExtensionCollection<ArchiveConversion> ArchiveConversions {
            get {
                if (this.archiveConversions == null) {
                    this.archiveConversions = new ExtensionCollection<ArchiveConversion>(this);
                }
                return this.archiveConversions;
            }
        }

        /// <summary>
        /// ArchiveFailure collection.
        /// </summary>
        public ExtensionCollection<ArchiveFailure> ArchiveFailures {
            get {
                if (this.archiveFailures == null) {
                    this.archiveFailures = new ExtensionCollection<ArchiveFailure>(this);
                }
                return this.archiveFailures;
            }
        }

        /// <summary>
        /// ArchiveNotify.
        /// </summary>
        /// <returns></returns>
        public string ArchiveNotify {
            get {
                return GetStringValue<ArchiveNotify>(DocumentslistNametable.ArchiveNotify,
                    DocumentslistNametable.NSDocumentslist);
            }
            set {
                SetStringValue<ArchiveNotify>(value,
                    DocumentslistNametable.ArchiveNotify,
                    DocumentslistNametable.NSDocumentslist);
            }
        }

        /// <summary>
        /// ArchiveStatus.
        /// </summary>
        /// <returns></returns>
        public string ArchiveStatus {
            get {
                return GetStringValue<ArchiveStatus>(DocumentslistNametable.ArchiveStatus,
                    DocumentslistNametable.NSDocumentslist);
            }
        }

        /// <summary>
        /// ArchiveComplete.
        /// </summary>
        /// <returns></returns>
        public DateTime ArchiveComplete {
            get {
                string date = GetStringValue<ArchiveComplete>(DocumentslistNametable.ArchiveComplete,
                    DocumentslistNametable.NSDocumentslist);
                DateTime dt;
                if (date != null && DateTime.TryParse(date, out dt)) {
                    return dt;
                }
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// ArchiveTotal.
        /// </summary>
        /// <returns></returns>
        public int ArchiveTotal {
            get {
                ArchiveTotal element = FindExtension(DocumentslistNametable.ArchiveTotal, DocumentslistNametable.NSDocumentslist) as ArchiveTotal;
                if (element != null) {
                    return element.IntegerValue;
                }
                return 0;
            }
        }

        /// <summary>
        /// ArchiveTotalComplete.
        /// </summary>
        /// <returns></returns>
        public int ArchiveTotalComplete {
            get {
                ArchiveTotalComplete element = FindExtension(DocumentslistNametable.ArchiveTotalComplete, DocumentslistNametable.NSDocumentslist) as ArchiveTotalComplete;
                if (element != null) {
                    return element.IntegerValue;
                }
                return 0;
            }
        }

        /// <summary>
        /// ArchiveTotalFailure.
        /// </summary>
        /// <returns></returns>
        public int ArchiveTotalFailure {
            get {
                ArchiveTotalFailure element = FindExtension(DocumentslistNametable.ArchiveTotalFailure, DocumentslistNametable.NSDocumentslist) as ArchiveTotalFailure;
                if (element != null) {
                    return element.IntegerValue;
                }
                return 0;
            }
        }

        /// <summary>
        /// ArchiveNotifyStatus.
        /// </summary>
        /// <returns></returns>
        public string ArchiveNotifyStatus {
            get {
                return GetStringValue<ArchiveNotifyStatus>(DocumentslistNametable.ArchiveNotifyStatus,
                    DocumentslistNametable.NSDocumentslist);
            }
        }
    }

    public class ArchiveResourceId : SimpleElement {
        /// <summary>
        /// default constructor for docs:archiveResourceId
        /// </summary>
        public ArchiveResourceId()
            : base(DocumentslistNametable.ArchiveResourceId,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }

        public ArchiveResourceId(string value)
            : base(DocumentslistNametable.ArchiveResourceId,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist,
            value) {
        }
    }

    public class ArchiveConversion : SimpleElement {
        /// <summary>
        /// default constructor for docs:archiveConversion
        /// </summary>
        public ArchiveConversion()
            : base(DocumentslistNametable.ArchiveConversion,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }

        public ArchiveConversion(string source, string target)
            : base(DocumentslistNametable.ArchiveConversion,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
                this.Source = source;
                this.Target = target;
        }

        /// <summary>
        /// Source property accessor
        /// </summary>
        public string Source {
            get {
                return Convert.ToString(Attributes[DocumentslistNametable.Source]);
            }
            set {
                Attributes[DocumentslistNametable.Source] = value;
            }
        }

        /// <summary>
        /// Target property accessor
        /// </summary>
        public string Target {
            get {
                return Convert.ToString(Attributes[DocumentslistNametable.Target]);
            }
            set {
                Attributes[DocumentslistNametable.Target] = value;
            }
        }
    }

    public class ArchiveNotify : SimpleElement {
        /// <summary>
        /// default constructor for docs:archiveNotify
        /// </summary>
        public ArchiveNotify()
            : base(DocumentslistNametable.ArchiveNotify,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }

        public ArchiveNotify(string value)
            : base(DocumentslistNametable.ArchiveNotify,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist,
            value) {
        }
    }

    public class ArchiveStatus : SimpleElement {
        /// <summary>
        /// default constructor for docs:archiveStatus
        /// </summary>
        public ArchiveStatus()
            : base(DocumentslistNametable.ArchiveStatus,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class ArchiveComplete : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:archiveComplete
        /// </summary>
        public ArchiveComplete()
            : base(DocumentslistNametable.ArchiveComplete,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class ArchiveTotal : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:archiveTotal
        /// </summary>
        public ArchiveTotal()
            : base(DocumentslistNametable.ArchiveTotal,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class ArchiveTotalComplete : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:archiveTotalComplete
        /// </summary>
        public ArchiveTotalComplete()
            : base(DocumentslistNametable.ArchiveTotalComplete,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class ArchiveTotalFailure : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:archiveTotalFailure
        /// </summary>
        public ArchiveTotalFailure()
            : base(DocumentslistNametable.ArchiveTotalFailure,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class ArchiveFailure : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:archiveFailure
        /// </summary>
        public ArchiveFailure()
            : base(DocumentslistNametable.ArchiveFailure,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }

        public string Reason {
            get {
                return Convert.ToString(Attributes[DocumentslistNametable.Reason]);
            }
        }
    }

    public class ArchiveNotifyStatus : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:archiveNotifyStatus
        /// </summary>
        public ArchiveNotifyStatus()
            : base(DocumentslistNametable.ArchiveNotifyStatus,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }
}
