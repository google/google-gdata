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

using System;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Extensions.Apps {

    /// <summary>
    /// Helper to instantiate all factories defined in here and attach 
    /// them to a base object.
    /// </summary>
    public class GAppsExtensions
    {
        /// <summary>
        /// Adds all Google Apps extensions to the passed in baseObject.
        /// </summary>
        /// <param name="baseObject">the <code>AtomBase</code> object,
        /// e.g. an <code>AbstractEntry</code> or <code>AbstractFeed</code></param>
        public static void AddExtension(AtomBase baseObject) 
        {
            baseObject.AddExtension(new EmailListElement());
            baseObject.AddExtension(new LoginElement());
            baseObject.AddExtension(new NameElement());
            baseObject.AddExtension(new NicknameElement());
            baseObject.AddExtension(new QuotaElement());
        }
    }

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
        /// XML attribute for the hashFunctionName flag of a login element.
        /// </summary>
        public const string XmlAttributeLoginHashFunctionName = "hashFunctionName";

        /// <summary>
        /// XML attribute for the admin flag of a login element.
        /// </summary>
        public const string XmlAttributeLoginAdmin = "admin";

        /// <summary>
        /// XML attribute for the agreedToTerms flag of a login element.
        /// </summary>
        public const string XmlAttributeLoginAgreedToTerms = "agreedToTerms";

        /// <summary>
        /// XML attribute for the changePasswordAtNextLogin flag of a login element.
        /// </summary>
        public const string XmlAttributeLoginChangePasswordAtNextLogin = "changePasswordAtNextLogin";

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

    /// <summary>
    /// Extension element sed to model a Google Apps email list.
    /// Has attribute "name".
    /// </summary>
    public class EmailListElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty EmailListElement instance.
        /// </summary>
        public EmailListElement()
            : base(AppsNameTable.XmlElementEmailList,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new EmailListElement instance with the specified value.
        /// </summary>
        /// <param name="name">the name attribute of this EmailListElement</param>
        public EmailListElement(string name)
            : base(AppsNameTable.XmlElementEmailList,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
            this.Name = name;
        }

        /// <summary>
        /// Name property accessor.
        /// </summary>
        public string Name
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.XmlAttributeEmailListName]); }
            set { this.getAttributes()[AppsNameTable.XmlAttributeEmailListName] = value; }
        }
    }

    /// <summary>
    /// Google Apps GData extension to model a user account.
    /// Has attributes: "userName", "password", "suspended",
    /// "ipWhitelisted", "admin", "agreedToTerms",
    /// "changePasswordAtNextLogin", and "hashFunctionName".
    /// </summary>
    public class LoginElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty LoginElement instance.
        /// </summary>
        public LoginElement()
            : base(AppsNameTable.XmlElementLogin,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new LoginElement instance with the specified value.
        /// </summary>
        /// <param name="userName">The account's username.</param>
        public LoginElement(string userName)
            : base(AppsNameTable.XmlElementLogin,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Constructs a new LoginElement instance with the specified values.
        /// </summary>
        /// <param name="userName">The account's username.</param>
        /// <param name="password">The account's password.</param>
        /// <param name="suspended">True if the account has been suspended,
        /// false otherwise.</param>
        /// <param name="ipWhitelisted">True if the account has been IP whitelisted,
        /// false otherwise.</param>
        public LoginElement(string userName, string password, bool suspended, bool ipWhitelisted)
            : base(AppsNameTable.XmlElementLogin,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
            this.UserName = userName;
            this.Password = password;
            this.Suspended = suspended;
            this.IpWhitelisted = ipWhitelisted;
        }

        /// <summary>
        /// Constructs a new LoginElement instance with the specified values.
        /// </summary>
        /// <param name="userName">The account's username.</param>
        /// <param name="password">The account's password.</param>
        /// <param name="suspended">True if the account has been suspended,
        /// false otherwise.</param>
        /// <param name="ipWhitelisted">True if the account has been IP whitelisted,
        /// false otherwise.</param>
        /// <param name="hashFunctionName">Hash function used to encode the password
        /// parameter.  Currently, only "SHA-1" is supported.</param>
        public LoginElement(string userName,
                            string password,
                            bool suspended,
                            bool ipWhitelisted,
                            string hashFunctionName)
            : base(AppsNameTable.XmlElementLogin,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
            this.UserName = userName;
            this.Password = password;
            this.Suspended = suspended;
            this.IpWhitelisted = ipWhitelisted;
            this.HashFunctionName = hashFunctionName;
        }

        /// <summary>
        /// UserName property accessor
        /// </summary>
        public string UserName
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.XmlAttributeLoginUserName]); }
            set { getAttributes()[AppsNameTable.XmlAttributeLoginUserName] = value; }
        }

        /// <summary>
        /// Password property accessor
        /// </summary>
        public string Password
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.XmlAttributeLoginPassword]); }
            set { getAttributes()[AppsNameTable.XmlAttributeLoginPassword] = value; }
        }

        /// <summary>
        /// Suspended property accessor
        /// </summary>
        public bool Suspended
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.XmlAttributeLoginSuspended]); }
            set { getAttributes()[AppsNameTable.XmlAttributeLoginSuspended] = value; }
        }

        /// <summary>
        /// IpWhitelisted property accessor
        /// </summary>
        public bool IpWhitelisted
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.XmlAttributeLoginIpWhitelisted]); }
            set { getAttributes()[AppsNameTable.XmlAttributeLoginIpWhitelisted] = value; }
        }

        /// <summary>
        /// HashFunctionName property accessor
        /// </summary>
        public string HashFunctionName
        {
            get
            {
                return Convert.ToString(getAttributes()[AppsNameTable.XmlAttributeLoginHashFunctionName]);
            }
            set
            {
                getAttributes()[AppsNameTable.XmlAttributeLoginHashFunctionName] = value;
            }
        }

        /// <summary>
        /// Admin property accessor.  The admin attribute is set to true if the user
        /// is an administrator and false if the user is not an administrator.
        /// </summary>
        public bool Admin
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.XmlAttributeLoginAdmin]); }
            set { getAttributes()[AppsNameTable.XmlAttributeLoginAdmin] = value; }
        }

        /// <summary>
        /// AgreedToTerms property accessor.  Read-only; true if the user has agreed
        /// to the terms of service.
        /// </summary>
        public bool AgreedToTerms
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.XmlAttributeLoginAgreedToTerms]); }
            set { getAttributes()[AppsNameTable.XmlAttributeLoginAgreedToTerms] = value; }
        }

        /// <summary>
        /// ChangePasswordAtNextLogin property accessor.  Optional; true if
        /// the user needs to change his or her password at next login.
        /// </summary>
        public bool ChangePasswordAtNextLogin
        {
            get { return Convert.ToBoolean(getAttributes()[AppsNameTable.XmlAttributeLoginChangePasswordAtNextLogin]); }
            set { getAttributes()[AppsNameTable.XmlAttributeLoginChangePasswordAtNextLogin] = value; }
        }
    }

    /// <summary>
    /// Google Apps GData extension describing a name.
    /// Has attributes "familyName" and "givenName".
    /// </summary>
    public class NameElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty NameElement instance.
        /// </summary>
        public NameElement()
            : base(AppsNameTable.XmlElementName,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new NameElement instance with the specified values.
        /// </summary>
        /// <param name="familyName">Family name (surname).</param>
        /// <param name="givenName">Given name (first name).</param>
        public NameElement(string familyName, string givenName)
            : base(AppsNameTable.XmlElementName,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
            this.FamilyName = familyName;
            this.GivenName = givenName;
        }

        /// <summary>
        /// FamilyName property accessor
        /// </summary>
        public string FamilyName
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.XmlAttributeNameFamilyName]); }
            set { getAttributes()[AppsNameTable.XmlAttributeNameFamilyName] = value; }
        }

        /// <summary>
        /// GivenName property accessor
        /// </summary>
        public string GivenName
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.XmlAttributeNameGivenName]); }
            set { getAttributes()[AppsNameTable.XmlAttributeNameGivenName] = value; }
        }
    }
    
    /// <summary>
    /// Extension element to model a Google Apps nickname.
    /// Has attribute "name".
    /// </summary>
    public class NicknameElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty <code>NicknameElement</code> instance.
        /// </summary>
        public NicknameElement()
            : base(AppsNameTable.XmlElementNickname,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new <code>NicknameElement</code> instance with the specified value.
        /// </summary>
        /// <param name="name">the name attribute of this <code>NicknameElement</code></param>
        public NicknameElement(string name)
            : base(AppsNameTable.XmlElementNickname,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
            this.Name = name;
        }

        /// <summary>
        /// Name property accessor.
        /// </summary>
        public string Name
        {
            get { return Convert.ToString(getAttributes()[AppsNameTable.XmlAttributeNicknameName]); }
            set { getAttributes()[AppsNameTable.XmlAttributeNicknameName] = value; }
        }
    }

    /// <summary>
    /// Extension element to model a Google Apps account quota.
    /// Has attribute "limit".
    /// </summary>
    public class QuotaElement : ExtensionBase
    {
        /// <summary>
        /// Constructs an empty QuotaElement instance.
        /// </summary>
        public QuotaElement()
            : base(AppsNameTable.XmlElementQuota,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
        }

        /// <summary>
        /// Constructs a new QuotaElement instance with the specified value.
        /// </summary>
        /// <param name="limit">the quota, in megabytes.</param>
        public QuotaElement(int limit)
            : base(AppsNameTable.XmlElementQuota,
                   AppsNameTable.appsPrefix,
                   AppsNameTable.appsNamespace)
        {
            this.Limit = limit;
        }

        /// <summary>
        /// Limit property accessor
        /// </summary>
        public int Limit
        {
            get { return Convert.ToInt32(getAttributes()[AppsNameTable.XmlAttributeQuotaLimit]); }
            set { getAttributes()[AppsNameTable.XmlAttributeQuotaLimit] = value; }
        }
    }
}
