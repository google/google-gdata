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
    /// Represents a continuity of care document record acting person.
    /// </summary>
    public class Actor
    {
        /// <summary>
        /// Gets or sets the id of the actor associated.
        /// </summary>
        [XElemBinding("ActorObjectID", BindingType.Value)]
        public string ActorObjectID { get; set; }

        /// <summary>
        /// Gets or sets the person associated as this actor.
        /// </summary>
        [XElemBinding("Person", BindingType.Element)]
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the status of this actor.
        /// </summary>
        [XElemBinding("Status", BindingType.Element)]
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the acting source for this actor.
        /// </summary>
        [XElemBinding("Source", BindingType.Element)]
        public Source Source { get; set; }
    }
}
