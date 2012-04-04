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

#define TRACE

using System;
using System.Xml;
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.AccessControl {
    /// <summary>
    /// GData schema extension describing an ACL with authorization key
    /// </summary>
    public class AclWithKey : SimpleContainer {
        /// <summary>
        /// Constructs a new AclWithKey instance.
        /// </summary>
        public AclWithKey() : this(null) {
        }

        /// <summary>
        /// Constructs a new AclWithKey instance.
        /// </summary>
        public AclWithKey(string key)
            : base(AclNameTable.XmlAclWithKeyElement,
            AclNameTable.gAclAlias,
            AclNameTable.gAclNamespace) {
            this.ExtensionFactories.Add(new AclRole());
            this.Attributes.Add(AclNameTable.XmlAttributeKey, key);
        }

        /// <summary>
        /// property accessor for the AclRole
        /// </summary>
        public AclRole Role {
            get {
                return FindExtension(AclNameTable.XmlAclRoleElement,
                    AclNameTable.gAclNamespace) as AclRole;
            }
            set {
                ReplaceExtension(AclNameTable.XmlAclRoleElement,
                    AclNameTable.gAclNamespace, value);
            }
        }

        /// <summary>
        /// property accessor for the key attribute
        /// </summary>
        public String Type {
            get {
                return this.Attributes[AclNameTable.XmlAttributeKey] as String;
            }
            set {
                this.Attributes[AclNameTable.XmlAttributeType] = value;
            }
        }
    }
}

