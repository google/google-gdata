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
using System.Text;
using Google.GData.Apps;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.Groups
{
    /// <summary>
    /// Base service for accessing Google Groups item feeds from the
    /// Google Apps Google Groups API.
    /// </summary>
    public class GroupsService : Service
    {
        /// <summary>
        /// URL suffixes for the Google Groups tasks
        /// </summary>
        public const String memberUriSuffix = "member";
        public const String ownerUriSuffix = "owner";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="domain">The hosted domain in which the Google Groups are
        /// being set up</param>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public GroupsService(String domain, String applicationName)
            : base(AppsNameTable.GAppsService, applicationName)
        {
            this.domain = domain;
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewGroupsItemEntry);
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        private String domain;        

        /// <summary>
        /// Accessor for Domain property.
        /// </summary>
        /// <param name="domain">The hosted domain in which the Google Groups are
        /// being set up</param>
        public String Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        /// <summary>
        /// overwritten Query method
        /// </summary>
        /// <param name="uri">The URI for the query</param>
        /// <returns>the retrieved AppsExtendedFeed</returns>
        public AppsExtendedFeed QueryGroups(Uri uri)
        {
            try
            {
                Stream feedStream = Query(uri);
                AppsExtendedFeed feed = new AppsExtendedFeed(uri, this);
                feed.Parse(feedStream, AlternativeFormat.Atom);
                feedStream.Close();
                if (true)
                {
                    AtomLink next, prev = null;
                    while ((next = feed.Links.FindService("next", null)) != null && next != prev)
                    {
                        feedStream = Query(new Uri(next.HRef.ToString()));
                        feed.Parse(feedStream, AlternativeFormat.Atom);
                        feedStream.Close();
                        prev = next;
                    }
                }
                return feed;
            }
            catch (GDataRequestException e)
            {
                AppsException a = AppsException.ParseAppsException(e);
                throw (a == null ? e : a);
            }
        }

        /// <summary>
        /// Creates a new group
        /// </summary>
        /// <param name="groupId">The groupId (required) argument identifies the ID of the new group.</param>
        /// <param name="groupName">The groupName (required) argument identifies the name of the group to 
        /// which the address is being added.</param>
        /// <param name="description">The description argument provides a general description of the group.</param>
        /// <param name="emailPermission">The emailPermission argument sets the permissions level of the group.</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
        public AppsExtendedEntry CreateGroup(String groupId, String groupName, String description, String emailPermission)
        {
            Uri createGroupUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
               + domain);
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(
                new PropertyElement(AppsGroupsNameTable.groupId, groupId));
            entry.Properties.Add(
                new PropertyElement(AppsGroupsNameTable.groupName, groupName));
            if (description != null && description != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(AppsGroupsNameTable.description, description));
            if (emailPermission != null && emailPermission != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(AppsGroupsNameTable.emailPermission, emailPermission));
            return base.Insert(createGroupUri, entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Updates a group
        /// </summary>
        /// <param name="groupId">The groupId (required) argument identifies the ID of the new group.</param>
        /// <param name="groupName">The groupName (required) argument identifies the name of the group to 
        /// which the address is being added.</param>
        /// <param name="description">The description argument provides a general description of the group.</param>
        /// <param name="emailPermission">The emailPermission argument sets the permissions level of the group.</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// update</returns>
        public AppsExtendedEntry UpdateGroup(String groupId, String groupName, String description, String emailPermission)
        {
            Uri updateGroupUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
               + domain + "/" + groupId);
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = updateGroupUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGroupsNameTable.groupId, groupId));
            if (groupName != null && groupName != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGroupsNameTable.groupName, groupName));
            if (description != null && description != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGroupsNameTable.description, description));
            if (emailPermission != null && emailPermission != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGroupsNameTable.emailPermission, emailPermission));
            return base.Update((AtomEntry)entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Retrieves a group by its groupId
        /// </summary>
        /// <param name="groupId">The groupId argument identifies the group</param>       
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// query</returns>
        public AppsExtendedEntry RetrieveGroup(String groupId)
        {
            Uri getGroupUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
                + domain + "/" + groupId);
            AppsExtendedFeed AppsExtendedFeed = QueryGroups(getGroupUri);
            if (AppsExtendedFeed.Entries.Count > 0)
                return AppsExtendedFeed.Entries[0] as AppsExtendedEntry;
            return null;
        }

        /// <summary>
        /// Retrieves all the groups
        /// </summary>      
        /// <returns>a <code>AppsExtendedFeed</code> containing all the <code>AppsExtendedEntry</code>
        /// from the Domain</returns>
        public AppsExtendedFeed RetrieveAllGroups()
        {
            Uri getDomainGroupsUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
               + domain);
            return QueryGroups(getDomainGroupsUri);
        }

        /// <summary>
        /// Retrieves a page from all the groups
        /// </summary>
        /// <param name="startKey">resume key from the previous response (used for pagination)</param>
        /// <returns>a <code>AppsExtendedFeed</code> containing all the <code>AppsExtendedEntry</code>
        /// from the Domain</returns>
        public AppsExtendedFeed RetrievePageOfGroups(String startKey)
        {
            Uri getDomainGroupsUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
                + domain + "?start=" + startKey);
            return QueryGroups(getDomainGroupsUri);
        }

        /// <summary>
        /// Retrieves groups of a member or groups of another group.
        /// </summary>
        /// <param name="memberId">username or groupId to retrieve the groups</param>
        /// <param name="directOnly">retrieve groups where the memberId is direct member or not</param>
        /// <returns>a <code>AppsExtendedFeed</code> containing all the <code>AppsExtendedEntry</code>
        /// from the Domain</returns>
        public AppsExtendedFeed RetrieveGroups(String memberId, Boolean directOnly)
        {
            Uri getGroupUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
               + domain + "?member=" + memberId + "&directOnly=" + directOnly.ToString());
            return QueryGroups(getGroupUri);
        }

        /// <summary>
        /// Deletes a group using its Id.
        /// </summary>
        /// <param name="groupId">Groups's id</param>
        public void DeleteGroup(String groupId)
        {
            Uri deleteGroupUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
               + domain + "/" + groupId);
            base.Delete(deleteGroupUri);
        }

        /// <summary>
        /// Adds a member to a group
        /// </summary>
        /// <param name="memberId">username or groupId to retrieve the groups</param>
        /// <param name="groupId">Groups's id</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
        public AppsExtendedEntry AddMemberToGroup(String memberId, String groupId)
        {
            Uri addMemberToGroupUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
               + domain + "/" + groupId + "/" + memberUriSuffix);
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(
                new PropertyElement(AppsGroupsNameTable.memberId, memberId));
            return base.Insert(addMemberToGroupUri, entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Retrieves all non-suspened members of the group
        /// </summary>
        /// <param name="groupId">Groups's id</param>
        /// <returns>a <code>AppsExtendedFeed</code> containing all the <code>AppsExtendedEntry</code>
        /// from the Domain</returns>
        public AppsExtendedFeed RetrieveAllMembers(String groupId)
        {
            return RetrieveAllMembers(groupId, false);
        }

        /// <summary>
        /// Retrieves all members of the group
        /// </summary>
        /// <param name="groupId">Groups's id</param>
        /// <param name="includeSuspendedUsers">When set to true, will also return suspended users that
        /// are members of the group.</param>
        /// <returns>a <code>AppsExtendedFeed</code> containing all the <code>AppsExtendedEntry</code>
        /// from the Domain</returns>
        public AppsExtendedFeed RetrieveAllMembers(String groupId, Boolean includeSuspendedUsers)
        {
            Uri getGroupMembersUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
               + domain + "/" + groupId + "/" + memberUriSuffix + "?includeSuspendedUsers="
               + includeSuspendedUsers);
            return QueryGroups(getGroupMembersUri);
        }

        /// <summary>
        /// Retrieves the members of the group
        /// </summary>
        /// <param name="memberId">username or groupId to retrieve</param>
        /// <param name="groupId">Groups's id</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// query</returns>
        public AppsExtendedEntry RetrieveMember(String memberId, String groupId)
        {
            Uri memberUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
                    + domain + "/" + groupId + "/" + memberUriSuffix + "/" + memberId);
            AppsExtendedFeed AppsExtendedFeed = QueryGroups(memberUri);
            if (AppsExtendedFeed.Entries.Count == 0)
                return null;
            else
                return AppsExtendedFeed.Entries[0] as AppsExtendedEntry;
        }

        /// <summary>
        /// Checks if a user or a group is member of a group
        /// </summary>
        /// <param name="memberId">username or groupId to check</param>
        /// <param name="groupId">Groups's id</param>
        /// <returns>a <code>Boolean</code></returns>
        public Boolean IsMember(String memberId, String groupId)
        {
            try
            {
                Uri isMemberUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
                    + domain + "/" + groupId + "/" + memberUriSuffix + "/" + memberId);
                AppsExtendedFeed AppsExtendedFeed = QueryGroups(isMemberUri);
                return (AppsExtendedFeed.Entries.Count > 0);
            }
            catch (AppsException appsException)
            {
                if (appsException.ErrorCode.Equals("1301"))
                    return false;
                else
                    throw appsException;
            }
        }

        /// <summary>
        /// Remove a member from a group
        /// </summary>
        /// <param name="memberId">username or groupId to remove</param>
        /// <param name="groupId">Groups's id</param>
        public void RemoveMemberFromGroup(String memberId, String groupId)
        {
            Uri removeMemberUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
                + domain + "/" + groupId + "/" + memberUriSuffix + "/" + memberId);
            base.Delete(removeMemberUri);
        }

        /// <summary>
        /// Adds an owner to a group
        /// </summary>
        /// <param name="email">owner's Email to add to the group</param>
        /// <param name="groupId">Groups's id</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
        public AppsExtendedEntry AddOwnerToGroup(String email, String groupId)
        {
            Uri addMemberToGroupUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
               + domain + "/" + groupId + "/" + ownerUriSuffix);
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(
                new PropertyElement(AppsGroupsNameTable.email, email));
            return base.Insert(addMemberToGroupUri, entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Retrieves all owners of the group
        /// </summary>
        /// <param name="groupId">Groups's id</param>
        /// <returns>a <code>AppsExtendedFeed</code> containing all the <code>AppsExtendedEntry</code>
        /// from the Domain</returns>
        public AppsExtendedFeed RetrieveGroupOwners(String groupId)
        {
            Uri getGroupOwnersUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
               + domain + "/" + groupId + "/" + ownerUriSuffix);
            return QueryGroups(getGroupOwnersUri);
        }

        /// <summary>
        /// Retrieves the owner of the group
        /// </summary>
        /// <param name="email">email of the owner to retrieve</param>
        /// <param name="groupId">Groups's id</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// query</returns>
        public AppsExtendedEntry RetrieveOwner(String email, String groupId)
        {
            Uri ownerUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
                    + domain + "/" + groupId + "/" + ownerUriSuffix + "/" + email);
            AppsExtendedFeed AppsExtendedFeed = QueryGroups(ownerUri);
            if (AppsExtendedFeed.Entries.Count == 0)
                return null;
            else
                return AppsExtendedFeed.Entries[0] as AppsExtendedEntry;
        }

        /// <summary>
        /// Checks if a user or a group is owner of a group.
        /// </summary>
        /// <param name="email">owner's Email to check</param>
        /// <param name="groupId">Groups's id</param>
        /// <returns>a <code>Boolean</code></returns>
        public Boolean IsOwner(String email, String groupId)
        {
            try
            {
                Uri isOwnerUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
                    + domain + "/" + groupId + "/" + ownerUriSuffix + "/" + email);
                AppsExtendedFeed AppsExtendedFeed = QueryGroups(isOwnerUri);
                return (AppsExtendedFeed.Entries.Count > 0);
            }
            catch (AppsException appsException)
            {
                if (appsException.ErrorCode.Equals("1301"))
                    return false;
                else
                    throw appsException;
            }
        }

        /// <summary>
        /// Remove a owner from a group.
        /// </summary>
        /// <param name="email">owner's Email to remove</param>
        /// <param name="groupId">Groups's id</param>
        public void RemoveOwnerFromGroup(String email, String groupId)
        {
            Uri removeOwnerUri = new Uri(AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri + "/"
                + domain + "/" + groupId + "/" + ownerUriSuffix + "/" + email);
            base.Delete(removeOwnerUri);
        }

        /// <summary>
        /// Event handler. Called when a new Google Groups entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the evet</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewGroupsItemEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry)
            {
                e.Entry = new AppsExtendedEntry();
            }
        }

        /// <summary>
        /// Overridden so that new feeds are returned as <code>GoogleMailSettingsFeed</code>s
        /// instead of base <code>AtomFeed</code>s.
        /// </summary>
        /// <param name="sender"> the object which sent the event</param>
        /// <param name="e">FeedParserEventArguments, holds the FeedEntry</param> 
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Google Groups Item Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            e.Feed = new AppsExtendedFeed(e.Uri, e.Service);
        }
    }
}
