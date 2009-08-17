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
    /// Represents a continuity of care document record body.
    /// </summary>
    public class Body
    {
        /// <summary>
        /// Gets or sets the body's functional status.
        /// </summary>
        [XmlArrayItem("Function")]
        [XElemBinding("FunctionalStatus", BindingType.Elements)]
        public List<ActiveDetail> FunctionalStatus { get; set; }

        /// <summary>
        /// Gets or sets the body's problems.
        /// </summary>
        [XElemBinding("Problems", BindingType.Elements)]
        public List<Problem> Problems { get; set; }

        /// <summary>
        /// Gets or sets the body's social history.
        /// </summary>
        [XElemBinding("SocialHistory", BindingType.Elements)]
        public List<SocialHistoryElement> SocialHistory { get; set; }

        /// <summary>
        /// Gets or sets the body's alerts.
        /// </summary>
        [XElemBinding("Alerts", BindingType.Elements)]
        public List<Alert> Alerts { get; set; }

        /// <summary>
        /// Gets or sets the body's medications.
        /// </summary>
        [XmlArrayItem("Medication")]
        [XElemBinding("Medications", BindingType.Elements)]
        public List<Remedial> Medications { get; set; }

        /// <summary>
        /// Gets or sets the body's immunizations.
        /// </summary>
        [XmlArrayItem("Immunization")]
        [XElemBinding("Immunizations", BindingType.Elements)]
        public List<Remedial> Immunizations { get; set; }

        /// <summary>
        /// Gets or sets the body's vital signs.
        /// </summary>
        [XElemBinding("VitalSigns", BindingType.Elements)]
        public List<Result> VitalSigns { get; set; }

        /// <summary>
        /// Gets or sets the body's results.
        /// </summary>
        [XElemBinding("Results", BindingType.Elements)]
        public List<Result> Results { get; set; }

        /// <summary>
        /// Gets or sets the body's procedures.
        /// </summary>
        [XmlArrayItem("Procedure")]
        [XElemBinding("Procedures", BindingType.Elements)]
        public List<ActiveDetail> Procedures { get; set; }
    }
}
