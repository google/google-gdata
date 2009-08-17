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
    /// Represents a CCR immunization/medication element.
    /// </summary>
    public class Remedial : ActiveDetail
    {
        /// <summary>
        /// Gets or sets the product associated with this remedial.
        /// </summary>
        [XElemBinding("Product", BindingType.Element)]
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the remedial directions.
        /// </summary>
        [XElemBinding("Directions", BindingType.Elements)]
        public List<Direction> Directions { get; set; }

        /// <summary>
        /// Gets or sets the remedial refills.
        /// </summary>
        [XElemBinding("Refills", BindingType.Elements)]
        public List<Refill> Refills { get; set; }
    }
}
