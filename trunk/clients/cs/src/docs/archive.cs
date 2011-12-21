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

using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Docs {
    /// <summary>
    /// Archive conversion.
    /// </summary>
    public class ArchiveConversion : SimpleElement {
        public ArchiveConversion()
            : base(DOCS.ArchiveConversionElement,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive notify.
    /// </summary>
    public class ArchiveNotify : SimpleElement {
        public ArchiveNotify()
            : base(DOCS.ArchiveNotifyElement,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive status.
    /// </summary>
    public class ArchiveStatus : SimpleElement {
        public ArchiveStatus()
            : base(DOCS.ArchiveStatusElement,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive resource identifier.
    /// </summary>
    public class ArchiveResourceId : SimpleElement {
        public ArchiveResourceId()
            : base(DOCS.ArchiveResourceIdElement,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive complete.
    /// </summary>
    public class ArchiveComplete : SimpleElement {
        public ArchiveComplete()
            : base(DOCS.ArchiveComplete,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive total.
    /// </summary>
    public class ArchiveTotal : SimpleElement {
        public ArchiveTotal()
            : base(DOCS.ArchiveTotal,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive total complete.
    /// </summary>
    public class ArchiveTotalComplete : SimpleElement {
        public ArchiveTotalComplete()
            : base(DOCS.ArchiveTotalComplete,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive total failure.
    /// </summary>
    public class ArchiveTotalFailure : SimpleElement {
        public ArchiveTotalFailure()
            : base(DOCS.ArchiveTotalFailure,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive failure.
    /// </summary>
    public class ArchiveFailure : SimpleElement {
        public ArchiveFailure()
            : base(DOCS.ArchiveFailure,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive notify status.
    /// </summary>
    public class ArchiveNotifyStatus : SimpleElement {
        public ArchiveNotifyStatus()
            : base(DOCS.ArchiveNotifyStatus,
            DOCS.Prefix, DOCS.Ns) { }
    }

    /// <summary>
    /// Archive.
    /// </summary>
    public class Archive : AbstractEntry {
        private ExtensionCollection<ArchiveResourceId> archiveResourceIds;
        private ExtensionCollection<ArchiveConversion> archiveConversions;

        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.Archive"/> class.
        /// </summary>
        public Archive()
            : base() {
            this.ProtocolMajor = VersionDefaults.VersionThree;
            this.AddExtension(new ArchiveConversion());
            this.AddExtension(new ArchiveStatus());
            this.AddExtension(new ArchiveNotify());
            this.AddExtension(new ArchiveResourceId());
            this.AddExtension(new ArchiveTotal());
            this.AddExtension(new ArchiveTotalComplete());
            this.AddExtension(new ArchiveTotalFailure());
            this.AddExtension(new ArchiveFailure());
            this.AddExtension(new ArchiveNotifyStatus());
            this.AddExtension(new QuotaBytesUsed());
        }

        /// <summary>
        /// Gets the archive resource identifiers.
        /// </summary>
        /// <value>
        /// The archive resource identifiers.
        /// </value>
        public ExtensionCollection<ArchiveResourceId> ArchiveResourceIds {
            get {
                if (this.archiveResourceIds == null) {
                    this.archiveResourceIds = new ExtensionCollection<ArchiveResourceId>();
                }
                return this.archiveResourceIds;
            }
        }

        /// <summary>
        /// Gets the archive conversions.
        /// </summary>
        /// <value>
        /// The archive conversions.
        /// </value>
        public ExtensionCollection<ArchiveConversion> ArchiveConversions {
            get {
                if (this.archiveConversions == null) {
                    this.archiveConversions = new ExtensionCollection<ArchiveConversion>();
                }
                return this.archiveConversions;
            }
        }

        /// <summary>
        /// Gets or sets the archive notify.
        /// </summary>
        /// <value>
        /// The archive notify.
        /// </value>
        public string ArchiveNotify {
            get {
                return this.GetStringValue<ArchiveNotify>(DOCS.ArchiveNotifyElement,
                    DOCS.Ns);
            }
            set {
                this.SetStringValue<ArchiveNotify>(value,
                    DOCS.ArchiveNotifyElement, DOCS.Ns);
            }
        }

        /// <summary>
        /// Gets the quota bytes used.
        /// </summary>
        /// <value>
        /// The quota bytes used.
        /// </value>
        public long QuotaBytesUsed {
            get {
                string used = QuotaBytesUsedElement.Value;
                long longValue;
                if (!long.TryParse(used, out longValue)) {
                    return 0;
                }

                return longValue;
            }
        }

        /// <summary>
        /// Gets the quota bytes used element.
        /// </summary>
        /// <value>
        /// The quota bytes used element.
        /// </value>
        public QuotaBytesUsed QuotaBytesUsedElement {
            get {
                return FindExtension(GDataParserNameTable.XmlQuotaBytesUsedElement,
                    GDataParserNameTable.gNamespace) as QuotaBytesUsed;
            }
        }
    }
}
