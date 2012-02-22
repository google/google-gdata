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

namespace Google.GData.Apps.Groups {
    /// <summary>
    /// Base service for accessing Google Groups item feeds from the
    /// Google Apps Google Groups API.
    /// </summary>
    public class GroupsService : AppsPropertyService {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="domain">The hosted domain in which the Google Groups are
        /// being set up</param>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public GroupsService(String domain, String applicationName)
            : base(domain, applicationName) {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
        }

        /// <summary>
        /// Retrieves a group in the domain.
        /// </summary>
        /// <param name="groupId">The ID of the group to be retrieved</param>
        /// <returns>The requested group</returns>
        public GroupEntry RetrieveGroup(String groupId) {
            String requestUri = String.Format("{0}/{1}/{2}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId);

            return Get(requestUri) as GroupEntry;
        }

        /// <summary>
        /// Retrieves all groups in the domain.
        /// </summary>
        /// <returns>The details of the existing groups for the domain</returns>
        public GroupFeed RetrieveAllGroups() {
            String requestUri = String.Format("{0}/{1}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain);

            return QueryExtendedFeed(requestUri, true) as GroupFeed;
        }

        /// <summary>
        /// Retrieves all groups for a member.
        /// </summary>
        /// <param name="memberId">The user for which you want to retrieve group subscriptions</param>
        /// <param name="directOnly">Whether to identify only members with direct association with the group</param>
        /// <returns>The details of the existing groups for the member</returns>
        public GroupFeed RetrieveGroups(String memberId, bool directOnly) {
            String requestUri = String.Format("{0}/{1}?{2}={3}&{4}={5}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                AppsGroupsNameTable.member,
                memberId,
                AppsGroupsNameTable.directOnly,
                directOnly.ToString());

            return QueryExtendedFeed(requestUri, true) as GroupFeed;
        }

        /// <summary>
        /// Creates a new group for the domain.
        /// </summary>
        /// <param name="groupId">The ID of the group</param>
        /// <param name="groupName">The name of the group</param>
        /// <param name="description">The general description of the group</param>
        /// <param name="emailPermission">The permission level of the group</param>
        /// <returns>The entry being created</returns>
        public GroupEntry CreateGroup(String groupId, String groupName, String description, PermissionLevel? emailPermission) {
            String requestUri = String.Format("{0}/{1}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain);

            GroupEntry entry = new GroupEntry();
            entry.GroupId = groupId;
            entry.GroupName = groupName;
            
            if (!string.IsNullOrEmpty(description)) {
                entry.Description = description;
            }

            if (emailPermission != null) {
                entry.EmailPermission = (PermissionLevel)emailPermission;
            }

            return Insert(requestUri, entry);
        }

        /// <summary>
        /// Updates an existing group in the domain.
        /// </summary>
        /// <param name="groupId">The ID of the group to be updated</param>
        /// <param name="groupName">The name of the group</param>
        /// <param name="description">The general description of the group</param>
        /// <param name="emailPermission">The permission level of the group</param>
        /// <returns>The updated entry</returns>
        public GroupEntry UpdateGroup(String groupId, String groupName, String description, PermissionLevel? emailPermission) {
            String requestUri = String.Format("{0}/{1}/{2}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId);

            GroupEntry entry = new GroupEntry();
            entry.EditUri = requestUri;
            entry.GroupId = groupId;
            entry.GroupName = groupName;

            if (!string.IsNullOrEmpty(description)) {
                entry.Description = description;
            }

            if (emailPermission != null) {
                entry.EmailPermission = (PermissionLevel)emailPermission;
            }

            return Update(entry);
        }

        /// <summary>
        /// Deletes a group in the domain.
        /// </summary>
        /// <param name="groupId">The ID of the group to be deleted</param>
        public void DeleteGroup(String groupId) {
            String requestUri = String.Format("{0}/{1}/{2}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId);

            Delete(requestUri);
        }

        /// <summary>
        /// Adds a member to a group.
        /// </summary>
        /// <param name="memberId">Username of the member that is being added to the group</param>
        /// <param name="groupId">The group to which the member is being added</param>
        /// <returns>a <code>MemberEntry</code> containing the results of the
        /// creation</returns>
        public MemberEntry AddMemberToGroup(String memberId, String groupId) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId,
                AppsGroupsNameTable.member);

            MemberEntry entry = new MemberEntry();
            entry.MemberId = memberId;

            return Insert(requestUri, entry);
        }

        /// <summary>
        /// Retrieves all members of a group.
        /// </summary>
        /// <param name="groupId">The ID of the group for which you wish to retrieve a member list</param>
        /// <returns>The list of members for the group</returns>
        public MemberFeed RetrieveAllMembers(String groupId) {
            return RetrieveAllMembers(groupId, true);
        }

        /// <summary>
        /// Retrieves all members of a group.
        /// </summary>
        /// <param name="groupId">The ID of the group for which you wish to retrieve a member list</param>
        /// <param name="includeSuspendedUsers">Whether to include suspended users</param>
        /// <returns>The list of members for the group</returns>
        public MemberFeed RetrieveAllMembers(String groupId, Boolean includeSuspendedUsers) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}?{4}={5}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId,
                AppsGroupsNameTable.member,
                AppsGroupsNameTable.includeSuspendedUsers,
                includeSuspendedUsers.ToString());

            return QueryExtendedFeed(requestUri, true) as MemberFeed;
        }

