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

/* 
 * Created by Morten Christensen, http://blog.sitereactor.dk | http://twitter.com/sitereactor
 */

using System;
using Google.GData.Client;

namespace Google.GData.WebmasterTools
{
    public class SitemapsEntry : WebmasterToolsBaseEntry
    {
        /// <summary>
        /// Constructs a new SitemapsEntry instance
        /// </summary>
        public SitemapsEntry() : base()
        {
            Tracing.TraceMsg("Created SitemapsEntry");

            this.AddExtension(new Mobile());
            this.AddExtension(new SitemapLastDownloaded());
            this.AddExtension(new SitemapMobile());
            this.AddExtension(new SitemapMobileMarkupLanguage());
            this.AddExtension(new SitemapNews());
            this.AddExtension(new SitemapNewsPublicationLabel());
            this.AddExtension(new SitemapType());
            this.AddExtension(new SitemapUrlCount());
        }

        /// <summary>
        /// getter/setter for Mobile subelement
        /// </summary>
        public string Mobile
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlMobileElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlMobileElement, value);
            }
        }

        /// <summary>
        /// getter/setter for SitemapLastDownloaded subelement
        /// </summary>
        public string SitemapLastDownloaded
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlSitemapLastDownloadedElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlSitemapLastDownloadedElement, value);
            }
        }

        /// <summary>
        /// getter/setter for SitemapMobile subelement
        /// </summary>
        public string SitemapMobile
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlSitemapMobileElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlSitemapMobileElement, value);
            }
        }

        /// <summary>
        /// getter/setter for SitemapMobileMarkupLanguage subelement
        /// </summary>
        public string SitemapMobileMarkupLanguage
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlSitemapMobileMarkupLanguageElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlSitemapMobileMarkupLanguageElement, value);
            }
        }

        /// <summary>
        /// getter/setter for SitemapNews subelement
        /// </summary>
        public string SitemapNews
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlSitemapNewsElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlSitemapNewsElement, value);
            }
        }

        /// <summary>
        /// getter/setter for SitemapNewsPublicationLabel subelement
        /// </summary>
        public string SitemapNewsPublicationLabel
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlSitemapNewsPublicationLabelElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlSitemapNewsPublicationLabelElement, value);
            }
        }

        /// <summary>
        /// getter/setter for SitemapType subelement
        /// </summary>
        public string SitemapType
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlSitemapTypeElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlSitemapTypeElement, value);
            }
        }

        /// <summary>
        /// getter/setter for SitemapUrlCount subelement
        /// </summary>
        public string SitemapUrlCount
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlSitemapUrlCountElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlSitemapUrlCountElement, value);
            }
        }
    }
}
