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
#region Using directives

#define USE_TRACING

using System;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions.MediaRss;

#endregion

//////////////////////////////////////////////////////////////////////
// contains typed collections based on the 1.1 .NET framework
// using typed collections has the benefit of additional code reliability
// and using them in the collection editor
// 
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Extensions
{

    /// <summary>
    /// base class to take an object pointer with extension information
    /// and expose a localname/namespace subset as a collection
    /// that still works on the original
    /// </summary>
    public abstract class ExtensionCollection<T> : CollectionBase where T : IExtensionElement
    {
             /// <summary>holds the owning feed</summary>
         private IExtensionContainer container;

         /// <summary>
         /// protected default constructor, not usable by outside
         /// </summary>
         protected ExtensionCollection()
         {
         }

        /// <summary>
        /// takes the base object, and the localname/ns combo to look for
        /// will copy objects to an internal array for caching. Note that when the external 
        /// ExtensionList is modified, this will have no effect on this copy
        /// </summary>
        /// <param name="containerElement">the base element holding the extension list</param>
        /// <param name="localName">the local name of the extension</param>
        /// <param name="ns">the namespace</param>
        public ExtensionCollection(IExtensionContainer containerElement, string localName, string ns) : base()
        {
             this.container = containerElement;
             if (this.container != null)
             {
                 ArrayList arr = this.container.FindExtensions(localName, ns); 
                 foreach (object o in arr )
                 {
                     List.Add(o);
                 }
             }
        }

           /// <summary>standard typed accessor method </summary> 
        public T this[ int index ]  
        {
            get  
            {
                return( (T) List[index] );
            }
            set  
            {
                setItem(index, value);
            }
        }

        /// <summary>
        /// useful for subclasses that want to overload the set method
        /// </summary>
        /// <param name="index">the index in the array</param>
        /// <param name="item">the item to set </param>
        protected void setItem(int index, T item)
        {
            if (List[index] != null)
            {
                if (this.container!=null)
                {
                    this.container.ExtensionElements.Remove(List[index]);
                }
            }
            List[index] = item;
            if (item != null && this.container != null)
            {
                this.container.ExtensionElements.Add(item);
            }
        }

      
        /// <summary>
        /// default untyped add implementation. Adds the object as well to the parent
        /// object ExtensionList
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(T value)
        {
            if (this.container != null)
            {
                this.container.ExtensionElements.Add(value); 
            }
            return( List.Add( value ) );
        }

        /// <summary>
        /// inserts an element into the collection by index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert( int index, T value )  
        {
            if (this.container != null && this.container.ExtensionElements.Contains(value))
            {
                this.container.ExtensionElements.Remove(value);
            }
            this.container.ExtensionElements.Add(value);
            List.Insert( index, value );
        }

        /// <summary>
        /// removes an element at a given index
        /// </summary>
        /// <param name="value"></param>
        public void Remove( T value )  
        {
            if (this.container != null)
            {
                this.container.ExtensionElements.Remove(value);
            }
            List.Remove( value );
        }

         /// <summary>standard typed indexOf method </summary>
        public int IndexOf(T value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>standard typed Contains method </summary> 
        public bool Contains(T value)
        {
            // If value is not of type AtomEntry, this will return false.
            return (List.Contains(value));
        }

        /// <summary>standard override OnClear, to remove the objects from the extension list</summary> 
        protected override void OnClear()  
        {
            if (this.container != null)
            {
                for (int i=0; i< this.Count;i++)
                {
                    this.container.ExtensionElements.Remove(List[i]);
                }
            }
        }
    }



    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for When Extensions.</summary> 
    //////////////////////////////////////////////////////////////////////
    public class ReminderCollection : ExtensionCollection<Reminder>
    {
        private ReminderCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public ReminderCollection(IExtensionContainer atomElement) 
            : base(atomElement, GDataParserNameTable.XmlReminderElement, BaseNameTable.gNamespace)
        {
        }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for When Extensions.</summary> 
    //////////////////////////////////////////////////////////////////////
    public class WhenCollection : ExtensionCollection<When>
    {
        private WhenCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public WhenCollection(IExtensionContainer atomElement) 
            : base(atomElement, GDataParserNameTable.XmlWhenElement, BaseNameTable.gNamespace)
        {
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Where Extensions.</summary> 
    //////////////////////////////////////////////////////////////////////
    public class WhereCollection : ExtensionCollection<Where>
    {

        private WhereCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public WhereCollection(IExtensionContainer atomElement) 
            : base(atomElement, GDataParserNameTable.XmlWhereElement, BaseNameTable.gNamespace)
        {
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Who Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class WhoCollection : ExtensionCollection<Who>
    {
        private WhoCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public WhoCollection(IExtensionContainer atomElement) 
            : base(atomElement, GDataParserNameTable.XmlWhoElement, BaseNameTable.gNamespace)
        {
        }
    }
    /////////////////////////////////////////////////////////////////////////////

     //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Email Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class EMailCollection : ExtensionCollection<EMail>
    {
        private EMailCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public EMailCollection(IExtensionContainer atomElement) 
            : base(atomElement, GDataParserNameTable.XmlEmailElement, BaseNameTable.gNamespace)
        {
        }
    }
    /////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for IMAddresses Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class IMCollection : ExtensionCollection<IMAddress>
    {
        private IMCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public IMCollection(IExtensionContainer atomElement) 
            : base(atomElement, GDataParserNameTable.XmlIMElement, BaseNameTable.gNamespace)
        {
        }
    }
    /////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Phonenumbers Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class PhonenumberCollection : ExtensionCollection<PhoneNumber>
    {
        private PhonenumberCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public PhonenumberCollection(IExtensionContainer atomElement) 
            : base(atomElement, GDataParserNameTable.XmlPhoneNumberElement, BaseNameTable.gNamespace)
        {
        }
    }
    /////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for PostalAddresses Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class PostalAddressCollection : ExtensionCollection<PostalAddress>
    {
        private PostalAddressCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public PostalAddressCollection(IExtensionContainer atomElement) 
            : base(atomElement, GDataParserNameTable.XmlPostalAddressElement, BaseNameTable.gNamespace)
        {
        }
    }
    /////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Who Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class OrganizationCollection : ExtensionCollection<Organization>
    {
        private OrganizationCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public OrganizationCollection(IExtensionContainer atomElement) 
            : base(atomElement, GDataParserNameTable.XmlOrganizationElement, BaseNameTable.gNamespace)
        {
        }
    }
    /////////////////////////////////////////////////////////////////////////////

   
} 
