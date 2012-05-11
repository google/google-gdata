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

namespace Google.GData.ContentForShopping.Elements {
    public class Performance : SimpleContainer {
        ExtensionCollection<Datapoint> datapointList;

        /// <summary>
        /// default constructor for sc:performance
        /// </summary>
        public Performance()
            : base(ContentForShoppingNameTable.Performance,
            ContentForShoppingNameTable.scDataPrefix,
            ContentForShoppingNameTable.BaseNamespace) {
            this.ExtensionFactories.Add(new Datapoint());
        }

        /// <summary>
        /// Datapoint collection.
        /// </summary>
        public ExtensionCollection<Datapoint> Datapoints {
            get {
                if (this.datapointList == null) {
                    this.datapointList = new ExtensionCollection<Datapoint>(this);
                }
                return this.datapointList;
            }
        }
    }
}
