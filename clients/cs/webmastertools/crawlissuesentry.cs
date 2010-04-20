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
    public class CrawlIssuesEntry : WebmasterToolsBaseEntry
    {
        /// <summary>
        /// Constructs a new CrawlIssuesEntry instance
        /// </summary>
        public CrawlIssuesEntry() : base()
        {
            Tracing.TraceMsg("Created CrawlIssuesEntry");

            this.AddExtension(new CrawlType());
            this.AddExtension(new IssueType());
            this.AddExtension(new IssueDetail());
            this.AddExtension(new LinkedFrom());
            this.AddExtension(new DateDetected());
        }

        /// <summary>
        /// getter/setter for CrawlType subelement
        /// </summary>
        public string CrawlType
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlCrawlTypeElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlCrawlTypeElement, value);
            }
        }

        /// <summary>
        /// getter/setter for IssueType subelement
        /// </summary>
        public string IssueType
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlIssueTypeElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlIssueTypeElement, value);
            }
        }

        /// <summary>
        /// getter/setter for IssueDetail subelement
        /// </summary>
        public string IssueDetail
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlIssueDetailElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlIssueDetailElement, value);
            }
        }

        /// <summary>
        /// getter/setter for LinkedFrom subelement
        /// </summary>
        public string LinkedFrom
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlLinkedFromElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlLinkedFromElement, value);
            }
        }

        /// <summary>
        /// getter/setter for DateDetected subelement
        /// </summary>
        public string DateDetected
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlDateDetectedElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlDateDetectedElement, value);
            }
        }
    }
}
