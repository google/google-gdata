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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/
using System;
using System.Collections;
using System.Xml;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.Migration
{
    /// <summary>
    /// A Google Apps mail item entry.  Represents a single email to be
    /// migrated: its message contents, starred and read status, and any
    /// labels to be applied to it.
    /// </summary>
    public class MailItemEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain mail item
        /// extension data.
        /// </summary>
        public static readonly AtomCategory MAIL_ITEM_CATEGORY =
            new AtomCategory(AppsMigrationNameTable.MailItem,
                             new AtomUri(BaseNameTable.gKind));

        private ExtensionCollection<LabelElement> labels;
        private ExtensionCollection<MailItemPropertyElement> mailItemProperties;

        /// <summary>
        /// Constructs a new <code>MailItemEntry</code> object.
        /// </summary>
        public MailItemEntry()
            : base()
        {
            Categories.Add(MAIL_ITEM_CATEGORY);

            GAppsExtensions.AddMailItemExtensions(this);
        }

        /// <summary>
        /// Rfc822Msg property accessor
        /// </summary>
        public Rfc822MsgElement Rfc822Msg
        {
            get
            {
                return FindExtension(AppsMigrationNameTable.AppsRfc822Msg,
                                     AppsMigrationNameTable.AppsNamespace) as Rfc822MsgElement;
            }
            set
            {
                ReplaceExtension(AppsMigrationNameTable.AppsRfc822Msg,
                                 AppsMigrationNameTable.AppsNamespace,
                                 value);
            }
        }

        /// <summary>
        /// Labels property accessor
        /// </summary>
        public ExtensionCollection<LabelElement> Labels
        {
            get
            {
                if (labels == null)
                {
                    labels = new ExtensionCollection<LabelElement>(this);
                }
                return labels;
            }
        }

        /// <summary>
        /// MailItemProperties accessor
        /// </summary>
        public ExtensionCollection<MailItemPropertyElement> MailItemProperties
        {
            get
            {
                if (mailItemProperties == null)
                {
                    mailItemProperties = new ExtensionCollection<MailItemPropertyElement>(this);
                }
                return mailItemProperties;
            }
        }
    }
}
