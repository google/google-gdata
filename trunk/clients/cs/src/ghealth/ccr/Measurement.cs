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

namespace Google.GData.Health
{
    /// <summary>
    /// Represents a measurement used by several measuring CCR items.
    /// </summary>
    public class Measurement
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [XElemBinding("Description", BindingType.Element)]
        public CodedValue Description { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [XElemBinding("Value", BindingType.Value)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        [XmlArrayItem("Unit")]
        [XElemBinding("Units", BindingType.MultipleValues)]
        public List<string> Units { get; set; }

        /// <summary>
        /// Gets or sets the sequence position.
        /// </summary>
        [XElemBinding(AlternateNames = new string[] 
            { "FrequencySequencePosition", "ResultSequencePosition", "StrengthSequencePosition", "DoseSequencePosition" },
            Type = BindingType.Value)]
        public string SequencePosition { get; set; }

        /// <summary>
        /// Gets or sets the variable modifier.
        /// </summary>
        [XElemBinding(AlternateNames = new string[] 
            { "VariableFrequencyModifier", "VariableResultModifier", "VariableStrengthModifier", "VariableDoseModifier" },
            Type = BindingType.Value)]
        public string VariableModifier { get; set; }
    }
}
