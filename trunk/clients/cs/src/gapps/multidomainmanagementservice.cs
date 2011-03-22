using System;
using System.Collections.Generic;
using System.Web;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps
{
	/// <summary>
	/// Service class for managing multiple domains within Google Apps. 
	/// </summary>
    public class MultiDomainManagementService : AppsPropertyService
    {
        public enum MultiDomainUserProperty
        {
            AliasEmail, FirstName, IpWhitelisted, IsAdmin, IsChangePasswordAtNextLogin, IsSuspended, LastName, NewEmail, Password, UserEmail
        }

        public MultiDomainManagementService(String domain, String applicationName)
            : base(domain, applicationName)
        {
        }

		/// <summary>
		/// Creates a new user for the specified domain.
		/// </summary>
        /// <param name="domain">The domain to use to create the user</param>
        /// <param name="userEmail">The user's email address</param>
        /// <param name="password">The user's password</param>
        /// <param name="firstName">The user's first name</param>
		/// <param name="lastName">The user's last name</param>
        /// <param name="isAdmin">Whether the user is an administrator for the domain</param>
        /// <returns>The created user</returns>
        public AppsExtendedEntry CreateDomainUser(String domain, String userEmail, String password,
            String firstName, String lastName, bool isAdmin)
        {
            return CreateDomainUser(domain, userEmail, password, null, firstName, lastName, isAdmin);
        }

        /// <summary>
        /// Creates a new user for the specified domain.
        /// </summary>
        /// <param name="domain">The domain to use to create the user</param>
        /// <param name="userEmail">The user's email address</param>
        /// <param name="password">The user's password</param>
        /// <param name="hashFunction">The hashing function used for passwords (MD5/SHA-1)</param>
        /// <param name="firstName">The user's first name</param>
        /// <param name="lastName">The user's last name</param>
        /// <param name="isAdmin">Whether the user is an administrator for the domain</param>
        /// <returns>The created user</returns>
        public AppsExtendedEntry CreateDomainUser(String domain, String userEmail, String password,
            String hashFunction, String firstName, String lastName, bool isAdmin)
        {
            Uri userUri = new Uri(String.Format("{0}/{1}",
                AppsMultiDomainNameTable.AppsMultiDomainUserBaseFeedUri, domain));

            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.UserEmail, userEmail));
            entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.Password, password));
            entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.FirstName, firstName));
            entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.LastName, lastName));
            entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.IsAdmin, isAdmin.ToString()));
            if (!string.IsNullOrEmpty(hashFunction)) {
                entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.HashFunction, hashFunction));
            }
            return Insert(userUri, entry);
        }

        /// <summary>
        /// Updates the given user
        /// </summary>
        /// <param name="domain">The user's domain</param>
        /// <param name="userEmail">The user's email address</param>
        /// <param name="attributes">The set of attributes to update</param>
		/// <returns>The updated user</returns>
        public AppsExtendedEntry UpdateDomainUser(String domain, String userEmail, IDictionary<MultiDomainUserProperty, String> attributes)
        {
            AppsExtendedEntry entry = new AppsExtendedEntry();
            String uri = String.Format("{0}/{1}/{2}",
                                       AppsMultiDomainNameTable.AppsMultiDomainUserBaseFeedUri, domain, userEmail);
            entry.EditUri = new Uri(uri);

            foreach (KeyValuePair<MultiDomainUserProperty, String> mapEntry in attributes)
            {
                String value = mapEntry.Value;
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                switch (mapEntry.Key)
                {
                    case MultiDomainUserProperty.FirstName:
                        entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.FirstName, value));
                        break;
                    case MultiDomainUserProperty.IpWhitelisted:
                        entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.IpWhitelisted, value));
                        break;
                    case MultiDomainUserProperty.IsAdmin:
                        entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.IsAdmin, value));
                        break;
                    case MultiDomainUserProperty.IsChangePasswordAtNextLogin:
                        entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.IsChangePasswordAtNextLogin, value));
                        break;
                    case MultiDomainUserProperty.IsSuspended:
                        entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.IsSuspended, value));
                        break;
                    case MultiDomainUserProperty.LastName:
                        entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.LastName, value));
                        break;
                    case MultiDomainUserProperty.NewEmail:
                        entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.NewEmail, value));
                        break;
                    case MultiDomainUserProperty.Password:
                        entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.Password, value));
                        break;
                    case MultiDomainUserProperty.UserEmail:
                        entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.UserEmail, value));
                        break;
                    default:
                        break;
                }
            }
            return Update(entry);
        }

        /// <summary>
        /// Renames an existing user
        /// </summary>
		/// <param name="domain">The user's domain</param>
		/// <param name="userEmail">Current user's email address</param>
		/// <param name="newEmail">New user's email address</param>
        /// <returns>The renamed user</returns>
        public AppsExtendedEntry RenameDomainUser(String domain, String userEmail, String newEmail)
        {
            AppsExtendedEntry entry = new AppsExtendedEntry();
            String uri = String.Format("{0}/{1}/{2}",
                                       AppsMultiDomainNameTable.AppsMultiDomainUserBaseFeedUri, domain, userEmail);
            entry.EditUri = new Uri(uri);
            entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.NewEmail, newEmail));
            return Update(entry);
        }

        /// <summary>
        /// Retrieves an user from the domain
        /// </summary>
		/// <param name="domain">The user's domain</param>
		/// <param name="userEmail">The user's email address</param>
        /// <returns>The user identified by the given email address</returns>
        public AppsExtendedEntry RetrieveDomainUser(String domain, String userEmail)
        {
            String uri = String.Format("{0}/{1}/{2}",
                                       AppsMultiDomainNameTable.AppsMultiDomainUserBaseFeedUri,
                                       domain, userEmail);

            return Get(uri) as AppsExtendedEntry;
        }

        /// <summary>
        /// Retrieves all users from the domain
        /// </summary>
        /// <param name="domain">The users' domain</param>
        /// <returns>Feed containing all domain users</returns>
        public AppsExtendedFeed RetrieveAllDomainUsers(String domain)
        {
            String uri = String.Format("{0}/{1}",
                                       AppsMultiDomainNameTable.AppsMultiDomainUserBaseFeedUri,
                                       domain);

            return QueryExtendedFeed(new Uri(uri), true);
        }

        /// <summary>
        /// Deletes an user from the domain
        /// </summary>
		/// <param name="domain">The user's domain</param>
		/// <param name="userEmail">The user's email address</param>
        public void DeleteDomainUser(String domain, String userEmail)
        {
            String uri = String.Format("{0}/{1}/{2}",
                                       AppsMultiDomainNameTable.AppsMultiDomainUserBaseFeedUri,
                                       domain,
                                       userEmail);
            Delete(new Uri(uri));
        }

        /// <summary>
        /// Creates an alias for an user in the domain
        /// </summary>
		/// <param name="domain">The user's domain</param>
		/// <param name="userEmail">The user's email address</param>
        /// <param name="aliasEmail">The alias to be added to the user</param>
        /// <returns>The created alias</returns>
        public AppsExtendedEntry CreateDomainUserAlias(String domain, String userEmail, String aliasEmail)
        {
            Uri userUri = new Uri(String.Format("{0}/{1}",
                AppsMultiDomainNameTable.AppsMultiDomainAliasBaseFeedUri, domain));

            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.UserEmail, userEmail));
            entry.Properties.Add(new PropertyElement(AppsMultiDomainNameTable.AliasEmail, aliasEmail));
            return Insert(userUri, entry);
        }

        /// <summary>
        /// Retrieves an alias for the user in the domain
        /// </summary>
		/// <param name="domain">The user alias's domain or the domain in the user's primary email address</param>
		/// <param name="aliasEmail">The alias's email address</param>
        /// <returns>The alias identified by the email address</returns>
        public AppsExtendedEntry RetrieveDomainUserAlias(String domain, String aliasEmail)
        {
            String uri = String.Format("{0}/{1}/{2}",
                                    AppsMultiDomainNameTable.AppsMultiDomainAliasBaseFeedUri,
                                    domain, aliasEmail);

            return Get(uri) as AppsExtendedEntry;
        }

        /// <summary>
		/// Retrieves all aliases from the domain
        /// </summary>
		/// <param name="domain">The aliases' domain or a user's primary email address domain</param>
        /// <returns>Feed containing all aliases for the domain</returns>
        public AppsExtendedFeed RetrieveAllDomainUserAlias(String domain)
        {
            String uri = String.Format("{0}/{1}",
                                       AppsMultiDomainNameTable.AppsMultiDomainAliasBaseFeedUri,
                                       domain);

            return QueryExtendedFeed(new Uri(uri), true);
        }

        /// <summary>
		/// Retrieves all aliases from the domain for an user
        /// </summary>
		/// <param name="domain">The alias's domain or the user's primary email address domain</param>
		/// <param name="userEmail">The user's email address</param>
        /// <returns>Feed containing all aliases for the domain user</returns>
        public AppsExtendedFeed RetrieveAllDomainUserAliasForUser(String domain, String userEmail)
        {
            String uri = String.Format("{0}/{1}?userEmail={2}",
                                       AppsMultiDomainNameTable.AppsMultiDomainAliasBaseFeedUri,
                                       domain,
                                       userEmail);

            return QueryExtendedFeed(new Uri(uri), true);
        }

        /// <summary>
        /// Deletes an alias from a domain user
        /// </summary>
		/// <param name="domain">The alias's domain or the user's primary email address domain</param>
		/// <param name="userEmail">The user's email address</param>
        public void DeleteDomainUserAlias(String domain, String userEmail)
        {
            String uri = String.Format("{0}/{1}/{2}",
                                       AppsMultiDomainNameTable.AppsMultiDomainAliasBaseFeedUri,
                                       domain,
                                       userEmail);
            Delete(new Uri(uri));
        }
    }
}
﻿