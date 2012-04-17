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
    public class RevisionEntry : DocumentEntry {
        /// <summary>
        /// Constructs a new RevisionEntry instance.
        /// </summary>
        public RevisionEntry()
            : base() {
            this.AddExtension(new Publish());
            this.AddExtension(new PublishAuto());
            this.AddExtension(new PublishOutsideDomain());
        }

        public bool Publish {
            get {
                bool value;
                if (!bool.TryParse(GetStringValue<Publish>(DocumentslistNametable.Publish,
                    DocumentslistNametable.NSDocumentslist), out value)) {
                    value = false;
                }
                return value;
            }
            set {
                SetStringValue<Publish>(value.ToString().ToLower(),
                    DocumentslistNametable.Publish,
                    DocumentslistNametable.NSDocumentslist);
            }
        }

        public bool PublishAuto {
            get {
                bool value;
                if (!bool.TryParse(GetStringValue<PublishAuto>(DocumentslistNametable.PublishAuto,
                    DocumentslistNametable.NSDocumentslist), out value)) {
                    value = false;
                }
                return value;
            }
            set {
                SetStringValue<PublishAuto>(value.ToString().ToLower(),
                    DocumentslistNametable.PublishAuto,
                    DocumentslistNametable.NSDocumentslist);
            }
        }

        public bool PublishOutsideDomain {
            get {
                bool value;
                if (!bool.TryParse(GetStringValue<PublishOutsideDomain>(DocumentslistNametable.PublishOutsideDomain,
                    DocumentslistNametable.NSDocumentslist), out value)) {
                    value = false;
                }
                return value;
            }
            set {
                SetStringValue<PublishOutsideDomain>(value.ToString().ToLower(),
                    DocumentslistNametable.PublishOutsideDomain,
                    DocumentslistNametable.NSDocumentslist);
            }
        }

    }

    public class Publish : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:publish
        /// </summary>
        public Publish()
            : base(DocumentslistNametable.Publish,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class PublishAuto : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:publishAuto
        /// </summary>
        public PublishAuto()
            : base(DocumentslistNametable.PublishAuto,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }

    public class PublishOutsideDomain : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:publishOutsideDomain
        /// </summary>
        public PublishOutsideDomain()
            : base(DocumentslistNametable.PublishOutsideDomain,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }
}
