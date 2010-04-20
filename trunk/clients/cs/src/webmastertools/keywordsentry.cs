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

/* 
 * Created by Morten Christensen, http://blog.sitereactor.dk | http://twitter.com/sitereactor
 */

using System;
using Google.GData.Client;

namespace Google.GData.WebmasterTools
{
    public class KeywordsEntry : WebmasterToolsBaseEntry
    {
        /// <summary>
        /// Constructs a new KeywordsEntry instance
        /// </summary>
        public KeywordsEntry() : base()
        {
            Tracing.TraceMsg("Created KeywordsEntry");

            this.AddExtension(new Keyword());
        }

        /// <summary>
        /// getter/setter for Keyword subelement
        /// </summary>
        public Keyword Keyword
        {
            get
            {
                return getWebmasterToolsExtension(WebmasterToolsNameTable.XmlKeywordElement) as
                    Keyword;
            }
            set
            {
                ReplaceExtension(WebmasterToolsNameTable.XmlKeywordElement, WebmasterToolsNameTable.gWebmasterToolsNamspace, value);
            }
        }
    }
}
