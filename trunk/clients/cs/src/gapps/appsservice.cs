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
using Google.GData.Apps.Groups;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps {
    /// <summary>
    /// The AppsService class provides a simpler interface
    /// for executing common Google Apps provisioning
    /// requests.
    /// </summary>
    public class AppsService {
        private NicknameService nicknameService;
        private UserService userAccountService;
        private GroupsService groupsService;

        private string applicationName;
        private string domain;

        /// <summary>
        /// Constructs an AppsService with the specified credentials
        /// for accessing provisioning feeds on the specified domain.
        /// </summary>
        /// <param name="domain">the domain to access</param>
        /// <param name="adminEmailAddress">the administrator's email address</param>
        /// <param name="adminPassword">the administrator's password</param>
        public AppsService(string domain, string adminEmailAddress, string adminPassword) {
            this.domain = domain;
            this.applicationName = "apps-" + domain;

            nicknameService = new NicknameService(applicationName);
            nicknameService.setUserCredentials(adminEmailAddress, adminPassword);

            userAccountService = new UserService(applicationName);
            userAccountService.setUserCredentials(adminEmailAddress, adminPassword);

            groupsService = new GroupsService(domain, applicationName);
            groupsService.setUserCredentials(adminEmailAddress, adminPassword);
        }

        /// <summary>
        /// Constructs an AppsService with the specified Authentication Token
        /// for accessing provisioning feeds on the specified domain.
        /// </summary>
        /// <param name="domain">the domain to access</param>
        /// <param name="authenticationToken">the administrator's Authentication Token</param>
        public AppsService(string domain, string authenticationToken) {
            this.domain = domain;
            this.applicationName = "apps-" + domain;

            nicknameService = new NicknameService(applicationName);
            nicknameService.SetAuthenticationToken(authenticationToken);

            userAccountService = new UserService(applicationName);
            userAccountService.SetAuthenticationToken(authenticationToken);

            groupsService = new GroupsService(domain, applicationName);
            groupsService.SetAuthenticationToken(authenticationToken);
        }

        /// <summary>indicates if the connection should be kept alive, 
        /// default is true
        /// </summary>
        /// <param name="keepAlive">bool to set if the connection should be keptalive</param>
        public void KeepAlive(bool keepAlive) {
            ((GDataRequestFactory)nicknameService.RequestFactory).KeepAlive = keepAlive;
            ((GDataRequestFactory)userAccountService.RequestFactory).KeepAlive = keepAlive;
            ((GDataRequestFactory)groupsService.RequestFactory).KeepAlive = keepAlive;
        }

        /// <summary>
        /// Generates a new Authentication Token for AppsService 
        /// with the specified credentials for accessing provisioning feeds on the specified domain.
        /// </summary>
        /// <param name="domain">the domain to access</param>
        /// <param name="adminEmailAddress">the administrator's email address</param>
        /// <param name="adminPassword">the administrator's password</param>
        /// <returns>the newly generated authentication token</returns>
        public static String GetNewAuthenticationToken(string domain, string adminEmailAddress, string adminPassword) {
            Service service = new Service(AppsNameTable.GAppsService, "apps-" + domain);
            service.setUserCredentials(adminEmailAddress, adminPassword);
            return service.QueryClientLoginToken();
        }

        /// <summary>
        /// ApplicationName property accessor
        /// </summary>
        public string ApplicationName {
            get { return applicationName; }
            set { applicationName = value; }
        }

        /// <summary>
        /// Domain property accessor
        /// </summary>
        public string Domain {
            get { return domain; }
            set { domain = value; }
        }

        /// <summary>
        /// GroupsService accessor
        /// </summary>
        public GroupsService Groups {
            get { return groupsService; }
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="username">the account's username</param>
        /// <param name="givenName">the user's first (given) name</param>
        /// <param name="familyName">the user's last (family) name</param>
        /// <param name="password">the account's password</param>
        /// <returns>the newly created UserEntry</returns>
        public UserEntry CreateUser(string username,
            string givenName,
            string familyName,
            string password) {
            UserEntry entry = new UserEntry();

            entry.Name = new NameElement(familyName, givenName);
            entry.Login = new LoginElement(username, password, false, false);

            UserQuery query = new UserQuery(Domain);

            return userAccountService.Insert(query.Uri, entry);
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="username">the account's username</param>
        /// <param name="givenName">the user's first (given) name</param>
        /// <param name="familyName">the user's last (family) name</param>
        /// <param name="password">the account's password</param>
        /// <param name="quotaLimitInMb">the account's quota, in MB</param>
        /// <returns>the newly created UserEntry</returns>
        public UserEntry CreateUser(string username,
            string givenName,
            string familyName,
            string password,
            int quotaLimitInMb) {
            UserEntry entry = new UserEntry();

            entry.Name = new NameElement(familyName, givenName);
            entry.Login = new LoginElement(username, password, false, false);
            entry.Quota = new QuotaElement(quotaLimitInMb);

            UserQuery query = new UserQuery(Domain);

            return userAccountService.Insert(query.Uri, entry);
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="username">the account's username</param>
        /// <param name="givenName">the user's first (given) name</param>
        /// <param name="familyName">the user's last (family) name</param>
        /// <param name="password">the account's password</param>
        /// <param name="passwordHashFunction">the name of the hash function to hash the password</param>
        /// <returns>the newly created UserEntry</returns>
        public UserEntry CreateUser(string username,
            string givenName,
            string familyName,
            string password,
            string passwordHashFunction) {
            UserEntry entry = new UserEntry();

            entry.Name = new NameElement(familyName, givenName);
            entry.Login = new LoginElement(username, password, false, false, passwordHashFunction);

            UserQuery query = new UserQuery(Domain);

            return userAccountService.Insert(query.Uri, entry);
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="username">the account's username</param>
        /// <param name="givenName">the user's first (given) name</param>
        /// <param name="familyName">the user's last (family) name</param>
        /// <param name="password">the account's password</param>
        /// <param name="passwordHashFunction">the name of the hash function to hash the password</param>
        /// <param name="quotaLimitInMb">the account's quota, in MB</param>
        /// <returns>the newly created UserEntry</returns>
        public UserEntry CreateUser(string username,
            string givenName,
            string familyName,
            string password,
            string passwordHashFunction,
            int quotaLimitInMb) {
            UserEntry entry = new UserEntry();

            entry.Name = new NameElement(familyName, givenName);
            entry.Login = new LoginElement(username, password, false, false, passwordHashFunction);
            entry.Quota = new QuotaElement(quotaLimitInMb);

            UserQuery query = new UserQuery(Domain);

            return userAccountService.Insert(query.Uri, entry);
        }

        /// <summary>
        /// Retrieves all user account entries on this domain.
        /// </summary>
        /// <returns>the feed containing all user account entries</returns>
        public UserFeed RetrieveAllUsers() {
            UserQuery query = new UserQuery(domain);
            return userAccountService.Query(query);
        }

        /// <summary>
        /// Retrieves a page of at most 100 users beginning with the
        /// specified username.  Usernames are ordered case-insensitively
        /// by ASCII value.
        /// </summary>
        /// <param name="startUsername">the first username that should appear
        /// in your result set</param>
        /// <returns>the feed containing the matching user account entries</returns>
        public UserFeed RetrievePageOfUsers(string startUsername) {
            UserQuery query = new UserQuery(domain);
            query.StartUserName = startUsername;
            query.RetrieveAllUsers = false;

            return userAccountService.Query(query);
        }

        /// <summary>
        /// Retrieves the entry for the specified user.
        /// </summary>
        /// <param name="username">the username to retrieve</param>
        /// <returns>the UserEntry for this user</returns>
        public UserEntry RetrieveUser(string username) {
            UserQuery query = new UserQuery(domain);
            query.UserName = username;

            UserFeed feed = userAccountService.Query(query);

            // It's safe to access Entries[0] here, because the service will
            // have already thrown an exception if the username was invalid.
            return feed.Entries[0] as UserEntry;
        }

        /// <summary>
        /// Updates the specified user account.
        /// </summary>
        /// <param name="entry">The updated entry; modified properties
        /// can include the user's first name, last name, username and
        /// password.</param>
        /// <returns>the updated UserEntry</returns>
        public UserEntry UpdateUser(UserEntry entry) {
            return entry.Update();
        }

        /// <summary>
        /// the AppsService is an application object holding several
        /// real services object. To allow the setting of advanced http properties,
        /// proxies and other things, we allow setting the factory class that is used. 
        /// 
        /// a getter does not make a lot of sense here, as which of the several factories in use
        /// are we getting? It also would give the illusion that you could get one object and then
        /// modify its settings. 
        /// </summary>
        /// <param name="factory">The factory to use for the AppsService</param>
        /// <returns></returns>
        public void SetRequestFactory(IGDataRequestFactory factory) {
            if (factory == null) {
                throw new ArgumentNullException("factory", "The factory object should not be NULL");
            }

            nicknameService.RequestFactory = factory;
            userAccountService.RequestFactory = factory;
            groupsService.RequestFactory = factory;
        }

        /// <summary>
        /// this creates a default AppsService Factory object that can be used to 
        /// be modified and then set using SetRequestFactory()
        /// </summary>
        /// <returns></returns>
        public IGDataRequestFactory CreateRequestFactory() {
            return new GDataGAuthRequestFactory(AppsNameTable.GAppsService, this.applicationName);
        }

        /// <summary>
        /// Suspends a user account.
        /// </summary>
        /// <param name="username">the username whose account you wish to suspend</param>
        /// <returns>the updated UserEntry</returns>
        public UserEntry SuspendUser(string username) {
            UserEntry entry = RetrieveUser(username);
            entry.Login.Suspended = true;
            return UpdateUser(entry);
        }

        /// <summary>
        /// Restores a user account.
        /// </summary>
        /// <param name="username">the username whose account you wish to restore</param>
        /// <returns>the updated UserEntry</returns>
        public UserEntry RestoreUser(string username) {
            UserEntry entry = RetrieveUser(username);
            entry.Login.Suspended = false;
            return UpdateUser(entry);
        }

        /// <summary>
        /// Adds admin privileges for a user.  Note that executing this method
        /// on a user who is already an admin has no effect.
        /// </summary>
        /// <param name="username">the user to make an administrator</param>
        /// <returns>the updated UserEntry</returns>
        public UserEntry AddAdminPrivilege(string username) {
            UserEntry entry = RetrieveUser(username);
            entry.Login.Admin = true;
            return UpdateUser(entry);
        }

        /// <summary>
        /// Removes admin privileges for a user.  Note that executing this method
        /// on a user who is not an admin has no effect.
        /// </summary>
        /// <param name="username">the user from which to revoke admin privileges</param>
        /// <returns>the updated UserEntry</returns>
        public UserEntry RemoveAdminPrivilege(string username) {
            UserEntry entry = RetrieveUser(username);
            entry.Login.Admin = false;
            return UpdateUser(entry);
        }

        /// <summary>
        /// Forces the specified user to change his or her password at the next
        /// login.
        /// </summary>
        /// <param name="username">the user who must change his/her password upon
        /// logging in next</param>
        /// <returns>the updated UserEntry</returns>
        public UserEntry ForceUserToChangePassword(string username) {
            UserEntry entry = RetrieveUser(username);
            entry.Login.ChangePasswordAtNextLogin = true;
            return UpdateUser(entry);
        }

        /// <summary>
        /// Deletes a user account.
        /// </summary>
        /// <param name="username">the username whose account you wish to delete</param>
        public void DeleteUser(string username) {
            UserQuery query = new UserQuery(domain);
            query.UserName = username;

            userAccountService.Delete(query.Uri);
        }

        /// <summary>
        /// Creates a nickname for the specified user.
        /// </summary>
        /// <param name="username">the user account for which you are creating a nickname</param>
        /// <param name="nickname">the nickname for the user account</param>
        /// <returns>the newly created NicknameEntry object</returns>
        public NicknameEntry CreateNickname(string username, string nickname) {
            NicknameQuery query = new NicknameQuery(Domain);

            NicknameEntry entry = new NicknameEntry();
            entry.Login = new LoginElement(username);
            entry.Nickname = new NicknameElement(nickname);

            return nicknameService.Insert(query.Uri, entry);
        }

        /// <summary>
        /// Retrieves all nicknames on this domain.
        /// </summary>
        /// <returns>the feed containing all nickname entries</returns>
        public NicknameFeed RetrieveAllNicknames() {
            NicknameQuery query = new NicknameQuery(Domain);
            return nicknameService.Query(query);
        }

        /// <summary>
        /// Retrieves a page of at most 100 nicknames beginning with the
        /// specified nickname.  Nicknames are ordered case-insensitively
        /// by ASCII value.
        /// </summary>
        /// <param name="startNickname">the first nickname that should appear
        /// in your result set</param>
        /// <returns>the feed containing the matching nickname entries</returns>
        public NicknameFeed RetrievePageOfNicknames(string startNickname) {
            NicknameQuery query = new NicknameQuery(domain);
            query.StartNickname = startNickname;
            query.RetrieveAllNicknames = false;

            return nicknameService.Query(query);
        }

        /// <summary>
        /// Retrieves all nicknames for the specified user.
        /// </summary>
        /// <param name="username">the username for which you wish to retrieve nicknames</param>
        /// <returns>the feed containing all nickname entries for this user</returns>
        public NicknameFeed RetrieveNicknames(string username) {
            NicknameQuery query = new NicknameQuery(Domain);
            query.UserName = username;

            return nicknameService.Query(query);
        }

        /// <summary>
        /// Retrieves the specified nickname.
        /// </summary>
        /// <param name="nickname">the nickname to retrieve</param>
        /// <returns>the resulting NicknameEntry</returns>
        public NicknameEntry RetrieveNickname(string nickname) {
            NicknameQuery query = new NicknameQuery(Domain);
            query.Nickname = nickname;

            NicknameFeed feed = nicknameService.Query(query);

            // It's safe to access Entries[0] here, because the service will
            // have already thrown an exception if the nickname was invalid.
            return feed.Entries[0] as NicknameEntry;
        }

        /// <summary>
        /// Deletes the specified nickname.
        /// </summary>
        /// <param name="nickname">the nickname that you wish to delete</param>
        public void DeleteNickname(string nickname) {
            NicknameQuery query = new NicknameQuery(Domain);
            query.Nickname = nickname;

            nicknameService.Delete(query.Uri);
        }
    }
}
