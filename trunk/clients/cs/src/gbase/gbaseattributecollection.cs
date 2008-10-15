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
#region Using directives
using System;
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
using System.Collections.Generic;
#endregion

namespace Google.GData.GoogleBase
{

    ///////////////////////////////////////////////////////////////////////
    /// <summary>A convenience class for accessing GBaseAttribute
    /// extensions in an atom feed or entry (Level 1).
    ///
    /// This class provides simple, but still low-level access to
    /// attributes in an atom feed or entry. It's the 1st level
    /// of the hierarchy. Subclasses provide more convenient accessor
    /// methods with type conversion and accessors for the most common
    /// Google base tags.
    ///
    /// Everything this object does is access and modify the extension
    /// list of the atom feed or entry. It has no state of its own.
    /// </summary>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseAttributeCollection : IEnumerable
    {
        private ExtensionList extensionElements;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an attribute collection and link it to an
        /// extension list.</summary>
        /// <param name="atomElement">atom element, usually a feed or
        /// an entry.</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttributeCollection(AtomBase atomElement)
            :
                this(atomElement.ExtensionElements)
        {
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an attribute collection and link it to an
        /// extension list.</summary>
        /// <param name="extensionElements">extension list to be queried and
        /// modified</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttributeCollection(ExtensionList extensionElements)
            : base()
        {
            this.extensionElements = extensionElements;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets all attributes with a specific name and any type.
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <returns>all attributes on the list that have this name, in order
        /// </returns>
        ///////////////////////////////////////////////////////////////////////
        public List<GBaseAttribute> GetAttributes(string name)
        {
            List<GBaseAttribute> retVal = new List<GBaseAttribute>();
            foreach (GBaseAttribute var in GetAttributeList(name))
            {
                retVal.Add(var);
            }
            return retVal;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets all attributes with a specific name and a specific
        /// type.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">attribute type</param>
        /// <returns>all attributes on the list that have this name and
        /// type (or one of its subtypes), in order</returns>
        ///////////////////////////////////////////////////////////////////////
        public List<GBaseAttribute> GetAttributes(string name, GBaseAttributeType type)
        {
            List<GBaseAttribute> retVal = new List<GBaseAttribute>();
            foreach (GBaseAttribute var in GetAttributeList(name, type))
            {
                retVal.Add(var);
            }
            return retVal;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets the first attribute with a specific name and any type.
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <returns>the first attribute found or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute GetAttribute(string name)
        {
            foreach (GBaseAttribute attribute in this)
            {
                if (name == attribute.Name)
                {
                    return attribute;
                }
            }
            return null;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets the first attribute with a specific name and type.
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">attribute type</param>
        /// <returns>the first attribute found with this name and type (or
        /// one of its subtypes) or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute GetAttribute(string name, GBaseAttributeType type)
        {
            foreach (GBaseAttribute attribute in this)
            {
                if (HasNameAndType(attribute, name, type))
                {
                    return attribute;
                }
            }
            return null;
        }

        private bool HasNameAndType(GBaseAttribute attr,
                                    String name,
                                    GBaseAttributeType type)
        {
            return name == attr.Name && (type == null || type.IsSupertypeOf(attr.Type));
        }

        private ExtensionList GetAttributeList(string name)
        {
            ExtensionList retval = ExtensionList.NotVersionAware();
            foreach (GBaseAttribute attribute in this)
            {
                if (name == attribute.Name)
                {
                    retval.Add(attribute);
                }
            }
            return retval;
        }

        private ExtensionList GetAttributeList(string name, GBaseAttributeType type)
        {
            ExtensionList retval = ExtensionList.NotVersionAware();
            foreach (GBaseAttribute attribute in this)
            {
                if (HasNameAndType(attribute, name, type))
                {
                    retval.Add(attribute);
                }
            }
            return retval;
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds an attribute at the end of the list.
        /// The might exist attributes with the same name, type and even
        /// value. This method will not remove them.</summary>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute Add(GBaseAttribute value)
        {
            extensionElements.Add(value);
            return value;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Adds an attribute at the end of the list.
        /// The might exist attributes with the same name, type and even
        /// value. This method will not remove them.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">attribute type</param>
        /// <param name="content">value, as a string</param>
        ///////////////////////////////////////////////////////////////////////
        public GBaseAttribute Add(String name,
                                  GBaseAttributeType type,
                                  String content)
        {
            GBaseAttribute attribute =
                new GBaseAttribute(name, type, content);
            Add(attribute);
            return attribute;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Remove all attributes with a specific name and any type.
        /// This method is dangerous. You might be better off calling
        /// the version of this method that takes a name and type.</summary>
        /// <param name="name">attribute name</param>
        ///////////////////////////////////////////////////////////////////////
        public void RemoveAll(String name)
        {
            RemoveAll(GetAttributeList(name));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Remove all attributes with a specific name and type or
        /// on of its subtypes.</summary>
        /// <param name="name">attribute name</param>
        /// <param name="type">attribute type</param>
        ///////////////////////////////////////////////////////////////////////
        public void RemoveAll(String name, GBaseAttributeType type)
        {
            RemoveAll(GetAttributeList(name, type));
        }

        private void RemoveAll(IList<IExtensionElementFactory> list)
        {
            foreach (GBaseAttribute attribute in list)
            {
                Remove(attribute);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Remove a specific attribute, if it's there.</summary>
        /// <param name="attribute">attribute to remove</param>
        ///////////////////////////////////////////////////////////////////////
        public void Remove(GBaseAttribute attribute)
        {
            extensionElements.Remove(attribute);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Checks whether an attribute can be found in the element.
        /// This method looks for an attribute with the exact same name, type
        /// and content.</summary>
        /// <returns>true if the attribute exists</returns>
        ///////////////////////////////////////////////////////////////////////
        public bool Contains(GBaseAttribute value)
        {
            return extensionElements.Contains(value);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Removes all Google Base attributes.</summary>
        ///////////////////////////////////////////////////////////////////////
        public void Clear()
        {
            ExtensionList toRemove = ExtensionList.NotVersionAware();
            foreach (GBaseAttribute attribute in this)
            {
                toRemove.Add(attribute);
            }
            RemoveAll(toRemove);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Reads an attribute from XML and add it.</summary>
        ///////////////////////////////////////////////////////////////////////
        public void AddFromXml(XmlNode node)
        {
            Add(GBaseAttribute.ParseGBaseAttribute(node));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets an enumerator over all GBaseAttribute in the
        /// atom feed or entry.</summary>
        ///////////////////////////////////////////////////////////////////////
        public IEnumerator GetEnumerator()
        {
            return new GBaseAttributeFilterEnumerator(extensionElements);
        }
    }

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Filters an IEnumerator, ignoring all elements that
    /// are not GBaseAttribute objects.</summary>
    ///////////////////////////////////////////////////////////////////////
    class GBaseAttributeFilterEnumerator : IEnumerator
    {
        private readonly IEnumerator orig;

        public GBaseAttributeFilterEnumerator(IEnumerable collection)
            : this(collection.GetEnumerator())
        {
        }

        public GBaseAttributeFilterEnumerator(IEnumerator orig)
        {
            this.orig = orig;
        }

        public bool MoveNext()
        {
            while (orig.MoveNext())
            {
                if (orig.Current is GBaseAttribute)
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            orig.Reset();
        }

        public object Current
        {
            get
            {
                return orig.Current;
            }
        }
    }

}
