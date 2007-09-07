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
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
using System.Globalization;

namespace Google.GData.Extensions {

    /// <summary>
    /// base class to implement extensions holding extensions
    /// </summary>
    public class SimpleContainer : SimpleElement
    {
        private ArrayList extensions;
        private ArrayList extensionFactories;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        protected SimpleContainer(string name, string prefix, string ns) : base(name, prefix, ns)
        {
        }

        /// <summary>
        /// copy constructor, used in parsing
        /// </summary>
        /// <param name="original"></param>
        protected SimpleContainer(SimpleContainer original) : base(original.XmlName, original.XmlPrefix, original.XmlNameSpace)
        {
    
            this.ExtensionFactories = original.ExtensionFactories;
        }

       
        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>the list of extensions for this container
        /// the elements in that list MUST implement IExtensionElementFactory 
        /// and IExtensionElement</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public ArrayList ExtensionElements
        {
            get 
            {
                if (this.extensions == null)
                {
                    this.extensions = new ArrayList();
                }
                return this.extensions;
            }
            set {this.extensions = value;}
        }

        /// <summary>
        /// Finds a specific ExtensionElement based on it's local name
        /// and it's namespace. If namespace is NULL, the first one where
        /// the localname matches is found. If there are extensionelements that do 
        /// not implment ExtensionElementFactory, they will not be taken into account
        /// </summary>
        /// <param name="localName">the xml local name of the element to find</param>
        /// <param name="ns">the namespace of the elementToPersist</param>
        /// <returns>Object</returns>
        public Object FindExtension(string localName, string ns) 
        {
            return Utilities.FindExtension(this.extensions, localName, ns);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>the list of extensions for this container
        /// the elements in that list MUST implement IExtensionElementFactory 
        /// and IExtensionElement</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public ArrayList ExtensionFactories
        {
            get 
            {
                if (this.extensionFactories == null)
                {
                    this.extensionFactories = new ArrayList();
                }
                return this.extensionFactories;
            }
            set {this.extensionFactories = value;}
        }

        // end of accessor public ArrayList Extensions
        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a Who object.</summary> 
        /// <param name="node">georsswhere node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns>the created SimpleElement object</returns>
        //////////////////////////////////////////////////////////////////////
        public override IExtensionElement CreateInstance(XmlNode node, AtomFeedParser parser) 
        {
            Tracing.TraceCall("for: " + XmlName);
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            SimpleContainer sc = null;
            
            object localname = node.LocalName;
            if (localname.Equals(this.XmlName))
            {
                // create a new container
                sc = new SimpleContainer(this);
            }

            if (node.HasChildNodes)
            {
                XmlNode childNode = node.FirstChild;
                while (childNode != null && childNode is XmlElement)
                {
                    foreach (IExtensionElementFactory f in this.ExtensionFactories)
                    {
                        if (String.Compare(childNode.NamespaceURI, f.XmlNameSpace) == 0)
                        {
                            if (String.Compare(childNode.LocalName, f.XmlName) == 0)
                            {
                                Tracing.TraceMsg("Added extension to SimpleContainer for: " + f.XmlName);
                                sc.ExtensionElements.Add(f.CreateInstance(childNode, parser));
                                break;
                            }
                        }
                    }
                    childNode = childNode.NextSibling;
                }
            }

            return sc;
        }

        
        /// <summary>
        /// Persistence method for the EnumConstruct object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(XmlPrefix, XmlName, XmlNameSpace);
            if (this.extensions != null)
            {
                foreach (IExtensionElement e in this.ExtensionElements)
                {
                    e.Save(writer);
                }
            }
            writer.WriteEndElement();
        }
        #endregion
    }
}  
