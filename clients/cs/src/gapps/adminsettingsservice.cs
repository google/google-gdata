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
using System.IO;
using System.Net;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.AdminSettings
{
    /// <summary>
    /// Base service for accessing Google Admin Settings item feeds from the
    /// Google Apps Google Domain Settings API.
    /// </summary>
    public class AdminSettingsService : Service
    {
        private string domain;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="domain">The hosted domain in which the Google Mail Settings are
        /// being set up</param>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public AdminSettingsService(string domain, string applicationName)
            : base(AppsNameTable.GAppsService, applicationName)
        {
            this.domain = domain;
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewGoogleMailSettingsItemEntry);
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        /// <summary>
        /// Accessor for Domain property.
        /// </summary>
        public string Domain
        {
            get { return domain; }
            set { this.domain = value; }
        }

        /// <summary>
        /// Gets the domain's default language
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetDefaultLanguage()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.DefaultLanguageUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Updates the domain's default language
        /// </summary>
        /// <param name="defaultLanguage">the new default language for the domain</param>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateDefaultLanguage(string defaultLanguage)
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.DefaultLanguageUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.DefaultLanguage, defaultLanguage));
            return base.Update<AdminSettingsEntry>(entry);
        }

        /// <summary>
        /// Gets the domain's organization name
        /// </summary>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetOrganizationName()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.OrganizationNameUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Updates the domain's organization name
        /// </summary>
        /// <param name="organizationName">the new organization name for the domain</param>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateOrganizationName(string organizationName)
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.OrganizationNameUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.OrganizationName, organizationName));
            return base.Update<AdminSettingsEntry>(entry);
        }

        /// <summary>
        /// Gets the domain's Maximum Number Of Users
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetMaximumNumberOfUsers()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.MaximumNumberOfUsersUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Gets the domain's Current Number Of Users
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetCurrentNumberOfUsers()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.CurrentNumberOfUsersUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Gets the domain's verification status
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetIsVerified()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.IsVerifiedUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Gets the domain's Support PIN
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetSupportPIN()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.SupportPINUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Gets the domain's Google Apps Edition
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetDomainEdition()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.EditionUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Gets the domain's Customers PIN
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetCustomerPIN()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.CustomerPINUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Gets the domain's Creation Time
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetCreationTime()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.CreationTimeUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Gets the domain's Country Code
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetCountryCode()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.CountryCodeUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Gets the domain's Administrator Secondary Email address
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetAdminSecondaryEmail()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.AdminSecondaryEmailUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Updates the domain's Administrator Secondary Email address
        /// </summary>
        /// <param name="adminSecondaryEmail">the new domain's admin Secondary Email domain</param>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateAdminSecondaryEmail(string adminSecondaryEmail)
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.AdminSecondaryEmailUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(
                new PropertyElement(AppsDomainSettingsNameTable.AdminSecondaryEmail, adminSecondaryEmail));
            return base.Update<AdminSettingsEntry>(entry);
        }

        /// <summary>
        /// Updates the domain's Custom Logo
        /// </summary>
        /// <param name="base64EncodedLogoImage">base 64 encoded binary data of logo image</param>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateCustomLogo(string base64EncodedLogoImage)
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.CustomLogoUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.LogoImage, base64EncodedLogoImage));
            return base.Update<AdminSettingsEntry>(entry);            
        }

        public string FileToBase64(string path)
        {
            string base64String = "";
            FileStream fs = new FileStream(path, System.IO.FileMode.Open, FileAccess.Read);
            byte[] binaryData = new Byte[fs.Length];
            long bytesRead = fs.Read(binaryData, 0, (int)fs.Length);
            fs.Close();
            base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
            return base64String;
        }

        public string UrlToBase64(Uri uri)
        {
            string base64String = "";
            WebClient webClient = new WebClient();
            byte[] binaryData = webClient.DownloadData(uri);
            base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
            webClient.Dispose();
            return base64String;
        }

        /// <summary>
        /// Gets the domain's CNAME verification status
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetCnameVerificationStatus()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.CnameUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Updates the domain's CNAME verification status
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateCnameVerificationStatus()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.CnameUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.Verified, Boolean.TrueString));
            return base.Update<AdminSettingsEntry>(entry);
        }

        /// <summary>
        /// Gets the domain's MX verification status
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetMxVerificationStatus()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.MxUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Updates the domain's MX verification status
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateMxVerificationStatus()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.MxUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.Verified, Boolean.TrueString));
            return base.Update<AdminSettingsEntry>(entry);
        }

        /// <summary>
        /// Gets the domain's SSO settings
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetSsoSettings()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.SsoGeneralUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Updates the domain's SSO settings
        /// </summary>        
        /// <param name="enableSSO">Enable or Disable SSO for the domain</param>
        /// <param name="samlSignonUri">http://www.example.com/sso/signon</param>
        /// <param name="samlLogoutUri">http://www.example.com/sso/logout</param>
        /// <param name="changePasswordUri">http://www.example.com/sso/changepassword</param>
        /// <param name="ssoWhitelist">CIDR formated IP address</param>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateSsoSettings( Boolean enableSSO, String samlSignonUri,
            String samlLogoutUri, String changePasswordUri, String ssoWhitelist, Boolean useDomainSpecificIssuer)
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.SsoGeneralUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.EnableSSO, enableSSO.ToString()));
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.SamlSignonUri, samlSignonUri));
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.SamlLogoutUri, samlLogoutUri));
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.ChangePasswordUri, changePasswordUri));
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.SsoWhitelist, ssoWhitelist));
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.UseDomainSpecificIssuer, useDomainSpecificIssuer.ToString()));
            return base.Update<AdminSettingsEntry>(entry);
        }

        /// <summary>
        /// Gets the domain's SSO Signing Key
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetSsoSigningkey()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.SsoSigningkeyUriSuffix;            
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Updates the domain's SSO Signing Key
        /// </summary>
        /// <param name="base64EncodedSigningKey">yourBase64EncodedPublicKey</param>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateSsoSigningkey(string base64EncodedSigningKey)
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.SsoSigningkeyUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.SigningKey, base64EncodedSigningKey));
            return base.Update<AdminSettingsEntry>(entry);
        }

        /// <summary>
        /// Gets the domain's Migration Access settings
        /// </summary>        
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetMigrationAccess()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.MigrationUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Updates the domain's Migration Access settings
        /// </summary>
        /// <param name="enableUserMigration">Enable or Disable User migration for the domain</param>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateMigrationAccess(Boolean enableUserMigration)
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.MigrationUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.EnableUserMigration, enableUserMigration.ToString()));
            return base.Update<AdminSettingsEntry>(entry);
        }

        /// <summary>
        /// Gets the domain's Email Gateway settings
        /// </summary>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry GetEmailGateway()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.GatewayUriSuffix;
            return Get(uri) as AdminSettingsEntry;
        }

        /// <summary>
        /// Updates the domain's Email Gateway settings
        /// </summary>
        /// <param name="smartHost">Either the IP address or hostname of your SMTP server.
        /// Google Apps routes outgoing mail to this server.</param>
        /// <param name="smtpMode"> The default value is SMTP. Another value, SMTP_TLS,
        /// secures a connection with TLS when delivering the message. </param>
        /// <returns>a <code>AdminSettingsEntry</code> containing the results of the
        /// operation</returns>
        public AdminSettingsEntry UpdateEmailGateway(string smartHost, string smtpMode)
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.GatewayUriSuffix;
            AdminSettingsEntry entry = new AdminSettingsEntry();
            entry.EditUri = uri;
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.SmartHost, smartHost));
            entry.Properties.Add(new PropertyElement(AppsDomainSettingsNameTable.SmtpMode, smtpMode));
            return base.Update<AdminSettingsEntry>(entry);
        }

        /// <summary>
        /// Gets the domain's Email Routing settings
        /// </summary>
        /// <returns>a <code>AdminSettingsFeed</code> containing the results of the
        /// operation</returns>
        public AdminSettingsFeed GetEmailRouting()
        {
            string uri = AppsDomainSettingsNameTable.AppsAdminSettingsBaseFeedUri
               + domain + AppsDomainSettingsNameTable.EmailroutingUriSuffix;
            FeedQuery feedQuery = new FeedQuery(uri);
            return Query(feedQuery) as AdminSettingsFeed;
        }

        /// <summary>
        /// Event handler. Called when a new Google Domain Settings entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the evet</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewGoogleMailSettingsItemEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry)
            {
                e.Entry = new AdminSettingsEntry();
            }
        }

        /// <summary>
        /// Overridden so that new feeds are returned as <code>AppsExtendedFeed</code>s
        /// instead of base <code>AtomFeed</code>s.
        /// </summary>
        /// <param name="sender"> the object which sent the event</param>
        /// <param name="e">FeedParserEventArguments, holds the FeedEntry</param> 
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Google Mail Settings Item Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            e.Feed = new AdminSettingsFeed(e.Uri, e.Service);
        }
    }
}
