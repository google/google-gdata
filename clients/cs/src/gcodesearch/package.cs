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
    /// GData schema extension describing a c:package
    /// Contains a filename extension with the number of the package
    /// and the packageuri extension with the its procedence.
    /// </summary>
    public class Package : IExtensionElementFactory 
    {
        /// <summary>
        /// holds the name fo the package
        /// </summary>
        private String name;
        /// <summary>
        /// holds the uri of the package
        /// </summary>
        private String uri;
        /// <summary>
        /// public available attribute to hold the name of the package
        /// </summary>
        public String Name
        {
            get {return name;}
        }
        /// <summary>
        ///  public available attribute to hold the uri of the package
        /// </summary>
        public String Uri
        {
            get {return uri;}
        }

        #region Package Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a Package object.</summary> 
        /// <param name="node">Package node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns>the created Package object</returns>
        //////////////////////////////////////////////////////////////////////
        public static Package ParsePackage(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            Package package = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            // Ensure that the namespace is correct.
            if (String.Compare(node.NamespaceURI,
                GCodeSearchParserNameTable.CSNamespace, true) == 0)
            {
                if (localname.Equals(GCodeSearchParserNameTable.EVENT_PACKAGE))
                {
                    package = new Package();
                    if (node.Attributes != null)
                    {
                        package.name =
                            node.Attributes[
                            GCodeSearchParserNameTable.ATTRIBUTE_NAME].Value;
                        package.uri =
                            node.Attributes[
                            GCodeSearchParserNameTable.ATTRIBUTE_URI].Value;
                    }
                    else 
                    {
                        throw new ArgumentNullException(
                            BaseNameTable.gBatchNamespace +
                            ":" + GCodeSearchParserNameTable.EVENT_PACKAGE +
                            " must contain the attributes " +
                            GCodeSearchParserNameTable.ATTRIBUTE_NAME + " and " +
                            GCodeSearchParserNameTable.ATTRIBUTE_URI);
                    }
                }
            }
            return package;
        }
        #endregion
        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public static string XmlName
        {
            get { return GCodeSearchParserNameTable.EVENT_PACKAGE; }
        }

        /// <summary>
        /// Persistence method for the Package object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        /// 
        public void Save(XmlWriter writer)
        {
            Tracing.Assert(writer != null, "writer should not be null");
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (Utilities.IsPersistable(name) &&
                Utilities.IsPersistable(uri))
            {
                writer.WriteStartElement(XmlPrefix,
                    XmlName, XmlNameSpace);
                writer.WriteAttributeString(GCodeSearchParserNameTable.ATTRIBUTE_NAME,
                                            name);
                writer.WriteAttributeString(GCodeSearchParserNameTable.ATTRIBUTE_URI,
                                            uri);
                writer.WriteEndElement();

            }
            else
            {
                throw new ArgumentNullException(GCodeSearchParserNameTable.CSPrefix +
                    ":" + XmlName + " is required.");
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
        /// <summary>Parses an xml node to create a Package object.</summary> 
        /// <param name="node">xml node</param>
        /// <param name="parser">the atomfeedparser to use for deep dive parsing</param>
        /// <returns>the created IExtensionElementFactory object</returns>
        //////////////////////////////////////////////////////////////////////
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            return ParsePackage(node, parser);
        }

        #endregion
    }
}