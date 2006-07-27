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

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for When Extensions.</summary> 
    //////////////////////////////////////////////////////////////////////
    public class WhenCollection : CollectionBase  
    {
         /// <summary>holds the owning feed</summary>
         private AtomBase atomElement;

         private WhenCollection()
         {
         }

        /// <summary>constructor</summary> 
        public WhenCollection(AtomBase atomElement) : base()
        {
             this.atomElement = atomElement;
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
                this.atomElement.ExtensionElements.Remove((When)List[index]);
                List[index] = value;
                this.atomElement.ExtensionElements.Add(value);
            }
        }

        /// <summary>standard typed add method </summary> 
        public int Add( When value )  
        {
            this.atomElement.ExtensionElements.Add(value); 
            return( List.Add( value ) );
        }

        /// <summary>standard typed indexOf method </summary> 
        public int IndexOf( When value )  
        {
            return( List.IndexOf( value ) );
        }
    
        /// <summary>standard typed insert method </summary> 
        public void Insert( int index, When value )  
        {
            if (this.atomElement.ExtensionElements.Contains(value))
            {
                this.atomElement.ExtensionElements.Remove(value);
            }
            this.atomElement.ExtensionElements.Add(value);
            List.Insert( index, value );
        }

        /// <summary>standard typed remove method </summary> 
        public void Remove( When value )  
        {
            this.atomElement.ExtensionElements.Remove(value);
            List.Remove( value );
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
    /// <summary>Typed collection for Where Extensions.</summary> 
    //////////////////////////////////////////////////////////////////////
    public class WhereCollection : CollectionBase
    {
        /// <summary>holds the owning feed</summary> 
        private AtomBase atomElement;

        private WhereCollection()
        {
        }

        /// <summary>constructor</summary> 
        public WhereCollection(AtomBase atomElement)
            : base()
        {
            this.atomElement = atomElement;
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
                this.atomElement.ExtensionElements.Remove((Where)List[index]);
                List[index] = value;
                this.atomElement.ExtensionElements.Add(value);
            }
        }

        /// <summary>standard typed add method </summary>
        public int Add(Where value)
        {
            this.atomElement.ExtensionElements.Add(value);
            return (List.Add(value));
        }

        /// <summary>standard typed indexOf method </summary>
        public int IndexOf(Where value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>standard typed insert method </summary>
        public void Insert(int index, Where value)
        {
            if (this.atomElement.ExtensionElements.Contains(value)){
                this.atomElement.ExtensionElements.Remove(value);
            }
            this.atomElement.ExtensionElements.Add(value);
            List.Insert(index, value);
        }

        /// <summary>standard typed remove method </summary>
        public void Remove(Where value)
        {
            this.atomElement.ExtensionElements.Remove(value);
            List.Remove(value);
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

        /// <summary>standard override OnClear, to remove the objects from the extension list</summary> 
        protected override void OnClear()  
        {
            for (int i=0; i< this.Count;i++)
            {
                this.atomElement.ExtensionElements.Remove((Where)List[i]);
            }
        }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Who Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class WhoCollection : CollectionBase
    {
        /// <summary>holds the owning feed</summary> 
        private AtomBase atomElement;

        private WhoCollection()
        {
        }

        /// <summary>constructor</summary>
        public WhoCollection(AtomBase atomElement)
            : base()
        {
            this.atomElement = atomElement;
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
                this.atomElement.ExtensionElements.Remove((Who)List[index]);
                List[index] = value;
                this.atomElement.ExtensionElements.Add(value);
            }
        }

        /// <summary>standard typed add method </summary>
        public int Add(Who value)
        {
            this.atomElement.ExtensionElements.Add(value);
            return (List.Add(value));
        }

        /// <summary>standard typed indexOf method </summary>
        public int IndexOf(Who value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>standard typed insert method </summary>
        public void Insert(int index, Who value)
        {
            if (this.atomElement.ExtensionElements.Contains(value))
            {
                this.atomElement.ExtensionElements.Remove(value);
            }
            this.atomElement.ExtensionElements.Add(value);
            List.Insert(index, value);
        }

        /// <summary>standard typed remove method </summary> 
        public void Remove(Who value)
        {
            this.atomElement.ExtensionElements.Remove(value);
            List.Remove(value);
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

        /// <summary>standard override OnClear, to remove the objects from the extension list</summary> 
        protected override void OnClear()  
        {
            for (int i=0; i< this.Count;i++)
            {
                this.atomElement.ExtensionElements.Remove((Who)List[i]);
            }
        }

    }
    /////////////////////////////////////////////////////////////////////////////

} 
