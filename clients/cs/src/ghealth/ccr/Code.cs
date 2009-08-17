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
using System.Xml.Serialization;
using System.Xml.Schema;

namespace Google.GData.Health
{
    /// <summary>
    /// Represents a CCR coded value.
    /// </summary>
    public class CodedValue
    {
        /// <summary>
        /// Gets or sets the name of the description.
        /// </summary>
        [XElemBinding("Text", BindingType.Value)]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the description codes.
        /// </summary>
        [XmlElement("Code")]
        [XElemBinding("Code", BindingType.Additive)]
        public List<Code> Codes { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static implicit operator string(CodedValue value)
        {
            return value.Text;
        }

        public static implicit operator CodedValue(string value)
        {
            return new CodedValue() { Text = value };
        }
    }

    /// <summary>
    /// Represents a single coded value.
    /// </summary>
    public class Code
    {
        /// <summary>
        /// Gets or sets the value of the given code.
        /// </summary>
        [XElemBinding("Value", BindingType.Value)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the coding system for this value.
        /// </summary>
        [XElemBinding("CodingSystem", BindingType.Value)]
        public string CodingSystem { get; set; }
    }

    /// <summary>
    /// Represents a coded value in terms of CCR routes.
    /// </summary>
    public class Route : CodedValue
    {
        /// <summary>
        /// Gets or sets the sequence position.
        /// </summary>
        [XElemBinding("RouteSequencePosition", BindingType.Value)]
        public string SequencePosition { get; set; }

        /// <summary>
        /// Gets or sets the variable modifier.
        /// </summary>
        [XElemBinding("VariableRouteModifier", BindingType.Value)]
        public string VariableModifier { get; set; }
    }
}
