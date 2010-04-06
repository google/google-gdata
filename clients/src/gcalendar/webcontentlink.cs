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
using System.Collections;
using Google.GData.Client;
#endregion

namespace Google.GData.Extensions
{
    /// <summary>
    /// Models a special kind of Atom link that contains a WebContent element.
    /// </summary>
    public class WebContentLink : AtomLink
    {
        /// <summary>
        /// defines the webcontent rel value for the atom:link
        /// </summary>
        public const string WEB_CONTENT_REL = GDataParserNameTable.NSGCal + 
            "/" + GDataParserNameTable.XmlWebContentElement;

        /// <summary>
        /// Constructs a new Atom link containing a WebContent extension
        /// and with the appropriate rel attribute.
        /// </summary>
        public WebContentLink() : this(true)
        {
        }

        /// <summary>
        /// Constructs a new Atom link containing a WebContent extension
        /// and with the appropriate rel attribute.
        /// This constructor lets you specify if you want the extension
        /// element to be created for you. The parser sets this to false
        /// as it refuses to override pre-existing elements (even empty ones.)
        /// </summary>
        /// <param name="createWebContent">Optionally create the web content extension.</param>
        public WebContentLink(bool createWebContent)
        {
            this.Rel = WEB_CONTENT_REL;
            if (createWebContent)
            {
                this.ExtensionElements.Add(new WebContent());
            }
        }


        /// <summary>
        /// The icon URL for the WebContent entry is really just the HRef
        /// of the link itself.
        /// </summary>
        public string Icon
        {
            get
            {
                return this.HRef.ToString();
            }
            set
            {
                this.HRef = new AtomUri(value);
            }
        }

        /// <summary>
        /// Alias for the Height property of the nested WebContent element.
        /// </summary>
        [CLSCompliant(false)]
        public uint Height
        {
            get
            {
                return this.WebContent.Height;
            }
            set
            {
                this.WebContent.Height = value;
            }
        }

        /// <summary>
        /// Alias for the Width property of the nested WebContent element.
        /// </summary>
        [CLSCompliant(false)]
        public uint Width
        {
            get
            {
                return this.WebContent.Width;
            }
            set
            {
                this.WebContent.Width = value;
            }
        }

        /// <summary>
        /// Alias for the GadgetPreferences property of the nested
        /// WebContent element.
        /// </summary>
        public SortedList GadgetPreferences
        {
            get
            {
                return this.WebContent.GadgetPreferences;
            }
            set
            {
                this.WebContent.GadgetPreferences = value;
            }
        }

        /// <summary>
        /// The Url property just references the Url of the nested 
        /// WebContent element
        /// </summary>
        public string Url
        {
            get
            {
                return this.WebContent.Url;
            }
            set
            {
                this.WebContent.Url = value;
            }
        }

        /// <summary>
        /// Property that lets one modify the associated WebContent
        /// entry directly.
        /// </summary>
        public WebContent WebContent
        {
            get
            {
                return (WebContent) this.FindExtension(GDataParserNameTable.XmlWebContentElement, 
                    GDataParserNameTable.NSGCal);
            }
            set
            {
                this.ReplaceExtension(GDataParserNameTable.XmlWebContentElement,
                    GDataParserNameTable.NSGCal, value);
            }
        }


    }
}
