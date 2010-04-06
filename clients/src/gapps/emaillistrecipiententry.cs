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
using Google.GData.Extensions;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps
{
    /// <summary>    /// A Google Apps email list recipient entry.  This class    /// defines a single recipient on an email list using a Who    /// (gd:who) object.    /// </summary>
    public class EmailListRecipientEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain email list
        /// recipient extension data.
        /// </summary>
        public static readonly AtomCategory EMAILLIST_RECIPIENT_CATEGORY =
            new AtomCategory(AppsNameTable.EmailListRecipient,
                             new AtomUri(BaseNameTable.gKind));

        /// <summary>        /// Constructs a new EmailListRecipientEntry without        /// setting the Recipient property.        /// </summary>
        public EmailListRecipientEntry()
            : base()
        {
            Categories.Add(EMAILLIST_RECIPIENT_CATEGORY);

            this.AddExtension(new Who());
        }

        /// <summary>        /// Constructs a new EmailListRecipientEntry, using        /// the specified email address to initialize the        /// Recipient property.        /// </summary>        /// <param name="recipientAddress">the recipient's
        /// email address</param>
        public EmailListRecipientEntry(String recipientAddress)
            : base()
        {
            Categories.Add(EMAILLIST_RECIPIENT_CATEGORY);
            this.AddExtension(new Who());

            Who who = new Who();

            who.Rel = Who.RelType.MESSAGE_TO;
            who.Email = recipientAddress;
            this.Recipient = who;

        }

        /// <summary>
        /// The recipient element in this entry.
        /// </summary>
        public Who Recipient
        {
            get 
            {
                return FindExtension(GDataParserNameTable.XmlWhoElement, BaseNameTable.gNamespace) as Who;
            }
            set 
            {
                ReplaceExtension(GDataParserNameTable.XmlWhoElement, BaseNameTable.gNamespace, value);
            }
        }

        /// <summary>
        /// Overrides the base Update() method to throw a ClientFeedException,
        /// because email list recipient entries cannot be updated.  This exception
        /// indicates that the client made an erroneous request to the email
        /// list feed.
        /// </summary>
        public new void Update()
        {
            throw new ClientFeedException("Email list recipient entries cannot be updated.");
        }
    }
}
