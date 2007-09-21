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
    public class ExtensionCollection : CollectionBase 
    {
             /// <summary>holds the owning feed</summary>
         private AtomBase atomElement;

         protected ExtensionCollection()
         {
         }

        /// <summary>
        /// takes the base object, and the localname/ns combo to look for
        /// will copy objects to an internal array for caching. Note that when the external 
        /// ExtensionList is modified, this will have no effect on this copy
        /// </summary>
        /// <param name="atomElement">the base element holding the extension list</param>
        /// <param name="localName">the local name of the extension</param>
        /// <param name="ns">the namespace</param>
        public ExtensionCollection(AtomBase atomElement, string localName, string ns) : base()
        {
             this.atomElement = atomElement;
             ArrayList arr = atomElement.FindExtensions(localName, ns); 
             foreach (object o in arr )
             {
                 List.Add(o);
             }
        }

        protected void setItem(int index, object item)
        {
            if (List[index] != null)
            {
                this.atomElement.ExtensionElements.Remove(List[index]);
            }
            List[index] = item;
            if (item != null)
            {
                this.atomElement.ExtensionElements.Add(item);
            }
        }

      
        public int Add(object value)
        {
            this.atomElement.ExtensionElements.Add(value); 
            return( List.Add( value ) );
        }

        public void Insert( int index, object value )  
        {
            if (this.atomElement.ExtensionElements.Contains(value))
            {
                this.atomElement.ExtensionElements.Remove(value);
            }
            this.atomElement.ExtensionElements.Add(value);
            List.Insert( index, value );
        }

        public void Remove( object value )  
        {
            this.atomElement.ExtensionElements.Remove(value);
            List.Remove( value );
        }

        /// <summary>standard override OnClear, to remove the objects from the extension list</summary> 
        protected override void OnClear()  
        {
            for (int i=0; i< this.Count;i++)
            {
                this.atomElement.ExtensionElements.Remove((When)List[i]);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for When Extensions.</summary> 
    //////////////////////////////////////////////////////////////////////
    public class WhenCollection : ExtensionCollection  
    {
        private WhenCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public WhenCollection(AtomBase atomElement) 
            : base(atomElement, GDataParserNameTable.XmlWhenElement, BaseNameTable.gNamespace)
        {
        }

        /// <summary>standard typed accessor method </summary> 
        public When this[ int index ]  
        {
            get  
            {
                return( (When) List[index] );
            }
            set  
            {
                setItem(index, value);
            }
        }

        /// <summary>standard typed add method </summary> 
        public int Add( When value )  
        {
            return base.Add(value);
       }

        /// <summary>standard typed indexOf method </summary> 
        public int IndexOf( When value )  
        {
            return( List.IndexOf( value ) );
        }
    
        /// <summary>standard typed insert method </summary> 
        public void Insert( int index, When value )  
        {
            base.Insert(index, value);
        }

        /// <summary>standard typed remove method </summary> 
        public void Remove( When value )  
        {
            base.Remove(value);
        }

        /// <summary>standard typed Contains method </summary> 
        public bool Contains( When value )  
        {
            // If value is not of type AtomEntry, this will return false.
            return( List.Contains( value ) );
        }

        /// <summary>standard typed OnValidate Override </summary> 
        protected override void OnValidate( Object value )  
        {
            if ( value as When == null)
                throw new ArgumentException( "value must be of type Google.GData.Extensions.When.", "value" );
        }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Where Extensions.</summary> 
    //////////////////////////////////////////////////////////////////////
    public class WhereCollection : ExtensionCollection
    {

        private WhereCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public WhereCollection(AtomBase atomElement) 
            : base(atomElement, GDataParserNameTable.XmlWhereElement, BaseNameTable.gNamespace)
        {
        }


        /// <summary>standard typed accessor method </summary>
        public Where this[int index]
        {
            get
            {
                return ((Where)List[index]);
            }
            set
            {
                setItem(index, value);
            }
        }

        /// <summary>standard typed add method </summary>
        public int Add(Where value)
        {
            return base.Add(value);
        }

        /// <summary>standard typed indexOf method </summary>
        public int IndexOf(Where value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>standard typed insert method </summary>
        public void Insert(int index, Where value)
        {
            base.Insert(index, value);
            
        }

        /// <summary>standard typed remove method </summary>
        public void Remove(Where value)
        {
            base.Remove(value);
        }

        /// <summary>standard typed Contains method </summary>
        public bool Contains(Where value)
        {
            // If value is not of type AtomEntry, this will return false.
            return (List.Contains(value));
        }

        /// <summary>standard typed OnValidate Override </summary>
        protected override void OnValidate(Object value)
        {
            if (value as Where == null)
                throw new ArgumentException("value must be of type Google.GData.Extensions.Where.", "value");
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Who Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class WhoCollection : ExtensionCollection
    {
        /// <summary>holds the owning feed</summary> 
        private AtomBase atomElement;

        private WhoCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public WhoCollection(AtomBase atomElement) 
            : base(atomElement, GDataParserNameTable.XmlWhoElement, BaseNameTable.gNamespace)
        {
        }

        /// <summary>standard typed accessor method </summary>
        public Who this[int index]
        {
            get
            {
                return ((Who)List[index]);
            }
            set
            {
                setItem(index,value);
            }
        }

        /// <summary>standard typed add method </summary>
        public int Add(Who value)
        {
            return base.Add(value);
        }

        /// <summary>standard typed indexOf method </summary>
        public int IndexOf(Who value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>standard typed insert method </summary>
        public void Insert(int index, Who value)
        {
            base.Insert(index, value);
        }

        /// <summary>standard typed remove method </summary> 
        public void Remove(Who value)
        {
            base.Remove(value);
        }

        /// <summary>standard typed Contains method </summary> 
        public bool Contains(Who value)
        {
            // If value is not of type AtomEntry, this will return false.
            return (List.Contains(value));
        }

        /// <summary>standard typed OnValidate Override </summary> 
        protected override void OnValidate(Object value)
        {
            if (value as Who == null)
                throw new ArgumentException("value must be of type Google.GData.Extensions.Who.", "value");
        }
    }
    /////////////////////////////////////////////////////////////////////////////

} 
