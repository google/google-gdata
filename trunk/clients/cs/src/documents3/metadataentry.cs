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
    public class MetadataEntry : DocumentEntry {
        /// <summary>
        /// ImportFormat collection
        /// </summary>
        private ExtensionCollection<ImportFormat> importFormats;

        /// <summary>
        /// ExportFormat collection
        /// </summary>
        private ExtensionCollection<ExportFormat> exportFormats;

        /// <summary>
        /// Feature collection
        /// </summary>
        private ExtensionCollection<Feature> features;

        /// <summary>
        /// MaxUploadSize collection
        /// </summary>
        private ExtensionCollection<MaxUploadSize> maxUploadSizes;

        /// <summary>
        /// AdditionalRoleInfo collection
        /// </summary>
        private ExtensionCollection<AdditionalRoleInfo> additionalRoleInfos;

        /// <summary>
        /// Constructs a new MetadataEntry instance.
        /// </summary>
        public MetadataEntry()
            : base() {
            this.AddExtension(new QuotaBytesTotal());
            this.AddExtension(new QuotaBytesUsedInTrash());
            this.AddExtension(new LargestChangestamp());
            this.AddExtension(new RemainingChangestamps());
            this.AddExtension(new ImportFormat());
            this.AddExtension(new ExportFormat());
            this.AddExtension(new Feature());
            this.AddExtension(new MaxUploadSize());
            this.AddExtension(new AdditionalRoleInfo());
        }

        /// <summary>
        /// QuotaBytesTotal.
        /// </summary>
        /// <returns></returns>
        public ulong QuotaBytesTotal {
            get {
                return Convert.ToUInt64(GetStringValue<QuotaBytesTotal>(DocumentslistNametable.QuotaBytesTotal,
                    BaseNameTable.gNamespace));
            }
        }

        /// <summary>
        /// QuotaBytesUsed.
        /// </summary>
        /// <returns></returns>
        public ulong QuotaBytesUsed {
            get {
                return this.QuotaUsed.UnsignedLongValue;
            }
        }

        /// <summary>
        /// QuotaBytesUsedInTrash.
        /// </summary>
        /// <returns></returns>
        public ulong QuotaBytesUsedInTrash {
            get {
                return Convert.ToUInt64(GetStringValue<QuotaBytesUsedInTrash>(DocumentslistNametable.QuotaBytesUsedInTrash,
                    DocumentslistNametable.NSDocumentslist));
            }
        }

        /// <summary>
        /// LargestChangestamp.
        /// </summary>
        /// <returns></returns>
        public int LargestChangestamp {
            get {
                LargestChangestamp changestamp = FindExtension(DocumentslistNametable.LargestChangestamp, DocumentslistNametable.NSDocumentslist) as LargestChangestamp;
                if (changestamp != null) {
                    return changestamp.IntegerValue;
                }
                return 0;
            }
        }

        /// <summary>
        /// RemainingChangestamps.
        /// </summary>
        /// <returns></returns>
        public int RemainingChangestamps {
            get {
                RemainingChangestamps element = FindExtension(DocumentslistNametable.RemainingChangestamps, DocumentslistNametable.NSDocumentslist) as RemainingChangestamps;
                if (element != null) {
                    return element.IntegerValue;
                }
                return 0;
            }
        }

        /// <summary>
        /// ImportFormat collection.
        /// </summary>
        public ExtensionCollection<ImportFormat> ImportFormats {
            get {
                if (this.importFormats == null) {
                    this.importFormats = new ExtensionCollection<ImportFormat>(this);
                }
                return this.importFormats;
            }
        }

        /// <summary>
        /// ExportFormat collection.
        /// </summary>
        public ExtensionCollection<ExportFormat> ExportFormats {
            get {
                if (this.exportFormats == null) {
                    this.exportFormats = new ExtensionCollection<ExportFormat>(this);
                }
                return this.exportFormats;
            }
        }

        /// <summary>
        /// Feature collection.
        /// </summary>
        public ExtensionCollection<Feature> Features {
            get {
                if (this.features == null) {
                    this.features = new ExtensionCollection<Feature>(this);
                }
                return this.features;
            }
        }

        /// <summary>
        /// MaxUploadSize collection.
        /// </summary>
        public ExtensionCollection<MaxUploadSize> MaxUploadSizes {
            get {
                if (this.maxUploadSizes == null) {
                    this.maxUploadSizes = new ExtensionCollection<MaxUploadSize>(this);
                }
                return this.maxUploadSizes;
            }
        }

        /// <summary>
        /// AdditionalRoleInfo collection.
        /// </summary>
        public ExtensionCollection<AdditionalRoleInfo> AdditionalRoleInfos {
            get {
                if (this.additionalRoleInfos == null) {
                    this.additionalRoleInfos = new ExtensionCollection<AdditionalRoleInfo>(this);
                }
                return this.additionalRoleInfos;
            }
        }
    }

    public class QuotaBytesTotal : SimpleElement {
        /// <summary>
        /// default constructor for gd:quotaBytesTotal 
        /// </summary>
        public QuotaBytesTotal()
            : base(DocumentslistNametable.QuotaBytesTotal,
            BaseNameTable.gDataPrefix,
            BaseNameTable.gNamespace) {
        }
    }

    public class QuotaBytesUsedInTrash : SimpleElement {
        /// <summary>
        /// default constructor for docs:quotaBytesUsedInTrash 
        /// </summary>
        public QuotaBytesUsedInTrash()
            : base(DocumentslistNametable.QuotaBytesUsedInTrash,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class LargestChangestamp : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:largestChangestamp 
        /// </summary>
        public LargestChangestamp()
            : base(DocumentslistNametable.LargestChangestamp,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class RemainingChangestamps : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:remainingChangestamps
        /// </summary>
        public RemainingChangestamps()
            : base(DocumentslistNametable.RemainingChangestamps,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public abstract class ConversionFormat : SimpleElement {
        /// <summary>
        /// base class for docs:importFormat and docs:exportFormat
        /// </summary>
        public ConversionFormat(string elementName)
            : base(elementName,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }

        /// <summary>
        /// Source property accessor
        /// </summary>
        public string Source {
            get {
                return Convert.ToString(Attributes[DocumentslistNametable.Source]);
            }
        }

        /// <summary>
        /// Target property accessor
        /// </summary>
        public string Target {
            get {
                return Convert.ToString(Attributes[DocumentslistNametable.Target]);
            }
        }
    }

    public class ImportFormat : ConversionFormat {
        /// <summary>
        /// default constructor for docs:importFormat 
        /// </summary>
        public ImportFormat()
            : base(DocumentslistNametable.ImportFormat) {
        }
    }

    public class ExportFormat : ConversionFormat {
        /// <summary>
        /// default constructor for docs:emportFormat 
        /// </summary>
        public ExportFormat()
            : base(DocumentslistNametable.ExportFormat) {
        }
    }

    public class Feature : SimpleContainer {
        /// <summary>
        /// base class for docs:importFormat and docs:exportFormat
        /// </summary>
        public Feature()
            : base(DocumentslistNametable.Feature,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
            this.ExtensionFactories.Add(new FeatureName());
            this.ExtensionFactories.Add(new FeatureRate());
        }

        /// <summary>
        /// FeatureName property accessor
        /// </summary>
        public string Name {
            get {
                return GetStringValue<FeatureName>(DocumentslistNametable.FeatureName,
                    DocumentslistNametable.NSDocumentslist);
            }
        }

        /// <summary>
        /// FeatureRate property accessor
        /// </summary>
        public string Rate {
            get {
                return GetStringValue<FeatureRate>(DocumentslistNametable.FeatureRate,
                    DocumentslistNametable.NSDocumentslist);
            }
        }
    }

    public class FeatureName : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:featureName 
        /// </summary>
        public FeatureName()
            : base(DocumentslistNametable.FeatureName,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class FeatureRate : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:featureRate 
        /// </summary>
        public FeatureRate()
            : base(DocumentslistNametable.FeatureRate,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class MaxUploadSize : SimpleElement {
        /// <summary>
        /// default constructor for docs:maxUploadSize
        /// </summary>
        public MaxUploadSize()
            : base(DocumentslistNametable.MaxUploadSize,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }

        /// <summary>
        /// Kind property accessor
        /// </summary>
        public string Kind {
            get {
                return Convert.ToString(Attributes[DocumentslistNametable.Kind]);
            }
        }
    }

    public class AdditionalRoleInfo : SimpleContainer {
        /// <summary>
        /// default constructor for docs:additionalRoleInfo
        /// </summary>
        public AdditionalRoleInfo()
            : base(DocumentslistNametable.AdditionalRoleInfo,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
            this.ExtensionFactories.Add(new AdditionalRoleSet());
        }

        /// <summary>
        /// Kind property accessor
        /// </summary>
        public string Kind {
            get {
                return Convert.ToString(Attributes[DocumentslistNametable.Kind]);
            }
        }

        public AdditionalRoleSet AdditionalRoleSet {
            get {
                return FindExtension(DocumentslistNametable.AdditionalRoleSet,
                    DocumentslistNametable.NSDocumentslist) as AdditionalRoleSet;
            }
        }
    }

    public class AdditionalRoleSet : SimpleContainer {
        /// <summary>
        /// default constructor for docs:additionalRoleSet
        /// </summary>
        public AdditionalRoleSet()
            : base(DocumentslistNametable.AdditionalRoleSet,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
            this.ExtensionFactories.Add(new AdditionalRole());
        }

        /// <summary>
        /// PrimaryRole property accessor
        /// </summary>
        public string PrimaryRole {
            get {
                return Convert.ToString(Attributes[DocumentslistNametable.PrimaryRole]);
            }
        }

        public AdditionalRole AdditionalRole {
            get {
                return FindExtension(DocumentslistNametable.AdditionalRole,
                    AclNameTable.gAclNamespace) as AdditionalRole;
            }
        }
    }

    public class AdditionalRole : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:additionalRole 
        /// </summary>
        public AdditionalRole()
            : base(DocumentslistNametable.AdditionalRole,
            DocumentslistNametable.Prefix,
            AclNameTable.gAclNamespace) {
        }
    }
}

