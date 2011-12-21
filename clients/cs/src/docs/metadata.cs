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
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.AccessControl;

namespace Google.GData.Docs {
    /// <summary>
    /// Metadata feed.
    /// </summary>
    public class MetadataFeed : AbstractFeed {
        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.MetadataFeed"/> class.
        /// </summary>
        /// <param name='uriBase'>
        /// URI base.
        /// </param>
        /// <param name='iService'>
        /// IService.
        /// </param>
        public MetadataFeed(Uri uriBase, IService iService) 
            : base(uriBase, iService) { }

        /// <summary>
        /// Creates the feed entry.
        /// </summary>
        /// <returns>
        /// The feed entry.
        /// </returns>
        public override AtomEntry CreateFeedEntry() {
            return new Metadata();
        }
    }

    /// <summary>
    /// docs:importFormat element.
    /// </summary>
    public class ImportFormat : SimpleElement {
        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.ImportFormat"/> class.
        /// </summary>
        public ImportFormat()
            : base(
            DOCS.ImportFormatElement,
            DOCS.Prefix,
            DOCS.Ns) { }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source {
            get {
                return this.Attributes["source"].ToString();
            }
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public string Target {
            get {
                return this.Attributes["target"].ToString();
            }
        }
    }

    /// <summary>
    /// docs:exportFormat element.
    /// </summary>
    public class ExportFormat : SimpleElement {
        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.ExportFormat"/> class.
        /// </summary>
        public ExportFormat()
            : base(
            DOCS.ExportFormatElement,
            DOCS.Prefix,
            DOCS.Ns) { }

        /// <summary>
        /// Source type of export format.
        /// </summary>
        public string Source {
            get {
                return this.Attributes["source"].ToString();
            }
        }

        /// <summary>
        /// Target type of export format.
        /// </summary>
        public string Target {
            get {
                return this.Attributes["target"].ToString();
            }
        }
    }

    /// <summary>
    /// docs:featureName element.
    /// </summary>
    public class FeatureName : SimpleElement {
        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.FeatureName"/> class.
        /// </summary>
        public FeatureName()
            : base(
            DOCS.FeatureNameElement,
            DOCS.Prefix,
            DOCS.Ns) { }
    }

    /// <summary>
    /// docs:featureRate element.
    /// </summary>
    public class FeatureRate : SimpleElement {
        public FeatureRate()
            : base(
            DOCS.FeatureRateElement,
            DOCS.Prefix,
            DOCS.Ns) { }
    }

    /// <summary>
    /// docs:feature element.
    /// </summary>
    public class Feature : SimpleContainer {
        public Feature()
            : base(DOCS.FeatureElement,
            DOCS.Prefix,
            DOCS.Ns) {
            this.ExtensionFactories.Add(new FeatureName());
            this.ExtensionFactories.Add(new FeatureRate());
        }

        /// <summary>
        /// Name of feature.
        /// </summary>
        public string FeatureName {
            get {
                FeatureName name = FeatureNameElement;
                return name.Value;
            }
        }

        /// <summary>
        /// Rate of feature.
        /// </summary>
        public string FeatureRate {
            get {
                FeatureRate rate = FeatureRateElement;
                return (rate != null) ? rate.Value : "";
            }
        }

        /// <summary>
        /// docs:featureName element of feature.
        /// </summary>
        public FeatureName FeatureNameElement {
            get {
                return FindExtension(
                    DOCS.FeatureNameElement,
                    DOCS.Ns) as FeatureName;
            }
        }

        /// <summary>
        /// docs:featureRate element of feature.
        /// </summary>
        public FeatureRate FeatureRateElement {
            get {
                return FindExtension(
                    DOCS.FeatureRateElement,
                    DOCS.Ns) as FeatureRate;
            }
        }
    }

    /// <summary>
    /// Max upload size.
    /// </summary>
    public class MaxUploadSize : SimpleElement {
        public MaxUploadSize()
            : base(
            DOCS.MaxUploadSizeElement,
            DOCS.Prefix,
            DOCS.Ns) {
        }

        /// <summary>
        /// Gets the kind.
        /// </summary>
        /// <value>
        /// The kind.
        /// </value>
        public string Kind {
            get {
                return Attributes["kind"].ToString();
            }
        }
    }

