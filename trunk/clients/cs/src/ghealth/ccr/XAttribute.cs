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
using System.Collections.Generic;
using System.Text;

namespace Google.GData.Health
{
    /// <summary>
    /// Represents the XElement Binding Type.
    /// </summary>
    internal enum BindingType
    {
        None,
        Value,
        Attribute,
        Element,
        Elements,
        MultipleValues,
        Additive
    }

    /// <summary>
    /// Represents a simple binding.
    /// </summary>
    internal class Binding
    {
        public Binding(string property, BindingType type, Type propertyType)
        {
            this.PropertyName = property;
            this.Type = type;
            this.PropertyType = propertyType;
        }

        public string PropertyName { get; set; }
        public BindingType Type { get; set; }
        public Type PropertyType { get; set; }
        public Type GenericType { get; set; }
    }

    /// <summary>
    /// Used to define the binding between an XElement and its subsequent values.
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    internal class XElemBindingAttribute : Attribute
    {
        /// <summary>
        /// Creates a binding to the element's value.
        /// </summary>
        public XElemBindingAttribute()
        {
            this.Name = "";
            this.Type = BindingType.None;
        }

        /// <summary>
        /// Creates a binding according to the given XName and binding type.
        /// </summary>
        /// <param name="name">The XName used to bind against.</param>
        /// <param name="type">The binding type associated with the name.</param>
        public XElemBindingAttribute(string name, BindingType type)
        {
            this.Name = name;
            this.Type = type;
        }

		/// <summary>
		/// Creates a binding according to its alternate names and binding type.
		/// </summary>
		/// <param name="type">The binding type associated with the name.</param>
		/// <param name="alternateNames">The alternate names for this binding.</param>
		public XElemBindingAttribute(BindingType type, params string[] alternateNames)
		{
			this.Type = type;
			this.AlternateNames = alternateNames;
		}

        /// <summary>
        /// When using the Elements Binding, it may be desirable to 
        /// specify the parent element containing the list of elements.
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// Used to determine what name to bind to.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Refers to names also used by this type.
        /// </summary>
        public string[] AlternateNames { get; set; }

        /// <summary>
        /// Used to determine what binding type to look for.
        /// </summary>
        public BindingType Type { get; set; }
    }
}
