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
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Contacts;
using System.Collections.Generic;

namespace Google.Contacts {
    /// <summary>
    /// the base class for contacts elements
    /// </summary>
    public abstract class ContactBase : Entry {
        /// <summary>
        /// returns if the Contact or Group is deleted
        /// </summary>
        /// <returns>the deleted status of the underlying object or false if no object set yet</returns>
        public bool Deleted {
            get {
                BaseContactEntry b = this.AtomEntry as BaseContactEntry;
                if (b != null) {
                    return b.Deleted;
                }
                return false;
            }
        }

        /// <summary>
        /// returns the extended properties on this object
        /// </summary>
        /// <returns>the properties on the underlying object, or null if no 
        /// object set yet</returns>
        public ExtensionCollection<ExtendedProperty> ExtendedProperties {
            get {
                BaseContactEntry b = this.AtomEntry as BaseContactEntry;
                if (b != null) {
                    return b.ExtendedProperties;
                }
                return null;
            }
        }
    }

    /// <summary>
    /// the Contact entry for a Contacts Feed
    /// </summary>
    public class Contact : ContactBase {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject() {
            if (this.AtomEntry == null) {
                this.AtomEntry = new ContactEntry();
            }
        }

        /// <summary>
        /// readonly accessor for the ContactEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public ContactEntry ContactEntry {
            get {
                return this.AtomEntry as ContactEntry;
            }
        }

        /// <summary>
        /// convenience accessor to find the primary Email
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public EMail PrimaryEmail {
            get {
                EnsureInnerObject();
                return this.ContactEntry.PrimaryEmail;
            }
        }

        /// <summary>
        /// convenience accessor to find the primary Phonenumber
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public PhoneNumber PrimaryPhonenumber {
            get {
                if (this.ContactEntry != null) {
                    return this.ContactEntry.PrimaryPhonenumber;
                }
                return null;
            }
        }

        /// <summary>
        /// convenience accessor to find the primary PostalAddress
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public StructuredPostalAddress PrimaryPostalAddress {
            get {
                EnsureInnerObject();
                return this.ContactEntry.PrimaryPostalAddress;
            }
        }

        /// <summary>
        /// convenience accessor to find the primary IMAddress
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public IMAddress PrimaryIMAddress {
            get {
                EnsureInnerObject();
                return this.ContactEntry.PrimaryIMAddress;
            }
        }

        /// <summary>
        /// returns the groupmembership info on this object
        /// </summary>
        /// <returns></returns>
        public ExtensionCollection<GroupMembership> GroupMembership {
            get {
                EnsureInnerObject();
                return this.ContactEntry.GroupMembership;
            }
        }

        /// <summary>
        /// getter/setter for the email extension element
        /// </summary>
        public ExtensionCollection<EMail> Emails {
            get {
                EnsureInnerObject();
                return this.ContactEntry.Emails;
            }
        }

        /// <summary>
        /// getter/setter for the IM extension element
        /// </summary>
        public ExtensionCollection<IMAddress> IMs {
            get {
                EnsureInnerObject();
                return this.ContactEntry.IMs;
            }
        }

        /// <summary>
        /// returns the phone number collection
        /// </summary>
        public ExtensionCollection<PhoneNumber> Phonenumbers {
            get {
                EnsureInnerObject();
                return this.ContactEntry.Phonenumbers;
            }
        }

        /// <summary>
        /// returns the postal address collection
        /// </summary>
        public ExtensionCollection<StructuredPostalAddress> PostalAddresses {
            get {
                EnsureInnerObject();
                return this.ContactEntry.PostalAddresses;
            }
        }

        /// <summary>
        /// returns the organization collection
        /// </summary>
        public ExtensionCollection<Organization> Organizations {
            get {
                EnsureInnerObject();
                return this.ContactEntry.Organizations;
            }
        }

        /// <summary>
        /// returns the language collection
        /// </summary>
        public ExtensionCollection<Language> Languages {
            get {
                EnsureInnerObject();
                return this.ContactEntry.Languages;
            }
        }

        /// <summary>
        /// retrieves the Uri of the Photo Link. To set this, you need to create an AtomLink object
        /// and add/replace it in the atomlinks colleciton. 
        /// </summary>
        /// <returns></returns>
        public Uri PhotoUri {
            get {
                EnsureInnerObject();
                return this.ContactEntry.PhotoUri;
            }
        }

        /// <summary>
        /// if a photo is present on this contact, it will have an etag associated with it,
        /// that needs to be used when you want to delete or update that picture.
        /// </summary>
        /// <returns>the etag value as a string</returns>
        public string PhotoEtag {
            get {
                EnsureInnerObject();
                return this.ContactEntry.PhotoEtag;
            }
            set {
                EnsureInnerObject();
                this.ContactEntry.PhotoEtag = value;
            }
        }

