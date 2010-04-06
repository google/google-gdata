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
    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Match Extensions.</summary> 
    //////////////////////////////////////////////////////////////////////
    public class MatchCollection : CollectionBase  
    {
        /// <summary>holds the owning feed</summary>
        private AtomBase atomElement;

        private MatchCollection()
        {
        }

        /// <summary>constructor</summary> 
        public MatchCollection(AtomBase atomElement) : base()
        {
            this.atomElement = atomElement;
        }

        /// <summary>standard typed accessor method </summary> 
        public Match this[ int index ]  
        {
            get  
            {
                return( (Match) List[index] );
            }
            set  
            {
                this.atomElement.ExtensionElements.Remove((Match)List[index]);
                List[index] = value;
                this.atomElement.ExtensionElements.Add(value);
            }
        }

        /// <summary>standard typed add method </summary> 
        public int Add( Match value )  
        {
            this.atomElement.ExtensionElements.Add(value); 
            return( List.Add( value ) );
        }

        /// <summary>standard typed indexOf method </summary> 
        public int IndexOf( Match value )  
        {
            return( List.IndexOf( value ) );
        }
    
        /// <summary>standard typed insert method </summary> 
        public void Insert( int index, Match value )  
        {
            if (this.atomElement.ExtensionElements.Contains(value))
            {
                this.atomElement.ExtensionElements.Remove(value);
            }
            this.atomElement.ExtensionElements.Add(value);
            List.Insert( index, value );
        }

        /// <summary>standard typed remove method </summary> 
        public void Remove( Match value )  
        {
            this.atomElement.ExtensionElements.Remove(value);
            List.Remove( value );
        }

        /// <summary>standard typed Contains method </summary> 
        public bool Contains( Match value )  
        {
            // If value is not of type AtomEntry, this will return false.
            return( List.Contains( value ) );
        }

        /// <summary>standard typed OnValidate Override </summary> 
        protected override void OnValidate( Object value )  
        {
            if ( value as Match == null)
                throw new ArgumentException(
                    "value must be of type Google.GData.CodeSearch.Match.",
                    "value" );
        }

        /// <summary>standard override OnClear,
        ///  to remove the objects from the extension list</summary> 
        protected override void OnClear()  
        {
            for (int i=0; i< this.Count;i++)
            {
                this.atomElement.ExtensionElements.Remove((Match)List[i]);
            }
        }
    }


    /// <summary>
    /// GData schema extension describing a c:match
    /// Contains a line extension with the number of the line in which the
    /// match occured and a linetext element with the line itself.
    /// </summary>
    public class Match : IExtensionElementFactory 
    {
        /// <summary>
        /// holds the attribute for the line number in which the match happens
        /// </summary>
        private string linenumber;
        /// <summary>
        /// holds the actual line in which the match happens
        /// </summary>
        private String linetext;
        /// <summary>
        /// public available attribute with the actual line in which the match happens
        /// </summary>
        public String LineNumber
        {
            get {return linenumber;}
        }
        /// <summary>
        /// public available attribute for the line element
        /// </summary>
        public String LineTextElement
        {
            get {return linetext;}
        }

        #region Match Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a Match object.</summary> 
        /// <param name="node">Match node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns>the created Match object</returns>
        //////////////////////////////////////////////////////////////////////
        public static Match ParseMatch(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            Match match = null;
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
                if (localname.Equals(GCodeSearchParserNameTable.EVENT_MATCH))
                {
                    match = new Match();
                    if (node.Attributes != null)
                    {
                        match.linenumber =
                            node.Attributes[
                            GCodeSearchParserNameTable.ATTRIBUTE_LINE_NUMBER].Value;
                    }
                    match.linetext = node.InnerText;
                    }
                    else 
                    {
                        throw new ArgumentNullException(
                            BaseNameTable.gBatchNamespace +
                            ":" + GCodeSearchParserNameTable.EVENT_MATCH +
                            " must contain the attribute " +
                            GCodeSearchParserNameTable.ATTRIBUTE_LINE_NUMBER);
                    }
                }
            return match;
        }
        #endregion
        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing
        ///  this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public static string XmlName
        {
            get { return GCodeSearchParserNameTable.EVENT_MATCH; }
        }

        /// <summary>
        /// Persistence method for the Match object
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

            if (Utilities.IsPersistable(linenumber) &&
                Utilities.IsPersistable(linetext))
            {
                writer.WriteStartElement(XmlPrefix,
                    XmlName, XmlNameSpace);

                writer.WriteAttributeString(GCodeSearchParserNameTable.ATTRIBUTE_LINE_NUMBER,
                                            linenumber);
                writer.WriteAttributeString("type",
                                            "text/html");   
                writer.WriteString(this.linetext);
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
        /// <summary>Parses an xml node to create a Match object.</summary> 
        /// <param name="node">xml node</param>
        /// <param name="parser">the atomfeedparser to use for deep dive parsing</param>
        /// <returns>the created IExtensionElementFactory object</returns>
        //////////////////////////////////////////////////////////////////////
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            return ParseMatch(node, parser);
        }

        #endregion
    }
}