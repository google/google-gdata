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
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Extensions {

    /// <summary>
    /// GData schema extension describing an RFC 2445 recurrence rule.
    /// </summary>
    public class Recurrence : IExtensionElement
    {
        private string value;

        /// <summary>
        ///  Value property accessor
        /// </summary>
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }


        #region Recurrence Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an xml node to create a Recurrence object</summary> 
        /// <param name="node">Recurrence node</param>
        /// <returns> the created Recurrence object</returns>
        //////////////////////////////////////////////////////////////////////
        public static Recurrence ParseRecurrence(XmlNode node)
        {
            Tracing.TraceCall();
            Recurrence recurrence = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;
            if (localname.Equals(GDataParserNameTable.XmlRecurrenceElement))
            {
                recurrence = new Recurrence();
                recurrence.Value = node.InnerText.Trim();
            }

            return recurrence;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return GDataParserNameTable.XmlRecurrenceElement; }
        }


        /// <summary>
        /// Persistence method for the Recurrence object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.value))
            {
                writer.WriteStartElement(BaseNameTable.gDataPrefix, XmlName, BaseNameTable.gNamespace);
                writer.WriteString(Value);
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
