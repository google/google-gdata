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

namespace Google.GData.Apps {
    /// <summary>
    /// A Google Apps nickname entry.  A NicknameEntry identifies a
    /// nickname and the user to whom the nickname is assigned.
    /// </summary>
    public class NicknameEntry : AbstractEntry {
        /// <summary>
        /// Category used to label entries that contain nickname
        /// extension data.
        /// </summary>
        public static AtomCategory NICKNAME_CATEGORY =
            new AtomCategory(AppsNameTable.Nickname,
                new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new NicknameEntry instance with the appropriate category
        /// to indicate that it is a nickname.
        /// </summary>
        public NicknameEntry()
            : base() {
            Categories.Add(NICKNAME_CATEGORY);

            GAppsExtensions.AddProvisioningExtensions(this);
        }

        /// <summary>
        /// The login element in this entry.
        /// </summary>
        public LoginElement Login {
            get {
                return FindExtension(AppsNameTable.AppsLogin,
                    AppsNameTable.AppsNamespace) as LoginElement;
            }
            set {
                ReplaceExtension(AppsNameTable.AppsLogin,
                    AppsNameTable.AppsNamespace,
                    value);
            }
        }

        /// <summary>
        /// The nickname element in this entry.
        /// </summary>
        public NicknameElement Nickname {
            get {
                return FindExtension(AppsNameTable.AppsNickname,
                    AppsNameTable.AppsNamespace) as NicknameElement;
            }
            set {
                ReplaceExtension(AppsNameTable.AppsNickname,
                    AppsNameTable.AppsNamespace,
                    value);
            }
        }

        /// <summary>
        /// Overrides the base Update() method to throw an
        /// exception, because nickname entries cannot be
        /// updated.
        /// </summary>
        public new void Update() {
            throw new GDataRequestException("Nickname entries cannot be updated.");
        }
    }
}
