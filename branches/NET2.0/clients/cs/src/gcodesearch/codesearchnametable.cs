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

using Google.GData.Extensions;

//////////////////////////////////////////////////////////////////////
// <summary>Contains the CodeSearch NameTable.</summary> 
//////////////////////////////////////////////////////////////////////
namespace Google.GData.CodeSearch 
{
    /// <summary>
    /// Subclass of the nametable, has the extensions for the GNamespace
    /// </summary>
    public class GCodeSearchParserNameTable : GDataParserNameTable
    {
        /// <summary>Google Codesearch namespace</summary> 
        public const string CSNamespace =
            "http://schemas.google.com/codesearch/2006";
        /// <summary>prefix for codesearch</summary> 
        public const string CSPrefix = "gcs";
        /// <summary> File element.</summary>
        public const string EVENT_FILE = "file";
        /// <summary> Uri attribute.</summary>
        public const string ATTRIBUTE_URI = "uri";
        /// <summary> FileName attribute.</summary>
        public const string ATTRIBUTE_NAME = "name";
        /// <summary> Line attribute.</summary>
        public const string ATTRIBUTE_LINE_NUMBER = "lineNumber";
        /// <summary> LineText element.</summary>
        public const string EVENT_LINETEXT = "linetext";
        /// <summary> Match element.</summary>
        public const string EVENT_MATCH = "match";
        /// <summary> Package element.</summary>
        public const string EVENT_PACKAGE = "package";
    }
        
}