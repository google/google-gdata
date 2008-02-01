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



namespace Google.GData.AccessControl
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an AccessControl feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class AclEntry : AbstractEntry
    {

        private AclRole aclRole;
        private AclScope aclScope;

        /// <summary>
        /// Category used to label entries that contain AccessControl extension data.
        /// </summary>
        public static AtomCategory ACL_CATEGORY =
        new AtomCategory(AclNameTable.ACL_KIND, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new AccessControlEntry instance with the appropriate category
        /// to indicate that it is an AccessControlEntry.
        /// </summary>
        public AclEntry()
        : base()
        {
            Categories.Add(ACL_CATEGORY);
        }


   
     

#region Public Methods
   
        /// <summary>
        ///  property accessor for the AclRole
        /// </summary>
        public AclRole Role
        {
            get { return this.aclRole;}
            set
            {
                if (this.aclRole != null)
                {
                    ExtensionElements.Remove(this.aclRole);
                }
                this.aclRole = value; 
                ExtensionElements.Add(this.aclRole);
            }
        }

        /// <summary>
        ///  property accessor for the AclScope
        /// </summary>
        public AclScope Scope 
        {
            get { return this.aclScope;}
            set
            {
                if (this.aclScope != null)
                {
                    ExtensionElements.Remove(this.aclScope);
                }
                this.aclScope = value; 
                ExtensionElements.Add(this.aclScope);
            }
        }

    
#endregion

       

#region AclEntry Parser

        //////////////////////////////////////////////////////////////////////
        /// <summary>parses the inner state of the element</summary>
        /// <param name="e">the extensionelement during the parsing process, xml node</param>
        /// <param name="parser">the atomFeedParser that called this</param>
        //////////////////////////////////////////////////////////////////////
        public override void Parse(ExtensionElementEventArgs e, AtomFeedParser parser)
        {
            Tracing.TraceCall("AclEntry:Parse is called:" + e);
            XmlNode node = e.ExtensionElement;
 
            if (String.Compare(node.NamespaceURI, AclNameTable.gAclNamespace, true) == 0)
            {
                // Parse a Role Element
                if (node.LocalName == AclNameTable.XmlAclRoleElement)
                {
                    this.Role = AclRole.parse(node);
                    e.DiscardEntry = true;
                }
                // Parse a Where Element
                else if (node.LocalName == AclNameTable.XmlAclScopeElement)
                {
                    this.Scope = AclScope.parse(node);
                    e.DiscardEntry = true;
                }
            }
        }

#endregion

    }
}