        /// <summary>
        /// Retrieves a group member.
        /// </summary>
        /// <param name="memberId">Username of the member that is being retrieved from the group</param>
        /// <param name="groupId">The ID of the group for which you wish to retrieve a member</param>
        /// <returns>The retrieved group member</returns>
        public MemberEntry RetrieveMember(String memberId, String groupId) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}/{4}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId,
                AppsGroupsNameTable.member,
                memberId);

            return Get(requestUri) as MemberEntry;
        }

        /// <summary>
        /// Checks whether an user is a group member.
        /// </summary>
        /// <param name="memberEmail">Email of the member that is being checked</param>
        /// <param name="groupId">The ID of the group for which you wish to check the membership</param>
        /// <returns>True if the user is a member of the group, false otherwise</returns>
        public bool IsMember(String memberEmail, String groupId) {
            try {
                MemberEntry entry = RetrieveMember(memberEmail, groupId);
                return entry != null;
            } catch (GDataRequestException e) {
                AppsException appsException = AppsException.ParseAppsException(e);
                if (appsException == null) {
                    return false;
                }

                if (appsException.ErrorCode.Equals(AppsException.EntityDoesNotExist)) {
                    return false;
                } else {
                    throw appsException;
                }
            }
        }

        /// <summary>
        /// Removes a member from a group
        /// </summary>
        /// <param name="memberId">Username of the member that is being removed from the group</param>
        /// <param name="groupId">The group from which the member is being removed</param>
        public void RemoveMemberFromGroup(String memberId, String groupId) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}/{4}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId,
                AppsGroupsNameTable.member,
                memberId);

            Delete(requestUri);
        }

        /// <summary>
        /// Adds an owner to a group.
        /// </summary>
        /// <param name="ownerEmail">Email of the owner that is being added to the group</param>
        /// <param name="groupId">The group to which the member is being added</param>
        /// <returns>a <code>OwnerEntry</code> containing the results of the
        /// creation</returns>
        public OwnerEntry AddOwnerToGroup(String ownerEmail, String groupId) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId,
                AppsGroupsNameTable.owner);

            OwnerEntry entry = new OwnerEntry();
            entry.Email = ownerEmail;

            return Insert(requestUri, entry);
        }

        /// <summary>
        /// Retrieves a group owner.
        /// </summary>
        /// <param name="ownerEmail">Email of the owner that is being retrieved from the group</param>
        /// <param name="groupId">The ID of the group for which you wish to retrieve an owner</param>
        /// <returns>The retrieved group owner</returns>
        public OwnerEntry RetrieveOwner(String ownerEmail, String groupId) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}/{4}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId,
                AppsGroupsNameTable.owner,
                ownerEmail);

            return Get(requestUri) as OwnerEntry;
        }

        /// <summary>
        /// Retrieves all owners of a group.
        /// </summary>
        /// <param name="groupId">The ID of the group for which you wish to retrieve the owner list</param>
        /// <returns>The list of owners for the group</returns>
        public OwnerFeed RetrieveAllOwners(String groupId) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId,
                AppsGroupsNameTable.owner);

            return QueryExtendedFeed(requestUri, true) as OwnerFeed;
        }

        /// <summary>
        /// Checks whether an user is a group owner.
        /// </summary>
        /// <param name="ownerEmail">Email of the owner that is being checked</param>
        /// <param name="groupId">The ID of the group for which you wish to check the ownership</param>
        /// <returns>True if the user is an owner of the group, false otherwise</returns>
        public bool IsOwner(String ownerEmail, String groupId) {
            try {
                OwnerEntry entry = RetrieveOwner(ownerEmail, groupId);
                return entry != null;
            } catch (GDataRequestException e) {
                AppsException appsException = AppsException.ParseAppsException(e);
                if (appsException == null) {
                    return false;
                }

                if (appsException.ErrorCode.Equals(AppsException.EntityDoesNotExist)) {
                    return false;
                } else {
                    throw appsException;
                }
            }
        }

        /// <summary>
        /// Removes an owner from a group
        /// </summary>
        /// <param name="ownerEmail">Email of the owner that is being removed from the group</param>
        /// <param name="groupId">The group from which the owner is being removed</param>
        public void RemoveOwnerFromGroup(String ownerEmail, String groupId) {
            String requestUri = String.Format("{0}/{1}/{2}/{3}/{4}",
                AppsGroupsNameTable.AppsGoogleGroupsBaseFeedUri,
                domain,
                groupId,
                AppsGroupsNameTable.owner,
                ownerEmail);

            Delete(requestUri);
        }

        /// <summary>
        /// Overridden so that new feeds are returned as <code>AppsExtendedFeed</code>s
        /// instead of base <code>AtomFeed</code>s.
        /// </summary>
        /// <param name="sender"> the object which sent the event</param>
        /// <param name="e">FeedParserEventArguments, holds the FeedEntry</param> 
        protected void OnNewFeed(object sender, ServiceEventArgs e) {
            Tracing.TraceMsg("Created new Google Groups Item Feed");
            
            if (e == null) {
                throw new ArgumentNullException("e");
            }
            
            e.Feed = getFeed(e.Uri, e.Service);
        }

        protected override AppsExtendedFeed getFeed(Uri uri, IService service) {
            String baseUri = uri.ToString();
            if (baseUri.Contains("/member")) {
                return new MemberFeed(uri, service);
            } else if (baseUri.Contains("/owner")) {
                return new OwnerFeed(uri, service);
            } else {
                return new GroupFeed(uri, service);
            }
        }
    }

    public class GroupFeed : GenericFeed<GroupEntry> {
        public GroupFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
        }
    }

    public class MemberFeed : GenericFeed<MemberEntry> {
        public MemberFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
        }
    }

    public class OwnerFeed : GenericFeed<OwnerEntry> {
        public OwnerFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
        }
    }
}

