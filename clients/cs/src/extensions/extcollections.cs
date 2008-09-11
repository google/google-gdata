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
using System.Collections.Generic;

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
    public class ExtensionCollection<T> : IList<T>
        where T : class, IExtensionElementAndFactory, new()
    {
        private List<T> _list = new List<T>();
        /// <summary>holds the owning feed</summary>
        private IExtensionContainer container;

        /// <summary>
        /// protected default constructor, not usable by outside
        /// </summary>
        public ExtensionCollection()
        {
        }

        /// <summary>constructor</summary> 
        public ExtensionCollection(IExtensionContainer atomElement)
            : this(atomElement, new T().XmlName, new T().XmlNameSpace)
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
        public ExtensionCollection(IExtensionContainer containerElement, string localName, string ns)
            : base()
        {
            this.container = containerElement;
            if (this.container != null)
            {
                List<IExtensionElementAndFactory> arr = this.container.FindExtensions(localName, ns);
                foreach (T o in arr)
                {
                    _list.Add(o);
                }
            }
        }

        public void Clear()
        {
            if (this.container != null)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this.container.ExtensionElements.Remove(_list[i]);
                }
            }
            this.Clear();
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>standard typed accessor method </summary> 
        public T this[int index]
        {
            get
            {
                return ((T)_list[index]);
            }
            set
            {
                setItem(index, value);
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
            _list.Add(value);
            return (_list.IndexOf(value));
        }

        /// <summary>standard typed Contains method </summary> 
        public bool Contains(T value)
        {
            // If value is not of type AtomEntry, this will return false.
            return (_list.Contains(value));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// inserts an element into the collection by index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, T value)
        {
            if (this.container != null && this.container.ExtensionElements.Contains(value))
            {
                this.container.ExtensionElements.Remove(value);
            }
            this.container.ExtensionElements.Add(value);
            _list.Insert(index, value);
        }

        /// <summary>standard typed indexOf method </summary>
        public int IndexOf(T value)
        {
            return (_list.IndexOf(value));
        }
        
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// removes an element at a given index
        /// </summary>
        /// <param name="value"></param>
        public bool Remove(T value)
        {
            this.container.ExtensionElements.Remove(value);
            return _list.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        #region ICollection<T> Members

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            return this.Remove(item);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// useful for subclasses that want to overload the set method
        /// </summary>
        /// <param name="index">the index in the array</param>
        /// <param name="item">the item to set </param>
        private void setItem(int index, T item)
        {
            if (_list[index] != null)
            {
                if (this.container != null)
                {
                    this.container.ExtensionElements.Remove(_list[index]);
                }
            }
            _list[index] = item;
            if (item != null && this.container != null)
            {
                this.container.ExtensionElements.Add(item);
            }
        }
    }
}
