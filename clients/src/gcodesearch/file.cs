/* Copyright (c) 2006-2008 Google Inc.
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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/
using System;
using System.Xml;
using System.Collections;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.CodeSearch 
{
    /// <summary>
    /// GData schema extension describing a c:file
    /// Contains a name extension with the name of the file.
    /// </summary>
    public class File : IExtensionElementFactory 
    {   /// <summary>
        /// holds the attribute for the name of the file
        /// </summary>
        protected String filename;
        /// <summary>
        /// public available attribute for the name of the file
        /// </summary>
        public String Name
        {
            get {return filename;}
        }

        #region File Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a File object.</summary> 
        /// <param name="node">File node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns>the created File object</returns>
        //////////////////////////////////////////////////////////////////////
        public static File ParseFile(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            File file = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            // Ensure that the namespace is correct.
            if (String.Compare(node.NamespaceURI,
                GCodeSearchParserNameTable.CSNamespace, true) == 0)
            {
                file = new File();
                if (node.Attributes != null)
                {
                    file.filename =
                        node.Attributes[
                        GCodeSearchParserNameTable.ATTRIBUTE_NAME].Value;
                }
                else 
                {
                    throw new ArgumentNullException(
                        BaseNameTable.gBatchNamespace +
                        ":" + GCodeSearchParserNameTable.EVENT_FILE + " must contain exactly one "
                        + GCodeSearchParserNameTable.ATTRIBUTE_NAME + " attribute");
                }
            }
            return file;
        }
        #endregion
        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing
        ///  this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public static string XmlName
        {
            get { return GCodeSearchParserNameTable.EVENT_FILE; }
        }

        /// <summary>
        /// Persistence method for the FileName object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            Tracing.Assert(writer != null, "writer should not be null");
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (Utilities.IsPersistable(filename))
            {
                writer.WriteStartElement(XmlPrefix,
                                         XmlName, XmlNameSpace);
                writer.WriteAttributeString(GCodeSearchParserNameTable.ATTRIBUTE_NAME,
                    filename);
                writer.WriteEndElement();

            }
            else
            {
                throw new ArgumentNullException("Attribute " +
                    GCodeSearchParserNameTable.ATTRIBUTE_NAME +" is required.");
            }
        }
        #endregion


        #region IExtensionElementFactory Members

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        string IExtensionElementFactory.XmlName
        {
            get
            {
                return XmlName;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlNameSpace
        {
            get
            {
                return GCodeSearchParserNameTable.CSNamespace;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlPrefix
        {
            get
            {
                return GCodeSearchParserNameTable.CSPrefix;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a File object.</summary> 
        /// <param name="node">xml node</param>
        /// <param name="parser">the atomfeedparser to use for deep dive parsing</param>
        /// <returns>the created IExtensionElementFactory object</returns>
        //////////////////////////////////////////////////////////////////////
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            return ParseFile(node, parser);
        }

        #endregion
    }
}