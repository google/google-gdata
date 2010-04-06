/* Copyright (c) 2007 Google Inc.
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
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps
{
    /// <summary>
    /// A Google Apps user entry.  A UserEntry object encapsulates
    /// the login, name and quota information for a single user.
    /// </summary>
    public class UserEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain user
        /// extension data.
        /// </summary>
        public static readonly AtomCategory USER_ACCOUNT_CATEGORY =
            new AtomCategory(AppsNameTable.User,
                             new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new UserEntry instance with the appropriate category
        /// to indicate that it is a user account.
        /// </summary>
        public UserEntry()
            : base()
        {
            Categories.Add(USER_ACCOUNT_CATEGORY);

            GAppsExtensions.AddProvisioningExtensions(this);
        }

        /// <summary>
        /// The login element in this user account entry.
        /// </summary>
        public LoginElement Login
        {
            get
            {
                return FindExtension(AppsNameTable.AppsLogin,
                                     AppsNameTable.AppsNamespace) as LoginElement;
            }
            set
            {
                ReplaceExtension(AppsNameTable.AppsLogin,
                                 AppsNameTable.AppsNamespace,
                                 value);
            }
        }

        /// <summary>
        /// The quota element in this user account entry.
        /// </summary>
        public QuotaElement Quota
        {
            get
            {
                return FindExtension(AppsNameTable.AppsQuota,
                                     AppsNameTable.AppsNamespace) as QuotaElement;
            }
            set
            {
                ReplaceExtension(AppsNameTable.AppsQuota,
                                 AppsNameTable.AppsNamespace,
                                 value);
            }
        }

        /// <summary>
        /// The name element in this user account entry.
        /// </summary>
        public NameElement Name
        {
            get
            {
                return FindExtension(AppsNameTable.AppsName,
                                     AppsNameTable.AppsNamespace) as NameElement;
            }
            set
            {
                ReplaceExtension(AppsNameTable.AppsName,
                                 AppsNameTable.AppsNamespace,
                                 value);
            }
        }

        /// <summary>
        /// Updates this UserEntry.
        /// </summary>
        /// <returns>the updated UserEntry</returns>
        public new UserEntry Update()
        {
            try
            {
                return base.Update() as UserEntry;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }
    }
}
