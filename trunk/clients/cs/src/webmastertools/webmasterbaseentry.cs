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
using Google.GData.Extensions;

namespace Google.GData.WebmasterTools
{
    public class WebmasterToolsBaseEntry : AbstractEntry
    {
        public SimpleElement getWebmasterToolsExtension(string extension)
        {
            return FindExtension(extension, WebmasterToolsNameTable.gWebmasterToolsNamspace) as SimpleElement;
        }

        public string getWebmasterToolsValue(string extension)
        {
            SimpleElement e = getWebmasterToolsExtension(extension);
            if (e != null)
            {
                return e.Value;
            }
            return null;
        }

        public SimpleElement setWebmasterToolsExtension(string extension, string newValue)
        {
            if (extension == null)
            {
                throw new System.ArgumentNullException("extension");
            }

            SimpleElement ele = getWebmasterToolsExtension(extension);
            if (ele == null && newValue != null)
            {
                ele = CreateExtension(extension, WebmasterToolsNameTable.gWebmasterToolsNamspace) as SimpleElement;
                if (ele != null)
                {
                    this.ExtensionElements.Add(ele);
                }
            }
            if (ele == null)
                throw new System.ArgumentException("invalid extension element specified");

            if (newValue == null && ele != null)
            {
                DeleteExtensions(extension, WebmasterToolsNameTable.gWebmasterToolsNamspace);
            }

            if (ele != null)
                ele.Value = newValue;

            return ele;
        }

        public string getWebmasterToolsAttributeValue(string extension)
        {
            SimpleAttribute e = getWebmasterToolsAttribute(extension);
            if (e != null)
            {
                return (string)e.Value;
            }
            return null;
        }

        public SimpleAttribute getWebmasterToolsAttribute(string extension)
        {
            return FindExtension(extension, WebmasterToolsNameTable.gWebmasterToolsNamspace) as SimpleAttribute;
        }

        public SimpleElement setWebmasterToolsAttribute(string extension, string newValue)
        {
            if (extension == null)
            {
                throw new System.ArgumentNullException("extension");
            }

            SimpleAttribute ele = getWebmasterToolsAttribute(extension);
            if (ele == null)
            {
                ele = CreateExtension(extension, WebmasterToolsNameTable.gWebmasterToolsNamspace) as SimpleAttribute;
                this.ExtensionElements.Add(ele);
            }

            ele.Value = newValue;

            return ele;
        }
    }
}
