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
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Spreadsheets
{
    /// <summary>
    /// Entry API customization class for defining entries in a List feed.
    /// </summary>
    public class ListEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain Cell extension data.
        /// </summary>
        public static AtomCategory LIST_CATEGORY
        = new AtomCategory(GDataSpreadsheetsNameTable.List,
                           new AtomUri(BaseNameTable.gKind));

#region Schema Extensions

        /// <summary>
        /// GData schema extension describing a custom element in a spreadsheet.
        /// </summary>
        public class Custom : IExtensionElementFactory
        {
            private string localName;
            private string value;

            /// <summary>
            /// Constructs an empty custom element
            /// </summary>
            public Custom()
            {
                LocalName = null;
                Value = null;
            }

            /// <summary>
            /// The local name of the custom element
            /// </summary>
            public string LocalName
            {
                get
                {
                    return localName;
                }

                set
                {
                    localName = value;
                }
            }

            /// <summary>
            /// The value of the custom element
            /// </summary>
            public string Value
            {
                get
                {
                    return value;
                }

                set
                {
                    this.value = value;
                }
            }

            /// <summary>
            /// Parses an XML node to create a Custom object
            /// </summary>
            /// <param name="node">Custom node</param>
            /// <param name="parser">AtomFeedParser to use</param>
            /// <returns>The created Custom object</returns>
            public static Custom ParseCustom(XmlNode node, AtomFeedParser parser)
            {
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }

                Custom custom = new Custom();

                if (node.Attributes.Count > 1)
                {
                    throw new ArgumentException("Custom elements should have 0 attributes");
                }

                if (node.HasChildNodes && node.FirstChild.NodeType != XmlNodeType.Text)
                {
                    //    throw new ArgumentException("Custom elements should have 0 children");
                }

                custom.LocalName = node.LocalName;

                if (node.HasChildNodes)
                {
                    custom.Value = node.FirstChild.Value;
                }
                else
                {
                    custom.value = "";
                }

                return custom;
            }

#region overload for persistence
            /// <summary>
            /// Custom elements are equal if they have the same local name.
            /// </summary>
            /// <param name="value">The custom element to compare against.</param>
            /// <returns>True if the LocalNames are equal, false otherwise</returns>
            public override bool Equals(object value)
            {
                Custom newCustom = value as Custom;
                if (newCustom != null)
                {
                    return this.LocalName.Equals(newCustom.LocalName);
                }

                return false;
            }

            /// <summary>
            /// The hash code is simply the hash of the local name
            /// </summary>
            /// <returns>The hash code calculated by String on the LocalName</returns>
            public override int GetHashCode()
            {
                return this.LocalName.GetHashCode();
            }

            /// <summary>
            /// Returns the constant representing the XML element.
            /// </summary>
            public string XmlName
            {
                get
                {
                    return LocalName;
                }
            }

            /// <summary>
            /// Used to save the EntryLink instance into the passed in xmlwriter
            /// </summary>
            /// <param name="writer">the XmlWriter to write into</param>
            public void Save(XmlWriter writer)
            {
                if (LocalName != null && Value != null)
                {
                    writer.WriteStartElement(XmlPrefix,
                                             XmlName, XmlNameSpace);
                    writer.WriteString(Value);
                    writer.WriteEndElement();
                }
            }
#endregion

            #region IExtensionElementFactory Members


            public string XmlNameSpace
            {
                get
                {
                    return GDataSpreadsheetsNameTable.NSGSpreadsheetsExtended;
                }
            }

            public string XmlPrefix
            {
                get
                {
                    return GDataSpreadsheetsNameTable.ExtendedPrefix;
                }
            }

            public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
            {
                return ParseCustom(node, parser);
            }

            #endregion
        } // class Custom

#endregion

#region Collection

        //////////////////////////////////////////////////////////////////////
        /// <summary>Typed collection for Custom Extensions.</summary> 
        //////////////////////////////////////////////////////////////////////
        public class CustomElementCollection : CollectionBase  
        {
            /// <summary>holds the owning feed</summary>
            private AtomBase atomElement;

            private CustomElementCollection()
            {
            }

            /// <summary>constructor</summary> 
            public CustomElementCollection(AtomBase atomElement) : base()
            {
                this.atomElement = atomElement;
            }

            /// <summary>standard typed accessor method </summary> 
            public Custom this[ int index ]  
            {
                get  
                {
                    return( (Custom) List[index] );
                }
                set  
                {
                    this.atomElement.ExtensionElements.Remove((Custom)List[index]);
                    List[index] = value;
                    this.atomElement.ExtensionElements.Add(value);
                }
            }

            /// <summary>standard typed add method </summary> 
            public int Add( Custom value )  
            {
                this.atomElement.ExtensionElements.Add(value); 
                return( List.Add( value ) );
            }

            /// <summary>standard typed indexOf method </summary> 
            public int IndexOf( Custom value )  
            {
                return( List.IndexOf( value ) );
            }

            /// <summary>standard typed insert method </summary> 
            public void Insert( int index, Custom value )  
            {
                if (this.atomElement.ExtensionElements.Contains(value))
                {
                    this.atomElement.ExtensionElements.Remove(value);
                }
                this.atomElement.ExtensionElements.Add(value);
                List.Insert( index, value );
            }

            /// <summary>standard typed remove method </summary> 
            public void Remove( Custom value )  
            {
                this.atomElement.ExtensionElements.Remove(value);
                List.Remove( value );
            }

            /// <summary>standard typed Contains method </summary> 
            public bool Contains( Custom value )  
            {
                // If value is not of type AtomEntry, this will return false.
                return( List.Contains( value ) );
            }

            /// <summary>standard typed OnValidate Override </summary> 
            protected override void OnValidate( Object value )  
            {
                if (value as Custom == null)
                    throw new ArgumentException( "value must be of type Google.GData.Extensions.When.", "value" );
            }

            /// <summary>standard override OnClear, to remove the objects from the extension list</summary> 
            protected override void OnClear()  
            {
                for (int i=0; i< this.Count;i++)
                {
                    this.atomElement.ExtensionElements.Remove((Custom)List[i]);
                }
            }
        }  
#endregion

        private CustomElementCollection elements;

        /// <summary>
        /// Constructs a new ListEntry instance with the appropriate category
        /// to indicate that it is a list entry.
        /// </summary>
        public ListEntry() : base()
        {
            Categories.Add(LIST_CATEGORY);
            elements = new CustomElementCollection(this);
        }

        /// <summary>
        /// The custom elements in this list entry
        /// </summary>
        public CustomElementCollection Elements
        {
            get
            {
                return elements;
            }
        }

#region override persistence
        /// <summary>
        /// Parses the inner state of the element. TODO. 
        /// </summary>
        /// <param name="e">The extension element that should be added to this entry</param>
        /// <param name="parser">The AtomFeedParser that called this</param>
        public override void Parse(ExtensionElementEventArgs e, AtomFeedParser parser)  
        {
            if (String.Compare(e.ExtensionElement.NamespaceURI, GDataSpreadsheetsNameTable.NSGSpreadsheetsExtended, true) == 0)
            {
                Elements.Add(Custom.ParseCustom(e.ExtensionElement, parser));
                e.DiscardEntry = true;
            }
        }
    }
#endregion

}
