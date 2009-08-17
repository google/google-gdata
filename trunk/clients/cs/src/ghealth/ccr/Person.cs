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
    /// Represents a continuity of care document record person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the person's name.
        /// </summary>
        [XElemBinding("Name", BindingType.Element)]
        public Name Name { get; set; }

        /// <summary>
        /// Gets or sets the person's date of birth.
        /// </summary>
        [XElemBinding("DateOfBirth", BindingType.Element)]
        public DateTimeContent DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the person's gender.
        /// </summary>
        [XElemBinding("Gender", BindingType.Element)]
        public CodedValue Gender { get; set; }
    }
}
