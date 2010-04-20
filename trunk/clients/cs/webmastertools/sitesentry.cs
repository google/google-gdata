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

using Google.GData.Client;

namespace Google.GData.WebmasterTools
{
    public class SitesEntry : WebmasterToolsBaseEntry
    {
        /// <summary>
        /// Constructs a new SitesEntry instance
        /// </summary>
        public SitesEntry()
        : base()
        {
            Tracing.TraceMsg("Created SitesEntry");
            
            this.AddExtension(new CrawlRate());
            this.AddExtension(new GeoLocation());
            this.AddExtension(new PreferredDomain());
            this.AddExtension(new VerificationMethod());
            this.AddExtension(new Verified());
        }

        /// <summary>
        /// typed override of the Update method
        /// </summary>
        /// <returns></returns>
        public new SitesEntry Update()
        {
            return base.Update() as SitesEntry;
        }

        /// <summary>
        /// getter/setter for Crawl Rate subelement
        /// </summary>
        public string CrawlRate 
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlCrawlRateElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlCrawlRateElement, value);
            }
        }

        /// <summary>
        /// getter/setter for Geolocation subelement
        /// </summary>
        public string GeoLocation
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlGeoLocationElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlGeoLocationElement, value);
            }
        }

        /// <summary>
        /// getter/setter for Preferred-Domain subelement
        /// </summary>
        public string PreferredDomain
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlPreferredDomainElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlPreferredDomainElement, value);
            }
        }

        /// <summary>
        /// getter/setter for Verification-Method subelement
        /// </summary>
        public VerificationMethod VerificationMethod
        {
            get
            {
                return getWebmasterToolsExtension(WebmasterToolsNameTable.XmlVerificationMethodElement) as
                    VerificationMethod;
            }
            set
            {
                ReplaceExtension(WebmasterToolsNameTable.XmlVerificationMethodElement, WebmasterToolsNameTable.gWebmasterToolsNamspace, value);
            }
        }

        /// <summary>
        /// getter/setter for Verified subelement
        /// </summary>
        public string Verified
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlVerifiedElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlVerifiedElement, value);
            }
        }
    }
}
