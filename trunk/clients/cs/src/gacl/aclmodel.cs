using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.AccessControl;

namespace Google.AccessControl {
    public class Acl : Entry {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject() {
            if (this.AtomEntry == null) {
                this.AtomEntry = new AclEntry();
            }
        }

        /// <summary>
        /// readonly accessor for the AclEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public AclEntry AclEntry {
            get {
                EnsureInnerObject();
                return this.AtomEntry as AclEntry;
            }
        }

        public string Role {
            get {
                EnsureInnerObject();
                if (this.AclEntry.Role != null)
                    return this.AclEntry.Role.Value;
                return null;
            }
            set {
                EnsureInnerObject();
                this.AclEntry.Role = new AclRole(value);
            }
        }

        public AclScope Scope {
            get {
                EnsureInnerObject();
                return this.AclEntry.Scope;
            }
            set {
                EnsureInnerObject();
                this.AclEntry.Scope = value;
            }
        }
    }
}
