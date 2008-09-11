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
using Google.GData.Client;

namespace Google.GData.Extensions.AppControl {

    /// <summary>
    /// MediaGroup container element for the MediaRss namespace
    /// </summary>
    public class AppControl : SimpleContainer
    {
        /// <summary>
        /// default constructor for media:group
        /// </summary>
        public AppControl() :
            base(BaseNameTable.XmlElementPubControl,
                 BaseNameTable.gAppPublishingPrefix,
                 BaseNameTable.NSAppPublishing)
        {
            this.ExtensionFactories.Add(new AppDraft());
        }

        /// <summary>
        /// returns the media:credit element
        /// </summary>
        public AppDraft Draft
        {
            get
            {
                return FindExtension(BaseNameTable.XmlElementPubDraft,
                                     BaseNameTable.NSAppPublishing) as AppDraft;
            }
            set
            {
                ReplaceExtension(BaseNameTable.XmlElementPubDraft,
                                BaseNameTable.NSAppPublishing,
                                value);
            }
        }
   }

    /// <summary>
    /// app:draft schema extension describing that an entry is in draft mode
    /// it's a child of app:control
    /// </summary>
    public class AppDraft : SimpleElement
    {
        /// <summary>
        /// default constructor for media:credit
        /// </summary>
        public AppDraft()
        : base(BaseNameTable.XmlElementPubDraft, 
               BaseNameTable.gAppPublishingPrefix,
               BaseNameTable.NSAppPublishing)
        {}

         /// <summary>
        /// default constructor for media:credit
        /// </summary>
        public AppDraft(bool isDraft)
        : base(BaseNameTable.XmlElementPubDraft, 
               BaseNameTable.gAppPublishingPrefix,
               BaseNameTable.NSAppPublishing,
               isDraft ? "yes" : "no")
        {}

          /// <summary>
        ///  Accessor Method for the value as integer
        /// </summary>
        public override bool BooleanValue
        {
            get { return this.Value == "yes" ? true : false; } 
            set { this.Value = (value == true) ? "yes" : "no"; }
        }
    }
}
