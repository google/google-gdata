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
    /// Represents a CCR refill.
    /// </summary>
    public class Refill
    {
        /// <summary>
        /// Gets or sets the refill number.
        /// </summary>
        [XElemBinding("Number", BindingType.Value)]
        public string Number { get; set; }

        public override string ToString()
        {
            return Number;
        }

        public static implicit operator string(Refill value)
        {
            return value.Number;
        }

        public static implicit operator Refill(string value)
        {
            return new Refill() { Number = value };
        }

        public static implicit operator int(Refill value)
        {
            return int.Parse(value.Number);
        }

        public static implicit operator Refill(int value)
        {
            return new Refill() { Number = value.ToString() };
        }

        public static implicit operator double(Refill value)
        {
            return int.Parse(value.Number);
        }

        public static implicit operator Refill(double value)
        {
            return new Refill() { Number = value.ToString() };
        }
    }
}
