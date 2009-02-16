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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/

#define USE_TRACING

using System;
using Google.GData.Client;
using Google.GData.Extensions;


namespace Google.GData.Contacts {

    /// <summary>
    /// short table to hold the namespace and the prefix
    /// </summary>
    public class ContactsNameTable 
    {
        /// <summary>static string to specify the Contacts namespace supported</summary>
        public const string NSContacts = "http://schemas.google.com/contact/2008"; 
        /// <summary>static string to specify the Google Contacts prefix used</summary>
        public const string contactsPrefix = "gContact"; 

        /// <summary>
        /// Group Member ship info element string
        /// </summary>
        public const string GroupMembershipInfo = "groupMembershipInfo";

        /// <summary>
        /// SystemGroup element, indicating that this entry is a system group
        /// </summary>
        public const string SystemGroupElement = "systemGroup";
    }



    /// <summary>
    /// an element is defined that represents a group to which the contact belongs   
    /// </summary>
    public class GroupMembership: SimpleElement
    {
        /// <summary>the  href attribute </summary>
        public const string XmlAttributeHRef = "href";
        /// <summary>the deleted attribute </summary>
        public const string XmlAttributeDeleted = "deleted";

        /// <summary>
        /// default constructor 
        /// </summary>
        public GroupMembership()
        : base(ContactsNameTable.GroupMembershipInfo, ContactsNameTable.contactsPrefix, ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(XmlAttributeHRef, null);
            this.Attributes.Add(XmlAttributeDeleted, null);
        }

       /////////////////////////////////////////////////////////////////////
       /// <summary>Identifies the group to which the contact belongs or belonged. 
       /// The group is referenced by its id.</summary>
       //////////////////////////////////////////////////////////////////////
       public string HRef
       {
           get
           {
               return this.Attributes[XmlAttributeHRef] as string;
           }
           set
           {
               this.Attributes[XmlAttributeHRef] = value;
           }
       }

       /////////////////////////////////////////////////////////////////////
       /// <summary>Means, that the group membership was removed for the contact. 
       /// This attribute will only be included if showdeleted is specified 
       /// as query parameter, otherwise groupMembershipInfo for groups a contact 
       /// does not belong to anymore is simply not returned.</summary>
       //////////////////////////////////////////////////////////////////////
       public string Deleted
       {
           get
           {
               return this.Attributes[XmlAttributeDeleted] as string;
           }
       }
    }


    /// <summary>
    /// extension element to represent a system group
    /// </summary>
    public class SystemGroup : SimpleElement
    {
        /// <summary>
        /// id attribute for the system group element
        /// </summary>
        /// <returns></returns>
        public const string XmlAttributeId = "id";

        /// <summary>
        /// default constructor 
        /// </summary>
        public SystemGroup()
        : base(ContactsNameTable.SystemGroupElement, ContactsNameTable.contactsPrefix, ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(XmlAttributeId, null);
        }

       /////////////////////////////////////////////////////////////////////
       /// <summary>Identifies the system group. Note that you still need 
       /// to use the group entries href membership to retrieve the group
       /// </summary>
       //////////////////////////////////////////////////////////////////////
       public string Id
       {
           get
           {
               return this.Attributes[XmlAttributeId] as string;
           }
       }
    }

    /// <summary>
    /// abstract class for a basecontactentry, used for contacts and groups
    /// </summary>
    public abstract class BaseContactEntry : AbstractEntry, IContainsDeleted
    {
        private ExtensionCollection<ExtendedProperty> xproperties;


        /// <summary>
        /// Constructs a new BaseContactEntry instance 
        /// to indicate that it is an event.
        /// </summary>
        public BaseContactEntry()
        : base()
        {
            Tracing.TraceMsg("Created BaseContactEntry Entry");
            this.AddExtension(new ExtendedProperty());
        }



        /// <summary>
        /// returns the extended properties on this object
        /// </summary>
        /// <returns></returns>
        public ExtensionCollection<ExtendedProperty> ExtendedProperties
        {
            get 
            {
                if (this.xproperties == null)
                {
                    this.xproperties = new ExtensionCollection<ExtendedProperty>(this); 
                }
                return this.xproperties;
            }
        }


