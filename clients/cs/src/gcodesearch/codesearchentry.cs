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
using System.IO; 
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.CodeSearch 
{
    /// <summary>
    /// Entry API customization class for defining entries in an CodeSearch feed.
    /// </summary>
    public class CodeSearchEntry : AbstractEntry
    { 
        /// <summary>
        /// Category used to label entries that contain CodeSearch
        ///  extension data.
        /// </summary>
        public static AtomCategory CODESEARCH_CATEGORY =
            new AtomCategory(GDataParserNameTable.Event,
            new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new CodeSearchEntry instance with
        ///  the appropriate category.
        /// </summary>
        public CodeSearchEntry()
            : base()
        {
            Categories.Add(CODESEARCH_CATEGORY);
            matches = new MatchCollection(this);
        }
        private File file;
        /// <summary>
        ///  property accessor for the File element
        /// </summary>
        public File FileElement 
        {
            get {return file;}
        }
        private Package package;
        /// <summary>
        ///  property accessor for the Package element
        /// </summary>
        public Package PackageElement
        {
            get {return package;}
        }
        private MatchCollection matches;
        /// <summary>
        ///  property accessor for the MatchCollection
        /// </summary>
        public MatchCollection Matches
        {
            get { return matches;}
        }

       //////////////////////////////////////////////////////////////////////
        /// <summary>parses the inner state of the element</summary>
        /// <param name="e">the Event arguments</param>
        /// <param name="parser">the atomFeedParser that called this</param>
        //////////////////////////////////////////////////////////////////////
        public override void Parse(ExtensionElementEventArgs e, AtomFeedParser parser)
        {
            XmlNode eventNode = e.ExtensionElement;

            Tracing.TraceMsg(eventNode.LocalName);
            // Ensure that the namespace is correct.
            if (String.Compare(eventNode.NamespaceURI,
                GCodeSearchParserNameTable.CSNamespace, true) == 0)
            {
                // Parse a File Element
                if (eventNode.LocalName == GCodeSearchParserNameTable.EVENT_FILE)
                {
                    file = File.ParseFile(eventNode, parser);
                    e.DiscardEntry = true;
                }
                    // Parse a Package Element
                else if ((eventNode.LocalName == GCodeSearchParserNameTable.EVENT_PACKAGE))
                {
                    package = Package.ParsePackage(eventNode, parser);
                    e.DiscardEntry = true;
                }
                    // Parse Match Elements 
                else if (eventNode.LocalName == GCodeSearchParserNameTable.EVENT_MATCH)
                {
                    matches.Add(Match.ParseMatch(eventNode, parser));
                    e.DiscardEntry = true;
                } 
            }
        }
    }
}