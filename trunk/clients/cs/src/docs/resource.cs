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
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Client.ResumableUpload;
using Google.GData.Extensions;
using Google.GData.AccessControl;
using System.Globalization;

namespace Google.GData.Docs {
    /// <summary>
    /// Resource entry.
    /// </summary>
    /// <exception cref='ArgumentNullException'>
    /// Is thrown when an argument passed to a method is invalid because it is <see langword="null" /> .
    /// </exception>
    public class Resource : AbstractEntry {
        public bool ToConvert = true;

        public Resource()
            : base() {
            this.ProtocolMajor = VersionDefaults.VersionThree;
            this.AddExtension(new FeedLink());
            this.AddExtension(new ResourceId());
            this.AddExtension(new WritersCanInvite());
            this.AddExtension(new LastViewed());
            this.AddExtension(new LastModifiedBy());
            this.AddExtension(new QuotaBytesUsed());
            this.AddExtension(new Content());
            this.AddExtension(new Publish());
            this.AddExtension(new PublishAuto());
            this.AddExtension(new PublishOutsideDomain());
        }

        /// <summary>
        /// add the documentslist namespace
        /// </summary>
        /// <param name="writer">The XmlWrite, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer) {
            base.AddOtherNamespaces(writer);
            if (writer == null) {
                throw new ArgumentNullException("writer");
            }

            string strPrefix = writer.LookupPrefix(DOCS.Ns);
            if (strPrefix == null) {
                writer.WriteAttributeString("xmlns",
                    DOCS.Prefix, null, DOCS.Ns);
            }
        }

        /// <summary>
        /// returns the resumable edit media Uri for a given entry
        /// </summary>
        public Uri ResumableEditUri {
            get {
                // scan the link collection
                return ResumableUploader.GetResumableEditUri(this.Links);
            }
        }

