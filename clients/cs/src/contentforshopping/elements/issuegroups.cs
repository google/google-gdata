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
using Google.GData.Extensions;

namespace Google.GData.ContentForShopping.Elements
{
    public class IssueGroups : SimpleContainer
    {
        /// <summary>
        /// IssueGroup collection
        /// </summary>
        private ExtensionCollection<IssueGroup> issueGroups;

        /// <summary>
        /// default constructor for sc:issue_groups
        /// </summary>
        public IssueGroups()
            : base(ContentForShoppingNameTable.IssueGroups,
                ContentForShoppingNameTable.scDataPrefix,
                ContentForShoppingNameTable.BaseNamespace)
        {
            this.ExtensionFactories.Add(new IssueGroup());
        }

        /// <summary>
        /// IssueGroup collection.
        /// </summary>
        public ExtensionCollection<IssueGroup> Groups {
            get {
                if (this.issueGroups == null) {
                    this.issueGroups = new ExtensionCollection<IssueGroup>(this);
                }
                return this.issueGroups;
            }
        }
    }
}
