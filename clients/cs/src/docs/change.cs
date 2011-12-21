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
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Docs {

    public class Change : Resource {
        public Change()
            : base() {
            this.ProtocolMajor = VersionDefaults.VersionThree;
            this.AddExtension(new Changestamp());
            this.AddExtension(new Removed());
        }

        public bool Removed {
            get {
                SimpleElement extension = this.FindExtension(
                    DOCS.Removed,
                    DOCS.Ns) as SimpleElement;
                return (extension != null);
            }
        }

        public string Changestamp {
            get {
                SimpleElement extension = this.FindExtension(
                    DOCS.Changestamp,
                    DOCS.Ns) as SimpleElement;
                return extension.Attributes["value"] as string;
            }
        }
    }

    public class LargestChangestamp : SimpleElement {
        public LargestChangestamp()
            : base(
            DOCS.LargestChangestamp,
            DOCS.Prefix,
            DOCS.Ns) {
        }
    }

    public class Removed : SimpleElement {
        public Removed()
            : base(DOCS.Removed,
            DOCS.Prefix, DOCS.Ns) {
        }
    }

    public class Changestamp : SimpleElement {
        public Changestamp()
            : base(DOCS.Changestamp,
            DOCS.Prefix, DOCS.Ns) {
        }
    }

    public class ChangeFeed : AbstractFeed {
        public ChangeFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
            this.AddExtension(new LargestChangestamp());
        }

        public override AtomEntry CreateFeedEntry() {
            return new Change();
        }

        public string LargestChangestamp {
            get {
                SimpleElement ext = this.FindExtension(
                    DOCS.LargestChangestamp,
                    DOCS.Ns) as SimpleElement;
                return ext.Attributes["value"] as string;
            }
        }
    }
}
