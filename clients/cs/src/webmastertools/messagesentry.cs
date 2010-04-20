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
    public class MessagesEntry : WebmasterToolsBaseEntry
    {
        /// <summary>
        /// Constructs a new MessagesEntry instance
        /// </summary>
        public MessagesEntry() : base()
        {
            Tracing.TraceMsg("Created MessagesEntry");

            this.AddExtension(new Body());
            this.AddExtension(new Date());
            this.AddExtension(new Language());
            this.AddExtension(new Read());
            this.AddExtension(new Subject());
        }

        /// <summary>
        /// getter/setter for Body subelement
        /// </summary>
        public string Body
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlBodyElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlBodyElement, value);
            }
        }

        /// <summary>
        /// getter/setter for Date subelement
        /// </summary>
        public string Date
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlDateElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlDateElement, value);
            }
        }

        /// <summary>
        /// getter/setter for Language subelement
        /// </summary>
        public Language LanguageElement
        {
            get
            {
                //return getWebmasterToolsValue(WebmasterToolsNameTable.XmlLanguageElement);
                return getWebmasterToolsExtension(WebmasterToolsNameTable.XmlLanguageElement) as
                    Language;
            }
            set
            {
                //setWebmasterToolsExtension(WebmasterToolsNameTable.XmlLanguageElement, value);
                ReplaceExtension(WebmasterToolsNameTable.XmlLanguageElement, WebmasterToolsNameTable.gWebmasterToolsNamspace, value);
            }
        }

        /// <summary>
        /// getter/setter for Read subelement
        /// </summary>
        public string Read
        {
            get
            {
                return getWebmasterToolsValue(WebmasterToolsNameTable.XmlReadElement);
            }
            set
            {
                setWebmasterToolsExtension(WebmasterToolsNameTable.XmlReadElement, value);
            }
        }

        /// <summary>
        /// getter/setter for Subject subelement
        /// </summary>
        public Subject Subject
        {
            get
            {
                //return getWebmasterToolsValue(WebmasterToolsNameTable.XmlSubjectElement);
                return getWebmasterToolsExtension(WebmasterToolsNameTable.XmlSubjectElement) as
                    Subject;
            }
            set
            {
                //setWebmasterToolsExtension(WebmasterToolsNameTable.XmlSubjectElement, value);
                ReplaceExtension(WebmasterToolsNameTable.XmlSubjectElement, WebmasterToolsNameTable.gWebmasterToolsNamspace, value);
            }
        }
    }
}
