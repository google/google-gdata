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
using System.Text;
using System.Globalization;
using System.Diagnostics;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.AccessControl {
    /// <summary>
    /// GData schema extension describing an account role
    /// </summary>
    public class AclRole : EnumConstruct {
        /// <summary>string constant for the none role</summary>
        public const string ROLE_NONE = "none";
        /// <summary>string constant for the read only role</summary>
        public const string CALENDAR_ROLE_READ = AclNameTable.gAclCalPrefix + "read";
        /// <summary>string constant for the free/busy role</summary>
        public const string CALENDAR_ROLE_FREEBUSY = AclNameTable.gAclCalPrefix + "freebusy";
        /// <summary>string constant for the editor role</summary>
        public const string CALENDAR_ROLE_EDITOR = AclNameTable.gAclCalPrefix + "editor";
        /// <summary>string constant for the owner role</summary>
        public const string CALENDAR_ROLE_OWNER = AclNameTable.gAclCalPrefix + "owner";
        /// <summary>string constant for the root role</summary>
        public const string CALENDAR_ROLE_ROOT = AclNameTable.gAclCalPrefix + "root";

        /// <summary>object constant for the none role</summary>
        public static AclRole ACL_NONE = new AclRole(ROLE_NONE);
        /// <summary>object constant for the read role</summary>
        public static AclRole ACL_CALENDAR_READ = new AclRole(CALENDAR_ROLE_READ);
        /// <summary>object constant for the freebusy role</summary>
        public static AclRole ACL_CALENDAR_FREEBUSY = new AclRole(CALENDAR_ROLE_FREEBUSY);
        /// <summary>object constant for the editor role</summary>
        public static AclRole ACL_CALENDAR_EDITOR = new AclRole(CALENDAR_ROLE_EDITOR);
        /// <summary>object constant for the owner role</summary>
        public static AclRole ACL_CALENDAR_OWNER = new AclRole(CALENDAR_ROLE_OWNER);
        /// <summary>object constant for the root role</summary>
        public static AclRole ACL_CALENDAR_ROOT = new AclRole(CALENDAR_ROLE_ROOT);

        /// <summary>
        ///  default constructor
        /// </summary>
        public AclRole()
            : base(AclNameTable.XmlAclRoleElement, AclNameTable.gAclAlias, AclNameTable.gAclNamespace) {
        }

        /// <summary>
        /// constructor with a default string value
        /// </summary>
        /// <param name="value">ACL value</param>
        public AclRole(string value)
            : base(AclNameTable.XmlAclRoleElement, AclNameTable.gAclAlias, AclNameTable.gAclNamespace, value) {
        }
    }
}
