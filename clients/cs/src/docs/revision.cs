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
    /// <summary>
    /// docs:publish element.
    /// </summary>
    public class Publish : SimpleElement {
        public Publish()
            : base(
            DOCS.PublishElement,
            DOCS.Prefix,
            DOCS.Ns) { }
    }

    /// <summary>
    /// docs:publishAuto element.
    /// </summary>
    public class PublishAuto : SimpleElement {
        public PublishAuto()
            : base(
            DOCS.PublishAutoElement,
            DOCS.Prefix,
            DOCS.Ns) { }
    }

    /// <summary>
    /// docs:publishOutsideDomain element.
    /// </summary>
    public class PublishOutsideDomain : SimpleElement {
        public PublishOutsideDomain()
            : base(
            DOCS.PublishOutsideDomainElement,
            DOCS.Prefix,
            DOCS.Ns) { }
    }

    /// <summary>
    /// Revision entry.
    /// </summary>
    public class Revision : Resource {
        public Revision()
            : base() {
            this.AddExtension(new PublishAuto());
            this.AddExtension(new Publish());
            this.AddExtension(new PublishOutsideDomain());
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Google.GData.Docs.Revision"/> is publish.
        /// </summary>
        /// <value>
        /// <c>true</c> if publish; otherwise, <c>false</c>.
        /// </value>
        public bool Publish {
            get {
                return this.GetValueAttribute(DOCS.PublishElement);
            }
            set {
                this.SetValueAttribute<Publish>(DOCS.PublishElement, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Google.GData.Docs.Revision"/> publish auto.
        /// </summary>
        /// <value>
        /// <c>true</c> if publish auto; otherwise, <c>false</c>.
        /// </value>
        public bool PublishAuto {
            get {
                return this.GetValueAttribute(DOCS.PublishAutoElement);
            }
            set {
                this.SetValueAttribute<PublishAuto>(DOCS.PublishAutoElement, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Google.GData.Docs.Revision"/> publish outside domain.
        /// </summary>
        /// <value>
        /// <c>true</c> if publish outside domain; otherwise, <c>false</c>.
        /// </value>
        public bool PublishOutsideDomain {
            get {
                return this.GetValueAttribute(DOCS.PublishOutsideDomainElement);
            }
            set {
                this.SetValueAttribute<PublishOutsideDomain>(DOCS.PublishOutsideDomainElement, value);
            }
        }
    }

    /// <summary>
    /// Revision feed.
    /// </summary>
    public class RevisionFeed : AbstractFeed {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Google.GData.Docs.RevisionFeed"/> class.
        /// </summary>
        /// <param name='uriBase'>
        /// URI base.
        /// </param>
        /// <param name='iService'>
        /// IService.
        /// </param>
        public RevisionFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) { }

        /// <summary>
        /// Creates the feed entry.
        /// </summary>
        /// <returns>
        /// The feed entry.
        /// </returns>
        public override AtomEntry CreateFeedEntry() {
            return new Revision();
        }
    }
}
