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
using System;
using System.Xml;
using System.IO;
using System.Collections;
using Google.GData.Client;
using System.Collections.Generic;

namespace Google.GData.GoogleBase
{

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Utilities for getting/setting IExtensionElement in a list,
    /// used by <see cref="GBaseEntry"/></summary>
    ///////////////////////////////////////////////////////////////////////
    public class GBaseUtilities
    {

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Gets the first extension of the givent type.</summary>
        /// <param name="extensionElements">list of IExtensionElement</param>
        /// <param name="type">type of the extension</param>
        /// <returns>an extension of the correct type or null</returns>
        ///////////////////////////////////////////////////////////////////////
        public static object GetExtension(IList<IExtensionElementFactory> extensionElements, Type type)
        {
            foreach (IExtensionElementFactory element in extensionElements)
            {
                if (type.IsInstanceOfType(element))
                {
                    return element;
                }
            }
            return null;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Replaces or adds an extension of the given type
        /// to the list.
        ///
        /// This method looks for the first element on the list of the
        /// given type and replaces it with the new value. If there are
        /// no element of this type yet, a new element is added at the
        /// end of the list.
        /// </summary>
        /// <param name="extensionElements">list of IExtensionElement</param>
        /// <param name="type">type of the element to be added, removed
        /// or replaced</param>
        /// <param name="newValue">new value for this element, null to
        /// remove it</param>
        ///////////////////////////////////////////////////////////////////////
        public static void SetExtension(IList<IExtensionElementFactory> extensionElements,
                                        Type type,
                                        IExtensionElementFactory newValue)
        {
            int count = extensionElements.Count;
            for (int i = 0; i < count; i++)
            {
                object element = extensionElements[i];
                if (type.IsInstanceOfType(element))
                {
                    if (newValue == null)
                    {
                        extensionElements.RemoveAt(i);
                    }
                    else
                    {
                        extensionElements[i] = newValue;
                    }
                    return;
                }
            }
            if (newValue != null)
            {
                extensionElements.Add(newValue);
            }
        }
    }

}
