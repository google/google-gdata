/* Copyright (c) 2007-2008 Google Inc.
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
/* Change history
 * Oct 13 2008  Joe Feser       joseph.feser@gmail.com
 * Converted ArrayLists and other .NET 1.1 collections to use Generics
 * Combined IExtensionElement and IExtensionElementFactory interfaces
 * 
 */
#region Using directives
using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
#endregion

namespace Google.GData.GoogleBase
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>Individual statistic in Stats (impressions, clicks,
    /// page views).
    ///
    /// Most of the time, the total is all there is to individual
    /// statistics, but there might be sometimes more specific
    /// per-source count information.
    ///
    /// Such objects are typically accessed through a property
    /// of a <c>Stats</c> object.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class Statistic
    {
        private int total;
        /// <summary>source name(string) x count(int)</summary>
        private Hashtable sourceCount;

        //////////////////////////////////////////////////////////////////////
        /// <summary>Total count for this statistics.</summary>
        //////////////////////////////////////////////////////////////////////
        public int Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Per-source count (subtotal), -1 for unknown count.
        /// </summary>
        //////////////////////////////////////////////////////////////////////
        public int this[string source]
        {
            get
            {
                if (sourceCount == null || !sourceCount.Contains(source))
                {
                    return -1;
                }
                return (int)sourceCount[source];
            }
            set
            {
                if (sourceCount == null)
                {
                    if (value == -1)
                    {
                        return;
                    }
                    sourceCount = new Hashtable();
                }

                if (value == -1)
                {
                    sourceCount.Remove(source);
                }
                else
                {
                    sourceCount[source] = value;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Puts the object back into its original state.</summary>
        //////////////////////////////////////////////////////////////////////
        private void Reset()
        {
            total = 0;
            sourceCount = null;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Parses an XML representation and updates the object</summary>
        ///////////////////////////////////////////////////////////////////////
        public void Parse(XmlNode xml)
        {
            Reset();
            String value = Utilities.GetAttributeValue("total", xml);
            if (value != null)
            {
                total = NumberFormat.ToInt(value);
            }

            for (XmlNode child = xml.FirstChild; child != null; child = child.NextSibling)
            {
                if ("source".Equals(child.LocalName) && GBaseNameTable.NSGBaseMeta.Equals(child.NamespaceURI))
                {
                    string name = Utilities.GetAttributeValue("name", child);
                    string countString = Utilities.GetAttributeValue("count", child);
                    if (name != null && countString != null)
                    {
                        this[name] = NumberFormat.ToInt(countString);
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates an XML representation for this object.</summary>
        ///////////////////////////////////////////////////////////////////////
        public void Save(string tagName, XmlWriter writer)
        {
            if (total > 0)
            {
                writer.WriteStartElement(GBaseNameTable.GBaseMetaPrefix, tagName, GBaseNameTable.NSGBaseMeta);
                writer.WriteAttributeString("total", NumberFormat.ToString(total));
                if (sourceCount != null)
                {
                    foreach (string source in sourceCount.Keys)
                    {
                        int count = (int)sourceCount[source];
                        writer.WriteStartElement(GBaseNameTable.GBaseMetaPrefix,
                                                 "source",
                                                 GBaseNameTable.NSGBaseMeta);
                        writer.WriteAttributeString("name", source);
                        writer.WriteAttributeString("count", NumberFormat.ToString(count));
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Object representation for the gm:stats tags in
    /// the customer item feed.</summary>
    //////////////////////////////////////////////////////////////////////
    public class Stats : IExtensionElementFactory
    {
        private Statistic impressions = new Statistic();
        private Statistic clicks = new Statistic();
        private Statistic pageViews = new Statistic();

        //////////////////////////////////////////////////////////////////////
        /// <summary>Impressions statistics</summary>
        //////////////////////////////////////////////////////////////////////
        public Statistic Impressions
        {
            get
            {
                return impressions;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Clicks statistics</summary>
        //////////////////////////////////////////////////////////////////////
        public Statistic Clicks
        {
            get
            {
                return clicks;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Page views statistics</summary>
        //////////////////////////////////////////////////////////////////////
        public Statistic PageViews
        {
            get
            {
                return pageViews;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Parses an XML representation and creates an object</summary>
        ///////////////////////////////////////////////////////////////////////
        public static Stats Parse(XmlNode xml)
        {
            Stats retval = new Stats();
            for (XmlNode child = xml.FirstChild; child != null; child = child.NextSibling)
            {
                if (GBaseNameTable.NSGBaseMeta.Equals(child.NamespaceURI))
                {
                    switch (child.LocalName)
                    {
                        case "impressions":
                            retval.Impressions.Parse(child);
                            break;

                        case "clicks":
                            retval.Clicks.Parse(child);
                            break;

                        case "page_views":
                            retval.PageViews.Parse(child);
                            break;
                    }
                }
            }
            return retval;
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates an XML representation for this object.</summary>
        ///////////////////////////////////////////////////////////////////////
        public void Save(XmlWriter writer)
        {
            if (impressions.Total > 0 || clicks.Total > 0 || pageViews.Total > 0)
            {
                writer.WriteStartElement(XmlPrefix,
                                         "stats",
                                         XmlNameSpace);
                impressions.Save("impressions", writer);
                clicks.Save("clicks", writer);
                pageViews.Save("page_views", writer);

                writer.WriteEndElement();
            }
        }

        #region IExtensionElementFactory Members

        public string XmlName
        {
            get
            {
                return "stats";
            }
        }

        public string XmlNameSpace
        {
            get
            {
                return GBaseNameTable.NSGBaseMeta;
            }
        }

        public string XmlPrefix
        {
            get
            {
                return GBaseNameTable.GBaseMetaPrefix;
            }
        }

        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            return Parse(node);
        }

        #endregion
    }

}