    /// <summary>
    /// Additional role.
    /// </summary>
    public class AdditionalRole : SimpleElement {
        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.AdditionalRole"/> class.
        /// </summary>
        public AdditionalRole()
            : base(
            DOCS.AdditionalRoleElement,
            AclNameTable.gAclPrefix,
            AclNameTable.gAclNamespace) { }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public override string Value {
            get {
                return Attributes["value"] as string;
            }
        }
    }

    /// <summary>
    /// Additional role set.
    /// </summary>
    public class AdditionalRoleSet : SimpleContainer {
        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.AdditionalRoleSet"/> class.
        /// </summary>
        public AdditionalRoleSet()
            : base(
            DOCS.AdditionalRoleSetElement,
            DOCS.Prefix,
            DOCS.Ns) {
            ExtensionFactories.Add(new AdditionalRole());
        }

        /// <summary>
        /// Gets the primary role.
        /// </summary>
        /// <value>
        /// The primary role.
        /// </value>
        public string PrimaryRole {
            get {
                return Attributes["primaryRole"].ToString();
            }
        }

        /// <summary>
        /// Gets the additional roles.
        /// </summary>
        /// <value>
        /// The additional roles.
        /// </value>
        public List<AdditionalRole> AdditionalRoles {
            get {
                return FindExtensions<AdditionalRole>(DOCS.AdditionalRoleElement,
                    AclNameTable.gAclNamespace);
            }
        }
    }

    /// <summary>
    /// Additional role info.
    /// </summary>
    public class AdditionalRoleInfo : SimpleContainer {
        public AdditionalRoleInfo()
            : base(
            DOCS.AdditionalRoleInfoElement,
            DOCS.Prefix,
            DOCS.Ns) {
            ExtensionFactories.Add(new AdditionalRoleSet());
        }

        /// <summary>
        /// Gets the kind.
        /// </summary>
        /// <value>
        /// The kind.
        /// </value>
        public string Kind {
            get {
                return Attributes["kind"] as string;
            }
        }

        /// <summary>
        /// Gets the additional role sets.
        /// </summary>
        /// <value>
        /// The additional role sets.
        /// </value>
        public List<AdditionalRoleSet> AdditionalRoleSets {
            get {
                return FindExtensions<AdditionalRoleSet>(DOCS.AdditionalRoleSetElement,
                    DOCS.Ns);
            }
        }
    }

    /// <summary>
    /// Metadata.
    /// </summary>
    public class Metadata : AbstractEntry {
        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.Metadata"/> class.
        /// </summary>
        public Metadata()
            : base() {
            AddExtension(new QuotaBytesUsed());
            AddExtension(new ImportFormat());
            AddExtension(new ExportFormat());
            AddExtension(new Feature());
            AddExtension(new MaxUploadSize());
            AddExtension(new AdditionalRoleInfo());
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
                return FindExtension(
                    GDataParserNameTable.XmlQuotaBytesUsedElement,
                    GDataParserNameTable.gNamespace) as QuotaBytesUsed;
            }
        }

        /// <summary>
        /// Gets the import formats.
        /// </summary>
        /// <value>
        /// The import formats.
        /// </value>
        public List<ImportFormat> ImportFormats {
            get {
                return FindExtensions<ImportFormat>(
                    DOCS.ImportFormatElement,
                    DOCS.Ns);
            }
        }

        /// <summary>
        /// Gets the export formats.
        /// </summary>
        /// <value>
        /// The export formats.
        /// </value>
        public List<ExportFormat> ExportFormats {
            get {
                return FindExtensions<ExportFormat>(
                    DOCS.ExportFormatElement,
                    DOCS.Ns);
            }
        }

        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>
        /// The features.
        /// </value>
        public List<Feature> Features {
            get {
                return FindExtensions<Feature>(
                    DOCS.FeatureElement,
                    DOCS.Ns);
            }
        }

        /// <summary>
        /// Gets the max upload sizes.
        /// </summary>
        /// <value>
        /// The max upload sizes.
        /// </value>
        public List<MaxUploadSize> MaxUploadSizes {
            get {
                return FindExtensions<MaxUploadSize>(
                    DOCS.MaxUploadSizeElement,
                    DOCS.Ns);
            }
        }

        /// <summary>
        /// Gets the additional role infos.
        /// </summary>
        /// <value>
        /// The additional role infos.
        /// </value>
        public List<AdditionalRoleInfo> AdditionalRoleInfos {
            get {
                return FindExtensions<AdditionalRoleInfo>(
                    DOCS.AdditionalRoleInfoElement,
                    DOCS.Ns);
            }
        }
    }
}
