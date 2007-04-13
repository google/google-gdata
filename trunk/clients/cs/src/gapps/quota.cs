/* Copyright (c) 2007 Google Inc.
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
using System.Text;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Apps
{
    /// <summary>
    /// Google Apps GData extension describing a user's quota.
    /// </summary>
    public class QuotaElement : IExtensionElement
    {
        /// <summary>
        /// Constructs an empty QuotaElement instance.
        /// </summary>
        public QuotaElement()
        {
        }

        /// <summary>
        /// Constructs a new QuotaElement instance with the specified value.
        /// </summary>
        /// <param name="limit">the quota, in megabytes.</param>
        public QuotaElement(int limit)
        {
            this.Limit = limit;
        }

        private int limit;

        /// <summary>
        /// Limit property accessor
        /// </summary>
        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        #region QuotaElement Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an xml node to create a QuotaElement object</summary> 
        /// <param name="node">quota node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns> the created QuotaElement object</returns>
        //////////////////////////////////////////////////////////////////////
        public static QuotaElement ParseQuota(XmlNode node)
        {
            Tracing.TraceCall();
            QuotaElement quota = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;

            if (localname.Equals(AppsNameTable.XmlElementQuota))
            {
                quota = new QuotaElement();
                if (node.Attributes != null)
                {
                    if (node.Attributes[AppsNameTable.XmlAttributeQuotaLimit] != null)
                    {
                        quota.limit = int.Parse(node.Attributes[AppsNameTable.XmlAttributeQuotaLimit].Value);
                    }
                }
            }

            return quota;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return AppsNameTable.XmlElementQuota; }
        }


        /// <summary>
        /// Persistence method for the QuotaElement object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.limit)) {
                writer.WriteStartElement(AppsNameTable.appsPrefix, XmlName, AppsNameTable.appsNamespace);
                writer.WriteAttributeString(AppsNameTable.XmlAttributeQuotaLimit, this.limit.ToString());
                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