        /// <summary>
        /// if this is a previously deleted contact, returns true
        /// to delete a contact, use the delete method
        /// </summary>
        public bool Deleted
        {
            get
            {
                if (FindExtension(GDataParserNameTable.XmlDeletedElement,
                                     BaseNameTable.gNamespace) != null) 
                {
                    return true;
                }
                return false;
            }
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Group feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class GroupEntry : BaseContactEntry
    {

        /// <summary>
        /// default contact term string for the contact relationship link
        /// </summary>
        public static string GroupTerm = "http://schemas.google.com/contact/2008#group";
        /// <summary>`
        /// Category used to label entries that contain contact extension data.
        /// </summary>
        public static AtomCategory GROUP_CATEGORY =
        new AtomCategory(GroupEntry.GroupTerm, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new ContactEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public GroupEntry()
        : base()
        {
            Tracing.TraceMsg("Created Group Entry");
            this.AddExtension(new SystemGroup());
            Categories.Add(GROUP_CATEGORY);
        }

        /// <summary>
        /// typed override of the Update method
        /// </summary>
        /// <returns></returns>
        public new GroupEntry Update()
        {
            return base.Update() as GroupEntry;
        }

        /// <summary>
        /// returns the systemgroup id, if this groupentry represents 
        /// a system group. 
        /// The values of the system group ids corresponding to these 
        /// groups can be found in the Reference Guide for the Contacts Data API.
        /// Currently the values can be Contacts, Friends, Family and Coworkers
        /// </summary>
        /// <returns></returns>
        public string SystemGroup
        {
            get
            {
                SystemGroup sg = FindExtension(ContactsNameTable.SystemGroupElement, 
                                  ContactsNameTable.NSContacts) as SystemGroup;
                if (sg != null)
                {
                    return sg.Id;
                }
                return null;
            }
        }


    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class ContactEntry : BaseContactEntry
    {

        /// <summary>
        /// default contact term string for the contact relationship link
        /// </summary>
        public static string ContactTerm = "http://schemas.google.com/contact/2008#contact";
        /// <summary>`
        /// Category used to label entries that contain contact extension data.
        /// </summary>
        public static AtomCategory CONTACT_CATEGORY =
        new AtomCategory(ContactEntry.ContactTerm, new AtomUri(BaseNameTable.gKind));


        private ExtensionCollection<EMail> emails;
        private ExtensionCollection<IMAddress> ims;
        private ExtensionCollection<PhoneNumber> phonenumbers;
        private ExtensionCollection<PostalAddress> postals;
        private ExtensionCollection<Organization> organizations;
        private ExtensionCollection<GroupMembership> groups;


        /// <summary>
        /// Constructs a new ContactEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public ContactEntry()
        : base()
        {
            Tracing.TraceMsg("Created Contact Entry");
            Categories.Add(CONTACT_CATEGORY);
            this.AddExtension(new GroupMembership());
            ContactsExtensions.AddExtension(this);
        }


        /// <summary>
        /// typed override of the Update method
        /// </summary>
        /// <returns></returns>
        public new ContactEntry Update()
        {
            return base.Update() as ContactEntry;
        }

        /// <summary>
        /// convienience accessor to find the primary Email
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public EMail PrimaryEmail
        {
            get
            {
                foreach (EMail e in this.Emails)
                {
                    if (e.Primary == true)
                    {
                        return e;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// convienience accessor to find the primary Phonenumber
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public PhoneNumber PrimaryPhonenumber
        {
            get
            {
                foreach (PhoneNumber p in this.Phonenumbers)
                {
                    if (p.Primary == true)
                    {
                        return p;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// convienience accessor to find the primary PostalAddress
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public PostalAddress PrimaryPostalAddress
        {
            get
            {
                foreach (PostalAddress p in this.PostalAddresses)
                {
                    if (p.Primary == true)
                    {
                        return p;
                    }
                }
                return null;
            }
        }
        
        /// <summary>
        /// convienience accessor to find the primary IMAddress
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public IMAddress PrimaryIMAddress
        {
            get
            {
                foreach (IMAddress im in this.IMs)
                {
                    if (im.Primary == true)
                    {
                        return im;
                    }
                }
                return null;
            }
        }




        /// <summary>
        /// returns the groupmembership info on this object
        /// </summary>
        /// <returns></returns>
        public ExtensionCollection<GroupMembership> GroupMembership
        {
            get 
            {
                if (this.groups == null)
                {
                    this.groups = new ExtensionCollection<GroupMembership>(this); 
                }
                return this.groups;
            }
        }



        /// <summary>
        /// getter/setter for the email extension element
        /// </summary>
        public ExtensionCollection<EMail> Emails
        {
            get 
            {
                if (this.emails == null)
                {
                    this.emails = new ExtensionCollection<EMail>(this); 
                }
                return this.emails;
            }
        }

        /// <summary>
        /// getter/setter for the IM extension element
        /// </summary>
        public ExtensionCollection<IMAddress> IMs
        {
            get 
            {
                if (this.ims == null)
                {
                    this.ims = new ExtensionCollection<IMAddress>(this); 
                }
                return this.ims;
            }
        }

        /// <summary>
        /// returns the phonenumber collection
        /// </summary>
        public ExtensionCollection<PhoneNumber> Phonenumbers
        {
            get 
            {
                if (this.phonenumbers == null)
                {
                    this.phonenumbers = new ExtensionCollection<PhoneNumber>(this); 
                }
                return this.phonenumbers;
            }
        }

        /// <summary>
        /// returns the phonenumber collection
        /// </summary>
        public ExtensionCollection<PostalAddress> PostalAddresses
        {
            get 
            {
                if (this.postals == null)
                {
                    this.postals = new ExtensionCollection<PostalAddress>(this); 
                }
                return this.postals;
            }
        }

        /// <summary>
        /// returns the phonenumber collection
        /// </summary>
        public ExtensionCollection<Organization> Organizations
        {
            get 
            {
                if (this.organizations == null)
                {
                    this.organizations = new ExtensionCollection<Organization>(this); 
                }
                return this.organizations;
            }
        }

        /// <summary>
        /// retrieves the Uri of the Photo Link. To set this, you need to create an AtomLink object
        /// and add/replace it in the atomlinks colleciton. 
        /// </summary>
        /// <returns></returns>
        public Uri PhotoUri
        {
            get 
            {
                AtomLink link = this.Links.FindService(GDataParserNameTable.ServicePhoto, null);
                return link == null ? null : new Uri(link.HRef.ToString());
            }
        }

        /// <summary>
        /// if a photo is present on this contact, it will have an etag associated with it,
        /// that needs to be used when you want to delete or update that picture.
        /// </summary>
        /// <returns>the etag value as a string</returns>
        public string PhotoEtag
        {
            get
            {
                AtomLink link = this.PhotoLink;
                if (link != null)
                {
                    foreach (XmlExtension x in link.ExtensionElements)
                    {
                        if (x.XmlNameSpace == GDataParserNameTable.gNamespace && x.XmlName == GDataParserNameTable.XmlEtagAttribute)
                        {
                            return x.Node.Value;
                        }
                    }
                }
                return null;
            }
            set 
            {
                AtomLink link = this.PhotoLink;
                if (link != null)
                {
                    foreach (XmlExtension x in link.ExtensionElements)
                    {
                        if (x.XmlNameSpace == GDataParserNameTable.gNamespace && x.XmlName == GDataParserNameTable.XmlEtagAttribute)
                        {
                            x.Node.Value = value;
                        }
                    }
                }
            }
        }

        private AtomLink PhotoLink
        {
            get 
            {
                AtomLink link = this.Links.FindService(GDataParserNameTable.ServicePhoto, null);
                return link;
            }
        }
    }
}

