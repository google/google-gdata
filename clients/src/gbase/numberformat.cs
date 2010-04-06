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
#region Using directives
using System;
using System.Globalization;
#endregion

namespace Google.GData.GoogleBase {
    ///////////////////////////////////////////////////////////////////////
    /// <summary>Handles number parsing/generation in a culture-independent
    /// manner.</summary>
    ///////////////////////////////////////////////////////////////////////
    class NumberFormat {
        /// <summary>Parses a string as a float.</summary>
        public static float ToFloat(String value) {
           return (float)Convert.ToDouble(value, CultureInfo.InvariantCulture);
       }

       /// <summary>Formats a float as a string.</summary>
       public static string ToString(float value) {
           return value.ToString("f", CultureInfo.InvariantCulture);
        }

        /// <summary>Parses a string as an integer.</summary>
        public static int ToInt(String value) {
           return Convert.ToInt32(value, CultureInfo.InvariantCulture);
       }

       /// <summary>Formats an integer as a string.</summary>
       public static string ToString(int value) {
           return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
