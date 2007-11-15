/* Copyright (c) 2007 Google Inc.
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
using Google.GData.Client;

namespace Google.GData.Extensions.Apps
{
    /// <summary>
    /// Standard typed collection of labels.
    /// </summary>
    public class LabelCollection : ExtensionCollection
    {
        /// <summary>holds the owning feed</summary> 
        private AtomBase atomElement;

        private LabelCollection()
            : base()
        {
        }

        /// <summary>constructor</summary>
        public LabelCollection(AtomBase atomElement)
            : base(atomElement, AppsMigrationNameTable.AppsLabel, AppsMigrationNameTable.AppsNamespace)
        {
            this.atomElement = atomElement;
        }

        /// <summary>standard typed accessor method </summary>
        public LabelElement this[int index]
        {
            get
            {
                return ((LabelElement)List[index]);
            }
            set
            {
                setItem(index, value);
            }
        }

        /// <summary>standard typed add method </summary>
        public int Add(LabelElement value)
        {
            return base.Add(value);
        }

        /// <summary>standard typed indexOf method </summary>
        public int IndexOf(LabelElement value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>standard typed insert method </summary>
        public void Insert(int index, LabelElement value)
        {
            base.Insert(index, value);
        }

        /// <summary>standard typed remove method </summary> 
        public void Remove(LabelElement value)
        {
            base.Remove(value);
        }

        /// <summary>standard typed Contains method </summary> 
        public bool Contains(LabelElement value)
        {
            // If value is not of type AtomEntry, this will return false.
            return (List.Contains(value));
        }

        /// <summary>standard typed OnValidate Override </summary> 
        protected override void OnValidate(Object value)
        {
            if (value as LabelElement == null)
            {
                throw new ArgumentException("value must be of type Google.GData.Extensions.Apps.LabelElement", "value");
            }
        }

    }
}
