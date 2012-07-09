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
    public class ProcessingStatus : SimpleElement
    {
        /// <summary>
        /// default constructor for sc:processing_status
        /// </summary>
        public ProcessingStatus()
            : base(ContentForShoppingNameTable.ProcessingStatus,
               ContentForShoppingNameTable.scDataPrefix,
               ContentForShoppingNameTable.BaseNamespace)
        {
        }

        /// <summary>
        /// constructor that takes status for sc:processing_status
        /// </summary>
        /// <param name="status">The entry's status.</param>
        public ProcessingStatus(string status)
            : base(ContentForShoppingNameTable.ProcessingStatus,
               ContentForShoppingNameTable.scDataPrefix,
               ContentForShoppingNameTable.BaseNamespace)
        {
            this.Status = status;
        }

        /// <summary>
        /// Status property accessor
        /// </summary>
        public string Status
        {
            get
            {
                return Convert.ToString(Attributes[ContentForShoppingNameTable.Status]);
            }
            set
            {
                Attributes[ContentForShoppingNameTable.Status] = value;
            }
        }
    }
}
