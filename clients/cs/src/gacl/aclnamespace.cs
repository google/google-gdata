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

namespace Google.GData.AccessControl {
    /// <summary>Google Access Control List namespace</summary>
    public class AclNameTable : BaseNameTable {
        /// <summary>default access control namespace</summary> 
        public const string gAclNamespace = "http://schemas.google.com/acl/2007";

        /// <summary>default access control prefix</summary> 
        public const string gAclPrefix = gAclNamespace + "#";

        /// <summary>
        /// hash prefixed start for AclNamespace strings
        /// </summary>
        public const string gAclCalPrefix = GDataParserNameTable.NSGCal + "#";

        /// <summary>default access control alias</summary> 
        public const string gAclAlias = "gAcl";

        /// <summary>Link that provides the URI for the access control list feed 
        /// </summary> 
        public const string LINK_REL_ACCESS_CONTROL_LIST =
            gAclPrefix + "accessControlList";

        /// <summary>UIR for the entry that is controlled by the ACL feed
        /// </summary> 
        public const string LINK_REL_CONTROLLED_OBJECT =
            gAclPrefix + "controlledObject";

        /// <summary>access kind</summary> 
        public const string ACL_KIND = gAclPrefix + "accessRule";

        /// <summary>the scope element</summary> 
        public const string XmlAclScopeElement = "scope";

        /// <summary>the role element</summary> 
        public const string XmlAclRoleElement = "role";

        /// <summary>the withKey element</summary> 
        public const string XmlAclWithKeyElement = "withKey";
    }
}
