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
    /// Represents a CCR result.
    /// </summary>
    public class Result : ActiveDetail
    {
        /// <summary>
        /// Gets or sets the result substance.
        /// </summary>
        [XElemBinding("Substance", BindingType.Value)]
        public string Substance { get; set; }

        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        [XmlElement("Test")]
        [XElemBinding("Test", BindingType.Additive)]
        public List<Test> Tests { get; set; }
    }

    /// <summary>
    /// Represents a result test.
    /// </summary>
    public class Test : ActiveDetail
    {
        /// <summary>
        /// Gets or sets the test result measurement.
        /// </summary>
        [XElemBinding("TestResult", BindingType.Element)]
        public Measurement TestResult { get; set; }

        /// <summary>
        /// Gets or sets the normal.
        /// </summary>
        [XmlElement("NormalResult")]
        [XElemBinding("NormalResult", BindingType.Element)]
        public NormalResult Normal { get; set; }

        /// <summary>
        /// Gets or sets the confidence value.
        /// </summary>
        [XElemBinding("ConfidenceValue", BindingType.Value)]
        public string ConfidenceValue { get; set; }
    }

    /// <summary>
    /// Represents a normal test result.
    /// </summary>
    public class NormalResult : Measurement
    {
        /// <summary>
        /// Gets or sets the normal.
        /// </summary>
        [XElemBinding("Normal", BindingType.Element)]
        public ActiveDetail Normal { get; set; }
    }
}
