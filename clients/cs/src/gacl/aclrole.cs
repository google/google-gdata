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

namespace Google.GData.AccessControl 
{

     /// <summary>
    /// GData schema extension describing an account role
    /// </summary>
    public class AclRole : EnumConstruct
    {
        /// <summary>string constant for the none role</summary>
        public const string ROLE_NONE =  "none";
        /// <summary>string constant for the read only role</summary>
        public const string CALENDAR_ROLE_READ = AclNameTable.gAclCalPrefix+ "read";
        /// <summary>string constant for the free/busy role</summary>
        public const string CALENDAR_ROLE_FREEBUSY = AclNameTable.gAclCalPrefix+ "freebusy";
        /// <summary>string constant for the editor role</summary>
        public const string CALENDAR_ROLE_EDITOR = AclNameTable.gAclCalPrefix+ "editor";
        /// <summary>string constant for the owner role</summary>
        public const string CALENDAR_ROLE_OWNER = AclNameTable.gAclCalPrefix+ "owner";
        /// <summary>string constant for the root role</summary>
        public const string CALENDAR_ROLE_ROOT = AclNameTable.gAclCalPrefix+ "root";

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
        : base(AclNameTable.XmlAclRoleElement)
        {
        }

        /// <summary>
        ///  constructor with a default string value
        /// </summary>
        /// <param name="value">transparency value</param>
        public AclRole(string value)
        : base(AclNameTable.XmlAclRoleElement, value)
        {
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public override string XmlNamespace
        {
            get { return AclNameTable.gAclNamespace; }
        }
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public override string XmlNamespacePrefix
        {
            get { return AclNameTable.gAclAlias; }
        }

        /// <summary>
        ///  parse method is called from the atom parser to populate an Transparency node
        /// </summary>
        /// <param name="node">the xmlnode to parser</param>
        /// <returns>Notifications object</returns>
        public static AclRole parse(XmlNode node)
        {
            AclRole role = null;
            Tracing.TraceMsg("Parsing a gAcl:AclRole");
            if (String.Compare(node.NamespaceURI, AclNameTable.gAclNamespace, true) == 0
                && String.Compare(node.LocalName, AclNameTable.XmlAclRoleElement) == 0)
            {
                role = new AclRole();
                if (node.Attributes != null)
                {
                    role.Value = node.Attributes["value"].Value;
                    Tracing.TraceMsg("AclRole parsed, value = " + role.Value);
                }
            }
            return role;
        }
    }
}
