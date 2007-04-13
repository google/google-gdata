/* Copyright (c) 2007 Google Inc.
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

using System;
using System.Text;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Apps
{

    /// <summary>
    /// AppsNameTable: holds XML element and attribute names
    /// specific to the Google Apps GData extension.
    /// </summary>
    public class AppsNameTable
    {
        /// <summary>
        /// Prefix for apps elements when writing.
        /// </summary>
        public const string appsPrefix = "apps";

        /// <summary>
        /// Google Apps user agent.
        /// </summary>
        public const string GAppsAgent = "GApps-CS/1.0.0";

        /// <summary>
        /// Identifier for Google Apps services.
        /// </summary>
        public const string GAppsService = "apps";

        /// <summary>
        /// Google Apps namespace.
        /// </summary>
        public const string appsNamespace = "http://schemas.google.com/apps/2006";

        /// <summary>
        /// Base feed URI for all Google Apps requests.
        /// </summary>
        public const string appsBaseFeedUri = "https://www.google.com/a/feeds/";

        /// <summary>
        /// Category term for a user account entry.
        /// </summary>
        public const string User = appsNamespace + "#user";

        /// <summary>
        /// Category term for a nickname entry.
        /// </summary>
        public const string Nickname = appsNamespace + "#nickname";

        /// <summary>
        /// Category term for an email list entry.
        /// </summary>
        public const string EmailList = appsNamespace + "#emailList";

        /// <summary>
        /// Category term for an email list recipient entry.
        /// </summary>
        public const string EmailListRecipient = EmailList + ".recipient";

        /// <summary>
        /// XML element name for user login information.
        /// </summary>
        public const string XmlElementLogin = "login";

        /// <summary>
        /// XML attribute for the username of a login element.
        /// </summary>
        public const string XmlAttributeLoginUserName = "userName";

        /// <summary>
        /// XML attribute for the password of a login element.
        /// </summary>
        public const string XmlAttributeLoginPassword = "password";

        /// <summary>
        /// XML attribute for the suspended flag of a login element.
        /// </summary>
        public const string XmlAttributeLoginSuspended = "suspended";

        /// <summary>
        /// XML attribute for the ipWhitelisted flag of a login element.
        /// </summary>
        public const string XmlAttributeLoginIpWhitelisted = "ipWhitelisted";

        /// <summary>
        /// XML element name for email list data.
        /// </summary>
        public const string XmlElementEmailList = "emailList";

        /// <summary>
        /// XML attribute for the name of an email list.
        /// </summary>
        public const string XmlAttributeEmailListName = "name";

        /// <summary>
        /// XML element name for nickname data.
        /// </summary>
        public const string XmlElementNickname = "nickname";

        /// <summary>
        /// XML attribute for the "name" value of a nickname.
        /// </summary>
        public const string XmlAttributeNicknameName = "name";

        /// <summary>
        /// XML element name for specifying user quota.
        /// </summary>
        public const string XmlElementQuota = "quota";

        /// <summary>
        /// XML attribute for the quota limit, in megabytes.
        /// </summary>
        public const string XmlAttributeQuotaLimit = "limit";

        /// <summary>
        /// XML element name for specifying a user name.
        /// </summary>
        public const string XmlElementName = "name";

        /// <summary>
        /// XML attribute for the "familyName" value of a name.
        /// </summary>
        public const string XmlAttributeNameFamilyName = "familyName";

        /// <summary>
        /// XML attribute for the "givenName" value of a name.
        /// </summary>
        public const string XmlAttributeNameGivenName = "givenName";

        /// <summary>
        /// XML attribute for a Google Apps error.
        /// </summary>
        public const string XmlElementError = "error";

        /// <summary>
        /// XML attribute for the "errorCode" value of an error.
        /// </summary>
        public const string XmlAttributeErrorErrorCode = "errorCode";

        /// <summary>
        /// XML attribute for the "invalidInput" value of an error.
        /// </summary>
        public const string XmlAttributeErrorInvalidInput = "invalidInput";

        /// <summary>
        /// XML attribute for the "reason" value of an error.
        /// </summary>
        public const string XmlAttributeErrorReason = "reason";
    }
}
