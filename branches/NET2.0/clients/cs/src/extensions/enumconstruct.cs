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

namespace Google.GData.Extensions {

    /// <summary>
    /// Extensible enum type used in many places.
    /// </summary>
    public abstract class EnumConstruct : IExtensionElement
    {

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="enumType">the type used to initialize</param>
        protected EnumConstruct(string enumType)
        {
            this.enumType = enumType;
            readOnly = false;
        }

        /// <summary>
        /// Creates a new EnumConstruct instance with a specific type and value.
        /// When this constructor is used the instance has a constant value and
        /// may not be modified by the setValue() API.
        /// </summary>
        /// <param name="enumType">the type used to initialize</param>
        /// <param name="initialValue">the initial value of the type</param>
        protected EnumConstruct(string enumType, string initialValue)
        {
            this.enumType = enumType;
            this.value = initialValue;
            readOnly = true;
        }

        /// <summary>
        /// Construct value cannot be changed
        /// </summary>
        private bool readOnly;

        /// <summary>
        ///  holds the enumType property
        /// </summary>
        protected string enumType = null;

        /// <summary>
        ///  Accessor Method for the enumType
        /// </summary>
        public string Type
        {
            get { return enumType; }
        }

        /// <summary>
        /// String Value
        /// </summary>
        protected string value;

        /// <summary>
        ///  Accessor Method for the value
        /// </summary>
        public string Value
        {
            get { return value; }
            set 
            {
                if (readOnly)
                {
                    throw new ArgumentException(enumType + " instance is read only");
                }
                this.value = value;
            }
        }

        /// <summary>
        ///  Equal operator overload
        /// </summary>
        /// <param name="o">the object to compare to</param>
        /// <returns>bool</returns>
        public override bool Equals(Object o)
        {
            //
            // Two EnumConstant instances are considered equal of they are of the
            // same concrete subclass and have the same type/value strings.  If
            // a subtype adds additional member elements that effect the equivalence  
            // test, it *must* override this implemention.
            //
            if (o == null || !this.GetType().Equals(o.GetType()))
            {
                return false;
            }

            EnumConstruct ec = (EnumConstruct)o;
            return Type.Equals(ec.Type) && value.Equals(ec.value);
        }
        
        /// <summary>
        ///  GetHashCode overload
        /// </summary>
        /// <returns>a hash based on the string value</returns>
        public override int GetHashCode()
        {
            // the hashcode for an enum will be derived by it's value          
            return value != null ? value.GetHashCode() : 0;
        }

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return Type; }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public virtual string XmlNamespace
        {
            get { return BaseNameTable.gNamespace; }
        }
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public virtual string XmlNamespacePrefix
        {
            get { return BaseNameTable.gDataPrefix; }
        }


        /// <summary>
        /// Persistence method for the EnumConstruct object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.value))
            {
                
                writer.WriteStartElement(XmlNamespacePrefix, XmlName, XmlNamespace); 
                writer.WriteAttributeString(BaseNameTable.XmlValue, this.value);
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}   
