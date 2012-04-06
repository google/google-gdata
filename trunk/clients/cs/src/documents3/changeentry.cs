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
#define USE_TRACING

using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.AccessControl;
using System.Globalization;

namespace Google.GData.Documents {
    public class ChangeEntry : DocumentEntry {
        /// <summary>
        /// Constructs a new ChangeEntry instance.
        /// </summary>
        public ChangeEntry()
            : base() {
            this.AddExtension(new Changestamp());
        }

        /// <summary>
        /// Changestamp.
        /// </summary>
        /// <returns></returns>
        public string Changestamp {
            get {
                return GetStringValue<Changestamp>(DocumentslistNametable.Changestamp,
                    DocumentslistNametable.NSDocumentslist);
            }
        }
    }

    public class Changestamp : SimpleAttribute {
        /// <summary>
        /// default constructor for docs:changestamp 
        /// </summary>
        public Changestamp()
            : base(DocumentslistNametable.Changestamp,
            DocumentslistNametable.Prefix,
            DocumentslistNametable.NSDocumentslist) {
        }
    }
}

