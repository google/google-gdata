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
    /// Extensible enum type used in many places.
    /// </summary>
    public abstract class SimpleElement : IExtensionElement, IExtensionElementFactory
    {

        private string xmlName;
        private string xmlPrefix;
        private string xmlNamespace;
        private string value;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        protected SimpleElement(string name, string prefix, string ns)
        {
            this.xmlName = name;
            this.xmlPrefix = prefix;
            this.xmlNamespace = ns;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        /// <param name="value">the intial value</param>
        protected SimpleElement(string name, string prefix, string ns, string value)
        {
            this.xmlName = name;
            this.xmlPrefix = prefix;
            this.xmlNamespace = ns;
            this.value = value;
        }

 
        /// <summary>
        ///  Accessor Method for the value
        /// </summary>
        public string Value
        {
            get { return value; }
            set { this.value = value;}
        }

        /// <summary>
        ///  Accessor Method for the value as integer
        /// </summary>
        public int IntegerValue
        {
            get { return Convert.ToInt32(value, CultureInfo.InvariantCulture); }
            set { this.value = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        ///  Accessor Method for the value as float
        /// </summary>
        public double FloatValue
        {
            get { return Convert.ToDouble(value, CultureInfo.InvariantCulture); }
            set { this.value = value.ToString(CultureInfo.InvariantCulture); }
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

        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create a Who object.</summary> 
        /// <param name="node">georsswhere node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns>the created SimpleElement object</returns>
        //////////////////////////////////////////////////////////////////////
        public IExtensionElement CreateInstance(XmlNode node, AtomFeedParser parser) 
        {
            Tracing.TraceCall();
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            SimpleElement e = null;
            
            object localname = node.LocalName;
            if (localname.Equals(this.XmlName))
            {
                // memberwise close is fine here, as everything is identical beside the value
                e = this.MemberwiseClone() as SimpleElement;
                e.Value = node.InnerText;
            }

            return e;
        }
      
        /// <summary>
        /// Persistence method for the EnumConstruct object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.value))
            {
                writer.WriteElementString(XmlName, XmlNameSpace, this.value);
             }
        }
        #endregion
    }
}  
