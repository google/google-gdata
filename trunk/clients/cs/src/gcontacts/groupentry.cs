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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/

#define USE_TRACING

using System;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Contacts {
    /// <summary>
    /// Entry API customization class for defining entries in an Group feed.
    /// </summary>
    public class GroupEntry : BaseContactEntry {
        /// <summary>
        /// default contact term string for the contact relationship link
        /// </summary>
        public static string GroupTerm = "http://schemas.google.com/contact/2008#group";

        /// <summary>
        /// Category used to label entries that contain contact extension data.
        /// </summary>
        public static AtomCategory GROUP_CATEGORY =
            new AtomCategory(GroupEntry.GroupTerm, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new ContactEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public GroupEntry()
            : base() {
            Tracing.TraceMsg("Created Group Entry");
            this.AddExtension(new SystemGroup());
            Categories.Add(GROUP_CATEGORY);
        }

        /// <summary>
        /// typed override of the Update method
        /// </summary>
        /// <returns></returns>
        public new GroupEntry Update() {
            return base.Update() as GroupEntry;
        }

        /// <summary>
        /// returns the systemgroup id, if this groupentry represents 
        /// a system group. 
        /// The values of the system group ids corresponding to these 
        /// groups can be found in the Reference Guide for the Contacts Data API.
        /// Currently the values can be Contacts, Friends, Family and Coworkers
        /// </summary>
        /// <returns></returns>
        public string SystemGroup {
            get {
                SystemGroup sg = FindExtension(ContactsNameTable.SystemGroupElement,
                    ContactsNameTable.NSContacts) as SystemGroup;

                if (sg != null) {
                    return sg.Id;
                }
                return null;
            }
        }
    }
}

