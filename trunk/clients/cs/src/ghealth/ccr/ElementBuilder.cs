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
using System.Reflection;
using System.Collections;
using System.Xml;

namespace Google.GData.Health
{
    /// <summary>
    /// Provides the ability to build objects from XElements when leveraging 
    /// the XElemAttribute on properties contained within the objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ElementBuilder<T>
        where T : class, new()
    {
        private Builder builder;

        public ElementBuilder()
        {
            this.builder = new ElementBuilder<T>.Builder(typeof(T));
        }

        public List<T> FromElements(XmlNodeList elements)
        {
            return this.builder.FromElements(elements, typeof(List<T>)) as List<T>;
        }

        public T FromElement(XmlNode element)
        {
            return this.builder.FromElement(element) as T;
        }

        /// <summary>
        /// Provides basic functionality for building objects.
        /// </summary>
        private class Builder
        {
            private Dictionary<Type, Builder> builders;
            private Dictionary<string, Binding> bindings;
            private Type type;

            public Builder(Type type)
            {
                builders = new Dictionary<Type, ElementBuilder<T>.Builder>();
                bindings = new Dictionary<string, Binding>();
                this.type = type;
                BuildBindings();
            }

            private void BuildBindings()
            {
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object[] attributes = property.GetCustomAttributes(typeof(XElemBindingAttribute), true);
					if (attributes != null && attributes.Length > 0)
					{
						foreach (XElemBindingAttribute binding in attributes)
						{
							if (binding != null)
							{
								Type propertyType = property.PropertyType;
								Type genericType = null;
								if (binding.Type == BindingType.Element)
								{
									if (!builders.ContainsKey(property.PropertyType))
										builders.Add(property.PropertyType, new ElementBuilder<T>.Builder(property.PropertyType));
								}
								else if (binding.Type == BindingType.Elements || binding.Type == BindingType.Additive)
								{
									Type[] types = property.PropertyType.GetGenericArguments();
									if (types.Length == 1)
									{
										if (!builders.ContainsKey(types[0]))
											builders.Add(types[0], new ElementBuilder<T>.Builder(types[0]));
										genericType = types[0];
									}
								}
								else if (binding.Type == BindingType.None)
								{
									binding.Name = type.Name;
								}
								Binding bind = new Binding(property.Name, binding.Type, propertyType) { GenericType = genericType };
								if (binding.AlternateNames != null && binding.AlternateNames.Length > 0)
								{
									foreach (string name in binding.AlternateNames)
									{
										bindings.Add(name, bind);
									}
								}
								else
								{
									bindings.Add(binding.Name, bind);
								}
							}
						}
					}
                }
            }

			public IList FromElements(XmlNodeList elements, Type t)
            {
                IList result = Activator.CreateInstance(t) as IList;

                if (result != null && elements != null)
                {
                    foreach (XmlNode elem in elements)
                    {
                        result.Add(FromElement(elem));
                    }
                }

                return result;
            }

            public object FromElement(XmlNode element)
            {
                object result = Activator.CreateInstance(this.type);

                if (element != null)
                {
                    // Assign all value elements
                    if (bindings.ContainsKey(type.Name))
                        SetProperty(ref result, bindings[type.Name].PropertyName, element.Value);

                    // Assign all attribute bindings for this element
                    XmlAttributeCollection attributes = element.Attributes;
                    if (attributes != null)
                    {
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute attribute = attributes[i];
							string name = attribute.LocalName;
							if (this.bindings.ContainsKey(name) && this.bindings[name].Type == BindingType.Attribute)
								SetProperty(ref result, bindings[name].PropertyName, attribute.Value);
						}
                    }

                    // For every element node, determine if there is a binding that can be built
                    XmlNodeList elements = element.ChildNodes;
                    if (elements != null)
                    {
                        foreach (XmlNode elem in elements)
                        {
                            string name = elem.LocalName;
                            if (this.bindings.ContainsKey(name))
                            {
                                Binding bind = this.bindings[name];
                                switch (bind.Type)
                                {
                                    case BindingType.Value:
                                        SetProperty(ref result, bind.PropertyName, elem.Value);
                                        break;
                                    case BindingType.Element:
                                        SetProperty(ref result, bind.PropertyName, builders[bind.PropertyType].FromElement(elem));
                                        break;
                                    case BindingType.Elements:
                                        SetProperty(ref result, bind.PropertyName, builders[bind.GenericType].FromElements(elem.ChildNodes, bind.PropertyType));
                                        break;
                                    case BindingType.MultipleValues:
                                        foreach (XmlNode item in elem.ChildNodes)
                                            AddToList(ref result, bind.PropertyName, item.Value);
                                        break;
                                    case BindingType.Additive:
                                        AddToList(ref result, bind.PropertyName, builders[bind.GenericType].FromElement(elem));
                                        break;
                                }
                            }
                        }
                    }
                }

                return result;
            }

            private void SetProperty(ref object reference, string property, object value)
            {
                PropertyInfo info = type.GetProperty(property);
                info.SetValue(reference, value, null);
            }

            private void AddToList(ref object reference, string property, object value)
            {
                PropertyInfo info = type.GetProperty(property);
                object obj = info.GetValue(reference, null);
                if (obj == null)
                    obj = Activator.CreateInstance(info.PropertyType);

                IList list = obj as IList;
                if (list != null)
                {
                    list.Add(value);
                }
                info.SetValue(reference, obj, null);
            }
        }
    }
}
