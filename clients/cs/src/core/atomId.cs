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

#define USE_TRACING

using System;
using System.Xml;
using System.Globalization;
using System.ComponentModel;


#endregion

//////////////////////////////////////////////////////////////////////
// contains AtomId
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>The "atom:id" element conveys a permanent, universally unique identifier for an entry or feed.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
#if WindowsCE || PocketPC
#else 
    [TypeConverterAttribute(typeof(AtomBaseLinkConverter)), DescriptionAttribute("Expand to see the link attributes for the Id.")]
#endif
    public class AtomId : AtomBaseLink
    {

        //////////////////////////////////////////////////////////////////////
        /// <summary>empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public AtomId() : base()
        {
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>public AtomId(string uri)</summary> 
        /// <param name="link">the URI for the ID</param>
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AtomId(string link) : base()
        {
            this.Uri = new AtomUri(link); 
        }
        /////////////////////////////////////////////////////////////////////////////


        #region Persistence overloads
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public override string XmlName 
        {
            get { return AtomParserNameTable.XmlIdElement; }
        }
        /////////////////////////////////////////////////////////////////////////////
        #endregion


    }
    /////////////////////////////////////////////////////////////////////////////
} 
/////////////////////////////////////////////////////////////////////////////
