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
    /// Google Apps GData extension to model a user account.
    /// Has attributes "userName", "password", "suspended", and
    /// "ipWhitelisted".
    /// </summary>
    public class LoginElement : IExtensionElement
    {
        /// <summary>
        /// Constructs an empty LoginElement instance.
        /// </summary>
        public LoginElement()
        {
        }

        /// <summary>
        /// Constructs a new LoginElement instance with the specified value.
        /// </summary>
        /// <param name="userName">The account's username.</param>
        public LoginElement(String userName)
        {
            this.userName = userName;
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
        public LoginElement(String userName,
                     String password,
                     Boolean suspended,
                     Boolean ipWhitelisted)
        {
            this.userName = userName;
            this.password = password;
            this.suspended = suspended;
            this.ipWhitelisted = ipWhitelisted;
        }

        private string userName;
        private string password;
        private bool suspended;
        private bool ipWhitelisted;

        /// <summary>
        /// UserName property accessor
        /// </summary>
        public string UserName 
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// Password property accessor
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Suspended property accessor
        /// </summary>
        public bool Suspended
        {
            get { return suspended; }
            set { suspended = value; }
        }

        /// <summary>
        /// IpWhitelisted property accessor
        /// </summary>
        public bool IpWhitelisted
        {
            get { return ipWhitelisted; }
            set { ipWhitelisted = value; }
        }

        #region LoginElement Parser
        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an xml node to create a LoginElement object</summary> 
        /// <param name="node">login node</param>
        /// <param name="parser">AtomFeedParser to use</param>
        /// <returns> the created Login object</returns>
        //////////////////////////////////////////////////////////////////////
        public static LoginElement ParseLogin(XmlNode node)
        {
            Tracing.TraceCall();
            LoginElement login = null;
            Tracing.Assert(node != null, "node should not be null");
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            object localname = node.LocalName;

            if (localname.Equals(AppsNameTable.XmlElementLogin))
            {
                login = new LoginElement();
                if (node.Attributes != null)
                {
                    if (node.Attributes[AppsNameTable.XmlAttributeLoginUserName] != null)
                    {
                        login.UserName = node.Attributes[AppsNameTable.XmlAttributeLoginUserName].Value;
                    }

                    if (node.Attributes[AppsNameTable.XmlAttributeLoginPassword] != null)
                    {
                        login.Password = node.Attributes[AppsNameTable.XmlAttributeLoginPassword].Value;
                    }

                    if (node.Attributes[AppsNameTable.XmlAttributeLoginSuspended] != null)
                    {
                        login.Suspended =
                            (node.Attributes[AppsNameTable.XmlAttributeLoginSuspended].Value ==
                            Boolean.TrueString.ToLower());
                    }

                    if (node.Attributes[AppsNameTable.XmlAttributeLoginIpWhitelisted] != null)
                    {
                        login.IpWhitelisted =
                            (node.Attributes[AppsNameTable.XmlAttributeLoginIpWhitelisted].Value ==
                            Boolean.TrueString.ToLower());
                    }
                }
            }

            return login;
        }
        #endregion

        #region overloaded for persistence

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get { return AppsNameTable.XmlElementLogin; }
        }


        /// <summary>
        /// Persistence method for the LoginElement object
        /// </summary>
        /// <param name="writer">the XmlWriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (Utilities.IsPersistable(this.UserName) ||
                Utilities.IsPersistable(this.Password))
            {
                writer.WriteStartElement(AppsNameTable.appsPrefix, XmlName, AppsNameTable.appsNamespace);

                if (Utilities.IsPersistable(this.UserName))
                {
                    writer.WriteAttributeString(AppsNameTable.XmlAttributeLoginUserName, this.UserName);
                }

                if (Utilities.IsPersistable(this.Password))
                {
                    writer.WriteAttributeString(AppsNameTable.XmlAttributeLoginPassword, this.Password);
                }

                writer.WriteAttributeString(AppsNameTable.XmlAttributeLoginSuspended,
                    this.Suspended.ToString().ToLower());
                writer.WriteAttributeString(AppsNameTable.XmlAttributeLoginIpWhitelisted,
                    this.IpWhitelisted.ToString().ToLower());

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
