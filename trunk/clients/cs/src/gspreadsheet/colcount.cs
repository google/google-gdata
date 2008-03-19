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
using System.Xml;
using Google.GData.Extensions;

namespace Google.GData.Spreadsheets
{

     /// <summary>
    /// GData schema extension for rowCount element.
    /// </summary>
    public class ColCountElement : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public ColCountElement()
        : base(GDataSpreadsheetsNameTable.XmlColCountElement, 
               GDataSpreadsheetsNameTable.Prefix,
               GDataSpreadsheetsNameTable.NSGSpreadsheets)
         {}
        /// <summary>
        /// default constructor with an initial value as a integer 
        /// </summary>
        [CLSCompliant(false)]
        public ColCountElement(uint initValue)
        : base(GDataSpreadsheetsNameTable.XmlColCountElement, 
               GDataSpreadsheetsNameTable.Prefix,
               GDataSpreadsheetsNameTable.NSGSpreadsheets,
               initValue.ToString())
        {}

        /// <summary>
        /// Gets or sets the count of rows.
        /// </summary>
        [CLSCompliant(false)]
        public uint Count
        {
            get
            {
                return this.UnsignedIntegerValue;
            }

            set
            {
                this.UnsignedIntegerValue = value;
            }
        }
    }
}
