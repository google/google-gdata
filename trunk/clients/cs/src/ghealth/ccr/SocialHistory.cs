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
    /// Represents a CCR social history element.
    /// </summary>
    public class SocialHistoryElement : ActiveDetail
    {
        /// <summary>
        /// Gets the race embedded into this social history element.
        /// </summary>
        public string Race
        {
            get
            {
                if (Type.Text.Equals(Constants.Types.Race) && Type.Codes.Contains(Constants.Codes.Race))
                    return Description.Text;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the social history episodes.
        /// </summary>
        [XmlArrayItem("Frequency")]
        [XElemBinding("Episodes", BindingType.Elements)]
        public List<Measurement> Episodes { get; set; }
    }
}
