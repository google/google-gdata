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
    /// Extensible type used in many places.
    /// </summary>
    public abstract class ExtensionBase : IExtensionElement, IExtensionElementFactory
    {

        private string xmlName;
        private string xmlPrefix;
        private string xmlNamespace;
        /// <summary>
        /// this holds the attribute list for an extension element
        /// </summary>
        protected SortedList attributes;



        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        protected ExtensionBase(string name, string prefix, string ns)
        {
            this.xmlName = name;
            this.xmlPrefix = prefix;
            this.xmlNamespace = ns;
        }

        /// <summary>
        /// returns the attributes list
        /// </summary>
        /// <returns>SortedList</returns>
        internal SortedList getAttributes()
        {
            if (this.attributes == null)
            {
                this.attributes = new SortedList();
            }
            return this.attributes;
        }
        
        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return this.xmlName; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlNameSpace
        {
            get { return this.xmlNamespace; }
        }
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlPrefix
        {
            get { return this.xmlPrefix; }
        }

        /// <summary>
        /// debugging helper
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + " for: " + XmlNameSpace + "- " + XmlName;
        }


         //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a Who object.</summary> 
        /// <param name="node">the xml parses node, can be NULL</param>
        /// <param name="parser">the xml parser to use if we need to dive deeper</param>
        /// <returns>the created IExtensionElement object</returns>
        //////////////////////////////////////////////////////////////////////
        public virtual IExtensionElement CreateInstance(XmlNode node, AtomFeedParser parser) 
        {
            Tracing.TraceCall();

            ExtensionBase e = null;

            if (node != null)
            {
                object localname = node.LocalName;
                if (localname.Equals(this.XmlName) == false ||
                    node.NamespaceURI.Equals(this.XmlNameSpace) == false)
                {
                    return null;
                }
            }
            
            // memberwise close is fine here, as everything is identical beside the value
            e = this.MemberwiseClone() as ExtensionBase;
            e.InitInstance(this);
            if (node.Attributes != null)
            {
                e.ProcessAttributes(node);
            }
            return e;
        }
  
        /// <summary>
        /// used to copy the attribute lists over
        /// </summary>
        /// <param name="factory"></param>
        internal void InitInstance(ExtensionBase factory)
        {
            this.attributes = null;
            for (int i=0; i < factory.getAttributes().Count; i++)
            {
                string name = factory.getAttributes().GetKey(i) as string;
                string value = factory.getAttributes().GetByIndex(i) as string;
                this.getAttributes().Add(name, value);
            }
        }

        /// <summary>
        /// default method override to handle attribute processing
        /// the base implementation does process the attributes list
        /// and reads all that are in there.
        /// </summary>
        /// <param name="node">XmlNode with attributes</param>
        public virtual void ProcessAttributes(XmlNode node)
        {
            if (node != null)
            {
                for (int i = 0; i < node.Attributes.Count; i++)
                {
                    this.getAttributes()[node.Attributes[i].LocalName] = node.Attributes[i].Value; 
                }
            }
            return;
        }

        /// <summary>
        /// helper method to extract an attribute as string from
        /// an xmlnode using the passed in node and the attributename
        /// </summary>
        /// <param name="node">node to extract attribute from</param>
        /// <param name="attributeName">the name of the attribute</param>
        /// <returns>string - null if attribute is not present</returns>
        protected string getAttribute(XmlNode node, string attributeName)
        { 
            string retValue = null;

            if (node.Attributes != null)
            {
                if (node.Attributes[attributeName] != null)
                {
                    retValue = node.Attributes[attributeName].Value;
                }
            }
            return retValue;
        }
      
        /// <summary>
        /// Persistence method for the EnumConstruct object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public virtual void Save(XmlWriter writer)
        {
            writer.WriteStartElement(XmlPrefix, XmlName, XmlNameSpace);
            if (this.attributes != null)
            {
                for (int i=0; i < this.getAttributes().Count; i++)
                {
                    if (this.getAttributes().GetByIndex(i) != null)
                    {
                        string name = this.getAttributes().GetKey(i) as string;
                        string value = Utilities.ConvertToXSDString(this.getAttributes().GetByIndex(i));
                        writer.WriteAttributeString(name, value);
                    }
                }
            }
            SaveInnerXml(writer);

            writer.WriteEndElement();
        }

        /// <summary>
        /// a subclass that want's to save addtional XML would need to overload this
        /// the default implementation does nothing
        /// </summary>
        /// <param name="writer"></param>
        public virtual void SaveInnerXml(XmlWriter writer)
        {
        }
        #endregion
    }
}  