        /// <summary>
        /// returns the resumabled create media Uri for a given entry
        /// </summary>
        public Uri ResumableCreateUri {
            get {
                return ResumableUploader.GetResumableCreateUri(this.Links);
            }
        }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>
        /// The type of the resource.
        /// </value>
        public string ResourceType {
            get {
                foreach (AtomCategory category in Categories) {
                    if (category.Scheme == BaseNameTable.gKind) {
                        return category.Label;
                    }
                }
                return null;
            }
            set {
                foreach (AtomCategory category in Categories) {
                    if (category.Scheme == BaseNameTable.gKind) {
                        Categories.Remove(category);
                    }
                }
                ToggleCategory(DOCS.ResourceType(value).category, true);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Google.GData.Docs.Resource"/> is starred.
        /// </summary>
        /// <value>
        /// <c>true</c> if starred; otherwise, <c>false</c>.
        /// </value>
        public bool Starred {
            get {
                return Categories.Contains(DOCS.ResourceType(DOCS.Starred).category);
            }
            set {
                ToggleCategory(DOCS.ResourceType(DOCS.Starred).category, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Google.GData.Docs.Resource"/> is hidden.
        /// </summary>
        /// <value>
        /// <c>true</c> if hidden; otherwise, <c>false</c>.
        /// </value>
        public bool Hidden {
            get {
                return Categories.Contains(DOCS.ResourceType(DOCS.Hidden).category);
            }
            set {
                ToggleCategory(DOCS.ResourceType(DOCS.Hidden).category, value);
            }
        }

        /// <summary>
        /// Gets the in collections.
        /// </summary>
        /// <value>
        /// The in collections.
        /// </value>
        public List<AtomLink> InCollections {
            get {
                List<AtomLink> links = this.Links.FindServiceList(DOCS.Parent, AtomLink.ATOM_TYPE);
                return links;
            }
        }

        public void SetValueAttribute<T>(string name, bool value) where T : ExtensionBase, new() {
            ExtensionBase ext = this.FindExtension(name, DOCS.Ns) as ExtensionBase;
            if (ext == null) {
                ext = new T();
            }
            ext.Attributes["value"] = value ? "true" : "false";
        }

        public bool GetValueAttribute(string name) {
            ExtensionBase ext = this.FindExtension(name, DOCS.Ns) as ExtensionBase;
            if (ext == null) {
                return false;
            } else {
                return ext.Attributes["value"].ToString() == "true";
            }
        }

        /// <summary>
        /// Gets the resource identifier.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        public string ResourceId {
            get {
                return GetStringValue<ResourceId>(
                    GDataParserNameTable.XmlResourceIdElement,
                    GDataParserNameTable.gNamespace);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Google.GData.Docs.Resource"/> writers can invite.
        /// </summary>
        /// <value>
        /// <c>true</c> if writers can invite; otherwise, <c>false</c>.
        /// </value>
        public bool WritersCanInvite {
            get {
                String v = GetStringValue<WritersCanInvite>(
                    DOCS.WritersCanInviteElement,
                    DOCS.Ns);
                // v can either be 1/0 or true/false
                bool ret = Utilities.XSDTrue == v;
                if (!ret) {
                    ret = "0" == v;
                }
                return ret;
            }
            set {
                String v = value ? Utilities.XSDTrue : Utilities.XSDFalse;
                SetStringValue<WritersCanInvite>(v, DOCS.WritersCanInviteElement, DOCS.Ns);
            }
        }

        /// <summary>
        /// Gets the last viewed.
        /// </summary>
        /// <value>
        /// The last viewed.
        /// </value>
        public DateTime LastViewed {
            get {
                LastViewed lastViewed = LastViewedElement;
                if (lastViewed != null && lastViewed.Value != null) {
                    DateTime dt = new DateTime();
                    if (!DateTime.TryParse(lastViewed.Value, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
                        return DateTime.MinValue;
                    }

                    return dt;
                }
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets the last viewed element.
        /// </summary>
        /// <value>
        /// The last viewed element.
        /// </value>
        public LastViewed LastViewedElement {
            get {
                return FindExtension(GDataParserNameTable.XmlLastViewedElement,
                    GDataParserNameTable.gNamespace) as LastViewed;
            }
        }

        /// <summary>
        /// Gets the last modified by.
        /// </summary>
        /// <value>
        /// The last modified by.
        /// </value>
        public LastModifiedBy LastModifiedBy {
            get {
                return FindExtension(GDataParserNameTable.XmlLastModifiedByElement,
                    GDataParserNameTable.gNamespace) as LastModifiedBy;
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

        /// <summary>
        /// returns the string that should represent the Uri to the access control list
        /// </summary>
        /// <returns>the value of the hret attribute for the acl feedlink, or null if not found</returns>
        public string AclFeedLink {
            get {
                List<FeedLink> list = FindExtensions<FeedLink>(
                    GDataParserNameTable.XmlFeedLinkElement,
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
        public string RevisionFeedLink {
            get {
                List<FeedLink> list = FindExtensions<FeedLink>(
                    GDataParserNameTable.XmlFeedLinkElement,
                    BaseNameTable.gNamespace);

                foreach (FeedLink fl in list) {
                    if (fl.Rel == DOCS.Revisions) {
                        return fl.Href;
                    }
                }

                return null;
            }
        }
    }

    /// <summary>
    /// Content.
    /// </summary>
    public class Content : SimpleElement {
        public Content()
            : base(DOCS.ContentElement,
            AtomParserNameTable.AtomPrefix,
            AtomParserNameTable.NSAtom) { }

        public string Src {
            get {
                return Attributes["src"] as string;
            }
        }

        public string Type {
            get {
                return Attributes["type"] as string;
            }
        }
    }

    /// <summary>
    /// Writers can invite.
    /// </summary>
    public class WritersCanInvite : SimpleAttribute {
        public WritersCanInvite()
            : base(
            DOCS.WritersCanInviteElement,
            DOCS.Prefix,
            DOCS.Ns) { }
    }

    /// <summary>
    /// Resource feed.
    /// </summary>
    public class ResourceFeed : AbstractFeed {
        /// <summary>
        /// Initializes a new instance of the <see cref="Google.GData.Docs.ResourceFeed"/> class.
        /// </summary>
        /// <param name='uriBase'>
        /// URI base.
        /// </param>
        /// <param name='iService'>
        /// IService.
        /// </param>
        public ResourceFeed(Uri uriBase, IService iService) 
            : base(uriBase, iService) { }

        /// <summary>
        /// Creates the feed entry.
        /// </summary>
        /// <returns>
        /// The feed entry.
        /// </returns>
        public override AtomEntry CreateFeedEntry() {
            return new Resource();
        }

        /// <summary>
        /// Handles the extension elements.
        /// </summary>
        /// <param name='e'>
        /// E.
        /// </param>
        /// <param name='parser'>
        /// Parser.
        /// </param>
        protected override void HandleExtensionElements(
            ExtensionElementEventArgs e, AtomFeedParser parser) {
            base.HandleExtensionElements(e, parser);
        }
    }
}

