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
using Google.GData.Client;

namespace Google.GData.ContentForShopping.Elements
{
    public class IssueGroup : SimpleContainer
    {
        /// <summary>
        /// Issue collection
        /// </summary>
        private ExtensionCollection<Issue> issues;

        /// <summary>
        /// default constructor for sc:issue_group
        /// </summary>
        public IssueGroup()
            : base(ContentForShoppingNameTable.IssueGroup,
                ContentForShoppingNameTable.scDataPrefix,
                ContentForShoppingNameTable.BaseNamespace)
        {
            this.ExtensionFactories.Add(new Issue());
        }

        /// <summary>
        /// id property accessor
        /// </summary>
        public string Id
        {
            get
            {
                return Convert.ToString(Attributes[AtomParserNameTable.XmlIdElement]);
            }
            set
            {
                Attributes[AtomParserNameTable.XmlIdElement] = value;
            }
        }

        /// <summary>
        /// Country property accessor
        /// </summary>
        public string Country
        {
            get
            {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Country]);
            }
            set
            {
                Attributes[ContentForShoppingNameTable.Country] = value;
            }
        }

        /// <summary>
        /// Issue collection.
        /// </summary>
        public ExtensionCollection<Issue> Issues {
            get {
                if (this.issues == null) {
                    this.issues = new ExtensionCollection<Issue>(this);
                }
                return this.issues;
            }
        }
    }
}
