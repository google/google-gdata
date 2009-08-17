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
    /// Represents a continuity of care body record functional status function entry.
    /// </summary>
    public class ActiveDetail
    {
        /// <summary>
        /// Gets when this detail starts.
        /// </summary>
        public DateTime StartDate
        {
            get
            {
				DateTimeContent date = null;
				foreach (DateTimeContent dtc in DateTimeElements)
				{
					if (dtc.Type == Constants.Types.StartDate ||
						dtc.Type == Constants.Types.CollectionStartDate)
					{
						date = dtc;
						break;
					}
				}

                if (date != null)
                    return date;
                else
                    return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets when this detail ends.
        /// </summary>
        public DateTime StopDate
        {
            get
			{
				DateTimeContent date = null;
				foreach (DateTimeContent dtc in DateTimeElements)
				{
					if (dtc.Type == Constants.Types.StopDate ||
						dtc.Type == Constants.Types.CollectionStopDate)
					{
						date = dtc;
						break;
					}
				}

                if (date != null)
                    return date;
                else
                    return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets or sets the date time content elements.
        /// </summary>
        [XmlElement("DateTime")]
        [XElemBinding("DateTime", BindingType.Additive)]
        public List<DateTimeContent> DateTimeElements { get; set; }

        /// <summary>
        /// Gets or sets the function type.
        /// </summary>
        [XElemBinding("Type", BindingType.Element)]
        public CodedValue Type { get; set; }

        /// <summary>
        /// Gets or sets the function description.
        /// </summary>
        [XElemBinding("Description", BindingType.Element)]
        public CodedValue Description { get; set; }

        /// <summary>
        /// Gets or sets the function status.
        /// </summary>
        [XElemBinding("Status", BindingType.Element)]
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the function's acting source.
        /// </summary>
        [XElemBinding("Source", BindingType.Element)]
        public Source Source { get; set; }
    }
}
