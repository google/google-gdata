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
using System.Xml;
using System.IO;
using System.Collections;
using Google.GData.Apps;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.AdminSettings
{
    /// <summary>
    /// A Google Apps Google Admin Settings entry.    
    /// </summary>
    public class AdminSettingsEntry : AppsExtendedEntry
    {
        /// <summary>
        /// Constructs a new <code>AdminSettingsEntry</code> object.
        /// </summary>
        public AdminSettingsEntry()
            : base()
        {
        }

        /// <summary>
        /// typed override of the Update method
        /// </summary>
        /// <returns>AdminSettingsEntry</returns>
        public new AdminSettingsEntry Update()
        {
            return base.Update() as AdminSettingsEntry;
        }

        /// <summary>
        /// DefaultLanguage Property accessor
        /// </summary>
        public string DefaultLanguage
        {
            get
            {
                PropertyElement property =
                    this.getPropertyByName(AppsDomainSettingsNameTable.DefaultLanguage);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.DefaultLanguage).Value = value; }
        }

        /// <summary>
        /// OrganizationName Property accessor
        /// </summary>
        public string OrganizationName
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.OrganizationName);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.OrganizationName).Value = value; }
        }

        /// <summary>
        /// MaximumNumberOfUsers Property accessor
        /// </summary>
        public string MaximumNumberOfUsers
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.MaximumNumberOfUsers);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.MaximumNumberOfUsers).Value = value; }
        }

        /// <summary>
        /// MaximumNumberOfUsers Property accessor
        /// </summary>
        public string CurrentNumberOfUsers
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.CurrentNumberOfUsers);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.CurrentNumberOfUsers).Value = value; }
        }

        /// <summary>
        /// IsVerified Property accessor
        /// </summary>
        public string IsVerified
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.IsVerified);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.IsVerified).Value = value; }
        }

        /// <summary>
        /// SupportPIN Property accessor
        /// </summary>
        public string SupportPIN
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.SupportPIN);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.SupportPIN).Value = value; }
        }
        
        /// <summary>
        /// Edition Property accessor
        /// </summary>
        public string Edition
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.Edition);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.Edition).Value = value; }
        }

        /// <summary>
        /// CustomerPIN Property accessor
        /// </summary>
        public string CustomerPIN
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.CustomerPIN);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.CustomerPIN).Value = value; }
        }

        /// <summary>
        /// CreationTime Property accessor
        /// </summary>
        public string CreationTime
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.CreationTime);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.CreationTime).Value = value; }
        }

        /// <summary>
        /// CountryCode Property accessor
        /// </summary>
        public string CountryCode
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.CountryCode);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.CountryCode).Value = value; }
        }

        /// <summary>
        /// AdminSecondaryEmail Property accessor
        /// </summary>
        public string AdminSecondaryEmail
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.AdminSecondaryEmail);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.AdminSecondaryEmail).Value = value; }
        }

        /// <summary>
        /// LogoImage Property accessor
        /// </summary>
        public string LogoImage
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.LogoImage);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.LogoImage).Value = value; }
        }

        /// <summary>
        /// RecordName Property accessor
        /// </summary>
        public string RecordName
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.RecordName);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.RecordName).Value = value; }
        }

        /// <summary>
        /// Verified Property accessor
        /// </summary>
        public string Verified
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.Verified);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.Verified).Value = value; }
        }

        /// <summary>
        /// VerifiedMethod Property accessor
        /// </summary>
        public string VerifiedMethod
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.VerifiedMethod);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.VerifiedMethod).Value = value; }
        }

        /// <summary>
        /// MaximumNumberOfUsers Property accessor
        /// </summary>
        public string SamlSignonUri
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.SamlSignonUri);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.SamlSignonUri).Value = value; }
        }

        /// <summary>
        /// SamlLogoutUri Property accessor
        /// </summary>
        public string SamlLogoutUri
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.SamlLogoutUri);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.SamlLogoutUri).Value = value; }
        }

        /// <summary>
        /// ChangePasswordUri Property accessor
        /// </summary>
        public string ChangePasswordUri
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.ChangePasswordUri);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.ChangePasswordUri).Value = value; }
        }

        /// <summary>
        /// EnableSSO Property accessor
        /// </summary>
        public string EnableSSO
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.EnableSSO);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.EnableSSO).Value = value; }
        }


        /// <summary>
        /// SsoWhitelist Property accessor
        /// </summary>
        public string SsoWhitelist
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.SsoWhitelist);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.SsoWhitelist).Value = value; }
        }

        /// <summary>
        /// UseDomainSpecificIssuer Property accessor
        /// </summary>
        public string UseDomainSpecificIssuer
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.UseDomainSpecificIssuer);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.UseDomainSpecificIssuer).Value = value; }
        }

        /// <summary>
        /// SigningKey Property accessor
        /// </summary>
        public string SigningKey
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.SigningKey);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.SigningKey).Value = value; }
        }

        /// <summary>
        /// EnableUserMigration Property accessor
        /// </summary>
        public string EnableUserMigration
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.EnableUserMigration);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.EnableUserMigration).Value = value; }
        }

        /// <summary>
        /// SmartHost Property accessor
        /// </summary>
        public string SmartHost
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.SmartHost);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.SmartHost).Value = value; }
        }

        /// <summary>
        /// SmtpMode Property accessor
        /// </summary>
        public string SmtpMode
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.SmtpMode);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.SmtpMode).Value = value; }
        }

        /// <summary>
        /// RouteDestination Property accessor
        /// </summary>
        public string RouteDestination
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.RouteDestination);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.RouteDestination).Value = value; }
        }

        /// <summary>
        /// RouteRewriteTo Property accessor
        /// </summary>
        public string RouteRewriteTo
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.RouteRewriteTo);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.RouteRewriteTo).Value = value; }
        }


        /// <summary>
        /// RouteEnabled Property accessor
        /// </summary>
        public string RouteEnabled
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.RouteEnabled);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.RouteEnabled).Value = value; }
        }

        /// <summary>
        /// BounceNotifications Property accessor
        /// </summary>
        public string BounceNotifications
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.BounceNotifications);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.BounceNotifications).Value = value; }
        }

        /// <summary>
        /// AccountHandling Property accessor
        /// </summary>
        public string AccountHandling
        {
            get
            {
                PropertyElement property =
                                    this.getPropertyByName(AppsDomainSettingsNameTable.AccountHandling);
                return property != null ? property.Value : null;
            }
            set { this.getPropertyByName(AppsDomainSettingsNameTable.AccountHandling).Value = value; }
        }
    }
}