        /// <summary>
        /// returns the location associated with a contact
        /// </summary>
        /// <returns></returns>
        public string Location {
            get {
                EnsureInnerObject();
                return this.ContactEntry.Location;
            }
            set {
                EnsureInnerObject();
                this.ContactEntry.Location = value;
            }
        }
        /// <summary>
        /// the contacts name object
        /// </summary>
        public Name Name {
            get {
                EnsureInnerObject();
                if (this.ContactEntry.Name == null)
                    this.ContactEntry.Name = new Name();
                return this.ContactEntry.Name;
            }
            set {
                EnsureInnerObject();
                this.ContactEntry.Name = value;
            }
        }
    }

    /// <summary>
    /// the group entry for a contacts groups Feed
    /// </summary>
    public class Group : ContactBase {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject() {
            if (this.AtomEntry == null) {
                this.AtomEntry = new GroupEntry();
            }
        }

        /// <summary>
        /// readonly accessor for the YouTubeEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public GroupEntry GroupEntry {
            get {
                return this.AtomEntry as GroupEntry;
            }
        }

        /// <summary>
        /// returns the systemgroup id, if this groupentry represents 
        /// a system group. 
        /// The values of the system group ids corresponding to these 
        /// groups can be found in the Reference Guide for the Contacts Data API.
        /// Currently the values can be Contacts, Friends, Family and Coworkers
        /// </summary>
        /// <returns>the system group or null</returns>
        public string SystemGroup {
            get {
                EnsureInnerObject();
                return this.GroupEntry.SystemGroup;
            }
        }

    }

    /// <summary>
    /// The Contacts Data API provides two types of feed: contacts feed and 
    /// contact groups feed.
    /// The feeds are private read/write feeds that can be used to view and
    /// manage a user's contacts/groups. Since they are private, a programmer 
    /// can access them only by using an authenticated request. That is, the
    ///  request must contain an authentication token for the user whose 
    /// contacts you want to retrieve.
    /// </summary>
    ///  <example>
    ///         The following code illustrates a possible use of   
    ///          the <c>ContactsRequest</c> object:  
    ///          <code>    
    ///            RequestSettings settings = new RequestSettings("yourApp");
    ///            settings.PageSize = 50; 
    ///            settings.AutoPaging = true;
    ///            ContactsRequest c = new ContactsRequest(settings);
    ///            Feed&lt;Contacts&gt; feed = c.GetContacts();
    ///     
    ///         foreach (Contact contact in feed.Entries)
    ///         {
    ///              Console.WriteLine(contact.Title);
    ///         }
    ///  </code>
    ///  </example>
    public class ContactsRequest : FeedRequest<ContactsService> {
        /// <summary>
        /// default constructor for a YouTubeRequest
        /// </summary>
        /// <param name="settings"></param>
        public ContactsRequest(RequestSettings settings)
            : base(settings) {
            this.Service = new ContactsService(settings.Application);
            PrepareService();
        }

        /// <summary>
        /// returns a Feed of contacts for the default user
        /// </summary>
        /// <returns>a feed of Contacts</returns>
        public Feed<Contact> GetContacts() {
            return GetContacts(null);
        }

        /// <summary>
        /// returns a Feed of contacts for the given user
        /// </summary>
        /// <param name="user">the username</param>
        /// <returns>a feed of Contacts</returns>
        public Feed<Contact> GetContacts(string user) {
            ContactsQuery q = PrepareQuery<ContactsQuery>(ContactsQuery.CreateContactsUri(user));
            return PrepareFeed<Contact>(q);
        }

        /// <summary>
        /// returns a feed of Groups for the default user
        /// </summary>
        /// <returns>a feed of Groups</returns>
        public Feed<Group> GetGroups() {
            return GetGroups(null);
        }

        /// <summary>
        ///  returns a feed of Groups for the given user
        /// </summary>
        /// <param name="user">the user for whom to retrieve the feed</param>
        /// <returns>a feed of Groups</returns>
        public Feed<Group> GetGroups(string user) {
            GroupsQuery q = PrepareQuery<GroupsQuery>(GroupsQuery.CreateGroupsUri(user));
            return PrepareFeed<Group>(q);
        }

        /// <summary>
        /// returns the photo stream for a given contact. If there is no photo,
        /// the 404 is catched and null is returned.
        /// </summary>
        /// <param name="c">the contact that you want to get the photo of</param>
        /// <returns></returns>
        public Stream GetPhoto(Contact c) {
            Stream retStream = null;
            try {
                if (c.PhotoUri != null) {
                    retStream = this.Service.Query(c.PhotoUri, c.PhotoEtag);
                }
            } catch (GDataRequestException e) {
                HttpWebResponse r = e.Response as HttpWebResponse;
                if (r != null && r.StatusCode != HttpStatusCode.NotFound) {
                    throw;
                }
            }
            return retStream;
        }

        /// <summary>
        /// sets the photo of a given contact entry
        /// </summary>
        /// <param name="c">the contact that should be modified</param>
        /// <param name="photoStream">a stream to an JPG image</param>
        /// <returns></returns>
        public void SetPhoto(Contact c, Stream photoStream) {
            Stream res = this.Service.StreamSend(c.PhotoUri, photoStream, GDataRequestType.Update, "image/jpg", null, c.PhotoEtag);
            GDataReturnStream r = res as GDataReturnStream;
            if (r != null) {
                c.PhotoEtag = r.Etag;
            }
            res.Close();
        }

        /// <summary>
        /// sets the photo of a given contact entry
        /// </summary>
        /// <param name="c">the contact that should be modified</param>
        /// <param name="photoStream">a stream to an JPG image</param>
        /// <param name="mimeType">specifies the MIME type of the image, e.g. image/jpg</param>
        /// <returns></returns>
        public void SetPhoto(Contact c, Stream photoStream, string mimeType) {
            Stream res = this.Service.StreamSend(c.PhotoUri, photoStream, GDataRequestType.Update, mimeType, null, c.PhotoEtag);
            res.Close();
        }
    }
}
