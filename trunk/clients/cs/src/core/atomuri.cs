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
using System.IO; 

#endregion

//////////////////////////////////////////////////////////////////////
// Contains AtomUri.
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>AtomUri object representation
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class AtomUri
    {
        string strContent;

        /// <summary>basic constructor for the atomUri</summary> 
        public AtomUri(Uri uri)
        {
            Tracing.Assert(uri != null, "uri should not be null");
            if (uri == null)
            {
                throw new ArgumentNullException("uri"); 
            }
            this.strContent = System.Web.HttpUtility.UrlDecode(uri.ToString());
        }

        /// <summary>alternating constructor with a string</summary> 
        public AtomUri(string str)
        {
            this.strContent = System.Web.HttpUtility.UrlDecode(str);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Content</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Content
        {
            get {return this.strContent;}
            set {this.strContent = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>override for ToString</summary> 
        public override string ToString()
        {
            return strContent;
        }

        /// <summary>comparison method similar to strings</summary> 
        public static int Compare(AtomUri theOne, AtomUri theOther)
        {
            if (theOne == null && theOther == null)
            {
                return 0;
            }
            if (theOther == null)
            {
                return 1; 
            }
            if (theOne == null)
            {
                return -1; 
            }
            return String.Compare(theOne.ToString(), theOther.ToString());

        }

    }
    /////////////////////////////////////////////////////////////////////////////
} 
/////////////////////////////////////////////////////////////////////////////
