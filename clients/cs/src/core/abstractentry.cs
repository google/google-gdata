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
using System.Net;
using System.Collections;
using Google.GData.Extensions.AppControl;
using Google.GData.Extensions;



#endregion

//////////////////////////////////////////////////////////////////////
// <summary>Contains AtomEntry, an object to represent the atom:entry
// element.</summary>
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{
   /// <summary>
    /// Entry API customization class for defining entries in a custom feed
    /// </summary>
    public abstract class AbstractEntry : AtomEntry, ISupportsEtag
    {

        /// <summary>
        /// default constructor, adding app:edited and etag extensions
        /// </summary>
        public AbstractEntry()
        {
            this.AddExtension(new AppEdited());
            this.AddExtension(new Etag());
        }

        private MediaSource mediaSource;
        /// <summary>
        /// base implementation, as with the abstract feed, we are adding
        /// the gnamespace
        /// </summary>
        /// <param name="writer">The XmlWrite, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer)
        {
            base.AddOtherNamespaces(writer);
            Utilities.EnsureGDataNamespace(writer);
        }

        /// <summary>
        /// Checks if this is a namespace declaration that we already added
        /// </summary>
        /// <param name="node">XmlNode to check</param>
        /// <returns>True if this node should be skipped</returns>
        protected override bool SkipNode(XmlNode node)
        {
            if (base.SkipNode(node))
            {
                return true;
            }

            if (node.NodeType == XmlNodeType.Attribute && 
                (node.Name.StartsWith("xmlns") == true) && 
                (String.Compare(node.Value,BaseNameTable.gNamespace)==0))
                return true;
            return false; 
        }


        /// <summary>
        /// helper to toggle categories
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="value"></param>
        public void ToggleCategory(AtomCategory cat, bool value)
        {
            if (value == true)
            {
                if (this.Categories.Contains(cat) == false)
                {
                    this.Categories.Add(cat);
                }
            } 
            else 
            { 
                this.Categories.Remove(cat);
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>access the associated media element. Note, that setting this
        /// WILL cause subsequent updates to be done using MIME multipart posts
        /// </summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public MediaSource MediaSource
        {
            get {return this.mediaSource;}
            set {this.mediaSource = value;}
        }
        // end of accessor public MediaSource Media

        //////////////////////////////////////////////////////////////////////
        /// <summary>returns this feeds etag, if any
        /// This is a protocol version 2 feature
        /// </summary>
        //////////////////////////////////////////////////////////////////////
        public string Etag
        {
            get
            {
                Etag e = FindExtension(BaseNameTable.XmlEtagElement,
                                     BaseNameTable.gNamespace) as Etag;

                if (e != null)
                    return e.Value;

                return null;
            }
            set
            {
                Etag e = new Etag(value);
                ReplaceExtension(BaseNameTable.XmlEtagElement,
                                 BaseNameTable.gNamespace,
                                 e);
            }
        }


        /// <summary>
        /// returns the app:edited element of the entry, if any. 
        /// This is a protocol version 2 feature
        /// </summary>
        public AppEdited Edited
        {
            get
            {

                return FindExtension(BaseNameTable.XmlElementPubEdited,
                                     BaseNameTable.NSAppPublishingFinal) as AppEdited;
            }
            set
            {
                ReplaceExtension(BaseNameTable.XmlElementPubEdited,
                                 BaseNameTable.NSAppPublishingFinal,
                                 value);
            }
        }
    }
}
/////////////////////////////////////////////////////////////////////////////
 
